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
    public class Stone : GameObject
    {
        // dispose 중복 호출 방지
        bool _disposed = false;

        public int weight {get;set;}
        private Font font;
        private StringFormat format;

        public Stone(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.STONE;

            font = new Font(ResourceLibrary.Families[0], 20, FontStyle.Regular);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            weight = 0;
        }

        // dispose 패턴
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // 관리 메모리 해제
            }

            // 비관리 메모리 해제

            _disposed = true;
            font.Dispose();
            format.Dispose();

            base.Dispose(disposing);
        }

        public override void SetSkin(int skinNum)
        {
            isVisible = true;
            Size _size = new Size(size.Width + 1, size.Height + 1);
            switch (skinNum)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = new Bitmap(MultiGame.Properties.Resources.Stone, _size);
                    break;
            }
        
        }
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            
            Graphics g= pe.Graphics;

            g.DrawImage(_image, Location);
            g.DrawString(weight.ToString(), font, Brushes.Black, new RectangleF(new Point(Location.X + 4,Location.Y),size), format);
        }


        public override void OnHit()
        {
            base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(ObjectTypes.STONE);
            byte[] message = generator.Generate();

            GameManager.GetInstance().SendMessage(message);
        }

    }
}
