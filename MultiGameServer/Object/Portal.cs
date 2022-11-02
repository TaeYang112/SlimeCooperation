using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class Portal : GameObject
    {
        public Point TargetLocation { get; set; }
        public Portal(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.PORTAL;
            Collision = true;
            Blockable = false;
            TargetLocation = new Point(0, 0);
            IsStatic = true;
        }

        public Portal(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent(EventParam param)
        {
            ClientCharacter clientChar = param.clientCharacter;

            // 문밖에 나갈 수 있는지 체크 후 문밖으로 나오게 함
            bool result = room.CollisionCheck(clientChar, TargetLocation);

            if (result == false)
            {
                clientChar.MoveNum++;
                
                MessageGenerator generator = new MessageGenerator(Protocols.S_MOVE);
                generator.AddInt(TargetLocation.X - clientChar.Location.X).AddInt(TargetLocation.Y - clientChar.Location.Y);
                generator.AddInt(clientChar.MoveNum);

                Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);

                clientChar.Location = TargetLocation;
            }            
        }
       
        

    }
}
