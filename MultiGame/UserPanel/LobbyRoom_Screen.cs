using MultiGameModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.UserPanel
{
    public partial class LobbyRoom_Screen : UserControl
    {
        private Form1 form;
        public LobbyRoom_Screen(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            roomTitle_lbl.Font = new Font(ResourceLibrary.Families[1], 30, FontStyle.Regular);
            roomNum_lbl.Font = new Font(ResourceLibrary.Families[1], 35, FontStyle.Regular);
            ready_btn.Font = new Font(ResourceLibrary.Families[0], 15, FontStyle.Regular);
            lobbyToFind_btn.Font = new Font(ResourceLibrary.Families[0], 15, FontStyle.Regular);

            lb_CReadyCheck.Font = new Font(ResourceLibrary.Families[0], 12, FontStyle.Regular);
            lb_LReadyCheck.Font = new Font(ResourceLibrary.Families[0], 12, FontStyle.Regular);
            lb_RReadyCheck.Font = new Font(ResourceLibrary.Families[0], 12, FontStyle.Regular);

        }

        private void ready_btn_Click(object sender, EventArgs e)
        {
            ClientCharacter userCharacter = GameManager.GetInstance().userClient.Character;

            // 서버로 보낼 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.C_READY);

            // 유저가 준비상태에서 버튼 누름
            if (userCharacter.IsReady == true)
            {
                // 준비 취소 시킴
                generator.AddBool(false);
                userCharacter.IsReady = false;
            }
            else
            {
                // 준비 상태로 만듬
                generator.AddBool(true);
                userCharacter.IsReady = true;
            }
            // 로비 UI 업데이트
            UpdateLobby();

            // 서버로 전송
            GameManager.GetInstance().SendMessage(generator.Generate());
        }

        private void lobbyToFind_btn_Click(object sender, EventArgs e)
        {
            // 서버로 보낼 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.C_EXIT_ROOM);

            // 서버로 방을 나갔다고 알림
            GameManager.GetInstance().myClient.SendMessage(generator.Generate());

            // 다른 클라이언트들 목록에서 제거
            GameManager.GetInstance().clientManager.ClientDic.Clear();

            // 레디 해제
            GameManager.GetInstance().userClient.Character.IsReady = false;

            // 메인화면으로 돌아감
            form.ChangeScreen(new MainMenu_Screen(form));
        }

        public void SetRoomTitle(string RoomCode, string title)
        {
            roomNum_lbl.Text = $"{RoomCode}번방";
            roomTitle_lbl.Text = $"{title}";

        }

        private void LobbyRoom_Screen_Load(object sender, EventArgs e)
        {

        }

        // 로비 화면 업데이트 ( 누가 접속했고,  레디를 했는지 등.. )
        public void UpdateLobby()
        {

            // 사용자 캐릭터
            ClientCharacter userCharacter = GameManager.GetInstance().userClient.Character;

            ClientCharacter[] clientChar = new ClientCharacter[2] { null, null };

            int count = 0;


            // 로비에 있는 다른 두 플레이어를 찾음
            foreach (var item in GameManager.GetInstance().clientManager.ClientDic)
            {
                clientChar[count] = item.Value;
                count++;

                if (count > 2) break;
            }


            // 로비화면 캐릭터 이미지 업데이트
            centerPicBox.Image = userCharacter.image.Clone() as Image;

            // 레디했으면 레디버튼 삽입
            if (userCharacter.IsReady == true)
            {
                lb_CReadyCheck.Text = "READY!";
            }
            else
            {
                lb_CReadyCheck.Text = "";
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if (clientChar[0] == null)
            {
                leftPicBox.Image = null;
                lb_LReadyCheck.Text = "";
            }
            else
            {
                leftPicBox.Image = clientChar[0].image.Clone() as Image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[0].IsReady == true)
                {
                    lb_LReadyCheck.Text = "READY!";
                }
                else
                {
                    lb_LReadyCheck.Text = "";
                }

            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if (clientChar[1] == null)
            {
                rightPicBox.Image = null;
                lb_RReadyCheck.Text = "";
            }
            else
            {
                rightPicBox.Image = clientChar[1].image.Clone() as Image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[1].IsReady == true)
                {
                    lb_RReadyCheck.Text = "READY!";
                }
                else
                {
                    lb_RReadyCheck.Text = "";
                }
            }

            // 레디 버튼 텍스트 업데이트
            if (userCharacter.IsReady == true)
            {
                ready_btn.Text = "CANCEL";
            }
            else
            {
                ready_btn.Text = "READY?";
            }

        }

        private void LobbyRoom_Screen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pictureBox3.ClientRectangle,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pictureBox2.ClientRectangle,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid);
        }
    }
}
