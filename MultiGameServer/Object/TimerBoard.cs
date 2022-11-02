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
        private TimerEventDelegate timerMatchAction = null;

        public int StartTime { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        private int currentTIme = 0;

        private int _timerCount = 0;
        public int TimerCount { get { return _timerCount; } }

        private object lockObj = new object();
        public TimerBoard(Room room, int key, Point Location, Size size, int timerCount)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.TIMER_BOARD;
            Collision = false;
            Blockable = false;
            _timerCount = timerCount;
            IsStatic = true;
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
        public void SetTimerMatchAction(TimerEventDelegate action)
        {
            timerMatchAction = action;
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
            lock (lockObj)
            {
                _timerCount--;
                currentTIme += (int)stoppedTime;

                if (_timerCount == 0)
                {
                    if (timerStopAction != null)
                        timerStopAction();

                    // 타이머 시간이 Min < < Max 가 아닐경우
                    if (StartTime - currentTIme > MaxTime || StartTime - currentTIme < MinTime)
                    {
                        if (timerNotMatchAction != null)
                            timerNotMatchAction();
                    }
                    else
                    {
                        if (timerMatchAction != null)
                            timerMatchAction();
                    }

                }

                MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                generator.AddInt(key).AddByte(Type).AddInt(-1);

                generator.AddBool(false).AddInt(Convert.ToInt32(Math.Max(0, stoppedTime)));

                room.SendMessageToAll_InRoom(generator.Generate());

            }
        }
    }
}
