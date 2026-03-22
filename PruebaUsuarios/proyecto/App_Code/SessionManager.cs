using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PruebaUsuarios.proyecto.App_Code
{
    public class SessionManager
    {
        public const string KEY_USUARIO_ID = "UsuarioId";
        public const string KEY_USERNAME = "Username";
        public const string KEY_NOMBRE = "Nombre";
        public const string KEY_APELLIDO = "Apellido";
        public const string KEY_EMAIL = "Email";
        public const string KEY_ULTIMA_ACTIVIDAD = "UltimaActividad";
        public const string KEY_SESION_EXTENDIDA = "SesionExtendida";

        public static int TiempoInactividadMinutos = 20;
        public static int TiempoAdvertenciaSegundos = 60;

        public static void CrearSesion(HttpSessionState session, int usuarioId, string username,
            string nombre, string apellido, string email)
        {
            session[KEY_USUARIO_ID] = usuarioId;
            session[KEY_USERNAME] = username;
            session[KEY_NOMBRE] = nombre;
            session[KEY_APELLIDO] = apellido;
            session[KEY_EMAIL] = email;
            session[KEY_ULTIMA_ACTIVIDAD] = DateTime.Now;
            session[KEY_SESION_EXTENDIDA] = false;
        }

        public static void ActualizarActividad(HttpSessionState session)
        {
            session[KEY_ULTIMA_ACTIVIDAD] = DateTime.Now;
            session[KEY_SESION_EXTENDIDA] = false;
        }

        public static void ExtenderSesion(HttpSessionState session)
        {
            session[KEY_ULTIMA_ACTIVIDAD] = DateTime.Now;
            session[KEY_SESION_EXTENDIDA] = true;
        }

        public static bool EstaAutenticado(HttpSessionState session)
        {
            return session[KEY_USUARIO_ID] != null;
        }

        public static int GetUsuarioId(HttpSessionState session)
        {
            if (session[KEY_USUARIO_ID] == null)
                return 0;
            return Convert.ToInt32(session[KEY_USUARIO_ID]);
        }

        public static string GetUsername(HttpSessionState session)
        {
            return session[KEY_USERNAME]?.ToString() ?? "";
        }

        public static string GetNombreCompleto(HttpSessionState session)
        {
            string nombre = session[KEY_NOMBRE]?.ToString() ?? "";
            string apellido = session[KEY_APELLIDO]?.ToString() ?? "";
            return $"{nombre} {apellido}".Trim();
        }

        public static TimeSpan GetTiempoInactividad(HttpSessionState session)
        {
            if (session[KEY_ULTIMA_ACTIVIDAD] == null)
                return TimeSpan.Zero;

            DateTime ultimaActividad = Convert.ToDateTime(session[KEY_ULTIMA_ACTIVIDAD]);
            return DateTime.Now - ultimaActividad;
        }

        public static int GetSegundosRestantesAdvertencia(HttpSessionState session)
        {
            if (session[KEY_ULTIMA_ACTIVIDAD] == null)
                return 0;

            TimeSpan inactividad = GetTiempoInactividad(session);
            int segundosInactivo = (int)inactividad.TotalSeconds;
            int tiempoTotalAdvertencia = TiempoInactividadMinutos * 60 + TiempoAdvertenciaSegundos;
            int segundosRestantes = Math.Max(0, tiempoTotalAdvertencia - segundosInactivo);

            int segundosAdvertencia = segundosRestantes - (TiempoInactividadMinutos * 60);
            return Math.Max(0, segundosAdvertencia);
        }

        public static bool DebeMostrarAdvertencia(HttpSessionState session)
        {
            if (session[KEY_ULTIMA_ACTIVIDAD] == null)
                return false;

            TimeSpan inactividad = GetTiempoInactividad(session);
            return inactividad.TotalMinutes >= TiempoInactividadMinutos;
        }

        public static void DestruirSesion(HttpSessionState session)
        {
            session.Abandon();
            session.Clear();
        }
    }
}