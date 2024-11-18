 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using System.Drawing.Printing;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;


namespace SISHBCC
{
    public partial class Recetario : Form
    {
        private int contador = 0; // Folio
        private PrintDocument printDoc = new PrintDocument(); // Sistema de impresión
        private string carpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Receta");
        private string nombreUsuario; // Almacén de nombre del usuario
        private SqlConnection connection;
        private readonly List<Medicamento> listaMedicamentos = new List<Medicamento>();
        private bool updatingComboBox = false;

        public Recetario()
        {
            InitializeComponent();
            nombreUsuario = Sesion.NombreUsuario;
            this.StartPosition = FormStartPosition.CenterScreen;
            printDoc.PrintPage += PrintDoc_PrintPage;
            MostrarFechaActual();
            InicializarConexion();
            CargarDatosUsuario();
            LlenarComboBox(comboBox3);
            LlenarComboBox(comboBox4);
            LlenarComboBox(comboBox5);
            ConfigurarEventosComboBox(comboBox3, textBox9);
            ConfigurarEventosComboBox(comboBox4, textBox12);
            ConfigurarEventosComboBox(comboBox5, textBox15);
            // Asignar el evento KeyDown a los controles de texto
            textBox1.KeyDown += Control_KeyDown;
            textBox2.KeyDown += Control_KeyDown;
            textBox3.KeyDown += Control_KeyDown;
            textBox4.KeyDown += Control_KeyDown;
            textBox7.KeyDown += Control_KeyDown;
            textBox8.KeyDown += Control_KeyDown;
            textBox9.KeyDown += Control_KeyDown;
            textBox10.KeyDown += Control_KeyDown;
            textBox11.KeyDown += Control_KeyDown;
            textBox12.KeyDown += Control_KeyDown;
            textBox13.KeyDown += Control_KeyDown;
            textBox14.KeyDown += Control_KeyDown;
            textBox15.KeyDown += Control_KeyDown;
            textBox16.KeyDown += Control_KeyDown;
            textBox5.KeyDown += Control_KeyDown;
            textBox6.KeyDown += Control_KeyDown;
            textBox17.KeyDown += Control_KeyDown;
            textBox18.KeyDown += Control_KeyDown;
            textBox19.KeyDown += Control_KeyDown;
            textBox20.KeyDown += Control_KeyDown;
            textBox21.KeyDown += Control_KeyDown;
            textBox22.KeyDown += Control_KeyDown;
            // Asignar el evento KeyDown a los ComboBox
            comboBox1.KeyDown += Control_KeyDown;
            comboBox2.KeyDown += Control_KeyDown;
            comboBox3.KeyDown += Control_KeyDown;
            comboBox4.KeyDown += Control_KeyDown;
            comboBox5.KeyDown += Control_KeyDown;
            comboBox6.KeyDown += Control_KeyDown;
            // Asignar el evento KeyDown al CheckBox
            checkBox1.KeyDown += Control_KeyDown;
        }
        // Asignar el evento KeyDown para loss controles
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevenir el sonido 'ding' de la tecla Enter

                // Lista de controles a omitir
                List<Control> omittedControls = new List<Control> { textBox16, textBox18 };

                // Obtener el siguiente control en el orden de tabulación
                Control nextControl = GetNextControl((Control)sender, true);
                while (nextControl != null && (!nextControl.TabStop || omittedControls.Contains(nextControl)))
                {
                    nextControl = GetNextControl(nextControl, true);
                }

