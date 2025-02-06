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
    public partial class Config_CompraDetallada : System.Web.UI.Page
    {
        private DetalleManager detalleManager = new DetalleManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompra();
            }
        }


        private void CargarCompra()
        {
            if (Request.QueryString["id"] != null)
            {
                int idCompra = int.Parse(Request.QueryString["id"]);
                var detalles = detalleManager.ObtenerPorDetalle(idCompra);
                var articulos = detalleManager.ObtenerArticulosPorDetalleCompra(idCompra);

                if (detalles.Count > 0 && articulos.Count > 0)
                {
                    DetalleCompra detalle = detalles[0];
                    DetalleArticulo articulo = articulos[0];

                    txtIdCompra.Text = detalle.Id_DetalleCompra.ToString();
                    txtFechaCompra.Text = detalle.Fecha_Compra.ToString("yyyy-MM-dd");
                    txtTotalCompra.Text = detalle.ImporteTotal.ToString("C");
                    ddlEstadoCompra.SelectedValue = detalle.EstadoCompraId.ToString();

                    txtArticuloId.Text = articulo.ArticuloId.ToString();
                    txtNombreArticulo.Text = articulo.NombreArticulo;
                    txtCantidad.Text = articulo.Cantidad.ToString();
                    txtPrecioUnidad.Text = articulo.PrecioUnidad.ToString("F2");
                }
                else
                {
                    lblMensaje.Text = "No se encontraron datos de la compra.";
                    lblMensaje.Visible = true;
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idCompra = int.Parse(txtIdCompra.Text);
            int articuloId = int.Parse(txtArticuloId.Text);
            int nuevaCantidad = int.Parse(txtCantidad.Text);
            decimal nuevoPrecio = decimal.Parse(txtPrecioUnidad.Text);

            bool actualizado = detalleManager.ActualizarDetalleCompra(idCompra, articuloId, nuevaCantidad, nuevoPrecio);

            if (actualizado)
            {
                lblMensaje.Text = "Compra actualizada correctamente.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
            }
            else
            {
                lblMensaje.Text = "Error al actualizar la compra.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/CompraDetallada.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idCompra = int.Parse(txtIdCompra.Text);

            bool eliminado = detalleManager.EliminarCompra(idCompra);

            if (eliminado)
            {
                Response.Redirect("CompraDetallada.aspx"); // Redirige tras eliminar
            }
            else
            {
                lblMensaje.Text = "No se pudo eliminar la compra.";
                lblMensaje.Visible = true;
            }
        }

    }
}