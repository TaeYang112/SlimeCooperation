﻿using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class ThornBush : GameObject
    {

        public ThornBush(Room room, int key, Point Location, Size size)
            : base(room, key,Location,size)
        {
            _type = ObjectTypes.THORN_BUSH;
            Collision = true;
            Blockable = true;
            IsStatic = true;
        }

        public ThornBush(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent(EventParam param)
        {
            base.OnEvent(param);

            room.AllDie();

        }
    }
}
