using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using RenomeadorDeArquivos.Entities.Exceptions;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RenomeadorDeArquivos
{
    class Image
    {
        public string FullPatch { get; private set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }

        public Image(string patch)
        {
            FullPatch = patch;
            Name = System.IO.Path.GetFileNameWithoutExtension(patch);
            Extension = System.IO.Path.GetExtension(patch);
            CheckMultiplesPages();
        }

        private void CheckMultiplesPages()
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(FullPatch))
            {
                FrameDimension frameDimension = new FrameDimension(img.FrameDimensionsList[0]);
                if (img.GetFrameCount(frameDimension) > 1)
                    throw new ImageException($"Image files with multiples pages are not permiss (File: {Name})");
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
                    throw new ImageException("Processing Canceled.");
            }
        }

        private void OptimizeImage(string newFolderPath)
        {
            try
            {
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
            catch (System.IO.IOException exception)
            {
                throw new ImageException(exception.Message);
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

