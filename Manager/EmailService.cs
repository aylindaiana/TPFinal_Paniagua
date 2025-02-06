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
        public void ArmarCorreoContraseña(string emailDestino, string nuevaContraseña, string asunto)
        {
            if (string.IsNullOrEmpty(emailDestino))
            {
                throw new Exception("El correo electrónico del destinatario no es válido.");
            }

            string cuerpo = "<html><body>" +
                            "<p>Estimado usuario,</p>" +
                            "<p>Le informamos que su contraseña ha sido restablecida con éxito. Su nueva contraseña es:</p>" +
                            "<h2 style='color: #ff7f50;'>" + nuevaContraseña + "</h2>" +
                            "<p>Para mayor seguridad, le recomendamos cambiar esta contraseña después de iniciar sesión.</p>" +
                            "<p>Si no ha solicitado este cambio, por favor contacte con nuestro soporte inmediatamente.</p>" +
                            "<p>Saludos,<br>El equipo de soporte de Rose Vibes.</p>" +
                            "</body></html>";

            email = new MailMessage();
            email.From = new MailAddress("dai83r2@gmail.com"); // Cambia este correo por uno adecuado
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
