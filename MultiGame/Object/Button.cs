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

            _image = MultiGame.Properties.Resources.Button.Clone() as Image;
            PressedImage = MultiGame.Properties.Resources.ButtonPressed.Clone() as Image;
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
                    _image = MultiGame.Properties.Resources.Button.Clone() as Image;
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
