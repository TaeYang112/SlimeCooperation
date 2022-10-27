using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage5 : MapBase
    {
        public Stage5(Room room) : base(room)
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

            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(500, 759), new Size(40, 40));
            Floor2.SkinNum = 1;
            objectManager.AddObject(Floor2);

            tempKey = objectManager.NextKey();
            Floor Floor3 = new Floor(room, tempKey, new Point(0, 480), new Size(350, 40));
            Floor3.SkinNum = 2;
            objectManager.AddObject(Floor3);

            tempKey = objectManager.NextKey();
            Floor Floor4 = new Floor(room, tempKey, new Point(340, 480), new Size(350, 40));
            Floor4.SkinNum = 2;
            objectManager.AddObject(Floor4);

            tempKey = objectManager.NextKey();
            Floor Floor12 = new Floor(room, tempKey, new Point(680, 480), new Size(350, 40));
            Floor12.SkinNum = 2;
            objectManager.AddObject(Floor12);

            tempKey = objectManager.NextKey();
            Floor Floor5 = new Floor(room, tempKey, new Point(960, 745), new Size(474, 55));
            Floor5.SkinNum = 1;
            objectManager.AddObject(Floor5);

            tempKey = objectManager.NextKey();
            Floor Floor6 = new Floor(room, tempKey, new Point(1010, 690), new Size(424, 55));
            Floor6.SkinNum = 1;
            objectManager.AddObject(Floor6);

            tempKey = objectManager.NextKey();
            Floor Floor7 = new Floor(room, tempKey, new Point(1060, 635), new Size(374, 55));
            Floor7.SkinNum = 1;
            objectManager.AddObject(Floor7);

            tempKey = objectManager.NextKey();
            Floor Floor8 = new Floor(room, tempKey, new Point(1110, 580), new Size(324, 55));
            Floor8.SkinNum = 1;
            objectManager.AddObject(Floor8);

            tempKey = objectManager.NextKey();
            Floor Floor9 = new Floor(room, tempKey, new Point(1160, 525), new Size(274, 55));
            Floor9.SkinNum = 1;
            objectManager.AddObject(Floor9);

            tempKey = objectManager.NextKey();
            Floor Floor13 = new Floor(room, tempKey, new Point(475, 310), new Size(400, 40));
            Floor13.SkinNum = 2;
            objectManager.AddObject(Floor13);

            tempKey = objectManager.NextKey();
            Floor Floor14 = new Floor(room, tempKey, new Point(435, 180), new Size(40,170));
            Floor14.SkinNum = 1;
            objectManager.AddObject(Floor14);

            tempKey = objectManager.NextKey();
            Floor Floor15 = new Floor(room, tempKey, new Point(0, 240), new Size(55, 240));
            Floor15.SkinNum = 1;
            objectManager.AddObject(Floor15);

            tempKey = objectManager.NextKey();
            Floor Floor16 = new Floor(room, tempKey, new Point(55, 280), new Size(55, 200));
            Floor16.SkinNum = 1;
            objectManager.AddObject(Floor16);

            tempKey = objectManager.NextKey();
            Floor Floor17 = new Floor(room, tempKey, new Point(110, 320), new Size(55, 160));
            Floor17.SkinNum = 1;
            objectManager.AddObject(Floor17);

            tempKey = objectManager.NextKey();
            Floor Floor18 = new Floor(room, tempKey, new Point(165, 360), new Size(55, 120));
            Floor18.SkinNum = 1;
            objectManager.AddObject(Floor18);

            tempKey = objectManager.NextKey();
            Floor Floor19 = new Floor(room, tempKey, new Point(150, 175), new Size(350, 40));
            Floor19.SkinNum = 2;
            objectManager.AddObject(Floor19);

            tempKey = objectManager.NextKey();
            Floor Floor20 = new Floor(room, tempKey, new Point(490, 175), new Size(350, 40));
            Floor20.SkinNum = 2;
            objectManager.AddObject(Floor20);

            tempKey = objectManager.NextKey();
            Floor Floor21 = new Floor(room, tempKey, new Point(830, 175), new Size(350, 40));
            Floor21.SkinNum = 2;
            objectManager.AddObject(Floor21);

            tempKey = objectManager.NextKey();
            Floor Floor22 = new Floor(room, tempKey, new Point(1070, 175), new Size(350, 40));
            Floor22.SkinNum = 2;
            objectManager.AddObject(Floor22);

            tempKey = objectManager.NextKey();
            Stone stone1 = new Stone(room, tempKey, new Point(680, 414), new Size(40, 65));
            stone1.weight = 2;
            objectManager.AddObject(stone1);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(1323, 87), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(500, 220), new Size(35, 50));
            objectManager.AddObject(KeyObject);
        }
    }
}