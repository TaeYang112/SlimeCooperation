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
    class Stage7 : MapBase
    {
        private List<int> randomSkin;
        private TimerBox timerbox1;
        public Stage7(Room room) : base(room)
        {
        }
        protected override void SetSpawnLocation() // 플레이어 시작 좌표
        {
            SpawnLocation[0] = new Point(545, 740);
            SpawnLocation[1] = new Point(720, 740);
            SpawnLocation[2] = new Point(895, 740);
        }
        public override void Start()
        {
            base.Start();
            timerbox1.TimerStart();
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

            // 라바
            tempKey = room.NextObjKey;
            Lava lava = new Lava(room, tempKey, new Point(0, 861), new Point(1440, 864));
            lava.SkinNum = -1;
            objectManager.AddObject(lava);

            // 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(545, 800), new Point(895, 865));
            Floor.SkinNum = 0;
            objectManager.AddObject(Floor);

            // 문 땅
            tempKey = room.NextObjKey;
            Floor Floor1 = new Floor(room, tempKey, new Point(1285, 800), new Point(1440, 865));
            Floor1.SkinNum = 0;
            objectManager.AddObject(Floor1);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(1350, 710), new Size(70, 90));
            objectManager.AddObject(door);

            // 벽1
            tempKey = room.NextObjKey;
            Platform platform1 = new Platform(room, tempKey, new Point(410, 740), new Size(125, 30));
            objectManager.AddObject(platform1);

            // 벽2
            tempKey = room.NextObjKey;
            Platform platform2 = new Platform(room, tempKey, new Point(180, 675), new Size(125, 30));
            objectManager.AddObject(platform2);

            // 벽3
            tempKey = room.NextObjKey;
            Platform platform3 = new Platform(room, tempKey, new Point(0, 610), new Size(125, 30));
            objectManager.AddObject(platform3);

            // 벽4
            tempKey = room.NextObjKey;
            Platform platform4 = new Platform(room, tempKey, new Point(250, 545), new Size(125, 30));
            objectManager.AddObject(platform4);

            // 벽5
            tempKey = room.NextObjKey;
            Platform platform5 = new Platform(room, tempKey, new Point(475, 480), new Size(125, 30));
            objectManager.AddObject(platform5);

            // 벽6
            tempKey = room.NextObjKey;
            Platform platform6 = new Platform(room, tempKey, new Point(30, 480), new Size(125, 30));
            objectManager.AddObject(platform6);

            // 벽7
            tempKey = room.NextObjKey;
            Platform platform7 = new Platform(room, tempKey, new Point(270, 415), new Size(125, 30));
            objectManager.AddObject(platform7);

            // 벽8
            tempKey = room.NextObjKey;
            Platform platform8 = new Platform(room, tempKey, new Point(0, 350), new Size(125, 30));
            objectManager.AddObject(platform8);

            // 벽9
            tempKey = room.NextObjKey;
            Platform platform9 = new Platform(room, tempKey, new Point(420, 610), new Size(125, 30));
            objectManager.AddObject(platform9);

            // 벽10
            tempKey = room.NextObjKey;
            Platform platform10 = new Platform(room, tempKey, new Point(50, 220), new Size(125, 30));
            objectManager.AddObject(platform10);

            // 벽11
            tempKey = room.NextObjKey;
            Platform platform11 = new Platform(room, tempKey, new Point(260, 285), new Size(125, 30));
            objectManager.AddObject(platform11);

            // 벽12
            tempKey = room.NextObjKey;
            Platform platform12 = new Platform(room, tempKey, new Point(430, 350), new Size(125, 30));
            objectManager.AddObject(platform12);

            // 돌 바닥1
            tempKey = room.NextObjKey;
            Platform platform13 = new Platform(room, tempKey, new Point(225, 165), new Size(125, 30));
            objectManager.AddObject(platform13);
          

            // 돌 바닥2
            tempKey = room.NextObjKey;
            Platform platform14 = new Platform(room, tempKey, new Point(636, 165), new Size(125, 30));
            objectManager.AddObject(platform14);

            // 돌 바닥3
            tempKey = room.NextObjKey;
            Platform Platform15 = new Platform(room, tempKey, new Point(1075, 165), new Size(125, 30));
            objectManager.AddObject(Platform15);

            // 벽16
            tempKey = room.NextObjKey;
            Platform Platform16 = new Platform(room, tempKey, new Point(620, 675), new Size(125, 30));
            objectManager.AddObject(Platform16);

            // 벽17
            tempKey = room.NextObjKey;
            Platform Platform17 = new Platform(room, tempKey, new Point(815, 610), new Size(125, 30));
            objectManager.AddObject(Platform17);

            // 벽18
            tempKey = room.NextObjKey;
            Platform Platform18 = new Platform(room, tempKey, new Point(675, 545), new Size(125, 30));
            objectManager.AddObject(Platform18);

            // 벽19
            tempKey = room.NextObjKey;
            Platform Platform19 = new Platform(room, tempKey, new Point(818, 480), new Size(125, 30));
            objectManager.AddObject(Platform19);

            // 벽20
            tempKey = room.NextObjKey;
            Platform Platform20 = new Platform(room, tempKey, new Point(655, 415), new Size(125, 30));
            objectManager.AddObject(Platform20);

            // 벽21
            tempKey = room.NextObjKey;
            Platform Platform21 = new Platform(room, tempKey, new Point(600, 290), new Size(125, 30));
            objectManager.AddObject(Platform21);

            // 벽22
            tempKey = room.NextObjKey;
            Platform Platform22 = new Platform(room, tempKey, new Point(455, 225), new Size(125, 30));
            objectManager.AddObject(Platform22);

            // 벽23
            tempKey = room.NextObjKey;
            Platform Platform23 = new Platform(room, tempKey, new Point(980, 415), new Size(125, 30));
            objectManager.AddObject(Platform23);

            // 벽24
            tempKey = room.NextObjKey;
            Platform Platform24 = new Platform(room, tempKey, new Point(800, 350), new Size(125, 30));
            objectManager.AddObject(Platform24);

            // 벽25
            tempKey = room.NextObjKey;
            Platform Platform25 = new Platform(room, tempKey, new Point(1000, 285), new Size(125, 30));
            objectManager.AddObject(Platform25);

            // 벽25
            tempKey = room.NextObjKey;
            Platform Platform26 = new Platform(room, tempKey, new Point(880, 220), new Size(125, 30));
            objectManager.AddObject(Platform26);

            // 벽27
            tempKey = room.NextObjKey;
            Platform Platform27 = new Platform(room, tempKey, new Point(1245, 285), new Size(125, 30));
            objectManager.AddObject(Platform27);

            // 벽27
            tempKey = room.NextObjKey;
            Platform Platform28 = new Platform(room, tempKey, new Point(1138, 347), new Size(125, 30));
            objectManager.AddObject(Platform28);

            // 벽27
            tempKey = room.NextObjKey;
            Platform Platform29 = new Platform(room, tempKey, new Point(1300, 220), new Size(125, 30));
            objectManager.AddObject(Platform29);


            // 돌1
            tempKey = room.NextObjKey;
            ColorStone stone1 = new ColorStone(room, tempKey, new Point(270, 99), new Size(40, 65));
            stone1.SkinNum = randomSkin[0];
            objectManager.AddObject(stone1);

            // 돌2
            tempKey = room.NextObjKey;
            ColorStone stone2 = new ColorStone(room, tempKey, new Point(680, 99), new Size(40, 65));
            stone2.SkinNum = randomSkin[1];
            objectManager.AddObject(stone2);

            // 돌3
            tempKey = room.NextObjKey;
            ColorStone stone3 = new ColorStone(room, tempKey, new Point(1118, 99), new Size(40, 65));
            stone3.SkinNum = randomSkin[2];
            objectManager.AddObject(stone3);


            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(1070, 750), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            //타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox = new TimerBox(room, tempKey, new Point(650, 0), new Size(150, 50));
            timerBox.StartTime = 40000;
            this.timerbox1 = timerBox;
            timerBox.SetTimerStopAction(delegate () { room.AllDie(); });
            objectManager.AddObject(timerBox);



        }

        private void InitRandomSkin()
        {
            randomSkin = new List<int>(new int[] { 0, 0, 0 });

            List<int> skinArr = new List<int>();
            int i = 0;
            foreach (var item in room.roomClientDic)
            {
                skinArr.Add(item.Value.SkinNum);
                i++;
            }

            Random rand = new Random(); //랜덤선언

            for (int j = 0; j < 2; j++)
            {
                //선언 및 초기화 부분//
                int num = rand.Next(skinArr.Count);

                randomSkin[j] = skinArr[num];

                if (skinArr.Count > 1)
                    skinArr.RemoveAt(num);
            }
            randomSkin[2] = skinArr[0];

        }
    }
}