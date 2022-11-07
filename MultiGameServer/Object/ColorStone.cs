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
    public class ColorStone : GameObject
    {
        private bool _disposed = false;

        private float GravityTime;                  // 중력 계산에 필요한 변수 ( 2차 함수 그래프의 x에 해당 )
        private float GravityPower;                 // 중력 파워
        private float dy;                           // 바로전에 얼만큼 뛰었는지
        private bool GravityStarted;                // 떨어지기 시작할 때 초기화를 하기 위한 플래그 변수
        private bool isGravity;                     // 중력의 ON / OFF
        // 움직임 타이머
        private System.Threading.Timer MoveTimer;

        public ColorStone(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.COLOR_STONE;
            Collision = true;
            Blockable = true;

            GravityTime = 0.0f;
            GravityPower = 50.0f;
            dy = 0;
            GravityStarted = false;
            isGravity = false;
            IsStatic = false;

            TimerCallback tc = new TimerCallback(CheckAndMove);
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
        }

        public ColorStone(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
            
            }
            MoveTimer.Dispose();
            _disposed = true;

            base.Dispose(disposing);
        }


        public override void OnStart()
        {
            base.OnStart();
            MoveTimer.Change(0, 13);
        }

        public void CheckAndMove(object obj)
        {
            Point velocity = new Point(0, 0);

            // 각각 방향에서 누가 밀고있으면 true
            ClientCharacter rightCharacter = BothCollisionCheck(true);
            ClientCharacter leftCharacter = BothCollisionCheck(false);

            // 오른쪽 인원이 무게보다 크고 왼쪽 인원이 없다면 왼쪽으로
            if (rightCharacter != null && leftCharacter == null)
            {
                velocity.X = -1;
            }
            else if(leftCharacter != null && rightCharacter == null)
            {
                velocity.X = 1;
            }

            if (isGravity)
            {
                float JumpHeight = (GravityTime * GravityTime - GravityPower * GravityTime) / 10.0f;
                GravityTime -= 1.2f;

                velocity.Y -= (int)(dy - JumpHeight);
                dy = JumpHeight;

            }
            else
                velocity.Y = 1;

            Point realVelo = Location;

            // 이동
            Move(velocity);

            realVelo = new Point(Location.X - realVelo.X, Location.Y - realVelo.Y);

            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            generator.AddInt(key);
            generator.AddByte(Type);
            generator.AddInt(-1);
            generator.AddInt(Location.X).AddInt(Location.Y);

            // 방안에 클라이언트들에게 돌이 움직였다고 알림
            room.SendMessageToAll_InRoom(generator.Generate());

        }


        // 충돌검사 후 이동
        public void Move(Point velocity)
        {
            Point resultLoc = Location;
            Point tempLoc;
            int dxy = 0;

            // x의 대한 충돌판정
            if (velocity.X != 0)
            {
                tempLoc = new Point(resultLoc.X + velocity.X, resultLoc.Y);

                if (velocity.X < 0) dxy = 1;
                else dxy = -1;

                // 만약 이동하려는곳에 다른 오브젝트가 있으면 좌표 1씩 옮겨서 체크해봄
                while (tempLoc.X != Location.X)
                {
                    // 충돌하지 않았으면
                    if (room.CollisionCheck(this, tempLoc) == false)
                    {
                        // 움직이기 위해 좌표 저장
                        resultLoc = tempLoc;
                        break;
                    }
                    // 충돌했으면
                    else
                    {
                        // 좌표 1칸 옮겨봄
                        tempLoc.X += dxy;
                    }
                }
            }

            // y에 대한 충돌 판정
            tempLoc = new Point(resultLoc.X, resultLoc.Y + velocity.Y);

            if (velocity.Y < 0) dxy = 1;
            else dxy = -1;


            // 만약 이동하려는곳에 다른 오브젝트가 있으면 좌표 1씩 옮겨서 체크해봄
            while (tempLoc.Y != Location.Y)
            {
                // 충돌하지 않았으면
                if (room.CollisionCheck(this, tempLoc) == false)
                {
                    // 이동
                    resultLoc = tempLoc;
                    GravityStart(true);
                    break;
                }
                // 충돌했으면
                else
                {
                    // 좌표 1칸 옮겨봄
                    tempLoc.Y += dxy;
                }
            }

            // 이동할 곳이 없으면 ( 반복문이 while 조건에 의해 종료 )
            if (tempLoc.Y == Location.Y)
            {
                // 만약 밑으로 가던중 충돌판정이 일어나면 
                if (velocity.Y > 0)
                {
                    //땅위에 있다는 플래그변수 true
                    GravityStart(false);
                }
            }

            // 실제 좌표를 이동시킴
            Location = resultLoc;
        }

        // 중력을 시작하는 함수
        public void GravityStart(bool bStart)
        {
            if (bStart == true)
            {
                if (GravityStarted == true) return;

                GravityStarted = true;
                isGravity = true;
                GravityTime = GravityPower / 2;
                dy = (GravityTime * GravityTime - GravityPower * GravityTime) / 10.0f;
            }
            else
            {
                GravityStarted = false;
                isGravity = false;
            }
        }

        public ClientCharacter BothCollisionCheck(bool bRight)
        {
            Point checkLocation = new Point(0, 0);
            if(bRight)
            {
                checkLocation = new Point(this.Location.X + this.size.Width + 1, this.Location.Y);
            }
            else
            {
                checkLocation = new Point(this.Location.X - 3, this.Location.Y);
            }


            // 검사 충돌 박스
            Rectangle a = new Rectangle(checkLocation, new Size(3, this.size.Height));

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in room.roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false) continue;

                // 클라이언트의 색과 돌의 색이 다를경우 무시
                if (otherClient.SkinNum != SkinNum) continue;

                // 오른쪽에서 미는데 왼쪽키를 안누르거나 왼쪽에서 미는데 오른쪽키를 안누르고있으면 나감
                if ((bRight == true && otherClient.bLeftPress == false) || (bRight == false && otherClient.bRightPress == false))
                {
                    continue;
                }

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 결과 반환
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    return otherClient;
                }
            }
            return null;
        }
    }
}
