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
        public Stage2(Room room)
        {
            int tempKey;

            // 맵 바깥 벽 (왼)
            tempKey = objectManager.NextKey();
            Floor leftWall = new Floor(room, tempKey, new Point(-11, 0), new Size(10, 450));
            objectManager.AddObject(leftWall);

            // 맵 바깥 벽 (우)
            tempKey = objectManager.NextKey();
            Floor rightWall = new Floor(room, tempKey, new Point(784, 0), new Size(10, 450));
            objectManager.AddObject(rightWall);

            // 땅
            tempKey = objectManager.NextKey();
            Floor Floor = new Floor(room, tempKey, new Point(0, 390), new Point(800, 450));
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);

            // 벽1
            tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(room, tempKey, new Point(613, 315), new Size(151, 29));
            Floor1.SkinNum = 2;
            objectManager.AddObject(Floor1);

            // 벽2
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(404, 244), new Size(151, 29));
            Floor2.SkinNum = 2;
            objectManager.AddObject(Floor2);

            // 벽3
            tempKey = objectManager.NextKey();
            Floor Floor3 = new Floor(room, tempKey, new Point(230, 187), new Size(151, 29));
            Floor3.SkinNum = 2;
            objectManager.AddObject(Floor3);

            // 벽4
            tempKey = objectManager.NextKey();
            Floor Floor4 = new Floor(room, tempKey, new Point(13, 143), new Size(151, 29));
            Floor4.SkinNum = 2;
            objectManager.AddObject(Floor4);

            // 벽5
            tempKey = objectManager.NextKey();
            Floor Floor5 = new Floor(room, tempKey, new Point(230, 71), new Size(151, 29));
            Floor5.SkinNum = 2;
            objectManager.AddObject(Floor5);

            // 벽6
            tempKey = objectManager.NextKey();
            Floor Floor6 = new Floor(room, tempKey, new Point(422, 499), new Size(151, 29));
            Floor6.SkinNum = 2;
            objectManager.AddObject(Floor6);

            // 벽7
            tempKey = objectManager.NextKey();
            Floor Floor7 = new Floor(room, tempKey, new Point(613, 111), new Size(151, 29));
            Floor7.SkinNum = 2;
            objectManager.AddObject(Floor7);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(694, 21), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(13, 93), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            // 돌
            tempKey = objectManager.NextKey();
            Stone stone = new Stone(room, tempKey, new Point(60, 52), new Size(60, 90));
            stone.weight = 2;
            objectManager.AddObject(stone);


        }

    }
}
