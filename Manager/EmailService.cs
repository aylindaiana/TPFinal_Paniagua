using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("sofireynoso555@gmail.com", "cfsb ahbp rgfa cvcn");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }
        public void ArmarCorreo(string emailDestino)
        {
            if (string.IsNullOrEmpty(emailDestino))
            {
                throw new Exception("El correo electrónico del destinatario no es válido.");
            }

            string asunto = "Notificación de Cambio de Contraseña Exitoso";
            string cuerpo = "<p>Estimado usuario,</p>" +
                            "<p>Le informamos que su contraseña ha sido cambiada exitosamente. Si no fue usted, por favor contacte con soporte en la brevedad.</p>" +
                            "<p>Saludos,<br>El equipo de soporte de Rose Pink.</p>";

            email = new MailMessage();

            email.From = new MailAddress("dai83r2@gmail.com");
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;

            email.To.Add(emailDestino);
        }

        public void enviarmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
