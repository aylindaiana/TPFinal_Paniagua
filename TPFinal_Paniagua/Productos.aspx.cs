using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        public List<Categoria> listaCategorias { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloManager manager = new ArticuloManager();
            listaArticulos = manager.ListarArticulosActivos();

            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();

            }

        }
        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton btnVerDetalle = (LinkButton)sender;
                string idArticulo = btnVerDetalle.CommandArgument;


                Session.Add("ArticuloId", idArticulo);

                Response.Redirect("DetalleProducto.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void filtrarPorCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btnCategoria = (LinkButton)sender;
            int idCategoria = int.Parse(btnCategoria.CommandArgument);

            ArticuloManager articulo = new ArticuloManager();

            if (idCategoria == 0) 
            {
                listaArticulos = articulo.ListarArticulosActivos(); // Método para obtener todos los productos
            }
            else
            {
                listaArticulos = articulo.ListarArticulosPorCategoria(idCategoria); // Filtrar por categoría
            }

            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

        //Funciones:
        private void CargarCategorias()
        {
            CategoriaManager categoriaNegocio = new CategoriaManager();
            listaCategorias = categoriaNegocio.ListarActivos();
            repCategorias.DataSource = listaCategorias;
            repCategorias.DataBind();
        }
        private void CargarProductos()
        {
            ArticuloManager articulo = new ArticuloManager();
            listaArticulos = articulo.ListarArticulosActivos(); // Método para obtener productos
            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

    }
}