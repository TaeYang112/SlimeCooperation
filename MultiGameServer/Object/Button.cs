using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class Button : GameObject
    {
        public GameObject TargetObject { get; set; }
        public Button(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = "Button";
            Collision = true;
            Blockable = false;
            TargetObject = null;
        }

        public Button(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent()
        {
            room.SendMessageToAll_InRoom($"ObjEvent#{key}#{Type}#{-1}@");
        }
       
        

    }
}
