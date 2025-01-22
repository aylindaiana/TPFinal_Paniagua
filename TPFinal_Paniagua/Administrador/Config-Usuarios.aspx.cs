using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Config_Usuarios : System.Web.UI.Page
    {
        UsuarioManager manager = new UsuarioManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId_Usuario.Enabled = false;
            chequearUsuarios();
            if (!IsPostBack)
            {
                AccesoManager manager = new AccesoManager();
                List<Acceso> list = manager.Listar();
                ddlAcceso.DataSource = list;
                ddlAcceso.DataTextField = "Nombre";
                ddlAcceso.DataValueField = "Id_Acceso";
                ddlAcceso.DataBind();
            }

            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            if (!string.IsNullOrEmpty(id) && !IsPostBack)
            {
                Usuario usuario = manager.Listar().Find(x => x.Id_Usuario == int.Parse(id));
                if (usuario != null)
                {

                    txtId_Usuario.Text = usuario.Id_Usuario.ToString();
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtEmail.Text = usuario.Email;
                    txtPassword.Text = usuario.Pass;
                    txtTelefono.Text = usuario.Telefono;
                    txtDireccion.Text = usuario.Direccion;
                    txtLocalidad.Text = usuario.Localidad;
                    txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                    ddlAcceso.SelectedValue = usuario.IdAcceso.ToString();

                    if (!usuario.Estado)
                    {
                        btnDeshabilitar.Text = "Reactivar";
                    }
                }

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string errores = ValidarFormulario();

            if (!string.IsNullOrEmpty(errores))
            {
                lblMensaje.Text = errores;
                lblMensaje.Visible = true;
                return;
            }
            Usuario usuario = new Usuario();

            try
            {
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                usuario.Telefono = txtTelefono.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Localidad = txtLocalidad.Text;
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                usuario.IdAcceso = int.Parse(ddlAcceso.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    usuario.Id_Usuario = int.Parse(txtId_Usuario.Text);
                    manager.Modificar(usuario);

                    lblMensaje.Text = "Su mofificacion se realizó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/UsuariosGeneral.aspx");
                }
                else
                {
                    manager.Agregar(usuario);

                    lblMensaje.Text = "Su usuario se agregó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/UsuariosGeneral.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/UsuariosGeneral.aspx");
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {

                UsuarioManager manager = new UsuarioManager();

                if (btnDeshabilitar.Text == "Reactivar")
                {
                    manager.Reactivar(int.Parse(txtId_Usuario.Text));
                    Response.Redirect("~/Administrador/UsuariosGeneral.aspx");
                }
                else

                {
                    manager.Desactivar(int.Parse(txtId_Usuario.Text));
                }



                Response.Redirect("~/Administrador/UsuariosGeneral.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);

                throw;
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
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errores.AppendLine("Debe agregar una contraseña.<br/>");
            }

            if (!long.TryParse(txtTelefono.Text, out _))
            {
                errores.AppendLine("El teléfono solo debe contener números y debe ser válido. <br/>");
            }

            return errores.ToString();

        }

        public void chequearUsuarios()
        {
            if (Session["AccesoId"] == null)
            {
                Response.Redirect("/Ingreso.aspx");
            }
            else
            {
                int idAcceso = Convert.ToInt32(Session["AccesoId"]);

                if (idAcceso != 1 && idAcceso != 2)
                {
                    Response.Redirect("/Error.aspx");
                    return;
                }
            }
        }
    }
}