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
    public delegate void LocSyncEventHandler(ClientCharacter clientCharacter);
    public class ClientCharacter
    {
        public int key { get; set; }                                                        // 각 클라이언트를 구별하기 위한 킷값
        public ClientData clientData { get; set; }                                          // TcpClient 객체
        public Point Location { get; set; }                                                 // 캐릭터의 위치

        private System.Threading.Timer MoveTimer;                                           // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private System.Threading.Timer SyncTimer;                                           // 캐릭터가 움직이기 시작하면 주기적으로 호출 ( 캐릭터의 위치를 클라이언트와 동기화 하기 위해 사용 )

        public event LocSyncEventHandler LocationSync;                                      // 캐릭터의 위치를 클라이언트와 동기화 하기 위한 이벤트

        public bool bLeftDown { get; set; }                                                 // 왼쪽 방향키가 눌려있으면 TRUE / FALSE
        public bool bRightDown { get; set; }                                                // 오른쪽 방향키가 눌려있으면 TRUE / FALSE
        public int RoomKey { get; set; }                                                    // 현재 클라이언트가 속해 있는 방 키

        public ClientCharacter(int key, ClientData clientData)
        {
            this.key = key;
            Location = new Point(0, 0);
            this.clientData = clientData;

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveCharacter);                              // 이벤트 발생 처리 루틴
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);   // TimerCallback , null, 타이머 시작 전 대기시간, 타이머 호출 주기

            //  캐릭터가 움직이기 시작하면 주기적으로 호출 ( 캐릭터의 위치를 클라이언트와 동기화 하기 위해 사용 )
            TimerCallback tc2 = new TimerCallback(LocationSyncFunc);
            SyncTimer = new System.Threading.Timer(tc2, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void MoveStart()
        { 
            MoveTimer.Change(0, 5);
            SyncTimer.Change(200, 200);
        }

        public void MoveStop()
        {
            MoveTimer.Change(Timeout.Infinite, Timeout.Infinite);
            SyncTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void MoveCharacter(object clientArgs)
        {
            Point Loc = Location;
            if (bLeftDown == true) Loc.X -= 1;
            if (bRightDown == true) Loc.X += 1;

            Location = Loc;
            // Console.WriteLine($"X : {Loc.X}    Y : {Loc.Y}");
        }

        private void LocationSyncFunc(object clientArgs)
        {
            LocationSync(this);
        }
    }
}
