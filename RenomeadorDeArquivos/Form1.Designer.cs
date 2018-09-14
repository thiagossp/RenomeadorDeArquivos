namespace RenomeadorDeArquivos
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSelecionarImagens = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelecionarPlanilha = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.checkBoxSha1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxNomeAtual = new System.Windows.Forms.ComboBox();
            this.comboBoxNomeNovo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelecionarImagens
            // 
            resources.ApplyResources(this.btnSelecionarImagens, "btnSelecionarImagens");
            this.btnSelecionarImagens.Name = "btnSelecionarImagens";
            this.btnSelecionarImagens.UseVisualStyleBackColor = true;
            this.btnSelecionarImagens.Click += new System.EventHandler(this.BtnSelecionarImagens_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // btnSelecionarPlanilha
            // 
            resources.ApplyResources(this.btnSelecionarPlanilha, "btnSelecionarPlanilha");
            this.btnSelecionarPlanilha.Name = "btnSelecionarPlanilha";
            this.btnSelecionarPlanilha.UseVisualStyleBackColor = true;
            this.btnSelecionarPlanilha.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            // 
            // btnProcessar
            // 
            resources.ApplyResources(this.btnProcessar, "btnProcessar");
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.BtnProcessar_Click);
            // 
            // checkBoxSha1
            // 
            resources.ApplyResources(this.checkBoxSha1, "checkBoxSha1");
            this.checkBoxSha1.Name = "checkBoxSha1";
            this.checkBoxSha1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBoxNomeAtual
            // 
            this.comboBoxNomeAtual.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxNomeAtual, "comboBoxNomeAtual");
            this.comboBoxNomeAtual.Name = "comboBoxNomeAtual";
            // 
            // comboBoxNomeNovo
            // 
            this.comboBoxNomeNovo.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxNomeNovo, "comboBoxNomeNovo");
            this.comboBoxNomeNovo.Name = "comboBoxNomeNovo";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxNomeNovo);
            this.Controls.Add(this.comboBoxNomeAtual);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSha1);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnSelecionarPlanilha);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSelecionarImagens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSelecionarImagens;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelecionarPlanilha;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.CheckBox checkBoxSha1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxNomeAtual;
        private System.Windows.Forms.ComboBox comboBoxNomeNovo;
        private System.Windows.Forms.Button button1;
    }
}

