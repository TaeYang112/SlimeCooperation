using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
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

        private Bitmap _portal_r;
        public static Bitmap Portal2
        {
            get
            {
                return inst._portal_r;
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

        private Bitmap _fallingLava;
        public static Bitmap FallingLava
        {
            get
            {
                return inst._fallingLava;
            }
        }

        public ResourceLibrary()
        {
            AddFontFromMemory();

            _portal = MultiGame.Properties.Resources.portal_2;
            ImageAnimator.Animate(_portal, null);

            _portal_r = MultiGame.Properties.Resources.protal_right;
            ImageAnimator.Animate(_portal_r, null);

            _lava = MultiGame.Properties.Resources.Lava;
            ImageAnimator.Animate(_lava, null);

            _fallingLava = MultiGame.Properties.Resources.FallingLava;
            ImageAnimator.Animate(_fallingLava, null);
        }

        private void AddFontFromMemory()
        {
            /*
            List<byte[]> fonts = new List<byte[]>();
            fonts.Add(Properties.Resources.EARLY_GAMEBOY);
            fonts.Add(Properties.Resources.DungGeunMo);

            foreach (byte[] font in fonts)
            {
                IntPtr fontBuffer = Marshal.AllocCoTaskMem(font.Length);
                Marshal.Copy(font, 0, fontBuffer, font.Length);
                privateFont.AddMemoryFont(fontBuffer, font.Length);

                Marshal.FreeHGlobal(fontBuffer);//메모리 해제
            }
            */

            List<byte[]> fonts = new List<byte[]>();
            fonts.Add(Properties.Resources.EARLY_GAMEBOY);
            fonts.Add(Properties.Resources.DungGeunMo);

            foreach(byte[] font in fonts)
            {
                Stream fontStream = new MemoryStream(font);
                //create an unsafe memory block for the data
                System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                //create a buffer to read in to
                Byte[] fontData = new Byte[fontStream.Length];
                //fetch the font program from the resource
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                //copy the bytes to the unsafe memory block
                Marshal.Copy(fontData, 0, data, (int)fontStream.Length);

                // We HAVE to do this to register the font to the system (Weird .NET bug !)
                uint cFonts = 0;
                AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);

                //pass the font to the font collection
                privateFont.AddMemoryFont(data, (int)fontStream.Length);
                //close the resource stream
                fontStream.Close();
                //free the unsafe memory
                Marshal.FreeCoTaskMem(data);
            }

        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);


    }
}
