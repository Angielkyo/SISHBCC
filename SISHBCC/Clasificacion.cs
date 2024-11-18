using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace SISHBCC
{
    public partial class Clasificacion : Form
    {
        private SqlConnection conexion;

        public Clasificacion()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            conexion = new SqlConnection(connectionString);
        }

        private void Clasificacion_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                conexion.Open();
                string consulta = "SELECT Nombre, Tipo FROM Usuarios";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                listBox1.Items.Clear(); // Limpiar ListBox antes de cargar datos

                while (reader.Read())
                {
                    string nombre = reader["Nombre"].ToString();
                    string tipo = reader["Tipo"].ToString();
                    listBox1.Items.Add($"{nombre} - {tipo}");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}