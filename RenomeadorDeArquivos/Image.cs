using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenomeadorDeArquivos
{
    class Image
    {
        public string FullPatch { get; private set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public int Pages { get; private set; }
        public bool Optimize { get; set; }

        public Image (string patch, bool optimize = false)
        {
            this.FullPatch = patch;
            this.Name = System.IO.Path.GetFileNameWithoutExtension(patch);
            this.Extension = System.IO.Path.GetExtension(patch);
            this.Optimize = optimize;
            this.Pages = PageCounter();
        }

        private int PageCounter ()
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(this.FullPatch))
            {
                FrameDimension frameDimension = new FrameDimension(img.FrameDimensionsList[0]);
                return img.GetFrameCount(frameDimension);                
            }
        }
    }
}
