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
            chequearUsuarios();
            try
            {
                if (!IsPostBack)
                {
                    CategoriaManager categoria = new CategoriaManager();
                    List<Categoria> list = categoria.ListarTodos();
                    ddlCategoria.DataSource = list;
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Id_Categoria";
                    ddlCategoria.DataBind();
                }

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (!string.IsNullOrEmpty(id) && !IsPostBack)
                {
                    Tipo tipo = manager.ListarTodos().Find(x => x.Id_Tipo == int.Parse(id));

                    if (tipo != null)
                    {
                        txtId_Tipo.Text = tipo.Id_Tipo.ToString();
                        txtNombre.Text = tipo.Nombre;
                        ddlCategoria.SelectedValue = tipo.CategoriaId.ToString();

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
            Tipo tipo = new Tipo();
            try
            {
                tipo.Nombre = txtNombre.Text.Trim();
                tipo.CategoriaId = int.Parse(ddlCategoria.SelectedValue);

                Response.Write("<script>alert('ID Tipo: " + txtId_Tipo.Text + "\\nCategoría Seleccionada: " + tipo.CategoriaId + "');</script>");

                if (Request.QueryString["id"] != null)
                {
                    tipo.Id_Tipo = int.Parse(txtId_Tipo.Text);

                    manager.Modificar(tipo);
                    lblMensaje.Text = "Su mofificacion se realizó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/Tipos.aspx");

                }
                else
                {
                    manager.Agregar(tipo);

                    lblMensaje.Text = "Su usuario se agregó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/Tipos.aspx");
                }
            }
            catch (Exception ex)
            {

                Response.Write("Hubo un problema:" + ex);

            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Tipos.aspx");
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoManager manager = new TipoManager();    

                if (btnDeshabilitar.Text == "Reactivar")
                {
                    manager.Reactivar(int.Parse(txtId_Tipo.Text));
                    Response.Redirect("~/Administrador/Tipos.aspx");
                }
                else

                {
                    manager.Desactivar(int.Parse(txtId_Tipo.Text));
                }
                Response.Redirect("~/Administrador/Tipos.aspx");

            }
            catch (Exception ex)
            {
                Response.Write("Hubo un problema:" + ex);
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            int categoriaId = int.Parse(ddlCategoria.SelectedValue);

            TipoManager tipoManager = new TipoManager();
            List<Tipo> tipos = tipoManager.ObtenerTiposPorCategoria(categoriaId);

            ddlCategoria.DataSource = tipos;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id_Categoria";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0, new ListItem("Selecciona un tipo", "0"));
            */
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