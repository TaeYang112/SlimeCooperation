using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Collections;
using System.Threading;
using MultiGame.Client;
using MultiGame.UserPanel;
using MultiGame.Object;

namespace MultiGame
{
    public class GameManager
    {
        // 자신 싱글톤 객체
        static GameManager gameManager = null;

        // 에러로 인한 프로그램 종료 플래그 변수
        private bool bExitReady = false;

        // 서버와 TCP통신을 담당하는 객체
        public MyClient myClient { get; set; }

        // 다른 클라이언트들을 관리하는 객체
        public ClientManager clientManager { get; set; }

        // 맵에 있는 오브젝트들을 관리하는 객체
        public ObjectManager objectManager { get; set; }

        // 사용자의 캐릭터
        public UserClient userClient { get; set; }

        // 게임 시작 여부
        public bool IsGameStart { get; set; }

        private Form1 form1;


        public static GameManager GetInstance()
        {
            if (gameManager == null)
            {
                gameManager = new GameManager();
            }
            return gameManager;
        }

        private GameManager()
        {
            // 클라이언트 객체 생성
            myClient = new MyClient();

            // 서버로부터 메세지를 받으면 onTakeMessage함수 호출
            myClient.TakeMessage += TakeMessage;

            // 서버로부터 에러를 받으면 TakeException 함수 호출
            myClient.onException += TakeException;

            // 다른 클라이언트들을 관리할 객체
            clientManager = new ClientManager();

            // 맵에 있는 오브젝트들을 관리할 객체
            objectManager = new ObjectManager();

            // 사용자 캐릭터
            userClient = new UserClient();

        }


        public void Start(Form1 form1)
        {
            this.form1 = form1;
            myClient.Start();
        }

        // 서버로부터 받은 메세지를 처리
        private void TakeMessage(string message)
        {
            // 메세지는 '@'으로 끝을 구분함, 메세지 여러개가 겹쳐있을 수 있기때문에 Split으로 나눔 ex) Location#1#2@Location#2#3@Location#5#2@
            string[] Messages = message.Split('@');

            for (int i = 0; i < Messages.Length - 1; i++)
            {
                // 메세지 해석
                ParseMessage(Messages[i]);
            }
        }

        // TCP 통신중 발생한 에러 처리
        private void TakeException(Exception e)
        {
            if (e.GetType().ToString() == "System.InvalidOperationException")
            {
                if (bExitReady) return;

                bExitReady = true;
                MessageBox.Show("서버와 연결되어있지 않습니다.", $"에러코드 : {-1}", MessageBoxButtons.OK);
                Application.Exit();
                
            }
        }
        
