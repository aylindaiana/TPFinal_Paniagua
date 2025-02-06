using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua
{
    public partial class RecuperarPasswod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(correo))
            {
                lblMensaje.Text = "Por favor ingrese un correo electrónico.";
                lblMensaje.Visible = true;
                return;
            }

            UsuarioManager usuarioManager = new UsuarioManager();
            bool correoExiste = usuarioManager.VerificarEmail(correo);

            if (!correoExiste)
            {
                lblMensaje.Text = "Este correo no está registrado en nuestro sistema.";
                lblMensaje.Visible = true;
                return;
            }

            try
            {

                string viejaPass = usuarioManager.RecuperarPass(correo);
                string nuevaPass = GenerarNuevaContraseña();
                usuarioManager.CambiarContraseña(correo, nuevaPass);
                string cuerpoCorreo = $"Cambio de Contraseña. Su anterior contraseña era: {viejaPass}.";

                EmailService emailService = new EmailService();
                emailService.ArmarCorreoContraseña(correo, nuevaPass, cuerpoCorreo);
                emailService.enviarmail();

                lblMensaje.Text = "Hemos enviado un correo con las instrucciones para recuperar su contraseña.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al enviar el correo: {ex.Message}";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        private string GenerarNuevaContraseña()
        {
           
            return Guid.NewGuid().ToString().Substring(0, 8); 
        }
    }
}