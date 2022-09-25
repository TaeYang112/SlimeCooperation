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
        }

        override public void SetSkin(int num)
        {
            switch(num)
            {
                case 0:
                    _image = MultiGame.Properties.Resources.Floor1;
                    break;
            }
        }
    }
}
