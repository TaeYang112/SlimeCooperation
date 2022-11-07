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
    public class TimerBox : GameObject
    {
        private Font font;
        private StringFormat format;

        private System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

        public int StartTime { get; set; }
        public int ServerTime { get; set; }

        public TimerBox(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = false;
            Blockable = false;
            _type = ObjectTypes.TIMER_BOX;

            st = new System.Diagnostics.Stopwatch();

            font = new Font(ResourceLibrary.Families[0], 18, FontStyle.Regular);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
        }

        ~TimerBox()
        {
            font.Dispose();
            format.Dispose();
            _image.Dispose();
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
                    _image = MultiGame.Properties.Resources.TimeBox;
                    break;
            }
        
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            var e = pe.Graphics;

            string time;

            if (st.IsRunning)
            {
                time = TimeToString(StartTime - st.ElapsedMilliseconds);
            }
            else
            {
                time = TimeToString(ServerTime);
            }

            e.DrawImage(_image, new Rectangle(Location, size));
            e.DrawString(time, font, Brushes.Black, new RectangleF(new Point(Location.X + 4, Location.Y), size), format);
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
            st.Stop();
        }

    }
}
