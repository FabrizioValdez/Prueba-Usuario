using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaUsuarios.proyecto.App_Code
{
    public class UsuarioDB
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UsuariosDB"].ConnectionString;
        }

        public static Usuario ObtenerPorUsername(string username)
        {
            Usuario usuario = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"SELECT Id, Username, PasswordHash, Email, Nombre, Apellido, 
                                Telefono, FechaNacimiento, Activo, Bloqueado, IntentosFallidos, 
                                UltimoBloqueo, FechaCreacion, UltimoAcceso 
                                FROM Usuarios WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = MapearUsuario(reader);
                        }
                    }
                }
            }

            return usuario;
        }

        public static bool ValidarCredenciales(string username, string password)
        {
            Usuario usuario = ObtenerPorUsername(username);

            if (usuario == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
        }

        public static void IncrementarIntentosFallidos(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"UPDATE Usuarios SET IntentosFallidos = IntentosFallidos + 1 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ReiniciarIntentosFallidos(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"UPDATE Usuarios SET IntentosFallidos = 0 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void BloquearCuenta(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"UPDATE Usuarios SET Bloqueado = 1, UltimoBloqueo = @FechaBloqueo, 
                                IntentosFallidos = 0 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    cmd.Parameters.AddWithValue("@FechaBloqueo", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DesbloquearCuenta(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"UPDATE Usuarios SET Bloqueado = 0, IntentosFallidos = 0 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarUltimoAcceso(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"UPDATE Usuarios SET UltimoAcceso = @Fecha, 
                                UltimoBloqueo = NULL WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool EstaBloqueada(int usuarioId)
        {
            bool bloqueado = false;
            DateTime? ultimoBloqueo = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"SELECT Bloqueado, UltimoBloqueo FROM Usuarios WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bloqueado = reader.GetBoolean(0);
                            if (!bloqueado) return false;

                            ultimoBloqueo = reader.IsDBNull(1) ? default(DateTime?) : reader.GetDateTime(1);
                        }
                    }
                }
            }

            if (ultimoBloqueo.HasValue)
            {
                TimeSpan tiempoBloqueado = DateTime.Now - ultimoBloqueo.Value;
                if (tiempoBloqueado.TotalMinutes >= 15)
                {
                    DesbloquearCuenta(usuarioId);
                    return false;
                }
            }

            return bloqueado;
        }

        public static int GetIntentosFallidos(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"SELECT IntentosFallidos FROM Usuarios WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuarioId);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int ObtenerCantidadUsuarios()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM Usuarios";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int CrearUsuario(string username, string password, string email,
            string nombre, string apellido, string telefono = null, DateTime? fechaNacimiento = null)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string query = @"INSERT INTO Usuarios (Username, PasswordHash, Email, Nombre, Apellido, 
                                Telefono, FechaNacimiento) VALUES (@Username, @Password, @Email, 
                                @Nombre, @Apellido, @Telefono, @FechaNacimiento);
                                SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", passwordHash);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", (object)fechaNacimiento ?? DBNull.Value);

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private static Usuario MapearUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? default(DateTime?) : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                Bloqueado = reader.GetBoolean(reader.GetOrdinal("Bloqueado")),
                IntentosFallidos = reader.GetInt32(reader.GetOrdinal("IntentosFallidos")),
                UltimoBloqueo = reader.IsDBNull(reader.GetOrdinal("UltimoBloqueo")) ? default(DateTime?) : reader.GetDateTime(reader.GetOrdinal("UltimoBloqueo")),
                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                UltimoAcceso = reader.IsDBNull(reader.GetOrdinal("UltimoAcceso")) ? default(DateTime?) : reader.GetDateTime(reader.GetOrdinal("UltimoAcceso"))
            };
        }
    }
}