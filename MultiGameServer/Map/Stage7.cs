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

            // 맵 바깥 벽(아래)
            tempKey = room.NextObjKey;
            Lava lava1 = new Lava(room, tempKey, new Point(0, 864), new Size(1440, 866));
            objectManager.AddObject(lava1);


            // 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(300, 865));
            Floor.SkinNum = 0;
            objectManager.AddObject(Floor);

            // 땅
            tempKey = room.NextObjKey;
            Floor Floor1 = new Floor(room, tempKey, new Point(370, 800), new Point(895, 865));
            Floor1.SkinNum = 0;
            objectManager.AddObject(Floor1);

            // 문 땅
            tempKey = room.NextObjKey;
            Floor Floor2 = new Floor(room, tempKey, new Point(1215, 800), new Point(1440, 865));
            Floor2.SkinNum = 0;
            objectManager.AddObject(Floor2);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(1350, 710), new Size(70, 90));
            objectManager.AddObject(door);


            // 벽
            tempKey = room.NextObjKey;
            Platform platform1 = new Platform(room, tempKey, new Point(70, 690), new Size(125, 30));
            objectManager.AddObject(platform1);

            tempKey = room.NextObjKey;
            Platform platform2 = new Platform(room, tempKey, new Point(70, 622), new Size(125, 30));
            objectManager.AddObject(platform2);

            tempKey = room.NextObjKey;
            Platform platform3 = new Platform(room, tempKey, new Point(70, 554), new Size(125, 30));
            objectManager.AddObject(platform3);

            tempKey = room.NextObjKey;
            Platform platform4 = new Platform(room, tempKey, new Point(70, 486), new Size(125, 30));
            objectManager.AddObject(platform4);

            tempKey = room.NextObjKey;
            Platform platform5 = new Platform(room, tempKey, new Point(70, 418), new Size(125, 30));
            objectManager.AddObject(platform5);

            tempKey = room.NextObjKey;
            Platform platform6 = new Platform(room, tempKey, new Point(70, 350), new Size(125, 30));
            objectManager.AddObject(platform6);

            tempKey = room.NextObjKey;
            Platform platform7 = new Platform(room, tempKey, new Point(70, 282), new Size(125, 30));
            objectManager.AddObject(platform7);

            tempKey = room.NextObjKey;
            Platform platform8 = new Platform(room, tempKey, new Point(70, 214), new Size(125, 30));
            objectManager.AddObject(platform8);
            //점프 높이 68


            // 돌 땅
            tempKey = room.NextObjKey;
            Floor Floor3 = new Floor(room, tempKey, new Point(225, 165), new Point(760, 230));
            Floor3.SkinNum = 0;
            objectManager.AddObject(Floor3);

            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(688, 230), new Point(760, 486));
            Floor3.SkinNum = 0;
            objectManager.AddObject(Floor4);

            tempKey = room.NextObjKey;
            Floor Floor5 = new Floor(room, tempKey, new Point(370, 486), new Point(760, 551));
            Floor3.SkinNum = 0;
            objectManager.AddObject(Floor5);



            // 돌1
            tempKey = room.NextObjKey;
            ColorStone stone1 = new ColorStone(room, tempKey, new Point(540, 400), new Size(100, 63));
            stone1.SkinNum = randomSkin[0];
            objectManager.AddObject(stone1);

            // 돌2
            tempKey = room.NextObjKey;
            ColorStone stone2 = new ColorStone(room, tempKey, new Point(500, 99), new Size(100, 63));
            stone2.SkinNum = randomSkin[1];
            objectManager.AddObject(stone2);

            // 돌3
            tempKey = room.NextObjKey;
            ColorStone stone3 = new ColorStone(room, tempKey, new Point(700, 736), new Size(40, 63));
            stone3.SkinNum = randomSkin[2];
            objectManager.AddObject(stone3);


            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(1070, 750), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            //타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox = new TimerBox(room, tempKey, new Point(650, 0), new Size(150, 50));
            timerBox.StartTime = 10000;
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