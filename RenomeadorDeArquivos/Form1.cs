using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RenomeadorDeArquivos
{
    public partial class Form1 : Form
    {
        List<string> imagens = new List<string>();
        DataTable planilha;

        private DataTable GetTabelaExcel(string arquivoExcel)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string Ext = Path.GetExtension(arquivoExcel);
                string connectionString = "";
                if (Ext == ".xls")
                {
                    connectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + arquivoExcel + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                else if (Ext == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + arquivoExcel + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                OleDbCommand dbCommand = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dbCommand.Connection = dbConnection;
                dbConnection.Open();
                DataTable dtSchema;
                dtSchema = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string nomePlanilha = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                dbConnection.Close();
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * From [" + nomePlanilha + "]";
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(dataTable);
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        private void CarregaComboBox()
        {
            try
            {
                comboBoxNomeAtual.Items.Clear();
                comboBoxNomeNovo.Items.Clear();
                string[] nomeColunas = planilha.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();

                comboBoxNomeAtual.Items.AddRange(nomeColunas);
                comboBoxNomeNovo.Items.AddRange(nomeColunas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private string CalcularSha1 (string stringOriginal)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            StringBuilder gerarString = new StringBuilder();

            byte[] buffer = Encoding.Default.GetBytes(stringOriginal);
            buffer = sha1.ComputeHash(buffer);
            foreach (byte item in buffer)
            {
                gerarString.Append(item.ToString("x2"));
            }
            return(gerarString.ToString().ToUpper());
        }

        private void ConverterTiffEmJpeg()
        {
            try
            {
                foreach (string img in imagens)
                {
                    using (Image imagem = Image.FromFile(img))
                    {
                        FrameDimension frameDimension = new FrameDimension(imagem.FrameDimensionsList[0]);

                        int frameNum = imagem.GetFrameCount(frameDimension);
                        string[] jpegPaths = new string[frameNum];

                        for (int frame = 0; frame < frameNum; frame++)
                        {
                            imagem.SelectActiveFrame(frameDimension, frame);
                            using (Bitmap bmp = new Bitmap(imagem, new Size(744,1052)))
                            {
                                jpegPaths[frame] = String.Format("{0}\\{1}{2}.jpg",
                                    Path.GetDirectoryName(img),
                                    Path.GetFileNameWithoutExtension(img),
                                    frame);

                                ImageCodecInfo jpgEncoder = GetEncoderInfo("image/jpeg");

                                Encoder encoder = Encoder.Quality;
                                EncoderParameters encoderParameters = new EncoderParameters(1);
                                EncoderParameter encoderParameter = new EncoderParameter(encoder, 20L);
                                encoderParameters.Param[0] = encoderParameter;
                                bmp.Save(jpegPaths[frame], jpgEncoder, encoderParameters);
                            }
                        }
                    }
                }
                
            }
            catch
            {

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

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelecionarImagens_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            imagens.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos de Imagem|*.jpg; *.tif",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string caminho in openFileDialog.FileNames)
                {
                    textBox1.AppendText(caminho + "\n");
                    imagens.Add(caminho);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Planilhas Excel|*.xlsx;*.xls",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog.FileName.ToString();         
                planilha = GetTabelaExcel(openFileDialog.FileName.ToString());
                dataGridView1.DataSource = planilha;
                CarregaComboBox();
            }
        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagens.Count() == 0)
                {
                    throw new System.ArgumentException("Nenhuma imagem foi selecionada");
                }
                if (textBox2.Text ==  "")
                {
                    throw new System.ArgumentException("Nenhuma planilha foi selecionada");
                }

                int contCopiados = 0;
                int contDuplicados = 0;
                int contNomesNaoEncontrado = 0;

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    throw new System.ArgumentException("Erro ao selecionar a pasta de destino");

                foreach (string img in imagens)
                {
                    string nomeDaImagem = System.IO.Path.GetFileName(img);
                    foreach (DataRow row in planilha.Rows)
                    {
                        if (row[comboBoxNomeAtual.SelectedIndex].ToString() == nomeDaImagem)
                        {
                            string novoNomeImagem = row[comboBoxNomeNovo.SelectedIndex].ToString();
                            if (checkBoxSha1.Checked)
                                novoNomeImagem = CalcularSha1(novoNomeImagem);
                            if (System.IO.File.Exists(folderBrowserDialog.SelectedPath + "\\" + novoNomeImagem + ".jpg"))
                                contDuplicados++;
                            else
                            {
                                if (Path.GetExtension(img) == ".tif")
                                    MessageBox.Show("Imagem TIF encontrada");
                                else
                                    System.IO.File.Copy(img, folderBrowserDialog.SelectedPath + "\\" + novoNomeImagem + Path.GetExtension(img));
                                contCopiados++;
                            }

                        }
                    }
                }
                contNomesNaoEncontrado = imagens.Count() - (contCopiados + contDuplicados);
                MessageBox.Show
                    (
                        "Processamento concluido\n\n" + 
                        contCopiados.ToString() + " Arquivos processados com sucesso\n" +
                        contDuplicados.ToString() + " Arquivos duplicados e não processados\n"+
                        contNomesNaoEncontrado.ToString() + " Arquivos sem nomes correspondentes e não processados"
                    );
                System.Diagnostics.Process.Start("explorer.exe", folderBrowserDialog.SelectedPath);
            }
            catch (System.IndexOutOfRangeException ex)
            {
                MessageBox.Show("É obrigatório selecinar as colunas que contêm os nomes atuais e novos.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar\nErro: " + ex.Message);
            }     
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConverterTiffEmJpeg();
        }
    }
}
