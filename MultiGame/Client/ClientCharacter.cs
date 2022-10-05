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
        // 오른쪽으로 이동했으면 true 아니면 false
        public bool MoveDirectionRight { get; set; }

        // 오른쪽을 보고있으면 true 아니면 false
        private bool _LookDirectionRight;
        public bool LookDirectionRight { get { return _LookDirectionRight; } }

        public bool IsReady { get; set; }

        public ClientCharacter(int key, Point Location, int skinNum)
            : base(key, Location, new Size(42,35))
        {
            IsReady = false;
            Collision = true;
            Blockable = true;

            // 이미지 관련
            MoveDirectionRight = true;
            _LookDirectionRight = true;

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
            if(MoveDirectionRight != LookDirectionRight)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _LookDirectionRight = MoveDirectionRight;
            }

            if (isVisible == false) return;

            Size siz = new Size(size.Width+1, size.Height+1);
            e.DrawImage(image,new Rectangle(Location, siz ));
            
        }


    }
}
