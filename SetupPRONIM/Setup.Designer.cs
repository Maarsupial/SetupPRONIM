namespace SetupPRONIM
{
    partial class Setup
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode(" CM");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode(" LC");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode(" PP");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode(" AF");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode(" GP");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode(" CP");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode(" AR");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode(" TP");
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelStart = new System.Windows.Forms.Panel();
            this.panelShortcut = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panelCheckShortcut = new System.Windows.Forms.Panel();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.treeViewShortcut = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.panelInstall = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxProgress = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.backgroundInstaller = new System.ComponentModel.BackgroundWorker();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelStart.SuspendLayout();
            this.panelShortcut.SuspendLayout();
            this.panelCheckShortcut.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            this.panelInstall.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.buttonBack);
            this.panelButtons.Controls.Add(this.buttonNext);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Location = new System.Drawing.Point(-1, 309);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(499, 48);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(238, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "Voltar";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(324, 12);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Próximo";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(410, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "PRONIM GovBr Setup Wizard";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(177, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Esse assistente irá te guiar pela instalação de todos os \r\nsoftwares da GovBr.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(177, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "É recomendável que você feche todas as outras aplicações\r\nantes de executar esse " +
    "Setup. Isso fará com que seja possível\r\ninstalar e atualizar arquivos importante" +
    "s do sistema.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Pressione \"Próximo\" para continuar.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SetupPRONIM.Properties.Resources.setup_home;
            this.pictureBox1.Location = new System.Drawing.Point(0, -5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 314);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelStart
            // 
            this.panelStart.Controls.Add(this.pictureBox1);
            this.panelStart.Controls.Add(this.label1);
            this.panelStart.Controls.Add(this.label2);
            this.panelStart.Controls.Add(this.label3);
            this.panelStart.Controls.Add(this.label4);
            this.panelStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStart.Location = new System.Drawing.Point(0, 0);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(497, 357);
            this.panelStart.TabIndex = 3;
            // 
            // panelShortcut
            // 
            this.panelShortcut.Controls.Add(this.label9);
            this.panelShortcut.Controls.Add(this.label8);
            this.panelShortcut.Controls.Add(this.panelCheckShortcut);
            this.panelShortcut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShortcut.Location = new System.Drawing.Point(0, 0);
            this.panelShortcut.Name = "panelShortcut";
            this.panelShortcut.Size = new System.Drawing.Size(497, 357);
            this.panelShortcut.TabIndex = 6;
            this.panelShortcut.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Escolha quais atalhos do PRONIM GovBr serão criados.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Escolha de atalhos";
            // 
            // panelCheckShortcut
            // 
            this.panelCheckShortcut.BackColor = System.Drawing.SystemColors.Control;
            this.panelCheckShortcut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCheckShortcut.Controls.Add(this.groupBoxDescription);
            this.panelCheckShortcut.Controls.Add(this.label6);
            this.panelCheckShortcut.Controls.Add(this.treeViewShortcut);
            this.panelCheckShortcut.Controls.Add(this.label5);
            this.panelCheckShortcut.Location = new System.Drawing.Point(-1, 52);
            this.panelCheckShortcut.Name = "panelCheckShortcut";
            this.panelCheckShortcut.Size = new System.Drawing.Size(499, 258);
            this.panelCheckShortcut.TabIndex = 1;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Controls.Add(this.label7);
            this.groupBoxDescription.Location = new System.Drawing.Point(338, 74);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(136, 161);
            this.groupBoxDescription.TabIndex = 3;
            this.groupBoxDescription.TabStop = false;
            this.groupBoxDescription.Text = "Descrição";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(4, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 39);
            this.label7.TabIndex = 0;
            this.label7.Text = "Posicione seu mouse\r\nsobre um componente\r\npara ver sua descrição.\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "Selecione os atalhos que\r\ndeverão ser criados:";
            // 
            // treeViewShortcut
            // 
            this.treeViewShortcut.CheckBoxes = true;
            this.treeViewShortcut.Location = new System.Drawing.Point(177, 80);
            this.treeViewShortcut.Name = "treeViewShortcut";
            treeNode1.Name = "cm";
            treeNode1.Text = " CM";
            treeNode2.Name = "lc";
            treeNode2.Text = " LC";
            treeNode3.Name = "pp";
            treeNode3.Text = " PP";
            treeNode4.Name = "af";
            treeNode4.Text = " AF";
            treeNode5.Name = "gp";
            treeNode5.Text = " GP";
            treeNode6.Name = "cp";
            treeNode6.Text = " CP";
            treeNode7.Name = "ar";
            treeNode7.Text = " AR";
            treeNode8.Name = "tp";
            treeNode8.Text = " TP";
            this.treeViewShortcut.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeViewShortcut.Size = new System.Drawing.Size(146, 154);
            this.treeViewShortcut.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(406, 39);
            this.label5.TabIndex = 0;
            this.label5.Text = "Marque os atalhos que você deseja criar e desmarque os que você não deseja criar." +
    "\r\nOs atalhos serão criados após a finalização do Setup.\r\nPressione \"Próximo\" par" +
    "a continuar.";
            // 
            // panelInstall
            // 
            this.panelInstall.Controls.Add(this.label10);
            this.panelInstall.Controls.Add(this.label11);
            this.panelInstall.Controls.Add(this.panel1);
            this.panelInstall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInstall.Location = new System.Drawing.Point(0, 0);
            this.panelInstall.Name = "panelInstall";
            this.panelInstall.Size = new System.Drawing.Size(497, 357);
            this.panelInstall.TabIndex = 7;
            this.panelInstall.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(273, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Escolha quais atalhos do PRONIM GovBr serão criados.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Escolha de atalhos";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxProgress);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.labelProgress);
            this.panel1.Location = new System.Drawing.Point(-1, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 258);
            this.panel1.TabIndex = 0;
            // 
            // textBoxProgress
            // 
            this.textBoxProgress.Location = new System.Drawing.Point(23, 56);
            this.textBoxProgress.Name = "textBoxProgress";
            this.textBoxProgress.ReadOnly = true;
            this.textBoxProgress.Size = new System.Drawing.Size(450, 179);
            this.textBoxProgress.TabIndex = 3;
            this.textBoxProgress.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(23, 31);
            this.progressBar.Maximum = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(450, 18);
            this.progressBar.TabIndex = 1;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(20, 16);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(57, 13);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "Progresso:";
            // 
            // backgroundInstaller
            // 
            this.backgroundInstaller.WorkerReportsProgress = true;
            this.backgroundInstaller.WorkerSupportsCancellation = true;
            this.backgroundInstaller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundInstaller_DoWork);
            this.backgroundInstaller.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundInstaller_ProgressChanged);
            this.backgroundInstaller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundInstaller_RunWorkerCompleted);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(497, 357);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelStart);
            this.Controls.Add(this.panelShortcut);
            this.Controls.Add(this.panelInstall);
            this.MaximumSize = new System.Drawing.Size(513, 395);
            this.MinimumSize = new System.Drawing.Size(513, 395);
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup PRONIM";
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelStart.ResumeLayout(false);
            this.panelStart.PerformLayout();
            this.panelShortcut.ResumeLayout(false);
            this.panelShortcut.PerformLayout();
            this.panelCheckShortcut.ResumeLayout(false);
            this.panelCheckShortcut.PerformLayout();
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.panelInstall.ResumeLayout(false);
            this.panelInstall.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelStart;
        private System.Windows.Forms.Panel panelShortcut;
        private System.Windows.Forms.Panel panelCheckShortcut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView treeViewShortcut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelInstall;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.ComponentModel.BackgroundWorker backgroundInstaller;
        private System.Windows.Forms.RichTextBox textBoxProgress;
    }
}

