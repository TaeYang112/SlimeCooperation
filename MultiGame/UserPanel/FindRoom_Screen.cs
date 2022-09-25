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
    public partial class FindRoom_Screen : UserControl
    {
        private Form1 form;
        public FindRoom_Screen(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void enterRoom_btn_Click(object sender, EventArgs e)
        {
            if (roomList_GridView.SelectedRows.Count == 0)
            {
                return;
            }

            // 선택한 방( 행 )의 키를 받음
            int roomKey = int.Parse(roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());

            // 서버로 입장 요청
            GameManager.GetInstance().RequestEnterRoom(roomKey);
        }

        private void findToMain_btn_Click(object sender, EventArgs e)
        {
            // 서버로부터 받는 방목록 정보 수신을 종료함
            GameManager.GetInstance().RequestLobbyInfo(false);

            // 메인화면으로 돌아감
            form.ChangeScreen(new MainMenu_Screen(form));
            
        }

        private void roomList_GridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int roomKey = int.Parse(roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());
            GameManager.GetInstance().RequestEnterRoom(roomKey);
        }
    }
}
