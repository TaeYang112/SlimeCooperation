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
            if(IsStart == false)
            {
                MoveTimer.Change(0, 13);
                IsStart = true;
            }
           
        }

        public void CheckAndMove(object obj)
        {

            // 각각 방향에서 Stone을 밀고있는 리스트
            List<ClientCharacter> rList = new List<ClientCharacter>();
            List<ClientCharacter> lList = new List<ClientCharacter>();

            // 밀었을 때 같이 움직일 클라이언트 리스트
            List<ClientCharacter> List = null;

            // 밀었을 때 움직일 거리
            int dx = 0;

            // 오른쪽에서 힘을 가하는 인원
            Point checkLoc = new Point(this.Location.X + this.size.Width + 1, this.Location.Y);
            TouchedClient(ref rList, true, checkLoc, new Size(1, this.size.Height), this);

            // 왼쪽에서 힘을 가하는 인원
            checkLoc = new Point(this.Location.X - 1, this.Location.Y);
            TouchedClient(ref lList, false, checkLoc, new Size(1, this.size.Height), this);

            // 오른쪽 인원이 무게보다 크고 왼쪽 인원이 없다면 왼쪽으로
            if (rList.Count >= weight && lList.Count == 0)
            {
                dx = -1;
                List = rList;
            }
            // 왼쪽 인원이 무게보다 크고 오른쪽 인원이 없다면 오른쪽으로
            else if (lList.Count >= weight && rList.Count == 0)
            {
                dx = 1;
                List = lList;
            }

            Console.WriteLine(lList.Count);

            // Stone이 움직여야한다면
            if (dx != 0)
            {
                // 실제 이동가능한지 체크 후 이동 ( 이동 가능하면 false )
                Point newLocation = new Point(Location.X + dx, Location.Y);
                bool result = room.CollisionCheck(this, newLocation);
                if (result == false)
                {
                    Location = newLocation;

                    // 클라이언트에게 Stone의 움직임을 알림
                    int showingWeight = Math.Max(0, weight - Math.Abs(rList.Count - lList.Count));
                    room.SendMessageToAll_InRoom($"ObjEvent#{key}#{Type}#{-1}#" +
                        $"{Location.X}#{Location.Y}#{showingWeight}@");

                    if (List != null)
                    {
                        // 클라이언트도 움직임
                        foreach (var client in List)
                        {
                            client.Location = new Point(client.Location.X + dx, client.Location.Y);
                            Program.GetInstance().SendMessage($"Move#{dx}#{0}@", client.key);
                        }
                    }
                }
            }
            else
            {
                // 클라이언트에게 Stone의 Weight을 알림
                int showingWeight = Math.Max(0, weight - Math.Abs(rList.Count - lList.Count));
                room.SendMessageToAll_InRoom($"ObjEvent#{key}#{Type}#{-1}#" +
                    $"{Location.X}#{Location.Y}#{showingWeight}@");
            }

            
        }


        public void TouchedClient(ref List<ClientCharacter> list,bool bRight, Point checkLocation, Size size, GameObject ignoreObject)
        {
            // 검사 충돌 박스
            Rectangle a = new Rectangle(checkLocation, size);

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in room.roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false || otherClient == ignoreObject) continue;

                // 오른쪽에서 미는데 왼쪽키를 안누르거나 왼쪽에서 미는데 오른쪽키를 안누르고있으면 나감
                if ((bRight == true && otherClient.bLeftPress == false) || (bRight == false && otherClient.bRightPress == false))
                {
                    continue;
                }

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 그 대상으로 부터 붙어있는 캐릭터 검사
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {

                    Point checkNewLoc;
                    if (bRight)
                    {
                        list.Add(otherClient);
                        // 대상 클라이언트의 오른쪽 면
                        checkNewLoc =
                            new Point(otherClient.Location.X + otherClient.size.Width + 1, otherClient.Location.Y);
                    }
                    else
                    {
                        list.Add(otherClient);
                        // 대상 클라이언트의 왼쪽 면
                        checkNewLoc =
                            new Point(otherClient.Location.X - 4, otherClient.Location.Y);
                    }
                    TouchedClient(ref list,bRight, checkNewLoc, new Size(3, otherClient.size.Height), otherClient);

                }
            }
        }

    }
}
