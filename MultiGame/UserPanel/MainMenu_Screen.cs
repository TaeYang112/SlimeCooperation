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
    public partial class MainMenu_Screen : UserControl
    {
        private Form1 form;
        public MainMenu_Screen(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            // 최적화
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            makeRoom_btn.Parent = this;

            makeRoom_btn.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
            findRoom_btn.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
            records_btn.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
            exitGame_btn.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);

            this.ActiveControl = null;
        }

        private void makeRoom_btn_Click(object sender, EventArgs e)
        {
            ActiveControl = null;

            // 방제목 입력하는 폼을 띄움
            MakeRoom_Form makeRoom_Form = new MakeRoom_Form();
            DialogResult result = makeRoom_Form.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                // 서버로 제목과 함께 방만들기 요청을 보냄
                MessageGenerator generator = new MessageGenerator(Protocols.REQ_CREATE_ROOM);
                generator.AddString(makeRoom_Form.roomTitle_TB.Text);

                GameManager.GetInstance().SendMessage(generator.Generate());
            }
            
            
        }

        private void findRoom_btn_Click(object sender, EventArgs e)
        {
            // 앞으로 서버로 부터 방목록 정보를 받도록 설정
            FindRoom_Screen findRoom_Screen = new FindRoom_Screen(form);
            findRoom_Screen.Name = "findRoom_Screen";

            form.ChangeScreen(findRoom_Screen);

            // 메시지 생성 후 서버로 전송
            MessageGenerator generator = new MessageGenerator(Protocols.REQ_ROOM_LIST);
            generator.AddBool(true);

            GameManager.GetInstance().SendMessage(generator.Generate());
        }

        private void exitGame_btn_Click(object sender, EventArgs e)
        {

            DialogResult result = new ExitProgram_Form().ShowDialog();

            if(result == DialogResult.Yes)
                Application.Exit();
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

             button.ForeColor = Color.White;
            button.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
          
            button.ForeColor = Color.Black;
            button.Font = new Font(ResourceLibrary.Families[0], 35, FontStyle.Regular);
        }

        private void MainMenu_Screen_Load(object sender, EventArgs e)
        {
            
        }

        private void records_btn_Click(object sender, EventArgs e)
        {
            // 메시지 생성 후 서버로 전송
            MessageGenerator generator = new MessageGenerator(Protocols.REQ_RECORD_LIST);

            GameManager.GetInstance().SendMessage(generator.Generate());
        }
    }
}
