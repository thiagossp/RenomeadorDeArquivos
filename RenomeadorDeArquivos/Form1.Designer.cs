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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelecionarImagens
            // 
            this.btnSelecionarImagens.Location = new System.Drawing.Point(12, 12);
            this.btnSelecionarImagens.Name = "btnSelecionarImagens";
            this.btnSelecionarImagens.Size = new System.Drawing.Size(75, 23);
            this.btnSelecionarImagens.TabIndex = 0;
            this.btnSelecionarImagens.Text = "Imagens";
            this.btnSelecionarImagens.UseVisualStyleBackColor = true;
            this.btnSelecionarImagens.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(556, 94);
            this.textBox1.TabIndex = 1;
            // 
            // btnSelecionarPlanilha
            // 
            this.btnSelecionarPlanilha.Location = new System.Drawing.Point(12, 140);
            this.btnSelecionarPlanilha.Name = "btnSelecionarPlanilha";
            this.btnSelecionarPlanilha.Size = new System.Drawing.Size(75, 23);
            this.btnSelecionarPlanilha.TabIndex = 2;
            this.btnSelecionarPlanilha.Text = "Planilha";
            this.btnSelecionarPlanilha.UseVisualStyleBackColor = true;
            this.btnSelecionarPlanilha.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 169);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(556, 20);
            this.textBox2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 195);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(556, 150);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(12, 458);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(75, 23);
            this.btnProcessar.TabIndex = 5;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxSha1
            // 
            this.checkBoxSha1.AutoSize = true;
            this.checkBoxSha1.Location = new System.Drawing.Point(12, 423);
            this.checkBoxSha1.Name = "checkBoxSha1";
            this.checkBoxSha1.Size = new System.Drawing.Size(213, 17);
            this.checkBoxSha1.TabIndex = 6;
            this.checkBoxSha1.Text = "Criptografar nomes das imagens (SHA1)";
            this.checkBoxSha1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Coluna contendo o nome atual do arquivo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Conluna contendo o novo nome do arquivo:";
            // 
            // comboBoxNomeAtual
            // 
            this.comboBoxNomeAtual.FormattingEnabled = true;
            this.comboBoxNomeAtual.Location = new System.Drawing.Point(237, 351);
            this.comboBoxNomeAtual.Name = "comboBoxNomeAtual";
            this.comboBoxNomeAtual.Size = new System.Drawing.Size(136, 21);
            this.comboBoxNomeAtual.TabIndex = 9;
            // 
            // comboBoxNomeNovo
            // 
            this.comboBoxNomeNovo.FormattingEnabled = true;
            this.comboBoxNomeNovo.Location = new System.Drawing.Point(237, 379);
            this.comboBoxNomeNovo.Name = "comboBoxNomeNovo";
            this.comboBoxNomeNovo.Size = new System.Drawing.Size(136, 21);
            this.comboBoxNomeNovo.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 487);
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
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

