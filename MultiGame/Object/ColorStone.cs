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
    public class ColorStone : GameObject
    {

        public ColorStone(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.COLOR_STONE;
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
                    _image = new Bitmap(MultiGame.Properties.Resources.Red_Stone, size);
                    break;
                case 1:
                    _image = new Bitmap(MultiGame.Properties.Resources.Orange_Stone, size);
                    break;
                case 2:
                    _image = new Bitmap(MultiGame.Properties.Resources.Yellow_Stone, size);
                    break;
                case 3:
                    _image = new Bitmap(MultiGame.Properties.Resources.Green_Stone, size);
                    break;
                case 4:
                    _image = new Bitmap(MultiGame.Properties.Resources.Blue_Stone, size);
                    break;
                case 5:
                    _image = new Bitmap(MultiGame.Properties.Resources.Purple_Stone, size);
                    break;
                case 6:
                    _image = new Bitmap(MultiGame.Properties.Resources.Pink_Stone, size);
                    break;
                case 7:
                    _image = new Bitmap(MultiGame.Properties.Resources.Gray_Stone, size);
                    break;
            }
        
        }
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            base.OnPaint(obj, pe);
        }


        public override void OnHit()
        {
            base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(type);
            byte[] message = generator.Generate();

            GameManager.GetInstance().SendMessage(message);
        }

    }
}
