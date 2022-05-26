﻿using System;
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
        private MyServer server;
        public RoomManager roomManager { get; set; }        // 방을 관리하는 객체
        public ClientManager clientManager { get; set; }    // 클라이언트들을 관리하는 객체

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

            clientManager = new ClientManager();
            roomManager = new RoomManager();

        }

        public void Start()
        {
            server.Start();
        }


        // 서버에 새로운 클라이언트가 접속하면 호출됨
        private void OnClientJoin(ClientData newClientData)
        {
            ClientCharacter newClient = clientManager.AddClient(newClientData);
            newClient.LocationSync += SyncLocation;

            Console.WriteLine(newClient.key + "번 클라이언트가 접속하였습니다.");


            // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
            newClient.clientData.client.GetStream().BeginRead(newClient.clientData.byteData, 0, newClient.clientData.byteData.Length, new AsyncCallback(DataRecieved), newClient);
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
                            char InpKey = char.Parse(SplitMessage[1]);                 // 입력된 키
                            char cKeyDown = char.Parse(SplitMessage[2]);            // 눌려있으면 T / F

                            bool bKeyDown = cKeyDown == 'T' ? true : false;         // T 이면 true / false

                            switch (InpKey)
                            {
                                case 'L':
                                    clientChar.bLeftDown = bKeyDown;
                                    break;
                                case 'R':
                                    clientChar.bRightDown = bKeyDown;
                                    break;
                            }
                            if (bKeyDown == true) clientChar.MoveStart();
                            else if (!(clientChar.bLeftDown || clientChar.bRightDown))
                            {
                                clientChar.MoveStop();
                            }

                            // 다른 클라이언트들에게 이 클라이언트의 입력을 알림
                            SendMessageToAll_InRoom($"KeyInput#{clientChar.key}#{InpKey}#{cKeyDown}@",clientChar.RoomKey, clientChar.key);
                            if (bKeyDown == false) SyncLocation(clientChar);
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
                    case "TryEnterRoom":
                        {
                            // 방 키
                            int roomKey = int.Parse(SplitMessage[1]);

                            // 입장
                            ClientEnterRoom(roomKey, clientChar);
                        }
                        break;
                    default:
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
            SendMessage($"EnterRoom#{room.key}@", clientChar.key);
        }

        // 로비단계에 있는 게임 방을 시작시킴
        public void RoomStart(Room room)
        {
            // room.start();
            foreach(var item in room.roomClientDic)
            {
                // 클라이언트에게 게임이 시작하였다고 알림
                SendMessage($"RoomStart@", item.Key);

                // 접속한 클라이언트에게 방에 있는 클라이언트들 정보를 알려줌
                foreach (var item2 in room.roomClientDic)
                {
                    // 본인 정보는 킷값을 -1로 전송
                    if (item2.Key == item.Key) 
                        SendMessage($"ClientInfo#-1#{item.Value.Location.X}#{item.Value.Location.Y}#@", item.Key);
                    else
                        SendMessage($"ClientInfo#{item2.Key}#{item2.Value.Location.X}#{item2.Value.Location.Y}#@", item.Key);
                }


                // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
                SendMessageToAll_InRoom($"ClientInfo#{item.Key}#{item.Value.Location.X}#{item.Value.Location.Y}#@",item.Value.RoomKey, item.Key);

            }
        }
        

        // 메세지 전송
        public void SendMessage(string message, int recieverKey)
        {
            ClientCharacter clientChar;

            clientManager.ClientDic.TryGetValue(recieverKey, out clientChar);
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

