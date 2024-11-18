using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISHBCC
{
    public partial class Reportes : Form
    {
        private Dictionary<string, string> fileDictionary;
        private Process explorerProcess;

        public Reportes(Dictionary<string, string> files)
        {
            InitializeComponent();
            fileDictionary = files;
            StartPosition = FormStartPosition.CenterScreen;
            // Rellena el ListBox con los archivos
            listBox1.Items.Clear();
            foreach (var fileName in fileDictionary.Keys)
            {
                listBox1.Items.Add(fileName);
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
    }
}
