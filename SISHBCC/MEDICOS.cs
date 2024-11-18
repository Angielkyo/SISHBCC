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
    public partial class MEDICOS : Form
    {
        public MEDICOS()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FARMACIA fARMACIA = new FARMACIA();
            fARMACIA.Show();

            this.Close();
        }
    }
}
