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

        public ClientCharacter(int key, Button character)
        {
            this.key = key;
            this.character = character;
        }
    }
}
