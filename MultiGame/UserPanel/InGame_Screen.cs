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

        private Image BackGroundImg;

        // 디버그
        private System.Threading.Timer FPSTimer;
        private int FPS = 0;

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

            BackGroundImg = MultiGame.Properties.Resources.BackGround1.Clone() as Image;


            TimerCallback tc2 = new TimerCallback(DebugTimer);                                    // 실행시킬 메소드
            FPSTimer = new System.Threading.Timer(tc2, null, 0, 1000);
            FPS = 0;
        }

    public void DebugTimer(object c)
    {
        Console.WriteLine(FPS + "FPS.");
        FPS = 0;
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
            var g = e.Graphics;

            // 배경
            g.DrawImage(BackGroundImg, new Rectangle(new Point(0,0), new Size(800,500)));

            // 오브젝트
            foreach (var item in GInst.objectManager.ObjectDic)
            {
                item.Value.OnPaint(sender, e);
            }

            // 캐릭터
            foreach (var item in GInst.clientManager.ClientDic)
            {
                item.Value.OnPaint(sender, e);
            }

            // 유저 캐릭터
            GInst.userClient.Character.OnPaint(sender,e);

            
            FPS++;
            
        }
    }
}
