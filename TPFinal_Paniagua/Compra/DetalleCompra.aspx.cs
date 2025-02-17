using Dominio;
using Manager;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPFinal_Paniagua.Administrador;

namespace TPFinal_Paniagua.Compra
{
    public partial class DetalleCompra : System.Web.UI.Page
    {
        public int UsuarioId { get; internal set; }
        public int CarritoId { get; internal set; }
        public decimal ImporteTotal { get; internal set; }
        public DateTime Fecha_Compra { get; internal set; }
        public int EstadoCompraId { get; internal set; }
        public string DireccionEntregar { get; internal set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblId.Enabled = true;
            if (!IsPostBack)
            {
                string idProduct = Session["ArticuloId"] as string;
                if (!string.IsNullOrEmpty(idProduct) && int.TryParse(idProduct, out int articuloId))
                {
                    CargaDetallesProducto(idProduct);
                    ObtenerImagenesArticulo(idProduct);
                    CargarTalles(articuloId);
                    lblId.Visible = false;
                }
                else
                {
                    Response.Write("Hubo un problema para encontrar el producto.");
                }
                ChequearStock();
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (Session["TalleSeleccionado"] != null)
            {
                int talleId = (int)Session["TalleSeleccionado"];
                Session["ArticuloId"] = lblId.Text;
                Session["TalleId"] = talleId;
                Session["CantidadSeleccionada"] = 1;

                // Depuración antes de redirigir
                Response.Write($"DEBUG: Agregando Artículo {lblId.Text} - Talle {talleId} - Cantidad {Session["CantidadSeleccionada"]}<br/>");

                Response.Redirect("/Compra/Carrito.aspx");
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona un talle antes de agregar al carrito.";
                lblMensaje.Visible = true;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Productos.aspx");
        }

        //Funciones:

        private void CargarTalles(int articuloId)
        {
            var talleManager = new TalleManager();
            List<Talles> talles = talleManager.ObtenerStockPorTalle(articuloId);

            // Debug: Ver los datos antes de pasarlos al Repeater
            foreach (var talle in talles)
            {
                Response.Write($"DEBUG: Talle ID = {talle.Id_Talle}, Stock = {talle.Stock}<br/>");
            }

            repTalles.DataSource = talles;
            repTalles.DataBind();
        }


        public void CargaDetallesProducto(string id)
        {
            ArticuloManager manager = new ArticuloManager();
            Articulo articulo = manager.ListarArticulosDetalleCompra().Find(x => x.Id_Articulo == Convert.ToInt32(id));
            lblId.Text = articulo.Id_Articulo.ToString();
            lblNombre.Text = articulo.Nombre;
            lblDescripcion.Text = articulo.Descripcion;
            lblPrecio.Text = articulo.Precio.ToString();
            //    imgArticulo.ImageUrl = articulo.ImagenURL;

            CategoriaManager categoriaManager = new CategoriaManager();
            Categoria categoria = categoriaManager.ListarTodos().Find(c => c.Id_Categoria == articulo.CategoriaId);
            lblCategoria.Text = categoria != null ? categoria.Nombre : "Categoría no encontrada";

            TipoManager tipoManager = new TipoManager();
            Tipo tipo = tipoManager.ListarTodos().Find(t => t.Id_Tipo == articulo.TipoId);
            lblTipo.Text = tipo != null ? tipo.Nombre : "Tipo no encontrado";



        }
        protected void rbtnTalle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            RepeaterItem item = (RepeaterItem)rbtn.NamingContainer;

            HiddenField hfIdTalle = (HiddenField)item.FindControl("hfIdTalle");
            HiddenField hfStock = (HiddenField)item.FindControl("hfStock");

            if (hfIdTalle != null && hfStock != null)
            {
                int idTalle = int.Parse(hfIdTalle.Value);
                int stockDisponible = int.Parse(hfStock.Value);

                // Guardamos en sesión el talle seleccionado y su stock
                Session["TalleSeleccionado"] = idTalle;
                Session["StockTalleSeleccionado"] = stockDisponible;

                lblMensaje.Text = $"Talle seleccionado: {idTalle}, Stock: {stockDisponible}";
                lblMensaje.Visible = true;
            }
            if (int.TryParse(Session["ArticuloId"] as string, out int articuloId))
            {
                CargarTalles(articuloId);  // Pasamos el int, no el string
            }
        }


        public void ChequearStock()
        {
            try
            {
                if (Session["TalleSeleccionado"] != null)
                {
                    int talleId = (int)Session["TalleSeleccionado"];
                    int stockTalle = 0;

                    if (Session["StockTalleSeleccionado"] != null)
                    {
                        stockTalle = (int)Session["StockTalleSeleccionado"];
                    }
                    else
                    {
                        ArticuloManager manager = new ArticuloManager();
                        stockTalle = manager.ObtenerStockTalle(int.Parse(lblId.Text), talleId);

                        Session["StockTalleSeleccionado"] = stockTalle;
                    }

                    Response.Write($"DEBUG: Stock disponible para Artículo {lblId.Text}, Talle {talleId} = {stockTalle}<br/>");

                    if (stockTalle == 0)
                    {
                        btnAgregarCarrito.Enabled = false;
                        btnAgregarCarrito.Text = "No hay stock para este talle.";
                        btnAgregarCarrito.CssClass = "btn btn-warning";
                    }
                    else
                    {
                        btnAgregarCarrito.Enabled = true;
                        btnAgregarCarrito.Text = "Agregar al carrito";
                        btnAgregarCarrito.CssClass = "btn btn-primary";
                    }
                }
                else
                {
                    btnAgregarCarrito.Enabled = false;
                    btnAgregarCarrito.Text = "Selecciona un talle";
                    btnAgregarCarrito.CssClass = "btn btn-secondary";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error en ChequearStock: " + ex.Message);
            }
        }



        private void ObtenerImagenesArticulo(string id)
        {
            ArticuloManager manager = new ArticuloManager();
            Articulo articulo = manager.ListarArticulosDetalleCompra().Find(x => x.Id_Articulo == Convert.ToInt32(id));

            if (articulo != null && articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                repImagenes.DataSource = articulo.Imagenes;
                repImagenes.DataBind();

                imgArticulo.ImageUrl = articulo.Imagenes[0].UrlImagen;
            }
            else
            {
                imgArticulo.ImageUrl = "https://via.placeholder.com/200";
            }
        }
    }
}