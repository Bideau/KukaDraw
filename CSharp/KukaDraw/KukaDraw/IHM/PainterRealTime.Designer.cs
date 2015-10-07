namespace KukaDraw.IHM
{
    partial class PainterRealTime
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
            this.bClear = new System.Windows.Forms.Button();
            this.pPainter = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(1058, 12);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(188, 97);
            this.bClear.TabIndex = 6;
            this.bClear.Text = "Effacer";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // pPainter
            // 
            this.pPainter.BackColor = System.Drawing.Color.White;
            this.pPainter.Location = new System.Drawing.Point(12, 12);
            this.pPainter.Name = "pPainter";
            this.pPainter.Size = new System.Drawing.Size(1040, 740);
            this.pPainter.TabIndex = 4;
            this.pPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseDown);
            this.pPainter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseMove);
            this.pPainter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pPainter_MouseUp);
            // 
            // PainterRealTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 760);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.pPainter);
            this.Name = "PainterRealTime";
            this.Text = "PainterRealTime";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Panel pPainter;
    }
}