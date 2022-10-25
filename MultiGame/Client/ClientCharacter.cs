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
        // 오른쪽을 보고있으면 true 아니면 false
        public bool LookDirectionRight { get; set; }

        public bool IsReady { get; set; }

        private int _skinNum = 0;
        public int SkinNum { get { return _skinNum; } set{SetSkin(_skinNum);} }

        private Image _rightImage;
        private Image _leftImage;
        private Image _DLeftImage;
        private Image _DRightImage;

        public ClientCharacter(int key, Point Location, int skinNum)
            : base(key, Location, new Size(60,50))
        {
            IsReady = false;
            Collision = true;
            Blockable = true;
            LookDirectionRight = true;

            SetSkin(skinNum);
        }

        override public void SetSkin(int skinNum)
        {
            _skinNum = skinNum;
            switch (skinNum % 8)
            {
                case 0:
                    _rightImage = MultiGame.Properties.Resources.Red;
                    _DRightImage = MultiGame.Properties.Resources.Red2;
                    break;
                case 1:
                    _rightImage = MultiGame.Properties.Resources.Orange;
                    _DRightImage = MultiGame.Properties.Resources.Orange2;
                    break;
                case 2:
                    _rightImage = MultiGame.Properties.Resources.Yellow;
                    _DRightImage = MultiGame.Properties.Resources.Yellow2;
                    break;
                case 3:
                    _rightImage = MultiGame.Properties.Resources.Green;
                    _DRightImage = MultiGame.Properties.Resources.Green2;
                    break;
                case 4:
                    _rightImage = MultiGame.Properties.Resources.Blue;
                    _DRightImage = MultiGame.Properties.Resources.Blue2;
                    break;
                case 5:
                    _rightImage = MultiGame.Properties.Resources.Purple;
                    _DRightImage = MultiGame.Properties.Resources.Purple2;
                    break;
                case 6:
                    _rightImage = MultiGame.Properties.Resources.Pink;
                    _DRightImage = MultiGame.Properties.Resources.Pink2;
                    break;
                case 7:
                    _rightImage = MultiGame.Properties.Resources.Gray;
                    _DRightImage = MultiGame.Properties.Resources.Gray2;
                    break;
            }

            _image = _rightImage;

            _leftImage = _rightImage.Clone() as Image;
            _leftImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            _DLeftImage = _DRightImage.Clone() as Image;
            _DLeftImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }


        public void GameStart()
        {
        }


        override public void OnPaint(object obj, PaintEventArgs pe)
        {
            var e = pe.Graphics;


            if (isVisible == false) return;

            Size siz = new Size(size.Width+1, size.Height+1);
            e.DrawImage(image,new Rectangle(Location, siz ));
            
        }

        public void SetDirection(bool bRight)
        {
            if (bRight)
            {
                _image = _rightImage;
            }
            else
            {
                _image = _leftImage;
            }
            LookDirectionRight = bRight;
        }

        public void SetDie(bool flag)
        {
            if(flag)
            {
                if (LookDirectionRight)
                    _image = _DRightImage;
                else
                    _image = _DLeftImage;
            }
            else
            {
                if (LookDirectionRight)
                    _image = _rightImage;
                else
                    _image = _leftImage;
            }
        }


    }
}
