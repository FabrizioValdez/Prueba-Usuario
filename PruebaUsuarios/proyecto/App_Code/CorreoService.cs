using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace PruebaUsuarios.proyecto.App_Code
{
    public class CorreoService
    {
        public static bool EnviarCorreoSincrono(string emailDestino, string nombreUsuario)
        {
            try
            {
                // Configuración desde Web.config
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];
                string smtpFrom = ConfigurationManager.AppSettings["SmtpFrom"];

                using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);

                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(smtpFrom, "Sistema de Seguridad"),
                        Subject = "⚠️ Su cuenta ha sido bloqueada temporalmente",
                        Body = $@"
                            <html>
                            <body style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                                <div style='background-color: #f8d7da; border: 1px solid #f5c6cb; border-radius: 8px; padding: 20px;'>
                                    <h2 style='color: #721c24; margin-top: 0;'>🔒 Cuenta Bloqueada Temporalmente</h2>
                                    <p>Hola <strong>{nombreUsuario}</strong>,</p>
                                    <p>Se ha detectado un patrón de inicio de sesión incorrecto en su cuenta.</p>
                                    <p>Su cuenta ha sido <strong>bloqueada temporalmente por 15 minutos</strong> como medida de seguridad.</p>
                                    <p>Una vez transcurrido el tiempo de bloqueo, podrá intentar iniciar sesión nuevamente.</p>
                                </div>
                            </body>
                            </html>",
                        IsBodyHtml = true
                    };

                    mail.To.Add(emailDestino);

                    // Envío directo (detiene la ejecución hasta que termine)
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}