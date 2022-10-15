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
using MultiGame.Client;
using MultiGame.UserPanel;
using MultiGameModule;

namespace MultiGame
{
    public partial class Form1 : Form
    {

        #region 멤버변수

        GameManager gameManager;
        #endregion

        public Form1()
        {
            InitializeComponent();

            
            // 최적화
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            gameManager = GameManager.GetInstance();

            this.Controls.Add(new MainMenu_Screen(this));
            
        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            gameManager.Start(this);
        }


        // 로비 화면 업데이트 ( 누가 접속했고,  레디를 했는지 등.. )
        public void UpdateLobby()
        {
            // 형변환
            LobbyRoom_Screen lobbyRoom_Screen = Controls[0] as LobbyRoom_Screen;

            // 현재 로비화면이 아닐경우 리턴
            if ( lobbyRoom_Screen == null) return;

            // 사용자 캐릭터
            ClientCharacter userCharacter = gameManager.userClient.Character;

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
            if (userCharacter.IsReady == true)
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
                if (clientChar[0].IsReady == true)
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
                if (clientChar[1].IsReady == true)
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
                }
                else
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = null;
                }
            }

            // 레디 버튼 텍스트 업데이트
            if(userCharacter.IsReady == true)
            {
                lobbyRoom_Screen.ready_btn.Text = "준비 취소";
            }
            else
            {
                lobbyRoom_Screen.ready_btn.Text = "준비";
            }
            
        }


        #region UI관련


        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 사용자 캐릭터
            UserClient userClient = gameManager.userClient;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            MessageGenerator generator = new MessageGenerator(Protocols.C_KEY_INPUT);

            switch (keyData)
            {
                case Keys.Left:
                    if(userClient.LeftDown == false)
                    {
                        userClient.LeftDown = true;
                        generator.AddByte(Keyboards.LEFT_ARROW).AddBool(true);
                        gameManager.SendMessage(generator.Generate());
                    }
                    break;
                case Keys.Right:
                    if(userClient.RightDown == false)
                    {
                        userClient.RightDown = true;
                        generator.AddByte(Keyboards.RIGHT_ARROW).AddBool(true);
                        gameManager.SendMessage(generator.Generate());
                    }
                    break;
                case Keys.Space:
                    userClient.Jump();
                    userClient.JumpDown = true;
                    break;
                case Keys.Up:
                    gameManager.userClient.Interaction();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);

            }
            

            return true;
        }

        // 키가 떼어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // 사용자 캐릭터
            UserClient userClient = gameManager.userClient;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
            {
                return;
            }

            MessageGenerator generator = new MessageGenerator(Protocols.C_KEY_INPUT);

            switch (e.KeyData)
            {
                case Keys.Left:
                    if(userClient.LeftDown == true)
                    {
                        userClient.LeftDown = false;
                        generator.AddByte(Keyboards.LEFT_ARROW).AddBool(false);
                        gameManager.SendMessage(generator.Generate());
                    }
                    break;
                case Keys.Right:
                    if(userClient.RightDown == true)
                    {
                        userClient.RightDown = false;
                        generator.AddByte(Keyboards.RIGHT_ARROW).AddBool(false);
                        gameManager.SendMessage(generator.Generate());
                    }
                    break;
                case Keys.Space:
                    userClient.JumpDown = false;
                    break;
                default:
                    break;
            }
        }

        // 폼의 포커스가 풀리면 ( 알트 탭, 다른 윈도우 선택시 ) 이벤트 발생
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            return;
            // 사용자 캐릭터
            UserClient userClient = gameManager.userClient;

            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (gameManager.IsGameStart == false)
            {
                return;
            }

            // 입력중인 키 모두 해제
            userClient.LeftDown = false;                                                            
            userClient.RightDown = false;                                                           
            userClient.JumpDown = false;
        }
        
        // 보여질 화면 변경
        public void ChangeScreen(UserControl newScreen)
        {
            this.Controls.Clear();
            this.Controls.Add(newScreen);
        }


        #endregion
    }

    
}