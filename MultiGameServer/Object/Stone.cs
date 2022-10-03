using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    public class Stone : GameObject
    {

        private bool IsStart;
        public int weight { get; set; }

        // 움직임 타이머
        private System.Threading.Timer MoveTimer;

        public Stone(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = "Stone";
            Collision = true;
            Blockable = true;
            IsStart = false;
            weight = 0;

            TimerCallback tc = new TimerCallback(CheckAndMove);
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
        }

        public Stone(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnClose()
        {
            base.OnClose();
            MoveTimer.Dispose();
        }

        public void onPush()
        {
            if (IsStart == false)
            {
                // 타이머 시작
                MoveTimer.Change(0, 13);
                IsStart = true;
            }

        }

        public void CheckAndMove(object obj)
        {
            // 오른쪽에서 가해지는 힘
            Point checkLoc = new Point(this.Location.X + this.size.Width + 1, this.Location.Y);
            int rightCount = TouchedClient(true, checkLoc, new Size(1, this.size.Height), this);

            // 왼쪽에서 가해지는 힘
            checkLoc = new Point(this.Location.X - 1, this.Location.Y);
            int leftCount = TouchedClient(false, checkLoc, new Size(1, this.size.Height), this);

            // 가해지는 힘이 무게보다 높으면 움직임 ( 서로 다른 방향은 상쇄됨 )
            if (Math.Abs(rightCount - leftCount) >= weight)
            {
                Point newLocation;
                // 오른쪽에서 오는 힘이 크면 왼쪽으로
                if (rightCount > leftCount)
                {
                    newLocation = new Point(Location.X - 3, Location.Y);
                }
                // 왼쪽에서 오는 힘이 크면 오른쪽으로
                else
                {
                    newLocation = new Point(Location.X + 3, Location.Y);
                }
                // 실제 이동가능한지 체크 후 이동 ( 충돌시 true )
                bool result = room.CollisionCheck(this, newLocation);
                if (result == false)
                {
                    Location = newLocation;
                }
            }

            room.SendMessageToAll_InRoom($"ObjEvent#{key}#{Type}#{-1}#" +
                $"{Location.X}#{Location.Y}#{weight - Math.Abs(rightCount - leftCount)}@");
            Console.WriteLine("sdf");
        }


        public int TouchedClient(bool bRight, Point checkLocation, Size size, GameObject ignoreObject)
        {
            int result = 0;

            // 검사 충돌 박스
            Rectangle a = new Rectangle(checkLocation, size);

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in room.roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false || otherClient == ignoreObject) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 그 대상으로 부터 붙어있는 캐릭터 검사
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    result++;

                    Point checkNewLoc;
                    if (bRight)
                    {
                        // 대상 클라이언트의 오른쪽 면
                        checkNewLoc =
                            new Point(otherClient.Location.X + otherClient.size.Width + 1, otherClient.Location.Y);
                    }
                    else
                    {
                        // 대상 클라이언트의 왼쪽 면
                        checkNewLoc =
                            new Point(otherClient.Location.X - 1, otherClient.Location.Y);
                    }
                    result += TouchedClient(bRight, checkNewLoc, new Size(1, otherClient.size.Height), otherClient);

                }
            }

            return result;
        }

    }
}
