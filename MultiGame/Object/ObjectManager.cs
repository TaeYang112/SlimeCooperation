using MultiGame.Object;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame
{
    public class ObjectManager
    {
        public ConcurrentDictionary<int, GameObject> ObjectDic { get; set; }

        // 게임 내의 열쇠 객체으 ㅣ킷값
        public int keyObjectKey { get; set; }

        // 게임 내의 문의 킷값
        public int doorKey { get; set; }

        // 락 오브젝트
        private object _lockObj = new object();
        public object LockObj { get { return _lockObj; } }

        public ObjectManager()
        {
            ObjectDic = new ConcurrentDictionary<int, GameObject>();
        }

        public GameObject AddObject(GameObject gameObject)
        {
            lock (LockObj)
            {
                // 새로운 오브젝트를 배열에 저장
                bool result = ObjectDic.TryAdd(gameObject.key, gameObject);

                // 이미 존재함
                if (result == false)
                {
                    // 이미 존재하는 오브젝트 반환
                    ObjectDic.TryGetValue(gameObject.key, out gameObject);
                }
            }
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
            lock (LockObj)
            {
                foreach (var item in ObjectDic)
                {
                    item.Value.Dispose();
                }

                ObjectDic.Clear();
                doorKey = -1;
                keyObjectKey = -1;
            }
        }
    }
}
