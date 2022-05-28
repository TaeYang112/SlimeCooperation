using System;
using System.Collections.Concurrent;
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
        // TCP 서버를 관리하고 클라이언트와 통신하는 객체
        private MyServer server;

        // 방을 관리하는 객체
        public RoomManager roomManager { get; set; }

        // 클라이언트들을 관리하는 객체
        public ClientManager clientManager { get; set; }

        // 서버와 클라이언트가 계속 연결되어있는지 확인하기 위해 일정시간마다 가짜 메세지를 보냄
        private System.Threading.Timer HeartBeatTimer;

        static void Main(string[] args)
        {
            Program program = new Program();
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
                                program.RoomStart(room);
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

        public Program()
        {
            server = new MyServer();
            server.ClientJoin += new ClientJoinEventHandler(OnClientJoin);
            server.ClientLeave += new ClientLeaveEventHandler(OnClientLeave);

            clientManager = new ClientManager();
            roomManager = new RoomManager();

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
            newClient.LocationSync += SyncLocation;

            Console.WriteLine("[INFO] "+ newClient.key + "번 클라이언트가 접속하였습니다.");


            // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
            newClient.clientData.client.GetStream().BeginRead(newClient.clientData.byteData, 0, newClient.clientData.byteData.Length, new AsyncCallback(DataRecieved), newClient);
        }

        // 클라이언트와 연결이 끊기면 호출됨
        private void OnClientLeave(ClientData oldClientData)
        {
            Console.WriteLine("[INFO] " + oldClientData.key + "번 클라이언트와의 연결이 끊겼습니다.");

            // 클라이언트 배열에서 제거
            ClientCharacter clientChar = clientManager.RemoveClient(oldClientData);

            // 클라이언트가 속해있는 방을 가져옴
            Room room;
            bool result = roomManager.RoomDic.TryGetValue(clientChar.RoomKey, out room);

            // 방이 존재하지 않으면 종료
            if(result == false)
            {
                return;
            }

            // 다른 플레이어들한테 알려줌
            SendMessageToAll_InRoom($"LeaveRoom#{clientChar.key}@",room.key, clientChar.key);

            // 룸의 클라이언트 배열에서도 제거
            room.ClientLeave(clientChar);

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
            Console.WriteLine("[INFO] 메세지 수신 : " + message);
            string[] Messages = message.Split('@');
            for (int i = 0; i < Messages.Length - 1; i++)
            {
                string[] SplitMessage = Messages[i].Split('#');
                switch (SplitMessage[0])
                {
                    // 클라이언트의 키보드 입력
                    case "KeyInput":
                        {
                            // 입력된 키
                            char InpKey = char.Parse(SplitMessage[1]);

                            // 눌려있으면 true / 아니면 false
                            bool bKeyDown = bool.Parse(SplitMessage[2]);

                            switch (InpKey)
                            {
                                case 'L':
                                    clientChar.bLeftDown = bKeyDown;
                                    break;
                                case 'R':
                                    clientChar.bRightDown = bKeyDown;
                                    break;
                            }
                            // 키가 눌렸다면 움직임 타이머 시작
                            if (bKeyDown == true)
                            {
                                clientChar.MoveStart();
                            }
                            // 모든 키가 떼어졌다면 움직임 타이머 종료
                            else if (!(clientChar.bLeftDown || clientChar.bRightDown))
                            {
                                clientChar.MoveStop();
                            }

                            // 다른 클라이언트들에게 이 클라이언트의 입력을 알림
                            SendMessageToAll_InRoom($"KeyInput#{clientChar.key}#{InpKey}#{bKeyDown}@",clientChar.RoomKey, clientChar.key);

                            // 키가 떼어졌을때 좌표를 보내서 완벽히 동기화 함
                            if (bKeyDown == false)
                            {
                                SyncLocation(clientChar);
                            }
                        }
                        break;
                    // 방 만들기 요청
                    case "CreateRoom":
                        {
                            // 방 제목
                            string RoomTitle = SplitMessage[1];

                            // 방 생성
                            Room newRoom = roomManager.CreateRoom(RoomTitle);

                            // 방 만든사람을 방에 접속시킴
                            ClientEnterRoom(newRoom.key, clientChar);
                        }
                        break;
                    // 방 입장 시도
                    case "TryEnterRoom":
                        {
                            // 방 키
                            int roomKey = int.Parse(SplitMessage[1]);

                            // 입장
                            ClientEnterRoom(roomKey, clientChar);
                        }
                        break;
                    // 레디
                    case "Ready":
                        {
                            // 레디 여부를 대입
                            bool bReady = bool.Parse(SplitMessage[1]);
                            clientChar.bReady = bReady;
                            
                            if( bReady == true)
                            {
                                // 클라이언트가 들어있는 방을 찾음
                                Room room;
                                bool result = roomManager.RoomDic.TryGetValue(clientChar.RoomKey, out room);

                                // 존재하지 않으면 return
                                if(result == false)
                                {
                                    return;
                                }

                                // 3명 이상의 클라이언트가 레디했다면
                                if( room.IsAllReady() == true )
                                {
                                    // 게임시작
                                    RoomStart(room);
                                }
                            }
                            
                        }
                        break;
                    default:
                        Console.WriteLine("디폴트 : {0}", Messages[i]);
                        break;
                }
            }
        }

        // 클라이언트를 방에 입장시킬 때 호출
        public void ClientEnterRoom(int roomKey, ClientCharacter clientChar)
        {
            // roomKey를 이용하여 room 객체를 찾음, 만약 없으면 return
            Room room;
            bool result = roomManager.RoomDic.TryGetValue(roomKey, out room);

            if (result == false) return;



            // 방 입장 ( 서버 관점 )
            room.ClientEnter(clientChar);

            // 방 입장을 클라이언트한테 알림
            SendMessage($"EnterRoom#{room.key}#{room.RoomTitle}@", clientChar.key);


            // 접속한 클라이언트에게 방에 있는 클라이언트들 정보를 알려줌
            foreach (var item2 in room.roomClientDic)
            {
                SendMessage($"EnterRoomOther#{item2.Key}#{item2.Value.bReady}#@", clientChar.key);
            }

            // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
            SendMessageToAll_InRoom($"EnterRoomOther#{clientChar.key}#False#@", clientChar.RoomKey, clientChar.key);
        }




        // 로비단계에 있는 게임 방을 시작시킴
        public void RoomStart(Room room)
        {
            // room.start();
            foreach(var item in room.roomClientDic)
            {
                // 클라이언트에게 게임이 시작하였다고 알림
                SendMessage($"RoomStart@", item.Key);

               

            }
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

        // 각 캐릭터의 좌표를 클라이언트와 서버간 동기화하기 위해 호출
        public void SyncLocation(ClientCharacter clientChar)
        {
            // 클라이언트에게 있어야 할 위치를 알려줌
            SendMessage($"Location#-1#{clientChar.Location.X}#{clientChar.Location.Y}#@", clientChar.key);

            // 방 안의 다른 클라이언트에도 이 클라이언트가 있어야할 위치를 알려줌
            SendMessageToAll_InRoom($"Location#{clientChar.key}#{clientChar.Location.X}#{clientChar.Location.Y}#@",clientChar.RoomKey ,clientChar.key);
        }
    }

}

