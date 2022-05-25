using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace MultiGame
{
    public partial class Form1 : Form
    {
        private MyClient myClient;                                                 // 서버와 TCP통신을 담당하는 객체
        private ClientManager clientManager;                                       // 다른 클라이언트들을 관리하는 객체
        private ClientCharacter userCharacter;

        private System.Threading.Timer InvalidTimer;
        public Form1()
        {
            InitializeComponent();
            // 다른 클라이언트들을 관리할 객체
            clientManager = new ClientManager();

            // 사용자 캐릭터
            userCharacter = new ClientCharacter(-1,new Point(364,293), 0);

            // 클라이언트 객체 생성
            myClient = new MyClient();

            // 서버로부터 메세지를 받으면 onTakeMessage함수 호출
            myClient.TakeMessage += new TakeMessageEventHandler(OnTakeMessage);

        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            // 클라이언트 시작
            myClient.Start();


            // 60프레임 Invalid ( 테스트 )
            TimerCallback tc = new TimerCallback(Invalidate_);                              
            InvalidTimer = new System.Threading.Timer(tc, null, 0, 10);  
        }

        public void Invalidate_(object d)
        {
            Invalidate();
        }

        // 서버로부터 받은 메세지를 해석함
        private void OnTakeMessage(string message)
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

                            ClientCharacter client;

                            // key == -1 ( 유저 캐릭터 )
                            if (key == -1)
                                client = userCharacter;
                            else
                            {
                                // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                                bool result = clientManager.ClientDic.TryGetValue(key, out client);

                                // 해당 클라이언트가 존재하지 않을경우 continue
                                if (result == false) continue;
                            }
                            if (key == -1)
                                Console.WriteLine($"동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");
                            else
                                Console.WriteLine($"{client.key}번 클라이언트 동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");

                                client.Location = new Point(x, y);
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
                            ClientCharacter clientCharacter = clientManager.AddClient(key, new Point(41,49), 1);
                        }
                        break;
                    // 다른 클라이언트의 키보드 입력 ( Input )
                    case "INP":
                        {
                            // 플레이어 번호
                            int key = int.Parse(SplitMessage[1]);

                            // 입력된 키
                            char InpKey = char.Parse(SplitMessage[2]);

                            // 눌렸으면 T / 아니면 F
                            char cKeyDown = char.Parse(SplitMessage[3]);

                            // T 이면 true / F 이면 false
                            bool bKeyDown = cKeyDown == 'T' ? true : false;

                            ClientCharacter client;

                            // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                            bool result = clientManager.ClientDic.TryGetValue(key, out client);

                            // 해당 클라이언트가 존재하지 않을경우 continue
                            if (result == false) continue;

                            switch (InpKey)
                            {
                                case 'L':
                                    client.bLeftDown = bKeyDown;
                                    break;
                                case 'R':
                                    client.bRightDown = bKeyDown;
                                    break;
                            }
                            if (bKeyDown == true) client.MoveStart();
                            else if (!(client.bLeftDown || client.bRightDown))
                            {
                                client.MoveStop();
                            }

                        }
                        break;
                    default:
                        Console.WriteLine("디폴트 : {0}", Messages[i]);
                    break;
                }
            }
        }
        
        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (userCharacter.bLeftDown == false)
                {
                    userCharacter.bLeftDown = true;                                     // bLeftDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    myClient.SendInputedKey('L', true);                                 // 서버한테 이동을 시작했다고 알림
                    userCharacter.MoveStart();
                }
                return true;
            }
            if (keyData == Keys.Right)
            {
                if (userCharacter.bRightDown == false)
                {
                    userCharacter.bRightDown = true;                                    // bRightDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    myClient.SendInputedKey('R', true);                                 // 서버한테 이동을 시작했다고 알림
                    userCharacter.MoveStart();                                            // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                }
                    
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    userCharacter.bLeftDown = false;                                                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    myClient.SendInputedKey('L', false);
                    break;
                case Keys.Right:
                    userCharacter.bRightDown = false;                                                   // 왼쪽 방향키가 떼어졌다는걸 알림
                    myClient.SendInputedKey('R', false);
                    break;
            }

            if (!(userCharacter.bLeftDown || userCharacter.bRightDown)) userCharacter.MoveStop();     // 왼쪽과 오른쪽 모두 떼어져 있다면 캐릭터 이동 타이머를 멈춤
        }




        // 폼의 포커스가 풀리면 ( 알트 탭 등 ) Key UP 이벤트가 안생기기 때문에 강제로 키 다운 변수를 false로 바꿈
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            userCharacter.bLeftDown = false;                                                            // 왼쪽 방향키가 떼어졌다는걸 알림
            myClient.SendInputedKey('L', false);

            userCharacter.bRightDown = false;                                                           // 왼쪽 방향키가 떼어졌다는걸 알림
            myClient.SendInputedKey('R', false);

            userCharacter.MoveStop();

        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            foreach(var item in clientManager.ClientDic)
            {
                item.Value.OnPaint(pe);
            }
            userCharacter.OnPaint(pe);
        }
    }
}