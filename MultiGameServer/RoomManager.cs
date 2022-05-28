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

        // 어떤방에 어떤 클라이언트가 있는지를 담고있는 객체
        public ConcurrentDictionary<int,Room> RoomDic { get; set; }

        // 새로운 방에 부여할 킷값을 저장
        private int CurrentKey;

        public RoomManager()
        {
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

        public void RemoveRoom(Room room)
        {
            RoomDic.TryRemove(room.key,out _);
        }
    }
}
