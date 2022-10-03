using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    class KeyObject : GameObject
    {
        // 열쇠 소유자 키
        public int ownerKey { get; set; }

        public KeyObject(Room room, int key, Point Location, Size size)
            : base(room,key, Location, size)
        {
            _type = "Key";
            ownerKey = -1;
            Collision = true;
            Blockable = false;
        }

        public KeyObject(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }
    }
}
