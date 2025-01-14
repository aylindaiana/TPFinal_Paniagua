using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Cofig_Usuarios : System.Web.UI.Page
    {
        UsuarioManager manager = new UsuarioManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId_Usuario.Enabled = false;

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
                    txtClave.Text = usuario.Pass;
                    txtCorreo.Text = usuario.Email;
                    txtTelefono.Text = usuario.Telefono;
                    txtDireccion.Text = usuario.Direccion;
                    txtLocalidad.Text = usuario.Localidad;
                    txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString();
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

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {

        }

    }
}