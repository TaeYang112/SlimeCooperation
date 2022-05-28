using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Drawing;

namespace MultiGame
{
    // 클라이언트들을 관리하는 클래스
    public class ClientManager
    {
        // 클라이언트들을 담는 배열
        // 단순 배열과 다른점은 여러개의 스레드가 접근할때 자동으로 동기화 시켜줌
        public ConcurrentDictionary<int, ClientCharacter> ClientDic { get; set; }


        public ClientManager()
        {
            ClientDic = new ConcurrentDictionary<int, ClientCharacter>();
        }

        public ClientCharacter AddOrGetClient(int key, Point Location, int skinNum)
        {
            ClientCharacter ClientChar = new ClientCharacter(key, Location, skinNum);
            // 새로운 클라이언트를 배열에 저장
            bool result = ClientDic.TryAdd(key, ClientChar);

            // 이미 존재함
            if(result == false)
            {
                // 이미 존재하는 클라이언트 반환
                ClientDic.TryGetValue(key, out ClientChar);

                ClientChar.Location = Location;
                ClientChar.SetSkin(skinNum);
            }
            return ClientChar;
        }

        public void RemoveClient(ClientCharacter clientCharacter)
        {
            ClientDic.TryRemove(clientCharacter.key, out _);
        }
    }
}
