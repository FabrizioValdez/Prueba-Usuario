using System;
using System.Linq;
using System.Web.UI;
using PruebaUsuarios.proyecto.App_Code;

namespace PruebaUsuarios.proyecto
{
    public partial class Inicio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Seeder.CrearUsuarioPruebaSiNoExiste();
                CargarClienteAleatorio();
            }
        }

        private void CargarClienteAleatorio()
        {
            try
            {
                Random random = new Random();
                int cantidadUsuarios = 0;

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["UsuariosDB"].ConnectionString))
                {
                    string queryCount = "SELECT COUNT(*) FROM Usuarios WHERE Activo = 1";
                    using (System.Data.SqlClient.SqlCommand cmdCount = new System.Data.SqlClient.SqlCommand(queryCount, conn))
                    {
                        conn.Open();
                        cantidadUsuarios = Convert.ToInt32(cmdCount.ExecuteScalar());
                    }

                    if (cantidadUsuarios > 0)
                    {
                        int indiceAleatorio = random.Next(1, cantidadUsuarios + 1);
                        
                        string queryUsuario = @"SELECT TOP 1 Nombre, Apellido FROM (
                            SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Nombre, Apellido 
                            FROM Usuarios WHERE Activo = 1
                        ) AS numbered WHERE RowNum = @Indice";
                        
                        using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(queryUsuario, conn))
                        {
                            cmd.Parameters.AddWithValue("@Indice", indiceAleatorio);
                            using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string nombre = reader["Nombre"].ToString();
                                    string apellido = reader["Apellido"].ToString();
                                    lblNombreCliente.Text = $"{nombre} {apellido}";
                                }
                                else
                                {
                                    lblNombreCliente.Text = "Cliente";
                                }
                            }
                        }
                    }
                    else
                    {
                        lblNombreCliente.Text = "Cliente";
                    }
                }
            }
            catch
            {
                lblNombreCliente.Text = "Cliente";
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
