using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using MultiGameModule;

namespace MultiGameServer
{
    class Stage4 : MapBase
    {
        private List<GameObject> oddPlatform = new List<GameObject>();
        private List<GameObject> evenPlatform = new List<GameObject>();
        private bool lastButtonPressed = false;
        public Stage4(Room room) : base(room)
        {

        }

        protected override void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(0, 740);
            SpawnLocation[1] = new Point(100, 740);
            SpawnLocation[2] = new Point(200, 740);
        }

        public override void Start()
        {
            base.Start();
            SwitchEvenPlatform(false);
            SwitchOddPlatform(false);
        }

        protected override void DesignMap()
        {
            int tempKey;

            // 맵 바깥 벽 (왼)
            tempKey = room.NextObjKey;
            Floor leftWall = new Floor(room, tempKey, new Point(-11, 0), new Size(10, 865));
            leftWall.SkinNum = -1;
            objectManager.AddObject(leftWall);

            // 맵 바깥 벽 (우)
            tempKey = room.NextObjKey;
            Floor rightWall = new Floor(room, tempKey, new Point(1424, 0), new Size(10, 865));
            rightWall.SkinNum = -1;
            objectManager.AddObject(rightWall);

            //아래 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 810), new Point(720, 865)); // 1440 865
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);
            //위 땅
            tempKey = room.NextObjKey;
            Floor Floor6 = new Floor(room, tempKey, new Point(0, 450), new Point(1155, 505)); // 1440 865
            Floor6.SkinNum = 2;
            objectManager.AddObject(Floor6);


            //우측 땅1 점프 68
            tempKey = room.NextObjKey;
            Floor Floor8 = new Floor(room, tempKey, new Point(1325, 493), new Point(1440, 900)); // 1440 865
            Floor8.SkinNum = 4;
            objectManager.AddObject(Floor8);
            // 우측 땅1 점프 68
            tempKey = room.NextObjKey;
            Floor Floor2 = new Floor(room, tempKey, new Point(1250, 560), new Point(1440, 900)); // 1440 865
            Floor2.SkinNum = 4;
            objectManager.AddObject(Floor2);
            // 우측 땅2
            tempKey = room.NextObjKey;
            Floor Floor3 = new Floor(room, tempKey, new Point(1100, 625), new Point(1250, 900)); // 1440 865
            Floor3.SkinNum = 4;
            objectManager.AddObject(Floor3);
            // 우측 땅3
            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(950, 688), new Point(1100, 900)); // 1440 865
            Floor4.SkinNum = 5;
            objectManager.AddObject(Floor4);
            // 우측 땅밑
            tempKey = room.NextObjKey;
            Floor Floor5 = new Floor(room, tempKey, new Point(608, 1057), new Point(1250, 1058)); // 1440 865
            Floor5.SkinNum = 2;
            objectManager.AddObject(Floor5);

            // 돌1
            tempKey = room.NextObjKey;
            Stone stone1 = new Stone(room, tempKey, new Point(425, 508), new Size(100, 300));
            stone1.weight = 3;
            objectManager.AddObject(stone1);

            // 가장 왼쪽 없어지는 발판
            tempKey = room.NextObjKey;
            Platform platform1 = new Platform(room, tempKey, new Point(82, 383), new Size(125, 30));
            objectManager.AddObject(platform1);
            oddPlatform.Add(platform1);

            // 두번째 없어지는 발판
            tempKey = room.NextObjKey;
            Platform platform2 = new Platform(room, tempKey, new Point(320, 320), new Size(125, 30));
            objectManager.AddObject(platform2);
            evenPlatform.Add(platform2);

            // 세번째 없어지는 발판
            tempKey = room.NextObjKey;
            Platform platform3 = new Platform(room, tempKey, new Point(560, 260), new Size(125, 30));
            objectManager.AddObject(platform3);
            oddPlatform.Add(platform3);

            // 네번째 없어지는 발판
            tempKey = room.NextObjKey;
            Platform platform4 = new Platform(room, tempKey, new Point(800, 200), new Size(125, 30));
            objectManager.AddObject(platform4);
            evenPlatform.Add(platform4);

            // 열쇠 발판
            tempKey = room.NextObjKey;
            Floor Floor7 = new Floor(room, tempKey, new Point(553, 111), new Size(125, 30));
            objectManager.AddObject(Floor7);

            // 문 밑에 발판
            tempKey = room.NextObjKey;
            Floor Floor9 = new Floor(room, tempKey, new Point(1040, 140), new Size(125, 30));
            objectManager.AddObject(Floor9);


            // 제일 왼쪽 버튼
            tempKey = room.NextObjKey;
            PressingButton button1 = new PressingButton(room, tempKey, new Point(55, 440), new Size(20, 10));
            button1.SetAction(delegate (bool bPressed) { 
                if(lastButtonPressed == false)
                    SwitchOddPlatform(bPressed);
            });
            objectManager.AddObject(button1);

            // 가운데 버튼
            tempKey = room.NextObjKey;
            PressingButton button2 = new PressingButton(room, tempKey, new Point(350, 440), new Size(20, 10));
            button2.SetAction(delegate (bool bPressed) { 
                if(lastButtonPressed == false)
                    SwitchEvenPlatform(bPressed);
            });
            objectManager.AddObject(button2);

            // 제일 오른쪽 버튼
            tempKey = room.NextObjKey;
            PressingButton button3 = new PressingButton(room, tempKey, new Point(1050, 130), new Size(20, 10));
            button3.SetAction(delegate (bool bPressed) {
                lastButtonPressed = bPressed;
                SwitchEvenPlatform(bPressed);
                SwitchOddPlatform(bPressed);
            });
            objectManager.AddObject(button3);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(1095, 50), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(570, 50), new Size(35, 50));
            objectManager.AddObject(KeyObject);
        }

        private void SwitchOddPlatform(bool flag)
        {
            if (flag == false)
            {
                foreach (var obj in oddPlatform)
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(obj.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(obj.Location.X).AddInt(obj.Location.Y);
                    generator.AddInt(obj.size.Width).AddInt(obj.size.Height);
                    generator.AddInt(obj.SkinNum).AddBool(false);
                    generator.AddBool(false).AddBool(false);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
            }
            else
            {
                foreach (var obj in oddPlatform)
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(obj.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(obj.Location.X).AddInt(obj.Location.Y);
                    generator.AddInt(obj.size.Width).AddInt(obj.size.Height);
                    generator.AddInt(obj.SkinNum).AddBool(true);
                    generator.AddBool(true).AddBool(true);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
            }
        }

        private void SwitchEvenPlatform(bool flag)
        {
            if (flag == false)
            {
                foreach (var obj in evenPlatform)
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(obj.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(obj.Location.X).AddInt(obj.Location.Y);
                    generator.AddInt(obj.size.Width).AddInt(obj.size.Height);
                    generator.AddInt(obj.SkinNum).AddBool(false);
                    generator.AddBool(false).AddBool(false);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
            }
            else
            {
                foreach (var obj in evenPlatform)
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(obj.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(obj.Location.X).AddInt(obj.Location.Y);
                    generator.AddInt(obj.size.Width).AddInt(obj.size.Height);
                    generator.AddInt(obj.SkinNum).AddBool(true);
                    generator.AddBool(true).AddBool(true);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
            }
        }
    }

    
}
