using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    class Door : GameObject
    {
        public bool isOpen { get; set; }

        public Door(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.DOOR;
            Collision = true;
            Blockable = false;
        }

        public Door(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }
    }
}
