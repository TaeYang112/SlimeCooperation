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
            int tempKey = objectManager.NextKey();
            Floor Floor1 = new Floor(tempKey, new Point(0, 390), new Size(800, 60));
            Floor1.SkinNum = 0;

            objectManager.AddObject(Floor1);


            tempKey = objectManager.NextKey();
            Floor Floor2 = new Floor(tempKey, new Point(200, 290), new Size(100, 100));
            Floor1.SkinNum = 0;

            objectManager.AddObject(Floor2);
        }

    }
}
