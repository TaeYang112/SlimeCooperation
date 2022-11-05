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
    class Stage9 : MapBase
    {
        private List<int> randomSkin;

        private TimerBox timerbox1;
        public Stage9(Room room) : base(room)
        {
        }

        public override void Start()
        {
            base.Start();
            timerbox1.TimerStart();

        }
        protected override void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(0, 740);
            SpawnLocation[1] = new Point(100, 740);
            SpawnLocation[2] = new Point(200, 740);
        }

        protected override void DesignMap()
        {
            InitRandomSkin();
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

            
            // 그냥 돌
            tempKey = room.NextObjKey;
            Platform platform = new Platform(room, tempKey, new Point(400, 720), new Size(70, 70));
            objectManager.AddObject(platform);

            // 타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox = new TimerBox(room, tempKey, new Point(650, 720), new Size(150, 100));
            timerBox.StartTime = 10000;
            this.timerbox1 = timerBox;
            timerBox.SetTimerStopAction(delegate () { room.AllDie(); });
            objectManager.AddObject(timerBox);

            // 버튼
            tempKey = room.NextObjKey;
            Button button = new Button(room, tempKey, new Point(650, 790), new Size(20, 10));
            button.SetAction(delegate () { timerBox.TimerStop(); });
            objectManager.AddObject(button);

            // 누르고 있을때만 작동하는 버튼
            tempKey = room.NextObjKey;
            PressingButton pressButton = new PressingButton(room, tempKey, new Point(1100, 790), new Size(20, 10));
            pressButton.SetAction(delegate (bool bPressed)
            {
                if(bPressed == true)
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(platform.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(platform.Location.X).AddInt(platform.Location.Y);
                    generator.AddInt(platform.size.Width).AddInt(platform.size.Height);
                    generator.AddInt(platform.SkinNum).AddBool(false);
                    generator.AddBool(false).AddBool(false);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
                else
                {
                    MessageGenerator generator = new MessageGenerator(Protocols.S_OBJECT_EVENT);
                    generator.AddInt(platform.key);
                    generator.AddByte(ObjectTypes.GAME_OBJECT);
                    generator.AddInt(-1);
                    generator.AddInt(platform.Location.X).AddInt(platform.Location.Y);
                    generator.AddInt(platform.size.Width).AddInt(platform.size.Height);
                    generator.AddInt(platform.SkinNum).AddBool(true);
                    generator.AddBool(true).AddBool(true);

                    room.SendMessageToAll_InRoom(generator.Generate());
                }
            });
            objectManager.AddObject(pressButton);

            tempKey = room.NextObjKey;
            Lava lava = new Lava(room, tempKey, new Point(800, 300), new Size(96, 408));
            lava.SkinNum = 1;
            objectManager.AddObject(lava);

            // 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);

            
        }

        private void InitRandomSkin()
        {
            randomSkin = new List<int>(new int[] { 0, 0, 0 });
            
            List<int> skinArr = new List<int>();
            int i = 0;
            foreach(var item in room.roomClientDic)
            {
                skinArr.Add(item.Value.SkinNum);
                i++;
            }

            Random rand = new Random(); //랜덤선언

            for(int j = 0; j<2; j++)
            {
                //선언 및 초기화 부분//
                int num = rand.Next(skinArr.Count);

                randomSkin[j] = skinArr[num];

                if(skinArr.Count > 1)
                    skinArr.RemoveAt(num);
            }
            randomSkin[2] = skinArr[0];

        }
    }
}
