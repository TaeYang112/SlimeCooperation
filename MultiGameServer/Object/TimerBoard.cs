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
        public delegate void TimerEventDelegate();

        private TimerEventDelegate timerStopAction = null;
        private TimerEventDelegate timerNotMatchAction = null;

        public int StartTime { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        private int currentTIme = 0;

        private int _timerCount = 0;
        public int TimerCount { get { return _timerCount; } }

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

        public void SetTimerStopAction(TimerEventDelegate action)
        {
            timerStopAction = action;
        }

        public void SetTimerNotMatchAction(TimerEventDelegate action)
        {
            timerNotMatchAction = action;
        }

        public void TimerStart()
        {
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key).AddByte(Type).AddInt(-1);

            generator.AddBool(true).AddInt(StartTime);

            room.SendMessageToAll_InRoom(generator.Generate());
        }


        public void TimerStop(long stoppedTime)
        {
            _timerCount--;
            currentTIme += (int)stoppedTime;

            if (_timerCount == 0)
            {
                if (timerStopAction != null)
                    timerStopAction();

                if (timerNotMatchAction != null && (StartTime - currentTIme > MaxTime || StartTime - currentTIme < MinTime))
                {
                    timerNotMatchAction();
                }
            }

            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key).AddByte(Type).AddInt(-1);

            generator.AddBool(false).AddInt(Convert.ToInt32(Math.Max(0,stoppedTime)));

            room.SendMessageToAll_InRoom(generator.Generate());
            
        }
    }
}
