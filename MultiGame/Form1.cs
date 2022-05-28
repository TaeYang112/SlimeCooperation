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

        #region 멤버변수

        // 서버와 TCP통신을 담당하는 객체
        private MyClient myClient;

        // 다른 클라이언트들을 관리하는 객체
        private ClientManager clientManager;    
        
        // 사용자의 캐릭터
        private ClientCharacter userCharacter;

        // 게임 시작 여부
        public bool IsGameStart { get; set; }                                       

        // 화면 업데이트( 60프레임 ) 타이머
        private System.Threading.Timer UpdateTimer;

        // UI
        private UserPanel.MainMenu_Screen mainMenu_Screen;
        private UserPanel.LobbyRoom_Screen lobbyRoom_Screen;
        private UserPanel.FindRoom_Screen findRoom_Screen;
        private UserPanel.MakeRoom_Form makeRoom_Form;
        private UserPanel.InGame_Screen inGame_Screen;
        #endregion



        public Form1()
        {
            InitializeComponent();
            InitializeScreen();

            // 다른 클라이언트들을 관리할 객체
            clientManager = new ClientManager();

            // 사용자 캐릭터
            userCharacter = new ClientCharacter(-1,new Point(364,293), 0);

            // 클라이언트 객체 생성
            myClient = new MyClient();

            // 서버로부터 메세지를 받으면 onTakeMessage함수 호출
            myClient.TakeMessage += new TakeMessageEventHandler(OnTakeMessage);

            

            this.Controls.Add(mainMenu_Screen);
        }

       

        ~Form1()
        {
            UpdateTimer.Dispose();
        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            // 클라이언트 시작
            myClient.Start();


            // 60프레임 화면 업데이트
            TimerCallback tc = new TimerCallback(Update);                              
            UpdateTimer = new System.Threading.Timer(tc, null, 0, 10);  
        }

        // 화면 다시그리기
        public void Update(object temp)
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
                    // 클라이언트의 캐릭터 위치 수신
                    case "Location":
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
                    // 클라이언트 정보 업데이트
                    case "UpdateClient":
                        {
                            /*
                            // 플레이어 번호
                            int key = int.Parse(SplitMessage[1]);

                            // 플레이어 좌표
                            int x = int.Parse(SplitMessage[2]);
                            int y = int.Parse(SplitMessage[3]);


                            ClientCharacter clientCharacter;

                            // 유저 클라이언트의 정보일경우 ( 자신의 key는 -1 )
                            if (key == -1)
                            {
                                clientCharacter = userCharacter;
                            }
                            else
                            {
                                // 자신이 아니면 새로운 클라이언트의 캐릭터 생성
                                clientCharacter = clientManager.AddOrGetClient(key, new Point(41, 49), 1);
                            }

                            clientCharacter.isVisible = true;
                            clientCharacter.Location = new Point(x, y);
                            */
                        }
                        break;
                    // 클라이언트가 방을 나감
                    case "LeaveRoom":
                        {
                            // 플레이어 번호
                            int key = int.Parse(SplitMessage[1]);

                            ClientCharacter clientChar;

                            // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                            bool result = clientManager.ClientDic.TryGetValue(key, out clientChar);

                            // 만약 존재하지 않으면 리턴
                            if(result == false)
                            {
                                return;
                            }

                            //클라이언트 배열에서 제거
                            clientManager.RemoveClient(clientChar);
                        }
                        break;
                    // 다른 클라이언트의 키보드 입력 ( Input )
                    case "KeyInput":
                        {
                            // 플레이어 번호
                            int key = int.Parse(SplitMessage[1]);

                            // 입력된 키
                            char InpKey = char.Parse(SplitMessage[2]);

                            // 눌렸으면 true / 아니면 false
                            bool bKeyDown = bool.Parse(SplitMessage[3]);

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
                    // 방에 입장 ( ENTER )
                    case "EnterRoom":
                        {
                            // 방 번호
                            string roomCode = SplitMessage[1];

                            // 방 제목
                            string roomTItle = SplitMessage[2];

                            Console.WriteLine($"{roomCode}번 '{roomTItle}' 방에 접속");

                            lobbyRoom_Screen.roomTitle_lbl.Text = $"{roomCode}번방 {roomTItle}";
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                this.Controls.Clear();
                                this.Controls.Add(lobbyRoom_Screen);
                                UpdateLobby();
                            }));
                        }
                        break;
                    // 방에 다른 클라이언트 입장
                    case "EnterRoomOther":
                        {
                            // 플레이어 번호
                            int key = int.Parse(SplitMessage[1]);

                            ClientCharacter clientCharacter;

                            // 자신이 아닐경우 새로운 클라이언트를 만들던가 기존의 클라이언트를 받아옴
                            clientCharacter = clientManager.AddOrGetClient(key, new Point(0, 0), 1);

                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                UpdateLobby();
                            }));
                        }
                        break;
                    case "RoomStart":
                        {
                            IsGameStart = true;
                            
                            this.Invoke(new MethodInvoker(delegate() 
                           {
                               this.Controls.Clear();
                               this.Controls.Add(inGame_Screen);
                           }));

                            foreach (var item in clientManager.ClientDic)
                            {
                                item.Value.isVisible = true;
                                item.Value.Location = new Point(0, 0);
                            }
                        }
                        break;
                    case "Ping":
                        {
                            // 클라이언트가 접속중인지 확인하기 위해 서버가 보내는 메시지
                            // 클라이언트는 반응이 없어도 됨
                        }
                        break;
                    default:
                        Console.WriteLine("디폴트 : {0}", Messages[i]);
                    break;
                }
            }
        }

        // 유저가 입력한 키를 서버로 보냄 ( 입력키, 누르면 true / 뗐으면 false )
        private void SendInputedKey(char inputKey, bool bPressed)
        {
            string message = $"KeyInput#{inputKey}#{bPressed}#@";
            myClient.SendMessage(message);
        }

        // 서버로 방만들기 요청
        public void CreateRoom(string RoomTitle)
        {
            myClient.SendMessage($"CreateRoom#{RoomTitle}@");
        }
        
        // 방 입장 요청
        public void EnterRoom(int i)
        {
            myClient.SendMessage($"TryEnterRoom#{i}@");
        }

        // 준비
        public void ReadyRoom(bool bReady)
        {
            myClient.SendMessage($"Ready#{bReady}@");
        }

        // 로비 화면 업데이트 ( 누가 접속했고,  레디를 했는지 등.. )
        public void UpdateLobby()
        {
            // 현재 로비화면이 아닐경우 리턴
            if (this.Controls.Contains(lobbyRoom_Screen) == false) return;


            ClientCharacter []clientChar = new ClientCharacter[2] { null, null };

            int count = 0;


            // 로비에 있는 다른 두 플레이어를 찾음
            foreach(var item in clientManager.ClientDic)
            {
                clientChar[count] = item.Value;
                count++;

                if (count > 2) break;
            }

            // 로비화면 캐릭터 이미지 업데이트
            lobbyRoom_Screen.centerPicBox.Image = userCharacter.image;

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if (clientChar[0] == null)
            {
                lobbyRoom_Screen.leftPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.leftPicBox.Image = clientChar[0].image;
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if ( clientChar[1] == null)
            {
                lobbyRoom_Screen.rightPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.rightPicBox.Image = clientChar[1].image;
            }

            // 레디 버튼 텍스트 업데이트
            if(userCharacter.isReady == true)
            {
                lobbyRoom_Screen.ready_btn.Text = "준비 취소";
            }
            else
            {
                lobbyRoom_Screen.ready_btn.Text = "준비";
            }
            
        }







        #region UI관련

        private void InitializeScreen()
        {
            // 메인 메뉴
            mainMenu_Screen = new UserPanel.MainMenu_Screen();
            mainMenu_Screen.findRoom_btn.Click += button_Click;
            mainMenu_Screen.makeRoom_btn.Click += button_Click;
            mainMenu_Screen.exitGame_btn.Click += button_Click;

            // 로비
            lobbyRoom_Screen = new UserPanel.LobbyRoom_Screen();
            lobbyRoom_Screen.ready_btn.Click += button_Click;
            lobbyRoom_Screen.goMain_btn.Click += button_Click;

            // 방찾기
            findRoom_Screen = new UserPanel.FindRoom_Screen();
            findRoom_Screen.goMain_btn.Click += button_Click;

            // 인게임
            inGame_Screen = new UserPanel.InGame_Screen();

            // 방만들기
            makeRoom_Form = new UserPanel.MakeRoom_Form();
            makeRoom_Form.make_btn.Click += button_Click;
            makeRoom_Form.cancleMakeRoom_btn.Click += button_Click;
        }

        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // 왼쪽 방향키
            if (keyData == Keys.Left)
            {
                if (userCharacter.bLeftDown == false)
                {
                    // bLeftDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    userCharacter.bLeftDown = true;

                    // 서버한테 이동을 시작했다고 알림
                    SendInputedKey('L', true);

                    // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                    userCharacter.MoveStart();
                }
                return true;
            }

            // 오른쪽 방향키
            if (keyData == Keys.Right)
            {
                if (userCharacter.bRightDown == false)
                {
                    // bRightDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    userCharacter.bRightDown = true;

                    // 서버한테 이동을 시작했다고 알림
                    SendInputedKey('R', true);

                    // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                    userCharacter.MoveStart();                                            
                }
                    
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    // bLeftDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bLeftDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    SendInputedKey('L', false);
                    break;

                case Keys.Right:
                    // bRightDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bRightDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    SendInputedKey('R', false);
                    break;
            }
            // 왼쪽과 오른쪽 모두 떼어져 있다면 캐릭터 이동 타이머를 멈춤
            if (!(userCharacter.bLeftDown || userCharacter.bRightDown)) userCharacter.MoveStop();     
        }




        // 폼의 포커스가 풀리면 ( 알트 탭, 다른 윈도우 선택시 ) 이벤트 발생
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return;
            }

            // 왼쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bLeftDown = false;                                                            
            SendInputedKey('L', false);

            // 오른쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bRightDown = false;                                                           
            SendInputedKey('R', false);

            // 이동이 중단되었기 때문에 이동 타이머 멈춤
            userCharacter.MoveStop();

        }


        // 폼을 그릴때 호출되는 메소드 
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            // 캐릭터는 컨트롤( 버튼, 텍스트박스 등..)이 아니므로 수동으로 그려야됨
            foreach(var item in clientManager.ClientDic)
            {
                item.Value.OnPaint(pe);
            }
            userCharacter.OnPaint(pe);
        }

        // 버튼을 클릭했을 때 호출됨
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch(btn.Name)
            {
                // 메인메뉴 화면 ( MainMenu_Screen )
                case "makeRoom_btn":
                    makeRoom_Form.ShowDialog();
                    break;
                case "findRoom_btn":
                    this.Controls.Clear();
                    this.Controls.Add(findRoom_Screen);
                    break;
                case "exitGame_btn":
                    MessageBox.Show("종료", "게임을 종료합니다.", MessageBoxButtons.OK);
                    Application.Exit();
                    break;

                // 방 만들기 화면 ( MakeRoom_Screen )
                case "cancelMakeRoom_btn":
                    this.Controls.Clear();
                    this.Controls.Add(mainMenu_Screen);
                    makeRoom_Form.Close();
                    break;
                case "make_btn":
                    CreateRoom(makeRoom_Form.roomTitle_TB.Text);
                    makeRoom_Form.Close();
                    break;

                // 방 찾기 화면 ( FindRoom_Screen )


                // 방 대기 화면 ( Room_Screen )  
                case "ready_btn":
                    // 준비(true) 와 준비X (false) 를 전환함
                    userCharacter.isReady = !( userCharacter.isReady );
                    UpdateLobby();
                    break;

                // 모든 화면 공용
                case "goMain_btn":
                    this.Controls.Clear();
                    this.Controls.Add(mainMenu_Screen);
                    break;
                default:
                    break;
            }

            
        }
        #endregion
    }
}