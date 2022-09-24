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
            GameObject Block1 = new GameObject(tempKey, new Point(300, 350), new Size(50, 50));

            objectManager.AddObject(Block1);
        }

    }
}
