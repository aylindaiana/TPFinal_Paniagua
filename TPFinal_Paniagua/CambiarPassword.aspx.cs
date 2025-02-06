using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            string passwordActual = txtPasswordActual.Text.Trim();
            string nuevaPassword = txtNuevaPassword.Text.Trim();
            string confirmarPassword = txtConfirmarPassword.Text.Trim();

            if (string.IsNullOrEmpty(passwordActual) || string.IsNullOrEmpty(nuevaPassword) || string.IsNullOrEmpty(confirmarPassword))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            if (nuevaPassword != confirmarPassword)
            {
                lblMensaje.Text = "La nueva contraseña y su confirmación no coinciden.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            UsuarioManager manager = new UsuarioManager();

            Usuario usuarioAutenticado = Session["usuario"] as Usuario;

            if (usuarioAutenticado == null)
            {
                lblMensaje.Text = "El usuario no está autenticado.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            Usuario usuario = manager.ObtenerUsuarioPorId(usuarioAutenticado.Id_Usuario);

            if (usuario == null || usuario.Pass != passwordActual)
            {
                lblMensaje.Text = "La contraseña actual no es correcta.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            usuario.Pass = nuevaPassword;

            try
            {
                manager.Modificar(usuario);
                lblMensaje.Text = "La contraseña se ha cambiado exitosamente.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cambiar la contraseña. Intenta nuevamente.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}