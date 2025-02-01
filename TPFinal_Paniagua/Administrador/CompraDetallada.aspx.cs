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
    public partial class CompraDetallada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack) 
            {
                DetalleManager manager = new DetalleManager();
                Session.Add("listaCompras", manager.ListarTodos());
                dgvCompras.DataSource = Session["listaCompras"];
                dgvCompras.DataBind();
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Navegacion.aspx");
        }
        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

            List<DetalleCompra> listar = (List<DetalleCompra>)Session["listaCompras"];
            List<DetalleCompra> buscarlist = listar.FindAll(x => x.NombreUsuario.ToUpper().Contains(txtBuscar.Text.ToUpper()) || x.ApellidoUsuario.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvCompras.DataSource = buscarlist;
            dgvCompras.DataBind();
            txtBuscar.Text = string.Empty;
        }


        protected void dgvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                try
                {
                    int idDetalleCompra = Convert.ToInt32(e.CommandArgument);

                    DetalleManager compraManager = new DetalleManager();
                    bool actualizado = compraManager.CambiarEstadoCompraCiclo(idDetalleCompra);

                    if (actualizado)
                    {
                        lblMensaje.Text = "Estado de compra actualizado correctamente.";
                        lblMensaje.CssClass = "text-success";
                    }
                    else
                    {
                        lblMensaje.Text = "No se pudo actualizar el estado.";
                        lblMensaje.CssClass = "text-danger";
                    }

                    lblMensaje.Visible = true;

                    CargarCompra();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cambiar el estado: " + ex.Message;
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                }
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
        public void CargarCompra()
        {
            DetalleManager detalle = new DetalleManager();
            List<DetalleCompra> compra = new List<DetalleCompra>();

            compra = detalle.ListarTodos();
            dgvCompras.DataSource = compra;
            dgvCompras.DataBind();
        }
    }
}