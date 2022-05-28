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
        public InGame_Screen()
        {
            InitializeComponent();

            // 60프레임 화면 업데이트
            TimerCallback tc = new TimerCallback(Update);
            UpdateTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);

            this.DoubleBuffered = true;
        }

        public void StartUpdateScreen(bool bStart)
        {
            if(bStart == true)
            {
                UpdateTimer.Change(0,10);
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

    }
}
