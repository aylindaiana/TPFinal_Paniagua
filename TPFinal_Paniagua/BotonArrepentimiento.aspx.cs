using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manager;

namespace TPFinal_Paniagua
{
    public partial class BotonArrepentimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailUsuario"] != null)
                {
                    txtEmail.Text = Session["EmailUsuario"].ToString();
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string email = txtEmail.Text;
            string numeroOrden = txtNumeroOrden.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(numeroOrden))
            {
                return;
            }

            try
            {

                EmailService emailService = new EmailService();
                emailService.ArmarCorreoContraseña(email, nombre, numeroOrden);
                emailService.enviarmail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}