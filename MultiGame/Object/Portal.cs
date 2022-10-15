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
    public class Portal : GameObject
    {

        public Portal(int key, Point Location, Size size) :
            base(key, Location, size)
        {
            SetSkin(0);
            Collision = true;
            Blockable = false;
            _type = ObjectTypes.PORTAL;
            
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
                    _image = MultiGame.Properties.Resources.portal_2;
                    ImageAnimator.Animate(_image, null);
                    break;
            }
        
        }

        public override void OnPaint(object obj, PaintEventArgs pe)
        {
            base.OnPaint(obj, pe);
        }


        public override void OnHit()
        {
             base.OnHit();

            MessageGenerator generator = new MessageGenerator(Protocols.C_OBJECT_EVENT);
            generator.AddInt(key);
            generator.AddByte(type);

            GameManager.GetInstance().SendMessage(generator.Generate());
            
        }

    }
}