                // Si se encuentra un control válido, mover el foco a él
                if (nextControl != null)
                {
                    nextControl.Focus();

                    // Si el siguiente control es un ComboBox, desplegar la lista
                    if (nextControl is System.Windows.Forms.ComboBox comboBox)
                    {
                        comboBox.DroppedDown = true;
                    }
                }
            }
        }

        private void LlenarComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            string query = "SELECT CLAVE, DESCRIPCION FROM MEDICAMENTOS";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string clave = reader["CLAVE"] as string ?? string.Empty;
                    string descripcion = reader["DESCRIPCION"] as string ?? string.Empty;
                    Medicamento medicamento = new Medicamento(clave, descripcion);
                    listaMedicamentos.Add(medicamento);
                    comboBox.Items.Add(medicamento);
                }
                reader.Close();
            }
        }

        private void ConfigurarEventosComboBox(System.Windows.Forms.ComboBox comboBox, Control nextControl)
        {
            comboBox.TextChanged += (sender, e) => comboBox_TextChanged(sender, e, comboBox);
            comboBox.KeyDown += comboBox_KeyDown;
            comboBox.SelectedIndexChanged += (sender, e) => comboBox_SelectedIndexChanged(sender, e, comboBox);
            comboBox.SelectionChangeCommitted += (sender, e) => comboBox_SelectionChangeCommitted(sender, e, comboBox, nextControl);
        }

        private void comboBox_TextChanged(object sender, EventArgs e, System.Windows.Forms.ComboBox comboBox)
        {
            if (updatingComboBox) return;

            // Si el elemento seleccionado es el mismo que el texto actual, no hacer nada
            if (comboBox.SelectedItem is Medicamento seleccionado && comboBox.Text == seleccionado.ToString())
            {
                return;
            }

            updatingComboBox = true;
            string filtro = comboBox.Text;
            comboBox.BeginUpdate();
            comboBox.Items.Clear();

            foreach (Medicamento medicamento in listaMedicamentos)
            {
                if (medicamento.Descripcion.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    medicamento.Clave.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    comboBox.Items.Add(medicamento);
                }
            }

            comboBox.DroppedDown = true;
            comboBox.SelectionStart = filtro.Length;
            comboBox.SelectionLength = 0;
            comboBox.EndUpdate();
            updatingComboBox = false;
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e, System.Windows.Forms.ComboBox comboBox)
        {
            if (!updatingComboBox)
            {
                Medicamento seleccionado = comboBox.SelectedItem as Medicamento;
                if (seleccionado != null)
                {
                    updatingComboBox = true;
                    comboBox.Text = seleccionado.ToString();
                    updatingComboBox = false;
                }
            }
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e, System.Windows.Forms.ComboBox comboBox, Control nextControl)
        {
            // Este evento se dispara cuando el usuario hace clic en un elemento de la lista desplegable
            Medicamento seleccionado = comboBox.SelectedItem as Medicamento;
            if (seleccionado != null)
            {
                updatingComboBox = true;
                comboBox.Text = seleccionado.ToString();
                updatingComboBox = false;

                // Mueve el foco al siguiente control
                nextControl.Focus();
            }
        }

        public class Medicamento
        {
            public string Clave { get; }
            public string Descripcion { get; }

            public Medicamento(string clave, string descripcion)
            {
                Clave = clave;
                Descripcion = descripcion;
            }

            public override string ToString()
            {
                return $"{Clave} - {Descripcion}";
            }
        }





        //la imprecion empieza aca

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // Ajustar la escala general
                float scale = 0.95f; // Reducir la escala a 95%
                e.Graphics.ScaleTransform(scale, scale);

                // Obtener el objeto Graphics desde el evento
                Graphics g = e.Graphics;

                // Obtener el tamaño del área imprimible de la página
                Rectangle printableArea = e.PageBounds;

                // Calcular el margen y el espacio entre impresiones
                int margin = 10; // Reducir el margen
                int espacioEntrePaginas = 2; // Reducir el espacio entre páginas

                // Calcular el tamaño de la parte superior y la parte inferior de la página
                int parteSuperiorHeight = printableArea.Height / 2 - margin - espacioEntrePaginas / 2;
                int parteInferiorHeight = printableArea.Height / 2 - margin - espacioEntrePaginas / 2;

                // Calcular el rectángulo para la parte superior
                Rectangle rectParteSuperior = new Rectangle(margin, margin, printableArea.Width - 2 * margin, parteSuperiorHeight);

                // Calcular el rectángulo para la parte inferior
                Rectangle rectParteInferior = new Rectangle(margin, printableArea.Height - margin - parteInferiorHeight, printableArea.Width - 2 * margin, parteInferiorHeight);

                // Dibujar la parte superior
                Bitmap bmpSuperior = new Bitmap(groupBox1.Width, groupBox1.Height);
                groupBox1.DrawToBitmap(bmpSuperior, groupBox1.ClientRectangle);
                g.DrawImage(bmpSuperior, rectParteSuperior);

                // Dibujar la parte inferior
                Bitmap bmpInferior = new Bitmap(groupBox1.Width, groupBox1.Height);
                groupBox1.DrawToBitmap(bmpInferior, groupBox1.ClientRectangle);
                g.DrawImage(bmpInferior, rectParteInferior);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje
                MessageBox.Show($"Error al imprimir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                // Consulta SQL para obtener información adicional del usuario
                string consultaSQL = "SELECT Nombre, Apellido_P, Apellido_M, Cedula FROM Usuarios WHERE NOMBRE = @Nombre";

                using (SqlCommand command = new SqlCommand(consultaSQL, connection))
                {
                    // Asegúrate de ajustar el tipo de datos del parámetro y establecer el tamaño
                    command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreUsuario;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Concatenar nombre y apellidos
                            string nombreCompleto = $"{reader.GetString(0)} {reader.GetString(1)} {reader.GetString(2)}";

                            // Asignar el nombre completo al TextBox
                            textBox16.Text = nombreCompleto;

                            // Asignar la cédula al TextBox correspondiente
                            textBox18.Text = reader.GetString(3); // Cedula
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del usuario: " + ex.Message);
            }
        }

        private void MostrarFechaActual()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Mostrar la fecha actual en el label
            label40.Text = $"Fecha : {fechaActual:dd} de {fechaActual:MMMM} del {fechaActual:yyyy}";
        }

        public void SetTextBoxText(string text)
        {
            label39.Text = text; // Mando a llamar el folio
        }

        private void LimpiarControles()
        {
            // Limpiar TextBox
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox17.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            // Limpiar ComboBox
            LimpiarComboBox(comboBox1);
            LimpiarComboBox(comboBox2);
            LimpiarComboBox(comboBox3);
            LimpiarComboBox(comboBox4);
            LimpiarComboBox(comboBox5);
            LimpiarComboBox(comboBox6);
            // Limpiar CheckBox
            checkBox1.Checked = false;
        }

        private void LimpiarComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.SelectedIndex = -1;
            comboBox.DroppedDown = false; // Asegurar que el ComboBox no esté desplegado
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar el cuadro de diálogo de impresión
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                    // Imprimir el documento
                    printDoc.Print();

                // Obtener la ruta completa para guardar el PDF
                string rutaCompleta = Path.Combine(carpetaDestino, ObtenerNombreUnicoParaPDF());

                // Guardar el PDF después de imprimir
                GuardarPDF(rutaCompleta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir y guardar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerNombreUnicoParaPDF()
        {
            // Obtener la fecha y hora actual como parte del nombre del archivo
            string fechaHoraActual = DateTime.Now.ToString("yyMMdd_HHmm");
            return $"receta_{fechaHoraActual}.pdf";
        }

        private void GuardarPDF(string ruta)
        {
            try
            {
                // Verificar si la carpeta de destino existe, si no, crearla
                if (!Directory.Exists(carpetaDestino))
                {
                    Directory.CreateDirectory(carpetaDestino);
                }

                // Combinar la ruta de destino con el nombre del archivo PDF
                string rutaCompleta = Path.Combine(carpetaDestino, ruta);

                using (var writer = new PdfWriter(rutaCompleta))
                using (var pdf = new PdfDocument(writer))
                {
                    // Crear un nuevo documento PDF
                    var document = new Document(pdf);

                    // Convertir el contenido del GroupBox a un Paragraph en el documento PDF
                    Bitmap bmp = new Bitmap(groupBox1.Width, groupBox1.Height);
                    groupBox1.DrawToBitmap(bmp, groupBox1.ClientRectangle);

                    // Ajustar el tamaño de la imagen (cambiar 0.5 por el factor de escala deseado)
                    float scaleFactor = 0.9f;  // Ajusta según tus necesidades
                    Bitmap resizedImage = ResizeImage(bmp, (int)(bmp.Width * scaleFactor), (int)(bmp.Height * scaleFactor));

                    // Convertir la imagen a formato iTextSharp
                    var image = new iText.Layout.Element.Image(ImageDataFactory.Create(resizedImageToByteArray(resizedImage)));

                    // Ajustar el tamaño de la imagen en el documento PDF
                    image.SetAutoScale(true);

                    // Agregar la imagen al documento PDF
                    document.Add(image);

                    // Mostrar mensaje solo para confirmar que se ha guardado el PDF
                    MessageBox.Show($"PDF guardado automáticamente en {rutaCompleta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Asegurar que la ventana principal vuelva a tomar el enfoque
                this.Activate();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje
                MessageBox.Show($"Error al guardar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Asegurar que la ventana principal vuelva a tomar el enfoque
                this.Activate();
            }
        }

        private Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private byte[] resizedImageToByteArray(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        private void InicializarConexion()
        {
            // Leer la cadena de conexión desde app.config
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

                // Obtener el valor actual del label39
                string folioActual = label39.Text;

                // Dividir el texto en partes usando los espacios como delimitadores
                string[] partes = folioActual.Split(new[] { ' ' }, StringSplitOptions.None);

                // Tomar la última parte (que debería ser el número)
                string ultimaParte = partes.Last();

                // Utilizar Regex para encontrar el número en la última parte
                Match match = Regex.Match(ultimaParte, @"\d+");
                if (match.Success)
                {
                    // Obtener el número actual del folio
                    if (int.TryParse(match.Value, out int valorActual))
                    {
                        // Incrementar el valor
                        contador = valorActual + 1;

                        // Formatear el número incrementado con el mismo número de dígitos que el número original
                        string nuevoNumero = contador.ToString().PadLeft(match.Value.Length, '0');

                        // Reemplazar el número en la última parte del texto
                        ultimaParte = ultimaParte.Substring(0, match.Index) + nuevoNumero;

                        // Reconstruir el texto original con la parte modificada
                        partes[partes.Length - 1] = ultimaParte;
                        string nuevoFolio = string.Join(" ", partes);

                        // Actualizar el label39 con el nuevo folio
                        label39.Text = nuevoFolio;

                        // Guardar el nuevo folio en la base de datos
                        GuardarFolioEnBD(nuevoFolio);
                    }
                }

                LimpiarControles(); // Llamar a método para limpiar los controles
                textBox1.Focus(); // Poner el foco en textBox1
            }

            private void GuardarFolioEnBD(string nuevoFolio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FOLIOS (Folio) VALUES (@Folio)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Folio", nuevoFolio);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el folio en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Obtener el nombre del archivo PDF guardado
            string rutaArchivoPdf = Path.Combine(carpetaDestino, ObtenerNombreUnicoParaPDF());

            if (File.Exists(rutaArchivoPdf))
            {
                EnviarCorreoElectronico(rutaArchivoPdf);
            }
            else
            {
                MessageBox.Show("El archivo PDF no se encuentra en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Metodo para enviar por correo electronico
        private void EnviarCorreoElectronico(string rutaArchivoPdf)
        {
            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient clienteSmtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("FarmaciaSIS@outlook.com", "usuario12")
                };

                mensaje.From = new MailAddress("FarmaciaSIS@outlook.com");
                mensaje.To.Add("pruebaproyec123@outlook.es");
                mensaje.Subject = "Archivo PDF Adjunto";
                mensaje.Body = "Adjunto encontrarás el archivo PDF generado.";

                Attachment adjunto = new Attachment(rutaArchivoPdf);
                mensaje.Attachments.Add(adjunto);

                clienteSmtp.Send(mensaje);

                MessageBox.Show("Correo electrónico enviado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el correo electrónico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
    