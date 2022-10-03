using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.Object
{
    public class Stone : GameObject
    {

        public int weight {get;set;}
        private Font font;
        private StringFormat format;

        public Stone(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = "Stone";

            _image = MultiGame.Properties.Resources.Floor1.Clone() as Image;
            font = new Font("맑은고딕", 20, FontStyle.Regular);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            weight = 0;
        }

        public override void SetSkin(int skinNum)
        {
            base.SetSkin(skinNum);
            switch(skinNum)
            {
                case 0:
                    _image = MultiGame.Properties.Resources.Stone.Clone() as Image;
                    break;
            }
        
        }
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            base.OnPaint(obj, pe);
            Graphics g= pe.Graphics;

            g.DrawString(weight.ToString(), font, Brushes.Black, new RectangleF(Location,size), format);
        }


        public override void OnHit()
        {
            base.OnHit();
            GameManager.GetInstance().SendMessage($"ObjEvent#{key}#Stone@");
        }

    }
}
