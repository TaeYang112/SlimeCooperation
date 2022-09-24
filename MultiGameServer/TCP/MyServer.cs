using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

// -----------------
// ----- 서버 ------
// -----------------

namespace MultiGameServer
{
    public delegate void ClientJoinEventHandler(ClientData newClient);
    public delegate void ClientLeaveEventHandler(ClientData oldClient);
    class MyServer
    {

        // TCP통신에서 서버를 담당하는 클래스
        public TcpListener server { get; set; }

        // server를 실행시킬 스레드    
        private Thread server_tr;

        // 클라이언트가 접속할경우 ClientJoin에 연결된 함수를 호출함
        public event ClientJoinEventHandler ClientJoin;

        // 클라이언트가 접속할경우 ClientLeave에 연결된 함수를 호출함
        public event ClientLeaveEventHandler ClientLeave;


        public MyServer()
        {
            // TcpListener 클래스 생성 ( IP, 포트 )
            server = new TcpListener(System.Net.IPAddress.Any, 8898);

            // 클라이언트 실행시킬 스레드 설정
            server_tr = new Thread(WaitAndAcceptClient);
            server_tr.IsBackground = true;

            
        }

        ~MyServer()
        {
            server.Stop();
            server_tr.Interrupt();
        }


        public void Start()
        {
            // 서버 시작
            server.Start();

            // 쓰레드 시작
            server_tr.Start();
        }

        private void WaitAndAcceptClient()
        {
            try
            {
                while (true)
                {
                    // 서버에 접속하려는 client 접속요청 허용후 클라이언트 객체에 할당
                    // 접속하려는 client가 없으면 무한 대기
                    TcpClient AcceptClient = server.AcceptTcpClient();

                    // ClientJoin 이벤트에 연결된 함수를 호출함
                    ClientJoin(new ClientData(AcceptClient));
                }
            }
            catch(ThreadStateException)
            {
                return;
            }

        }

        public void SendMessage(string message, ClientData receiver)
        {
            // 서버로 메세지 전송 하기 위한 string to byte 형변환
            byte[] buf = Encoding.Default.GetBytes(message);

            try
            {
                // 메세지 전송
                receiver.client.GetStream().Write(buf, 0, buf.Length);
            }
            catch
            {
                ClientLeave(receiver);
                Console.WriteLine("에러메세지 : " + message);
            }
        }

    }
}
