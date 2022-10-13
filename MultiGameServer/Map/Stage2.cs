using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage2 : MapBase
    {
        public Stage2(Room room) : base(room)
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

            // 돌1
            tempKey = objectManager.NextKey();
            Stone stone1 = new Stone(room, tempKey, new Point(663, 759), new Size(40, 40));
            stone1.weight = 1;
            objectManager.AddObject(stone1);

            // 벽1
            tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(room, tempKey, new Point(208, 715), new Size(125, 30));
            Floor1.SkinNum = 3;
            objectManager.AddObject(Floor1);

            // 벽2
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(485, 690), new Size(125, 30));
            Floor2.SkinNum = 3;
            objectManager.AddObject(Floor2);

            // 벽3
            tempKey = objectManager.NextKey();
            Floor Floor3 = new Floor(room, tempKey, new Point(762, 665), new Size(125, 30));
            Floor3.SkinNum = 3;
            objectManager.AddObject(Floor3);

            // 벽4
            tempKey = objectManager.NextKey();
            Floor Floor4 = new Floor(room, tempKey, new Point(1039, 640), new Size(125, 30));
            Floor4.SkinNum = 3;
            objectManager.AddObject(Floor4);

            // 벽5
            tempKey = objectManager.NextKey();
            Floor Floor5 = new Floor(room, tempKey, new Point(1236, 615), new Size(205, 30));
            Floor5.SkinNum = 3;
            objectManager.AddObject(Floor5);

            // 돌문
            tempKey = objectManager.NextKey();
            StoneDoor stoneDoor = new StoneDoor(room, tempKey, new Point(363, 699), new Size(50, 100));
            objectManager.AddObject(stoneDoor);

            // 버튼
            tempKey = objectManager.NextKey(); // 1340 780
            Button button = new Button(room, tempKey, new Point(363, 679), new Size(30, 20));
            button.TargetObject = stoneDoor;
            objectManager.AddObject(button);

            // 돌2
            tempKey = objectManager.NextKey();
            Stone stone2 = new Stone(room, tempKey, new Point(1310, 514), new Size(60, 100));
            stone2.weight = 3;
            objectManager.AddObject(stone2);


            // 벽6
            tempKey = objectManager.NextKey();
            Floor Floor6 = new Floor(room, tempKey, new Point(422, 499), new Size(125, 30));
            Floor6.SkinNum = 3;
            objectManager.AddObject(Floor6);

            // 벽7
            tempKey = objectManager.NextKey();
            Floor Floor7 = new Floor(room, tempKey, new Point(613, 111), new Size(125, 30));
            Floor7.SkinNum = 3;
            objectManager.AddObject(Floor7);

            

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(694, 21), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(13, 93), new Size(35, 50));
            objectManager.AddObject(KeyObject);
        }
    }
}
