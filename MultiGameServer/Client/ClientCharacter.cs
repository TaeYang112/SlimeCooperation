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
    public class ClientCharacter : GameObject
    {
        // TcpClient 객체
        public ClientData clientData { get; set; }


        // 현재 클라이언트가 속해 있는 방 키
        public int RoomKey { get; set; }                                                    


        // 레디 여부
        public bool bReady { get; set; }

        // 방을 찾고있는지 여부
        public bool bFindingRoom { get; set; }

        public ClientCharacter(int key, ClientData clientData)
            :base(key,new Point(0,0), new Size(60,50))
        {
            this.clientData = clientData;
            clientData.key = key;
            bReady = false;
            bFindingRoom = false;
        }

        public void GameStart()
        {
        }

        
    }
}
