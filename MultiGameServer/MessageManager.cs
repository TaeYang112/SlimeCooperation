using MultiGameModule;
using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class MessageManager
    {
        private Program program;

        public MessageManager(Program program)
        {
            this.program = program;
        }

        private byte[] remainedMessage = null;

        // 받은 메시지를 해석함
        public void ParseMessage(ClientCharacter clientChar, byte[] message)
        {
            // 만약 저번 해석때 메시지가 남았더라면 남은 메시지와 이어붙임
            byte[] message2;
            if(remainedMessage == null)
            {
                message2 = message;
            }
            else
            {
                message2 = new byte[remainedMessage.Length + message.Length];
                Array.Copy(remainedMessage, message2, remainedMessage.Length);
                Array.Copy(message, 0, message2, remainedMessage.Length, message.Length);
            }
            
            MessageConverter converter = new MessageConverter(message2);

            while (true)
            {
                bool result = converter.NextMessage();

                // 다음 메시지가 없으면 종료
                if(result == false)
                {
                    remainedMessage = converter.RemainMessage;
                    break;
                }

                byte protocol = converter.Protocol;
                switch (protocol)
                {
                    // 방 만들기 요청
                    case Protocols.REQ_CREATE_ROOM:
                        {
                            ClientCreateRoom(clientChar, converter);
                        }
                        break;
                    // 방 입장 시도
                    case Protocols.REQ_ENTER_ROOM:
                        {
                            ClientEnterRoom(clientChar, converter);
                        }
                        break;
                    // 레디
                    case Protocols.C_READY:
                        {
                            ClientReady(clientChar, converter);
                        }
                        break;
                    // 클라이언트가 로비 정보 요청 / 요청 종료
                    case Protocols.REQ_ROOM_LIST:
                        {
                            ClientReqRoomList(clientChar, converter);
                        }
                        break;
                    // 클라이언트가 로비를 나감
                    case Protocols.C_EXIT_ROOM:
                        {
                            ClientExitRoom(clientChar, converter);
                        }
                        break;
                    // 클라이언트의 좌표를 받아 다른 클라이언트에게 알려줌
                    case Protocols.C_LOCATION:
                        {
                            ClientLocation(clientChar, converter);
                        }
                        break;
                    // 플레이어가 쳐다보는 방향 수신 ( true : 오른쪽 )
                    case Protocols.C_LOOK_DIRECTION:
                        {
                            ClientLookDirection(clientChar, converter);
                        }
                        break;
                    // 클라이언트가 오브젝트와 상호작용함
                    case Protocols.C_OBJECT_EVENT:
                        {
                            ObjectEvent(clientChar, converter);
                        }
                        break;
                    // 클라이언트 키입력
                    case Protocols.C_KEY_INPUT:
                        {
                            ClientKeyInput(clientChar, converter);
                        }
                        break;
                    default:
                        Console.WriteLine("에러");
                        break;

                }
            }
        }




        // 클라이언트 방만들기 요청
        public void ClientCreateRoom(ClientCharacter clientChar, MessageConverter converter)
        {
            // 방 제목
            string RoomTitle = converter.NextString();

            // 방 생성
            Room newRoom = program.roomManager.CreateRoom(RoomTitle);

            // 방 만든사람을 방에 접속시킴
            newRoom.ClientEnter(clientChar);

            clientChar.IsFindingRoom = false;

            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.RES_ADD_ROOM_LIST);
            generator.AddInt(newRoom.key);
            generator.AddString(RoomTitle);
            generator.AddInt(newRoom.GetPeopleCount());

            // 방찾기에 있는 클라이언트한테 방이 새로 생긴걸 알려줌
            foreach (var item in program.clientManager.ClientDic)
            {
                if (item.Value.IsFindingRoom == true)
                {
                    program.SendMessage(generator.Generate(), item.Key);
                }
            }
        }

        // 클라이언트 방 입장 요청
        public void ClientEnterRoom(ClientCharacter clientChar, MessageConverter converter)
        {
            // 방 키
            int roomKey = converter.NextInt();

            // 방키로 방을 찾음
            Room room;
            bool result = program.roomManager.RoomDic.TryGetValue(roomKey, out room);

            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_ERROR);

            // 방을 찾지 못할경우
            if (result == false)
            {
                // 방이 없다고 에러 보냄
                generator.AddInt(1);
                program.SendMessage(generator.Generate(), clientChar.key);
                return;
            }

            // 인원수가 3명 이상일경우
            if (room.GetPeopleCount() >= 3)
            {
                // 방이 꽉찼다고 에러 보냄
                generator.AddInt(0);
                program.SendMessage(generator.Generate(), clientChar.key);
                return;
            }

            room.ClientEnter(clientChar);

            clientChar.IsFindingRoom = false;

            // 인원수가 바뀐것을 클라이언트들에게 알려줌
            program.SendUpdateRoomInfo(room);
        }

        public void ClientReady(ClientCharacter clientChar, MessageConverter converter)
        {
            // 레디 여부를 대입
            bool bReady = converter.NextBool();
            clientChar.IsReady = bReady;


            // 클라이언트가 들어있는 방을 찾음
            Room room = clientChar.room;

            // 존재하지 않으면 return
            if (room == null) return;

            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_READY_OTHER);
            generator.AddInt(clientChar.key);
            generator.AddBool(bReady);

            // 방안의 다른 클라이언트한테 레디했다고 알려줌
            room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

            if (bReady == true)
            {
                // 3명 이상의 클라이언트가 레디했다면
                if (room.IsAllReady() == true)
                {
                    // 게임시작
                    room.GameStart(1);

                    // 방찾기 중인 클라이언트들의 방목록에서 시작한 방을 제거
                    program.SendDelRoomInfo(room);
                }
            }
        }

        public void ClientReqRoomList(ClientCharacter clientChar, MessageConverter converter)
        {
            clientChar.IsFindingRoom = converter.NextBool();

            MessageGenerator generator = new MessageGenerator(Protocols.RES_ADD_ROOM_LIST);

            // 정보 요청이 true라면 원래 있던 방목록을 전달함
            if (clientChar.IsFindingRoom == true)
            {
                foreach (var item in program.roomManager.RoomDic)
                {
                    Room room = item.Value;
                    if (item.Value.bGameStart == false)
                    {
                        generator.AddInt(room.key);
                        generator.AddString(room.RoomTitle);
                        generator.AddInt(room.GetPeopleCount());

                        program.SendMessage(generator.Generate(), clientChar.key);
                    }
                }
            }
        }

        public void ClientExitRoom(ClientCharacter clientChar, MessageConverter converter)
        {
            // 클라이언트가 속해있는 방을 가져옴
            Room room = clientChar.room;

            // 방이 존재하지 않으면 종료
            if (room == null) return;


            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_EXIT_ROOM_OTHER);
            generator.AddInt(clientChar.key);

            // 다른 플레이어들한테 알려줌
            room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

            // 룸의 클라이언트 배열에서도 제거
            room.ClientLeave(clientChar);

            // 만약 클라이언트가 나감으로서 아무도 방에 남지않을경우 방을 제거
            if (room.GetPeopleCount() == 0)
            {
                program.roomManager.RemoveRoom(room);

                // 방찾기 중인 클라이언트한테 알려줌
                program.SendDelRoomInfo(room);
            }
            else
            {
                // 아니면 클라이언트들에게 인원수가 바뀐것을 알려줌
                program.SendUpdateRoomInfo(room);
            }
        }

        public void ClientLocation(ClientCharacter clientChar, MessageConverter converter)
        {
            int x = converter.NextInt();
            int y = converter.NextInt();

            // 클라이언트가 속해있는 방을 가져옴
            Room room = clientChar.room;

            // 방이 존재하지 않으면 종료
            if (room == null) return;

            int dx = x - clientChar.Location.X;
            int dy = y - clientChar.Location.Y;

            int MoveNum = converter.NextInt();

            // 클라이언트에게 이동 명령을 보낸뒤 다시 MoveNum이 돌아오기 전까지의 이동을 무시함
            if(MoveNum == clientChar.MoveNum)
            {
                Console.WriteLine("무브넘" + MoveNum);
                clientChar.IgnoreLocation = false;
                clientChar.MoveNum++;
            }
            else
            {
                if (clientChar.IgnoreLocation == true) return;
            }

            // 충돌체크 후 이동할 수 있는 좌표 가능
            Point point = room.CharacterLocationValidCheck(new Point(dx, dy), clientChar);

            clientChar.Location = new Point(point.X, point.Y);

            if (point.X != x || point.Y != y)
            {
                // 클라이언트에게 좌표를 조정하라고 알림
                MessageGenerator generator3 = new MessageGenerator(Protocols.S_MOVE);
                generator3.AddInt(point.X - x );
                generator3.AddInt(point.Y - y);
                generator3.AddInt(clientChar.MoveNum);
                Program.GetInstance().SendMessage(generator3.Generate(), clientChar.key);

                clientChar.IgnoreLocation = true;
            }



            // 다른 클라이언트들에게 이 클라이언트가 움직였다는거를 알려줌
            MessageGenerator generator2 = new MessageGenerator(Protocols.S_LOCATION_OTHER);
            generator2.AddInt(clientChar.key);
            generator2.AddInt(point.X);
            generator2.AddInt(point.Y);

            // 전체 클라이언트에게 이동한 좌표 전송
            room.SendMessageToAll_InRoom(generator2.Generate(), clientChar.key);
        }

       

        public void ClientLookDirection(ClientCharacter clientChar, MessageConverter converter)
        {
            bool bLookRight = converter.NextBool();

            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_LOOK_DIRECTION_OTHER);
            generator.AddInt(clientChar.key);
            generator.AddBool(bLookRight);

            // 서버로 전송
            clientChar.room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);
        }


        public void ObjectEvent(ClientCharacter clientChar, MessageConverter converter)
        {
            int key = converter.NextInt();
            byte type = converter.NextByte();

            // 클라이언트가 속해있는 방을 가져옴
            Room room = clientChar.room;

            // 방이 존재하지 않으면 종료
            if (room == null) return;

            // 해당 오브젝트를 찾음
            GameObject gameObject;
            bool objResult = room.Map.objectManager.ObjectDic.TryGetValue(key, out gameObject);

            if (objResult == false) return;


            // 메시지 생성
            MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);

            switch (type)
            {
                case ObjectTypes.KEY_OBJECT:
                    {
                        KeyObject keyObj = gameObject as KeyObject;

                        
                        if (keyObj == null) break;

                        // 소유자가 있으면 무시
                        if (keyObj.ownerKey != -1) break;
                        else
                        {
                            keyObj.ownerKey = clientChar.key;

                            // 메시지를 만들어서 전송
                            generator.AddInt(key).AddByte(ObjectTypes.KEY_OBJECT).AddInt(clientChar.key);
                            room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);


                            generator.Clear();

                            // 당사자한테는 킷값을 -1로 보냄
                            generator.AddInt(key).AddByte(ObjectTypes.KEY_OBJECT).AddInt(-1);
                            program.SendMessage(generator.Generate(), clientChar.key);
                        }
                    }
                    break;
                case ObjectTypes.DOOR:
                    {
                        Door door = gameObject as Door;

                        if (door == null) break;

                        // 문이 이미 열렸을 경우
                        if (door.isOpen)
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
                                    program.SendMessage(generator.Generate(), clientChar.key);

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
                                generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(-1).AddByte(DoorEvent.ENTER);
                                program.SendMessage(generator.Generate(), clientChar.key);

                                // 3명이상 들어갔을 경우 다음 맵으로 이동
                                if (EnteredCount >= 3)
                                    room.NextGame();

                            }

                        }
                        // 문이 닫혀있을 경우
                        {
                            // 맵에서 키를 검색함
                            KeyObject keyObject = null;
                            foreach (var item in room.Map.objectManager.ObjectDic)
                            {
                                keyObject = item.Value as KeyObject;

                                if (keyObject != null) break;
                            }
                            if (keyObject == null) break;

                            // 클라이언트가 열쇠를 가지고 있다면
                            if (clientChar.key == keyObject.ownerKey)
                            {
                                // 메시지를 만들어서 전송
                                generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(clientChar.key).AddByte(DoorEvent.OPEN);
                                room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

                                generator.Clear();

                                // 당사자한테는 킷값을 -1로 보냄
                                generator.AddInt(key).AddByte(ObjectTypes.DOOR).AddInt(-1).AddByte(DoorEvent.OPEN);
                                program.SendMessage(generator.Generate(), clientChar.key);
                                door.isOpen = true;
                            }

                        }
                    }
                    break;
                case ObjectTypes.STONE:
                    {
                        Stone stone = gameObject as Stone;

                        if (stone == null) break;

                        stone.OnEvent();
                    }
                    break;
                case ObjectTypes.BUTTON:
                    {
                        MultiGameServer.Object.Button button = gameObject as Button;

                        if (button == null) break;

                        button.OnEvent();
                    }
                    break;
                default:
                    break;
            }
        }
        
        public void ClientKeyInput(ClientCharacter clientChar, MessageConverter converter)
        {
            byte keyType = converter.NextByte();
            bool bPress = converter.NextBool();

            if (keyType == Keyboards.RIGHT_ARROW)
            {
                clientChar.bRightPress = bPress;
            }
            else
            {
                clientChar.bLeftPress = bPress;
            }
        }
       

    }
}
