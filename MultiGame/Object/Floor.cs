using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame.Object
{
    public class Floor : GameObject
    {
        public Floor(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = "Floor";
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
                    _image = MultiGame.Properties.Resources.Floor1;
                    break;
                case 1:
                    _image = MultiGame.Properties.Resources.Floor2;
                    break;
                case 2:
                    _image = MultiGame.Properties.Resources.Floor3;
                    break;
                case 3:
                    _image = MultiGame.Properties.Resources.Floor4;
                    break;
            }
        }
    }
}
