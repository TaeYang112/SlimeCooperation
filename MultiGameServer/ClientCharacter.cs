using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

// -----------------
// ----- 서버 ------
// -----------------

namespace MultiGameServer
{
    class ClientCharacter
    {
        public int key { get; set; }                                                        // 각 클라이언트를 구별하기 위한 킷값
        public ClientData clientData { get; set; }                                          // TcpClient 객체
        public Point Location { get; set; }                                                 // 캐릭터의 위치

        public Thread characterMove_tr { get; set; }                                        // 클라이언트들을 움직이는 쓰레드


        private bool bMoving;                                                               // 클라이언트가 움직이고 있는지 체크하는 플래그변수. ( false 시 스레드 종료함 )
        public bool IsMoving { get { return bMoving; } }

        public bool bLeftDown { get; set; }                                                 // 왼쪽 방향키가 눌려있으면 TRUE / FALSE
        public bool bRightDown { get; set; }                                                // 오른쪽 방향키가 눌려있으면 TRUE / FALSE

        public ClientCharacter(int key, ClientData clientData)
        {
            this.key = key;
            Location = new Point(0, 0);
            this.clientData = clientData;

            bMoving = false;
            characterMove_tr = null;
        }

        public void MoveStart()
        {
            bMoving = true;
        }

        public void MoveStop()
        {
            bMoving = false;
        }
    }
}
