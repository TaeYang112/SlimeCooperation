using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiGameServer
{
    class Server
    {
        static void Main(string[] args)
        {
            MyServer server = new MyServer();
            server.Start();
        }
    }


    class MyServer
    {
        // TCP통신에서 서버를 담당하는 클래스
        public TcpListener server { get; set; }

        // 클라이언트를 관리하기 위해 만든 클래스
        public ClientManager clientManager { get; set; }

        public MyServer()
        {
            // TcpListener 클래스 생성 ( IP, 포트 )
            server = new TcpListener(System.Net.IPAddress.Any, 8898);

            clientManager = new ClientManager();
        }

        ~MyServer()
        {
            server.Stop();
        }


        public void Start()
        {
            // 서버 시작
            server.Start();


            while (true)
            {
                // 서버에 접속하려는 client 접속요청 허용후 클라이언트 객체에 할당
                // 접속하려는 client가 없으면 무한 대기
                TcpClient AcceptClient = server.AcceptTcpClient();

                ClientData clientData = clientManager.AddClient(AcceptClient);

                Console.WriteLine(clientData.key + "번 클라이언트가 접속하였습니다.");

                // 새로 접속한 클라이언트에게 기존에 있던 클라이언트를 알려줌
                foreach (var item in clientManager.ClientDic)
                {
                    if (item.Value.key == clientData.key) continue;

                    SendMessage($"NCL#{item.Value.key}#{item.Value.Location.X}#{item.Value.Location.Y}#@", clientData.key);
                }

                // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
                SendMessageToAll($"NCL#{clientData.key}#{clientData.Location.X}#{clientData.Location.Y}#@", clientData.key);

                // client의 메세지가 발생하면 DataRecieved 메소드가 호출되도록 예약
                clientData.client.GetStream().BeginRead(clientData.byteData, 0, clientData.byteData.Length, new AsyncCallback(DataRecieved), clientData);
            }

        }

        // 메세지 수신
        private void DataRecieved(IAsyncResult ar)
        {
            ClientData clientData = ar.AsyncState as ClientData;
            try
            {
                int bytesRead = clientData.client.GetStream().EndRead(ar);


                // 전달받은 byte를 string으로 바꿈
                string stringData = Encoding.Default.GetString(clientData.byteData, 0, bytesRead);

                // 메세지를 해석함
                ParseMessage(clientData, stringData);
                //Console.WriteLine(stringData);
                clientData.client.GetStream().BeginRead(clientData.byteData, 0, clientData.byteData.Length, new AsyncCallback(DataRecieved), clientData);
            }
            catch
            {
                Console.WriteLine(clientData.key + "번 클라이언트와의 연결이 끊어졌습니다.");
                clientManager.RemoveClient(clientData);
            }

        }

        // 받은 메세지를 해석함
        private void ParseMessage(ClientData clientData, string message)
        {
            string[] SplitMessage = message.Split('#');
            switch (SplitMessage[0])
            {
                // 클라이언트의 캐릭터 위치를 갱신함
                case "LOC":
                    {
                        int x = int.Parse(SplitMessage[1]);
                        int y = int.Parse(SplitMessage[2]);

                        clientData.Location = new Point(x, y);

                        // 나머지 클라이언트들에게도 전달함
                        SendMessageToAll($"LOC#{clientData.key}#{x}#{y}#@", clientData.key);
                    }
                    break;
                default:
                    break;
            }

        }

        // 메세지 송신
        public void SendMessageToAll(string message, int sender = -1)
        {
            // 서버로 메세지 전송 하기 위한 string to byte 형변환
            byte[] buf = Encoding.Default.GetBytes(message);

            foreach (var item in clientManager.ClientDic)
            {
                if (item.Value.key == sender) continue;

                try
                {
                    // 메세지 전송
                    item.Value.client.GetStream().Write(buf, 0, buf.Length);
                }
                catch
                {
                    clientManager.RemoveClient(item.Value);
                }

            }

        }

        public void SendMessage(string message, int recieverKey = -1)
        {
            // 서버로 메세지 전송 하기 위한 string to byte 형변환
            byte[] buf = Encoding.Default.GetBytes(message);

            ClientData receiver;
            clientManager.ClientDic.TryGetValue(recieverKey, out receiver);

            try
            {
                // 메세지 전송
                receiver.client.GetStream().Write(buf, 0, buf.Length);
            }
            catch
            {
                clientManager.RemoveClient(receiver);
            }

        }

    }



    // 클라이언트들을 관리하는 클래스
    class ClientManager
    {
        // 클라이언트들을 담는 배열
        // 단순 배열과 다른점은 여러개의 스레드가 접근할때 자동으로 동기화 시켜줌
        public ConcurrentDictionary<int, ClientData> ClientDic { get; set; }

        // 새로운 클라이언트에게 부여할 킷값을 저장
        private int CurrentKey;

        public ClientManager()
        {
            ClientDic = new ConcurrentDictionary<int, ClientData>();
            CurrentKey = 0;
        }

        public ClientData AddClient(TcpClient newClient)
        {
            ClientData newClientData = new ClientData(CurrentKey, newClient);
            // 새로운 클라이언트를 배열에 저장
            ClientDic.TryAdd(CurrentKey, newClientData);

            CurrentKey++;

            return newClientData;
        }

        public void RemoveClient(ClientData clientData)
        {
            ClientData temp;
            ClientDic.TryRemove(clientData.key, out temp);
        }
    }



    // TcpClient만으로는 클라이언트를 표현하기 힘들어 만든 클래스
    class ClientData
    {
        // TCP 통신에서 TcpServer에 대응되는 클라이언트 객체
        public TcpClient client { get; set; }

        // 메세지를 받을 때 사용하는 버퍼
        public byte[] byteData { get; set; }

        // 각 클라이언트를 구별하기 위한 킷값
        public int key { get; set; }

        public Point Location { get; set; }

        public ClientData(int key, TcpClient client)
        {
            this.key = key;
            this.client = client;
            byteData = new byte[1024];
            Location = new Point(0, 0);
        }
    }
}
