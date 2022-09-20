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

namespace MultiGame
{


    public class GameManager
    {
        // 자신 싱글톤 객체
        static GameManager gameManager = null;

        // 서버와 TCP통신을 담당하는 객체
        public MyClient myClient { get; set; }

        // 다른 클라이언트들을 관리하는 객체
        public ClientManager clientManager { get; set; }

        // 사용자의 캐릭터
        public ClientCharacter userCharacter { get; set; }

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

            // 사용자 캐릭터
            userCharacter = new ClientCharacter(-1, new Point(364, 293), 0);

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

                        // key == -1 ( 유저 캐릭터 )
                        if (key == -1)
                            client = userCharacter;
                        else
                        {
                            // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                            bool result = clientManager.ClientDic.TryGetValue(key, out client);

                            // 해당 클라이언트가 존재하지 않을경우 리턴
                            if (result == false) return;
                        }
                        if (key == -1)
                            Console.WriteLine($"동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");
                        else
                            Console.WriteLine($"{client.key}번 클라이언트 동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");

                        client.Location = new Point(x, y);
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
                            client = userCharacter;
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
                        form1.inGame_Screen.Paint -= clientChar.OnPaint;


                        // 게임이 시작하지 않았다면 로비 업데이트 
                        if (IsGameStart == false)
                        {
                            form1.UpdateLobby();
                        }

                    }
                    break;
                // 다른 클라이언트의 키보드 입력
                case "KeyInputOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 입력된 키
                        char InpKey = char.Parse(SplitMessage[2]);

                        // 눌렸으면 true / 아니면 false
                        bool bKeyDown = bool.Parse(SplitMessage[3]);

                        ClientCharacter client;

                        // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                        bool result = clientManager.ClientDic.TryGetValue(key, out client);

                        // 해당 클라이언트가 존재하지 않을경우 리턴
                        if (result == false) return;

