﻿using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage4 : MapBase
    {
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

            //아래 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 810), new Point(720, 865)); // 1440 865
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);
            //위 땅
            tempKey = room.NextObjKey;
            Floor Floor6 = new Floor(room, tempKey, new Point(0, 450), new Point(1155, 505)); // 1440 865
            Floor6.SkinNum = 2;
            objectManager.AddObject(Floor6);


            //우측 땅1 점프 68
            tempKey = room.NextObjKey;
            Floor Floor8 = new Floor(room, tempKey, new Point(1325, 493), new Point(1440, 900)); // 1440 865
            Floor8.SkinNum = 4;
            objectManager.AddObject(Floor8);
            // 우측 땅1 점프 68
            tempKey = room.NextObjKey;
            Floor Floor2 = new Floor(room, tempKey, new Point(1250, 560), new Point(1440, 900)); // 1440 865
            Floor2.SkinNum = 4;
            objectManager.AddObject(Floor2);
            // 우측 땅2
            tempKey = room.NextObjKey;
            Floor Floor3 = new Floor(room, tempKey, new Point(1100, 625), new Point(1250, 900)); // 1440 865
            Floor3.SkinNum = 4;
            objectManager.AddObject(Floor3);
            // 우측 땅3
            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(950, 688), new Point(1100, 900)); // 1440 865
            Floor4.SkinNum = 5;
            objectManager.AddObject(Floor4);
            // 우측 땅밑
            tempKey = room.NextObjKey;
            Floor Floor5 = new Floor(room, tempKey, new Point(608, 1057), new Point(1250, 1058)); // 1440 865
            Floor5.SkinNum = 2;
            objectManager.AddObject(Floor5);

            // 돌1
            tempKey = room.NextObjKey;
            Stone stone1 = new Stone(room, tempKey, new Point(425, 508), new Size(100, 300));
            stone1.weight = 3;
            objectManager.AddObject(stone1);


            // 벽7
            tempKey = room.NextObjKey;
            Floor Floor7 = new Floor(room, tempKey, new Point(613, 111), new Size(125, 30));
            Floor7.SkinNum = 3;
            objectManager.AddObject(Floor7);



            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(10, 359), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(710, 200), new Size(35, 50));
            objectManager.AddObject(KeyObject);
        }
    }
}
