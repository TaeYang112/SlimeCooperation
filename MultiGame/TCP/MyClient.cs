using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;



namespace MultiGame
{
    public delegate void TakeMessageEventHandler(string message);
    public delegate void ExceptionEventHandler(Exception exception);
    public class MyClient
    {
        // 서버로부터 메세지가 도착하면 이벤트 알림
        public event TakeMessageEventHandler TakeMessage;

        // 에러가 발생하면 이벤트 알림
        public event ExceptionEventHandler onException;

        // 메세지 버퍼
        private byte[] readByteData;

        // TcpClient ( 통신 클래스 )
        private TcpClient client;

        // client를 실행시킬 스레드            
        private Thread client_tr;                                                  


        public MyClient()
        {
            readByteData = new byte[1024];

            // 클라이언트 생성
            client = new TcpClient();

            client.NoDelay = true;

            // 클라이언트 실행시킬 스레드
            client_tr = new Thread(TryToConnect);
            client_tr.IsBackground = true;
        }

        ~MyClient()
        {
            //클라이언트 종료
            client.Close();

        }


        // 클라이언트(스레드) 실행
        public void Start()
        {
            client_tr.Start();
        }


        // 서버에 연결
        public void TryToConnect()
        {
            while (true)
            {
                try
                {
                    // 서버에 연결 ( 서버IP, 포트 )
                    client.Connect("119.196.90.61", 8898);
                }
                catch
                {
                    Console.WriteLine("서버에 접속에 실패하였습니다.");
                    Console.WriteLine("접속 시도중...");
                    Thread.Sleep(1000);
                    continue;
                }
                Console.WriteLine("서버에 접속하였습니다.");
                break;
            }
            
            // 서버로 부터 메세지를 받을경우 OnMessageReceive 메소드 호출
            client.GetStream().BeginRead(readByteData, 0, readByteData.Length, new AsyncCallback(OnMessageReceive), null);
        }


        // 메세지를 서버로 보냄
        public void SendMessage(string message)
        {
            // 서버로 메세지 전송 하기 위한 string to byte 형변환
            byte[] buf = Encoding.Default.GetBytes(message);

            try
            {
                // 서버로 write
                client.GetStream().Write(buf, 0, buf.Length);
            }
            catch(System.InvalidOperationException e)
            {
                onException(e);
            }
        }

        private void OnMessageReceive(IAsyncResult ar)
        {
            // 출력을 위해 byte를 String으로 형변환
            string stringData = Encoding.Default.GetString(readByteData);
            Array.Clear(readByteData, 0, readByteData.Length);

            // 이벤트 발생 ( 이벤트에 연결된 함수들 호출 ) 
            TakeMessage(stringData);

            // 다시 메세지가 올때 이 함수가 호출되도록 함
            try
            {
                client.GetStream().BeginRead(readByteData, 0, readByteData.Length, new AsyncCallback(OnMessageReceive), null);

            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("서버와 연결이 끊어졌습니다.");
            }
            catch
            {
                Console.WriteLine("알수 없는 오류발생");
            }
        }
    }
}
