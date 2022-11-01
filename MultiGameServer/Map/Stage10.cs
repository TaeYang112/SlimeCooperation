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
    class Stage10 : MapBase
    {
        private List<int> randomSkin;

        private TimerBox timerbox1;
        private TimerBox timerbox2;
        private TimerBox timerbox3;
        private TimerBoard timerBoard;
        public Stage10(Room room) : base(room)
        {
        }

        public override void Start()
        {
            base.Start();
            timerbox1.TimerStart();
            timerbox2.TimerStart();
            timerbox3.TimerStart();
            timerBoard.TimerStart();

        }
        protected override void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(0, 740);
            SpawnLocation[1] = new Point(100, 740);
            SpawnLocation[2] = new Point(200, 740);
        }

        protected override void DesignMap()
        {
            InitRandomSkin();
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


            // 타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox = new TimerBox(room, tempKey, new Point(450, 720), new Size(150, 100));
            timerBox.StartTime = 10000;
            this.timerbox1 = timerBox;
            timerBox.SetTimerStopAction(delegate () { this.timerBoard.TimerStop(timerBox.StartTime - timerBox.Time); });
            objectManager.AddObject(timerBox);

            // 버튼
            tempKey = room.NextObjKey;
            Button button = new Button(room, tempKey, new Point(450, 790), new Size(20, 10));
            button.SetAction(delegate () { timerBox.TimerStop(); });
            objectManager.AddObject(button);



            // 타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox2 = new TimerBox(room, tempKey, new Point(650, 720), new Size(150, 100));
            timerBox2.StartTime = 10000;
            this.timerbox2 = timerBox2;
            timerBox2.SetTimerStopAction(delegate () { this.timerBoard.TimerStop(timerBox2.StartTime - timerbox2.Time); });
            objectManager.AddObject(timerBox2);

            // 버튼
            tempKey = room.NextObjKey;
            Button button2 = new Button(room, tempKey, new Point(650, 790), new Size(20, 10));
            button2.SetAction(delegate () { timerBox2.TimerStop(); ; });
            objectManager.AddObject(button2);



            // 타이머
            tempKey = room.NextObjKey;
            TimerBox timerBox3 = new TimerBox(room, tempKey, new Point(850, 720), new Size(150, 100));
            timerBox3.StartTime = 10000;
            this.timerbox3 = timerBox3;
            timerBox3.SetTimerStopAction(delegate () { this.timerBoard.TimerStop(timerBox3.StartTime - timerbox3.Time); });
            objectManager.AddObject(timerBox3);

            // 버튼
            tempKey = room.NextObjKey;
            Button button3 = new Button(room, tempKey, new Point(850, 790), new Size(20, 10));
            button3.SetAction(delegate () { timerBox3.TimerStop(); });
            objectManager.AddObject(button3);


            // 타이머 보드
            tempKey = room.NextObjKey;
            TimerBoard timerBoard = new TimerBoard(room, tempKey, new Point(650, 0), new Size(400, 100),3);
            timerBoard.StartTime = 30000;
            timerBoard.MinTime = 0;
            timerBoard.MaxTime = 1000;
            timerBoard.SetTimerNotMatchAction(delegate () { room.AllDie(); });
            this.timerBoard = timerBoard;
            objectManager.AddObject(timerBoard);

            // 땅
            tempKey = room.NextObjKey;
            Floor Floor = new Floor(room, tempKey, new Point(0, 800), new Point(1440, 865));
            Floor.SkinNum = 2;
            objectManager.AddObject(Floor);

            
        }

        private void InitRandomSkin()
        {
            randomSkin = new List<int>(new int[] { 0, 0, 0 });
            
            List<int> skinArr = new List<int>();
            int i = 0;
            foreach(var item in room.roomClientDic)
            {
                skinArr.Add(item.Value.SkinNum);
                i++;
            }

            Random rand = new Random(); //랜덤선언

            for(int j = 0; j<2; j++)
            {
                //선언 및 초기화 부분//
                int num = rand.Next(skinArr.Count);

                randomSkin[j] = skinArr[num];

                if(skinArr.Count > 1)
                    skinArr.RemoveAt(num);
            }
            randomSkin[2] = skinArr[0];

        }
    }
}
