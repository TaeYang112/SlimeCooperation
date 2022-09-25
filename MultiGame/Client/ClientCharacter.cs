using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MultiGame
{

    // Client 정보를 갖는 클래스
    public class ClientCharacter : GameObject
    {
        public bool bLookRight { get; set; }
        private bool FlipImage;

        public bool isVisible { get; set; }
        public bool isReady { get; set; }

        public ClientCharacter(int key, Point Location, int skinNum)
            : base(key, Location, new Size(60,50))
        {
            isReady = false;
            Collision = true;
             
            // 이미지 관련
            bLookRight = true;
            FlipImage = false;
            SetSkin(skinNum);
        }

        override public void SetSkin(int skinNum)
        {
            switch (skinNum % 8)
            {
                case 0:
                    _image = MultiGame.Properties.Resources.Red.Clone() as Image;
                    break;
                case 1:
                    _image = MultiGame.Properties.Resources.Orange.Clone() as Image;
                    break;
                case 2:
                    _image = MultiGame.Properties.Resources.Yellow.Clone() as Image;
                    break;
                case 3:
                    _image = MultiGame.Properties.Resources.Green.Clone() as Image;
                    break;
                case 4:
                    _image = MultiGame.Properties.Resources.Blue.Clone() as Image;
                    break;
                case 5:
                    _image = MultiGame.Properties.Resources.Purple.Clone() as Image;
                    break;
                case 6:
                    _image = MultiGame.Properties.Resources.Pink.Clone() as Image;
                    break;
                case 7:
                    _image = MultiGame.Properties.Resources.Gray.Clone() as Image;
                    break;
            }
        }
        public void GameStart()
        {
        }


        override public void OnPaint(object obj, PaintEventArgs pe)
        {
            var e = pe.Graphics;
            if(FlipImage)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                FlipImage = false;
            }
            e.DrawImage(image,new Rectangle(Location, size ));
            
        }

        public void SetLookDirection(bool bRight)
        {
            if (bRight != this.bLookRight)
            {
                FlipImage = true;
                this.bLookRight = bRight;
            }
        }


    }
}
