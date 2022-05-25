using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class RoomManager
    {
        // 클라이언트들을 관리하는 객체
        // 실제 클라이언트 정보가 포함됨
        public ClientManager clientManager { get; set; }

        // 어떤방에 어떤 클라이언트가 있는지를 담고있는 객체
        public ConcurrentDictionary<int,Room> RoomDic { get; set; }

        // 새로운 방에 부여할 킷값을 저장
        private int CurrentKey;

        public RoomManager()
        {
            clientManager = clientManager = new ClientManager();

            RoomDic = new ConcurrentDictionary<int, Room>();
        }

        public Room CreateRoom(string RoomTitle)
        {
            Room newRoom = new Room(CurrentKey, RoomTitle);

            // 새로운 클라이언트를 배열에 저장
            RoomDic.TryAdd(CurrentKey, newRoom);

            CurrentKey++;

            return newRoom;
        }

        public class Room
        {
            public int key { get; }
            public ConcurrentDictionary<int,bool> clientKeyDic { get; }
            public string RoomTitle { get; set; }
            public Room(int key, string RoomTitle)
            {
                this.key = key;
                this.RoomTitle = RoomTitle;
                clientKeyDic = new ConcurrentDictionary<int, bool>();
            }

            public void ClientEnter(int clientKey)
            {
                clientKeyDic.TryAdd(clientKey,true);
            }

            public void ClientLeave(int clientKey)
            {
                clientKeyDic.TryRemove(clientKey, out _);
            }
        }
    }
}
