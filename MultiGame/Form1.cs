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
        private System.Windows.Forms.Timer MoveTimer;                              // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private ClientManager clientManager;                                       // 다른 클라이언트들을 관리하는 객체
        private ClientCharacter userCharacter;
        public Form1()
        {
            InitializeComponent();

            // 다른 클라이언트들을 관리할 객체
            clientManager = new ClientManager();

            // 사용자 캐릭터
            userCharacter = new ClientCharacter(-1, button1);

            // 클라이언트 객체 생성
            myClient = new MyClient();

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            MoveTimer = new System.Windows.Forms.Timer();
            MoveTimer.Interval = 10;
            MoveTimer.Tick += new EventHandler(MoveCharacter_timer);

            // 서버로부터 메세지를 받으면 onTakeMessage함수 호출
            myClient.TakeMessage += new TakeMessageEventHandler(OnTakeMessage);

        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            // 클라이언트 시작
            myClient.Start();
        }


        // 현재 KeyDown 되어있는 키를 확인하여 움직임
        private void MoveCharacter_timer(object sender, EventArgs e)
        {
            Point Location = button1.Location;
            if (userCharacter.bLeftDown == true) Location.X -= 2;                                                // 왼쪽 방향키가 눌려있는 상태라면 왼쪽으로 움직임
            if (userCharacter.bRightDown == true) Location.X += 2;                                               // 오른쪽 방향키가 눌려있는 상태라면 오른쪽으로 움직임

            button1.Location = Location;
            Console.WriteLine($"X : {Location.X}    Y : {Location.Y}");
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
                                Console.WriteLine($"X : {client.character.Location.X}  Y : {client.character.Location.Y}  ->  X : {x}  Y : {y}");


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
                            ClientCharacter clientCharacter = clientManager.AddClient(key, character);

                            // 메인 스레드에 있는 폼에 접근하기 위해서는 Invoke 사용해야됨
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    character.Size = new Size(70, 70);
                                    this.Controls.Add(character);
                                }));
                            }
                            else
                            {
                                character.Size = new Size(70, 70);
                                this.Controls.Add(character);
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
                    MoveTimer.Start();                                                  // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                }
                return true;
            }
            if (keyData == Keys.Right)
            {
                if (userCharacter.bRightDown == false)
                {
                    userCharacter.bRightDown = true;                                    // bRightDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    myClient.SendInputedKey('R', true);                                 // 서버한테 이동을 시작했다고 알림
                    MoveTimer.Start();                                                  // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
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

            if (!(userCharacter.bLeftDown || userCharacter.bRightDown)) MoveTimer.Stop();               // 왼쪽과 오른쪽 모두 떼어져 있다면 캐릭터 이동 타이머를 멈춤
        }




        // 폼의 포커스가 풀리면 ( 알트 탭 등 ) Key UP 이벤트가 안생기기 때문에 강제로 키 다운 변수를 false로 바꿈
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            userCharacter.bLeftDown = false;                                                            // 왼쪽 방향키가 떼어졌다는걸 알림
            myClient.SendInputedKey('L', false);

            userCharacter.bRightDown = false;                                                           // 왼쪽 방향키가 떼어졌다는걸 알림
            myClient.SendInputedKey('R', false);

            MoveTimer.Stop();                                                                           // 캐릭터 이동 타이머를 멈춤

        }
    }
}