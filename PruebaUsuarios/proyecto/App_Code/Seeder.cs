using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaUsuarios.proyecto.App_Code
{
    public class Seeder
    {
        public static void CrearUsuarioPruebaSiNoExiste()
        {
            try
            {
                int totalUsuarios = UsuarioDB.ObtenerCantidadUsuarios();

                if (totalUsuarios == 0)
                {
                    UsuarioDB.CrearUsuario(
                        username: "admin",
                        password: "Admin123",
                        email: "admin@test.com",
                        nombre: "Usuario",
                        apellido: "Prueba",
                        telefono: "+1234567890",
                        fechaNacimiento: new DateTime(1990, 1, 15)
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error en Seeder: " + ex.Message);
            }
        }
    }
}