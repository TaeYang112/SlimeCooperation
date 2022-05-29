using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MultiGame
{

   

    // Client 정보를 갖는 클래스
    public class ClientCharacter
    {
        // 각 클라이언트를 구별하기 위한 킷값
        public int key { get; set; }

        // 좌표
        public Point Location { get; set; }

        // 크기
        public Size size { get; set; }

        // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private System.Threading.Timer MoveTimer;

        // 캐릭터 이미지
        public Image image { get; set; }

        // 플레이어가 쳐다보는 방향
        private Direction lookingDirection;

        public bool isVisible { get; set; }
        public bool isReady { get; set; }


        // 키가 눌려있는지 확인하는 변수
        public bool bLeftDown { get; set; }
        public bool bRightDown { get; set; }


        public enum Direction
        {
            Default,
            Left,
            Right
        }

        ~ClientCharacter()
        {
            MoveTimer.Dispose();
        }

        public ClientCharacter(int key, Point Location, int skinNum)
        {
            // 멤버변수 초기화
            this.key = key;

            bLeftDown = false;
            bRightDown = false;
            this.Location = Location;
            size = new Size(41, 49);
            isVisible = false;
            isReady = false;
            lookingDirection = Direction.Right;

            SetSkin(skinNum);

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveCharacter);                                    // 실행시킬 메소드
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);   // TimerCallback , null, 타이머 시작 전 대기시간, 타이머 호출 주기
        }

        public void SetSkin(int skinNum)
        {
            switch (skinNum % 8)
            {
                case 0:
                    image = MultiGame.Properties.Resources.red;
                    break;
                case 1:
                    image = MultiGame.Properties.Resources.orange;
                    break;
                case 2:
                    image = MultiGame.Properties.Resources.yellow;
                    break;
                case 3:
                    image = MultiGame.Properties.Resources.green;
                    break;
                case 4:
                    image = MultiGame.Properties.Resources.blue;
                    break;
                case 5:
                    image = MultiGame.Properties.Resources.purple;
                    break;
                case 6:
                    image = MultiGame.Properties.Resources.pink;
                    break;
                case 7:
                    image = MultiGame.Properties.Resources.gray;
                    break;
            }
        }

        // 현재 KeyDown 되어있는 키를 확인하여 움직임
        private void MoveCharacter(object stateInfo)
        {
            Point Loc = Location;
            Direction moveDirection = Direction.Default;

            // 왼쪽 방향키가 눌려있는 상태라면 왼쪽으로 움직임
            if (bLeftDown == true)
            {
                Loc.X -= 1;
                moveDirection = Direction.Left;
            }

            // 오른쪽 방향키가 눌려있는 상태라면 오른쪽으로 움직임
            if (bRightDown == true)
            {
                Loc.X += 1;
                moveDirection = Direction.Right;
            }

            // 움직인 방향에 따라 이미지를 뒤집음
            PlipImageWithDirection(moveDirection);

            // 좌표 대입
            Location = Loc;
        }

        public void MoveStart()
        {
            MoveTimer.Change(0, 5);
        }

        public void MoveStop()
        {
            MoveTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void OnPaint(Object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;

             var e = pe.Graphics;
                
             e.DrawImage(image,new Rectangle(Location, size ));
            
        }

        public void PlipImageWithDirection(Direction moveDirection)
        {
            // 움직이지 않고 있다면 리턴
            if (moveDirection == Direction.Default)
            {
                return;
            }

            // 움직이는 방향과 쳐다보는 방향이 다르면 뒤집음
            if( lookingDirection != moveDirection)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                lookingDirection = moveDirection;
            }

        }
    }
}
