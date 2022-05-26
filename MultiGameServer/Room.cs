using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class Room
    {
        public int key { get; }
        public ConcurrentDictionary<int, ClientCharacter> roomClientDic { get; }
        public string RoomTitle { get; set; }
        public Room(int key, string RoomTitle)
        {
            this.key = key;
            this.RoomTitle = RoomTitle;
            roomClientDic = new ConcurrentDictionary<int, ClientCharacter>();
        }

        public void ClientEnter(ClientCharacter clientChar)
        {
            clientChar.RoomKey = key;
            roomClientDic.TryAdd(clientChar.key, clientChar);
        }

        public void ClientLeave(ClientCharacter clientChar)
        {
            clientChar.RoomKey = -1;
            roomClientDic.TryRemove(clientChar.key, out _);
        }
    }
}
