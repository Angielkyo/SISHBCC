using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Configuration;
namespace SISHBCC
{
    public partial class Folio : Form
    {
        private Recetario Recetario;

        public Folio()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Recetario = new Recetario(); // Crea una nueva instancia de la clase "Recetario"
            this.FormClosing += Folio_FormClosing; // Registra el evento FormClosing
            textBox1.KeyPress += TextBox1_KeyPress; // Asigna el evento KeyPress al TextBox
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Cambia el foco al siguiente control en el orden de tabulación
                SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true; // Indica que el evento está completamente controlado
            }
        }

        private void Folio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Recetario.Dispose();
            // Libera los recursos del objeto "Recetario" al cerrar el formulario "Folio"
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folio = textBox1.Text;

            if (!string.IsNullOrEmpty(folio))
            {
                if (FolioExiste(folio))
                {
                    MessageBox.Show("El folio ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    // Agregar el folio a la base de datos
                    AgregarFolio(folio);
                }
            }

            // Mostrar el segundo formulario
            Recetario.Show();

            // Establecer el texto del textBox1 en el segundo formulario
            Recetario.SetTextBoxText(folio);

            // Ocultar el formulario "Folio"
            this.Hide();
        }

        private bool FolioExiste(string folio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM FOLIOS WHERE Folio = @Folio";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Folio", folio);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el folio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
        }

        private void AgregarFolio(string folio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FOLIOS (Folio) VALUES (@Folio)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Folio", folio);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el folio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Manejar el evento de cambio de texto si es necesario
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu menu = Application.OpenForms.OfType<Menu>().FirstOrDefault();

            // Verificar si se encontró el formulario del menú
            if (menu != null)
            {
                // Mostrar el formulario del menú y cerrar el formulario actual
                menu.Show();
                this.Close();
            }
            else
            {
                // Si el formulario del menú no se encontró, mostrar un mensaje de error
                MessageBox.Show("Error: No se encontró el formulario del menú.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
