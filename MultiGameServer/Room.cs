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
        public SortedSet<int> skinList;

        public string RoomTitle { get; set; }

        public bool bGameStart { get; set; }

        public Room(int key, string RoomTitle)
        {
            this.key = key;
            this.RoomTitle = RoomTitle;
            roomClientDic = new ConcurrentDictionary<int, ClientCharacter>();
            bGameStart = false;

            skinList = new SortedSet<int>();
            for (int i = 0; i < 8; i++)
            {
                skinList.Add(i);
            }
        }

        // 방에 클라이언트를 추가함
        public void ClientEnter(ClientCharacter clientChar)
        {
            // 클라이언트가 속한 방키를 설정
            clientChar.RoomKey = key;

            // 랜덤으로 스킨 부여
            int skinNum = GetRandomSkin();
            clientChar.SkinNum = skinNum;

            // 스킨 중복제거
            skinList.Remove(skinNum);

            roomClientDic.TryAdd(clientChar.key, clientChar);
        }

        // 클라이언트를 나가게 한뒤, 남은 인원수 반환
        public int ClientLeave(ClientCharacter clientChar)
        {
            clientChar.RoomKey = -1;
            roomClientDic.TryRemove(clientChar.key, out _);

            // 나갈때 스킨을 다시 돌려줌
            skinList.Add(clientChar.SkinNum);

            return GetPeopleCount();
        }


        // 방에 있는 클라이언트가 3명이상 레디할경우 true 아니면 false 반환
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

        // 방에 있는 클라이언트 수를 반환
        public int GetPeopleCount()
        {
            return roomClientDic.Count;
        }

        public int GetRandomSkin()
        {
            Random random = new Random();

            // 랜덤 숫자를 뽑음
            int num = random.Next(skinList.Count - 1);
            int i = 0;
            int result = 0;

            foreach(var item in skinList)
            {
                if( i == num)
                {
                    result = item;
                    break;
                }
                i++;
            }

            return result;
        }

    }
}