        // 메세지를 해석 후 실행
        private void ParseMessage(string Message)
        {
            // 메세지는 '#'으로 각 매개인자를 구분함
            string[] SplitMessage = Message.Split('#');
            switch (SplitMessage[0])
            {
                // 클라이언트의 캐릭터 위치 수신
                case "Location":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 좌표
                        int x = int.Parse(SplitMessage[2]);
                        int y = int.Parse(SplitMessage[3]);

                        ClientCharacter client;

                        // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                        bool result = clientManager.ClientDic.TryGetValue(key, out client);

                        // 해당 클라이언트가 존재하지 않을경우 리턴
                        if (result == false) return;

                        // 위치 설정
                        Point velocity = new Point(x - client.Location.X, y - client.Location.Y);
                        client.Location = new Point(x, y);

                        if (velocity.X != 0)
                        {
                            // 움직인 클라이언트가 내 아래에 있는지 확인
                            List<ClientCharacter> list = GetClientsUnderTheFoot(userClient.Character);

                            if(list.Count == 1)
                            {
                                foreach(var underClient in list)
                                {
                                    // 만약 아래에 있는 클라이언트가 지금 움직인 클라이언트라면
                                    if(underClient == client)
                                    {
                                        userClient.MoveExtra(new Point(velocity.X, 0));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                // 플레이어가 쳐다보는 방향 ( true : 오른쪽 )
                case "LookR":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        bool bLookRight = bool.Parse(SplitMessage[2]);

                        ClientCharacter client;

                        // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                        bool result = clientManager.ClientDic.TryGetValue(key, out client);

                        if (result == false) return;

                        client.MoveDirectionRight = bLookRight;
                    }
                    break;
                // 서버에 의해 캐릭터가 움직여짐
                case "Move":
                    {
                        // 좌표
                        int x = int.Parse(SplitMessage[1]);
                        int y = int.Parse(SplitMessage[2]);

                        // 이동
                        userClient.Move(new Point(x, y));

                    }
                    break;
                // 오브젝트 생성
                case "NewObject":
                    {
                        // 키
                        int key = int.Parse(SplitMessage[1]);

                        // 타입
                        string type = SplitMessage[2];
                        // 좌표
                        int x = int.Parse(SplitMessage[3]);
                        int y = int.Parse(SplitMessage[4]);

                        // 사이즈
                        int width = int.Parse(SplitMessage[5]);
                        int height = int.Parse(SplitMessage[6]);

                        // 스킨 번호
                        int skinNum = int.Parse(SplitMessage[7]);

                        GameObject newObject = null;
                        switch (type)
                        {
                            case "Floor":
                                {
                                    newObject = new Floor(key, new Point(x, y), new Size(width, height));
                                }
                                break;
                            case "Key":
                                {
                                    newObject = new KeyObject(key, new Point(x, y), new Size(width, height));
                                    objectManager.keyObjectKey = key;
                                }
                                break;
                            case "Door":
                                {
                                    newObject = new Door(key, new Point(x, y), new Size(width, height));
                                    objectManager.doorKey = key;
                                }
                                break;
                            case "Stone":
                                {
                                    int weight = int.Parse(SplitMessage[8]);
                                    Stone stone = new Stone(key, new Point(x, y), new Size(width, height));
                                    stone.weight = weight;

                                    newObject = stone;
                                }
                                break;
                            default:
                                break;
                        }

                        if(newObject != null)
                        {
                            newObject.SetSkin(skinNum);
                            objectManager.AddObject(newObject);
                        }
                    }
                    break;
                // 서버로부터 오브젝트 이벤트 수신
                case "ObjEvent":
                    {
                        int key = int.Parse(SplitMessage[1]);
                        string type = SplitMessage[2];
                        int clientKey = int.Parse(SplitMessage[3]);

                        // 해당 오브젝트를 찾음
                        GameObject gameObject;
                        bool objResult = objectManager.ObjectDic.TryGetValue(key, out gameObject);

                        if (objResult == false) return;

                        // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                        ClientCharacter client;
                        if (clientKey == -1) client = userClient.Character;
                        else
                        {
                            bool result = clientManager.ClientDic.TryGetValue(clientKey, out client);
                            if (result == false)
                                return;
                        }


                        switch (type)
                        {
                            case "KeyObject":
                                {
                                    KeyObject keyObj = gameObject as KeyObject;
                                    if (keyObj == null) return;

                                    // keyObj.isVisible = false;
                                    keyObj.Collision = false;
                                    keyObj.SetOwner(client);
                                    
                                }
                                break;
                            case "Door":
                                {
                                    Door door = gameObject as Door;
                                    if (door == null) return;

                                    string context = SplitMessage[4];

                                    // 문이 열림
                                    if(context == "Open")
                                    {
                                        door.Open(true);
                                        int keyObjectKey = objectManager.keyObjectKey;
                                        objectManager.ObjectDic[keyObjectKey].isVisible = false;
                                    }
                                    // 문안으로 들어감
                                    else if(context == "Enter")
                                    {
                                        client.isVisible = false;
                                        client.Collision = false;

                                        // 만약 유저클라이언트일 경우 더이상 움직이지 못하게함
                                        if(clientKey == -1)
                                        {
                                            userClient.CanMove = false;
                                        }
                                    }
                                    // 문밖으로 나감
                                    else
                                    {
                                        client.isVisible = true;
                                        client.Collision = true;

                                        // 만약 유저클라이언트일 경우 움직일 수 있게 함
                                        if (clientKey == -1)
                                        {
                                            userClient.CanMove = true;
                                        }
                                    }
                                }
                                break;
                            case "Stone":
                                {
                                    Stone stone = gameObject as Stone;
                                    if (gameObject == null) return;

                                    int x = int.Parse(SplitMessage[4]);
                                    int y = int.Parse(SplitMessage[5]);
                                    int weight = int.Parse(SplitMessage[6]);

                                    stone.Location = new Point(x, y);
                                    stone.weight = weight;
                                }
                                break;
                        }
                    }
                    break;
                // 클라이언트 정보 업데이트
                case "UpdateClient":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 스킨 번호
                        int skinNum = int.Parse(SplitMessage[2]);

                        ClientCharacter client;

                        // key == -1 ( 유저 캐릭터 )
                        if (key == -1)
                            client = userClient.Character;
                        else
                        {
                            // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                            bool result = clientManager.ClientDic.TryGetValue(key, out client);

                            // 해당 클라이언트가 존재하지 않을경우 리턴
                            if (result == false) return;
                        }

                        client.SetSkin(skinNum);

                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            form1.UpdateLobby();
                        }));
                    }
                    break;
                // 다른 클라이언트가 방을 나감
                case "LeaveRoomOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        ClientCharacter clientChar;

                        // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                        bool result = clientManager.ClientDic.TryGetValue(key, out clientChar);

                        // 만약 존재하지 않으면 리턴
                        if (result == false)
                        {
                            return;
                        }

                        //클라이언트 배열에서 제거
                        clientManager.RemoveClient(clientChar);


                        // 게임이 시작하지 않았다면 로비 업데이트 
                        if (IsGameStart == false)
                        {
                            form1.UpdateLobby();
                        }

                    }
                    break;
                // 방에 입장
                case "EnterRoom":
                    {
                        // 방 번호
                        string roomCode = SplitMessage[1];

                        // 방 제목
                        string roomTItle = SplitMessage[2];

                        Console.WriteLine($"{roomCode}번 '{roomTItle}' 방에 접속");


                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            LobbyRoom_Screen lobbyRoom = new LobbyRoom_Screen(form1);

                            // 방 제목
                            lobbyRoom.SetRoomTitle(roomCode, roomTItle);

                            // 화면 전환
                            form1.ChangeScreen(lobbyRoom);

                            // 게임 목록 정보 수신 거부
                            RequestLobbyInfo(false);

                            // 로비 화면 갱신
                            form1.UpdateLobby();
                        }));
                    }
                    break;
                // 방에 다른 클라이언트 입장
                case "EnterRoomOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 레디 여부
                        bool bReady = bool.Parse(SplitMessage[2]);

                        // 스킨 번호
                        int skinNum = int.Parse(SplitMessage[3]);

                        ClientCharacter clientCharacter;

                        // 새로운 클라이언트 생성
                        clientCharacter = clientManager.AddClient(key, new Point(0, 0), 1);
                        clientCharacter.IsReady = bReady;
                        clientCharacter.SetSkin(skinNum);


                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            form1.UpdateLobby();
                        }));
                    }
                    break;
                // 다른 클라이언트가 레디함
                case "ReadyOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 레디 여부
                        bool bReady = bool.Parse(SplitMessage[2]);

                        // 플레이어 번호를 가지고 플레이어를 찾음
                        ClientCharacter clientCharacter;

                        bool result = clientManager.ClientDic.TryGetValue(key, out clientCharacter);

                        // 존재하지 않은 클라이언트면 종료
                        if (result == false) return;

                        clientCharacter.IsReady = bReady;

                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            form1.UpdateLobby();
                        }));
                    }
                    break;
                // 게임이 시작함
                case "MapStart":
                    {
                        // 좌표
                        int x = int.Parse(SplitMessage[1]);
                        int y = int.Parse(SplitMessage[2]);

                        // 모든 오브젝트 제거
                        objectManager.ClearObjects();

                        // 좌표 설정
                        userClient.Character.Location = new Point(x, y);

                        // 게임이 처음 시작하는 거라면 ( 첫번째 맵을 플레이하는거라면 )
                        if(IsGameStart == false)
                        {
                            // 시작 플래그 변수 변경
                            IsGameStart = true;

                            // 인게임 화면으로 변경
                            form1.Invoke(new MethodInvoker(delegate ()
                            {
                                InGame_Screen inGame_Screen = new InGame_Screen(form1);
                                form1.ActiveControl = null;
                                inGame_Screen.StartUpdateScreen(true);

                                form1.ChangeScreen(inGame_Screen);
                            }));
                        }
                        

                        // 각 캐릭터들 설정
                        foreach (var item in clientManager.ClientDic)
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
                    break;
                // 클라이언트가 접속중인지 확인하기 위해 서버가 보내는 메시지
                case "Ping":
                    {
                        // 클라이언트는 반응이 없어도 됨
                    }
                    break;
                // 방찾기 화면에서 방 목록관련 정보 수신
                case "RoomList":
                    {
                        // 형변환
                        FindRoom_Screen findRoom_Screen = form1.Controls[0] as FindRoom_Screen;

                        // 방찾기 화면이 아닌경우 리턴
                        if (findRoom_Screen == null) return;

                        switch (SplitMessage[1])
                        {
                            // 방 정보 추가 ( 방찾기 화면 입장 or 방이 새로 생김 )
                            case "Add":
                                form1.Invoke(new MethodInvoker(delegate ()
                                {
                                    findRoom_Screen.roomList_GridView.Rows.Add(SplitMessage[2], SplitMessage[3], SplitMessage[4] + "/3");
                                }));
                                break;
                            // 방 정보 삭제 ( 방이 사라짐 or 해당 방 게임이 시작함 )
                            case "Del":
                                {
                                    foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                                    {
                                        // 방 배열을 돌면서 방번호와 같은 방을 찾음
                                        if (item.Cells[0].Value.ToString() == SplitMessage[2])
                                        {
                                            form1.Invoke(new MethodInvoker(delegate ()
                                            {
                                                // 방 제거
                                                findRoom_Screen.roomList_GridView.Rows.Remove(item);
                                            }));
                                        }
                                    }

                                }
                                break;
                            // 방 정보 수정 ( 방의 인원수가 변경됨 )
                            case "Update":
                                {
                                    foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                                    {
                                        // 방 배열을 돌면서 방번호와 같은 방을 찾음
                                        if (item.Cells[0].Value.ToString() == SplitMessage[2])
                                        {
                                            form1.Invoke(new MethodInvoker(delegate ()
                                            {
                                                // 인원수 업데이트
                                                item.Cells[2].Value = SplitMessage[3] + "/3";
                                            }));
                                        }
                                    }
                                }
                                break;
                            default:
                                break;

                        }
                    }
                    break;
                case "Error":
                    {
                        int errorCode = int.Parse(SplitMessage[1]);

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
                    break;
                default:
                    Console.WriteLine("디폴트 : {0}", Message);
                    break;
            }
        }


        // 서버로 메세지 전송
        public void SendMessage(string message)
        {
            myClient.SendMessage(message);
        }

        // 서버로 방만들기 요청
        public void RequestCreateRoom(string RoomTitle)
        {
            myClient.SendMessage($"CreateRoom#{RoomTitle}@");
        }

        // 로비 정보 요청
        public void RequestLobbyInfo(bool result)
        {
            myClient.SendMessage($"LobbyInfo#{result}@");
        }

        // 방 입장 요청
        public void RequestEnterRoom(int i)
        {
            myClient.SendMessage($"TryEnterRoom#{i}@");
        }

        // 준비 요청
        public void RequestReady(bool bReady)
        {
            myClient.SendMessage($"Ready#{bReady}@");
        }


        // 대상 클라이언트 발 아래에 있는 클라이언트 리스트 반환
        public List<ClientCharacter> GetClientsUnderTheFoot(ClientCharacter client)
        {
            List<ClientCharacter> list = new List<ClientCharacter>();

            // 대상의 발아래 충돌박스
            Size size = new Size(client.size.Width - 4, 1);
            Point location = new Point(client.Location.X + 2, client.Location.Y + client.size.Height + 1);
            Rectangle a = new Rectangle(location, size);

            // 모든 클라이언트와 비교
            foreach (var item in clientManager.ClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (otherClient == client || otherClient.Collision == false)
                {
                    continue;
                }

                // 대상 충돌판정
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약  겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    list.Add(otherClient);
                }
            }

            return list;
        }

    }
}

