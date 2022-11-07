using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame
{
    public class GameObject : IDisposable
    {
        private bool _disposed = false;

        // 오브젝트를 구분하는 키
        public int key { get; set; }

        // 위치
        public Point Location { get; set; }

        // 크기
        public Size size { get; set; }

        // 화면에 표시 여부
        public bool isVisible { get; set; }

        // 충돌 검사 여부
        public bool Collision { get; set; }

        // 충돌이 발생했을 때 false이면 통과함
        public bool Blockable { get; set; }

        protected byte _type;
        public byte type { get {return _type; } }

        // 이미지
        protected Image _image;
        public Image image { get { return _image; } }

        public GameObject(int key, Point Location, Size size)
        {
            this.key = key;
            this.Location = Location;
            this.size = size;
            this.Collision = false;
            this.Blockable = false;
            this.isVisible = true;
            this._type = ObjectTypes.GAME_OBJECT;
        }

        ~GameObject() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // dispose 패턴
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // 관리 메모리 해제
            }

            // 비관리 메모리 해제
            _image.Dispose();
            _disposed = true;
        }

        // 플레이어와 겹쳤을 때 호출됨
        virtual public void OnHit()
        {

        }

        virtual public void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            var e = pe.Graphics;

            e.DrawImage(image, new Rectangle(Location, size));

        }

        virtual public void SetSkin(int skinNum)
        {
        }

    }
}
        