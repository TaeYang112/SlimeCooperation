using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class Floor : GameObject
    {

        public Floor(Room room, int key, Point Location, Size size)
            : base(room, key,Location,size)
        {
            _type = ObjectTypes.FLOOR;
            Collision = true;
            Blockable = true;
        }

        public Floor(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }
    }
}
