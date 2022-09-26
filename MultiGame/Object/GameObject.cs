using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame
{
    public class GameObject
    {
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
        }

        // 플레이어와 겹쳤을 때 호출됨
        virtual public void OnHit()
        {

        }

        virtual public void OnPaint(object obj, PaintEventArgs pe)
        {
            var e = pe.Graphics;

            if (isVisible == false) return;
            e.DrawImage(image, new Rectangle(Location, size));

        }

        virtual public void SetSkin(int skinNum)
        {
        }
    }
}
        