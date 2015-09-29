namespace KukaDraw.IHM
{
    partial class OpenSVGForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbPathFile = new System.Windows.Forms.TextBox();
            this.bOpen = new System.Windows.Forms.Button();
            this.pbFileSVG = new System.Windows.Forms.PictureBox();
            this.bDraw = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbFileSVG)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chemin du fichier";
            // 
            // tbPathFile
            // 
            this.tbPathFile.Location = new System.Drawing.Point(107, 10);
            this.tbPathFile.Name = "tbPathFile";
            this.tbPathFile.Size = new System.Drawing.Size(484, 20);
            this.tbPathFile.TabIndex = 1;
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(597, 8);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 2;
            this.bOpen.Text = "Ouvrir";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // pbFileSVG
            // 
            this.pbFileSVG.Location = new System.Drawing.Point(12, 36);
            this.pbFileSVG.Name = "pbFileSVG";
            this.pbFileSVG.Size = new System.Drawing.Size(660, 660);
            this.pbFileSVG.TabIndex = 3;
            this.pbFileSVG.TabStop = false;
            // 
            // bDraw
            // 
            this.bDraw.Location = new System.Drawing.Point(12, 702);
            this.bDraw.Name = "bDraw";
            this.bDraw.Size = new System.Drawing.Size(663, 62);
            this.bDraw.TabIndex = 4;
            this.bDraw.Text = "Dessiner";
            this.bDraw.UseVisualStyleBackColor = true;
            // 
            // OpenSVGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 776);
            this.Controls.Add(this.bDraw);
            this.Controls.Add(this.pbFileSVG);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.tbPathFile);
            this.Controls.Add(this.label1);
            this.Name = "OpenSVGForm";
            this.Text = "OpenSVG";
            ((System.ComponentModel.ISupportInitialize)(this.pbFileSVG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPathFile;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.PictureBox pbFileSVG;
        private System.Windows.Forms.Button bDraw;

    }
}