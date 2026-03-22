using PruebaUsuarios.proyecto.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PruebaUsuarios.proyecto
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();

                if (Request.QueryString["expired"] == "1")
                {
                    pnlSesionExpirada.Visible = true;
                }

                if (SessionManager.EstaAutenticado(Session))
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }

            pnlMensaje.Visible = false;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtContrasena.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MostrarMensaje("Por favor, complete todos los campos.", "danger");
                return;
            }

            Usuario usuario = UsuarioDB.ObtenerPorUsername(username);

            if (usuario == null)
            {
                MostrarMensaje("El usuario no existe.", "danger");
                return;
            }

            if (UsuarioDB.EstaBloqueada(usuario.Id))
            {
                usuario = UsuarioDB.ObtenerPorUsername(username);

                // Calculamos los minutos restantes (usamos Double para precisión antes de redondear)
                TimeSpan diferencia = DateTime.Now - usuario.UltimoBloqueo.Value;
                double minutosQueFaltan = 15 - diferencia.TotalMinutes;

                // Redondeamos hacia arriba (ejemplo: 14.2 se convierte en 15)
                int minutosAMostrar = (int)Math.Ceiling(minutosQueFaltan);

                // Evitamos que muestre números negativos si el tiempo ya casi expira
                if (minutosAMostrar < 1) minutosAMostrar = 1;

                lblTiempoBloqueo.Text = $"Tiempo restante: {minutosAMostrar} minutos";

                pnlBloqueo.Visible = true;
                txtUsuario.Enabled = false;
                txtContrasena.Enabled = false;
                btnIngresar.Enabled = false;
                return;
            }

            if (!usuario.Activo)
            {
                MostrarMensaje("La cuenta está desactivada. Contacte al soporte.", "danger");
                return;
            }

            if (UsuarioDB.ValidarCredenciales(username, password))
            {
                if (usuario.Bloqueado)
                {
                    UsuarioDB.DesbloquearCuenta(usuario.Id);
                }

                UsuarioDB.ReiniciarIntentosFallidos(usuario.Id);
                UsuarioDB.ActualizarUltimoAcceso(usuario.Id);

                SessionManager.CrearSesion(Session, usuario.Id, usuario.Username,
                    usuario.Nombre, usuario.Apellido, usuario.Email);

                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                int intentosActuales = UsuarioDB.GetIntentosFallidos(usuario.Id) + 1;
                UsuarioDB.IncrementarIntentosFallidos(usuario.Id);

                if (intentosActuales >= 5)
                {
                    UsuarioDB.BloquearCuenta(usuario.Id);

                    Task.Run(() => CorreoService.EnviarCorreoSincrono(usuario.Email, usuario.Nombre));

                    MostrarMensaje("Cuenta bloqueada temporalmente. Se ha enviado una notificación a su correo.", "danger");
                    txtUsuario.Enabled = false;
                    txtContrasena.Enabled = false;
                    btnIngresar.Enabled = false;
                }
                else
                {
                    int intentosRestantes = 5 - intentosActuales;
                    MostrarMensaje($"Credenciales incorrectas. Intentos restantes: {intentosRestantes}", "danger");
                }
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.CssClass = $"alert alert-{tipo}";
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }
    }
}