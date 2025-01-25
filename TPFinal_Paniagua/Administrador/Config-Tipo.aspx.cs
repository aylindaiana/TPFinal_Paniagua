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
    public partial class Config_Tipo : System.Web.UI.Page
    {
        TipoManager manager = new TipoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId_Tipo.Enabled = false;
            //chequearUsuarios()
            try
            {

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (!string.IsNullOrEmpty(id) && !IsPostBack)
                {
                    Tipo tipo = manager.ListarTodos().Find(x => x.Id_Tipo == int.Parse(id));

                    if (tipo != null)
                    {
                        txtId_Tipo.Text = tipo.Id_Tipo.ToString();
                        txtNombre.Text = tipo.Nombre;

                        if (!tipo.Estado)
                        {
                            btnDeshabilitar.Text = "Reactivar";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Hubo un problema:" + ex);
                Response.Redirect("/Error.aspx");
            }
;        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string errores = ValidarFormulario();

            if (!string.IsNullOrEmpty(errores))
            {
                lblMensaje.Text = errores;
                lblMensaje.Visible = true;
                return;
            }
            Categoria categoria = new Categoria();
            try
            {

            }
            catch (Exception ex)
            {

                Response.Write("Hubo un problema:" + ex);
                Response.Redirect("/Error.aspx");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Categorias.aspx");
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {

        }

        //Funciones:
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
        private string ValidarFormulario()
        {
            StringBuilder errores = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errores.AppendLine("El nombre es obligatorio.<br/>");
            }
            return errores.ToString();

        }
    }
}