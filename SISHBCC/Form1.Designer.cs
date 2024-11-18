namespace SISHBCC
{
    partial class Menu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ALMACEN = new System.Windows.Forms.LinkLabel();
            this.FARMACIA = new System.Windows.Forms.LinkLabel();
            this.RECETARIO = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utileriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respaldarBaseDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.responsablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ALMACEN
            // 
            this.ALMACEN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ALMACEN.AutoSize = true;
            this.ALMACEN.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ALMACEN.LinkColor = System.Drawing.Color.Black;
            this.ALMACEN.Location = new System.Drawing.Point(18, 291);
            this.ALMACEN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ALMACEN.Name = "ALMACEN";
            this.ALMACEN.Size = new System.Drawing.Size(158, 38);
            this.ALMACEN.TabIndex = 0;
            this.ALMACEN.TabStop = true;
            this.ALMACEN.Text = "ALMACEN";
            this.ALMACEN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ALMACEN_LinkClicked);
            // 
            // FARMACIA
            // 
            this.FARMACIA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FARMACIA.AutoSize = true;
            this.FARMACIA.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FARMACIA.LinkColor = System.Drawing.Color.Black;
            this.FARMACIA.Location = new System.Drawing.Point(512, 291);
            this.FARMACIA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FARMACIA.Name = "FARMACIA";
            this.FARMACIA.Size = new System.Drawing.Size(174, 38);
            this.FARMACIA.TabIndex = 1;
            this.FARMACIA.TabStop = true;
            this.FARMACIA.Text = "FARMACIA";
            this.FARMACIA.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FARMACIA_LinkClicked);
            // 
            // RECETARIO
            // 
            this.RECETARIO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RECETARIO.AutoSize = true;
            this.RECETARIO.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RECETARIO.ForeColor = System.Drawing.Color.Black;
            this.RECETARIO.LinkColor = System.Drawing.Color.Black;
            this.RECETARIO.Location = new System.Drawing.Point(996, 291);
            this.RECETARIO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RECETARIO.Name = "RECETARIO";
            this.RECETARIO.Size = new System.Drawing.Size(190, 38);
            this.RECETARIO.TabIndex = 2;
            this.RECETARIO.TabStop = true;
            this.RECETARIO.Text = "RECETARIO";
            this.RECETARIO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RECETARIO_LinkClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem,
            this.utileriasToolStripMenuItem,
            this.acercaDeToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 35);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // utileriasToolStripMenuItem
            // 
            this.utileriasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarContraseñaToolStripMenuItem,
            this.respaldarBaseDeToolStripMenuItem});
            this.utileriasToolStripMenuItem.Name = "utileriasToolStripMenuItem";
            this.utileriasToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.utileriasToolStripMenuItem.Text = "Utilerias";
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(309, 34);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // respaldarBaseDeToolStripMenuItem
            // 
            this.respaldarBaseDeToolStripMenuItem.Name = "respaldarBaseDeToolStripMenuItem";
            this.respaldarBaseDeToolStripMenuItem.Size = new System.Drawing.Size(309, 34);
            this.respaldarBaseDeToolStripMenuItem.Text = "Respaldar Base de Datos";
            this.respaldarBaseDeToolStripMenuItem.Click += new System.EventHandler(this.respaldarBaseDeToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem1
            // 
            this.acercaDeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.responsablesToolStripMenuItem});
            this.acercaDeToolStripMenuItem1.Name = "acercaDeToolStripMenuItem1";
            this.acercaDeToolStripMenuItem1.Size = new System.Drawing.Size(109, 29);
            this.acercaDeToolStripMenuItem1.Text = "Acerca de:";
            this.acercaDeToolStripMenuItem1.Click += new System.EventHandler(this.acercaDeToolStripMenuItem1_Click);
            // 
            // responsablesToolStripMenuItem
            // 
            this.responsablesToolStripMenuItem.Name = "responsablesToolStripMenuItem";
            this.responsablesToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.responsablesToolStripMenuItem.Text = "Responsables";
            this.responsablesToolStripMenuItem.Click += new System.EventHandler(this.responsablesToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 626);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.RECETARIO);
            this.Controls.Add(this.FARMACIA);
            this.Controls.Add(this.ALMACEN);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Menu";
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Menu_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel ALMACEN;
        private System.Windows.Forms.LinkLabel FARMACIA;
        private System.Windows.Forms.LinkLabel RECETARIO;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utileriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respaldarBaseDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem responsablesToolStripMenuItem;
    }
}

