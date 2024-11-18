using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SISHBCC
{
    public partial class AGREGAR_DATOS : Form
    {
        private SqlConnection connection;
        private CATALAGO catalogo;

        public AGREGAR_DATOS(SqlConnection conn, CATALAGO catalogo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.connection = conn;
            this.catalogo = catalogo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string clave = textBox1.Text;
            string descripcion = textBox2.Text;
            string cause = textBox3.Text;
            string esMedicamento = textBox4.Text;

            // Validar los campos antes de proceder
            if (string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(cause) || string.IsNullOrWhiteSpace(esMedicamento))
            {
                MessageBox.Show("Todos los campos deben ser completados.");
                return;
            }

            try
            {
                // Abrir la conexión si no está abierta
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "INSERT INTO CATALAGO (Clave, Descripcion, Cause, EsMedicamento) VALUES (@Clave, @Descripcion, @Cause, @EsMedicamento)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Clave", clave);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Cause", cause);
                    command.Parameters.AddWithValue("@EsMedicamento", esMedicamento);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Datos agregados correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error de base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar datos: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            catalogo.Show();
        }
    }
}