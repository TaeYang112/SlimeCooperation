using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class Platform : GameObject
    {

        public Platform(Room room, int key, Point Location, Size size)
            : base(room, key,Location,size)
        {
            _type = ObjectTypes.PLATFORM;
            Collision = true;
            Blockable = true;
            IsStatic = true;
        }

        public Platform(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }
    }
}
