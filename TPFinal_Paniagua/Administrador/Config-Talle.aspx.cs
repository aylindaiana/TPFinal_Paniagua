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
    public partial class Config_Talle : System.Web.UI.Page
    {
        TalleManager manager = new TalleManager();
        ArticuloManager articuloManager = new ArticuloManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId_Talle.Enabled = false;
            chequearUsuarios();

            List<Articulo> articulos = articuloManager.ListarArticulosActivos(); // Obtener lista de artículos
            chkArticulos.DataSource = articulos;
            chkArticulos.DataTextField = "Nombre"; // Suponiendo que tienes un campo Nombre en Articulo
            chkArticulos.DataValueField = "Id_Articulo"; // Asignar Id del artículo
            chkArticulos.DataBind();

            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            if (!string.IsNullOrEmpty(id) && !IsPostBack)
            {
                Talles talle = manager.ListarTodos().Find(x => x.Id_Talle == int.Parse(id));

                if (talle != null)
                {
                    txtId_Talle.Text = talle.Id_Talle.ToString();
                    txtNombre.Text = talle.Nombre;

                    if (!talle.Estado)
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
            Talles talle = new Talles();
            try
            {
                talle.Nombre = txtNombre.Text.Trim();

                if (Request.QueryString["id"] != null)
                {
                    talle.Id_Talle = int.Parse(txtId_Talle.Text);

                    manager.Modificar(talle);
                    lblMensaje.Text = "Su mofificacion se realizó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    Response.Redirect("~/Administrador/Talle.aspx");

                }
                else
                {
                    manager.Agregar(talle);
                    List<int> idArticulosSeleccionados = new List<int>();
                    foreach (ListItem item in chkArticulos.Items)
                    {
                        if (item.Selected)
                        {
                            idArticulosSeleccionados.Add(int.Parse(item.Value)); // Obtener los Ids de los artículos seleccionados
                        }
                    }
                    foreach (int idArticulo in idArticulosSeleccionados)
                    {
                        manager.AsociarStockArticuloTalle(idArticulo, talle.Id_Talle, 0);
                    }
                    lblMensaje.Text = "Su Talle se agregó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;
                    Response.Redirect("~/Administrador/Talle.aspx");
                }


            }
            catch (Exception ex)
            {

                Response.Write("Hubo un problema:" + ex);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Talle.aspx");
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {

                TalleManager manager = new TalleManager();

                if (btnDeshabilitar.Text == "Reactivar")
                {
                    manager.Reactivar(int.Parse(txtId_Talle.Text));
                    Response.Redirect("~/Administrador/Talle.aspx");
                }
                else

                {
                    manager.Desactivar(int.Parse(txtId_Talle.Text));
                }
                Response.Redirect("~/Administrador/Talle.aspx");

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