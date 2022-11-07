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
    public class Platform : GameObject
    {

        public Platform(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.PLATFORM;
        }

        ~Platform()
        {
            _image.Dispose();
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
                    _image = MultiGame.Properties.Resources.Log;
                    break;
                case 1:
                    _image = MultiGame.Properties.Resources.Log_Snow;
                    break;
                default:
                    _image = MultiGame.Properties.Resources.Log;
                    break;
            }

        }
        
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            base.OnPaint(obj, pe);
        }
    }
}
