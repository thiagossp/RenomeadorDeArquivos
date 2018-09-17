using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RenomeadorDeArquivos
{
    class Images
    {
        public List<Image> ImageList { get; set; }

        public Images(string[] patch)
        {
            ImageList = new List<Image>();
            foreach (string p in patch)
            {
                Image image = new Image(p);
                ImageList.Add(image);
            }
        }

        public int Rename (DataTable dataTable, int oldReferenceName, int newReferenceName, bool crypto = false)
        {

            int renamedCounter = 0;
            Security security = new Security();
            foreach (Image image in ImageList)
            {
                if (dataTable.Columns[oldReferenceName].DataType != System.Type.GetType("System.String"))
                    throw new System.ArgumentException("A coluna de referência contendo o nome atual do arquivos é inválida");
                DataRow[] dataRow = dataTable.Select(dataTable.Columns[oldReferenceName].ToString() + " = '" + image.Name + image.Extension + "'");
                if (dataRow.Count() == 1)
                {
                    if (crypto)
                        image.Name = security.StringToSha1(dataRow[0][newReferenceName].ToString());
                    else
                    {
                        image.Name = dataRow[0][newReferenceName].ToString();
                    }
                    renamedCounter++;
                }
                
            }
            return renamedCounter;
        }

        public bool Save (string folderPath, bool optimizeAll = false)
        {
            foreach (Image image in this.ImageList)
            {
                if (image.Pages > 1)
                    throw new System.ArgumentException("Não é permitido arquivos de imagens com multiplas páginas (" + image.Name + ")");
                if ((image.Optimize) || (optimizeAll))
                    OptimezeImage(image, folderPath);
                else
                    System.IO.File.Copy(image.FullPatch, folderPath + "\\" + image.Name + ".jpg");
            }
            System.Diagnostics.Process.Start("explorer.exe", folderPath);
            return true;
        }

        private void OptimezeImage (Image image, string folderPatch)
        {
            folderPatch = folderPatch + "\\" + image.Name + ".jpg";
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(image.FullPatch))
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
                    bmp.Save(folderPatch, jpgEncoder, encoderParameters);
                }
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
