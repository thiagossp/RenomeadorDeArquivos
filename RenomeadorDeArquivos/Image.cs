using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RenomeadorDeArquivos
{
    class Image
    {
        public string FullPatch { get; private set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public int Pages { get; private set; }

        public Image(string patch)
        {
            FullPatch = patch;
            Name = System.IO.Path.GetFileNameWithoutExtension(patch);
            Extension = System.IO.Path.GetExtension(patch);
            Pages = PageCounter();
        }

        private int PageCounter()
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(this.FullPatch))
            {
                FrameDimension frameDimension = new FrameDimension(img.FrameDimensionsList[0]);
                return img.GetFrameCount(frameDimension);
            }
        }

        public void Rename(string newName, bool crypto = false)
        {            
            if (crypto)
            {
                Security security = new Security();
                Name = security.StringToSha1(newName);
            }
            else
                Name = newName;
        }

        public void Save(string newFolderPath, bool optimize = false)
        {
            try
            {
                if (optimize)
                    OptimizeImage(newFolderPath);
                else
                    System.IO.File.Copy(FullPatch, newFolderPath + "\\" + Name + ".jpg");
            }
            catch (System.IO.IOException ex)
            {
                DialogResult dialogResult = MessageBox.Show(ex.Message + "\n\nO arquivo não será processado.\n\nDeseja continuar com o processamento?", "Atenção", MessageBoxButtons.YesNo);
                if (dialogResult != DialogResult.Yes)
                    throw new System.ArgumentException("Processamento cancelado.");
            }
        }

        private void OptimizeImage(string newFolderPath)
        {
            try
            {

                if (Pages > 1)
                    throw new System.ArgumentException("Não é permitido arquivos de imagens com multiplas páginas (" + Name + ")");
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(FullPatch))
                {
                    double reductionFactor = 750 / Convert.ToDouble(img.Height);
                    int newVerticalResolution = Convert.ToInt32(Math.Round(reductionFactor * img.Height));
                    int newHorizontalResolution = Convert.ToInt32(Math.Round(reductionFactor * img.Width));
                    using (Bitmap bmp = new Bitmap(img, new Size(newHorizontalResolution, newVerticalResolution)))
                    {
                        ImageCodecInfo jpgEncoder = GetEncoderInfo("image/jpeg");
                        Encoder encoder = Encoder.Quality;
                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        EncoderParameter encoderParameter = new EncoderParameter(encoder, 25L);
                        encoderParameters.Param[0] = encoderParameter;
                        bmp.Save(newFolderPath, jpgEncoder, encoderParameters);
                    }
                }
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

    }
}

