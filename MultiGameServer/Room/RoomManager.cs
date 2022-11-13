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

        private List<KeyValuePair<string, long>> timeList = new List<KeyValuePair<string, long>>();
        public List<KeyValuePair<string,long>> TimeList { get { return timeList; } }

        public RoomManager()
        {
            RoomDic = new ConcurrentDictionary<int, Room>();
        }

        public Room CreateRoom(string RoomTitle)
        {
            Room newRoom = new Room(CurrentKey, RoomTitle, this);

            // 새로운 클라이언트를 배열에 저장
            RoomDic.TryAdd(CurrentKey, newRoom);

            CurrentKey++;

            return newRoom;
        }

        public void RemoveRoom(Room room)
        {
            room.Close();
            RoomDic.TryRemove(room.key,out _);
        }

        // 시간을 점수판에 등록하고 순위를 반환 ( 순위 밖이면 등록X, -1 반환 )
        public int RegisterRecord(string roomTitle, long time)
        {
            if(timeList.Count == 0)
            {
                timeList.Add(new KeyValuePair<string, long>(roomTitle, time));
                return 1;
            }

            // 등록된 순위
            int rank = -1;

            int i = 0;
            while(i < timeList.Count)
            {
                long s = timeList[i].Value;

                if (s <= time)
                {
                    i++;
                    continue;
                }
                else
                {
                    // 등록할 시간이 더 빨리 클리어했을 경우
                    timeList.Insert(i, new KeyValuePair<string, long>(roomTitle, time));

                    rank = i + 1;
                    break;
                }
                
            }

            if (timeList.Count < 10 && rank == -1)
            {
                timeList.Add(new KeyValuePair<string, long>(roomTitle, time));

                rank = i + 1;
            }

            if (timeList.Count > 10) timeList.RemoveAt(10);

            return rank;
        }

        // 기록 제거
        public void RemoveRecord(int rank)
        {
            int idx = rank - 1;
            if( idx <= timeList.Count - 1 && idx >= 0)
                timeList.RemoveAt(idx);
        }
    }
}
