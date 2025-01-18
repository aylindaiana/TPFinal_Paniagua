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
            }

        }
        protected void filtrarPorCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btnCategoria = (LinkButton)sender;
            int idCategoria = int.Parse(btnCategoria.CommandArgument);

            ArticuloManager articulo = new ArticuloManager();

            if (idCategoria == 0) // Mostrar todos
            {
                listaArticulos = articulo.ListarArticulosActivos(); // Método para obtener todos los productos
            }
            else
            {
            //    listaArticulos = articuloNegocio.listarPorCategoria(idCategoria); // Filtrar por categoría
            }

         //   repRepetidor.DataSource = listaArticulos;
           // repRepetidor.DataBind();
        }

        //Funciones:
        private void CargarCategorias()
        {
            CategoriaManager categoriaNegocio = new CategoriaManager();
            listaCategorias = categoriaNegocio.ListarActivos();
            repCategorias.DataSource = listaCategorias;
            repCategorias.DataBind();
        }

    }
}