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
    public class StoneDoor : GameObject
    {
        private System.Threading.Timer MoveTimer;

        public int MaxOffset { get; set; }

        private int baseY { get; set; }

        public byte OpeningMode { get; set; }

        public StoneDoor(Room room, int key, Point Location, Size size)
            : base(room, key,Location,size)
        {
            _type = ObjectTypes.STONE_DOOR;
            Collision = true;
            Blockable = true;
            MaxOffset = 100;
            baseY = Location.Y;
            TimerCallback tc = new TimerCallback(Opening);
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
            IsStatic = false;
            OpeningMode = StoneDoorMode.MOVE;
        }

        public StoneDoor(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent()
        {
            Open();
        }

        public void Open()
        {
            if(OpeningMode == StoneDoorMode.MOVE)
            {
                // 타이머를 이용하여 정해진 거리만큼 움직임
                MoveTimer.Change(0, 13);
            }
            else
            {
                // 클라이언트들에게 문이 사라졌다고 알려줌
                MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                generator.AddInt(key);
                generator.AddByte(Type);
                generator.AddInt(-1);
                generator.AddByte(StoneDoorMode.BEGONE);
                room.SendMessageToAll_InRoom(generator.Generate());

                // 서버 자체적으로 충돌판정 없앰
                Collision = false;
            }
        }

        public void Opening(object o)
        {
            Location = new Point(Location.X, Location.Y - 2);

            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key);
            generator.AddByte(Type);
            generator.AddInt(-1);
            generator.AddByte(StoneDoorMode.MOVE);
            generator.AddInt(Location.X).AddInt(Location.Y);
            room.SendMessageToAll_InRoom(generator.Generate());

            /*
            //Size tempSize = new Size(size.Width, 1);
            Size tempSize = size;
            //Point tempLoc = new Point(Location.X, Location.Y + size.Height + 1);
            Point tempLoc = Location;
            Rectangle a = new Rectangle(tempLoc, tempSize);

            generator.Clear();
            generator.Protocol = Protocols.S_MOVE;
            generator.AddInt(0).AddInt(-2);

            foreach(var item in room.roomClientDic)
            {
                ClientCharacter clientChar = item.Value;

                Rectangle b = new Rectangle(clientChar.Location, clientChar.size);

                if(Rectangle.Intersect(a,b).IsEmpty == false)
                {
                    Program.GetInstance().SendMessage(generator.GetMessage(), clientChar.key);
                }
            }
            */
            if (Location.Y <= baseY - MaxOffset) MoveTimer.Change(Timeout.Infinite, Timeout.Infinite);

            
        }
    }
}
