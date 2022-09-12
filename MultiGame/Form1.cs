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

        // 메인 화면
        private UserPanel.MainMenu_Screen _mainMenu_Screen;
        public UserPanel.MainMenu_Screen mainMenu_Screen { get { return _mainMenu_Screen; } }

        // 로비 화면
        private UserPanel.LobbyRoom_Screen _lobbyRoom_Screen;
        public UserPanel.LobbyRoom_Screen lobbyRoom_Screen { get { return _lobbyRoom_Screen; } }

        // 방찾기 화면
        private UserPanel.FindRoom_Screen _findRoom_Screen;
        public UserPanel.FindRoom_Screen findRoom_Screen { get { return _findRoom_Screen; } }

        // 방 만들기 창
        private UserPanel.MakeRoom_Form _makeRoom_Form;
        public UserPanel.MakeRoom_Form makeRoom_Form { get { return _makeRoom_Form; } }

        // 게임 화면
        private UserPanel.InGame_Screen _inGame_Screen;
        public UserPanel.InGame_Screen inGame_Screen { get { return _inGame_Screen; } }


        GameManager gameManager;
        #endregion

        public Form1()
        {
            InitializeComponent();
            InitializeScreen();

            // 최적화
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            gameManager = GameManager.GetInstance();

            this.Controls.Add(mainMenu_Screen);
        }

        ~Form1()
        {
            
        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            gameManager.Start(this);
        }


        // 로비 화면 업데이트 ( 누가 접속했고,  레디를 했는지 등.. )
        public void UpdateLobby()
        {
            // 현재 로비화면이 아닐경우 리턴
            if (this.Controls.Contains(lobbyRoom_Screen) == false) return;

            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userCharacter;

            ClientCharacter[] clientChar = new ClientCharacter[2] { null, null };

            int count = 0;


            // 로비에 있는 다른 두 플레이어를 찾음
            foreach (var item in gameManager.clientManager.ClientDic)
            {
                clientChar[count] = item.Value;
                count++;

                if (count > 2) break;
            }


            // 로비화면 캐릭터 이미지 업데이트
            lobbyRoom_Screen.centerPicBox.Image = userCharacter.image.Clone() as Image;

            // 레디했으면 레디버튼 삽입
            if (userCharacter.isReady == true)
            {
                lobbyRoom_Screen.centerCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
            }
            else
            {
                lobbyRoom_Screen.centerCheckPicBox.Image = null;
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if (clientChar[0] == null)
            {
                lobbyRoom_Screen.leftPicBox.Image = null;
                lobbyRoom_Screen.leftCheckPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.leftPicBox.Image = clientChar[0].image.Clone() as Image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[0].isReady == true)
                {
                    lobbyRoom_Screen.leftCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
                }
                else
                {
                    lobbyRoom_Screen.leftCheckPicBox.Image = null;
                }
                   
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if ( clientChar[1] == null)
            {
                lobbyRoom_Screen.rightPicBox.Image = null;
                lobbyRoom_Screen.rightCheckPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.rightPicBox.Image = clientChar[1].image.Clone() as Image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[1].isReady == true)
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
                }
                else
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = null;
                }
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
            _mainMenu_Screen = new UserPanel.MainMenu_Screen();
            mainMenu_Screen.findRoom_btn.Click += button_Click;
            mainMenu_Screen.makeRoom_btn.Click += button_Click;
            mainMenu_Screen.exitGame_btn.Click += button_Click;

            // 로비
            _lobbyRoom_Screen = new UserPanel.LobbyRoom_Screen();
            lobbyRoom_Screen.ready_btn.Click += button_Click;
            lobbyRoom_Screen.lobbyToFind_btn.Click += button_Click;

            // 방찾기
            _findRoom_Screen = new UserPanel.FindRoom_Screen();
            findRoom_Screen.findToMain_btn.Click += button_Click;
            findRoom_Screen.enterRoom_btn.Click += button_Click;
            findRoom_Screen.roomList_GridView.CellDoubleClick += DataGridDoubleClick;

            // 인게임
            _inGame_Screen = new UserPanel.InGame_Screen();
          

            // 방만들기
            _makeRoom_Form = new UserPanel.MakeRoom_Form();
            makeRoom_Form.make_btn.Click += button_Click;
            makeRoom_Form.cancelMakeRoom_btn.Click += button_Click;

        }

        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userCharacter;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
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
                    gameManager.SendInputedKey('L', true);
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
                    gameManager.SendInputedKey('R', true);                                        
                }
                    
                return true;
            }

            // 점프
            if (keyData == Keys.Space)
            {
                userCharacter.Jump();

                gameManager.SendInputedKey('J', true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userCharacter;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    // bLeftDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bLeftDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    gameManager.SendInputedKey('L', false);
                    break;

                case Keys.Right:
                    // bRightDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bRightDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    gameManager.SendInputedKey('R', false);
                    break;
            }
        }




        // 폼의 포커스가 풀리면 ( 알트 탭, 다른 윈도우 선택시 ) 이벤트 발생
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userCharacter;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
            {
                return;
            }

            // 왼쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bLeftDown = false;                                                            
            gameManager.SendInputedKey('L', false);

            // 오른쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bRightDown = false;                                                           
            gameManager.SendInputedKey('R', false);

        }

        // 버튼을 클릭했을 때 호출됨
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userCharacter;

            switch (btn.Name)
            {
                // 메인메뉴 화면 ( MainMenu_Screen )
                case "makeRoom_btn":    // 방만들기
                    {
                        makeRoom_Form.roomTitle_TB.Text = "";

                        // 방제목 입력하는 폼을 띄움
                        makeRoom_Form.ShowDialog();
                    }
                    break;
                case "findRoom_btn":    // 방찾기
                    {
                        // 방목록 초기화
                        findRoom_Screen.roomList_GridView.Rows.Clear();

                        // 앞으로 서버로 부터 방목록 정보를 받도록 설정
                        gameManager.RequestLobbyInfo(true);

                        // 방찾기 창을 띄움
                        this.Controls.Clear();
                        this.Controls.Add(findRoom_Screen);
                    }
                    break;
                case "exitGame_btn":    // 게임종료
                    {
                        MessageBox.Show("종료", "게임을 종료합니다.", MessageBoxButtons.OK);
                        Application.Exit();
                    }
                    break;

                // 방 만들기 화면 ( MakeRoom_Screen )
                case "cancelMakeRoom_btn":// 취소
                    {
                        // 방만들기 폼을 종료하고 메인화면으로 이동
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                        makeRoom_Form.Close();
                    }
                    break;
                case "make_btn":        // 만들기
                    {
                        // 서버로 제목과 함께 방만들기 요청을 보냄
                        gameManager.RequestCreateRoom(makeRoom_Form.roomTitle_TB.Text);

                        // 방만들기 폼 종료
                        makeRoom_Form.Close();
                    }
                    break;

                // 방 찾기 화면 ( FindRoom_Screen )
                case "enterRoom_btn": // 입장
                    {
                        // 선택한 방이 없으면 무시
                        if (findRoom_Screen.roomList_GridView.SelectedRows.Count == 0)
                        {
                            return;
                        }

                        // 선택한 방( 행 )의 키를 받음
                        int roomKey = int.Parse(findRoom_Screen.roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());

                        // 서버로 입장 요청
                        gameManager.RequestEnterRoom(roomKey);
                    }
                    break;
                case "findToMain_btn":  // 뒤로가기
                    {
                        // 서버로부터 받는 방목록 정보 수신을 종료함
                        gameManager.RequestLobbyInfo(false);

                        // 메인화면으로 이동
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                    }
                    break;
                // 방 대기 화면 ( Room_Screen )  
                case "ready_btn":       // 준비 / 준비 취소
                    {
                        // 유저가 준비상태에서 버튼 누름
                        if ( userCharacter.isReady == true )
                        {
                            // 준비 취소 시킴
                            gameManager.RequestReady(false);
                            userCharacter.isReady = false;
                        }
                        else
                        {
                            // 준비 상태로 만듬
                            gameManager.RequestReady(true);
                            userCharacter.isReady = true;
                        }
                        UpdateLobby();
                    }
                    break;
                case "lobbyToFind_btn":     // 나가기
                    {
                        // 서버로 방을 나갔다고 알림
                        gameManager.myClient.SendMessage($"ExitLobby#d@");

                        // 방찾기화면으로 이동, 방목록 정보 수신
                        findRoom_Screen.roomList_GridView.Rows.Clear();
                        gameManager.RequestLobbyInfo(true);
                        gameManager.clientManager.ClientDic.Clear();

                        userCharacter.isReady = false;
                        this.Controls.Clear();
                        this.Controls.Add(findRoom_Screen);
                    }
                    break;

                // 모든 화면 공용
                case "goMain_btn":     // 메인화면으로, 뒤로가기 등..
                    {
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                    }
                    break;
                default:
                    break;
            }

            
        }
        private void DataGridDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int roomKey = int.Parse(findRoom_Screen.roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());
            gameManager.RequestEnterRoom(roomKey);
        }

        #endregion


    }

    
}