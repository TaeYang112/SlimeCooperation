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

        override public void SetSkin(int num)
        {
            isVisible = true;
            switch(num)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image =new Bitmap(MultiGame.Properties.Resources.Log, size);
                    break;
                case 1:
                    _image =new Bitmap(MultiGame.Properties.Resources.Log_Snow, size);
                    break;
                case 5:
                    _image = new Bitmap(MultiGame.Properties.Resources.Log_Single, size);
                    break;
                default:
                    _image =new Bitmap(MultiGame.Properties.Resources.Log, size);
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
