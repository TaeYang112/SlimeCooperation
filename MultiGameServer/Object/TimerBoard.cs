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
    public class TimerBoard : GameObject
    {
        private System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

        public delegate void TimerStopDelegate();

        private TimerStopDelegate timerStopAction = null;

        public int StartTime { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        private int _timerCount = 0;
        public int TimerCount { get { return _timerCount; } }

        public int Time { get { return Convert.ToInt32(Math.Max(0, StartTime - st.ElapsedMilliseconds)); } }

        public TimerBoard(Room room, int key, Point Location, Size size, int timerCount)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.TIMER_BOARD;
            Collision = false;
            Blockable = false;
            _timerCount = timerCount;
        }

        public TimerBoard(Room room, int key, Point Location, Point Location2, int timerCount)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y), timerCount)
        {
        }

        public override void OnClose()
        {
            base.OnClose();
            st.Stop();
        }

        public override void OnEvent(EventParam param)
        {

        }

        public void SetTimerStopAction(TimerStopDelegate action)
        {
            timerStopAction = action;
        }

        public void TimerStart()
        {
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key).AddByte(Type).AddInt(-1);

            generator.AddBool(true).AddInt(StartTime);

            room.SendMessageToAll_InRoom(generator.Generate());
            st.Start();
        }


        public void TimerStop()
        {
            _timerCount--;

            if(timerStopAction != null)
                timerStopAction();

            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key).AddByte(Type).AddInt(-1);

            generator.AddBool(false).AddInt(Convert.ToInt32(Math.Max(0,st.ElapsedMilliseconds)));

            room.SendMessageToAll_InRoom(generator.Generate());
            
        }
    }
}
