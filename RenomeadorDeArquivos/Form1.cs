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

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            foreach (string caminho in openFileDialog1.FileNames)
            {
                textBox1.AppendText(caminho + "\n");
                imagens.Add(caminho);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Planilhas Excel|*.xlsx;*.xls";
            openFileDialog2.Multiselect = false;
            
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog2.FileName.ToString();         
                planilha = GetTabelaExcel(openFileDialog2.FileName.ToString());
                dataGridView1.DataSource = planilha;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (String img in imagens)
            {
                string nomeDaImagem = System.IO.Path.GetFileName(img);
                foreach (DataRow row in planilha.Rows)
                {
                    if (row[2].ToString() == nomeDaImagem)
                    {
                        //SHA1 sha = new SHA1CryptoServiceProvider();
                        //string NomeEmSha = sha.(row[1].ToString());
                        MessageBox.Show("Encontrado: " + nomeDaImagem);
                        System.IO.File.Copy(img, @"E:\Projetos\C#\RenomeadorDeArquivos\Arquivos para testes\exportacao\\" + row[1].ToString() + ".jpg");
                    }

                }
            }
            
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
