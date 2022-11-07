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
    class Stage9 : MapBase
    {
        public Stage9(Room room) : base(room)
        {
            _skin = 2;
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
            Floor.SkinNum = 2;
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

            // 오른쪽 밑벽
            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(995, 649), new Size(500, 215));
            Floor4.SkinNum = 2;
            objectManager.AddObject(Floor4);


            // 돌1
            tempKey = room.NextObjKey;
            Stone stone1 = new Stone(room, tempKey, new Point(1145, 428), new Size(60, 220));
            stone1.weight = 3;
            objectManager.AddObject(stone1);

            // 포탈
            tempKey = room.NextObjKey;
            Portal portal1 = new Portal(room, tempKey, new Point(1320, 528), new Size(120, 120));
            portal1.TargetLocation = new Point(10, 10);
            objectManager.AddObject(portal1);





            // 왼쪽위 벽
            tempKey = room.NextObjKey;
            Floor floor8 = new Floor(room, tempKey, new Point(0, 150), new Size(400, 100));
            floor8.SkinNum = 2;
            objectManager.AddObject(floor8);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(1350, 80), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            // 천장
            tempKey = room.NextObjKey;
            Floor Floor13 = new Floor(room, tempKey, new Point(0, -35), new Size(1440, 30));
            objectManager.AddObject(Floor13);

            // 벽6
            tempKey = room.NextObjKey;
            Platform platform11 = new Platform(room, tempKey, new Point(610, 236), new Size(125, 30));
            objectManager.AddObject(platform11);

            // 벽7
            tempKey = room.NextObjKey;
            Platform platform12 = new Platform(room, tempKey, new Point(910, 206), new Size(125, 30));
            objectManager.AddObject(platform12);

            // 열쇠 발판
            tempKey = room.NextObjKey;
            Floor floor13 = new Floor(room, tempKey, new Point(1195, 170), new Size(235, 50));
            floor13.SkinNum = 2;
            objectManager.AddObject(floor13);

            tempKey = room.NextObjKey;
            PressingButton button1 = new PressingButton(room, tempKey, new Point(140, 790), new Size(20, 10));
            // button.SetAction(delegate () { timerBox.TimerStop(); });
            objectManager.AddObject(button1);

            tempKey = room.NextObjKey;
            PressingButton button2 = new PressingButton(room, tempKey, new Point(450, 790), new Size(20, 10));
            // button.SetAction(delegate () { timerBox.TimerStop(); });
            objectManager.AddObject(button2);

            tempKey = room.NextObjKey;
            Button button3 = new Button(room, tempKey, new Point(1270, 160), new Size(20, 10));
            // button.SetAction(delegate () { timerBox.TimerStop(); });
            objectManager.AddObject(button3);



            // 긴 용암
            tempKey = room.NextObjKey;
            Lava lava1 = new Lava(room, tempKey, new Point(0, 335), new Size(1440, 65));
            objectManager.AddObject(lava1);

            // 떨어지는 용암
            tempKey = room.NextObjKey;
            Lava lava2 = new Lava(room, tempKey, new Point(790, 0), new Size(96, 350));
            lava2.SkinNum = 1;
            objectManager.AddObject(lava2);

            // 벽14
            tempKey = room.NextObjKey;
            Floor floor14 = new Floor(room, tempKey, new Point(0, 370), new Size(1440, 40));
            floor14.SkinNum = 2;
            objectManager.AddObject(floor14);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(900, 710), new Size(70, 90));
            objectManager.AddObject(door);

        }
    }
}
