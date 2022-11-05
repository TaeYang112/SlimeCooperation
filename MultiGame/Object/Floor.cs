using MultiGameModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.Object
{
    public class Floor : GameObject
    {
        private Image _image2;
        private int _image2_Height = 0;
        private TextureBrush tBrush1;
        private TextureBrush tBrush2;
        bool singleImage = false;

        public Floor(int key, Point Location, Size size):
            base(key,Location,size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.FLOOR;
        }

        override public void SetSkin(int num)
        {
            singleImage = false;
            isVisible = true;
            switch(num)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = MultiGame.Properties.Resources.Grass_Dirt;
                    _image2 = MultiGame.Properties.Resources.Grass;
                    _image2_Height = _image2.Height;
                    break;
                case 1:
                    _image = MultiGame.Properties.Resources.dirt_snow;
                    _image2 = MultiGame.Properties.Resources.snow;
                    _image2_Height = _image2.Height;
                    break;
                case 2:
                    _image = MultiGame.Properties.Resources.Lava_Dirt;
                    _image2 = MultiGame.Properties.Resources.Lava_Grass;
                    _image2_Height = _image2.Height;
                    break;
                default:
                    _image = MultiGame.Properties.Resources.Grass_Dirt;
                    _image2 = MultiGame.Properties.Resources.Grass;
                    _image2_Height = _image2.Height;
                    break;
            }
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
           // attributes.
            tBrush1 = new TextureBrush(_image);
            tBrush2 = new TextureBrush(_image2);
            tBrush2.Transform = new System.Drawing.Drawing2D.Matrix(
          1.0f,
          0.0f,
          0.0f,
          1.0f,
          0.0f,
          Location.Y);

        }
        
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            if (isVisible == false) return;

            g.FillRectangle(tBrush1, new Rectangle(Location, size));
            
            

            if (singleImage == false)
                g.FillRectangle(tBrush2, new Rectangle(Location, new Size(size.Width, _image2_Height)));

        }
    }
}
