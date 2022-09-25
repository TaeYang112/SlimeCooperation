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
    public partial class InGame_Screen : UserControl
    {

        // 화면 업데이트( 60프레임 ) 타이머
        private System.Threading.Timer UpdateTimer;

        private Form1 form;
        public InGame_Screen(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            // 60프레임 화면 업데이트
            TimerCallback tc = new TimerCallback(Update);
            UpdateTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);

            // 최적화
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void StartUpdateScreen(bool bStart)
        {
            if(bStart == true)
            {
                UpdateTimer.Change(0,13);
            }
            else
            {
                UpdateTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        // 화면 다시그리기
        public void Update(object temp)
        {
            Invalidate();
        }

        ~InGame_Screen()
        {
            UpdateTimer.Dispose();
        }

        private void InGame_Screen_Load(object sender, EventArgs e)
        {

        }

        private void InGame_Screen_Paint(object sender, PaintEventArgs e)
        {
            GameManager GInst = GameManager.GetInstance();

            foreach(var item in GInst.clientManager.ClientDic)
            {
                item.Value.OnPaint(sender, e);
            }

            foreach (var item in GInst.objectManager.ObjectDic)
            {
                item.Value.OnPaint(sender, e);
            }

            GInst.userClient.Character.OnPaint(sender,e);
        }
    }
}
