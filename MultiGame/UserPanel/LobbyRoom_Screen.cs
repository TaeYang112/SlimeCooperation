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
        }

        private void ready_btn_Click(object sender, EventArgs e)
        {
            ClientCharacter userCharacter = GameManager.GetInstance().userClient.Character;
            // 유저가 준비상태에서 버튼 누름
            if (userCharacter.IsReady == true)
            {
                // 준비 취소 시킴
                GameManager.GetInstance().RequestReady(false);
                userCharacter.IsReady = false;
            }
            else
            {
                // 준비 상태로 만듬
                GameManager.GetInstance().RequestReady(true);
                userCharacter.IsReady = true;
            }
            form.UpdateLobby();
        }

        private void lobbyToFind_btn_Click(object sender, EventArgs e)
        {
            // 서버로 방을 나갔다고 알림
            GameManager.GetInstance().myClient.SendMessage($"ExitLobby#d@");

            // 다른 클라이언트들 목록에서 제거
            GameManager.GetInstance().clientManager.ClientDic.Clear();

            // 레디 해제
            GameManager.GetInstance().userClient.Character.IsReady = false;

            // 메인화면으로 돌아감
            form.ChangeScreen(new MainMenu_Screen(form));
        }

        public void SetRoomTitle(string RoomCode, string title)
        {
            roomTitle_lbl.Text = $"{RoomCode}번방 {title}";
            Console.WriteLine(roomTitle_lbl.Text);
        }
    }
}
