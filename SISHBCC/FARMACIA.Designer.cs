namespace SISHBCC
{
    partial class FARMACIA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalagoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almacenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entradaFarmaciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salidaFarmaciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peticionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.farmaciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosCaducadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detalladoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.archivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utileriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datosGeneralesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CLCLAVE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLDESCRIPCION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLPRESENTACION = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CLCANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLCSURTIDA = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem,
            this.catalagoToolStripMenuItem,
            this.capturaToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.utileriasToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.nuevoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1334, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // catalagoToolStripMenuItem
            // 
            this.catalagoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosToolStripMenuItem,
            this.almacenesToolStripMenuItem,
            this.medicosToolStripMenuItem,
            this.serviciosToolStripMenuItem});
            this.catalagoToolStripMenuItem.Name = "catalagoToolStripMenuItem";
            this.catalagoToolStripMenuItem.Size = new System.Drawing.Size(98, 29);
            this.catalagoToolStripMenuItem.Text = "Catalago";
            // 
            // productosToolStripMenuItem
            // 
            this.productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            this.productosToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.productosToolStripMenuItem.Text = "Productos";
            this.productosToolStripMenuItem.Click += new System.EventHandler(this.productosToolStripMenuItem_Click_1);
            // 
            // almacenesToolStripMenuItem
            // 
            this.almacenesToolStripMenuItem.Name = "almacenesToolStripMenuItem";
            this.almacenesToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.almacenesToolStripMenuItem.Text = "Almacenes";
            // 
            // medicosToolStripMenuItem
            // 
            this.medicosToolStripMenuItem.Name = "medicosToolStripMenuItem";
            this.medicosToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.medicosToolStripMenuItem.Text = "Medicos";
            this.medicosToolStripMenuItem.Click += new System.EventHandler(this.medicosToolStripMenuItem_Click_1);
            // 
            // serviciosToolStripMenuItem
            // 
            this.serviciosToolStripMenuItem.Name = "serviciosToolStripMenuItem";
            this.serviciosToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.serviciosToolStripMenuItem.Text = "Servicios";
            // 
            // capturaToolStripMenuItem
            // 
            this.capturaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entradaFarmaciaToolStripMenuItem,
            this.salidaFarmaciaToolStripMenuItem,
            this.peticionesToolStripMenuItem});
            this.capturaToolStripMenuItem.Name = "capturaToolStripMenuItem";
            this.capturaToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.capturaToolStripMenuItem.Text = "Captura";
            // 
            // entradaFarmaciaToolStripMenuItem
            // 
            this.entradaFarmaciaToolStripMenuItem.Name = "entradaFarmaciaToolStripMenuItem";
            this.entradaFarmaciaToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.entradaFarmaciaToolStripMenuItem.Text = "Entrada Farmacia";
            this.entradaFarmaciaToolStripMenuItem.Click += new System.EventHandler(this.entradaFarmaciaToolStripMenuItem_Click_1);
            // 
            // salidaFarmaciaToolStripMenuItem
            // 
            this.salidaFarmaciaToolStripMenuItem.Name = "salidaFarmaciaToolStripMenuItem";
            this.salidaFarmaciaToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.salidaFarmaciaToolStripMenuItem.Text = "Salida Farmacia";
            this.salidaFarmaciaToolStripMenuItem.Click += new System.EventHandler(this.salidaFarmaciaToolStripMenuItem_Click_1);
            // 
            // peticionesToolStripMenuItem
            // 
            this.peticionesToolStripMenuItem.Name = "peticionesToolStripMenuItem";
            this.peticionesToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.peticionesToolStripMenuItem.Text = "Peticiones";
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.farmaciaToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(98, 29);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // farmaciaToolStripMenuItem
            // 
            this.farmaciaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosCaducadosToolStripMenuItem,
            this.archivosToolStripMenuItem});
            this.farmaciaToolStripMenuItem.Name = "farmaciaToolStripMenuItem";
            this.farmaciaToolStripMenuItem.Size = new System.Drawing.Size(183, 34);
            this.farmaciaToolStripMenuItem.Text = "Farmacia";
            // 
            // productosCaducadosToolStripMenuItem
            // 
            this.productosCaducadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detalladoToolStripMenuItem1});
            this.productosCaducadosToolStripMenuItem.Name = "productosCaducadosToolStripMenuItem";
            this.productosCaducadosToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.productosCaducadosToolStripMenuItem.Text = "Productos Caducados";
            this.productosCaducadosToolStripMenuItem.Click += new System.EventHandler(this.productosCaducadosToolStripMenuItem_Click);
            // 
            // detalladoToolStripMenuItem1
            // 
            this.detalladoToolStripMenuItem1.Name = "detalladoToolStripMenuItem1";
            this.detalladoToolStripMenuItem1.Size = new System.Drawing.Size(190, 34);
            this.detalladoToolStripMenuItem1.Text = "Detallado";
            this.detalladoToolStripMenuItem1.Click += new System.EventHandler(this.detalladoToolStripMenuItem1_Click_1);
            // 
            // archivosToolStripMenuItem
            // 
            this.archivosToolStripMenuItem.Name = "archivosToolStripMenuItem";
            this.archivosToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.archivosToolStripMenuItem.Text = "Archivos";
            this.archivosToolStripMenuItem.Click += new System.EventHandler(this.archivosToolStripMenuItem_Click);
            // 
            // utileriasToolStripMenuItem
            // 
            this.utileriasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosToolStripMenuItem,
            this.datosGeneralesToolStripMenuItem});
            this.utileriasToolStripMenuItem.Name = "utileriasToolStripMenuItem";
            this.utileriasToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.utileriasToolStripMenuItem.Text = "Utilerias";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(242, 34);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            // 
            // datosGeneralesToolStripMenuItem
            // 
            this.datosGeneralesToolStripMenuItem.Name = "datosGeneralesToolStripMenuItem";
            this.datosGeneralesToolStripMenuItem.Size = new System.Drawing.Size(242, 34);
            this.datosGeneralesToolStripMenuItem.Text = "Datos Generales";
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click_1);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(80, 29);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Recetario Colectivo de Insumos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(990, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Folio:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(372, 115);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(589, 26);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(1047, 120);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(202, 26);
            this.textBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 186);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Turno:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1071, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hora:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(194, 177);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(146, 26);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(1130, 177);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(120, 26);
            this.dateTimePicker2.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Matutino",
            "Vespertino",
            "Nocturno",
            "Fin de Semana"});
            this.comboBox1.Location = new System.Drawing.Point(600, 175);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 28);
            this.comboBox1.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CLCLAVE,
            this.CLDESCRIPCION,
            this.CLPRESENTACION,
            this.CLCANTIDAD,
            this.CLCSURTIDA});
            this.dataGridView1.Location = new System.Drawing.Point(18, 434);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(1308, 231);
            this.dataGridView1.TabIndex = 11;
            // 
            // CLCLAVE
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLCLAVE.DefaultCellStyle = dataGridViewCellStyle1;
            this.CLCLAVE.HeaderText = "Clave";
            this.CLCLAVE.MinimumWidth = 8;
            this.CLCLAVE.Name = "CLCLAVE";
            this.CLCLAVE.Width = 150;
            // 
            // CLDESCRIPCION
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLDESCRIPCION.DefaultCellStyle = dataGridViewCellStyle2;
            this.CLDESCRIPCION.HeaderText = "Descripcion";
            this.CLDESCRIPCION.MinimumWidth = 8;
            this.CLDESCRIPCION.Name = "CLDESCRIPCION";
            this.CLDESCRIPCION.Width = 300;
            // 
            // CLPRESENTACION
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLPRESENTACION.DefaultCellStyle = dataGridViewCellStyle3;
            this.CLPRESENTACION.FillWeight = 200F;
            this.CLPRESENTACION.HeaderText = "Presentación";
            this.CLPRESENTACION.Items.AddRange(new object[] {
            "TABLETA",
            "CREMA",
            "SOL.OFTALMICA",
            "SUSPENSION RECTAL",
            "SOLIDOS GRANULADOS",
            "SOLUCION INYECTABLE",
            "GRAGEA",
            "GEL"});
            this.CLPRESENTACION.MinimumWidth = 8;
            this.CLPRESENTACION.Name = "CLPRESENTACION";
            this.CLPRESENTACION.Width = 150;
            // 
            // CLCANTIDAD
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLCANTIDAD.DefaultCellStyle = dataGridViewCellStyle4;
            this.CLCANTIDAD.HeaderText = "Cantidad Solicitada";
            this.CLCANTIDAD.MinimumWidth = 8;
            this.CLCANTIDAD.Name = "CLCANTIDAD";
            this.CLCANTIDAD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CLCANTIDAD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CLCANTIDAD.Width = 150;
            // 
            // CLCSURTIDA
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLCSURTIDA.DefaultCellStyle = dataGridViewCellStyle5;
            this.CLCSURTIDA.HeaderText = "Cantidad Surtida";
            this.CLCSURTIDA.MinimumWidth = 8;
            this.CLCSURTIDA.Name = "CLCSURTIDA";
            this.CLCSURTIDA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CLCSURTIDA.Width = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(123, 295);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Medicamentos:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(250, 291);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(998, 28);
            this.comboBox2.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(609, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 34);
            this.label7.TabIndex = 14;
            this.label7.Text = "FARMACIA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SISHBCC.Properties.Resources.notificacion;
            this.pictureBox1.Location = new System.Drawing.Point(1281, 42);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FARMACIA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 728);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FARMACIA";
            this.Text = "FARMACIA";
            this.Load += new System.EventHandler(this.FARMACIA_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem catalagoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capturaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utileriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almacenesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entradaFarmaciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salidaFarmaciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peticionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem farmaciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosCaducadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detalladoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datosGeneralesToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLCLAVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLDESCRIPCION;
        private System.Windows.Forms.DataGridViewComboBoxColumn CLPRESENTACION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLCANTIDAD;
        private System.Windows.Forms.DataGridViewButtonColumn CLCSURTIDA;
        private System.Windows.Forms.ToolStripMenuItem archivosToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
    }
}