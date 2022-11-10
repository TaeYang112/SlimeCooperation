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
    public class ThornBush : GameObject
    {
        private bool _disposed = false;

        private TextureBrush tb;
        public ThornBush(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.THORN_BUSH;
        }

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
            tb.Dispose();

            _disposed = true;

            base.Dispose(disposing);
        }

        override public void SetSkin(int num)
        {
            isVisible = true;
            switch(num)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = new Bitmap(MultiGame.Properties.Resources.ThornBush);
                    break;
            }

            tb = new TextureBrush(_image);
            tb.Transform = new System.Drawing.Drawing2D.Matrix(
          1.0f,
          0.0f,
          0.0f,
          1.0f,
          0.0f,
          Location.Y);
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {

            Graphics g = pe.Graphics;
            // 잔디
            g.FillRectangle(tb, new Rectangle(Location, size));
        }

        public override void OnHit()
        {
            base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(ObjectTypes.THORN_BUSH);
            GameManager.GetInstance().SendMessage(generator.Generate());

        }
    }
}
