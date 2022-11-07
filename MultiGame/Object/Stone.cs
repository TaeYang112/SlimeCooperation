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
            _type = ObjectTypes.STONE;

            font = new Font(ResourceLibrary.Families[0], 20, FontStyle.Regular);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            weight = 0;
        }

        ~Stone()
        {
            font.Dispose();
            format.Dispose();
            _image.Dispose();
        }

        public override void SetSkin(int skinNum)
        {
            isVisible = true;
            switch (skinNum)
            {
                case -1:
                    isVisible = false;
                    break;
                case 0:
                    _image = MultiGame.Properties.Resources.Stone;
                    break;
            }
        
        }
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            base.OnPaint(obj, pe);
            Graphics g= pe.Graphics;

            g.DrawString(weight.ToString(), font, Brushes.Black, new RectangleF(new Point(Location.X + 4,Location.Y),size), format);
        }


        public override void OnHit()
        {
            base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(ObjectTypes.STONE);
            byte[] message = generator.Generate();

            GameManager.GetInstance().SendMessage(message);
        }

    }
}
