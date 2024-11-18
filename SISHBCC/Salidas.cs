using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SISHBCC;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SISHBCC
{
    public partial class Salidas : Form
    {
        private bool carpetaSalidasCreada = false;
        private System.Windows.Forms.Button ultimoBotonSeleccionado = null;
        private Color colorOriginalBoton;

        public Salidas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Método para crear la carpeta principal "Salidas" dentro de "Almacen" en el escritorio
        private void CrearCarpetaSalidas()
        {
            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaAlmacen = Path.Combine(rutaEscritorio, "Almacen");
            string rutaSalidas = Path.Combine(rutaAlmacen, "Salidas");

            if (!Directory.Exists(rutaAlmacen))
            {
                MessageBox.Show("La carpeta 'Almacen' no existe en el escritorio.");
                return;
            }

            if (!Directory.Exists(rutaSalidas))
            {
                Directory.CreateDirectory(rutaSalidas);
                MessageBox.Show("Carpeta 'Salidas' creada correctamente dentro de 'Almacen'.");
            }

            carpetaSalidasCreada = true;
        }

        // Método para crear carpetas de meses dentro de la carpeta "Salidas"
        private void CrearCarpetaMes(string nombreMes, System.Windows.Forms.Button boton)
        {
            if (!carpetaSalidasCreada)
            {
                CrearCarpetaSalidas();
            }

            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaAlmacen = Path.Combine(rutaEscritorio, "Almacen");
            string rutaSalidas = Path.Combine(rutaAlmacen, "Salidas");
            string rutaCarpetaMes = Path.Combine(rutaSalidas, nombreMes);

            if (!Directory.Exists(rutaSalidas))
            {
                MessageBox.Show("La carpeta 'Salidas' no existe en la carpeta 'Almacen'.");
                return;
            }

            if (!Directory.Exists(rutaCarpetaMes))
            {
                Directory.CreateDirectory(rutaCarpetaMes);
                MessageBox.Show($"Carpeta '{nombreMes}' creada correctamente en 'Salidas'.");
            }

            if (ultimoBotonSeleccionado != null)
            {
                ultimoBotonSeleccionado.BackColor = colorOriginalBoton;
            }
            colorOriginalBoton = boton.BackColor;
            boton.BackColor = Color.Red;
            ultimoBotonSeleccionado = boton;

            // Guardar la ruta de la carpeta del mes seleccionado
            ultimoBotonSeleccionado.Tag = rutaCarpetaMes;
        }

        // Método para imprimir y guardar en PDF
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ultimoBotonSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un mes antes de crear el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rutaCarpetaMes = ultimoBotonSeleccionado.Tag.ToString();
            string fechaActual = DateTime.Now.ToString("yyyyMMddHHmmss");
            string nombreArchivo = $"Colectivo_{fechaActual}.pdf";
            string rutaPDF = Path.Combine(rutaCarpetaMes, nombreArchivo);

            Document doc = new Document();

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaPDF, FileMode.Create));
                doc.Open();

                // Agregar imagen de encabezado
                string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "limber.jpeg");
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                imagen.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height / 4); // Ajustar la altura si es necesario
                imagen.Alignment = iTextSharp.text.Image.ALIGN_CENTER; // Centrar la imagen
                imagen.SetAbsolutePosition(0, doc.PageSize.Height - imagen.ScaledHeight); // Posiciona la imagen en la parte superior de la página
                doc.Add(imagen);

                // Agregar un espacio entre la imagen y el contenido del documento
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));// Párrafo vacío para espacio

                // Agregar valores de los ComboBox y Labels al documento PDF
                string label1Text = label1.Text;
                string comboBox1Text = comboBox1.SelectedItem?.ToString() ?? "N/A";
                doc.Add(new Paragraph($"{label1Text}: {comboBox1Text}"));
                doc.Add(new Paragraph(" ")); // Párrafo vacío para espacio

                string label2Text = label2.Text;
                string comboBox2Text = comboBox2.SelectedItem?.ToString() ?? "N/A";
                doc.Add(new Paragraph($"{label2Text}: {comboBox2Text}"));
                doc.Add(new Paragraph(" ")); // Párrafo vacío para espacio

                string label3Text = label3.Text;
                string comboBox3Text = comboBox3.SelectedItem?.ToString() ?? "N/A";
                doc.Add(new Paragraph($"{label3Text}: {comboBox3Text}"));
                doc.Add(new Paragraph(" ")); // Párrafo vacío para espacio

                // Agregar tabla con los datos del DataGridView
                PdfPTable tablaPDF = new PdfPTable(dataGridView1.Columns.Count);
                tablaPDF.WidthPercentage = 100;

                // Agregar encabezados de columna
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                    tablaPDF.AddCell(celda);
                }

                // Recorrer las filas y celdas del DataGridView
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

                // Agregar la tabla al documento
                doc.Add(tablaPDF);

                MessageBox.Show("Documento PDF creado en: " + rutaPDF, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }

       
     
        // Otros métodos
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Implementación para salir
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar buscar = new Buscar();
            buscar.ShowDialog();
            buscar.Close();
        }

        // Eventos de los botones para crear las carpetas de los meses
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Enero", button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Febrero", button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Marzo", button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Abril", button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Mayo", button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Junio", button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Julio", button7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Agosto", button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Septiembre", button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Octubre", button10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Noviembre", button11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CrearCarpetaMes("Diciembre", button12);
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ALMACEN aLMACEN = new ALMACEN();
            aLMACEN.Show();

            this.Close();
        }
    }
}