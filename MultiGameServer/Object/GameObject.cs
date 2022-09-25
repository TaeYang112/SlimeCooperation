using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class GameObject
    {
        // 오브젝트를 구분하는 키
        public int key { get; set; }

        // 타입
        protected string _type;
        public string Type { get { return _type; } }

        // 스킨 번호
        public int SkinNum { get; set; }

        // 위치
        public Point Location { get; set; }

        // 크기
        public Size size { get; set; }

        // 충돌 검사 여부
        public bool CollisionEnable { get; set; }

        public GameObject(int key, Point Location, Size size)
        {
            this.key = key;
            this.Location = Location;
            this.size = size;
            this.CollisionEnable = false;
            this._type = "object";
            SkinNum = 0;
        }

        // 플레이어와 겹쳤을 때 호출됨
        virtual public void OnHit()
        {

        }
    }
}
