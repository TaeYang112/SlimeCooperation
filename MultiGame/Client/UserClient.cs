using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGame.Client
{
    public class UserClient
    {
        public ClientCharacter Character { get; set; }

        // 키가 눌려있는지 확인하는 변수
        public bool LeftDown { get; set; }
        public bool RightDown { get; set; }
        public bool JumpDown { get; set; }

        // 점프중인지
        public bool isJump { get; set; }
        public bool isGround { get; set; }

        // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private System.Threading.Timer MoveTimer;

        private System.Threading.Timer JumpTimer;

        public UserClient()
        {
            LeftDown = false;
            RightDown = false;
            JumpDown = false;

            isJump = false;
            isGround = false;

            Character = new ClientCharacter(-1, new Point(0, 0), 0);

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveWithKeyDown);                                    // 실행시킬 메소드
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);   // TimerCallback , null, 타이머 시작 전 대기시간, 타이머 호출 주기

            // 점프를 종료시키는 타이머
            TimerCallback tc3 = new TimerCallback(JumpStop);
            JumpTimer = new System.Threading.Timer(tc3, null, Timeout.Infinite, Timeout.Infinite);


        }
        public void Start()
        {
            MoveTimer.Change(0, 13);
            Character.GameStart();
        }

        // 현재 KeyDown 되어있는 키를 확인하여 움직임
        public void MoveWithKeyDown(object stateInfo)
        {
            Point velocity = new Point(0, 0);

            bool bLookRight = false;

            // 왼쪽 방향키가 눌려있는 상태라면 왼쪽으로 움직임
            if (LeftDown == true)
            {
                velocity.X -= 2;
                bLookRight = false;
            }

            // 오른쪽 방향키가 눌려있는 상태라면 오른쪽으로 움직임
            if (RightDown == true)
            {
                velocity.X += 2;
                bLookRight = true;
            }
            
            if(LeftDown ^ RightDown && Character.bLookRight != bLookRight)
            {
                GameManager.GetInstance().SendMessage($"LookR#{bLookRight}@");
                Character.SetLookDirection(bLookRight);
            }
            
            // 중력
            if (isJump)
            {
                velocity.Y -= 3;
            }
            else
            {
                velocity.Y += 3;
            }

            // 이동
            Move(velocity);
        }

        // 매개변수 velocity를 이용하여 움직임
        public void MoveWithVelocity(Point velocity)
        {
            // 이동
            Move(velocity);
        }

        // 점프
        public void Jump()
        {
            if (isJump == true || isGround == false) return;

            isJump = true;
            JumpTimer.Change(300, Timeout.Infinite);
        }

        // 점프 종료
        public void JumpStop(object stateInfo)
        {
            isJump = false;
        }

        // 캐릭터 충돌검사 후 이동
        public void Move(Point velocity)
        {
            Point resultLoc = Character.Location;
            Point tempLoc;

            // x의 대한 충돌판정
            if (velocity.X != 0)
            {
                tempLoc = new Point(resultLoc.X + velocity.X, resultLoc.Y);

                // 충돌하지 않았으면
                if (CollisionCheck(tempLoc) == false)
                {
                    // 움직이기 위해 좌표 저장
                    resultLoc = tempLoc;
                }
            }

            tempLoc = new Point(resultLoc.X, resultLoc.Y + velocity.Y);

            // y에 대한 충돌 판정
            // 겹치지 않았을 경우
            if (CollisionCheck(tempLoc) == false)
            {
                // 이동
                resultLoc = tempLoc;
                isGround = false;
            }
            else
            {
                // 만약 밑으로 가던중 충돌판정이 일어나면 
                if (velocity.Y > 0)
                {
                    //땅위에 있다는 플래그변수 true
                    isGround = true;

                    // 땅에 도착했을 때 점프버튼을 누르고있다면 다시 점프
                    if (JumpDown == true)
                        Jump();
                }
                // 만약 위로 가던중 충돌판정이 일어나면
                else
                {
                    JumpStop(this);
                    JumpTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }

            }

            // 실제 좌표를 이동시킴
            Character.Location = resultLoc;

            // 서버로 전송
            GameManager.GetInstance().SendMessage($"Location#{resultLoc.X}#{resultLoc.Y}#@");
        }

        // 겹치면 true 반환
        public bool CollisionCheck(Point newLocation)
        {
            // 임시 바닥
            if (newLocation.Y >= 400) return true;

            // 캐릭터의 충돌 박스
            Rectangle a = new Rectangle(newLocation, Character.size);

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in GameManager.GetInstance().clientManager.ClientDic)
            {
                ClientCharacter otherClient = item.Value;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    return true;
                }
            }

            // 맵의 모든 오브젝트와 부딪히는지 체크함
            foreach (var item in GameManager.GetInstance().objectManager.ObjectDic)
            {
                GameObject gameObject = item.Value;

                if (gameObject.Collision == false) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(gameObject.Location, gameObject.size);

                // 만약 움직였을때 겹친다면 충돌 발생
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    gameObject.OnHit();

                    // 해당 오브젝트가 길을 막을 수 있으면 true반환하여 이동 제한
                    if (gameObject.Blockable == true) return true;
                    else continue;
                }
            }
            Character.Location = newLocation;

            return false;
        }

        
    }
}
