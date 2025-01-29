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
    public partial class Config_Categoria : System.Web.UI.Page
    {
        CategoriaManager manager = new CategoriaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId_Categoria.Enabled = false;
            chequearUsuarios();


                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (!string.IsNullOrEmpty(id) && !IsPostBack)
                {
                    Categoria categoria = manager.ListarTodos().Find(x => x.Id_Categoria == int.Parse(id));

                    if (categoria != null)
                    {
                        txtId_Categoria.Text = categoria.Id_Categoria.ToString();
                        txtNombre.Text = categoria.Nombre;

                        if (!categoria.Estado)
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
            Categoria categoria = new Categoria();
            try
            {
                categoria.Nombre = txtNombre.Text.Trim();

                if (Request.QueryString["id"] != null)
                {
                    categoria.Id_Categoria = int.Parse(txtId_Categoria.Text);

                    manager.Modificar(categoria);
                    lblMensaje.Text = "Su mofificacion se realizó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/Categorias.aspx");

                }
                else
                {
                    manager.Agregar(categoria);
                    lblMensaje.Text = "Su usuario se agregó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;
                    Response.Redirect("~/Administrador/Categorias.aspx");
                }

                
            }
            catch (Exception ex)
            {

                Response.Write("Hubo un problema:" + ex);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Categorias.aspx");
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {

                CategoriaManager manager = new CategoriaManager();

                if (btnDeshabilitar.Text == "Reactivar")
                {
                    manager.Reactivar(int.Parse(txtId_Categoria.Text));
                    Response.Redirect("~/Administrador/Categorias.aspx");
                }
                else

                {
                    manager.Desactivar(int.Parse(txtId_Categoria.Text));
                }
                Response.Redirect("~/Administrador/Categorias.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);

                throw;
            }
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