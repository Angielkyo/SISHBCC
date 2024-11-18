using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISHBCC
{
    public partial class PRODUCTOS_CADUCADOS : Form
    {
        public PRODUCTOS_CADUCADOS()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ejecuta el método cuando se hace clic en la opción de Imprimir.
            // Crear la carpeta si no existe
            string rutaCarpeta = Path.Combine(Environment.CurrentDirectory);
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                MessageBox.Show("Carpeta creada correctamente en: " + rutaCarpeta);
            }
            // Define el nombre del archivo PDF y la ruta completa
            string nombreArchivo = "datos.pdf";
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 10f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
            doc.Open();

            // Agrega imágenes al documento  para el formato
            string rutaImagen = "C:\\Users\\natar\\Downloads\\logoSALUD.png";
            if (File.Exists(rutaImagen))
            {

                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                imagen.ScaleAbsolute(doc.PageSize.Width, (doc.PageSize.Width / imagen.Width) * imagen.Height);
                imagen.Alignment = Element.ALIGN_CENTER;
                doc.Add(imagen);
            }
            // Crea una tabla PDF y configura según las necesidades especificadas
            PdfPTable tablaPDF = new PdfPTable(dataGridView1.Columns.Count);
            tablaPDF.DefaultCell.Padding = 3;
            tablaPDF.WidthPercentage = 100;
            tablaPDF.HorizontalAlignment = Element.ALIGN_LEFT;
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                tablaPDF.AddCell(celda);
            }
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (celda.Value != null)
                    {
                        tablaPDF.AddCell(celda.Value.ToString());
                    }
                    else
                    {
                        tablaPDF.AddCell("");
                    }
                }
            }


            doc.Add(tablaPDF);
            doc.Close();

            // Abre el PDF con el visor predeterminado
            Process.Start("C:\\Program Files\\Adobe\\Acrobat DC\\Acrobat\\Acrobat.exe", rutaCompleta);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FARMACIA fARMACIA = new FARMACIA();
            fARMACIA.Show();

            this.Close();
        }
    }
}
