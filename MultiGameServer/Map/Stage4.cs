using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage4 : MapBase
    {
        private List<int> randomSkin;
        public Stage4(Room room) : base(room)
        {
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

            
            // 색깔 돌1
            tempKey = room.NextObjKey;
            ColorStone colorStone1 = new ColorStone(room, tempKey, new Point(400, 720), new Size(70, 70));
            colorStone1.SkinNum = randomSkin[0];
            objectManager.AddObject(colorStone1);

            // 색깔 돌2
            tempKey = room.NextObjKey;
            ColorStone colorStone2 = new ColorStone(room, tempKey, new Point(650, 720), new Size(70, 70));
            colorStone2.SkinNum = randomSkin[1];
            objectManager.AddObject(colorStone2);

            // 색깔 돌3
            tempKey = room.NextObjKey;
            ColorStone colorStone3 = new ColorStone(room, tempKey, new Point(900, 720), new Size(70, 70));
            colorStone3.SkinNum = randomSkin[2];
            objectManager.AddObject(colorStone3);
            

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
