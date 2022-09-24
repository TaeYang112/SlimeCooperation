using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class MapBase
    {
        public ObjectManager objectManager{ get; set; }

        public MapBase()
        {
            objectManager = new ObjectManager();
        }

    }
}
