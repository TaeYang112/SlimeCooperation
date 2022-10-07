using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class MapBase
    {
        protected Point[] SpawnLocation;

        protected Room room;

        public ObjectManager objectManager { get; set; }

       

        public MapBase(Room room)
        {
            this.room = room;

            objectManager = new ObjectManager();
            SpawnLocation = new Point[3];

            SetSpawnLocation();
            DesignMap();
        }

        protected virtual void SetSpawnLocation()
        {
            SpawnLocation[0] = new Point(0,330);
            SpawnLocation[1] = new Point(100,330);
            SpawnLocation[2] = new Point(200,330);
        }

        protected virtual void DesignMap()
        {

        }

        public Point GetSpawnLocation(int num)
        {
            return SpawnLocation[num];
        }

    }
}
