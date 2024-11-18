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
    public partial class Productos : Form
    {
        private string connectionString;
        private SqlConnection connection;

        public Productos()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        private void CargarDatos()
        {
            try
            {
                // Abrir la conexión
                connection.Open();

                // Consulta SQL para obtener datos
                string query = "SELECT Clave, Descripcion FROM Catalogo";
                SqlCommand command = new SqlCommand(query, connection);

                // Crear un adaptador de datos y llenar un DataSet
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                // Asignar los datos al DataGrid
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                connection.Close();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obtener la ruta del escritorio del usuario
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Carpeta "Almacen" en el escritorio del usuario
            string almacenPath = Path.Combine(desktopPath, "Almacen");

            // Carpeta "Productos" dentro de "Almacen"
            string productosPath = Path.Combine(almacenPath, "Productos");

            // Verificar si la carpeta "Productos" existe, si no, crearla
            if (!Directory.Exists(productosPath))
            {
                Directory.CreateDirectory(productosPath);
            }

            // Crear un nombre de archivo único con la palabra "productos", fecha y hora actual
            string fileName = $"productos_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            // Combinar la carpeta "Productos" con el nombre de archivo único
            string fullPath = Path.Combine(productosPath, fileName);

            // Crear un nuevo documento PDF
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(fullPath, FileMode.Create));
            doc.Open();

            // Agregar un título
            Paragraph title = new Paragraph("Catálogo de Productos");
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);

            // Agregar una tabla y configurar sus columnas
            PdfPTable table = new PdfPTable(dataGridView1.ColumnCount);
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                table.AddCell(new Phrase(dataGridView1.Columns[i].HeaderText));
            }

            // Agregar los datos del DataGridView a la tabla del PDF
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString()));
                    }
                }
            }

            doc.Add(table);
            doc.Close();

            MessageBox.Show("Datos guardados exitosamente en " + fullPath);
        }
        

        private void Productos_Load_1(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ALMACEN aLMACEN = new ALMACEN();
            aLMACEN.Show();

            this.Close();
        }
    }
}
