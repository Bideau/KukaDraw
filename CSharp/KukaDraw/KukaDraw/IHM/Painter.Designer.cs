namespace KukaDraw.IHM
{
    partial class Painter
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
            this.pPainter = new System.Windows.Forms.Panel();
            this.bDraw = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pPainter
            // 
            this.pPainter.BackColor = System.Drawing.Color.White;
            this.pPainter.Location = new System.Drawing.Point(13, 13);
            this.pPainter.Name = "pPainter";
            this.pPainter.Size = new System.Drawing.Size(1188, 840);
            this.pPainter.TabIndex = 0;
            this.pPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseDown);
            this.pPainter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseMove);
            this.pPainter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseUp);
            // 
            // bDraw
            // 
            this.bDraw.Location = new System.Drawing.Point(1207, 13);
            this.bDraw.Name = "bDraw";
            this.bDraw.Size = new System.Drawing.Size(188, 97);
            this.bDraw.TabIndex = 1;
            this.bDraw.Text = "Dessiner";
            this.bDraw.UseVisualStyleBackColor = true;
            this.bDraw.Click += new System.EventHandler(this.bDraw_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(1207, 116);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(188, 97);
            this.bClear.TabIndex = 2;
            this.bClear.Text = "Effacer";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(1207, 219);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(188, 97);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "Sauvgarder";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // Painter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 865);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bDraw);
            this.Controls.Add(this.pPainter);
            this.Name = "Painter";
            this.Text = "Painter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pPainter;
        private System.Windows.Forms.Button bDraw;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bSave;

    }
}