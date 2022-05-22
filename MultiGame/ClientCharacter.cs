using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame
{
    // Client 정보를 갖는 클래스
    public class ClientCharacter
    {
        // 각 클라이언트를 구별하기 위한 킷값
        public int key { get; set; }

        public Button character { get; set; }

        // 키가 눌려있는지 확인하는 변수
        public bool bLeftDown { get; set; }
        public bool bRightDown { get; set; }

        public ClientCharacter(int key, Button character)
        {
            // 멤버변수 초기화
            this.key = key;
            this.character = character;
            bLeftDown = false;
            bRightDown = false;

        }
    }
}
