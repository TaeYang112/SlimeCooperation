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
        // 각 클라이언트를 구별하기 위한 킷값
        public int key {
            get
            {
                return clientData.key;
            }
            set
            {
                clientData.key = value;
            }
        }

        // TcpClient 객체
        public ClientData clientData { get; set; }

        // 캐릭터의 위치
        public Point Location { get; set; }

        public Size size { get; set; }

        // 캐릭터 스킨 번호
        public int SkinNum { get; set; }

        // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머
        private System.Threading.Timer MoveTimer;
        // 캐릭터가 움직이기 시작하면 주기적으로 호출 ( 캐릭터의 위치를 클라이언트와 동기화 하기 위해 사용 )
        private System.Threading.Timer SyncTimer;

        // 캐릭터의 위치를 클라이언트와 동기화 하기 위한 이벤트
        public event LocSyncEventHandler LocationSync;                                      

        // 클라이언트의 키 입력을 관리하는 속성
        public bool bLeftDown { get; set; }
        public bool bRightDown { get; set; }

        // 현재 클라이언트가 속해 있는 방 키
        public int RoomKey { get; set; }                                                    


        // 레디 여부
        public bool bReady { get; set; }

        // 방을 찾고있는지 여부
        public bool bFindingRoom { get; set; }

        public ClientCharacter(int key, ClientData clientData)
        {
            this.clientData = clientData;
            this.key = key;
            Location = new Point(0, 0);
            size = new Size(41, 49);
            bReady = false;
            bFindingRoom = false;

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            TimerCallback tc = new TimerCallback(MoveCharacter);                                    // 이벤트 발생 처리 루틴
            MoveTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);   // TimerCallback , null, 타이머 시작 전 대기시간, 타이머 호출 주기


            //  캐릭터가 움직이기 시작하면 주기적으로 호출 ( 캐릭터의 위치를 클라이언트와 동기화 하기 위해 사용 )
            TimerCallback tc2 = new TimerCallback(LocationSyncFunc);
            SyncTimer = new System.Threading.Timer(tc2, null, Timeout.Infinite, Timeout.Infinite);
        }


        // 소멸자 호출시 비관리 메모리 제거
        ~ClientCharacter()
        {
            MoveTimer.Dispose();
            SyncTimer.Dispose();
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

            //Location = Loc;
            Program.GetInstance().MoveObject(this, Loc);
            // Console.WriteLine($"X : {Loc.X}    Y : {Loc.Y}");
        }

        private void LocationSyncFunc(object clientArgs)
        {
            LocationSync(this);
        }

        
    }
}
