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
        public Stage1()
        {
            // 땅
            int tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(tempKey, new Point(0, 390), new Size(800, 60));
            Floor1.SkinNum = 0;
            objectManager.AddObject(Floor1);

            // 끝에 벽
            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(tempKey, new Point(700, 240), new Size(100, 150));
            Floor2.SkinNum = 0;
            objectManager.AddObject(Floor2);

            // 열쇠
            tempKey = objectManager.NextKey();
            KeyObject KeyObject = new KeyObject(tempKey, new Point(720, 190), new Size(35, 50));
            KeyObject.SkinNum = 0;
            objectManager.AddObject(KeyObject);

            // 문
            tempKey = objectManager.NextKey();
            Door door = new Door(tempKey, new Point(550, 300), new Size(70, 90));
            door.SkinNum = 0;
            objectManager.AddObject(door);
        }

    }
}
