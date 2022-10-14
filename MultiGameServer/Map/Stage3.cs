using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage3 : MapBase
    {
        public Stage3(Room room) : base(room)
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
            int tempKey;

            // 맵 바깥 벽 (왼)
            tempKey = objectManager.NextKey();
            Floor leftWall = new Floor(room, tempKey, new Point(-11, 0), new Size(10, 865));
            leftWall.SkinNum = -1;
            objectManager.AddObject(leftWall);

            // 맵 바깥 벽 (우)
            tempKey = objectManager.NextKey();
            Floor rightWall = new Floor(room, tempKey, new Point(1424, 0), new Size(10, 865));
            rightWall.SkinNum = -1;
            objectManager.AddObject(rightWall);

            // 땅
            tempKey = objectManager.NextKey();
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);

            // 벽1
            tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(room, tempKey, new Point(203, 735), new Size(125, 30));
            Floor1.SkinNum = 3;
            objectManager.AddObject(Floor1);

            // 벽2
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(475, 705), new Size(125, 30));
            Floor2.SkinNum = 3;
            objectManager.AddObject(Floor2);

            // 벽3
            tempKey = objectManager.NextKey();
            Floor Floor3 = new Floor(room, tempKey, new Point(731, 677), new Size(125, 30));
            Floor3.SkinNum = 3;
            objectManager.AddObject(Floor3);

            // 벽4
            tempKey = objectManager.NextKey();
            Floor Floor4 = new Floor(room, tempKey, new Point(995, 649), new Size(125, 30));
            Floor4.SkinNum = 3;
            objectManager.AddObject(Floor4);
            tempKey = objectManager.NextKey();

            Floor Floor5 = new Floor(room, tempKey, new Point(1120, 649), new Size(125, 30));
            Floor5.SkinNum = 3;
            objectManager.AddObject(Floor5);
            tempKey = objectManager.NextKey();

            Floor Floor6 = new Floor(room, tempKey, new Point(1245, 649), new Size(200, 30));
            Floor6.SkinNum = 3;
            objectManager.AddObject(Floor6);

            // 돌1
            tempKey = objectManager.NextKey();
            Stone stone1 = new Stone(room, tempKey, new Point(1145, 410), new Size(60, 220));
            stone1.weight = 1;
            objectManager.AddObject(stone1);

            // 땅2
            tempKey = objectManager.NextKey();
            Floor Floor7 = new Floor(room, tempKey, new Point(0, 335), new Size(1440, 65));
            Floor7.SkinNum = 2;
            objectManager.AddObject(Floor7);

            // 벽5
            tempKey = objectManager.NextKey();
            Floor Floor8 = new Floor(room, tempKey, new Point(0, 98), new Size(125, 30));
            Floor8.SkinNum = 3;
            objectManager.AddObject(Floor8);

            tempKey = objectManager.NextKey();
            Floor Floor9 = new Floor(room, tempKey, new Point(125, 98), new Size(125, 30));
            Floor9.SkinNum = 3;
            objectManager.AddObject(Floor9);

            tempKey = objectManager.NextKey();
            Floor Floor10 = new Floor(room, tempKey, new Point(250, 98), new Size(150, 30));
            Floor10.SkinNum = 3;
            objectManager.AddObject(Floor10);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(115, 40), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            // 천장
            tempKey = objectManager.NextKey();
            Floor Floor13 = new Floor(room, tempKey, new Point(0, -30), new Size(1440, 30));
            Floor13.SkinNum = 3;
            objectManager.AddObject(Floor13);


            // 돌2
            tempKey = objectManager.NextKey();
            Stone stone2 = new Stone(room, tempKey, new Point(150, 10), new Size(40, 75));
            stone2.weight = 1;
            objectManager.AddObject(stone2);

            // 벽6
            tempKey = objectManager.NextKey();
            Floor Floor11 = new Floor(room, tempKey, new Point(605, 236), new Size(125, 30));
            Floor11.SkinNum = 3;
            objectManager.AddObject(Floor11);

            // 벽7
            tempKey = objectManager.NextKey();
            Floor Floor12 = new Floor(room, tempKey, new Point(900, 206), new Size(125, 30));
            Floor12.SkinNum = 3;
            objectManager.AddObject(Floor12);

            // 벽8
            tempKey = objectManager.NextKey();
            Floor Floor14 = new Floor(room, tempKey, new Point(1195, 170), new Size(235, 30));
            Floor14.SkinNum = 3;
            objectManager.AddObject(Floor14);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(1350, 80), new Size(70, 90));
            objectManager.AddObject(door);


        }
    }
}
