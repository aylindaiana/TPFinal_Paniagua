using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Compra
{
    public partial class Pagar : System.Web.UI.Page
    {
        DetalleManager detalleManager = new DetalleManager();
        UsuarioManager usuarioManager = new UsuarioManager();  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReconocerUsuario();
                CargarResumenCompra();
            }
        }

        protected void Pago_CheckedChanged(object sender, EventArgs e)
        {
           
            if (rbtnCredito.Checked || rbtnDebito.Checked)
            {
                txtNumeroTarjeta.Enabled = true;
                txtNombreTitular.Enabled = true;
                txtVencimiento.Enabled = true;
                txtCVV.Enabled = true;
                txtDNI.Enabled = true;

                divEfectivo.Visible = false; 
            }

            else if (rbtnEfectivo.Checked)
            {
                txtNumeroTarjeta.Enabled = false;
                txtNombreTitular.Enabled = false;
                txtVencimiento.Enabled = false;
                txtCVV.Enabled = false;
                txtDNI.Enabled = false;
                txtNumeroTarjeta.Visible = false;
                txtNombreTitular.Visible = false;
                txtVencimiento.Visible = false;
                txtCVV.Visible = false;
                txtDNI.Visible = false;

                divEfectivo.Visible = true; 
            }
        }

        protected void Envio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAcordar.Checked)
            {

                lblCodigoPostal.Visible = true;
                codigoPostal.Visible = true;
            }
            else if (rbtnDomicilio.Checked) 
            {
                lblCodigoPostal.Visible = false;
                codigoPostal.Visible = false;
            }
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            string errores = ValidarFormulario();

            if (!string.IsNullOrEmpty(errores))
            {
                lblMensaje.Text = errores;
                lblMensaje.Visible = true;
                return;
            }


            ActualizarStock();
            Response.Redirect("~/Confirmacion.aspx");
        }


        //Funciones: 
        public void ReconocerUsuario()
        {
            List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            int idArticulo = 1;
            
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["usuarioActual"];

        }
        public void ActualizarStock()
        {

            List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            Usuario usuario = Session["usuarioActual"] as Usuario;

            if (listaArticulo == null || diccionarioCantidades == null || usuario == null)
            {
                lblMensaje.Text = "Faltan datos en la sesión para procesar el pedido. Debes Registrate o Iniciar Sesion";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;

                System.Threading.Thread.Sleep(3000);

                Response.Redirect("~/Ingreso.aspx");
            }

            decimal subtotal = listaArticulo
                .Where(a => diccionarioCantidades.ContainsKey(a.Id_Articulo))
                .Sum(a => a.Precio * diccionarioCantidades[a.Id_Articulo]);

            CarritoManager carrito = new CarritoManager();

            int idCarrito = carrito.AgregarCarrito(usuario.Id_Usuario);
            int idUsuario = usuario.Id_Usuario;
            DateTime fecha = DateTime.Now;

            usuario.Direccion = usuarioManager.ObtenerUsuarioPorId(idUsuario).Direccion;
            Dominio.DetalleCompra detalle = new Dominio.DetalleCompra();
            
            detalle.UsuarioId = idUsuario;
            detalle.CarritoCompraId = idCarrito;
            detalle.ImporteTotal =subtotal;
            detalle.Fecha_Compra = fecha;
            detalle.EstadoCompraId = 1;
            detalle.DireccionEntregar = usuario.Direccion;
            detalleManager.Agregar(detalle);

            ArticuloManager articuloManager = new ArticuloManager();
            foreach (var articulo in listaArticulo)
            {
                if (diccionarioCantidades.TryGetValue(articulo.Id_Articulo, out int cantidadVendida))
                {
                    int stockDisponible = articuloManager.ObtenerStock(articulo.Id_Articulo);
                    if (stockDisponible >= cantidadVendida)
                    {
                        articuloManager.ActualizarStock(articulo.Id_Articulo, cantidadVendida);
                    }
                    else
                    {
                        throw new InvalidOperationException($"No hay suficiente stock para el artículo con Id: {articulo.Id_Articulo}. Stock disponible: {stockDisponible}, La Cantidad requerida: {cantidadVendida}.");
                    }
                }
            }

            Session["ListaArticulos"] = null;
            Session["DiccionarioCantidades"] = null;
            listaArticulo = null;
            diccionarioCantidades = null;
            usuario = null;

            lblMensaje.Text = "Pago realizado con éxito.";
            lblMensaje.CssClass = "text-success"; 
            lblMensaje.Visible = true;

        }

        private void CargarResumenCompra()
        {
            List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            if (listaArticulo != null && diccionarioCantidades != null)
            {
                var resumenCompra = listaArticulo
                    .Where(a => diccionarioCantidades.ContainsKey(a.Id_Articulo))
                    .Select(a => new
                    {
                        Nombre = a.Nombre,
                        Cantidad = diccionarioCantidades[a.Id_Articulo]
                    }).ToList();

                rptResumenCompra.DataSource = resumenCompra;
                rptResumenCompra.DataBind();

                decimal total = listaArticulo.Sum(a => a.Precio * diccionarioCantidades[a.Id_Articulo]);
                lblTotal.Text = "$" + total.ToString("N2");
            }
        }


        private string ValidarFormulario()
        {
            StringBuilder errores = new StringBuilder();

            if (!rbtnCredito.Checked && !rbtnDebito.Checked && !rbtnEfectivo.Checked)
            {
                errores.AppendLine("Debe seleccionar un método de pago.<br/>");
            }
            if (!rbtnAcordar.Checked && !rbtnDomicilio.Checked)
            {
                errores.AppendLine("Debe seleccionar una forma de envio.<br/>");
            }
            if (rbtnCredito.Checked || rbtnDebito.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || txtNumeroTarjeta.Text.Length != 16)
                {
                    errores.AppendLine("El numero es invalido o incorrecto. Intente Nuevamente.<br/>");
                }

                if (string.IsNullOrWhiteSpace(txtNombreTitular.Text))
                {
                    errores.AppendLine("Tiene que agregar un nombre válido.<br/>");
                }

                if (string.IsNullOrWhiteSpace(txtVencimiento.Text))
                {
                    errores.AppendLine("Debe rellenar este campo.<br/>");
                }

                if (string.IsNullOrWhiteSpace(txtCVV.Text) || txtCVV.Text.Length != 3)
                {
                    errores.AppendLine("Ingrese un formato valido y complete todo los campo.<br/>");
                }
            }
            return errores.ToString();
        }
    }
}