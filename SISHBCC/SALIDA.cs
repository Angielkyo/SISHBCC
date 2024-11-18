using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SISHBCC
{
    public partial class SALIDA : Form
    {
        public SALIDA()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = true;
            textBox1.KeyDown += Control_KeyDown;
            textBox2.KeyDown += Control_KeyDown;
            comboBox1.KeyDown += Control_KeyDown;
            comboBox2.KeyDown += Control_KeyDown;

        }
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevenir el sonido 'ding' de la tecla Enter

                // Mover el foco al siguiente control
                if (sender == textBox1)
                {
                    textBox2.Focus();
                   
                }
                else if (sender == textBox2)
                {
                    // Si es el último control, puedes cambiar el foco al primer control o a otro según necesites
                    comboBox1.Focus();
                    comboBox1.DroppedDown = true;
                }
                else if (sender == comboBox1)
                {
                    // Si es el último control, puedes cambiar el foco al primer control o a otro según necesites
                    comboBox2.Focus();
                    comboBox2.DroppedDown = true;
                }
                else if (sender == comboBox2)
                {
                    // Si es el último control, puedes cambiar el foco al primer control o a otro según necesites
                    textBox3.Focus();
                    
                }
            }
        }
            private void imprmirToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    // Crear la carpeta "salidas" en la carpeta "Farmacia" del escritorio si no existe
                    string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string rutaCarpetaFarmacia = Path.Combine(rutaEscritorio, "Farmacia");
                    string rutaCarpetaSalidas = Path.Combine(rutaCarpetaFarmacia, "salidas");

                    if (!Directory.Exists(rutaCarpetaSalidas))
                    {
                        Directory.CreateDirectory(rutaCarpetaSalidas);
                        MessageBox.Show("Carpeta 'salidas' creada correctamente en: " + rutaCarpetaSalidas);
                    }

                    // Define el nombre del archivo PDF y la ruta completa
                    string nombreArchivo = "Salida Farmacia.pdf";
                    string rutaCompleta = Path.Combine(rutaCarpetaSalidas, nombreArchivo);
                    Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
                    doc.Open();

                    // Agrega imágenes al documento para el formato
                    string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "roxi.jpeg");
                    if (File.Exists(rutaImagen))
                    {
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                        imagen.ScaleAbsolute(doc.PageSize.Width, (doc.PageSize.Width / imagen.Width) * imagen.Height);
                        imagen.Alignment = Element.ALIGN_CENTER;
                        doc.Add(imagen);
                    }
                    else
                    {
                        MessageBox.Show("La imagen no existe en la ruta especificada: " + rutaImagen);
                    }
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph(" "));

                    PdfPTable tabla = new PdfPTable(2); // 2 columnas para alinear label y control
                    tabla.WidthPercentage = 100; // Tamaño de la tabla al 100% del ancho de página
                    tabla.SpacingBefore = 10f; // Espacio antes de la tabla
                    tabla.SpacingAfter = 10f; // Espacio después de la tabla

                    // Celdas para cada par de label y control
                    PdfPCell cell;

                    // Fila 1: label1 y dateTimePicker1
                    cell = new PdfPCell(new Phrase(label1.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dateTimePicker1.Value.ToString("dd/MM/yyyy")));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Fila 2: label2 y textBox1
                    cell = new PdfPCell(new Phrase(label2.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(textBox1.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Fila 3: label3 y comboBox1
                    cell = new PdfPCell(new Phrase(label3.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(comboBox1.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Fila 4: label4 y textBox3
                    cell = new PdfPCell(new Phrase(label4.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(textBox3.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Fila 5: label5 y textBox2
                    cell = new PdfPCell(new Phrase(label5.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(textBox2.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Fila 6: label6 y comboBox2
                    cell = new PdfPCell(new Phrase(label6.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    cell = new PdfPCell(new Phrase(comboBox2.Text));
                    cell.Border = PdfPCell.NO_BORDER; // Eliminar bordes de la celda
                    tabla.AddCell(cell);

                    // Agregar la tabla al documento
                    doc.Add(tabla);

                    // Crea una tabla PDF y configura según las necesidades especificadas
                    PdfPTable tablaPDF = new PdfPTable(dataGridView1.Columns.Count);
                    tablaPDF.DefaultCell.Padding = 3;
                    tablaPDF.WidthPercentage = 100;
                    tablaPDF.HorizontalAlignment = Element.ALIGN_LEFT;

                    // Agrega encabezados de columna a la tabla
                    foreach (DataGridViewColumn columna in dataGridView1.Columns)
                    {
                        PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                        tablaPDF.AddCell(celda);
                    }

                    // Agrega filas de datos a la tabla
                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            if (celda.Value != null)
                            {
                                tablaPDF.AddCell(celda.Value.ToString());
                            }
                            else
                            {
                                tablaPDF.AddCell("");
                            }
                        }
                    }

                    // Añadir tabla al documento
                    doc.Add(tablaPDF);
                    doc.Close();

                    // Abrir PDF con el visor predeterminado
                    Process.Start(rutaCompleta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }

            private void salirToolStripMenuItem_Click(object sender, EventArgs e)
            {
                FARMACIA fFARMACIA = new FARMACIA();
                fFARMACIA.Show();
                this.Close();
            }
        }
    }