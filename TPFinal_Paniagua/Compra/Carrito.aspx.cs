﻿using Dominio;
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
               // lblError.Text = "";
               // lblError.Visible = false;
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
            lblError.Text = "";
            lblError.Visible = false;
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;


            if (listaArticulos == null || listaArticulos.Count == 0)
            {
                lblError.Text = "El carrito está vacío, no se puede realizar ninguna compra sin ningún artículo seleccionado.";
                lblError.Visible = true;
                return;
            }
            decimal importeTotal = (Session["ImporteTotal"] != null) ? (decimal)Session["ImporteTotal"] : 0;
            Session["ImporteTotal"] = importeTotal;

            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario == null)
            {
                Response.Redirect("~/Ingreso.aspx");
            }
            else
            {
                Session["usuarioActual"] = usuario;
                Response.Redirect("Pagar.aspx");
            }
        }

        protected void dgvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string idArticulo = e.CommandArgument.ToString();

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
        public void CargarArticulos(string idArticulo)
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
                    Session["DiccionarioCantidades"] = new Dictionary<string, int>();
                }

                Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;

                if (Session["DiccionarioTalles"] == null)
                {
                    Session["DiccionarioTalles"] = new Dictionary<string, int>();
                }
                Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int>;

                if (Session["DiccionarioStock"] == null)
                {
                    Session["DiccionarioStock"] = new Dictionary<string, int>();
                }
                Dictionary<string, int> diccionarioStock = Session["DiccionarioStock"] as Dictionary<string, int>;

                ArticuloManager manager = new ArticuloManager();
                Articulo articulo = manager.ListarArticulosTodos().Find(x => x.Id_Articulo == Convert.ToInt32(idArticulo));

                if (articulo == null)
                {
                    lblError.Text = "El artículo seleccionado no existe.";
                    lblError.Visible = true;
                    return;
                }

                if (Session["TalleSeleccionado"] != null)
                {
                    int idTalle = Convert.ToInt32(Session["TalleSeleccionado"]);
                    int cantidad = Convert.ToInt32(Session["CantidadSeleccionada"] ?? 1);


                    string claveArticuloTalle = $"{idArticulo}-{idTalle}";

                    int stockDisponible = manager.ObtenerStockTalle(articulo.Id_Articulo, idTalle);

                    diccionarioStock[claveArticuloTalle] = stockDisponible;

                    if (stockDisponible >= cantidad)
                    {
                        articulo.Stock = stockDisponible;

                        if (diccionarioCantidades.ContainsKey(claveArticuloTalle))
                        {
                            diccionarioCantidades[claveArticuloTalle] += cantidad;
                        }
                        else
                        {
                            diccionarioCantidades[claveArticuloTalle] = cantidad;
                            diccionarioTalles[claveArticuloTalle] = idTalle;
                            listaArticulos.Add(articulo); 
                        }
                    

                        Session["ListaArticulos"] = listaArticulos;
                        Session["DiccionarioCantidades"] = diccionarioCantidades;
                        Session["DiccionarioTalles"] = diccionarioTalles;
                        Session["DiccionarioStock"] = diccionarioStock;

                    }
                    else
                    {
                        lblError.Text = "No hay suficiente stock disponible para el talle seleccionado.";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al agregar el artículo: " + ex.Message;
                lblError.Visible = true;
            }
        }


        public void CargarCarrito()
        {
            if (Session["DiccionarioCantidades"] == null || Session["DiccionarioTalles"] == null)
                return;

            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;
            Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int>;
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;

            try
            {
                if (listaArticulos != null && diccionarioCantidades != null && diccionarioTalles != null)
                {
                    var carrito = diccionarioTalles
                        .Select(kv => new
                        {
                            Clave = kv.Key,
                            IdArticulo = kv.Key.Split('-')[0], //obtiene el Id_Articulo desde la clave
                            Talle = kv.Value
                        })
                        .GroupBy(x => new { x.IdArticulo, x.Talle }) //agrupa por artículo y talle
                        .Select(grupo =>
                        {
                            string claveArticuloTalle = grupo.Key.IdArticulo + "-" + grupo.Key.Talle;

                            Articulo articulo = listaArticulos.FirstOrDefault(a => a.Id_Articulo.ToString() == grupo.Key.IdArticulo);

                            if (articulo != null && diccionarioCantidades.ContainsKey(claveArticuloTalle))
                            {
                                return new
                                {
                                    Id_Articulo = articulo.Id_Articulo,
                                    Nombre = articulo.Nombre,
                                    Precio = articulo.Precio,
                                    Cantidad = diccionarioCantidades[claveArticuloTalle],
                                    StockMaximo = StockTalle(articulo.Id_Articulo, grupo.Key.Talle),

                                    Subtotal = articulo.Precio * diccionarioCantidades[claveArticuloTalle],
                                    Talle = grupo.Key.Talle
                                };
                            }
                            return null;
                        })
                        .Where(item => item != null)
                        .ToList();

                    decimal subtotalGeneral = carrito.Sum(item => item.Subtotal);
                    Session["Subtotal"] = subtotalGeneral;

                    dgvCarrito.DataSource = carrito;
                    dgvCarrito.DataBind();
                }
                else
                {
                    dgvCarrito.DataSource = null;
                    dgvCarrito.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar el carrito: " + ex.Message;
                lblError.Visible = true;
            }
        }


        private int StockTalle(int idArticulo, int talle)
        {
            Dictionary<string, int> diccionarioStock = Session["DiccionarioStock"] as Dictionary<string, int>;

       //     Response.Write("<br/>DEBUG: Revisando DiccionarioStock en ObtenerStockPorTalle:<br/>");
            if (diccionarioStock != null)
            {
         //       foreach (var item in diccionarioStock)
             //   {
           //         Response.Write($"DEBUG Stock: Clave={item.Key}, Stock={item.Value} <br/>");
             //   }

                string clave = idArticulo + "-" + talle;
                if (diccionarioStock.ContainsKey(clave))
                {
                    return diccionarioStock[clave];
                }
                else
                {
                    Response.Write($"DEBUG: No se encontró stock para clave {clave}. Retornando 0.<br/>");
                }
            }
            else
            {
                Response.Write("DEBUG: DiccionarioStock es NULL.<br/>");
            }

            return 0; 
        }

        private void CalcularTotal()
        {
           // lblError.Text = "";
           // lblError.Visible = false;
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;

            try
            {
                decimal total = 0;
                if (listaArticulos != null && diccionarioCantidades != null)
                {
                    foreach (var clave in diccionarioCantidades.Keys)
                    {
                        string[] partes = clave.Split('-');
                        string idArticulo = partes[0]; // Extraer el ID del artículo

                        Articulo articulo = listaArticulos.FirstOrDefault(a => a.Id_Articulo.ToString() == idArticulo);
                        if (articulo != null)
                        {
                            total += articulo.Precio * diccionarioCantidades[clave];
                        }
                    }
                }
                Session["ImporteTotal"] = total;

                lblTotal.Text = $"Total: ${total.ToString("0.00")}";
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al calcular el total: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void AumentarCantidad(string claveArticuloTalle)
        {
           // lblError.Text = "";
           // lblError.Visible = false;
            Dictionary<string, Tuple<int, decimal>> carrito = Session["Carrito"] as Dictionary<string, Tuple<int, decimal>>;
            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;

            if (carrito != null && carrito.ContainsKey(claveArticuloTalle) && diccionarioCantidades != null)
            {
                int idArticulo = Convert.ToInt32(claveArticuloTalle.Split('-')[0]);
                int idTalle = (Session["DiccionarioTalles"] as Dictionary<string, int>)[claveArticuloTalle];

                ArticuloManager manager = new ArticuloManager();
                int stockDisponible = manager.ObtenerStockTalle(idArticulo, idTalle);
                decimal precioUnitario = carrito[claveArticuloTalle].Item2 / carrito[claveArticuloTalle].Item1; // Obtener precio unitario

                if (diccionarioCantidades[claveArticuloTalle] < stockDisponible)
                {
                    diccionarioCantidades[claveArticuloTalle]++;
                    carrito[claveArticuloTalle] = new Tuple<int, decimal>(
                        carrito[claveArticuloTalle].Item1 + 1,
                        carrito[claveArticuloTalle].Item2 + precioUnitario
                    );

                    Session["Carrito"] = carrito;
                    Session["DiccionarioCantidades"] = diccionarioCantidades;
                }
                else
                {
                    lblError.Text = "No puedes agregar más productos, stock insuficiente.";
                    lblError.CssClass = "text-danger"; 
                    lblError.Visible = true;
                }
            }
        }


        private void DisminuirCantidad(string claveArticuloTalle)
        {
             lblError.Text = "";
             lblError.Visible = false;
            Dictionary<string, Tuple<int, decimal>> carrito = Session["Carrito"] as Dictionary<string, Tuple<int, decimal>>;
            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;

            if (carrito != null && carrito.ContainsKey(claveArticuloTalle) && diccionarioCantidades != null)
            {
                decimal precioUnitario = carrito[claveArticuloTalle].Item2 / carrito[claveArticuloTalle].Item1;

                if (diccionarioCantidades[claveArticuloTalle] > 1)
                {
                    diccionarioCantidades[claveArticuloTalle]--;
                    carrito[claveArticuloTalle] = new Tuple<int, decimal>(
                        carrito[claveArticuloTalle].Item1 - 1,
                        carrito[claveArticuloTalle].Item2 - precioUnitario
                    );
                }
                else
                {
                    diccionarioCantidades.Remove(claveArticuloTalle);
                    carrito.Remove(claveArticuloTalle);
                }

                Session["Carrito"] = carrito;
                Session["DiccionarioCantidades"] = diccionarioCantidades;
            }
        }



        private void EliminarArticulo(string claveArticuloTalle)
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;
            Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int>;

            try
            {
                if (listaArticulos != null && diccionarioCantidades != null)
                {
                    diccionarioCantidades.Remove(claveArticuloTalle);
                    diccionarioTalles.Remove(claveArticuloTalle);

                    string[] partes = claveArticuloTalle.Split('-');
                    int idArticulo = Convert.ToInt32(partes[0]);

                    if (!diccionarioCantidades.Keys.Any(k => k.StartsWith(idArticulo + "-")))
                    {
                        listaArticulos.RemoveAll(a => a.Id_Articulo == idArticulo);
                    }

                    Session["ListaArticulos"] = listaArticulos;
                    Session["DiccionarioCantidades"] = diccionarioCantidades;
                    Session["DiccionarioTalles"] = diccionarioTalles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}