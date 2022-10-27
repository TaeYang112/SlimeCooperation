using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiGame
{
    public class ResourceLibrary
    {

        private static ResourceLibrary inst = new ResourceLibrary();

        public PrivateFontCollection privateFont = new PrivateFontCollection();
        public static FontFamily[] Families
        {
            get
            {
                return inst.privateFont.Families;
            }
        }

        private Bitmap _portal;
        public static Bitmap Portal
        {
            get
            {
                return inst._portal;
            }
        }

        private Bitmap _lava;
        public static Bitmap Lava
        {
            get
            {
                return inst._lava;
            }
        }

        public ResourceLibrary()
        {
            AddFontFromMemory();

            _portal = MultiGame.Properties.Resources.portal_2;
            ImageAnimator.Animate(_portal, null);

            _lava = MultiGame.Properties.Resources.Lava;
            ImageAnimator.Animate(_lava, null);
        }

        private void AddFontFromMemory()
        {
            List<byte[]> fonts = new List<byte[]>();
            fonts.Add(Properties.Resources.EARLY_GAMEBOY);

            foreach (byte[] font in fonts)
            {
                IntPtr fontBuffer = Marshal.AllocCoTaskMem(font.Length);
                Marshal.Copy(font, 0, fontBuffer, font.Length);
                privateFont.AddMemoryFont(fontBuffer, font.Length);
            }
        }
    }
}
