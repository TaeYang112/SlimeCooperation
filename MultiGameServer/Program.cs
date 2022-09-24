using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

// -----------------
// ----- 서버 ------
// -----------------

namespace MultiGameServer
{
    class Program
    {
        public static Program program = null;

        // TCP 서버를 관리하고 클라이언트와 통신하는 객체
        private MyServer server;

        // 방을 관리하는 객체
        public RoomManager roomManager { get; set; }

        // 클라이언트들을 관리하는 객체
        public ClientManager clientManager { get; set; }

        // 서버와 클라이언트가 계속 연결되어있는지 확인하기 위해 일정시간마다 가짜 메세지를 보냄
        private System.Threading.Timer HeartBeatTimer;

        // 하나의 클라이언트가 여러번 나가는거를 막기위한 세마포
        public Semaphore sema_ClientLeave;

        static void Main(string[] args)
        {
            Program program = Program.GetInstance();
            program.Start();

            while(true)
            {
                string []command = Console.ReadLine().Split(' ');

                switch(command[0])
                {
                    case "/roomstart":
                        {
                            if(command.Length != 2)
                            {
                                Console.WriteLine("[ERROR] 올바르지 않은 매개변수 개수입니다.");
                                continue;
                            }
                            try
                            {
                                int roomKey = int.Parse(command[1]);
                                Room room;
                                bool result = program.roomManager.RoomDic.TryGetValue(roomKey, out room);
                                if (result  == false)
                                {
                                    Console.WriteLine("[ERROR] 존재하지 않은 방입니다.");
                                    continue;
                                }
                                room.GameStart();
                                Console.WriteLine("[INFO] " + roomKey + "번 방을 시작하였습니다.");
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("[ERROR] 올바르지 않은 매개변수 형식입니다.");
                                continue;
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("[ERROR] 매개변수가 존재하지 않습니다.");
                                continue;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("[ERROR] 알수없는 명령어입니다.");
                        break;
                }
            }
                


        }

        public static Program GetInstance()
        {
            if(program == null)
            {
                program = new Program();
            }
            return program;
        }

        private Program()
        {
            server = new MyServer();
            server.ClientJoin += new ClientJoinEventHandler(OnClientJoin);
            server.ClientLeave += new ClientLeaveEventHandler(OnClientLeave);

            clientManager = new ClientManager();
            roomManager = new RoomManager();

            sema_ClientLeave = new Semaphore(1, 1);

            // 서버와 클라이언트가 계속 연결되어있는지 확인하기 위해 일정시간마다 가짜 메세지를 보내는 타이머
            TimerCallback tc = new TimerCallback(HeartBeat);
            HeartBeatTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
        }

        ~Program()
        {
            HeartBeatTimer.Dispose();
        }
        public void Start()
        {
            server.Start();
            HeartBeatTimer.Change(0, 1000);
        }


        // 서버에 새로운 클라이언트가 접속하면 호출됨
        private void OnClientJoin(ClientData newClientData)
        {
            ClientCharacter newClient = clientManager.AddClient(newClientData);

            Console.WriteLine("[INFO] "+ newClient.key + "번 클라이언트가 접속하였습니다.");


            // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
            newClient.clientData.client.GetStream().BeginRead(newClient.clientData.byteData, 0, newClient.clientData.byteData.Length, new AsyncCallback(DataRecieved), newClient);
        }


        // 클라이언트와 연결이 끊기면 호출됨
        private void OnClientLeave(ClientData oldClientData)
        {
            sema_ClientLeave.WaitOne();

            // 클라이언트 배열에서 가져옴
            ClientCharacter clientChar;
            bool bClientValid = clientManager.ClientDic.TryGetValue(oldClientData.key, out clientChar);

            // 클라이언트가 존재하지 않으면 리턴
            if(bClientValid == false)
            {
                sema_ClientLeave.Release();
                return;
            }

            Console.WriteLine("[INFO] " + oldClientData.key + "번 클라이언트와의 연결이 끊겼습니다.");

            // 클라이언트가 속해있는 방을 가져옴
            Room room;
            bool bRoomValid = roomManager.RoomDic.TryGetValue(clientChar.RoomKey, out room);

            // 방이 존재하면
            if(bRoomValid)
            {
                // 방에서 클라이언트 제거
                int peopleCount = room.ClientLeave(clientChar);

                // 방이 대기상태일 때
                if (room.bGameStart == false)
                {
                    // 만약 방에 남은 인원이 없으면
                    if (peopleCount < 1)
                    {
                        // 방 제거
                        roomManager.RemoveRoom(room);

                        // 방찾기 화면에 있는 클라들한테 방이 없어졌다고 알려줌
                        SendDelRoomInfo(room);
                    }
                    else
                    {
                        // 방 안의 다른 플레이어들한테 알려줌
                        SendMessageToAll_InRoom($"LeaveRoomOther#{clientChar.key}@", room.key, clientChar.key);

                        // 방의 인원수가 바뀐것을 클라이언트들에게 알려줌
                        SendUpdateRoomInfo(room);
                    }

                }
                // 게임이 시작한 상태
                else
                {
                    // 만약 방에 남은 인원이 없으면
                    if (peopleCount < 1)
                    {
                        // 방 제거
                        roomManager.RemoveRoom(room);
                    }
                    else
                    {
                        // 방 안의 다른 플레이어들한테 알려줌
                        SendMessageToAll_InRoom($"LeaveRoomOther#{clientChar.key}@", room.key, clientChar.key);
                    }
                }
            }

            // 최종적으로 클라이언트 관리목록에서 제거
            clientManager.RemoveClient(oldClientData);

            sema_ClientLeave.Release();
        }


        // 클라이언트로부터 메세지 수신
        private void DataRecieved(IAsyncResult ar)
        {
            ClientCharacter clientChar = ar.AsyncState as ClientCharacter; ;
            ClientData clientData = clientChar.clientData;
            try
            {
                // 전달받은 byte를 string으로 바꿈
                int bytesRead = clientData.client.GetStream().EndRead(ar);
                string stringData = Encoding.Default.GetString(clientData.byteData, 0, bytesRead);
                

                // 메세지를 해석함
                ParseMessage(clientChar, stringData);


                // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
                clientData.client.GetStream().BeginRead(clientData.byteData, 0, clientData.byteData.Length, new AsyncCallback(DataRecieved), clientChar);
            }
            catch
            {
            }

        }

        // 서버와 클라이언트가 계속 연결되어있는지 확인하기 위해 일정시간마다 가짜 메세지를 보냄
        private void HeartBeat(object t)
        {
            SendMessageToAll("Ping@");
        }



        // 받은 메세지를 해석함
        private void ParseMessage(ClientCharacter clientChar, string message)
        {
            string[] Messages = message.Split('@');
            for (int i = 0; i < Messages.Length - 1; i++)
            {
                string[] SplitMessage = Messages[i].Split('#');
                switch (SplitMessage[0])
                {
                    // 방 만들기 요청
                    case "CreateRoom":
                        {
                            // 방 제목
                            string RoomTitle = SplitMessage[1];

                            // 방 생성
                            Room newRoom = roomManager.CreateRoom(RoomTitle);

                            // 방 만든사람을 방에 접속시킴
                            ClientEnterRoom(newRoom, clientChar);

                            clientChar.bFindingRoom = false;

                            // 방찾기에 있는 클라이언트한테 방이 새로 생긴걸 알려줌
                            foreach (var item in clientManager.ClientDic)
                            {
                                if (item.Value.bFindingRoom == true)
                                {
                                    SendMessage($"RoomList#Add#{newRoom.key}#{newRoom.RoomTitle}#{newRoom.GetPeopleCount()}@", item.Key);
                                }
                            }

                        }
                        break;
                    // 방 입장 시도
                    case "TryEnterRoom":
                        {
                            // 방 키
                            int roomKey = int.Parse(SplitMessage[1]);

                            // 방키로 방을 찾음
                            Room room;
                            bool result = roomManager.RoomDic.TryGetValue(roomKey, out room);

                            // 방을 찾지 못할경우
                            if (result == false)
                            {
                                // 방이 없다고 에러 보냄
                                SendMessage("Error#1@", clientChar.key);
                                continue;
                            }

                            // 인원수가 3명 이상일경우
                            if (room.GetPeopleCount() >= 3)
                            {
                                // 방이 꽉찼다고 에러 보냄
                                SendMessage("Error#0@", clientChar.key);
                                continue;
                            }


                            // 입장
                            ClientEnterRoom(room, clientChar);

                            
                            // 인원수가 바뀐것을 클라이언트들에게 알려줌
                            SendUpdateRoomInfo(room);
                            
                        }
                        break;
                    // 레디
                    case "Ready":
                        {
                            // 레디 여부를 대입
                            bool bReady = bool.Parse(SplitMessage[1]);
                            clientChar.bReady = bReady;


                            // 클라이언트가 들어있는 방을 찾음
                            Room room;
                            bool result = roomManager.RoomDic.TryGetValue(clientChar.RoomKey, out room);

                            // 존재하지 않으면 return
                            if (result == false)
                            {
                                continue;
                            }

                            // 방안의 다른 클라이언트한테 레디했다고 알려줌
                            SendMessageToAll_InRoom($"ReadyOther#{clientChar.key}#{bReady}@", room.key, clientChar.key);

                            if (bReady == true)
                            {
                                // 3명 이상의 클라이언트가 레디했다면
                                if (room.IsAllReady() == true)
                                {
                                    // 게임시작
                                    room.GameStart();

                                    // 방찾기 중인 클라이언트들의 방목록에서 시작한 방을 제거
                                    SendDelRoomInfo(room);
                                }
                            }

                        }
                        break;
                    // 클라이언트가 로비 정보 요청 / 요청 종료
                    case "LobbyInfo":
                        {
                            clientChar.bFindingRoom = bool.Parse(SplitMessage[1]);

                            // 정보 요청이 true라면 원래 있던 방목록을 전달함
                            if(clientChar.bFindingRoom == true)
                            {
                                foreach (var item in roomManager.RoomDic)
                                {
                                    if (item.Value.bGameStart == false)
                                    {
                                        SendMessage($"RoomList#Add#{item.Value.key}#{item.Value.RoomTitle}#{item.Value.GetPeopleCount()}@", clientChar.key);
                                    }
                                }
                            }
                            
                        }
                        break;
                    // 클라이언트가 로비를 나감
                    case "ExitLobby":
                        {
                            // 클라이언트가 속해있는 방을 가져옴
                            Room room;
                            bool result = roomManager.RoomDic.TryGetValue(clientChar.RoomKey, out room);

                            // 방이 존재하지 않으면 종료
                            if (result == false)
                            {
                                continue;
                            }

                            // 다른 플레이어들한테 알려줌
                            SendMessageToAll_InRoom($"LeaveRoomOther#{clientChar.key}@", room.key, clientChar.key);

                            // 룸의 클라이언트 배열에서도 제거
                            room.ClientLeave(clientChar);

                            // 만약 클라이언트가 나감으로서 아무도 방에 남지않을경우 방을 제거
                            if(room.GetPeopleCount() == 0)
                            {
                                roomManager.RemoveRoom(room);
                                
                                // 방찾기 중인 클라이언트한테 알려줌
                                SendDelRoomInfo(room);
                            }
                            else
                            {
                                // 아니면 클라이언트들에게 인원수가 바뀐것을 알려줌
                                SendUpdateRoomInfo(room);
                            }
                            
                        }
                        break;
                    // 클라이언트의 좌표를 받아 다른 클라이언트에게 알려줌
                    case "Location":
                        {
                            int x = int.Parse(SplitMessage[1]);
                            int y = int.Parse(SplitMessage[2]);

                            int velocity_x = x - clientChar.Location.X;
                            clientChar.Location = new Point(x, y);

                            // 전체 클라이언트에게 전송
                            SendMessageToAll_InRoom($"Location#{clientChar.key}#{x}#{y}@",clientChar.RoomKey,clientChar.key);
                           
                            // 움직인 클라이언트 위에 다른 클라이언트가 있는지 확인
                            List<ClientCharacter> list = GetClientsOverTheHead(clientChar);

                            if(velocity_x != 0)
                            {
                                // 위에 있는 클라이언트들도 같은 방향으로 움직이게 함
                                foreach (var client in list)
                                {
                                    SendMessage($"Move#{velocity_x}#{0}@", client.key);
                                }
                            }
                        }
                        break;
                    // 플레이어가 쳐다보는 방향 수신 ( true : 오른쪽 )
                    case "LookR":
                        {
                            bool bLookRight = bool.Parse(SplitMessage[1]);

                            SendMessageToAll_InRoom($"LookR#{clientChar.key}#{bLookRight}@",clientChar.RoomKey,clientChar.key);
                        }
                        break;
                    default:
                        Console.WriteLine("디폴트 : {0}", Messages[i]);
                        break;
                }
            }
        }


        // 방찾기 화면에 있는 클라이언트들에게 방의 인원수가 바뀐것을 알림
        public void SendUpdateRoomInfo(Room room)
        {
            foreach(var item in clientManager.ClientDic)
            {
                if (item.Value.bFindingRoom == true)
                {
                    SendMessage($"RoomList#Update#{room.key}#{room.GetPeopleCount()}@", item.Key);
                }
            }
        }

        // 방찾기 화면에 있는 클라이언트들에게 방의 정보를 없애라고 알림 ( 인원수 0명이 되었거나 게임이 시작할경우 호출) 
        public void SendDelRoomInfo(Room room)
        {
            foreach (var item in clientManager.ClientDic)
            {
                if (item.Value.bFindingRoom == true)
                {
                    SendMessage($"RoomList#Del#{room.key}@", item.Key);
                }
            }
        }

        // 클라이언트를 방에 입장시킬 때 호출
        public void ClientEnterRoom(Room room, ClientCharacter clientChar)
        {

            // 방 입장 ( 서버 관점 )
            room.ClientEnter(clientChar);

            // 방 입장을 클라이언트한테 알림
            SendMessage($"EnterRoom#{room.key}#{room.RoomTitle}@", clientChar.key);

            // 자신의 정보를 알려줌
            SendMessage($"UpdateClient#-1#{clientChar.SkinNum}@", clientChar.key);

            // 접속한 클라이언트에게 방에 있는 클라이언트들 정보를 알려줌
            foreach (var item in room.roomClientDic)
            {
                if (item.Key == clientChar.key) continue;
                SendMessage($"EnterRoomOther#{item.Key}#{item.Value.bReady}#{item.Value.SkinNum}@", clientChar.key);
            }

            // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
            SendMessageToAll_InRoom($"EnterRoomOther#{clientChar.key}#False#{clientChar.SkinNum}@", clientChar.RoomKey, clientChar.key);
        }
   

        // 메세지 전송
        public void SendMessage(string message, int recieverKey)
        {
            ClientCharacter clientChar;

            bool result = clientManager.ClientDic.TryGetValue(recieverKey, out clientChar);
            if (result == false) return;

            server.SendMessage(message, clientChar.clientData);

        }

        // 모든 클라이언트들에게 메세지 전송 ( senderKey로 예외 클라이언트 설정 )
        public void SendMessageToAll(string message, int senderKey = -1)
        {
            foreach (var item in clientManager.ClientDic)
            {
                if (item.Value.key == senderKey) continue;

                SendMessage(message, item.Value.key);
            }

        }

        // 방 안의 모든 클라이언트들에게 메세지 전송 ( senderKey로 예외 클라이언트 설정 )
        public void SendMessageToAll_InRoom(string message,int roomKey, int senderKey = -1)
        {
            // 킷값을 이용하여 방을 찾음, 없으면 return
            Room room;
            bool result = roomManager.RoomDic.TryGetValue(roomKey, out room);
            if (result == false) return;

            
            foreach (var item in room.roomClientDic)
            {
                if (item.Value.key == senderKey) continue;

                SendMessage(message, item.Value.key);
            }

        }


        // 대상 클라이언트 머리위에 있는 클라이언트 리스트 반환
        public List<ClientCharacter> GetClientsOverTheHead(ClientCharacter client)
        {
            List<ClientCharacter> list = new List<ClientCharacter>();

            // 대상의 머리위 충돌박스
            Size size = new Size(client.size.Width, 10);
            Point location = new Point(client.Location.X, client.Location.Y - 10);
            Rectangle a = new Rectangle(location, size);

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

