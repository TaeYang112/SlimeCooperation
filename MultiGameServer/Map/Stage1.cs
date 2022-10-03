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
            // 땅
            int tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(room, tempKey, new Point(0, 390), new Point(800, 450));
            Floor1.SkinNum = 0;
            objectManager.AddObject(Floor1);

            // 튀어나온벽
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(room, tempKey, new Point(700, 240), new Point(800, 490));
            Floor2.SkinNum = 0;
            objectManager.AddObject(Floor2);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(room, tempKey, new Point(550, 300), new Point(620, 390));
            door.SkinNum = 0;
            objectManager.AddObject(door);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(720, 190), new Point(755, 240));
            KeyObject.SkinNum = 0;
            objectManager.AddObject(KeyObject);

            // 돌
            tempKey = objectManager.NextKey();
            Stone stone = new Stone(room, tempKey, new Point(450, 349), new Point(490, 389));
            stone.SkinNum = 0;
            stone.weight = 2;
            objectManager.AddObject(stone);


        }

    }
}
