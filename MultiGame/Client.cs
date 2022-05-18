using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace MultiGame
{ 
    public partial class Form1
    {
        public class MyClient
        {

            private byte[] readByteData;                            // 메세지 버퍼
            private TcpClient client;                               // TcpClient ( 통신 클래스 )
            private ClientManager clientManager;                    // 다른 클라이언트들을 관리하는 클래스
            private Form1 FormInst;                                 // 메인 윈도우 폼         
            private Thread client_tr;                               // client를 실행시킬 스레드                               


            public MyClient(Form1 FormInst)
            {
                this.FormInst = FormInst;

                readByteData = new byte[1024];

                // 클라이언트 생성
                client = new TcpClient();

                // 다른 클라이언트들을 관리할 객체
                clientManager = new ClientManager();

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
                        client.Connect("222.108.250.61", 8898);
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


            // 클라이언트의 키 입력을 서버로 보냄
            public void SendInputedKey(char inputKey, bool bPressed)
            {
                // 패킷(정보)
                string message = $"KEY#{inputKey}#{(bPressed == true ? 1 : 0)}#@";
                // 서버로 메세지 전송 하기 위한 string to byte 형변환
                byte[] buf = Encoding.Default.GetBytes(message);

                // 서버로 write
                client.GetStream().Write(buf, 0, buf.Length);
            }


            private void OnMessageReceive(IAsyncResult ar)
            {
                // 출력을 위해 byte를 String으로 형변환
                string stringData = Encoding.Default.GetString(readByteData);
                Array.Clear(readByteData, 0, readByteData.Length);
                ParseMessage(stringData);

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

            // 받은 메세지를 해석함
            private void ParseMessage(string message)
            {
                // 메세지는 '@'으로 끝을 구분함, 메세지 여러개가 겹쳐있을 수 있기때문에 Split으로 나눔
                string[] Messages = message.Split('@');

                for (int i = 0; i < Messages.Length - 1; i++)
                {
                    // 메세지는 '#'으로 각 매개인자를 구분함
                    string[] SplitMessage = Messages[i].Split('#');

                    switch (SplitMessage[0])
                    {
                        // 다른 클라이언트의 캐릭터 위치를 갱신함 ( Location )
                        case "LOC":
                            {
                                // 플레이어 번호
                                int key = int.Parse(SplitMessage[1]);

                                // 좌표
                                int x = int.Parse(SplitMessage[2]);
                                int y = int.Parse(SplitMessage[3]);

                                ClientData client;
                                bool result = clientManager.ClientDic.TryGetValue(key, out client);

                                // 해당 클라이언트가 존재하지 않을경우 continue
                                if (result == false) continue;


                                // 메인 스레드에 있는 폼에 접근하기 위해서는 Invoke 사용해야됨
                                if (client.character.InvokeRequired)
                                {
                                    client.character.Invoke(new MethodInvoker(delegate ()
                                    {
                                        client.character.Location = new Point(x, y);
                                    }));
                                }
                                else
                                {
                                    client.character.Location = new Point(x, y);
                                }
                            }
                            break;
                        // 새로운 클라이언트가 접속함 ( New Client )
                        case "NCL":
                            {
                                // 플레이어 번호
                                int key = int.Parse(SplitMessage[1]);

                                // 플레이어 좌표
                                int x = int.Parse(SplitMessage[2]);
                                int y = int.Parse(SplitMessage[3]);

                                // 새로운 클라이언트의 캐릭터 생성
                                Button character = new Button();

                                character.Location = new Point(x, y);
                                character.Text = key.ToString();

                                // 관리를 위해 클라이언트 매니저에 등록
                                ClientData clientData = clientManager.AddClient(key, character);

                                // 메인 스레드에 있는 폼에 접근하기 위해서는 Invoke 사용해야됨
                                if (FormInst.InvokeRequired)
                                {
                                    FormInst.Invoke(new MethodInvoker(delegate ()
                                    {
                                        character.Size = new Size(70, 70);
                                        FormInst.Controls.Add(character);
                                    }));
                                }
                                else
                                {
                                    character.Size = new Size(70, 70);
                                    FormInst.Controls.Add(character);
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("디폴트 : {0}", Messages[i]);
                            break;
                    }
                }
            }


            // 클라이언트들을 관리하는 클래스
            public class ClientManager
            {
                // 클라이언트들을 담는 배열
                // 단순 배열과 다른점은 여러개의 스레드가 접근할때 자동으로 동기화 시켜줌
                public ConcurrentDictionary<int, ClientData> ClientDic { get; set; }


                public ClientManager()
                {
                    ClientDic = new ConcurrentDictionary<int, ClientData>();
                }

                public ClientData AddClient(int key, Button character)
                {
                    ClientData newClientData = new ClientData(key, character);
                    // 새로운 클라이언트를 배열에 저장
                    ClientDic.TryAdd(key, newClientData);

                    return newClientData;
                }

                public void RemoveClient(ClientData clientData)
                {
                    ClientData temp;
                    ClientDic.TryRemove(clientData.key, out temp);
                }
            }



            // Client 정보를 갖는 클래스
            public class ClientData
            {
                // 각 클라이언트를 구별하기 위한 킷값
                public int key { get; set; }

                public Button character { get; set; }

                public ClientData(int key, Button character)
                {
                    this.key = key;
                    this.character = character;
                }
            }
        }
    }
}