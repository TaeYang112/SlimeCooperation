using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class ObjectManager
    {
        public ConcurrentDictionary<int, GameObject> ObjectDic { get; set; }

        // 오브젝트관리를 위에 부여할 키
        private int key;

        public ObjectManager()
        {
            ObjectDic = new ConcurrentDictionary<int, GameObject>();
            key = 0;
        }

        public GameObject AddObject(GameObject gameObject)
        {
            // 새로운 오브젝트를 배열에 저장
            ObjectDic.TryAdd(key, gameObject);

            return gameObject;
        }

        public void RemoveObejct(GameObject gameObject)
        {
            ObjectDic.TryRemove(gameObject.key, out _);
        }

        public void RemoveObejct(int key)
        {
            ObjectDic.TryRemove(key, out _);
        }

        public void ClearObjects()
        {
            ObjectDic.Clear();
        }

        public int NextKey()
        {
            return ++key;
        }
    }
}
