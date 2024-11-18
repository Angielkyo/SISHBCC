using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;

namespace SISHBCC
{
    public partial class CREAR_USUARIO : Form
    {
        public CREAR_USUARIO(bool v)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            NOMBRES.KeyPress += TxtNombres_KeyPress;
            CORREO.KeyPress += TxtCorreo_KeyPress;
            CONTRASEÑA.KeyPress += TxtContraseña_KeyPress;
            CURP.KeyPress += TxtCurp_KeyPress;
            CONFIRMAR.KeyPress += TxtConfirmarContraseña_KeyPress;
            CONTRASEÑA.UseSystemPasswordChar = true;
            CONFIRMAR.UseSystemPasswordChar = true;
        }

        private void TxtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CORREO.Focus();
                e.Handled = true;
            }
        }

        private void TxtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CURP.Focus();
                e.Handled = true;
            }
        }

        private void TxtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CONFIRMAR.Focus();
                e.Handled = true;
            }
        }

        private void TxtCurp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CONTRASEÑA.Focus();
                e.Handled = true;
            }
        }

        private void TxtConfirmarContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                REGISTRARSE.PerformClick();
                e.Handled = true;
            }
        }

        private async void REGRESAR_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            Login login = new Login();
            login.Show();

            this.Close();

            progressBar1.Visible = false;
        }

        private void UpdateProgressBar(int progress)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new MethodInvoker(delegate { UpdateProgressBar(progress); }));
            }
            else
            {
                progressBar1.Value = progress;
            }
        }

        private bool InsertarUsuarioEnBaseDeDatos(string Nombre, string Correo, string Contraseña, string ConfirmarContraseña, string Curp, string APELLIDO_P, string APELLIDO_M, string TIPO, string NUMERO_T, string CEDULA)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string consulta = "INSERT INTO Usuarios (Nombre, Apellido_P, Apellido_M, Curp, Cedula, Numero_T, Correo, Tipo, Contraseña, ConfirmarContraseña) VALUES (@Nombre, @Apellido_P, @Apellido_M, @Curp, @Cedula, @Numero_T, @Correo, @Tipo, @Contraseña, @ConfirmarContraseña)";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", Nombre);
                    comando.Parameters.AddWithValue("@Apellido_P", APELLIDO_P);
                    comando.Parameters.AddWithValue("@Apellido_M", APELLIDO_M);
                    comando.Parameters.AddWithValue("@Curp", Curp);
                    comando.Parameters.AddWithValue("@Cedula", CEDULA);
                    comando.Parameters.AddWithValue("@Numero_T", NUMERO_T);
                    comando.Parameters.AddWithValue("@Correo", Correo);
                    comando.Parameters.AddWithValue("@Tipo", TIPO);
                    comando.Parameters.AddWithValue("@Contraseña", Contraseña);
                    comando.Parameters.AddWithValue("@ConfirmarContraseña", ConfirmarContraseña);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Error de SQL: " + sqlEx.Message);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error general: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        private async void REGISTRARSE_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            await Task.Run(() =>
            {
                string tipo = TIPO.Text.ToUpper(); // Convertir a mayúsculas para evitar problemas con la validación del tipo
                bool registroExitoso = false;

                // Validar la longitud de la contraseña
                if (CONTRASEÑA.Text.Length < 8)
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                    return;
                }

                // Validar el valor del tipo
                if (tipo == "ALMACEN" || tipo == "FARMACIA" || tipo == "RECETARIO")
                {
                    registroExitoso = InsertarUsuarioEnBaseDeDatos(NOMBRES.Text, CORREO.Text, CONTRASEÑA.Text, CONFIRMAR.Text, CURP.Text, APELLIDO_P.Text, APELLIDO_M.Text, tipo, NUMERO.Text, CEDULA.Text);
                }
                else
                {
                    MessageBox.Show("El valor del tipo no es válido. Debe ser 'ALMACEN', 'FARMACIA' o 'RECETARIO'.");
                }

                for (int i = 0; i <= 30; i++)
                {
                    System.Threading.Thread.Sleep(20);
                    UpdateProgressBar(i);

                    if (i == 30)
                    {
                        if (registroExitoso)
                        {
                            MessageBox.Show("Registro exitoso.");
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar el usuario. Por favor, inténtelo de nuevo.");
                        }
                    }
                }
            });

            progressBar1.Visible = false;
        }
    }
}
