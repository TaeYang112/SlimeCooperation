﻿using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame.Object
{
    public class StoneDoor : GameObject
    {
        public StoneDoor(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.STONE_DOOR;
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
                    _image = new Bitmap(MultiGame.Properties.Resources.Stone, size); ;
                    break;
            }
        }

    }
}
