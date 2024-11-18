using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISHBCC
{
    public partial class Notificacion : Form
    {

        private List<MimeMessage> correos; // Declara la variable correos

        public Notificacion(List<MimeMessage> correos)
        {
            InitializeComponent();
            InicializarDataGridView();
            this.correos = correos; // Asigna la lista de correos a la variable de clase
            MostrarCorreos(correos);
        }
        private void InicializarDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Add("De", "De");
            dataGridView1.Columns.Add("Asunto", "Asunto");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.CellDoubleClick += DataGridViewCorreos_CellDoubleClick;
        }

        private void MostrarCorreos(List<MimeMessage> correos)
        {
            dataGridView1.Rows.Clear();
            foreach (var correo in correos)
            {
                dataGridView1.Rows.Add(correo.From.ToString(), correo.Subject, correo.Date.ToString());
            }
        }
        public void ActualizarCorreos(List<MimeMessage> correos)
        {
            this.correos = correos; // Actualiza la lista de correos
            MostrarCorreos(correos);
        }

        private void DataGridViewCorreos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var de = dataGridView1.Rows[e.RowIndex].Cells["De"].Value.ToString();
                var asunto = dataGridView1.Rows[e.RowIndex].Cells["Asunto"].Value.ToString();
                var fecha = dataGridView1.Rows[e.RowIndex].Cells["Fecha"].Value.ToString();

                // Obtener el objeto MimeMessage correspondiente al correo
                var correo = correos.FirstOrDefault(c => c.From.ToString() == de && c.Subject == asunto && c.Date.ToString() == fecha);
                if (correo != null)
                {
                    // Abrir el archivo PDF adjunto directamente
                    AbrirArchivoAdjunto(correo);

                    // Marcar el correo como leído
                    MarcarComoLeido(correo);
                }
            }
        }

        private void AbrirArchivoAdjunto(MimeMessage correo)
        {
            foreach (var attachment in correo.Attachments)
            {
                if (attachment is MimePart part)
                {
                    var tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), part.FileName);

                    using (var stream = System.IO.File.Create(tempFilePath))
                    {
                        part.Content.DecodeTo(stream);
                    }

                    // Abre el archivo PDF en el visor predeterminado del sistema
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = tempFilePath,
                        UseShellExecute = true
                    });

                    break; // Abre solo el primer archivo adjunto PDF
                }
            }
        }

        private void MarcarComoLeido(MimeMessage correo)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap-mail.outlook.com", 993, true);
                    client.Authenticate("desmon12@outlook.es", "ZXCVBNM12*");

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);

                    // Construye la consulta para buscar el correo específico por MessageId
                    var query = SearchQuery.HeaderContains("Message-ID", correo.MessageId);

                    var uids = inbox.Search(query);

                    foreach (var uid in uids)
                    {
                        inbox.AddFlags(uid, MessageFlags.Seen, true);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar el correo como leído: " + ex.Message);
            }
        }
    }
}
