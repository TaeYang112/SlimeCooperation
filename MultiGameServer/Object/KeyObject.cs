using MultiGameModule;
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
            _type = ObjectTypes.KEY_OBJECT;
            ownerKey = -1;
            Collision = true;
            Blockable = false;
        }

        public KeyObject(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent(EventParam param)
        {
            base.OnEvent(param);

            ClientCharacter clientChar = param.clientCharacter;

            // 소유자가 있으면 무시
            if (ownerKey != -1)
            {
                return;
            }
            else
            {
                MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                ownerKey = clientChar.key;

                // 메시지를 만들어서 전송
                generator.AddInt(key).AddByte(ObjectTypes.KEY_OBJECT).AddInt(clientChar.key);
                room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);


                generator.Clear();

                // 당사자한테는 킷값을 -1로 보냄
                generator.AddInt(key).AddByte(ObjectTypes.KEY_OBJECT).AddInt(-1);
                Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);
            }
        }
    }
}
