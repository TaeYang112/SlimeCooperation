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
    public partial class MainMenu_Screen : UserControl
    {
        private Form1 form;
        public MainMenu_Screen(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            makeRoom_btn.Parent = this;
        }

        private void makeRoom_btn_Click(object sender, EventArgs e)
        {
            // 방제목 입력하는 폼을 띄움
            MakeRoom_Form makeRoom_Form = new MakeRoom_Form();
            DialogResult result = makeRoom_Form.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                // 서버로 제목과 함께 방만들기 요청을 보냄
                GameManager.GetInstance().RequestCreateRoom(makeRoom_Form.roomTitle_TB.Text);
            }
        }

        private void findRoom_btn_Click(object sender, EventArgs e)
        {
            // 앞으로 서버로 부터 방목록 정보를 받도록 설정
            form.ChangeScreen(new FindRoom_Screen(form));

            GameManager.GetInstance().RequestLobbyInfo(true);
        }

        private void exitGame_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("종료", "게임을 종료합니다.", MessageBoxButtons.OK);
            Application.Exit();
        }

    }
}
