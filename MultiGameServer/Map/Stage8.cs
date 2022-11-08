using MultiGameServer.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    class Stage8 : MapBase
    {
        public Stage8(Room room) : base(room)
        {
            _skin = 2;
        }

        protected override void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(5, 740);
            SpawnLocation[1] = new Point(80, 740);
            SpawnLocation[2] = new Point(145, 740);
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

            #region 1층
            //땅 1층
            tempKey = room.NextObjKey;
            Floor Floor2 = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            Floor2.SkinNum = 2;
            objectManager.AddObject(Floor2);

            // Don't push 버튼
            tempKey = room.NextObjKey;
            Button button1 = new Button(room, tempKey, new Point(215, 787), new Size(40, 13));
            button1.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button1);


            tempKey = room.NextObjKey;
            Button button2 = new Button(room, tempKey, new Point(415, 787), new Size(40, 13));
            button2.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button2);


            tempKey = room.NextObjKey;
            Button button3 = new Button(room, tempKey, new Point(615, 787), new Size(40, 13));
            button3.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button3);


            tempKey = room.NextObjKey;
            Button button4 = new Button(room, tempKey, new Point(815, 787), new Size(40, 13));
            button4.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button4);


            tempKey = room.NextObjKey;
            Button button5 = new Button(room, tempKey, new Point(1015, 787), new Size(40, 13));
            button5.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button5);


            tempKey = room.NextObjKey;
            Button button6 = new Button(room, tempKey, new Point(1215, 787), new Size(40, 13));
            button6.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button6);

            //포탈1
            tempKey = room.NextObjKey;
            Portal portal1 = new Portal(room, tempKey, new Point(1300, 670), new Size(120, 120));
            portal1.TargetLocation = new Point(1350, 400);
            objectManager.AddObject(portal1);

            #endregion

            #region 2층

            // 땅2층
            tempKey = room.NextObjKey;
            Floor Floor4 = new Floor(room, tempKey, new Point(1290, 465), new Size(150, 65));
            Floor4.SkinNum = 2;
            objectManager.AddObject(Floor4);

            tempKey = room.NextObjKey;
            Floor Floor5 = new Floor(room, tempKey, new Point(940, 465), new Size(200, 65));
            Floor5.SkinNum = 2;
            objectManager.AddObject(Floor5);

            // Don't push 버튼
            tempKey = room.NextObjKey;
            Button button7 = new Button(room, tempKey, new Point(1018, 452), new Size(40, 13));
            button7.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button7);

            tempKey = room.NextObjKey;
            Floor Floor6 = new Floor(room, tempKey, new Point(590, 465), new Size(200, 65));
            Floor6.SkinNum = 2;
            objectManager.AddObject(Floor6);

            tempKey = room.NextObjKey;
            Button button8 = new Button(room, tempKey, new Point(668, 452), new Size(40, 13));
            button8.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button8);

            tempKey = room.NextObjKey;
            Floor Floor7 = new Floor(room, tempKey, new Point(240, 465), new Size(200, 65));
            Floor7.SkinNum = 2;
            objectManager.AddObject(Floor7);

            tempKey = room.NextObjKey;
            Button button9 = new Button(room, tempKey, new Point(318, 452), new Size(40, 13));
            button9.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button9);

            tempKey = room.NextObjKey;
            Floor Floor8 = new Floor(room, tempKey, new Point(0, 465), new Size(150, 65));
            Floor8.SkinNum = 2;
            objectManager.AddObject(Floor8);

            //포탈2
            tempKey = room.NextObjKey;
            Portal portal2 = new Portal(room, tempKey, new Point(20, 350), new Size(120, 120));
            portal2.TargetLocation = new Point(45, 85);
            portal2.SkinNum = 0;
            objectManager.AddObject(portal2);

            #endregion

            #region 3층
            //땅 3층
            tempKey = room.NextObjKey;
            Floor Floor3 = new Floor(room, tempKey, new Point(0, 165), new Point(150, 230));
            Floor3.SkinNum = 2;
            objectManager.AddObject(Floor3);

            tempKey = room.NextObjKey;
            Floor Floor9 = new Floor(room, tempKey, new Point(325, 165), new Size(150, 65));
            Floor9.SkinNum = 2;
            objectManager.AddObject(Floor9);

            tempKey = room.NextObjKey;
            Button button10 = new Button(room, tempKey, new Point(378, 152), new Size(40, 13));
            button10.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button10);

            tempKey = room.NextObjKey;
            Floor Floor10 = new Floor(room, tempKey, new Point(650, 165), new Size(150, 65));
            Floor10.SkinNum = 2;
            objectManager.AddObject(Floor10);

            tempKey = room.NextObjKey;
            Button button11 = new Button(room, tempKey, new Point(703, 152), new Size(40, 13));
            button11.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button11);

            tempKey = room.NextObjKey;
            Floor Floor11 = new Floor(room, tempKey, new Point(975, 165), new Size(150, 65));
            Floor11.SkinNum = 2;
            objectManager.AddObject(Floor11);

            tempKey = room.NextObjKey;
            Button button12 = new Button(room, tempKey, new Point(1028, 152), new Size(40, 13));
            button12.SetAction(delegate () { room.AllDie(); });
            objectManager.AddObject(button12);

            // 문
            tempKey = room.NextObjKey;
            Door door = new Door(room, tempKey, new Point(1350, 75), new Size(70, 90));
            objectManager.AddObject(door);

            // 열쇠
            tempKey = room.NextObjKey;
            KeyObject KeyObject = new KeyObject(room, tempKey, new Point(1300, 105), new Size(35, 50));
            objectManager.AddObject(KeyObject);

            tempKey = room.NextObjKey;
            Floor Floor13 = new Floor(room, tempKey, new Point(1290, 165), new Size(150, 65));
            Floor13.SkinNum = 2;
            objectManager.AddObject(Floor13);

            #endregion

        }

    }
}