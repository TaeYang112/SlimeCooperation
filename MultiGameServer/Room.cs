using MultiGameServer.Object;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
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

        private MapBase _Map;
        public MapBase Map { get { return _Map; } }

        public string RoomTitle { get; set; }

        public bool bGameStart { get; set; }

        public int EnteredCount { get; set; }

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

        public void Close()
        {
            Map.objectManager.ClearObjects();
            roomClientDic.Clear();
        }

        // 방에 클라이언트를 추가함
        public void ClientEnter(ClientCharacter clientChar)
        {
            // 클라이언트가 속한 방을 설정
            clientChar.room = this;

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
            clientChar.room = null;
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
                if (item.Value.IsReady == true)
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

        // 문에 들어감, 문에 들어간 캐릭터 수 반환
        public int EnterDoor(ClientCharacter clientChar, bool flag)
        {
            // 문에 들어감
            if (flag == true)
            {
                if (clientChar.IsEnterDoor == false)
                {
                    EnteredCount++;
                    clientChar.Collision = false;
                    clientChar.IsEnterDoor = true;

                    
                }

            }
            // 문에서 나옴
            else
            {
                if (clientChar.IsEnterDoor == true)
                {
                    EnteredCount--;
                    clientChar.Collision = true;
                    clientChar.IsEnterDoor = false;

                    
                }
            }

            return EnteredCount;
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

        // 대상 클라이언트 머리위에 있는 클라이언트 리스트 반환
        public List<ClientCharacter> GetClientsOverTheHead(ClientCharacter client)
        {
            List<ClientCharacter> list = new List<ClientCharacter>();

            // 대상의 머리위 충돌박스
            Size size = new Size(client.size.Width-4, 1);
            Point location = new Point(client.Location.X+2, client.Location.Y - 1);
            Rectangle a = new Rectangle(location, size);

            // 모든 클라이언트와 비교
            foreach (var item in roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (otherClient == client || otherClient.Collision == false)
                {
                    continue;
                }

                // 대상 충돌판정
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약  겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    list.Add(otherClient);
                }
            }

            return list;
        }

        // 대상 클라이언트 발 아래에 있는 클라이언트 리스트 반환
        public List<ClientCharacter> GetClientsUnderTheFoot(ClientCharacter client)
        {
            List<ClientCharacter> list = new List<ClientCharacter>();

            // 대상의 발아래 충돌박스
            Size size = new Size(client.size.Width - 4, 1);
            Point location = new Point(client.Location.X + 2, client.Location.Y + client.size.Height + 1);
            Rectangle a = new Rectangle(location, size);

            // 모든 클라이언트와 비교
            foreach (var item in roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (otherClient == client || otherClient.Collision == false)
                {
                    continue;
                }

                // 대상 충돌판정
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약  겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    list.Add(otherClient);
                }
            }

            return list;
        }

        // 겹치면 true 반환
        public bool CollisionCheck(GameObject target, Point newLocation)
        {
            // 대상의 충돌 박스
            Rectangle a = new Rectangle(newLocation, target.size);

            // 모든 캐릭터와 부딪히는지 체크함
            foreach (var item in roomClientDic)
            {
                ClientCharacter otherClient = item.Value;

                if (otherClient == target) continue;

                // 충돌이 꺼져있으면 무시
                if (otherClient.Collision == false) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(otherClient.Location, otherClient.size);

                // 만약 움직였을때 겹친다면 리턴
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    return true;
                }
            }
            // 맵의 모든 오브젝트와 부딪히는지 체크함
            foreach (var item in Map.objectManager.ObjectDic)
            {
                GameObject gameObject = item.Value;

                if (gameObject == target) continue;

                if (gameObject.Collision == false) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(gameObject.Location, gameObject.size);
                // 만약 움직였을때 겹친다면 충돌 발생
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    // 해당 오브젝트가 길을 막을 수 있으면 true반환하여 이동 제한
                    if (gameObject.Blockable == true) return true;
                    else continue;
                }
            }
            
            return false;
        }

        public void GameStart()
        {
            if (Map != null)
                Map.objectManager.ClearObjects();
            
            _Map = new Stage1(this);
            bGameStart = true;

            EnteredCount = 0;

            // 프로그램 인스턴스
            Program PInst = Program.GetInstance();

            int X = 0;
            foreach (var item in roomClientDic)
            {
                // 내부적으로 각 클라이언트 시작 위치 설정
                item.Value.Location = new Point(X, 330);
                X += 100;

                // 클라이언트에게 게임 시작을 알려주고 시작 위치 설정
                PInst.SendMessage($"MapStart#{item.Value.Location.X}#{item.Value.Location.Y}@", item.Key);

                // 클라이언트들의 시작 위치를 알려줌
                foreach (var item2 in roomClientDic)
                {
                    if (item.Key == item2.Key) continue;

                    // 각 플레이어들의 위치를 전송
                    else
                        PInst.SendMessage($"Location#{item2.Key}#{item2.Value.Location.X}#{item2.Value.Location.Y}#@", item.Key);
                }

                // 맵에 있는 오브젝트들을 알려줌
                foreach(var objectPair in Map.objectManager.ObjectDic)
                {
                    GameObject gameObject = objectPair.Value;

                    if(gameObject.Type == "Stone")
                    {
                        Stone stone = gameObject as Stone;
                        
                        PInst.SendMessage($"NewObject#" +
                        $"{objectPair.Key}#{gameObject.Type}#" +
                        $"{gameObject.Location.X}#{gameObject.Location.Y}#" +
                        $"{gameObject.size.Width}#{gameObject.size.Height}#" +
                        $"{gameObject.SkinNum}#{stone.weight}@", item.Key);
                    }
                    else
                    {
                        PInst.SendMessage($"NewObject#" +
                        $"{objectPair.Key}#{gameObject.Type}#" +
                        $"{gameObject.Location.X}#{gameObject.Location.Y}#" +
                        $"{gameObject.size.Width}#{gameObject.size.Height}#" +
                        $"{gameObject.SkinNum}@", item.Key);
                    }
                    
                }
                item.Value.IsEnterDoor = false;
                item.Value.Blockable = true;
                item.Value.Collision = true;
                item.Value.GameStart();
            }

        }

        // 방 안의 모든 클라이언트들에게 메세지 전송 ( senderKey로 예외 클라이언트 설정 )
        public void SendMessageToAll_InRoom(string message, int senderKey = -1)
        {
            foreach (var item in roomClientDic)
            {
                if (item.Value.key == senderKey) continue;

                Program.GetInstance().SendMessage(message, item.Value.key);
            }

        }
    }
}
