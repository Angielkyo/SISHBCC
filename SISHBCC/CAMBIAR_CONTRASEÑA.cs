using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SISHBCC

{
    public partial class Cambiar_contraseña : Form
    {
        private int codigoVerificacion; // Variable para almacenar el código de verificación generado
        private bool codigoVerificado = false;
        private string connectionString;
        private string correoServidor;
        private string contraseñaServidor;
        private Form _formPadre;

        public Cambiar_contraseña()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Leer configuración desde app.config
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            correoServidor = ConfigurationManager.AppSettings["CorreoServidor"];
            contraseñaServidor = ConfigurationManager.AppSettings["ContraseñaServidor"];
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correoUsuario = textBox1.Text;

            if (string.IsNullOrWhiteSpace(correoUsuario))
            {
                MessageBox.Show("Por favor, ingresa tu correo electrónico.");
                return;
            }

            // Verificar si el correo existe en la base de datos
            if (!CorreoExisteEnBD(correoUsuario))
            {
                MessageBox.Show("El correo electrónico proporcionado no está registrado.");
                return;
            }

            // Generar el código de verificación y enviar correo
            codigoVerificacion = GenerarCodigoVerificacion();
            EnviarCorreo(correoUsuario, codigoVerificacion);

            // Mostrar ventana emergente para ingresar código de verificación
            VentanaCodigoVerificacion ventanaCodigo = new VentanaCodigoVerificacion();
            if (ventanaCodigo.ShowDialog() == DialogResult.OK)
            {
                int codigoIngresado = ventanaCodigo.CodigoIngresado;

                // Verificar el código de verificación ingresado con el código generado
                if (codigoIngresado == codigoVerificacion)
                {
                    MessageBox.Show("Código de verificación correcto. Puede cambiar su contraseña.");
                    codigoVerificado = true;
                    // Abrir ventana para modificar contraseña
                    VentanaModificarContraseña ventanaModificarContraseña = new VentanaModificarContraseña();
                    if (ventanaModificarContraseña.ShowDialog() == DialogResult.OK)
                    {
                        string nuevaContraseña = ventanaModificarContraseña.NuevaContraseña;
                        string confirmarContraseña = ventanaModificarContraseña.ConfirmarContraseña;

                        // Verificar si las contraseñas coinciden
                        if (nuevaContraseña == confirmarContraseña)
                        {
                            // Modificar la contraseña en la base de datos
                            if (ModificarContraseñaEnBD(correoUsuario, nuevaContraseña))
                            {
                                MessageBox.Show("Contraseña modificada correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("Error al intentar modificar la contraseña.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se canceló la modificación de contraseña.");
                    }
                }
                else
                {
                    MessageBox.Show("Código de verificación incorrecto. Por favor, inténtelo de nuevo.");
                }
            }
        }

        private bool ModificarContraseñaEnBD(string correoUsuario, string nuevaContraseña)
        {
            string query = "UPDATE Usuarios SET Contraseña = @Contraseña WHERE Correo = @Correo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Contraseña", nuevaContraseña);
                command.Parameters.AddWithValue("@Correo", correoUsuario);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si la actualización fue exitosa
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar modificar la contraseña: " + ex.Message);
                    return false;
                }
            }
        }

        private bool CorreoExisteEnBD(string correo)
        {
            bool correoExiste = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Correo", correo);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    correoExiste = (count > 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el correo electrónico en la base de datos: " + ex.Message);
                }
            }

            return correoExiste;
        }

        //"Envía un código de verificación al correo ingresado."
        private int GenerarCodigoVerificacion()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        private void EnviarCorreo(string correoDestino, int codigoVerificacion)
        {
            string mensaje = "Su código de verificación para cambiar la contraseña es: " + codigoVerificacion;

            using (MailMessage mail = new MailMessage(correoServidor, correoDestino))
            {
                mail.Subject = "Código de verificación para cambiar la contraseña";
                mail.Body = mensaje;

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(correoServidor, contraseñaServidor);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }

    public class VentanaCodigoVerificacion : Form
    {
        public int CodigoIngresado { get; private set; }

        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnAceptar; // Botón para aceptar el código ingresado

        public VentanaCodigoVerificacion()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button(); // Inicializar el botón
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(40, 45);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(100, 75);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 35);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar"; // Texto del botón
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click); // Evento de clic del botón
            // 
            // VentanaCodigoVerificacion
            // 
            this.ClientSize = new System.Drawing.Size(200, 150);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Código de Verificación";
            this.Controls.Add(this.btnAceptar); // Agregar el botón a la ventana
            this.Controls.Add(this.txtCodigo);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int codigo;
            if (int.TryParse(txtCodigo.Text, out codigo))
            {
                CodigoIngresado = codigo;
                DialogResult = DialogResult.OK; // Establecer el resultado del diálogo como OK
                Close(); // Cerrar la ventana
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un código de verificación válido.");
            }
        }
    }

    public class VentanaModificarContraseña : Form
    {
        public string NuevaContraseña { get; private set; }
        public string ConfirmarContraseña { get; private set; }

        private Label lblNuevaContraseña;
        private Label lblConfirmarContraseña;
        private System.Windows.Forms.TextBox txtNuevaContraseña;
        private System.Windows.Forms.TextBox txtConfirmarContraseña;
        private System.Windows.Forms.Button btnAceptar;

        public VentanaModificarContraseña()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.lblNuevaContraseña = new System.Windows.Forms.Label();
            this.lblConfirmarContraseña = new System.Windows.Forms.Label();
            this.txtNuevaContraseña = new System.Windows.Forms.TextBox();
            this.txtConfirmarContraseña = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNuevaContraseña
            // 
            this.lblNuevaContraseña.AutoSize = true;
            this.lblNuevaContraseña.Location = new System.Drawing.Point(50, 20);
            this.lblNuevaContraseña.Name = "lblNuevaContraseña";
            this.lblNuevaContraseña.Size = new System.Drawing.Size(95, 13);
            this.lblNuevaContraseña.TabIndex = 0;
            this.lblNuevaContraseña.Text = "Nueva contraseña";
            // 
            // lblConfirmarContraseña
            // 
            this.lblConfirmarContraseña.AutoSize = true;
            this.lblConfirmarContraseña.Location = new System.Drawing.Point(50, 60);
            this.lblConfirmarContraseña.Name = "lblConfirmarContraseña";
            this.lblConfirmarContraseña.Size = new System.Drawing.Size(112, 13);
            this.lblConfirmarContraseña.TabIndex = 1;
            this.lblConfirmarContraseña.Text = "Confirmar contraseña";
            // 
            // txtNuevaContraseña
            // 
            this.txtNuevaContraseña.Location = new System.Drawing.Point(50, 40);
            this.txtNuevaContraseña.Name = "txtNuevaContraseña";
            this.txtNuevaContraseña.Size = new System.Drawing.Size(150, 20);
            this.txtNuevaContraseña.TabIndex = 2;
            // 
            // txtConfirmarContraseña
            // 
            this.txtConfirmarContraseña.Location = new System.Drawing.Point(50, 80);
            this.txtConfirmarContraseña.Name = "txtConfirmarContraseña";
            this.txtConfirmarContraseña.Size = new System.Drawing.Size(150, 20);
            this.txtConfirmarContraseña.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(75, 120);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 33);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // VentanaModificarContraseña
            // 
            this.ClientSize = new System.Drawing.Size(250, 150);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtConfirmarContraseña);
            this.Controls.Add(this.txtNuevaContraseña);
            this.Controls.Add(this.lblConfirmarContraseña);
            this.Controls.Add(this.lblNuevaContraseña);
            this.ResumeLayout(false);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Obtener las contraseñas ingresadas y cerrar la ventana
            NuevaContraseña = txtNuevaContraseña.Text;
            ConfirmarContraseña = txtConfirmarContraseña.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
