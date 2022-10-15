using MultiGame.Client;
using MultiGame.Object;
using MultiGame.UserPanel;
using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiGame
{
    public partial class GameManager
    {
        private class MessageManager
        {

            private GameManager gameManager;

            public MessageManager(GameManager gameManager)
            {
                this.gameManager = gameManager;
            }

            private byte[] remainedMessage = null;

            // 메시지를 해석 후 실행
            public void ParseMessage(byte[] message)
            {
                // 만약 저번 해석때 메시지가 남았더라면 남은 메시지와 이어붙임
                byte[] message2;
                if (remainedMessage == null)
                {
                    message2 = message;
                }
                else
                {
                    message2 = new byte[remainedMessage.Length + message.Length];
                    Array.Copy(remainedMessage, message2, remainedMessage.Length);
                    Array.Copy(message, 0, message2, remainedMessage.Length, message.Length);
                }

                MessageConverter converter = new MessageConverter(message2);

                while (true)
                {
                    bool result = converter.NextMessage();

                    // 다음 메시지가 없으면 종료
                    if (result == false)
                    {
                        remainedMessage = converter.RemainMessage;
                        break;
                    }

                    byte protocol = converter.Protocol;

                    switch (protocol)
                    {
                        // 다른 클라이언트의 캐릭터 위치 수신
                        case Protocols.S_LOCATION_OTHER:
                            {
                                Location(converter);
                            }
                            break;
                        // 플레이어가 쳐다보는 방향 ( true : 오른쪽 )
                        case Protocols.S_LOOK_DIRECTION_OTHER:
                            {
                                LookDirection(converter);
                            }
                            break;
                        // 서버에 의해 캐릭터가 움직여짐
                        case Protocols.S_MOVE:
                            {
                                Move(converter);
                            }
                            break;
                        // 오브젝트 생성
                        case Protocols.S_NEW_OBJECT:
                            {
                                NewObject(converter);
                            }
                            break;
                        // 서버로부터 오브젝트 이벤트 수신
                        case Protocols.S_OBJECT_EVENT:
                            {
                                ObjectEvent(converter);
                            }
                            break;
                        // 다른 클라이언트가 방을 나감
                        case Protocols.S_EXIT_ROOM_OTHER:
                            {
                                ExitRoom(converter);
                            }
                            break;
                        // 방에  입장
                        case Protocols.RES_ENTER_ROOM:
                            {
                                EnterRoom(converter);
                            }
                            break;
                        case Protocols.S_ENTER_ROOM_OTHER:
                            {
                                EnterRoomOther(converter);
                            }
                            break;
                        // 다른 클라이언트가 레디함
                        case Protocols.S_READY_OTHER:
                            {
                                Ready(converter);
                            }
                            break;
                        // 게임이 시작함
                        case Protocols.S_MAP_START:
                            {
                                MapStart(converter);
                            }
                            break;
                        // 클라이언트가 접속중인지 확인하기 위해 서버가 보내는 메시지
                        case Protocols.S_PING:
                            {
                                // 클라이언트는 반응이 없어도 됨
                            }
                            break;
                        // 방찾기 화면에서 방 목록관련 정보 수신
                        case Protocols.RES_ADD_ROOM_LIST:
                            {
                                AddRoomList(converter);
                            }
                            break;
                        case Protocols.RES_DEL_ROOM_LIST:
                            {
                                DelRoomList(converter);
                            }
                            break;
                        case Protocols.RES_UPDATE_ROOM_LIST:
                            {
                                UpdateRoomList(converter);
                            }
                            break;
                        case Protocols.S_ERROR:
                            {
                                Error(converter);
                            }
                            break;
                        default:
                            Console.WriteLine("에러 : " + protocol);
                            break;
                    }
                }
            }

            public void Location(MessageConverter converter)
            {
                // 플레이어 번호
                int key = converter.NextInt();

                // 좌표
                int x = converter.NextInt();
                int y = converter.NextInt();

                ClientCharacter client;

                // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                bool result = gameManager.clientManager.ClientDic.TryGetValue(key, out client);

                // 해당 클라이언트가 존재하지 않을경우 리턴
                if (result == false) return;

                // 위치 설정
                Point velocity = new Point(x - client.Location.X, y - client.Location.Y);
                client.Location = new Point(x, y);

                UserClient userClient = gameManager.userClient;
                if (velocity.X != 0)
                {
                    // 움직인 클라이언트가 내 아래에 있는지 확인
                    List<ClientCharacter> list = userClient.GetClientsUnderTheFoot();

                    if (list.Count == 1)
                    {
                        foreach (var underClient in list)
                        {
                            // 만약 아래에 있는 클라이언트가 지금 움직인 클라이언트라면
                            if (underClient == client)
                            {
                                userClient.MoveExtra(new Point(velocity.X, 0));
                                break;
                            }
                        }
                    }
                }
            }


            public void LookDirection(MessageConverter converter)
            {
                // 플레이어 번호
                int key = converter.NextInt();

                bool bLookRight = converter.NextBool();

                ClientCharacter client;

                // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                bool result = gameManager.clientManager.ClientDic.TryGetValue(key, out client);

                if (result == false) return;

                client.MoveDirectionRight = bLookRight;
            }

            public void Move(MessageConverter converter)
            {
                // 좌표
                int x = converter.NextInt();
                int y = converter.NextInt();
                int MoveNum = converter.NextInt();

                UserClient userClient = gameManager.userClient;

                lock(userClient.MoveLock)
                {
                    // 만약 서버로부터 새로운 이동명령이 오면
                    if(userClient.MoveNum != MoveNum)
                    {
                        userClient.MoveNum = MoveNum;

                        // 텔레포트
                        userClient.Move(new Point(x, y), true);
                    }
                    else
                    {
                        // 이동
                        userClient.Move(new Point(x, y));
                    }
                    
                }
                
            }

            public void NewObject(MessageConverter converter)
            {
                // 키
                int key = converter.NextInt();

                // 타입
                byte type = converter.NextByte();

                // 좌표
                int x = converter.NextInt();
                int y = converter.NextInt();
                Point location = new Point(x, y);

                // 사이즈
                int width = converter.NextInt();
                int height = converter.NextInt();
                Size size = new Size(width, height);

                // 스킨 번호
                int skinNum = converter.NextInt();

                GameObject newObject = null;
                ObjectManager objectManager = gameManager.objectManager;
                switch (type)
                {
                    case ObjectTypes.FLOOR:
                        {
                            newObject = new Floor(key, location, size);
                        }
                        break;
                    case ObjectTypes.KEY_OBJECT:
                        {
                            newObject = new KeyObject(key, location, size);
                            objectManager.keyObjectKey = key;
                        }
                        break;
                    case ObjectTypes.DOOR:
                        {
                            newObject = new Door(key, location, size);
                            objectManager.doorKey = key;
                        }
                        break;
                    case ObjectTypes.STONE:
                        {
                            int weight = converter.NextInt();
                            Stone stone = new Stone(key, location, size);
                            stone.weight = weight;

                            newObject = stone;
                        }
                        break;
                    case ObjectTypes.BUTTON:
                        {
                            newObject = new MultiGame.Object.Button(key, location, size);
                        }
                        break;
                    case ObjectTypes.STONE_DOOR:
                        {
                            newObject = new StoneDoor(key, location, size);
                        }
                        break;
                    case ObjectTypes.PORTAL:
                        {
                            newObject = new Portal(key, location, size);
                        }
                        break;
                    default:
                        break;
                }

                if (newObject != null)
                {
                    newObject.SetSkin(skinNum);
                    objectManager.AddObject(newObject);
                }
            }

            public void ObjectEvent(MessageConverter converter)
            {
                int key = converter.NextInt();
                byte type = converter.NextByte();
                int clientKey = converter.NextInt();

                ObjectManager objectManager = gameManager.objectManager;
                UserClient userClient = gameManager.userClient;

                // 해당 오브젝트를 찾음
                GameObject gameObject;
                bool objResult = objectManager.ObjectDic.TryGetValue(key, out gameObject);

                if (objResult == false) return;

                // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                ClientCharacter client;
                if (clientKey == -1) client = userClient.Character;
                else
                {
                    bool result = gameManager.clientManager.ClientDic.TryGetValue(clientKey, out client);
                    if (result == false)
                        return;
                }


                switch (type)
                {
                    case ObjectTypes.KEY_OBJECT:
                        {
                            KeyObject keyObj = gameObject as KeyObject;
                            if (keyObj == null)
                                return;
                            // keyObj.isVisible = false;
                            keyObj.Collision = false;
                            keyObj.SetOwner(client);

                        }
                        break;
                    case ObjectTypes.DOOR:
                        {
                            Door door = gameObject as Door;
                            if (door == null) return;

                            byte doorEvent = converter.NextByte();

                            // 문이 열림
                            switch (doorEvent)
                            {
                                case DoorEvent.OPEN:
                                    {
                                        door.Open(true);
                                        int keyObjectKey = objectManager.keyObjectKey;
                                        objectManager.ObjectDic[keyObjectKey].isVisible = false;
                                        
                                    }
                                    break;
                                case DoorEvent.ENTER:
                                    {
                                        int dd = converter.NextInt();
                                        client.isVisible = false;
                                        client.Collision = false;
                                        // 만약 유저클라이언트일 경우 더이상 움직이지 못하게함
                                        if (clientKey == -1)
                                        {
                                            userClient.CanMove = false;
                                        }
                                    }
                                    break;
                                case DoorEvent.LEAVE:
                                    {
                                        client.isVisible = true;
                                        client.Collision = true;

                                        // 만약 유저클라이언트일 경우 움직일 수 있게 함
                                        if (clientKey == -1)
                                        {
                                            userClient.CanMove = true;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case ObjectTypes.STONE:
                        {
                            Stone stone = gameObject as Stone;
                            if (stone == null)
                            {
                                return;
                            }

                            int x = converter.NextInt();
                            int y = converter.NextInt();
                            int weight = converter.NextInt();

                            stone.Location = new Point(x, y);
                            stone.weight = weight;
                        }
                        break;
                    case ObjectTypes.BUTTON:
                        {
                            MultiGame.Object.Button button = gameObject as MultiGame.Object.Button;
                            if (button == null) return;

                            button.Pressed = true;

                        }
                        break;
                    case ObjectTypes.STONE_DOOR:
                        {
                            int OpenMode = converter.NextByte();

                            if(OpenMode == StoneDoorMode.MOVE)
                            {
                                int x = converter.NextInt();
                                int y = converter.NextInt();

                                // 캐릭터가 돌 위에 있을경우 같이 움직임
                                int dy = y - gameObject.Location.Y;
                                gameObject.Location = new Point(x, y);
                                if (dy < 0)
                                {
                                    Rectangle a = new Rectangle(new Point(x, y), gameObject.size);

                                    Size size = new Size(userClient.Character.size.Width, 1);
                                    Point location = new Point(userClient.Character.Location.X, userClient.Character.Location.Y + userClient.Character.size.Height + 1);
                                    Rectangle b = new Rectangle(location, size);

                                    Rectangle result = Rectangle.Intersect(a, b);

                                    if (result.IsEmpty == false)
                                    {
                                        userClient.Move(new Point(0, dy));
                                    }
                                }

                            }
                            else
                            {
                                gameObject.isVisible = false;
                                gameObject.Collision = false;
                            }
                        }
                        break;
                    case ObjectTypes.PORTAL:
                        {
                            
                        }
                        break;
                }
            }


            public void ExitRoom(MessageConverter converter)
            {

                // 플레이어 번호
                int key = converter.NextInt();

                ClientCharacter clientChar;

                // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                bool result = gameManager.clientManager.ClientDic.TryGetValue(key, out clientChar);

                // 만약 존재하지 않으면 리턴
                if (result == false)
                {
                    return;
                }

                //클라이언트 배열에서 제거
                gameManager.clientManager.RemoveClient(clientChar);


                // 게임이 시작하지 않았다면 로비 업데이트 
                if (gameManager.IsGameStart == false)
                {
                    gameManager.form1.UpdateLobby();
                }

            }

            public void EnterRoom(MessageConverter converter)
            {
                // 방 번호
                int roomCode = converter.NextInt();

                // 방 제목
                string roomTItle = converter.NextString();

                // 스킨 번호
                int skinNum = converter.NextInt();

                Console.WriteLine($"{roomCode}번 '{roomTItle}' 방에 접속");

                Form1 form = gameManager.form1;

                

                form.Invoke(new MethodInvoker(delegate ()
                {
                    LobbyRoom_Screen lobbyRoom = new LobbyRoom_Screen(form);

                    // 스킨 변경
                    gameManager.userClient.Character.SetSkin(skinNum);

                    // 방 제목
                    lobbyRoom.SetRoomTitle(roomCode.ToString(), roomTItle);

                    // 화면 전환
                    form.ChangeScreen(lobbyRoom);

                    // 로비 화면 갱신
                    form.UpdateLobby();
                }));
            }

            public void EnterRoomOther(MessageConverter converter)
            {

                // 플레이어 번호
                int key = converter.NextInt();

                // 레디 여부
                bool bReady = converter.NextBool();

                // 스킨 번호
                int skinNum = converter.NextInt();

                ClientCharacter clientCharacter;

                // 새로운 클라이언트 생성
                clientCharacter = gameManager.clientManager.AddClient(key, new Point(0, 0), 1);
                clientCharacter.IsReady = bReady;
                clientCharacter.SetSkin(skinNum);


                gameManager.form1.Invoke(new MethodInvoker(delegate ()
                {
                    gameManager.form1.UpdateLobby();
                }));
            }

            public void Ready(MessageConverter converter)
            {
                // 플레이어 번호
                int key = converter.NextInt();

                // 레디 여부
                bool bReady = converter.NextBool();

                // 플레이어 번호를 가지고 플레이어를 찾음
                ClientCharacter clientCharacter;

                bool result = gameManager.clientManager.ClientDic.TryGetValue(key, out clientCharacter);

                // 존재하지 않은 클라이언트면 종료
                if (result == false) return;

                clientCharacter.IsReady = bReady;

                gameManager.form1.Invoke(new MethodInvoker(delegate ()
                {
                    gameManager.form1.UpdateLobby();
                }));
            }


            public void MapStart(MessageConverter converter)
            {
                // 좌표
                int x = converter.NextInt();
                int y = converter.NextInt();

                Console.WriteLine("시작");

                UserClient userClient = gameManager.userClient;


                // 모든 오브젝트 제거
                gameManager.objectManager.ClearObjects();

                // 좌표 설정
                userClient.Character.Location = new Point(x, y);

                // 게임이 처음 시작하는 거라면 ( 첫번째 맵을 플레이하는거라면 )
                if (gameManager.IsGameStart == false)
                {
                    // 시작 플래그 변수 변경
                    gameManager.IsGameStart = true;

                    // 인게임 화면으로 변경
                    gameManager.form1.Invoke(new MethodInvoker(delegate ()
                    {
                        InGame_Screen inGame_Screen = new InGame_Screen(gameManager.form1);
                        gameManager.form1.ActiveControl = null;
                        inGame_Screen.StartUpdateScreen(true);

                        gameManager.form1.ChangeScreen(inGame_Screen);
                    }));
                }


                // 각 캐릭터들 설정
                foreach (var item in gameManager.clientManager.ClientDic)
                {
                    // 화면에 출력
                    item.Value.isVisible = true;

                    // 충돌 판정 켬
                    item.Value.Blockable = true;
                    item.Value.Collision = true;

                    // 움직임 시작
                    item.Value.GameStart();
                }

                // 유저 캐릭터
                userClient.Character.isVisible = true;
                userClient.Character.Collision = true;
                userClient.Character.Blockable = true;
                userClient.Start();
            }



            public void AddRoomList(MessageConverter converter)
            {
                int RoomCode = converter.NextInt();
                string RoomTitle = converter.NextString();
                int PlayerCount = converter.NextInt();

                // 형변환
                FindRoom_Screen findRoom_Screen = gameManager.form1.Controls[0] as FindRoom_Screen;

                // 방찾기 화면이 아닌경우 리턴
                if (findRoom_Screen == null) return;

                gameManager.form1.Invoke(new MethodInvoker(delegate ()
                {
                    findRoom_Screen.roomList_GridView.Rows.Add(RoomCode, RoomTitle, PlayerCount + "/3");
                }));
            }

            public void DelRoomList(MessageConverter converter)
            {
                int RoomCode = converter.NextInt();

                // 형변환
                FindRoom_Screen findRoom_Screen = gameManager.form1.Controls[0] as FindRoom_Screen;

                // 방찾기 화면이 아닌경우 리턴
                if (findRoom_Screen == null) return;

                foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                {
                    // 방 배열을 돌면서 방번호와 같은 방을 찾음
                    if ((int)item.Cells[0].Value == RoomCode)
                    {
                        gameManager.form1.Invoke(new MethodInvoker(delegate ()
                        {
                            // 방 제거
                            findRoom_Screen.roomList_GridView.Rows.Remove(item);
                        }));
                    }
                }
            }

            public void UpdateRoomList(MessageConverter converter)
            {
                int RoomCode = converter.NextInt();
                int PlayerCount = converter.NextInt();

                // 형변환
                FindRoom_Screen findRoom_Screen = gameManager.form1.Controls[0] as FindRoom_Screen;

                // 방찾기 화면이 아닌경우 리턴
                if (findRoom_Screen == null) return;

                foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                {
                    // 방 배열을 돌면서 방번호와 같은 방을 찾음
                    if ((int)item.Cells[0].Value == RoomCode)
                    {
                        gameManager.form1.Invoke(new MethodInvoker(delegate ()
                        {
                            // 인원수 업데이트
                            item.Cells[2].Value = PlayerCount + "/3";
                        }));
                    }
                }
            }

            public void Error(MessageConverter converter)
            {
                int errorCode = converter.NextInt();

                switch (errorCode)
                {
                    case 0:
                        {
                            MessageBox.Show("해당 방은 꽉찼습니다.", $"에러코드 : {errorCode}", MessageBoxButtons.OK);
                        }
                        break;
                    case 1:
                        {
                            MessageBox.Show("존재하지 않은 방입니다.", $"에러코드 : {errorCode}", MessageBoxButtons.OK);
                        }
                        break;
                    default:
                        Console.WriteLine("알수 없는 에러코드 {0}", errorCode);
                        break;
                }
            }
        }
    }
}