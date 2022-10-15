using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame.Object
{
    public class Lava : GameObject
    {
        public Lava(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.FLOOR;
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
                    _image = MultiGame.Properties.Resources.Lava1;
                    break;
            }
        }
    }
}
