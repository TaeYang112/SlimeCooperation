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

        // 레디 여부
        public bool IsReady { get; set; }

        // 방을 찾고있는지 여부
        public bool IsFindingRoom { get; set; }

        // 문에 들어갔는지
        public bool IsEnterDoor { get; set;}


        // 키 입력 여부
        public bool bLeftPress { get; set; }
        public bool bRightPress { get; set; }

        public ClientCharacter(Room room, int key, ClientData clientData)
            :base(room, key,new Point(0,0), new Size(60,50))
        {
            this.clientData = clientData;
            clientData.key = key;
            IsReady = false;
            IsFindingRoom = false;
            IsEnterDoor = false;
            Collision = true;
            Blockable = true;

            bRightPress = false;
            bLeftPress = false;
        }

        public void GameStart()
        {
        }

        
    }
}
