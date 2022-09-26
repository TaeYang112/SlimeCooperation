using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame.Object
{
    class KeyObject : GameObject
    {
        // 열쇠 소유자 키
        public int ownerKey { get; set; }

        public KeyObject(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = false;
            _image = MultiGame.Properties.Resources.Key.Clone() as Image;
        }

        public override void OnHit()
        {
            base.OnHit();
            Collision = false;
            Console.WriteLine("충돌");
            GameManager.GetInstance().SendMessage($"ObjEvent#{key}#KeyObject@");
        }
    }
}
