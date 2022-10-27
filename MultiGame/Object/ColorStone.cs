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
    public class ColorStone : GameObject
    {
        private Brush brush;

        public ColorStone(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = true;
            _type = ObjectTypes.COLOR_STONE;
            brush = Brushes.Red;
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
                    brush = Brushes.Red;
                    break;
                case 1:
                    brush = Brushes.Orange;
                    break;
                case 2:
                    brush = Brushes.Yellow;
                    break;
                case 3:
                    brush = Brushes.Green;
                    break;
                case 4:
                    brush = Brushes.Blue;
                    break;
                case 5:
                    brush = Brushes.Purple;
                    break;
                case 6:
                    brush = Brushes.Pink;
                    break;
                case 7:
                    brush = Brushes.Gray;

                    break;
            }
        
        }
        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            if (isVisible == false) return;
            Graphics g= pe.Graphics;

            g.FillRectangle(brush, new Rectangle(Location, size));
        }


        public override void OnHit()
        {
            base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key).AddByte(type);
            byte[] message = generator.Generate();

            GameManager.GetInstance().SendMessage(message);
        }

    }
}
