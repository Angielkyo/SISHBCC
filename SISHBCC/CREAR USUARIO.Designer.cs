namespace SISHBCC
{
    partial class CREAR_USUARIO
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
            this.NOMBRES = new System.Windows.Forms.TextBox();
            this.APELLIDO_P = new System.Windows.Forms.TextBox();
            this.APELLIDO_M = new System.Windows.Forms.TextBox();
            this.CURP = new System.Windows.Forms.TextBox();
            this.CEDULA = new System.Windows.Forms.TextBox();
            this.NUMERO = new System.Windows.Forms.TextBox();
            this.CORREO = new System.Windows.Forms.TextBox();
            this.TIPO = new System.Windows.Forms.TextBox();
            this.CONTRASEÑA = new System.Windows.Forms.TextBox();
            this.CONFIRMAR = new System.Windows.Forms.TextBox();
            this.REGISTRARSE = new System.Windows.Forms.Button();
            this.REGRESAR = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // NOMBRES
            // 
            this.NOMBRES.Location = new System.Drawing.Point(44, 43);
            this.NOMBRES.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NOMBRES.Name = "NOMBRES";
            this.NOMBRES.Size = new System.Drawing.Size(380, 26);
            this.NOMBRES.TabIndex = 0;
            this.NOMBRES.Text = "NOMBRE";
            // 
            // APELLIDO_P
            // 
            this.APELLIDO_P.Location = new System.Drawing.Point(44, 98);
            this.APELLIDO_P.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.APELLIDO_P.Name = "APELLIDO_P";
            this.APELLIDO_P.Size = new System.Drawing.Size(380, 26);
            this.APELLIDO_P.TabIndex = 1;
            this.APELLIDO_P.Text = "APELLIDO PATERNO";
            // 
            // APELLIDO_M
            // 
            this.APELLIDO_M.Location = new System.Drawing.Point(44, 155);
            this.APELLIDO_M.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.APELLIDO_M.Name = "APELLIDO_M";
            this.APELLIDO_M.Size = new System.Drawing.Size(380, 26);
            this.APELLIDO_M.TabIndex = 2;
            this.APELLIDO_M.Text = "APELLIDO MATERNO";
            // 
            // CURP
            // 
            this.CURP.Location = new System.Drawing.Point(44, 209);
            this.CURP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CURP.Name = "CURP";
            this.CURP.Size = new System.Drawing.Size(380, 26);
            this.CURP.TabIndex = 3;
            this.CURP.Text = "CURP";
            // 
            // CEDULA
            // 
            this.CEDULA.Location = new System.Drawing.Point(44, 265);
            this.CEDULA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CEDULA.Name = "CEDULA";
            this.CEDULA.Size = new System.Drawing.Size(380, 26);
            this.CEDULA.TabIndex = 4;
            this.CEDULA.Text = "CEDULA";
            // 
            // NUMERO
            // 
            this.NUMERO.Location = new System.Drawing.Point(44, 322);
            this.NUMERO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.Size = new System.Drawing.Size(380, 26);
            this.NUMERO.TabIndex = 5;
            this.NUMERO.Text = "NUMERO DE TELEFONO";
            // 
            // CORREO
            // 
            this.CORREO.Location = new System.Drawing.Point(44, 377);
            this.CORREO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CORREO.Name = "CORREO";
            this.CORREO.Size = new System.Drawing.Size(380, 26);
            this.CORREO.TabIndex = 6;
            this.CORREO.Text = "CORREO ELECTRONICO";
            // 
            // TIPO
            // 
            this.TIPO.Location = new System.Drawing.Point(44, 431);
            this.TIPO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TIPO.Name = "TIPO";
            this.TIPO.Size = new System.Drawing.Size(380, 26);
            this.TIPO.TabIndex = 7;
            this.TIPO.Text = "TIPO DE USUARIO";
            // 
            // CONTRASEÑA
            // 
            this.CONTRASEÑA.Location = new System.Drawing.Point(44, 491);
            this.CONTRASEÑA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CONTRASEÑA.Name = "CONTRASEÑA";
            this.CONTRASEÑA.Size = new System.Drawing.Size(380, 26);
            this.CONTRASEÑA.TabIndex = 8;
            this.CONTRASEÑA.Text = "CONTRASEÑA";
            // 
            // CONFIRMAR
            // 
            this.CONFIRMAR.Location = new System.Drawing.Point(44, 546);
            this.CONFIRMAR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CONFIRMAR.Name = "CONFIRMAR";
            this.CONFIRMAR.Size = new System.Drawing.Size(380, 26);
            this.CONFIRMAR.TabIndex = 9;
            this.CONFIRMAR.Text = "CONFIRMAR CONTRASEÑA";
            // 
            // REGISTRARSE
            // 
            this.REGISTRARSE.Location = new System.Drawing.Point(44, 640);
            this.REGISTRARSE.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.REGISTRARSE.Name = "REGISTRARSE";
            this.REGISTRARSE.Size = new System.Drawing.Size(135, 35);
            this.REGISTRARSE.TabIndex = 10;
            this.REGISTRARSE.Text = "REGISTRASE";
            this.REGISTRARSE.UseVisualStyleBackColor = true;
            this.REGISTRARSE.Click += new System.EventHandler(this.REGISTRARSE_Click);
            // 
            // REGRESAR
            // 
            this.REGRESAR.Location = new System.Drawing.Point(266, 640);
            this.REGRESAR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.REGRESAR.Name = "REGRESAR";
            this.REGRESAR.Size = new System.Drawing.Size(135, 35);
            this.REGRESAR.TabIndex = 11;
            this.REGRESAR.Text = "REGRESAR";
            this.REGRESAR.UseVisualStyleBackColor = true;
            this.REGRESAR.Click += new System.EventHandler(this.REGRESAR_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 705);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(382, 18);
            this.progressBar1.TabIndex = 12;
            // 
            // CREAR_USUARIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 758);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.REGRESAR);
            this.Controls.Add(this.REGISTRARSE);
            this.Controls.Add(this.CONFIRMAR);
            this.Controls.Add(this.CONTRASEÑA);
            this.Controls.Add(this.TIPO);
            this.Controls.Add(this.CORREO);
            this.Controls.Add(this.NUMERO);
            this.Controls.Add(this.CEDULA);
            this.Controls.Add(this.CURP);
            this.Controls.Add(this.APELLIDO_M);
            this.Controls.Add(this.APELLIDO_P);
            this.Controls.Add(this.NOMBRES);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "CREAR_USUARIO";
            this.Text = "Usuario Nuevo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NOMBRES;
        private System.Windows.Forms.TextBox APELLIDO_P;
        private System.Windows.Forms.TextBox APELLIDO_M;
        private System.Windows.Forms.TextBox CURP;
        private System.Windows.Forms.TextBox CEDULA;
        private System.Windows.Forms.TextBox NUMERO;
        private System.Windows.Forms.TextBox CORREO;
        private System.Windows.Forms.TextBox TIPO;
        private System.Windows.Forms.TextBox CONTRASEÑA;
        private System.Windows.Forms.TextBox CONFIRMAR;
        private System.Windows.Forms.Button REGISTRARSE;
        private System.Windows.Forms.Button REGRESAR;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}