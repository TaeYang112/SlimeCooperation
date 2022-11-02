using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.Object
{
    public class TimerBoard : GameObject
    {
        private Font font;
        private StringFormat format;

        private System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

        public int StartTime { get; set; }

        private int _timerCount;
        public int TimerCount { get { return _timerCount; } }

        public int ServerTime { get; set; }

        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        public TimerBoard(int key, Point Location, Size size, int timerCount) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = false;
            Blockable = false;
            _type = ObjectTypes.TIMER_BOARD;

            _timerCount = timerCount;

            st = new System.Diagnostics.Stopwatch();

            font = new Font(ResourceLibrary.Families[0], 15, FontStyle.Regular);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
        }

        public override void SetSkin(int skinNum)
        {
            isVisible = true;
            switch (skinNum)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = MultiGame.Properties.Resources.TimeBoard;
                    break;
            }
        
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            var e = pe.Graphics;

            long temp = StartTime - st.ElapsedMilliseconds * _timerCount - ServerTime;

            string str = TimeToString(MinTime) + " ▷ " + TimeToString(temp) + " ▷ " + TimeToString(MaxTime);

            e.DrawImage(_image, new Rectangle(Location, size));
            e.DrawString(str, font, Brushes.Black, new RectangleF(new Point(Location.X + 4, Location.Y), size), format);
        }

        private string TimeToString(long time)
        {
            string result = "";

            if (time < 0)
            {
                result = "0.00";
            }
            else
            {
                result = String.Format("{0:f}", time / 1000.0f);
            }

            return result;
        }

        public void TimerStart()
        {
            st.Start();
        }

        public void TImerStop()
        {
            _timerCount--;
            
            // 더이상 남은 타이머가 없다면 종료시킴
            if(_timerCount == 0)
                st.Stop();
        }

    }
}
