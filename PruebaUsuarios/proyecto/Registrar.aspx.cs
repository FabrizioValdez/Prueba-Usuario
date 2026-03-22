using System;
using System.Web.UI;
using PruebaUsuarios.proyecto.App_Code;

namespace PruebaUsuarios.proyecto
{
    public partial class Registrar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlError.Visible = false;
            pnlSuccess.Visible = false;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string username = txtUsername.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;
                string telefono = txtTelefono.Text.Trim();
                DateTime? fechaNacimiento = null;

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                {
                    fechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                }

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || 
                    string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || 
                    string.IsNullOrEmpty(password))
                {
                    MostrarError("Todos los campos obligatorios deben ser completados.");
                    return;
                }

                if (password.Length < 6)
                {
                    MostrarError("La contrasena debe tener al menos 6 caracteres.");
                    return;
                }

                if (password != confirmPassword)
                {
                    MostrarError("Las contrasenas no coinciden.");
                    return;
                }

                int nuevoId = UsuarioDB.CrearUsuario(username, password, email, nombre, apellido, telefono, fechaNacimiento);

                if (nuevoId > 0)
                {
                    pnlSuccess.Visible = true;
                    LimpiarCampos();
                }
                else
                {
                    MostrarError("No se pudo crear el usuario. El nombre de usuario o correo ya existe.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al registrar: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            pnlError.Visible = true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtTelefono.Text = "";
            txtFechaNacimiento.Text = "";
        }
    }
}
