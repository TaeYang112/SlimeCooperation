using MultiGameModule;
using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
                    room.GameStart();
                    room.MapChange(1);

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
                    if (item.Value.IsGameStart == false)
                    {
                        generator.AddInt(room.key);
                        generator.AddString(room.RoomTitle);
                        generator.AddInt(room.GetPeopleCount());

                        program.SendMessage(generator.Generate(), clientChar.key);
                        generator.Clear();
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

            int MoveNum = converter.NextInt();
            bool isTeleport = converter.NextBool();

            // 서버에서 클라이언트의 위치를 옮겼을 때 동기화를 위해 번호를 맞춤
            if (MoveNum != clientChar.MoveNum)
            {
                return;
            }

            Point point = new Point(x,y);

            // 텔레포트가 라면
            if (isTeleport == true)
            {
                clientChar.Location = point;
            }
            else
            {
                int dx = x - clientChar.Location.X;
                int dy = y - clientChar.Location.Y;

                // 충돌체크 후 이동할 수 있는 좌표 가능
                point = room.CharacterLocationValidCheck(new Point(dx, dy), clientChar);

                clientChar.Location = new Point(point.X, point.Y);

                // 만약 클라이언트가 가려는 좌표에 다른 무언가가 있다면
                // 클라이언트에게 좌표를 조정하라고 명령
                if (point.X - x != 0 || point.Y - y != 0)
                {
                    clientChar.MoveNum++;
                    clientChar.Location = point;

                    MessageGenerator generator3 = new MessageGenerator(Protocols.S_MOVE);
                    generator3.AddInt(point.X - x);
                    generator3.AddInt(point.Y - y);
                    generator3.AddInt(clientChar.MoveNum);
                    Program.GetInstance().SendMessage(generator3.Generate(), clientChar.key);
                }
            }
           
            
            

            // 다른 클라이언트들에게 이 클라이언트가 움직였다는거를 알려줌
            MessageGenerator generator2 = new MessageGenerator(Protocols.S_LOCATION_OTHER);
            generator2.AddInt(clientChar.key);
            generator2.AddInt(clientChar.Location.X);
            generator2.AddInt(clientChar.Location.Y);

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

            GameObject.EventParam param = new GameObject.EventParam(clientChar);

 
            gameObject.OnEvent(param);

        }
        
        public void ClientKeyInput(ClientCharacter clientChar, MessageConverter converter)
        {
            byte keyType = converter.NextByte();
            bool bPress = converter.NextBool();

            switch(keyType)
            {
                case Keyboards.RIGHT_ARROW:
                    clientChar.bRightPress = bPress;
                    break;
                case Keyboards.LEFT_ARROW:
                    clientChar.bLeftPress = bPress;
                    break;
                case Keyboards.RESTART:
                    {
                        Room room = clientChar.room;
                        room.ClientRestartPress(clientChar);

                        MessageGenerator generator = new MessageGenerator(Protocols.S_RESTART_OTHER);
                        generator.AddInt(clientChar.key);
                        generator.AddBool(clientChar.RestarPressed);

                        room.SendMessageToAll_InRoom(generator.Generate(), clientChar.key);

                        generator.Clear();
                        generator.AddInt(-1);
                        generator.AddBool(clientChar.RestarPressed);

                        program.SendMessage(generator.Generate(), clientChar.key);
                    }
                    break;
            }
        }
       

    }
}
