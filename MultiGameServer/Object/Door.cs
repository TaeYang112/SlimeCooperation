using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer.Object
{
    class Door : GameObject
    {
        public bool isOpen { get; set; }

        public Door(Room room, int key, Point Location, Size size)
            : base(room, key, Location, size)
        {
            _type = ObjectTypes.DOOR;
            Collision = true;
            Blockable = false;
        }

        public Door(Room room, int key, Point Location, Point Location2)
            : this(room, key, Location, new Size(Location2.X - Location.X, Location2.Y - Location.Y))
        {
        }

        public override void OnEvent(EventParam param)
        {
            base.OnEvent(param);
            ClientCharacter clientChar = param.clientCharacter;
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
            // 문이 이미 열렸을 경우
            if (isOpen)
            {
                // 문 안이라면
                if (clientChar.IsEnterDoor == true)
                {
                    // 문밖에 나갈 수 있는지 체크 후 문밖으로 나오게 함
                    bool result = room.CollisionCheck(clientChar, clientChar.Location);

                    if (result == false)
                    {
                        // 문 밖으로 나옴
                        room.EnterDoor(clientChar, false);

                        
                        // 메시지를 만들어서 전송
                        generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(clientChar.key).AddByte(DoorEvent.LEAVE);
                        room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);
                        
                        generator.Clear();
                        
                        // 당사자한테는 킷값을 -1로 보냄
                        generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(-1).AddByte(DoorEvent.LEAVE);
                        Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);
                        
                    }

                }
                // 문 밖이라면
                else
                {
                    // 문 안에 들어감
                    int EnteredCount = room.EnterDoor(clientChar, true);

                    // 메시지를 만들어서 전송
                    generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(clientChar.key).AddByte(DoorEvent.ENTER);
                    room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

                    generator.Clear();
                    
                    // 당사자한테는 킷값을 -1로 보냄
                    generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(-1).AddByte(DoorEvent.ENTER).AddInt(clientChar.key);
                    Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);
                    
                    // 3명이상 들어갔을 경우 다음 맵으로 이동
                    if (EnteredCount >= 3)
                        room.NextGame();

                }

            }
            // 문이 닫혀있을 경우
            else
            {
                // 맵에서 키를 검색함
                KeyObject keyObject = null;
                foreach (var item in room.Map.objectManager.ObjectDic)
                {
                    keyObject = item.Value as KeyObject;

                    if (keyObject != null)
                    {
                        break;
                    }
                }
                if (keyObject == null)
                {
                    return;
                }

                // 클라이언트가 열쇠를 가지고 있다면
                if (clientChar.key == keyObject.ownerKey)
                {
                    // 메시지를 만들어서 전송
                    generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(clientChar.key).AddByte(DoorEvent.OPEN);
                    room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

                    generator.Clear();

                    // 당사자한테는 킷값을 -1로 보냄
                    generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(-1).AddByte(DoorEvent.OPEN);
                    Program.GetInstance().SendMessage(generator.Generate(), clientChar.key);
                    isOpen = true;
                }

            }
        }
    }
}
