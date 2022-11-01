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
            form.UpdateLobby();

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
            roomTitle_lbl.Text = $"{RoomCode}번방 {title}";
        }
    }
}
