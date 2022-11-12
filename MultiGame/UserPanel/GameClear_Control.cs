using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.UserPanel
{
    public partial class GameClear_Control : UserControl
    {
        private string oriString = "GAME CLEAR!";
        private int idx = 0;
        private bool isFold = true;
        private System.Threading.Timer timer2 = null;

        private Form1 form;

        public GameClear_Control(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            label1.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
            label2.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
            label3.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
            label4.Font = new Font(ResourceLibrary.Families[0], 25, FontStyle.Regular);
            btn_record.Font = new Font(ResourceLibrary.Families[0], 13, FontStyle.Regular);
            score_GridView.DefaultCellStyle.Font = new Font(ResourceLibrary.Families[1], 16, FontStyle.Regular);

            TimerCallback tc = new TimerCallback(timer2_Tick);
            timer2 = new System.Threading.Timer(tc,null,Timeout.Infinite, Timeout.Infinite);
        }

        ~GameClear_Control()
        {
            timer2.Dispose();
        }

        private void GameClear_Form_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            timer1.Enabled = true;
            Size = new Size(Size.Width, 299);
        }

        private void score_GridView_SelectionChanged(object sender, EventArgs e)
        {
            score_GridView.ClearSelection();
        }

        public void UpdateResult(int time, int rank)
        {

            label2.Text = $"축하드립니다! {TimeToString(time)}만에 클리어 하셨습니다!";
            if(rank == -1)
                label3.Text = $"아쉽게도 순위권 안에는 들어가지 못했습니다..";
            else
                label3.Text = $"무려 {rank}등의 자리를 차지하셨습니다!";
        }

        public void UpdateScoreBoard(List<string> titles, List<int> times)
        {
            for(int i=0; i<10; i++)
            {
                if (i < titles.Count)
                {
                    score_GridView.Rows.Add($"{i + 1}", titles[i], TimeToString(times[i]));
                }
                else
                {
                    score_GridView.Rows.Add($"{i + 1}", "-", "-");
                }
            }
        }

        private string TimeToString(int time)
        {
            int sec = time / 1000;
            int min = sec / 60;
            sec %= 60;

            if(min > 0)
                return $"{min}분 {sec}초";
            else
                return $"{sec}초";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += oriString[idx++];
            if (idx >= oriString.Length) timer1.Enabled = false;
        }

        private void timer2_Tick(object sender)
        {
            Size temp = Size;
            temp.Height += 10;

            Invoke(new MethodInvoker(delegate()
            {
                Size = temp;
            }));
            

            if (Size.Height >= 734)
            {
                timer2.Change(Timeout.Infinite, Timeout.Infinite);
                isFold = false;
            }
        }

        private void btn_record_Click(object sender, EventArgs e)
        {
            timer2.Change(0, 16);
            btn_record.Visible = false;
        }

        private void GameClear_Form_Paint(object sender, PaintEventArgs e)
        {
            if (isFold == true)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 0, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 3, ButtonBorderStyle.Solid,
                                     Color.Black, 3, ButtonBorderStyle.Solid);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            MainMenu_Screen mainMenu_Screen = new MainMenu_Screen(form);
            mainMenu_Screen.Name = "mainMenu_Screen";

            // 화면 전환
            form.ChangeScreen(mainMenu_Screen);

            GameManager.GetInstance().clientManager.ClientDic.Clear();
            GameManager.GetInstance().objectManager.ClearObjects();
        }
    }
}
