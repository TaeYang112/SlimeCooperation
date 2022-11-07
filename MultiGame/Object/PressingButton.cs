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
    public class PressingButton : GameObject
    {
        // dispose 중복 호출 방지
        bool _disposed = false;

        public bool Pressed { get; set; }

        private Image PressedImage;
        public PressingButton(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = false;
            _type = ObjectTypes.PRESSING_BUTTON;
            Pressed = false;

            PressedImage = MultiGame.Properties.Resources.ButtonPressed;
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
            PressedImage.Dispose();

            _disposed = true;

            base.Dispose(disposing);
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
                    _image = new Bitmap(MultiGame.Properties.Resources.Button, size);
                    break;
            }
        
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            var e = pe.Graphics;

            if(Pressed == false)
                e.DrawImage(image, new Rectangle(Location, size));
            else
                e.DrawImage(PressedImage, new Rectangle(new Point(Location.X, Location.Y + size.Height/2), new Size(size.Width, size.Height/2)));

        }


        public override void OnHit()
        {
            if(Pressed == false)
            {
                base.OnHit();

                MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
                generator.AddInt(key).AddByte(ObjectTypes.PRESSING_BUTTON);
                byte[] message = generator.Generate();

                GameManager.GetInstance().SendMessage(message);
            }
            
        }

    }
}