                        switch (InpKey)
                        {
                            case 'L':
                                client.bLeftDown = bKeyDown;
                                break;
                            case 'R':
                                client.bRightDown = bKeyDown;
                                break;
                            case 'J':
                                client.bJumpDown = bKeyDown;
                                if(bKeyDown) client.Jump();
                                break;
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
                            form1.lobbyRoom_Screen.roomTitle_lbl.Text = $"{roomCode}번방 {roomTItle}";
                            form1.Controls.Clear();
                            form1.Controls.Add(form1.lobbyRoom_Screen);
                            RequestLobbyInfo(false);
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
                        clientCharacter = clientManager.AddOrGetClient(key, new Point(0, 0), 1);
                        clientCharacter.isReady = bReady;
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

                        clientCharacter.isReady = bReady;

                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            form1.UpdateLobby();
                        }));
                    }
                    break;
                // 게임이 시작함
                case "RoomStart":
                    {
                        // 플래그 변수 변경
                        IsGameStart = true;

                        // 인게임 화면으로 변경
                        form1.Invoke(new MethodInvoker(delegate ()
                        {
                            form1.Controls.Clear();
                            form1.Controls.Add(form1.inGame_Screen);
                            form1.inGame_Screen.StartUpdateScreen(true);
                        }));

                        // 각 캐릭터들 설정
                        foreach (var item in clientManager.ClientDic)
                        {
                            // 화면에 출력
                            form1.inGame_Screen.Paint += item.Value.OnPaint;
                            item.Value.isVisible = true;

                            // 움직임 시작
                            item.Value.GameStart();
                        }

                        // 유저 캐릭터
                        form1.inGame_Screen.Paint += userCharacter.OnPaint;
                        userCharacter.isReady = false;
                        userCharacter.isVisible = true;
                        userCharacter.GameStart();
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
                        switch (SplitMessage[1])
                        {
                            // 방 정보 추가 ( 방찾기 화면 입장 or 방이 새로 생김 )
                            case "Add":
                                form1.Invoke(new MethodInvoker(delegate ()
                                {
                                    form1.findRoom_Screen.roomList_GridView.Rows.Add(SplitMessage[2], SplitMessage[3], SplitMessage[4] + "/3");
                                }));
                                break;
                            // 방 정보 삭제 ( 방이 사라짐 or 해당 방 게임이 시작함 )
                            case "Del":
                                {
                                    foreach (DataGridViewRow item in form1.findRoom_Screen.roomList_GridView.Rows)
                                    {
                                        // 방 배열을 돌면서 방번호와 같은 방을 찾음
                                        if (item.Cells[0].Value.ToString() == SplitMessage[2])
                                        {
                                            form1.Invoke(new MethodInvoker(delegate ()
                                            {
                                                // 방 제거
                                                form1.findRoom_Screen.roomList_GridView.Rows.Remove(item);
                                            }));
                                        }
                                    }

                                }
                                break;
                            // 방 정보 수정 ( 방의 인원수가 변경됨 )
                            case "Update":
                                {
                                    foreach (DataGridViewRow item in form1.findRoom_Screen.roomList_GridView.Rows)
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


        // 유저가 입력한 키를 서버로 보냄 ( 입력키, 누르면 true / 뗐으면 false )
        public void SendInputedKey(char inputKey, bool bPressed)
        {
            string message = $"KeyInput#{inputKey}#{bPressed}#@";
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


        // 오브젝트 이동
        public void MoveObject(ClientCharacter client, Point velocity)
        {
            Point resultLoc = client.Location;
            Point tempLoc;

            // X의 변화가 있다면 x의 변화에 대한 충돌판정
            if (velocity.X != 0)
            {
                tempLoc = new Point(resultLoc.X + velocity.X , resultLoc.Y);

                // 충돌하지 않았으면
                if (CollisionCheck(client, tempLoc))
                {
                    // 움직이기 위해 좌표 저장
                    resultLoc = tempLoc;

                    // 머리위에 다른 캐릭터가 있는지 검사
                    List<ClientCharacter> list = GetClientsOverTheHead(client);

                    // 머리위 캐릭터들에게 자신이 움직이는 방향으로 같이 움직이게함
                    foreach (var item in list)
                    {
                        item.MoveCharacter(new Point(velocity.X, 0));
                    }
                }

            }


            tempLoc = new Point(resultLoc.X, resultLoc.Y + velocity.Y);

            // 위, 아래에 대한 충돌 판정
            if (CollisionCheck(client, tempLoc))
            {
                resultLoc = tempLoc;
                client.isGround = false;
            }
            else
            {
                // 만약 밑으로 가던중 충돌판정이 일어나면 
                if(velocity.Y > 0)
                {
                    //땅위에 있다는 플래그변수 true
                    client.isGround = true;

                    // 땅에 도착했을 때 점프버튼을 누르고있다면 다시 점프
                    if (client.bJumpDown == true)
                        client.Jump();
                }
                
            }


            // 실제 좌표를 이동시킴
            client.Location = resultLoc;
        }

        public bool CollisionCheck(ClientCharacter client, Point newLocation)
        {
            // 임시 바닥
            if (newLocation.Y >= 400) return false;

            // 해당 오브젝트의 충돌 박스
            Rectangle a = new Rectangle(newLocation, client.size);

            // 움직인 캐릭터가 자신(클라이언트 주인)과 부딪히는지 체크함
            if (client != userCharacter)
            {
                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(userCharacter.Location, userCharacter.size);

                // 만약 움직였을때 겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    return false;
                }
            }

            // 모든 오브젝트와 부딪히는지 체크함
            foreach (var item in clientManager.ClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (item.Value == client)
                {
                    continue;
                }
                
                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    return false;
                }
            }
            client.Location = newLocation;

            return true;
        }

        // 대상 클라이언트 머리위에 있는 클라이언트 리스트 반환
        public List<ClientCharacter> GetClientsOverTheHead(ClientCharacter client)
        {
            List<ClientCharacter> list = new List<ClientCharacter>();

            // 대상의 머리위 충돌박스
            Size size = new Size(client.size.Width, 10);
            Point location = new Point(client.Location.X, client.Location.Y - 10);
            Rectangle a = new Rectangle(location, size);


            // 유저캐릭터(클라이언트 주인)와 비교
            if (client != userCharacter)
            {
                // 대상 충돌판정
                Rectangle b = new Rectangle(userCharacter.Location, userCharacter.size);

                // 만약 움직였을때 겹친다면 리스트에 추가
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    list.Add(userCharacter);
                }
            }

            // 모든 클라이언트와 비교
            foreach (var item in clientManager.ClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (item.Value == client)
                {
                    continue;
                }

                // 대상 충돌판정
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    list.Add(otherClient);
                }
            }

            return list;
        }

    }
}

