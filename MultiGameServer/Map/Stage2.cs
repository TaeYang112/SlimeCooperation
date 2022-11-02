using MultiGameModule;
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
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            Floor.SkinNum = 0;
            objectManager.AddObject(Floor);

            // 돌1
            tempKey = room.NextObjKey;
            Stone stone1 = new Stone(room, tempKey, new Point(185, 622), new Size(40, 65));
            stone1.weight = 2;
            objectManager.AddObject(stone1);

            // 벽1
            tempKey = room.NextObjKey;
            Floor floor1 = new Floor(room, tempKey, new Point(130, 690), new Size(125, 30));
            objectManager.AddObject(floor1);

            // 벽2
            tempKey = room.NextObjKey;
            Platform platform2 = new Platform(room, tempKey, new Point(415, 690), new Size(125, 30));
            objectManager.AddObject(platform2);

            // 벽3
            tempKey = room.NextObjKey;
            Platform platform3 = new Platform(room, tempKey, new Point(692, 665), new Size(125, 30));
            objectManager.AddObject(platform3);

            // 벽4
            tempKey = room.NextObjKey;
            Platform platform4 = new Platform(room, tempKey, new Point(950, 640), new Size(125, 30));
            objectManager.AddObject(platform4);

            // 벽5
            tempKey = room.NextObjKey;
            Floor floor5 = new Floor(room, tempKey, new Point(1150, 580), new Size(275, 30));
            floor5.SkinNum = 0;
            objectManager.AddObject(floor5);

            // 돌2
            tempKey = room.NextObjKey;
            Stone stone2 = new Stone(room, tempKey, new Point(1265, 479), new Size(60, 100));
            stone2.weight = 3;
            objectManager.AddObject(stone2);

            // 돌3
            tempKey = room.NextObjKey;
            Stone stone3 = new Stone(room, tempKey, new Point(1254, 389), new Size(40, 40));
            stone3.weight = 1;
            objectManager.AddObject(stone3);

            // 사라지는 돌문
            tempKey = room.NextObjKey;
            StoneDoor stoneDoor = new StoneDoor(room, tempKey, new Point(850,40 ), new Point(900, 129));
            stoneDoor.OpeningMode = StoneDoorMode.BEGONE;
            objectManager.AddObject(stoneDoor);

            // 버튼1
            tempKey = room.NextObjKey;
            Button Button1 = new Button(room, tempKey, new Point(1330,570), new Size(20, 10));
            Button1.SetAction(delegate() { stoneDoor.Open(); });
            objectManager.AddObject(Button1);

            // 벽6
            tempKey = room.NextObjKey;
            Platform platform6 = new Platform(room, tempKey, new Point(1224, 430), new Size(125, 30));
            objectManager.AddObject(platform6);

            // 벽6
            tempKey = room.NextObjKey;
            Platform platform8 = new Platform(room, tempKey, new Point(930, 475), new Size(125, 30));
            objectManager.AddObject(platform8);

            tempKey = room.NextObjKey;
            Platform platform9 = new Platform(room, tempKey, new Point(700, 425), new Size(125, 30));
            objectManager.AddObject(platform9);

            tempKey = room.NextObjKey;
            Platform platform10 = new Platform(room, tempKey, new Point(470, 375), new Size(125, 30));
            objectManager.AddObject(platform10);

            tempKey = room.NextObjKey;
            Platform platform11 = new Platform(room, tempKey, new Point(240, 325), new Size(125, 30));
            objectManager.AddObject(platform11);

            tempKey = room.NextObjKey;
            Floor floor12 = new Floor(room, tempKey, new Point(1, 260), new Size(125, 30));
            objectManager.AddObject(floor12);

            tempKey = room.NextObjKey;
            Platform platform13 = new Platform(room, tempKey, new Point(450, 220), new Size(125, 30));
            objectManager.AddObject(platform13);

            tempKey = room.NextObjKey;
            Platform platform14 = new Platform(room, tempKey, new Point(650, 180), new Size(125, 30));
            objectManager.AddObject(platform14);

            // 열쇠 바닥
            tempKey = room.NextObjKey;
            Floor floor7 = new Floor(room, tempKey, new Point(850, 130), new Size(125, 30));
            objectManager.AddObject(floor7);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(10, 170), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(950, 60), new Size(35, 50));
            objectManager.AddObject(KeyObject);
        }
    }
}