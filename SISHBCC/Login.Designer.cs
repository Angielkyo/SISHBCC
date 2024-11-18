namespace SISHBCC
{
    partial class Login
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
            this.LUSUARIO = new System.Windows.Forms.Label();
            this.lCONTRASEÑA = new System.Windows.Forms.Label();
            this.USUARIO = new System.Windows.Forms.TextBox();
            this.CONTRASEÑA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lNUEVOUSUARIO = new System.Windows.Forms.LinkLabel();
            this.lMANUAL = new System.Windows.Forms.LinkLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.INGRESAR = new System.Windows.Forms.Button();
            this.pictureBoxOjo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOjo)).BeginInit();
            this.SuspendLayout();
            // 
            // LUSUARIO
            // 
            this.LUSUARIO.AutoSize = true;
            this.LUSUARIO.Location = new System.Drawing.Point(9, 125);
            this.LUSUARIO.Name = "LUSUARIO";
            this.LUSUARIO.Size = new System.Drawing.Size(59, 13);
            this.LUSUARIO.TabIndex = 0;
            this.LUSUARIO.Text = "USUARIO:";
            // 
            // lCONTRASEÑA
            // 
            this.lCONTRASEÑA.AutoSize = true;
            this.lCONTRASEÑA.Location = new System.Drawing.Point(9, 207);
            this.lCONTRASEÑA.Name = "lCONTRASEÑA";
            this.lCONTRASEÑA.Size = new System.Drawing.Size(84, 13);
            this.lCONTRASEÑA.TabIndex = 1;
            this.lCONTRASEÑA.Text = "CONTRASEÑA:";
            // 
            // USUARIO
            // 
            this.USUARIO.Location = new System.Drawing.Point(12, 141);
            this.USUARIO.Name = "USUARIO";
            this.USUARIO.Size = new System.Drawing.Size(353, 20);
            this.USUARIO.TabIndex = 2;
            // 
            // CONTRASEÑA
            // 
            this.CONTRASEÑA.Location = new System.Drawing.Point(12, 223);
            this.CONTRASEÑA.Name = "CONTRASEÑA";
            this.CONTRASEÑA.Size = new System.Drawing.Size(353, 20);
            this.CONTRASEÑA.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario asignado por la unidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña valida";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(246, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "SISHBCC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(248, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 34);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sistema Hospital \r\nBasico Comunitario\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(280, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Version:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(334, 125);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "1.0.2";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lNUEVOUSUARIO
            // 
            this.lNUEVOUSUARIO.AutoSize = true;
            this.lNUEVOUSUARIO.LinkColor = System.Drawing.Color.Black;
            this.lNUEVOUSUARIO.Location = new System.Drawing.Point(12, 286);
            this.lNUEVOUSUARIO.Name = "lNUEVOUSUARIO";
            this.lNUEVOUSUARIO.Size = new System.Drawing.Size(106, 13);
            this.lNUEVOUSUARIO.TabIndex = 11;
            this.lNUEVOUSUARIO.TabStop = true;
            this.lNUEVOUSUARIO.Text = "Crear Nuevo Usuario";
            this.lNUEVOUSUARIO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lNUEVOUSUARIO_LinkClicked);
            // 
            // lMANUAL
            // 
            this.lMANUAL.AutoSize = true;
            this.lMANUAL.LinkColor = System.Drawing.Color.Black;
            this.lMANUAL.Location = new System.Drawing.Point(153, 286);
            this.lMANUAL.Name = "lMANUAL";
            this.lMANUAL.Size = new System.Drawing.Size(42, 13);
            this.lMANUAL.TabIndex = 12;
            this.lMANUAL.TabStop = true;
            this.lMANUAL.Text = "Manual";
            this.lMANUAL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lMANUAL_LinkClicked_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 310);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(350, 10);
            this.progressBar1.TabIndex = 13;
            // 
            // INGRESAR
            // 
            this.INGRESAR.Location = new System.Drawing.Point(287, 258);
            this.INGRESAR.Name = "INGRESAR";
            this.INGRESAR.Size = new System.Drawing.Size(75, 23);
            this.INGRESAR.TabIndex = 14;
            this.INGRESAR.Text = "Ingresar";
            this.INGRESAR.UseVisualStyleBackColor = true;
            this.INGRESAR.Click += new System.EventHandler(this.INGRESAR_Click_1);
            // 
            // pictureBoxOjo
            // 
            this.pictureBoxOjo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxOjo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxOjo.Image = global::SISHBCC.Properties.Resources.ojocontra;
            this.pictureBoxOjo.Location = new System.Drawing.Point(342, 223);
            this.pictureBoxOjo.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxOjo.Name = "pictureBoxOjo";
            this.pictureBoxOjo.Size = new System.Drawing.Size(20, 15);
            this.pictureBoxOjo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOjo.TabIndex = 15;
            this.pictureBoxOjo.TabStop = false;
            this.pictureBoxOjo.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(387, 332);
            this.Controls.Add(this.pictureBoxOjo);
            this.Controls.Add(this.INGRESAR);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lMANUAL);
            this.Controls.Add(this.lNUEVOUSUARIO);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CONTRASEÑA);
            this.Controls.Add(this.USUARIO);
            this.Controls.Add(this.lCONTRASEÑA);
            this.Controls.Add(this.LUSUARIO);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOjo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LUSUARIO;
        private System.Windows.Forms.Label lCONTRASEÑA;
        private System.Windows.Forms.TextBox USUARIO;
        private System.Windows.Forms.TextBox CONTRASEÑA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel lNUEVOUSUARIO;
        private System.Windows.Forms.LinkLabel lMANUAL;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button INGRESAR;
        private System.Windows.Forms.PictureBox pictureBoxOjo;
    }
}