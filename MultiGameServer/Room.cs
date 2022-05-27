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

        public bool IsAllReady()
        {
            int count = 0;
            foreach (var item in roomClientDic)
            {
                if (item.Value.bReady == true)
                {
                    count++;
                }
            }
            Console.WriteLine("[INFO] " + key + "번 방 "+ count + "/3 READY");
            // 준비한 캐릭터가 3명 이상일경우 true
            if (count >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
