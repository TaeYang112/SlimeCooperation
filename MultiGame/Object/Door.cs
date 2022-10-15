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
    public class Door : GameObject
    {
        private bool _open;
        public bool isOpen { get { return _open; } }

        public Door(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = false;
            Blockable = false;
            _open = false;
            _type = ObjectTypes.DOOR;
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
                    _image = MultiGame.Properties.Resources.Door;
                    break;
                case 1:
                    _image = MultiGame.Properties.Resources.DoorOpened;
                    break;
            }
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            var e = pe.Graphics;

            if (isVisible == false) return;

            if(isOpen)
                e.DrawImage(image, new Rectangle(Location, new Size(120, size.Height)));
            else
                e.DrawImage(image, new Rectangle(Location, size));
        }


        public void Open(bool flag)
        {
            _open = flag;
            if(flag)
            {
                SetSkin(1);
            }
            else
            {
                SetSkin(0);
            }
        }
    }
}
