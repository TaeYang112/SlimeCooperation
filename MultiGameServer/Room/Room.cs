using MultiGameModule;
using MultiGameServer.Object;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class Room
    {
        public int key { get; }

        private int _nextObjKey = 0;
        public int NextObjKey { get { return _nextObjKey++; } }
        public ConcurrentDictionary<int, ClientCharacter> roomClientDic { get; }
        public SortedSet<int> skinList;

        private MapBase _Map;
        public MapBase Map { get { return _Map; } }

        public string RoomTitle { get; set; }

        public bool bGameStart { get; set; }

        // 문안에 들어간 인원수
        public int EnteredCount { get; set; }

        // 재시작 요청한 인원수
        public int RestartPressedCount { get; set; }

        // 현재 몇 스테이지
        public int stageNum { get; set; }

        public bool IsAllDie { get; set; }

        // 죽고 부활 타이머
        private System.Threading.Timer RespawnTimer;

        public Room(int key, string RoomTitle)
        {
            this.key = key;
            this.RoomTitle = RoomTitle;
            roomClientDic = new ConcurrentDictionary<int, ClientCharacter>();
            bGameStart = false;
            stageNum = 1;
            RestartPressedCount = 0;

            skinList = new SortedSet<int>();
            for (int i = 0; i < 8; i++)
            {
                skinList.Add(i);
            }
            IsAllDie = false;

            TimerCallback tc = new TimerCallback(Respawn);
            RespawnTimer = new System.Threading.Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Close()
        {
            if (Map != null)
            {
                Map.Close();
            }
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



            // 방 입장을 클라이언트한테 알림
            MessageGenerator generator = new MessageGenerator(Protocols.RES_ENTER_ROOM);
            generator.AddInt(key).AddString(RoomTitle).AddInt(clientChar.SkinNum);

            Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);



            // 접속한 클라이언트에게 방에 원래 있던 클라이언트들 정보를 알려줌
            generator.Protocol = Protocols.S_ENTER_ROOM_OTHER;
            foreach (var item in roomClientDic)
            {
                if (item.Key == clientChar.key) continue;

                generator.Clear();
                generator.AddInt(item.Key);
                generator.AddBool(item.Value.IsReady);
                generator.AddInt(item.Value.SkinNum);

                Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);
            }


            // 기존 클라이언트들에게 새로 접속한 클라이언트를 알려줌
            generator.Clear();
            generator.AddInt(clientChar.key);
            generator.AddBool(clientChar.IsReady);
            generator.AddInt(clientChar.SkinNum);

            SendMessageToAll_InRoom(generator.Generate(), clientChar.key);
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

        public void ClientRestartPress(ClientCharacter clientChar)
        {
            if(clientChar.RestarPressed == false)
            {
                clientChar.RestarPressed = true;
                RestartPressedCount++;
                
                if(RestartPressedCount >= 3)
                {
                    GameStart(stageNum);
                }
            }
            else
            {
                clientChar.RestarPressed = false;
                RestartPressedCount--;
            }
            Console.WriteLine(key + "번방 다시시작 : " + RestartPressedCount + "/" + 3);
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
            if (count >= 1)
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

        public void AllDie()
        {
            if (IsAllDie == false)
            {
                IsAllDie = true;
                MessageGenerator generator = new MessageGenerator(Protocols.S_ALLDIE);

                SendMessageToAll_InRoom(generator.Generate());

                RespawnTimer.Change(1500, Timeout.Infinite);
            }
        }

        private void Respawn(object o)
        {
            GameStart(stageNum);
            IsAllDie = false;
        }

        public Point CharacterLocationValidCheck(Point velocity, ClientCharacter character)
        {
            Point resultLoc = character.Location;
            Point tempLoc;
            int dxy = 0;

            // x의 대한 충돌판정
            if (velocity.X != 0)
            {
                tempLoc = new Point(resultLoc.X + velocity.X, resultLoc.Y);

                if (velocity.X < 0) dxy = 1;
                else dxy = -1;

                while (true)
                {
                    if (CharacterCollisionCheck(character, tempLoc) == false)
                    {
                        resultLoc = tempLoc;
                        break;
                    }
                    tempLoc.X += dxy;
                }
            }

            // y에 대한 충돌 판정
            if(velocity.Y != 0)
            {
                tempLoc = new Point(resultLoc.X, resultLoc.Y + velocity.Y);

                if (velocity.Y < 0) dxy = 1;
                else dxy = -1;

                while (true)
                {
                    if (CharacterCollisionCheck(character, tempLoc) == false)
                    {
                        resultLoc = tempLoc;
                        break;
                    }
                    tempLoc.Y += dxy;
                }
            }
            


            return resultLoc;
        }

        // 겹치면 true 반환
        public bool CharacterCollisionCheck(GameObject target, Point newLocation)
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
                if (gameObject.Collision == false || gameObject.IsStatic == true) continue;

                // 대상 오브젝트의 충돌 박스
                Rectangle b = new Rectangle(gameObject.Location, gameObject.size);
                // 만약 움직였을때 겹친다면 충돌 발생
                if (Rectangle.Intersect(a, b).IsEmpty == false)
                {
                    // 해당 오브젝트가 길을 막을 수 있으면 true반환하여 이동 제한
                    if (gameObject.Blockable == true)
                    {
                        return true;
                    }
                    else continue;
                }
            }

            return false;
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

        public void GameStart(int stageNum)
        {
            this.stageNum = stageNum;
            this.RestartPressedCount = 0;

            if (Map != null)
                Map.objectManager.ClearObjects();
            

            switch(stageNum)
            {
                case 1:
                    _Map = new Stage1(this);
                    break;
                case 2:
                    _Map = new Stage2(this);
                    break;
                case 3:
                    _Map = new Stage3(this);
                    break;
                case 5:
                    _Map = new Stage5(this);
                    break;
                case 9:
                    _Map = new Stage9(this);
                    break;
                case 10:
                    _Map = new Stage10(this);
                    break;
                default:
                    _Map = new Stage1(this);
                    break;
            }
            
            bGameStart = true;

            EnteredCount = 0;

            // 프로그램 인스턴스
            Program PInst = Program.GetInstance();

            // 메시지 생성기
            MessageGenerator generator = new MessageGenerator();

            int num = 0;
            foreach (var item in roomClientDic)
            {
                // 내부적으로 각 클라이언트 시작 위치 설정
                item.Value.Location = Map.GetSpawnLocation(num++);
                item.Value.RestarPressed = false;

                // 맵 시작 메시지 생성
                generator.Clear();
                generator.Protocol = Protocols.S_MAP_START;
                generator.AddInt(item.Value.Location.X);
                generator.AddInt(item.Value.Location.Y);

                // 전송
                PInst.SendMessage(generator.Generate(), item.Key);

                // 클라이언트들의 시작 위치를 알려줌
                foreach (var item2 in roomClientDic)
                {
                    if (item.Key == item2.Key) continue;

                    // 각 플레이어들의 위치를 전송
                    else
                    {
                        // 위치 메시지 생성
                        generator.Clear();
                        generator.Protocol = Protocols.S_LOCATION_OTHER;
                        generator.AddInt(item2.Key);
                        generator.AddInt(item2.Value.Location.X);
                        generator.AddInt(item2.Value.Location.Y);

                        // 전송
                        PInst.SendMessage(generator.Generate(), item.Key);
                    }
                        
                }

                generator.Clear();
                generator.Protocol = Protocols.S_NEW_OBJECT;

                // 맵에 있는 오브젝트들을 알려줌
                foreach(var objectPair in Map.objectManager.ObjectDic)
                {
                    GameObject gameObject = objectPair.Value;

                    generator.Clear();
                    generator.AddInt(objectPair.Key);
                    generator.AddByte(gameObject.Type);
                    generator.AddInt(gameObject.Location.X).AddInt(gameObject.Location.Y);
                    generator.AddInt(gameObject.size.Width).AddInt(gameObject.size.Height);
                    generator.AddInt(gameObject.SkinNum);

                    switch (gameObject.Type)
                    {
                        // 돌 오브젝트는 자신의 무게를 메시지에 추가해서 보냄
                        case ObjectTypes.STONE:
                            {
                                Stone stone = gameObject as Stone;

                                generator.AddInt(stone.weight);
                            }
                            break;
                        case ObjectTypes.TIMER_BOARD:
                            {
                                TimerBoard timeBoard = gameObject as TimerBoard;

                                generator.AddInt(timeBoard.MinTime);
                                generator.AddInt(timeBoard.MaxTime);
                                generator.AddInt(timeBoard.StartTime);
                                generator.AddInt(timeBoard.TimerCount);
                            }
                            break;
                        default:
                            break;
                    }

                    // 서버로 전송
                    PInst.SendMessage(generator.Generate(), item.Key);
                }

                item.Value.IsEnterDoor = false;
                item.Value.Blockable = true;
                item.Value.Collision = true;
                item.Value.GameStart();
            }
            _Map.Start();

        }

        public void NextGame()
        {
            GameStart(++stageNum);
        }

        // 방 안의 모든 클라이언트들에게 메시지 전송 ( senderKey로 예외 클라이언트 설정 )
        public void SendMessageToAll_InRoom(byte[] message, int senderKey = -1)
        {
            foreach (var item in roomClientDic)
            {
                if (item.Value.key == senderKey) continue;

                Program.GetInstance().SendMessage(message, item.Value.key);
            }

        }
    }
}
