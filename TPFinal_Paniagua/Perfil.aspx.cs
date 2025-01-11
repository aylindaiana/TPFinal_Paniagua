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
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Ingreso.aspx");
                }

                Precargar();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string errores = ValidarFormulario();

                if (!string.IsNullOrEmpty(errores))
                {
                    lblMensaje.Text = errores;
                    lblMensaje.Visible = true;
                    return;
                }
                Usuario usuarioActual = (Usuario)Session["usuario"];

                UsuarioManager manager = new UsuarioManager();
                Usuario actualizar = new Usuario();

                actualizar.Id_Usuario = usuarioActual.Id_Usuario;
                actualizar.Nombre = txtNombre.Text.Trim();
                actualizar.Apellido = txtApellido.Text.Trim();
                actualizar.Email = txtEmail.Text.Trim();
                actualizar.Pass = txtPassword.Text.Trim();
                actualizar.Direccion = txtDireccion.Text.Trim();
                actualizar.Localidad = txtLocalidad.Text.Trim();
                actualizar.Telefono = txtTelefono.Text.Trim();
                actualizar.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                actualizar.IdAcceso = usuarioActual.IdAcceso;

                manager.Modificar(actualizar);

                Session["usuario"] = actualizar;

                lblMensaje.Text = "Se actualizó el usuario exitosamente.";
                lblMensaje.CssClass = "text-success";

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al actualizar el usuario.";
                lblMensaje.CssClass = "text-danger";

            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
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
                FechaNacimiento > DateTime.Now || FechaNacimiento < new DateTime(1900, 1, 1))
            {
                errores.AppendLine("La fecha de nacimiento no es válida. Debe ser una fecha real y no futura. <br/>");
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                errores.AppendLine("El email no tiene un formato válido.<br/>");
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
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

        private void Precargar()
        {
            Usuario usuarioActual = (Usuario)Session["usuario"];
            Usuario use = new Usuario();
            if (usuarioActual != null)
            {
                System.Diagnostics.Debug.WriteLine("Usuario en sesión:");
                System.Diagnostics.Debug.WriteLine($"Nombre: {usuarioActual.Nombre}");
                System.Diagnostics.Debug.WriteLine($"Apellido: {usuarioActual.Apellido}");
                System.Diagnostics.Debug.WriteLine($"Email: {usuarioActual.Email}");
                System.Diagnostics.Debug.WriteLine($"FechaNacimiento: {usuarioActual.FechaNacimiento}");
                System.Diagnostics.Debug.WriteLine($"Dirección: {usuarioActual.Direccion}");
                System.Diagnostics.Debug.WriteLine($"Localidad: {usuarioActual.Localidad}");
                System.Diagnostics.Debug.WriteLine($"Teléfono: {usuarioActual.Telefono}");

                use.Id_Usuario = usuarioActual.Id_Usuario;
                txtNombre.Text = use.Nombre ?? string.Empty;
                txtApellido.Text = use.Apellido ?? string.Empty;
                txtEmail.Text = usuarioActual.Email ?? string.Empty;
                txtDireccion.Text = usuarioActual.Direccion ?? string.Empty;
                txtPassword.Text = usuarioActual.Pass;
                txtLocalidad.Text = usuarioActual.Localidad ?? string.Empty;
                txtTelefono.Text = usuarioActual.Telefono ?? string.Empty;
                use.IdAcceso = usuarioActual.IdAcceso;

                if (usuarioActual.FechaNacimiento != DateTime.MinValue)
                {
                    txtFechaNacimiento.Text = usuarioActual.FechaNacimiento.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtFechaNacimiento.Text = string.Empty;
                }
            }
        }

    }
}