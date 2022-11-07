using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGameServer
{
    public class GameObject : IDisposable
    {
        // Dispose 체크 플래그 변수
        private bool _disposed = false;

        // 오브젝트를 구분하는 키
        public int key { get; set; }

        public Room room { get; set; }

        // 타입
        protected byte _type;
        public byte Type { get { return _type; } }

        // 스킨 번호
        public int SkinNum { get; set; }

        // 위치
        public Point Location { get; set; }

        // 크기
        public Size size { get; set; }

        // 충돌 검사 여부
        public bool Collision { get; set; }

        // 충돌이 발생했을 때 false이면 통과함
        public bool Blockable { get; set; }

        // 움직임이 정적인지
        public bool IsStatic { get; set; }

        public GameObject(Room room, int key, Point Location, Size size)
        {
            this.key = key;
            this.Location = Location;
            this.size = size;
            this.Collision = false;
            this.Blockable = false;
            this._type = ObjectTypes.GAME_OBJECT;
            this.room = room;
            this.IsStatic = true;
            SkinNum = 0;
        }

        ~GameObject() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // 가상 Dispose 메서드
        protected virtual void Dispose(bool isDisposing)
        {
            // Dispose 는 한 번만 수행되도록 합니다.
            if (_disposed)
                return;

            if (isDisposing)
            {
                // 해당 구문에 관리 리소스 정리합니다.

            }
            _disposed = true;
        }

        // 오브젝트와 상호작용이 일어났을 때 발생
        virtual public void OnEvent(EventParam param)
        {

        }

        // 맵이 시작 되었을 때 호출됨
        virtual public void OnStart()
        {

        }

        

        public class EventParam
        {
            public ClientCharacter clientCharacter { get; set; }
            public object[] Param { get; set; }
            private EventParam()
            {
            }
            public EventParam(ClientCharacter clientChar)
            {
                this.clientCharacter = clientChar;
            }
        }
    }
}
