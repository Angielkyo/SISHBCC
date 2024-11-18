using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using SISHBCC;
using System.Configuration;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace SISHBCC
{
    public partial class FARMACIA : Form
    {
        private string tipoUsuario;
        private readonly string connectionString;
        private List<Medicamento> todosLosMedicamentos = new List<Medicamento>();
        private Dictionary<string, string> datosFormulario;
        private bool updatingComboBox = false;
        private bool userSelected = true;

        private Timer correoTimer;
        private List<MimeMessage> correosNoLeidos;
        private Notificaciones notificacionesForm;
        private NotifyIcon notifyIcon;
        private static FARMACIA instance;
        public FARMACIA()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Obtener la cadena de conexión del archivo de configuración
            connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            LlenarComboBox();
            ConfigurarEventosComboBox();

            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            textBox1.KeyDown += Control_KeyDown;
            textBox2.KeyDown += Control_KeyDown;
            comboBox1.KeyDown += Control_KeyDown;

            // Inicializa el NotifyIcon
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information, // Icono de la notificación
                Visible = true
            };

            // Inicializa el PictureBox como invisible
            pictureBox1.Visible = false;

            // Establecer la instancia del formulario
            instance = this;

            // Timer para verificar correos
            correoTimer = new Timer
            {
                Interval = 30000 // Verificar cada 30 segundos
            };
            correoTimer.Tick += (s, e) => VerificarCorreos();
            correoTimer.Start();

            // Llamar a VerificarCorreos() inmediatamente para establecer el estado inicial
            VerificarCorreos();

            // Suscribir el evento FormClosed al método FARMACIA_FormClosed
            this.FormClosed += FARMACIA_FormClosed;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevenir el sonido 'ding' de la tecla Enter

                // Mover el foco al siguiente control
                if (sender == textBox1)
                {
                    textBox2.Focus();
                }
                else if (sender == textBox2)
                {
                    comboBox1.Focus();
                    comboBox1.DroppedDown = true;
                }
                else if (sender == comboBox1)
                {
                    // Si es el último control, puedes cambiar el foco al primer control o a otro según necesites
                    comboBox2.Focus();
                }
            }
        }

        private void ConfigurarEventosComboBox()
        {
            comboBox2.SelectionChangeCommitted += comboBox2_SelectionChangeCommitted;
            comboBox2.PreviewKeyDown += ComboBox2_PreviewKeyDown;
            comboBox2.KeyDown += ComboBox2_KeyDown;
            comboBox2.KeyUp += ComboBox2_KeyUp;
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

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si la celda seleccionada es de tipo DataGridViewButtonCell (suponiendo que tengas botones para eliminar en alguna columna)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Mostrar un mensaje de confirmación
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar esta fila?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Eliminar la fila del DataGridView
                    dataGridView1.Rows.Remove(selectedRow);
                }
            }
        }

        private async void LlenarComboBox()
        {
            string query = "SELECT Clave, Descripción FROM COMPENDIO$";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();  // Uso de async para mejorar la interfaz.

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            comboBox2.Items.Clear();
                            todosLosMedicamentos.Clear();
                            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();

                            while (await reader.ReadAsync())
                            {
                                var descripcion = reader["Descripción"].ToString();
                                var clave = reader["Clave"].ToString();

                                Medicamento medicamento = new Medicamento(clave, descripcion);
                                comboBox2.Items.Add(medicamento);
                                todosLosMedicamentos.Add(medicamento);

                                autoCompleteCollection.Add(medicamento.ToString());
                            }

                            comboBox2.AutoCompleteCustomSource = autoCompleteCollection;
                        }
                    }
                }
                catch (SqlException sqlEx)  // Manejo específico de problemas de SQL
                {
                    MessageBox.Show("Error de base de datos al llenar el ComboBox: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al llenar el ComboBox: " + ex.Message);
                }
            }

            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private bool suppressAutoComplete = false;

        private void ComboBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Verificar si la tecla presionada es una tecla de dirección
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                suppressAutoComplete = true;
            }
            else
            {
                suppressAutoComplete = false;
            }
        }

        private void ComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            // Ignorar las teclas de dirección si el ComboBox está cerrado
            if (suppressAutoComplete && !comboBox2.DroppedDown)
            {
                e.Handled = true;
                return;
            }
        }

        private void ComboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (suppressAutoComplete)
            {
                return;
            }

            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                string text = comboBox.Text.ToLower();
                List<Medicamento> filteredMedicamentos = new List<Medicamento>();

                foreach (Medicamento medicamento in todosLosMedicamentos)
                {
                    if (medicamento.Clave.ToLower().Contains(text) || medicamento.Descripcion.ToLower().Contains(text))
                    {
                        filteredMedicamentos.Add(medicamento);
                    }
                }

                if (filteredMedicamentos.Count > 0)
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(filteredMedicamentos.ToArray());

                    comboBox2.DroppedDown = true;
                    // Mantener la selección de texto
                    comboBox2.SelectionStart = text.Length;
                    comboBox2.SelectionLength = comboBox2.Text.Length - text.Length;
                    Cursor.Current = Cursors.Default;
                }
            }
        }


        private async void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var seleccionado = comboBox2.SelectedItem as Medicamento;
                if (seleccionado != null)
                {
                    string claveSeleccionada = seleccionado.Clave;
                    string descripcionSeleccionada = seleccionado.Descripcion;

                    string query = "SELECT COUNT(*) FROM [LISTA DE MEDICAMENTOS$] WHERE Clave = @Clave";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Clave", claveSeleccionada);

                        try
                        {
                            await connection.OpenAsync();
                            int count = (int)await command.ExecuteScalarAsync();

                            if (count > 0)
                            {
                                MessageBox.Show($"El medicamento '{descripcionSeleccionada}' está en existencia en [LISTA DE MEDICAMENTOS$].");

                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridView1);

                                row.Cells[0].Value = claveSeleccionada;
                                row.Cells[1].Value = descripcionSeleccionada;

                                dataGridView1.Rows.Add(row);
                            }
                            else
                            {
                                MessageBox.Show($"El medicamento '{descripcionSeleccionada}' no está en existencia en [LISTA DE MEDICAMENTOS$].");
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            MessageBox.Show("Error de base de datos: " + sqlEx.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al comprobar la existencia del medicamento: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void FARMACIA_Load(object sender, EventArgs e)
        {
            comboBox2.MaxDropDownItems = 5;
            comboBox2.IntegralHeight = false;
        }

        private bool VerificarCamposCompletos()
        {
            return !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) &&
                   dateTimePicker1.Value != DateTime.MinValue && dateTimePicker2.Value != DateTime.MinValue &&
                   comboBox1.SelectedItem != null &&
                   dataGridView1.Rows.Count > 0 && dataGridView1.Columns.Count > 0;
        }

        private void guardarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (VerificarCamposCompletos())
            {
                datosFormulario = new Dictionary<string, string>
                {
                    { "Nombre y Clave de la Jurisdicción Distrito No.1", string.Empty },
                    { "Nombre y Clave de la Unidad Médica: HOSPITAL BASICO COMUNITARIO", string.Empty },
                    { "Cobertura: Población Abierta", string.Empty },
                    { "Fecha", DateTime.Now.ToShortDateString() },
                    { "FECHA", dateTimePicker1.Value.ToShortDateString() },
                    { "TURNO", comboBox1.SelectedItem?.ToString() ?? string.Empty },
                    { "HORA", dateTimePicker2.Value.ToShortTimeString() }
                };

                // Obtener la ruta del escritorio del usuario y crear la carpeta "Farmacia/Colectivo"
                string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string rutaCarpetaFarmacia = Path.Combine(rutaEscritorio, "Farmacia");
                string rutaCarpetaColectivo = Path.Combine(rutaCarpetaFarmacia, "Colectivo");

                if (!Directory.Exists(rutaCarpetaColectivo))
                {
                    Directory.CreateDirectory(rutaCarpetaColectivo);
                }

                // Generar un nombre único para el archivo
                string nombreArchivoOriginal = "Colectivo.xlsx";
                string nombreArchivoUnico = GenerarNombreUnico(nombreArchivoOriginal);

                // Definir la ruta del archivo en la carpeta "Colectivo"
                string rutaArchivo = Path.Combine(rutaCarpetaColectivo, nombreArchivoUnico);

                // Copiar el archivo original al directorio de salida si no existe
                string rutaArchivoOriginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivoOriginal);
                if (!File.Exists(rutaArchivo))
                {
                    File.Copy(rutaArchivoOriginal, rutaArchivo);
                }

                // Agregar datos al archivo Excel
                AgregarDatosAExcel(dataGridView1, textBox1, textBox2, dateTimePicker1, dateTimePicker2, comboBox1, rutaArchivo);

                if (File.Exists(rutaArchivo))
                {
                    // Crear respaldo del archivo
                    string extensionArchivo = Path.GetExtension(rutaArchivo);
                    string fechaHoraActual = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    //string nombreArchivoRespaldo = $"{Path.GetFileNameWithoutExtension(nombreArchivoUnico)}Respaldo{fechaHoraActual}{extensionArchivo}";

                    //string rutaRespaldo = Path.Combine(rutaCarpetaColectivo, nombreArchivoRespaldo);

                    //File.Copy(rutaArchivo, rutaRespaldo, true);

                    // Enviar correo electrónico con el archivo guardado
                    EnviarCorreoElectronico(rutaArchivo);

                    // Abrir el archivo guardado
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = rutaArchivo,
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show("El archivo no existe en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar completos antes de guardar o imprimir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarNombreUnico(string nombreArchivoOriginal)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string nombreSinExtension = Path.GetFileNameWithoutExtension(nombreArchivoOriginal);
            string extension = Path.GetExtension(nombreArchivoOriginal);
            return $"{nombreSinExtension}_{timestamp}{extension}";
        }

        private void AgregarDatosAExcel(DataGridView dataGridView, System.Windows.Forms.TextBox textBox1, System.Windows.Forms.TextBox textBox2, DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2, System.Windows.Forms.ComboBox comboBox1, string filePath)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("El archivo Excel especificado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Hoja1"];

                    worksheet.Cells[4, 3].Value = textBox1.Text;
                    worksheet.Cells[4, 6].Value = textBox2.Text;
                    worksheet.Cells[7, 5].Value = dateTimePicker1.Value.Day;
                    worksheet.Cells[7, 6].Value = dateTimePicker1.Value.Month;
                    worksheet.Cells[7, 7].Value = dateTimePicker1.Value.Year;
                    worksheet.Cells[6, 9].Value = dateTimePicker2.Value.ToString("HH:mm");
                    worksheet.Cells[6, 8].Value = comboBox1.SelectedItem?.ToString() ?? "";

                    int filaInicioDatos = 10;
                    int columnaInicioDatos = 1;

                    foreach (DataGridViewRow filaDataGridView in dataGridView.Rows)
                    {
                        worksheet.Cells[filaInicioDatos, columnaInicioDatos].Value = filaDataGridView.Cells["CLCLAVE"].Value?.ToString();
                        worksheet.Cells[filaInicioDatos, columnaInicioDatos + 1].Value = filaDataGridView.Cells["CLDESCRIPCION"].Value?.ToString();
                        worksheet.Cells[filaInicioDatos, columnaInicioDatos + 3].Value = filaDataGridView.Cells["CLPRESENTACION"].Value?.ToString();
                        worksheet.Cells[filaInicioDatos, columnaInicioDatos + 4].Value = filaDataGridView.Cells["CLCANTIDAD"].Value?.ToString();
                        worksheet.Cells[filaInicioDatos, columnaInicioDatos + 7].Value = filaDataGridView.Cells["CLCSURTIDA"].Value?.ToString();

                        filaInicioDatos++;
                    }

                    excelPackage.Save();
                }

                MessageBox.Show("Los datos se han agregado correctamente al archivo Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar datos al archivo Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnviarCorreoElectronico(string rutaArchivo)
        {
            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient clienteSmtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("pruebaproyec123@outlook.es", "Z123RODR")
                };

                mensaje.From = new MailAddress("pruebaproyec123@outlook.es");
                mensaje.To.Add("desmon12@outlook.es");
                mensaje.Subject = "Archivo Excel";
                mensaje.Body = "Adjunto encontrarás el archivo Excel.";

                Attachment adjunto = new Attachment(rutaArchivo);
                mensaje.Attachments.Add(adjunto);

                clienteSmtp.Send(mensaje);

                MessageBox.Show("Correo electrónico enviado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el correo electrónico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CATALAGO cATALAGO = new CATALAGO();
            cATALAGO.Show();
            this.Hide();
        }

        private void medicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MEDICOS mEDICOS = new MEDICOS();
            mEDICOS.Show();
            this.Hide();
        }

        private void entradaFarmaciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENTRADA eNTRADA = new ENTRADA();
            eNTRADA.Show();
            this.Hide();
        }

        private void salidaFarmaciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SALIDA sALIDA = new SALIDA();
            sALIDA.Show();
            this.Hide();
        }

        private void detalladoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PRODUCTOS_CADUCADOS pRODUCTOS_CADUCADOS = new PRODUCTOS_CADUCADOS();
            pRODUCTOS_CADUCADOS.Show();
            this.Hide();
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


        private void productosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CATALAGO cATALAGO = new CATALAGO();
            cATALAGO.Show();
            this.Hide();
        }

        private void entradaFarmaciaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ENTRADA eNTRADA = new ENTRADA();
            eNTRADA.Show();
            this.Hide();
        }

        private void salidaFarmaciaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SALIDA sALIDA = new SALIDA();
            sALIDA.Show();
            this.Hide();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpia los TextBox
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            // Limpia los ComboBox
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            // Limpia el DataGridView
            dataGridView1.Rows.Clear();
        }
        private void archivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string farmaciaPath = Path.Combine(desktopPath, "Farmacia");

            // Lista para almacenar los directorios a procesar
            List<string> directories = new List<string>();

            try
            {
                if (Directory.Exists(farmaciaPath))
                {
                    directories.Add(farmaciaPath);

                    // Agrega subdirectorios dentro de la carpeta Farmacia
                    string[] subDirectories = Directory.GetDirectories(farmaciaPath, "*", SearchOption.AllDirectories);
                    directories.AddRange(subDirectories);
                }
                else
                {
                    MessageBox.Show("La carpeta Farmacia no existe en el escritorio.", "Directorio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Diccionario para almacenar los archivos
                Dictionary<string, string> fileDictionary = new Dictionary<string, string>();

                // Recorre cada directorio y obtiene los archivos
                foreach (string directoryPath in directories)
                {
                    string[] files = Directory.GetFiles(directoryPath);

                    // Agrega cada archivo al diccionario
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        if (!fileDictionary.ContainsKey(fileName)) // Para evitar duplicados
                        {
                            fileDictionary[fileName] = file;
                        }
                    }
                }

                // Abre el formulario Reportes y pasa los archivos
                Reportes reportesForm = new Reportes(fileDictionary);
                reportesForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static FARMACIA GetInstance()
        {
            return instance;
        }

        private void FARMACIA_FormClosed(object sender, FormClosedEventArgs e)
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
                    client.Authenticate("pruebaproyec123@outlook.es", "Z123RODR");

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
                var notificacionesForm = new Notificaciones(correosNoLeidos);
                notificacionesForm.Show();
                pictureBox1.Visible = false; // Oculta el PictureBox después de hacer clic
            }
            else
            {
                MessageBox.Show("No hay correos no leídos", "Información");
            }
        }

        private void medicosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MEDICOS mEDICOS = new MEDICOS();
            mEDICOS.Show();
            this.Hide();

        }

        private void productosCaducadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void detalladoToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            PRODUCTOS_CADUCADOS pRODUCTOS_CADUCADOS = new PRODUCTOS_CADUCADOS();
            pRODUCTOS_CADUCADOS.Show();
            this.Hide();
        }
    }
}