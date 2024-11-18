using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using com.itextpdf.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using SISHBCC;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SISHBCC
{
    public partial class ALMACEN : Form
    {
        private Dictionary<string, string> fileDictionary = new Dictionary<string, string>();
        private Process explorerProcess;



        private Timer correoTimer;
        private List<MimeMessage> correosNoLeidos;
        private Notificacion notificacionForm;
        private NotifyIcon notifyIcon;
        private static ALMACEN instance;

        public ALMACEN()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            // Inicializa el PictureBox como invisible
            pictureBox1.Visible = false;

            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information, // Icono de la notificación
                Visible = true
            };
            // Establecer la instancia del formulario
            instance = this;
            // Timer para verificar correos
            correoTimer = new Timer
            {
                Interval = 30000 // Verificar cada 30 segundos
            };
            correoTimer.Tick += (s, e) => VerificarCorreos();
            correoTimer.Start();

            // Llama a VerificarCorreos() inmediatamente para establecer el estado inicial
            VerificarCorreos();

            // Suscribir el evento FormClosed al método ALMACEN_FormClosed
            this.FormClosed += ALMACEN_FormClosed;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void entradasNormalesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Entradas entradas = new Entradas();
     
            entradas.ShowDialog();
            this.Close();
        }

      

       

        private void clasificacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clasificacion clasificacion = new Clasificacion();
            clasificacion.ShowDialog();
            clasificacion.Close();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cambiar_contraseña cambiar_Contraseña = new Cambiar_contraseña();
            cambiar_Contraseña.Show();

            this.Hide();

            // Suponiendo que "this" hace referencia a la ventana "Menu"
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            productos.Show();
            this.Hide();

            // Suponiendo que "this" hace referencia a la ventana "Menu"
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Show();

            this.Hide();

            // Suponiendo que "this" hace referencia a la ventana "Menu"
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void colectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salidas salidas = new Salidas();
            salidas.Show();

            this.Hide();

            // Suponiendo que "this" hace referencia a la ventana "Menu"
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void aCERCADEToolStripMenuItem_Click(object sender, EventArgs e)
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
            string connectionString = "Data Source=ALEXANDERPC2;Initial Catalog=SISHBCC;Integrated Security=True;"; // Configura tu cadena de conexión aquí
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

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedFile = listBox1.SelectedItem.ToString();
                if (fileDictionary.ContainsKey(selectedFile))
                {
                    string filePath = fileDictionary[selectedFile];
                    try
                    {
                        // Abre el archivo con la aplicación predeterminada
                        Process.Start(filePath);

                        // Abre el explorador de archivos en la ubicación del archivo y selecciona el archivo
                        if (explorerProcess == null || explorerProcess.HasExited)
                        {
                            explorerProcess = Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No se pudo abrir el archivo o su ubicación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void almacenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Obtiene el directorio de escritorio del usuario actual
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string almacenPath = Path.Combine(desktopPath, "ALMACEN");

            // Lista para almacenar los directorios a procesar
            List<string> directories = new List<string>();

            if (Directory.Exists(almacenPath))
            {
                directories.Add(almacenPath);

                // Agrega subdirectorios dentro de la carpeta ALMACEN
                string[] subDirectories = Directory.GetDirectories(almacenPath, "*", SearchOption.AllDirectories);
                directories.AddRange(subDirectories);
            }
            else
            {
                MessageBox.Show($"La carpeta ALMACEN no existe en el escritorio.", "Directorio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Limpia el ListBox y el diccionario
            listBox1.Items.Clear();
            fileDictionary.Clear();

            // Recorre cada directorio y obtiene los archivos
            foreach (string directoryPath in directories)
            {
                string[] files = Directory.GetFiles(directoryPath);

                // Agrega cada archivo al ListBox y al diccionario
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    if (!fileDictionary.ContainsKey(fileName)) // Para evitar duplicados
                    {
                        listBox1.Items.Add(fileName);
                        fileDictionary[fileName] = file;
                    }
                }
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
                   "JORGE CECILIO CALYMAYOR ESPINOZA\n" +
                   "RODRIGO NATAREN RIOS\n" +
                   "Información de los Responsables");
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SISHBCC.Menu menu = Application.OpenForms.OfType<SISHBCC.Menu>().FirstOrDefault();

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static ALMACEN GetInstance()
        {
            return instance;
        }

        private void ALMACEN_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Detener el timer cuando se cierra el formulario
            correoTimer.Stop();
            instance = null; // Limpiar la instancia
        }

        private void VerificarCorreos()
        {
            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap-mail.outlook.com", 993, true);
                    client.Authenticate("desmon12@outlook.es", "ZXCVBNM12*");

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    var query = SearchQuery.NotSeen;
                    var uids = inbox.Search(query);

                    if (uids.Count > 0)
                    {
                        var nuevosCorreos = new List<MimeMessage>();
                        foreach (var uid in uids)
                        {
                            var message = inbox.GetMessage(uid);
                            nuevosCorreos.Add(message);
                        }

                        // Solo muestra notificaciones si hay correos nuevos
                        if (nuevosCorreos.Count > 0)
                        {
                            correosNoLeidos = nuevosCorreos;
                            // Muestra una alerta en la bandeja del sistema
                            notifyIcon.BalloonTipTitle = "Nuevos Correos";
                            notifyIcon.BalloonTipText = $"Tienes {nuevosCorreos.Count} correos nuevos.";
                            notifyIcon.ShowBalloonTip(3000); // Muestra la notificación por 3 segundos

                            correosNoLeidos = nuevosCorreos; // Guarda los correos para mostrarlos cuando se haga clic en el PictureBox
                            pictureBox1.Visible = true; // Muestra el PictureBox
                        }
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {

            }
        }

      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (correosNoLeidos != null && correosNoLeidos.Count > 0)
            {
                var notificacionForm = new Notificacion(correosNoLeidos);
                notificacionForm.Show();
                pictureBox1.Visible = false; // Oculta el PictureBox después de hacer clic
            }
            else
            {
                MessageBox.Show("No hay correos no leídos", "Información");
            }
        }
    }
}
    

