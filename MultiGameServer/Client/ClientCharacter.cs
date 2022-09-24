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
            size = new Size(60, 50);
            bReady = false;
            bFindingRoom = false;
        }


        // 소멸자 호출시 비관리 메모리 제거
        ~ClientCharacter()
        {
        }

        public void GameStart()
        {
        }

        
    }
}
