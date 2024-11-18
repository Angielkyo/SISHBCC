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
using System.Configuration;
using System.Data.SqlClient;


namespace SISHBCC
{
    public partial class Menu : Form
    {
        public string tipoUsuario;
        public static string TipoUsuario { get; set; }

        public Menu(string tipoUsuario)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            TipoUsuario = tipoUsuario; // Guardar el tipo de usuario recibido
        }

        private void Menu_Load_1(object sender, EventArgs e)
        {
            if (TipoUsuario == null)
            {
                MessageBox.Show("No tienes acceso al módulo.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Mostrar los enlaces según el tipo de usuario
            switch (TipoUsuario)
            {
                case "ALMACEN":
                    ALMACEN.Visible = true;
                    break;
                case "FARMACIA":
                    FARMACIA.Visible = true;
                    break;
                case "RECETARIO":
                    RECETARIO.Visible = true;
                    break;

                case "SuperUsuario":
                    ALMACEN.Visible = true;
                    FARMACIA.Visible = true;
                    RECETARIO.Visible = true;
                    // Agrega cualquier otra funcionalidad específica para superusuarios aquí
                    break;
                default:
                    MessageBox.Show("Tipo de usuario no reconocido.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    break;
            }
        }

        private void AbrirFormularioModulo(string modulo)
        {
            if (TipoUsuario != modulo && TipoUsuario != "ADMINISTRADOR" && TipoUsuario != "SuperUsuario")
            {
                MessageBox.Show($"No tienes acceso al módulo {modulo}.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (modulo)
            {
                case "ALMACEN":
                    CrearCarpetaEnEscritorio("ALMACEN");
                    ALMACEN aLMACEN = new ALMACEN();
                    aLMACEN.Show();
                    break;
                case "FARMACIA":
                    CrearCarpetaEnEscritorio("Farmacia");
                    FARMACIA fARMACIA = new FARMACIA();
                    fARMACIA.Show();
                    break;
                case "RECETARIO":
                    Folio rECETARIO = new Folio();
                    rECETARIO.Show();
                    break;
                default:
                    MessageBox.Show("Módulo no reconocido.");
                    break;
            }

            this.Hide();
        }

        private void CrearCarpetaEnEscritorio(string nombreCarpeta)
        {
            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaCarpeta = Path.Combine(rutaEscritorio, nombreCarpeta);

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                MessageBox.Show($"Carpeta '{nombreCarpeta}' creada en el escritorio.", "Carpeta Creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ALMACEN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirFormularioModulo("ALMACEN");
        }

        private void FARMACIA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirFormularioModulo("FARMACIA");
        }

        private void RECETARIO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirFormularioModulo("RECETARIO");
        }

        private void CERRARSESION_Click_1(object sender, EventArgs e)
        {
            Application.OpenForms
                   .OfType<Form>()
                   .ToList()
                   .ForEach(form => form.Close());
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms
                               .OfType<Form>()
                               .ToList()
                               .ForEach(form => form.Close());
            Application.Exit();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DESARROLLADO POR:\n" +
            "ILMER ALEXANDER VELAZQUEZ LOPEZ\n" +
            "RODRIGO NATAREN RIOS\n" +
            "ANGEL FABIAN PEREZ CRUZ",
            "Información del Desarrollador");
        }

        private void respaldarBaseDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string backupFilePath = @"C:\Respaldo\SISHBCC.bak";

            // Crear la carpeta si no existe
            string directoryPath = System.IO.Path.GetDirectoryName(backupFilePath);
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            BackupDatabase("SISHBCC", backupFilePath);
        }
        private void BackupDatabase(string databaseName, string backupFilePath)
        {
            // Leer la cadena de conexión desde el archivo app.config
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}' WITH INIT";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(backupQuery, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Respaldo de la base de datos completado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al respaldar la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cambiar_contraseña cambiar_Contraseña = new Cambiar_contraseña();
            cambiar_Contraseña.Show();

            this.Hide();


            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DESARROLLADO POR:\n" +
           "ILMER ALEXANDER VELAZQUEZ LOPEZ\n" +
           "RODRIGO NATAREN RIOS\n" +
           "ANGEL FABIAN PEREZ CRUZ",
           "Información del Desarrollador");
        }

        private void responsablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RESPONSABLES SISHBCC:\n" +
        "JORJE CECILIO CALYMAYOR ESPINOZA \n" +
        "SERGIO JOAQUIN MIJANGOS MARTINEZ",
        "Información de los responsables");
        }
    }
    
}
