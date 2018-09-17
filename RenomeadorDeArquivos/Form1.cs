using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RenomeadorDeArquivos
{
    public partial class Form1 : Form
    {
        List<string> imagens = new List<string>();
        Images images;
        ExcelTable excelTable = new ExcelTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelecionarImagens_Click(object sender, EventArgs e)
        {
            textBoxImages.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos de Imagem|*.jpg; *.tif",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                images = new Images(openFileDialog.FileNames);
                
                foreach (string caminho in openFileDialog.FileNames)
                {
                    textBoxImages.AppendText(caminho + "\n");
                }
            }
        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (images.ImageList.Count <= 0)
                {
                    throw new System.ArgumentException("Nenhuma imagem foi selecionada.");
                }
                if (textBox2.Text ==  "")
                {
                    throw new System.ArgumentException("Nenhuma planilha foi selecionada.");
                }

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    int differenceCount = images.ImageList.Count - images.Rename(excelTable.DataTable, comboBoxNomeAtual.SelectedIndex, comboBoxNomeNovo.SelectedIndex, checkBoxSha1.Checked);
                    if (differenceCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Foram encontrados arquivos de imagens que não possuem referências na tabela.\n\n" + "Deseja continuar mesmo assim? (Os nomes originais serão mantidos)", "Atenção", MessageBoxButtons.YesNo);
                        if (dialogResult != DialogResult.Yes)
                            throw new System.ArgumentException("Processamento cancelado.");
                    }

                    if (images.Save(folderBrowserDialog.SelectedPath, checkBoxOptimizeAll.Checked))
                        MessageBox.Show("Processamento concluído.");
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

        private  void Form1_Load(object sender, EventArgs e)
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
                    textBox2.Text = openFileDialog.FileName.ToString();
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
