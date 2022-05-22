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
        private MyServer server;
        public ClientManager clientManager { get; set; }        // 클라이언트들을 관리하는 객체
        

        static void Main(string[] args)
        {
            Program program = new Program();

            program.Start();

            while(true)
                Console.ReadLine();

        }

        public Program()
        {
            server = new MyServer();
            server.ClientJoin += new ClientJoinEventHandler(OnClientJoin);

            clientManager = new ClientManager();

        }

        public void Start()
        {
            server.Start();
        }

        private void ClientMove(object clientArgs)
        {
            Console.WriteLine("시작");
            ClientCharacter client = clientArgs as ClientCharacter;
            while (true)
            {
                if(client.IsMoving == true)
                {
                    Point Location = client.Location;
                    if (client.bLeftDown == true) Location.X -= 2;
                    if (client.bRightDown == true) Location.X += 2;

                    client.Location = Location;
                    Console.WriteLine($"X : {Location.X}    Y : {Location.Y}");
                }
                Thread.Sleep(10);
            }
        }


        // 서버에 새로운 클라이언트가 접속하면 호출됨
        private void OnClientJoin(ClientData newClientData)
        {
            ClientCharacter newClient = clientManager.AddClient(newClientData);
            newClient.Location = new Point(364, 293);
            newClient.characterMove_tr = new Thread(ClientMove);
            newClient.characterMove_tr.Start(newClient);
            


            Console.WriteLine(newClient.key + "번 클라이언트가 접속하였습니다.");


            // 새로 접속한 클라이언트에게 있어야 할 위치를 알려줌
            SendMessage($"LOC#-1#{newClient.Location.X}#{newClient.Location.Y}#@", newClient.key);

            // 새로 접속한 클라이언트에게 기존에 있던 클라이언트를 알려줌
            foreach (var item in clientManager.ClientDic)
            {
                if (item.Value.key == newClient.key) continue;

                SendMessage($"NCL#{item.Value.key}#{item.Value.Location.X}#{item.Value.Location.Y}#@", newClient.key);
            }


            // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
            SendMessageToAll($"NCL#{newClient.key}#{newClient.Location.X}#{newClient.Location.Y}#@", newClient.key);




            // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
            newClient.clientData.client.GetStream().BeginRead(newClient.clientData.byteData, 0, newClient.clientData.byteData.Length, new AsyncCallback(DataRecieved), newClient);
        }

        // 메세지 수신
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
            string[] SplitMessage = message.Split('#');
            switch (SplitMessage[0])
            {
                // 클라이언트의 키보드 입력 ( Input )
                case "INP":
                    {
                        char key = char.Parse(SplitMessage[1]);                 // 입력된 키
                        char cKeyDown = char.Parse(SplitMessage[2]);            // 눌려있으면 T / F

                        bool bKeyDown = cKeyDown == 'T' ? true : false;         // T 이면 true / false

                        switch (key)
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

                            // 클라이언트에게 있어야 할 위치를 알려줌
                            SendMessage($"LOC#-1#{clientChar.Location.X}#{clientChar.Location.Y}#@", clientChar.key);
                        }

                    }
                    break;
                default:
                    break;
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
    }

}
