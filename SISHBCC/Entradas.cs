using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.Data.SqlClient;
using Image = iTextSharp.text.Image;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;

namespace SISHBCC
{
    public partial class Entradas : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

        public Entradas()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.KeyDown += TextBox1_KeyDown;
            textBox2.KeyDown += TextBox2_KeyDown;
            textBox3.KeyDown += TextBox3_KeyDown;
            textBox4.KeyDown += TextBox4_KeyDown;
            textBox5.KeyDown += TextBox5_KeyDown;
            textBox6.KeyDown += TextBox6_KeyDown;
            comboBox1.KeyDown += ComboBox1_KeyDown;
            comboBox2.KeyDown += ComboBox2_KeyDown;

            comboBox1.Enter += ComboBox_Enter;
            comboBox2.Enter += ComboBox_Enter;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox2.Focus();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox3.Focus();
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox4.Focus();
            }
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                comboBox1.Focus();
            }
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox5.Focus();
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                comboBox2.Focus();
            }
        }

        private void ComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBox6.Focus();
            }
        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Evento Enter para desplegar el ComboBox automáticamente
        private void ComboBox_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox != null)
            {
                comboBox.DroppedDown = true;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar. Por favor, agregue datos al DataGridView.");
                return;
            }

            if (!VerificarDatosCompletos())
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.");
                return;
            }

            try
            {
                // Guardar datos en la base de datos SQL
                guardarDatosEnSQL();

                // Generar el PDF
                generarPDF();

                MessageBox.Show("Datos guardados y PDF generado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para guardar los datos del DataGridView en la base de datos SQL
        private void guardarDatosEnSQL()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string query = "INSERT INTO Almacen (Cantidad, lote, Caducidad, Clave, Descripcion) VALUES (@Cantidad, @lote, @Caducidad, @Clave, @Descripcion)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Cantidad", fila.Cells["Column1"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@lote", fila.Cells["Column2"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Caducidad", fila.Cells["Column3"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Clave", fila.Cells["Column4"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Descripcion", fila.Cells["Column5"].Value ?? DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        // Método para generar el PDF
        private void generarPDF()
        {
            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string carpetaAlmacen = Path.Combine(rutaEscritorio, "Almacen");

            if (!Directory.Exists(carpetaAlmacen))
            {
                MessageBox.Show("La carpeta 'Almacen' no existe en el escritorio.");
                return;
            }

            string carpetaEntrada = Path.Combine(carpetaAlmacen, "Entrada");
            if (!Directory.Exists(carpetaEntrada))
            {
                Directory.CreateDirectory(carpetaEntrada);
                MessageBox.Show("Carpeta 'Entrada' creada correctamente dentro de 'Almacen'.");
            }

            string fechaHoraActual = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string nombreArchivo = $"Entradas_{fechaHoraActual}.pdf";
            string rutaArchivo = Path.Combine(carpetaEntrada, nombreArchivo);

            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                string rutaImagen = @"limber.jpeg";
                if (File.Exists(rutaImagen))
                {
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                    imagen.ScaleToFit(doc.PageSize.Width - doc.LeftMargin - doc.RightMargin, 150f);
                    imagen.Alignment = Element.ALIGN_CENTER;
                    doc.Add(imagen);
                }

                doc.Add(new Paragraph("\n\n"));

                PdfPTable tabla = new PdfPTable(4);
                tabla.DefaultCell.Padding = 5;
                tabla.WidthPercentage = 100;
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cellTitulo = new PdfPCell(new Phrase("COLECTIVO ENTRADA"));
                cellTitulo.Colspan = 4;
                cellTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(cellTitulo);

                tabla.AddCell(new PdfPCell(new Phrase(label2.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox1.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label3.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox2.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label4.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox3.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label5.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox4.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label6.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(comboBox1.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label7.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox5.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label8.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(comboBox2.Text)));

                tabla.AddCell(new PdfPCell(new Phrase(label9.Text)));
                tabla.AddCell(new PdfPCell(new Phrase(textBox6.Text)));

                doc.Add(tabla);

                doc.Add(new Paragraph("\n\n"));

                PdfPTable tablaDataGridView = new PdfPTable(dataGridView1.Columns.Count);
                tablaDataGridView.WidthPercentage = 100;

                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    tablaDataGridView.AddCell(new Phrase(columna.HeaderText));
                }

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            tablaDataGridView.AddCell(new Phrase(celda.Value?.ToString() ?? string.Empty));
                        }
                    }
                }

                doc.Add(tablaDataGridView);

                doc.Add(new Paragraph("\n\n"));

                // Crear una tabla para las firmas
                PdfPTable tablaFirmas = new PdfPTable(3);
                tablaFirmas.WidthPercentage = 100;

                tablaFirmas.AddCell(new PdfPCell(new Phrase("ENTREGO: __________")) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                tablaFirmas.AddCell(new PdfPCell(new Phrase("RECIBIÓ: __________")) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                tablaFirmas.AddCell(new PdfPCell(new Phrase("VO.BO: __________")) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                doc.Add(tablaFirmas);

                doc.Close();

                Process.Start(rutaArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al crear el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }

        private bool VerificarDatosCompletos()
        {
            return !(
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                comboBox1.SelectedIndex == -1 ||
                comboBox2.SelectedIndex == -1
            );
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ALMACEN aLMACEN = new ALMACEN();
            aLMACEN.Show();
            this.Close();
        }

        private void rgfToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Entradas_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox != null)
            {
                comboBox.DroppedDown = true;
            }
        }
    }
}