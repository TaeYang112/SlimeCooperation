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

        // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private System.Threading.Timer MoveTimer;

        // 땅인지
        public bool isGround { get; set; }

        // 점프 및 중력관련
        public bool isJump { get; set; }            // 점프중인지 체크하는 속성
        private float JumpTime;                     // 중력(점프) 계산에 필요한 변수 ( 2차 함수 그래프의 x에 해당 )
        private float JumpPower;                    // 점프의 높이
        private float dy;                           // 바로전에 얼만큼 뛰었는지
        private bool GravityStarted;                // 떨어지기 시작할 때 초기화를 하기 위한 플래그 변수
        private bool isGravity;                     // 중력의 ON / OFF


        public UserClient()
        {
            LeftDown = false;
            RightDown = false;
            JumpDown = false;

            isJump = false;
            isGround = false;
            JumpTime = 0.0f;
            JumpPower = 50.0f;
            dy = 0;
            GravityStarted = false;
            isGravity = false;

            Character = new ClientCharacter(-1, new Point(0, 0), 0);

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 60fps )
            TimerCallback tc = new TimerCallback(MoveWithKeyDown);
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);

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
                velocity.X -= 3;
                bLookRight = false;
            }

            // 오른쪽 방향키가 눌려있는 상태라면 오른쪽으로 움직임
            if (RightDown == true)
            {
                velocity.X += 3;
                bLookRight = true;
            }

            if (LeftDown ^ RightDown && Character.bLookRight != bLookRight)
            {
                GameManager.GetInstance().SendMessage($"LookR#{bLookRight}@");
                Character.SetLookDirection(bLookRight);
            }

            // 이차 함수를 이용한 점프
            if (isJump)
            {
                float JumpHeight = (JumpTime * JumpTime - JumpPower * JumpTime) / 10.0f;      // y = x^2 - ax 그래프
                JumpTime += 1.2f;

                // 이차 함수의 맨 위에 도달
                if (JumpTime > JumpPower/2)
                {
                    JumpTime = 0;
                    dy = 0;
                    isJump = false;
                }
                else
                {
                    velocity.Y -= (int)(dy - JumpHeight);
                    dy = JumpHeight;
                }
            }
            // 중력
            else
            {
                if (isGravity)
                {
                    float JumpHeight = (JumpTime * JumpTime  - JumpPower * JumpTime) / 10.0f;
                    JumpTime -= 1.2f;

                    velocity.Y -= (int)(dy - JumpHeight);
                    dy = JumpHeight;
                    
                }
                else
                    velocity.Y = 1;

            }
            // 이동
            Move(velocity);
        }
        
        // 중력을 시작하는 함수
        public void GravityStart(bool bStart)
        {
            if(bStart == true)
            {
                if (GravityStarted == true) return;

                GravityStarted = true;
                isGravity = true;
                JumpTime = JumpPower / 2;
                dy = (JumpTime * JumpTime - JumpPower * JumpTime) / 10.0f;
            }
            else
            {
                GravityStarted = false;
                isGravity = false;
            }
        }
        // 점프
        public void Jump()
        {
            if (isJump == true || isGround == false) return;
            GravityStart(false);
            JumpTime = 0;
            dy = 0;
            isJump = true;
        }


        // 캐릭터 충돌검사 후 이동
        public void Move(Point velocity)
        {
            GameObject CollidedObj;
            Point resultLoc = Character.Location;
            Point tempLoc;

            // x의 대한 충돌판정
            if (velocity.X != 0)
            {
                tempLoc = new Point(resultLoc.X + velocity.X, resultLoc.Y);

                // 충돌하지 않았으면
                CollidedObj = CollisionCheck(tempLoc);
                if (CollidedObj == null)
                {
                    // 움직이기 위해 좌표 저장
                    resultLoc = tempLoc;
                }
                // 충돌했으면 최대한 옆으로 이동함
                else
                {
                    if(velocity.X < 0)
                    {
                        resultLoc = new Point( CollidedObj.Location.X + CollidedObj.size.Width + 1, tempLoc.Y);
                    }
                    else
                    {
                        resultLoc = new Point(CollidedObj.Location.X - Character.size.Width - 1, tempLoc.Y);
                    }
                }
            }

            tempLoc = new Point(resultLoc.X, resultLoc.Y + velocity.Y);

            // y에 대한 충돌 판정
            // 겹치지 않았을 경우
            CollidedObj = CollisionCheck(tempLoc);
            if (CollidedObj == null)
            {
                // 이동
                resultLoc = tempLoc;
                isGround = false;
                if(isJump == false ) GravityStart(true);
            }
            else
            {
                // 만약 밑으로 가던중 충돌판정이 일어나면 
                if (velocity.Y > 0)
                {
                    //땅위에 있다는 플래그변수 true
                    isGround = true;
                    GravityStart(false);

                    // 최대한 이동
                    resultLoc = new Point(tempLoc.X, CollidedObj.Location.Y - Character.size.Height - 1);
                    Console.WriteLine(resultLoc.Y);
                    // 땅에 도착했을 때 점프버튼을 누르고있다면 다시 점프
                    if (JumpDown == true)
                        Jump();
                }
                // 만약 위로 가던중 충돌판정이 일어나면
                else
                {
                    isJump = false;
                    resultLoc = new Point(tempLoc.X, CollidedObj.Location.Y + CollidedObj.size.Height + 1);
                }

            }

            // 실제 좌표를 이동시킴
            Character.Location = resultLoc;

            // 서버로 전송
            GameManager.GetInstance().SendMessage($"Location#{resultLoc.X}#{resultLoc.Y}#@");
        }

        // 겹치면 겹친 GameObject 반환
        public GameObject CollisionCheck(Point newLocation)
        {
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
                    return otherClient;
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
                    if (gameObject.Blockable == true) return gameObject;
                    else continue;
                }
            }

            return null;
        }


    }
}
