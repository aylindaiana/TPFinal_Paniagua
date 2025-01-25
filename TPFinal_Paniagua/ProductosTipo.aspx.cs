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
    public partial class ProductosTipo : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        public List<Tipo> listaTipos { get; set; }
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
        protected void filtrarPorTipo_Click(object sender, EventArgs e)
        {
            LinkButton btnTipo = (LinkButton)sender;
            int idTipo = int.Parse(btnTipo.CommandArgument);

            ArticuloManager articulo = new ArticuloManager();

            if (idTipo == 0)
            {
                listaArticulos = articulo.ListarArticulosActivos();
            }
            else
            {
                listaArticulos = articulo.ListarArticulosPorTipo(idTipo);
            }

            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

        //Funciones:
        private void CargarCategorias()
        {
            TipoManager tipo = new TipoManager();
            listaTipos = tipo.ListarActivos();
            repTipos.DataSource = listaTipos;
            repTipos.DataBind();
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