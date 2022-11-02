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
    public class Button: GameObject
    {
        // 버튼이 눌렸을 때의 동작
        public delegate void OnPressDelegate();
        OnPressDelegate onPressDelegate;

        public bool Pressed { get; set; }

        public Button(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.BUTTON;
            Collision = false;
            Blockable = false;
            Pressed = false;
            IsStatic = true;
        }

        public Button(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public void SetAction(OnPressDelegate action)
        {
            onPressDelegate = action;
        }

        public override void OnEvent(EventParam param)
        {
            if (Pressed == true) return;
            Pressed = true;

            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key);
            generator.AddByte(Type);
            generator.AddInt(-1);

            if (onPressDelegate != null) onPressDelegate();

            room.SendMessageToAll_InRoom(generator.Generate());
        }
       
        

    }
}
