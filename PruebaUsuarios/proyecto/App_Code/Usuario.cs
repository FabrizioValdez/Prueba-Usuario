using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaUsuarios.proyecto.App_Code
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? UltimoBloqueo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string Iniciales
        {
            get
            {
                if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Apellido))
                    return (Nombre[0].ToString() + Apellido[0].ToString()).ToUpper();
                if (!string.IsNullOrEmpty(Username))
                    return Username.Substring(0, Math.Min(2, Username.Length)).ToUpper();
                return "??";
            }
        }
    }
}