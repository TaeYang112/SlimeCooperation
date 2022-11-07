using MultiGameModule;
using System.Drawing;
using System.Threading;

namespace MultiGameServer.Object
{
    public class PressingButton : GameObject
    {

        private bool Pressed { get; set; }

        // 이미 눌린 버튼위에 누르고 있는사람이 없는지를 계속 체크하는 타이머
        private Timer PressCheckTimer;

        // 버튼이 눌렸을 때와 떼졌을 때의 동작
        public delegate void OnPressDelegate(bool bPressed);
        OnPressDelegate onPressDelegate;

        public PressingButton(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.PRESSING_BUTTON;
            Collision = false;
            Blockable = false;
            Pressed = false;
            IsStatic = true;

            onPressDelegate = null;

            TimerCallback tc = new TimerCallback(PressCheck);
            PressCheckTimer = new Timer(tc);
        }

        public PressingButton(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        ~PressingButton()
        {
            PressCheckTimer.Dispose();
        }
        public override void OnClose()
        {
            base.OnClose();
        }
        public override void OnEvent(EventParam param)
        {
            if (Pressed == true) return;

            Pressed = true;
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key);
            generator.AddByte(Type);
            generator.AddInt(-1);
            generator.AddBool(true);

            room.SendMessageToAll_InRoom(generator.Generate());

            PressCheckTimer.Change(100, 100);
            if (onPressDelegate != null) onPressDelegate(true);
        }

        public void SetAction(OnPressDelegate action)
        {
            onPressDelegate = action;
        }

        private void PressCheck(object o)
        {
            bool result = false;

            // 검사 충돌 박스
            Rectangle a = new Rectangle(Location, size);

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in room.roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 결과 반환
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    result = true;
                    break;
                }
            }

            // 아무도 버튼을 누르고 있지 않다면
            if (result == false)
            {
                // 누르고 있지 않은 상태로 변경
                PressCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);

                Pressed = false;

                MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                generator.AddInt(key);
                generator.AddByte(Type);
                generator.AddInt(-1);
                generator.AddBool(false);

                room.SendMessageToAll_InRoom(generator.Generate());

                if (onPressDelegate != null) onPressDelegate(false);
            }

        }



    }
}
