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
    public partial class VERIFICACION : Form
    {
        private readonly Dictionary<string, string> superUsuarios;

        public VERIFICACION(Dictionary<string, string> superUsuarios)
        {
            InitializeComponent();
            this.superUsuarios = superUsuarios;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            textBox1.PasswordChar = '*';
            textBox1.KeyPress += TxtPassword_KeyPress;
        }

        private void VERIFICACION_Load(object sender, EventArgs e)
        {
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;

            if (superUsuarios.Values.Contains(password))
            {
                CREAR_USUARIO crearUsuarioForm = new CREAR_USUARIO(true);
                crearUsuarioForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña de superusuario inválida. Acceso denegado.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}