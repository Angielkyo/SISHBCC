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
    public partial class VERSION : Form
    {
        public VERSION()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botón para cerrar el formulario VERSION y mostrar el formulario Login
            Login LOGIN = new Login();
            LOGIN.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}