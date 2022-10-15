using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGame.Object
{
    public class KeyObject : GameObject
    {
        // 열쇠 소유자 
        private ClientCharacter _owner;
        public ClientCharacter owner { get { return _owner; } }

        public Timer MoveTimer { get; }

        public KeyObject(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = false;
            _owner = null;
            _type = ObjectTypes.KEY_OBJECT;

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveToTargetTimer);                                    // 실행시킬 메소드
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite); 
        }

        override public void SetSkin(int num)
        {
            isVisible = true;
            switch (num)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = MultiGame.Properties.Resources.Key;
                    break;
            }
        }

        public override void OnHit()
        {
            base.OnHit();
            Collision = false;

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(ObjectTypes.KEY_OBJECT);
            byte[] message = generator.Generate();

            GameManager.GetInstance().SendMessage(message);
        }

       public void SetOwner(ClientCharacter newOwner)
       {
            _owner = newOwner;
            if(_owner != null)
            {
                MoveStart(true);
            }
            else
            {
                MoveStart(false);
            }
       }

       private void MoveStart(bool flag)
        {
            if (flag) MoveTimer.Change(0, 13);
            else MoveTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void MoveToTargetTimer(object o)
        {
            Point TargetLocation = new Point( _owner.Location.X + _owner.size.Width / 2 - size.Width / 2, _owner.Location.Y);

            Point Velocity = new Point((TargetLocation.X - Location.X )/ 15, (TargetLocation.Y - Location.Y) / 15);

            Location = new Point(Location.X + Velocity.X, Location.Y + Velocity.Y);
        }
    }
}
