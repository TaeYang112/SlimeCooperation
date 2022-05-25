using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MultiGameServer
{
    // 클라이언트들을 관리하는 클래스
    public class ClientManager
    {
        // 클라이언트들을 담는 배열
        // 단순 배열과 다른점은 여러개의 스레드가 접근할때 자동으로 동기화 시켜줌
        public ConcurrentDictionary<int, ClientCharacter> ClientDic { get; }

        // 새로운 클라이언트에게 부여할 킷값을 저장
        private int CurrentKey;

        public ClientManager()
        {
            ClientDic = new ConcurrentDictionary<int, ClientCharacter>();
            CurrentKey = 0;
        }

        public ClientCharacter AddClient(ClientData newClient)
        {
            ClientCharacter newClientCharacter = new ClientCharacter(CurrentKey, newClient);

            // 새로운 클라이언트를 배열에 저장
            ClientDic.TryAdd(CurrentKey, newClientCharacter);

            CurrentKey++;

            return newClientCharacter;
        }

        public void RemoveClient(ClientCharacter client)
        {
            ClientDic.TryRemove(client.key, out _);
        }
    }
}
