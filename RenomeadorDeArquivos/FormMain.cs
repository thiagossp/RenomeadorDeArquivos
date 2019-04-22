using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace RenomeadorDeArquivos
{
    public partial class FormMain : Form
    {
        private List<Image> imageList = new List<Image>();
        private ExcelTable excelTable = new ExcelTable();

        public FormMain()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Pronto";
        }

        private void BtnSelecionarImagens_Click(object sender, EventArgs e)
        {
            textBoxImages.Clear();
            imageList.Clear();
            toolStripStatusLabel1.Text = "Carregando Imagens...";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos de Imagem|*.jpg; *.tif",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = openFileDialog.FileNames.Length;
                foreach (string path in openFileDialog.FileNames)
                {
                    imageList.Add(new Image(path));
                    textBoxImages.AppendText(path + "\n");
                    toolStripProgressBar1.Value++;
                }
                toolStripStatusLabel1.Text = "Pronto";
            }
        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageList.Count <= 0)
                {
                    throw new System.ArgumentException("Nenhuma imagem foi selecionada.");
                }
                if (excelTable.Patch == null)
                {
                    throw new System.ArgumentException("Nenhuma planilha foi selecionada.");
                }

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = System.IO.Path.GetDirectoryName(imageList[0].FullPatch);
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Maximum = imageList.Count;
                    toolStripStatusLabel1.Text = "Processando...";
                    foreach (Image img in imageList)
                    {
                        DataTable dataTable = excelTable.DataTable;
                        DataRow[] dataRow = dataTable.Select(dataTable.Columns[comboBoxNomeAtual.SelectedIndex].ToString() + " = '" + img.Name + img.Extension + "'");
                        if (dataRow.Length == 1)
                            img.Rename(dataRow[0][comboBoxNomeNovo.SelectedIndex].ToString(), checkBoxSha1.Checked);
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show($"O arquivo {img.Name} não possui referênciaa na tabela.\n\nDeseja continuar mesmo assim? (Os nomes originais serão mantidos)", "Atenção", MessageBoxButtons.YesNo);
                            if (dialogResult != DialogResult.Yes)
                                throw new System.ArgumentException("Processamento cancelado.");
                        }
                        img.Save(folderBrowserDialog.SelectedPath);
                        toolStripProgressBar1.Value++;
                    }
                    MessageBox.Show("Processamento concluído.", "Processamento");
                    toolStripStatusLabel1.Text = "Pronto";
                }
                else
                    throw new System.ArgumentException("Erro ao selecionar a pasta de destino.");
            }

            catch (System.IndexOutOfRangeException ex)
            {
                MessageBox.Show("É obrigatório selecinar as colunas que contêm os nomes atuais e novos.\n" + ex);
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex, "Erro ao processar");
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSelecionarPlanilha_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Planilhas Excel|*.xlsx;*.xls",
                    Multiselect = false
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelTable.LoadFile(openFileDialog.FileName.ToString());
                    dataGridView1.DataSource = excelTable.DataTable;
                    comboBoxNomeAtual.Items.Clear();
                    comboBoxNomeAtual.Items.AddRange(excelTable.GetColumnsNames());
                    comboBoxNomeNovo.Items.Clear();
                    comboBoxNomeNovo.Items.AddRange(excelTable.GetColumnsNames());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o arquivo.\nErro: " + ex.Message);
            }
        }
    }
}
