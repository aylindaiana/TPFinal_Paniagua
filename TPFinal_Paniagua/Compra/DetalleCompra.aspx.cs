using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Compra
{
    public partial class DetalleCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblId.Enabled = true;
            if (!IsPostBack)
            {
                string idProduct = Session["ArticuloId"] as string;

                if (!string.IsNullOrEmpty(idProduct))
                {
                    CargaDetallesProducto(idProduct);
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
            int cantidadSeleccionada = 1;

            Session["CantidadSeleccionada"] = cantidadSeleccionada;


            Session["ArticuloId"] = lblId.Text;


            Response.Redirect("/Compra/Carrito.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Productos.aspx");
        }

        //Funciones:
        public void CargaDetallesProducto(string id)
        {
            ArticuloManager manager = new ArticuloManager();
            Articulo articulo = manager.ListarArticulosActivos().Find(x => x.Id_Articulo == Convert.ToInt32(id));
            lblId.Text = articulo.Id_Articulo.ToString();
            lblNombre.Text = articulo.Nombre;
            lblDescripcion.Text = articulo.Descripcion;
            lblPrecio.Text = articulo.Precio.ToString();
            imgArticulo.ImageUrl = articulo.ImagenURL;

            CategoriaManager categoriaManager = new CategoriaManager();
            Categoria categoria = categoriaManager.ListarTodos().Find(c => c.Id_Categoria == articulo.CategoriaId);
            lblCategoria.Text = categoria != null ? categoria.Nombre : "Categoría no encontrada";

            TipoManager tipoManager = new TipoManager();
            Tipo tipo = tipoManager.ListarTodos().Find(t => t.Id_Tipo == articulo.TipoId);
            lblTipo.Text = tipo != null ? tipo.Nombre : "Tipo no encontrado";



        }
        public void ChequearStock()
        {
            try
            {
                ArticuloManager manager = new ArticuloManager();
                int stock = manager.ObtenerStock(int.Parse(lblId.Text));
                if (stock == 0)
                {
                    btnAgregarCarrito.Enabled = false;
                    btnAgregarCarrito.Text = "Artículo sin stock.";
                    btnAgregarCarrito.CssClass = "btn btn-warning";
                }
                else
                {
                    btnAgregarCarrito.Enabled = true;
                    btnAgregarCarrito.Text = "Agregar al carrito";
                    btnAgregarCarrito.CssClass = "btn btn-primary";
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error de conversión: " + ex.Message);
            }
        }
    }
}