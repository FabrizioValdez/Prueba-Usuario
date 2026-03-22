using System;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using PruebaUsuarios.proyecto.App_Code;


namespace PruebaUsuarios.proyecto
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.EstaAutenticado(Session))
            {
                Response.Redirect("Inicio.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            int usuarioId = SessionManager.GetUsuarioId(Session);
            Usuario usuario = UsuarioDB.ObtenerPorUsername(SessionManager.GetUsername(Session));

            if (usuario != null)
            {
                lblNombreCompleto.Text = usuario.NombreCompleto;
                lblIniciales.Text = usuario.Iniciales;
                lblNombre.Text = usuario.Nombre;
                lblApellido.Text = usuario.Apellido;
                lblUsername.Text = usuario.Username;
                lblEmail.Text = usuario.Email;
                lblTelefono.Text = string.IsNullOrEmpty(usuario.Telefono) ? "No especificado" : usuario.Telefono;
                lblFechaNacimiento.Text = usuario.FechaNacimiento?.ToString("dd/MM/yyyy") ?? "No especificada";
                lblUltimoAcceso.Text = usuario.UltimoAcceso?.ToString("dd/MM/yyyy HH:mm") ?? "Primer acceso";
                lblNombreUsuario.Text = usuario.NombreCompleto;
            }
        }

        [WebMethod]
        public static bool ExtenderSesion()
        {
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    SessionManager.ExtenderSesion(HttpContext.Current.Session);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SessionManager.DestruirSesion(Session);
            Response.Redirect("Login.aspx");
        }
    }
}