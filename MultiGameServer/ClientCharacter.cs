using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        public bool bLeftDown { get; set; }                                                 // 왼쪽 방향키가 눌려있으면 TRUE / FALSE
        public bool bRightDown { get; set; }                                                // 오른쪽 방향키가 눌려있으면 TRUE / FALSE

        public ClientCharacter(int key, ClientData clientData)
        {
            this.key = key;
            Location = new Point(0, 0);
            this.clientData = clientData;
        }
    }
}
