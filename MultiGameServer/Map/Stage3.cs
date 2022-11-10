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
            _skin = 0;
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

            // 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(995, 865));
            objectManager.AddObject(Floor);

            // 벽1
            tempKey = room.NextObjKey;
            Platform platform1 = new Platform(room, tempKey, new Point(203, 735), new Size(125, 30));
            objectManager.AddObject(platform1);

            // 벽2
            tempKey = room.NextObjKey;
            Platform platform2 = new Platform(room, tempKey, new Point(475, 705), new Size(125, 30));
            objectManager.AddObject(platform2);

            // 벽3
            tempKey = room.NextObjKey;
            Platform platform3 = new Platform(room, tempKey, new Point(731, 677), new Size(125, 30));
            objectManager.AddObject(platform3);

            // 벽4
            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(995, 649), new Size(500, 215));
            objectManager.AddObject(Floor4);
            tempKey = room.NextObjKey;

            // 포탈
            tempKey = room.NextObjKey;
            Portal portal1 = new Portal(room, tempKey, new Point(1285, 528), new Size(120, 120));
            portal1.TargetLocation = new Point(10, 10);
            objectManager.AddObject(portal1);

            // 돌1
            tempKey = room.NextObjKey;
            Stone stone1 = new Stone(room, tempKey, new Point(1145, 428), new Size(60, 220));
            stone1.weight = 3;
            objectManager.AddObject(stone1);

            // 덤불
            tempKey = room.NextObjKey;
            ThornBush lava = new ThornBush(room, tempKey, new Point(0, 300), new Size(1440, 65));
            objectManager.AddObject(lava);

            // 덤불 밑 바닥
            tempKey = room.NextObjKey;
            Floor Floor10 = new Floor(room, tempKey, new Point(0, 365), new Point(1440, 420));
            objectManager.AddObject(Floor10);


            // 벽5
            tempKey = room.NextObjKey;
            Floor floor8 = new Floor(room, tempKey, new Point(0, 98), new Size(400, 100));
            objectManager.AddObject(floor8);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(115, 40), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            // 천장
            tempKey = room.NextObjKey;
            Floor Floor13 = new Floor(room, tempKey, new Point(0, -30), new Size(1440, 30));
            Floor13.SkinNum = -1;
            objectManager.AddObject(Floor13);

            
            // 돌2
            tempKey = room.NextObjKey;
            Stone stone2 = new Stone(room, tempKey, new Point(250, 22), new Size(40, 75));
            stone2.weight = 3;
            objectManager.AddObject(stone2);
            
            // 벽6
            tempKey = room.NextObjKey;
            Platform platform11 = new Platform(room, tempKey, new Point(625, 236), new Size(125, 30));
            objectManager.AddObject(platform11);

            // 벽7
            tempKey = room.NextObjKey;
            Platform platform12 = new Platform(room, tempKey, new Point(920, 206), new Size(125, 30));
            objectManager.AddObject(platform12);

            // 벽8
            tempKey = room.NextObjKey;
            Floor floor13 = new Floor(room, tempKey, new Point(1195, 170), new Size(235, 50));
            objectManager.AddObject(floor13);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(1350, 80), new Size(70, 90));
            objectManager.AddObject(door);


        }
    }
}
