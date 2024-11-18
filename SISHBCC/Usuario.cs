using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

namespace SISHBCC
{
    public partial class Usuario : Form
    {
        private string connectionString;

        public Usuario()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            this.StartPosition = FormStartPosition.CenterScreen;
            MostrarDatosEnDataGridView();
        }

        private void MostrarDatosEnDataGridView()
        {
            // Consulta SQL para obtener los datos de la tabla Usuario1
            string query = "SELECT NombreUsuario, HoraIngreso FROM Usuario1";

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Crear un adaptador de datos para ejecutar la consulta y llenar un DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                // Abrir la conexión y llenar el DataTable
                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                // Asignar el DataTable como origen de datos del DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este método se ejecuta cuando se hace clic en el contenido de una celda del DataGridView, si necesitas realizar alguna acción aquí, puedes agregar tu lógica.
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear un documento PDF
            Document doc = new Document();

            try
            {
                // Definir la ruta para guardar el PDF en el escritorio del usuario
                string nombreArchivo = "datos.pdf"; // Nombre del archivo PDF
                string rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

                // Crear el archivo PDF
                PdfWriter.GetInstance(doc, new FileStream(rutaPDF, FileMode.Create));
                doc.Open();

                // Agregar imagen al documento si existe
                string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "limber.jpeg");
                if (File.Exists(rutaImagen))
                {
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                    imagen.ScaleAbsolute(doc.PageSize.Width, (doc.PageSize.Width / imagen.Width) * imagen.Height);
                    imagen.Alignment = Element.ALIGN_CENTER;
                    doc.Add(imagen);
                }

                // Crear una tabla para el contenido del DataGridView
                PdfPTable tablaPDF = new PdfPTable(dataGridView1.Columns.Count);

                // Agregar encabezados de columnas al PDF
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                    tablaPDF.AddCell(celda);
                }

                // Agregar datos de DataGridView al PDF
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
                            tablaPDF.AddCell(""); // Agregar una celda vacía si el valor es nulo
                        }
                    }
                }

                // Agregar la tabla al documento PDF
                doc.Add(tablaPDF);

                // Mostrar mensaje de éxito
                MessageBox.Show("Documento PDF creado en el escritorio: " + rutaPDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el PDF: " + ex.Message);
            }
            finally
            {
                // Cerrar el documento PDF
                doc.Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ALMACEN aLMACEN = new ALMACEN();
            aLMACEN.Show();

            this.Close();
        }
    }
}