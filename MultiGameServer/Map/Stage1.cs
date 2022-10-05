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
        public Stage1(Room room)
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
            Floor Floor1 = new Floor(room, tempKey, new Point(0, 390), new Point(800, 450));
            objectManager.AddObject(Floor1);

            // 튀어나온벽
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(700, 240), new Point(800, 391));
            Floor2.SkinNum = 1;
            objectManager.AddObject(Floor2);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(550, 300), new Point(620, 390));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(720, 190), new Point(755, 240));
            objectManager.AddObject(KeyObject);

            // 돌
            tempKey = objectManager.NextKey();
            Stone stone = new Stone(room, tempKey, new Point(450, 349), new Point(490, 389));
            stone.weight = 2;
            objectManager.AddObject(stone);

            // 버튼
            tempKey = objectManager.NextKey();
            MultiGameServer.Object.Button button = new MultiGameServer.Object.Button(room, tempKey, new Point(350, 369), new Point(380, 389));
            objectManager.AddObject(button);


        }

    }
}
