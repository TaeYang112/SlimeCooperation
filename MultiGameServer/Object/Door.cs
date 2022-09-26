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

        public Door(int key, Point Location, Size size)
            : base(key, Location, size)
        {
            _type = "Door";
        }
    }
}
