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
    public class Button : GameObject
    {
        private bool _disposed = false;

        public bool Pressed { get; set; }

        private Image PressedImage;
        public Button(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = false;
            _type = ObjectTypes.BUTTON;
            Pressed = false;

            
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
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.ButtonPressed, size);
                    break;
                case 1:
                    _image = new Bitmap(MultiGame.Properties.Resources.Red_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Red_Button2, size);
                    break;
                case 2:
                    _image = new Bitmap(MultiGame.Properties.Resources.Oragne_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Oragne_Button2, size);
                    break;
                case 3:
                    _image = new Bitmap(MultiGame.Properties.Resources.Yellow_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Yellow_Button2, size);
                    break;
                case 4:
                    _image = new Bitmap(MultiGame.Properties.Resources.Green_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Green_Button2, size);
                    break;
                case 5:
                    _image = new Bitmap(MultiGame.Properties.Resources.Blue_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Blue_Button2, size);
                    break;
                case 6:
                    _image = new Bitmap(MultiGame.Properties.Resources.Purple_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Purple_Button2, size);
                    break;
                case 7:
                    _image = new Bitmap(MultiGame.Properties.Resources.Pink_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Pink_Button2, size);
                    break;
                case 8:
                    _image = new Bitmap(MultiGame.Properties.Resources.Gray_Button, size);
                    PressedImage = new Bitmap(MultiGame.Properties.Resources.Gray_Button2, size);
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
                generator.AddInt(key).AddByte(ObjectTypes.BUTTON);
                byte[] message = generator.Generate();

                GameManager.GetInstance().SendMessage(message);
            }
            
        }

    }
}
