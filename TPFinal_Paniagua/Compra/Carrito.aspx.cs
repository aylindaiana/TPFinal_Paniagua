using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Compra
{
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos = new List<Articulo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idArticulo = Session["ArticuloId"] as string;
                if (!string.IsNullOrEmpty(idArticulo))
                {
                    CargarArticulos(idArticulo);
                    Session["ArticuloId"] = null;
                }
                CargarCarrito();
                CalcularTotal();
            }
        }


        protected void btnPay_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario == null)
            {
                Response.Redirect("/Inicio.aspx");
            }
            else
            {
                Session["usuarioActual"] = usuario;
                Response.Redirect("Pagar.aspx");
            }
        }

        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idArticulo = Convert.ToInt32(e.CommandArgument); 

            switch (e.CommandName)
            {
                case "Aumentar":
                    AumentarCantidad(idArticulo);
                    break;

                case "Disminuir":
                    DisminuirCantidad(idArticulo);
                    break;

                case "Eliminar":
                    EliminarArticulo(idArticulo);
                    break;
            }
            CargarCarrito();
            CalcularTotal();
        }

        //Funciones:
        public void CargarArticulos(string id)
        {
            try
            {
                if (Session["ListaArticulos"] == null)
                {
                    Session["ListaArticulos"] = new List<Articulo>();
                }

                List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;

                if (Session["DiccionarioCantidades"] == null)
                {
                    Session["DiccionarioCantidades"] = new Dictionary<int, int>();
                }

                Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

                ArticuloManager manager = new ArticuloManager();
                Articulo articulo = new Articulo();
                if (articulo != null)
                {
                    int cantidad = Convert.ToInt32(Session["CantidadSeleccionada"]);
                    if (!listaArticulos.Any(a => a.Id_Articulo == articulo.Id_Articulo))
                    {
                        listaArticulos.Add(articulo);
                        diccionarioCantidades[articulo.Id_Articulo] = cantidad;
                    }
                    else
                    {
                        diccionarioCantidades[articulo.Id_Articulo] += cantidad;
                    }

                    Session["ListaArticulos"] = listaArticulos;
                    Session["DiccionarioCantidades"] = diccionarioCantidades;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CargarCarrito()
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            try
            {
                if (listaArticulos != null && diccionarioCantidades != null)
                {
                    var carrito = listaArticulos.Select(a => new
                    {
                        a.Id_Articulo,
                        a.Nombre,
                        a.Precio,
                        Cantidad = diccionarioCantidades[a.Id_Articulo],
                        StockMaximo = a.Stock,
                        Subtotal = a.Precio * diccionarioCantidades[a.Id_Articulo]
                    }).ToList();

                    decimal subtotalGeneral = carrito.Sum(item => item.Subtotal);
                    Session["Subtotal"] = subtotalGeneral;

                    repCarrito.DataSource = carrito;
                    repCarrito.DataBind();
                }
                else
                {
                    repCarrito.DataSource = null;
                    repCarrito.DataBind();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CalcularTotal()
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            try
            {
                decimal total = 0;
                if (listaArticulos != null && diccionarioCantidades != null)
                {
                    total = listaArticulos.Sum(a => a.Precio * diccionarioCantidades[a.Id_Articulo]);
                }
                lblTotal.Text = $"Total: ${total.ToString("0.00")}";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void AumentarCantidad(int idArticulo)
        {
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;

            try
            {
                if (diccionarioCantidades != null && listaArticulos != null)
                {
                    if (diccionarioCantidades.ContainsKey(idArticulo))
                    {
                        Articulo articulo = listaArticulos.FirstOrDefault(a => a.Id_Articulo == idArticulo);

                        if (articulo != null)
                        {
                            // Verificar si la cantidad actual es menor que el stock máximo
                            int cantidadActual = diccionarioCantidades[idArticulo];
                            if (cantidadActual < articulo.Stock)
                            {
                                // Incrementar la cantidad solo si no supera el stock máximo
                                diccionarioCantidades[idArticulo]++;
                                Session["DiccionarioCantidades"] = diccionarioCantidades;
                            }
                            else
                            {
                                Response.Write("No se puede incrementar más. Alcanzaste el tope del stock disponible.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void DisminuirCantidad(int idArticulo)
        {
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            try
            {
                if (diccionarioCantidades != null && diccionarioCantidades.ContainsKey(idArticulo))
                {
                    if (diccionarioCantidades[idArticulo] > 1)
                    {
                        diccionarioCantidades[idArticulo]--;
                    }
                    else
                    {
                        EliminarArticulo(idArticulo);
                    }

                    Session["DiccionarioCantidades"] = diccionarioCantidades;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void EliminarArticulo(int idArticulo)
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            try
            {
                if (listaArticulos != null && diccionarioCantidades != null)
                {
                    listaArticulos.RemoveAll(a => a.Id_Articulo == idArticulo);
                    diccionarioCantidades.Remove(idArticulo);

                    Session["ListaArticulos"] = listaArticulos;
                    Session["DiccionarioCantidades"] = diccionarioCantidades;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}