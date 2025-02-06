using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua
{
    public partial class Registro : System.Web.UI.Page
    {
        CarritoManager carritoManager = new CarritoManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string errores = ValidarFormulario();

            if (!string.IsNullOrEmpty(errores))
            {
                lblMensaje.Text = errores;
                lblMensaje.Visible = true;
                return;
            }
            UsuarioManager manager = new UsuarioManager();
            
            Usuario usuario = new Usuario();

            try
            {
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                usuario.Direccion = txtDireccion.Text;
                usuario.Localidad = txtLocalidad.Text;
                usuario.Telefono = txtTelefono.Text;
                usuario.IdAcceso = 3;

                manager.Agregar(usuario);

                if (manager.Login(usuario))
                {
                    Session["usuario"] = usuario;
                    Session["AccesoId"] = usuario.IdAcceso;

                    lblMensaje.Text = "Te registraste exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    carritoManager.AgregarCarrito(usuario.Id_Usuario);

                    Response.Redirect("Inicio.aspx");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Funciones:
        private string ValidarFormulario()
        {
            StringBuilder errores = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errores.AppendLine("El nombre es obligatorio.<br/>");
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errores.AppendLine("El apellido es obligatorio.<br/>");
            }
            if (string.IsNullOrWhiteSpace(txtFechaNacimiento.Text) ||
                !DateTime.TryParse(txtFechaNacimiento.Text, out DateTime FechaNacimiento) ||
                FechaNacimiento > DateTime.Now)
            {
                errores.AppendLine("La fecha de nacimiento no es válida o es una fecha futura.<br/>");
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                errores.AppendLine("El email no tiene un formato válido.<br/>");
            }
            if(string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errores.AppendLine("Debe agregar una contraseña.<br/>");
            }

            if (txtPassword.Text != txtConfirmacionPassword.Text)
            {
                errores.AppendLine("La contraseña y la confirmación deben ser iguales. <br/>");
            }

            if (!long.TryParse(txtTelefono.Text, out _))
            {
                errores.AppendLine("El teléfono solo debe contener números y debe ser válido. <br/>");
            }

            return errores.ToString();

        }
    }
}