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
using System.Diagnostics;
using System.Configuration;
using SISHBCC;
using System.IO;



namespace SISHBCC
{
    public static class Sesion
    {
        public static string NombreUsuario { get; set; }
    }

    public partial class Login : Form
    {
        private readonly string connectionString;
        private readonly SqlConnection connection;

        private readonly Dictionary<string, string> superUsuarios = new Dictionary<string, string>
        {
            { "USUARIO1", "admin2002" },
            { "Usuario1", "admin2024" },
            { "Usuario01", "user2024" },
            { "Adminrod", "rod2002" },
            { "Angelkyo", "kyo2001" }
        };
        private bool esSuperUsuario;
        private bool mostrarContraseña = false;

        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // Suscribir eventos para mostrar/ocultar contraseña
            pictureBoxOjo.MouseDown += PictureBoxOjo_MouseDown;
            pictureBoxOjo.MouseUp += PictureBoxOjo_MouseUp;

            // Eventos de teclado y cambios en el campo de contraseña
            CONTRASEÑA.TextChanged += TxtContraseña_TextChanged;
            USUARIO.KeyPress += TxtUsuario_KeyPress;
            CONTRASEÑA.KeyPress += TxtContraseña_KeyPress;

            // Configuración de la conexión a la base de datos
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CONTRASEÑA.Focus();
                e.Handled = true;
            }
        }

        private void TxtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                INGRESAR.PerformClick();
                e.Handled = true;
            }
        }

        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
            MostrarContraseña();
        }

        private void MostrarContraseña()
        {
            CONTRASEÑA.PasswordChar = mostrarContraseña ? '\0' : '*';
        }

        private void PictureBoxOjo_MouseDown(object sender, MouseEventArgs e)
        {
            mostrarContraseña = true;
            MostrarContraseña();
        }

        private void PictureBoxOjo_MouseUp(object sender, MouseEventArgs e)
        {
            mostrarContraseña = false;
            MostrarContraseña();
        }

        private void INGRESAR_Click_1(object sender, EventArgs e)
        {
            StartProgressBar();
            if (VerificarConexion())
            {
                if (VerificarCredenciales())
                {
                    PasarAlSiguienteFormulario();
                    Sesion.NombreUsuario = USUARIO.Text;
                    Console.WriteLine("Nombre de usuario asignado: " + Sesion.NombreUsuario);
                }
                else
                {
                    MessageBox.Show("Credenciales inválidas. Acceso denegado.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos. Verifique su conexión e inténtelo de nuevo.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerificarConexion()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        private bool VerificarCredenciales()
        {
            string nombre = USUARIO.Text;
            string contraseña = CONTRASEÑA.Text;

            if (superUsuarios.ContainsKey(nombre) && superUsuarios[nombre] == contraseña)
            {
                esSuperUsuario = true;
                return true;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre = @Nombre AND contraseña = @contraseña";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@contraseña", contraseña);

                    int count = (int)command.ExecuteScalar();
                    esSuperUsuario = false;
                    return count > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void PasarAlSiguienteFormulario()
        {
            string nombre = USUARIO.Text;
            DateTime hora = DateTime.Now;
            string tipo = esSuperUsuario ? "SuperUsuario" : ObtenerTipoUsuario(nombre);
            try
            {
                connection.Open();
                InsertarHoraIngreso(nombre, hora, tipo);
                MessageBox.Show($"Usuario: {nombre}\nHora: {hora} \nTipo: {tipo}");

                Menu form = new Menu(tipo);
                form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public string ObtenerTipoUsuario(string nombreUsuario)
        {
            if (esSuperUsuario)
            {
                return "SuperUsuario";
            }
            string tipoUsuario = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Tipo FROM Usuarios WHERE Nombre = @Nombre";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombreUsuario);
                    tipoUsuario = (string)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el tipo de usuario: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return tipoUsuario;
        }

        private void InsertarHoraIngreso(string nombreUsuario, DateTime horaIngreso, string tipo)
        {
            try
            {
                string query = "INSERT INTO Usuario1 (NombreUsuario, HoraIngreso, Tipo) VALUES (@NombreUsuario, @HoraIngreso, @Tipo)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@HoraIngreso", horaIngreso.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@Tipo", tipo);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la hora de ingreso en la base de datos: " + ex.Message);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            // Implementación vacía si no hay lógica específica para este evento
        }

        private void StartProgressBar()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = 30;
            for (int i = 0; i <= 30; i++)
            {
                progressBar1.Value = i;
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Implementación vacía si no hay lógica específica para este evento
        }

        private void LMANUAL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string rutaArchivoPDF = @"C:\Users\natar\Documents\Doc1.pdf";
            if (System.IO.File.Exists(rutaArchivoPDF))
            {
                Process.Start("C:\\Program Files\\Adobe\\Acrobat DC\\Acrobat\\Acrobat.exe", rutaArchivoPDF);
            }
            else
            {
                MessageBox.Show("El archivo PDF no existe en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lNUEVOUSUARIO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VERIFICACION verificacionForm = new VERIFICACION(superUsuarios);
            verificacionForm.Show();
            this.Hide();
        }

        private void lMANUAL_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string rutaPDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MANUAL.pdf");
                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaPDF,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VERSION VERSION = new VERSION();
            VERSION.Show();
            this.Hide();
        }

        private void pictureBoxOjo_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
