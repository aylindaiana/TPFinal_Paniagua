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

                Response.Redirect("/Compra/DetalleCompra.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var articulo = (Articulo)e.Item.DataItem;
                var imgProducto = e.Item.FindControl("imgProducto") as Image;

                if (imgProducto != null && articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    imgProducto.ImageUrl = articulo.Imagenes[0].UrlImagen;
                    imgProducto.Visible = true; 
                }
            }
        }


        protected void filtrarPorCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btnCategoria = (LinkButton)sender;
            int idCategoria = int.Parse(btnCategoria.CommandArgument);

            ArticuloManager articulo = new ArticuloManager();

            if (idCategoria == 0) 
            {
                listaArticulos = articulo.ListarArticulosActivos(); 
            }
            else
            {
                listaArticulos = articulo.ListarArticulosPorCategoria(idCategoria); 
            }

            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

        //Funciones:
        private void CargarCategorias()
        {
            CategoriaManager categoria = new CategoriaManager();
            listaCategorias = categoria.ListarActivos();
            repCategorias.DataSource = listaCategorias;
            repCategorias.DataBind();
        }
        private void CargarProductos()
        {
            ArticuloManager articulo = new ArticuloManager();
            listaArticulos = articulo.ListarArticulosActivos(); 
            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

    }
}