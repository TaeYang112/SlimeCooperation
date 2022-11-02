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
    public class TimerBox : GameObject
    {
        private System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

        public int StartTime { get; set; }

        private System.Threading.Timer timer;

        public delegate void TimerStopDelegate();

        private TimerStopDelegate timerStopAction = null;

        public int Time { get { return Convert.ToInt32(Math.Max(0, StartTime - st.ElapsedMilliseconds)); } }
        

        public TimerBox(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.TIMER_BOX;
            Collision = false;
            Blockable = false;
            IsStatic = true;

            System.Threading.TimerCallback tc = new System.Threading.TimerCallback(TimerStop);
            timer = new System.Threading.Timer(tc);
        }

        public TimerBox(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnClose()
        {
            base.OnClose();
            timer.Dispose();
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

            timer.Change(StartTime, Timeout.Infinite);
        }


        public void TimerStop(object o = null)
        {
            if (st.IsRunning == false) return;

            timer.Change(Timeout.Infinite, Timeout.Infinite);
            st.Stop();

            if(timerStopAction != null)
                timerStopAction();

            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key).AddByte(Type).AddInt(-1);

            generator.AddBool(false).AddInt(Convert.ToInt32(Math.Max(0,StartTime - st.ElapsedMilliseconds)));

            room.SendMessageToAll_InRoom(generator.Generate());
            
        }
    }
}
