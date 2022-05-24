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

        public PictureBox characterBox { get; set; }

        public Point Location { get { return characterBox.Location; } set { characterBox.Location = value; } }

        private System.Threading.Timer MoveTimer;                                           // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머

        // 키가 눌려있는지 확인하는 변수
        public bool bLeftDown { get; set; }
        public bool bRightDown { get; set; }

        public ClientCharacter(int key, PictureBox characterBox)
        {
            // 멤버변수 초기화
            this.key = key;
            this.characterBox = characterBox;
            bLeftDown = false;
            bRightDown = false;

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveCharacter);                              // 이벤트 발생 처리 루틴
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);   // TimerCallback , null, 타이머 시작 전 대기시간, 타이머 호출 주기
        }


        // 현재 KeyDown 되어있는 키를 확인하여 움직임
        private void MoveCharacter(object stateInfo)
        {
            Point Loc = Location;
            if (bLeftDown == true) Loc.X -= 2;                                                // 왼쪽 방향키가 눌려있는 상태라면 왼쪽으로 움직임
            if (bRightDown == true) Loc.X += 2;                                               // 오른쪽 방향키가 눌려있는 상태라면 오른쪽으로 움직임

            Location = Loc;
            // Console.WriteLine($"X : {Location.X}    Y : {Location.Y}");
        }

        public void MoveStart()
        {
            MoveTimer.Change(0, 10);
        }

        public void MoveStop()
        {
            MoveTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
