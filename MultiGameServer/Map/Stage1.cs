using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage1 : MapBase
    {
        public Stage1(Room room) : base(room)
        {
        }

        protected override void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(480, 740);
            SpawnLocation[1] = new Point(580, 740);
            SpawnLocation[2] = new Point(680, 740);
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
            Floor Floor1 = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            objectManager.AddObject(Floor1);

            // 언덕
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(1300, 600), new Point(1423, 800));
            Floor2.SkinNum = 1;
            objectManager.AddObject(Floor2);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(100, 710), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(1350, 550), new Size(35, 50));
            objectManager.AddObject(KeyObject);


            // 거대 돌
            tempKey = objectManager.NextKey();
            Stone stone = new Stone(room, tempKey, new Point(170, 400), new Point(270, 799));
            stone.weight = 2;
            objectManager.AddObject(stone);
        }

    }
}
