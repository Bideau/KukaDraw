namespace KukaDraw.IHM
{
    partial class MenuForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.bDrawRealTime = new System.Windows.Forms.Button();
            this.bDraw = new System.Windows.Forms.Button();
            this.bDrawSVG = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bConnect);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connexion";
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(385, 15);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(103, 23);
            this.bConnect.TabIndex = 4;
            this.bConnect.Text = "Connexion";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(279, 17);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 21);
            this.tbPort.TabIndex = 3;
            this.tbPort.Text = "30002";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port :";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(77, 17);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(157, 21);
            this.tbAddress.TabIndex = 1;
            this.tbAddress.Text = "192.168.1.7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adresse IP :";
            // 
            // gbMenu
            // 
            this.gbMenu.Controls.Add(this.button4);
            this.gbMenu.Controls.Add(this.bDrawRealTime);
            this.gbMenu.Controls.Add(this.bDraw);
            this.gbMenu.Controls.Add(this.bDrawSVG);
            this.gbMenu.Location = new System.Drawing.Point(13, 124);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(494, 189);
            this.gbMenu.TabIndex = 1;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "Menu";
            this.gbMenu.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(261, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(227, 75);
            this.button4.TabIndex = 3;
            this.button4.Text = "Ecrire";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // bDrawRealTime
            // 
            this.bDrawRealTime.Location = new System.Drawing.Point(261, 101);
            this.bDrawRealTime.Name = "bDrawRealTime";
            this.bDrawRealTime.Size = new System.Drawing.Size(227, 75);
            this.bDrawRealTime.TabIndex = 2;
            this.bDrawRealTime.Text = "Dessin Temps réél";
            this.bDrawRealTime.UseVisualStyleBackColor = true;
            this.bDrawRealTime.Click += new System.EventHandler(this.bDrawRealTime_Click);
            // 
            // bDraw
            // 
            this.bDraw.Location = new System.Drawing.Point(6, 101);
            this.bDraw.Name = "bDraw";
            this.bDraw.Size = new System.Drawing.Size(227, 75);
            this.bDraw.TabIndex = 1;
            this.bDraw.Text = "Dessiner";
            this.bDraw.UseVisualStyleBackColor = true;
            this.bDraw.Click += new System.EventHandler(this.bDraw_Click);
            // 
            // bDrawSVG
            // 
            this.bDrawSVG.Location = new System.Drawing.Point(7, 20);
            this.bDrawSVG.Name = "bDrawSVG";
            this.bDrawSVG.Size = new System.Drawing.Size(227, 75);
            this.bDrawSVG.TabIndex = 0;
            this.bDrawSVG.Text = "Dessiner une Image SVG";
            this.bDrawSVG.UseVisualStyleBackColor = true;
            this.bDrawSVG.Click += new System.EventHandler(this.bDrawSVG_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lStatus);
            this.groupBox3.Location = new System.Drawing.Point(13, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(494, 50);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(7, 17);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(68, 13);
            this.lStatus.TabIndex = 0;
            this.lStatus.Text = "Deconnecter";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 320);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbMenu);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbMenu.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button bDrawRealTime;
        private System.Windows.Forms.Button bDraw;
        private System.Windows.Forms.Button bDrawSVG;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lStatus;
    }
}