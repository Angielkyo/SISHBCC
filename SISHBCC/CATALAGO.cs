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
using Microsoft.Data.SqlClient;
using System.Configuration;



namespace SISHBCC
{
    public partial class CATALAGO : Form
    {
        private readonly string connectionString;

        public CATALAGO()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Obtén la cadena de conexión desde app.config
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
        }

        private async void CATALAGO_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            string query = "SELECT * FROM CATALAGO";

            dataGridView1.Rows.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dataGridView1.Rows.Add(reader["Clave"], reader["Descripcion"], reader["Cause"], reader["EsMedicamento"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FARMACIA fARMACIA = new FARMACIA();
            fARMACIA.Show();

            this.Close();
        }

        private async void btnAGREGAR_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (AGREGAR_DATOS agregarDatosForm = new AGREGAR_DATOS(connection, this))
                {
                    if (agregarDatosForm.ShowDialog() == DialogResult.OK)
                    {
                        await LoadData();
                    }
                }
            }
        }

        private async void  btnBUSCAR_Click_1(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            string query = "SELECT * FROM CATALAGO WHERE Clave LIKE @SearchTerm OR Descripcion LIKE @SearchTerm OR Cause LIKE @SearchTerm OR EsMedicamento LIKE @SearchTerm";

            dataGridView1.Rows.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dataGridView1.Rows.Add(reader["Clave"], reader["Descripcion"], reader["Cause"], reader["EsMedicamento"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar datos: " + ex.Message);
            }
        }

        private async void btnELIMINAR_Click_1(object sender, EventArgs e)
        {
            string searchTerm = textBox2.Text;

            DialogResult result = MessageBox.Show("¿Seguro que quieres eliminar los registros que coinciden con '" + searchTerm + "'?", "Confirmar Eliminación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM CATALAGO WHERE Clave LIKE @SearchTerm OR Descripcion LIKE @SearchTerm OR Cause LIKE @SearchTerm OR EsMedicamento LIKE @SearchTerm";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    await LoadData();

                    MessageBox.Show("Registros eliminados correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar registros: " + ex.Message);
                }
            }
        }

        private async void btnACTUALIZAR_Click_1(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
