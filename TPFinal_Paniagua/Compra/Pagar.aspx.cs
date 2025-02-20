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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using TPFinal_Paniagua.Administrador;

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

        private string GenerarPDF()
        {
            try
            {
                Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int> ?? new Dictionary<string, int>();

                List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo>;
                Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int>;
                Usuario usuario = Session["usuarioActual"] as Usuario;

                if (listaArticulo == null || diccionarioCantidades == null || usuario == null)
                {
                    return null;
                }
                //---------
                foreach (var articulo in listaArticulo)
                {
                    Console.WriteLine($"Articulo: {articulo.Id_Articulo} - {articulo.Nombre} - Precio: {articulo.Precio}");
                }

                foreach (var kvp in diccionarioCantidades)
                {
                    Console.WriteLine($"Clave: {kvp.Key}, Cantidad: {kvp.Value}");
                }
                //---------
                string carpetaFacturas = Server.MapPath("~/Facturas/");
                if (!Directory.Exists(carpetaFacturas))
                {
                    Directory.CreateDirectory(carpetaFacturas);
                }

                string nombreArchivo = $"Factura_{usuario.Id_Usuario}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string ruta = Path.Combine(carpetaFacturas, nombreArchivo);

                Document doc = new Document(PageSize.A4, 30, 30, 50, 50);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                PdfPTable headerTable = new PdfPTable(1);
                headerTable.WidthPercentage = 100;

                string logoPath = Server.MapPath("/Img/LogoRoseVibes.JPEG");
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(120, 120);
                    logo.Alignment = Element.ALIGN_CENTER;
                    PdfPCell logoCell = new PdfPCell(logo) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
                    headerTable.AddCell(logoCell);
                }

                PdfPCell titleCell = new PdfPCell(new Phrase("Rose Vibes", titleFont))
                {
                    Border = PdfPCell.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingBottom = 10
                };
                headerTable.AddCell(titleCell);
                doc.Add(headerTable);

                doc.Add(new Paragraph($"Factura N°: {DateTime.Now:yyyyMMddHHmmss}\n", textFont));
                doc.Add(new Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy}", textFont));
                doc.Add(new Paragraph($"Cliente: {usuario.Nombre} {usuario.Apellido} (ID: {usuario.Id_Usuario})\n\n", textFont));

                PdfPTable table = new PdfPTable(3) { WidthPercentage = 100 };
                table.SetWidths(new float[] { 50f, 25f, 25f });

                PdfPCell header1 = new PdfPCell(new Phrase("Artículo", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
                PdfPCell header2 = new PdfPCell(new Phrase("Cantidad", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
                PdfPCell header3 = new PdfPCell(new Phrase("Precio Total", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };

                table.AddCell(header1);
                table.AddCell(header2);
                table.AddCell(header3);

                decimal total = 0;

                var articulosAgrupados = listaArticulo
                    .GroupBy(a => a.Id_Articulo) 
                    .ToDictionary(g => g.Key, g => g.ToList()); 

               HashSet<string> procesados = new HashSet<string>(); 

                foreach (var grupoArticulo in articulosAgrupados)
                {
                    var idArticulo = grupoArticulo.Key;
                    var articulos = grupoArticulo.Value; 
                    var tallesDelArticulo = diccionarioTalles
                        .Where(kv => kv.Key.StartsWith(idArticulo + "-"))
                        .ToDictionary(kv => kv.Key, kv => kv.Value);

                    foreach (var kvp in tallesDelArticulo)
                    {
                        string claveArticuloTalle = kvp.Key; 
                        int idTalle = kvp.Value;

                        if (!diccionarioCantidades.TryGetValue(claveArticuloTalle, out int cantidad) || cantidad == 0)
                        {
                            continue; 
                        }

                        string claveUnica = $"{idArticulo}-{idTalle}";

                        if (!procesados.Contains(claveUnica)) 
                        {
                            var articulo = articulos.FirstOrDefault(); 

                            decimal subtotal = articulo.Precio * cantidad;
                            total += subtotal;

                            table.AddCell(new PdfPCell(new Phrase($"{articulo.Nombre} - Talle {idTalle}", textFont)) { Padding = 5 });
                            table.AddCell(new PdfPCell(new Phrase(cantidad.ToString(), textFont)) { Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase("$" + subtotal.ToString("N2"), textFont)) { Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });

                            procesados.Add(claveUnica); 
                        }
                    }
                }

                doc.Add(table);
                doc.Add(new Paragraph($"\nTotal Pagado: ${total:N2}\n", titleFont));

                doc.Close();
                return "~/Facturas/" + nombreArchivo;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al generar el PDF: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return null;
            }
        }


        private void GuardarRutaFactura(int idUsuario, string rutaFactura)
        {
            try
            {
                DetalleManager manager = new DetalleManager();
                bool resultado = manager.GuardarRutaFactura(idUsuario, rutaFactura);

                if (resultado)
                {
                    lblMensaje.Text = "Factura guardada correctamente.";
                    lblMensaje.CssClass = "text-success";
                }
                else
                {
                    lblMensaje.Text = "No se pudo guardar la factura.";
                    lblMensaje.CssClass = "text-danger";
                }

                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error en la base de datos: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


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
            try
            {
                List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo>;
                Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int> ?? new Dictionary<string, int>();
                Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int> ?? new Dictionary<string, int>();

                Usuario usuario = Session["usuarioActual"] as Usuario;
                Session["DiccionarioCantidades"] = diccionarioCantidades;  

                if (listaArticulo == null || listaArticulo.Count == 0)
                {
                    Response.Write("❌ Error: No hay artículos en la lista.<br/>");
                    return;
                }

                if (diccionarioCantidades == null)
                {
                    lblMensaje.Text = "Error: El diccionario de cantidades no está en sesión.<br/>";
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                    return;
                }
                if (diccionarioCantidades.Count == 0)
                {
                    Response.Write("⚠️ Advertencia: Diccionario de cantidades está vacío.<br/>");
                }

                if (diccionarioTalles.Count == 0)
                {
                    Response.Write("⚠️ Advertencia: Diccionario de talles está vacío.<br/>");
                }

                if (usuario == null)
                {
                    lblMensaje.Text = "Error: No se encontró usuario en sesión. Debes iniciar sesión.<br/>";
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;

                    System.Threading.Thread.Sleep(2000);
                    Response.Redirect("~/Ingreso.aspx");
                }
                decimal subtotal = diccionarioCantidades
                    .Sum(kv =>
                    {
                        string clave = kv.Key; // Ejemplo: "10-2"
                        int cantidad = kv.Value;

                        int idArticulo = int.Parse(clave.Split('-')[0]); // Extraer el ID del artículo

                        Articulo articulo = listaArticulo.FirstOrDefault(a => a.Id_Articulo == idArticulo);

                        return articulo != null ? articulo.Precio * cantidad : 0;
                    });

                //  Response.Write($"✅ Subtotal corregido: {subtotal}<br/>");


                CarritoManager carrito = new CarritoManager();

                int idCarrito = carrito.AgregarCarrito(usuario.Id_Usuario);
            
                int idUsuario = usuario.Id_Usuario;
                DateTime fecha = DateTime.Now;

                usuario.Direccion = usuarioManager.ObtenerUsuarioPorId(idUsuario).Direccion;
                Dominio.DetalleCompra detalle = new Dominio.DetalleCompra();

                detalle.UsuarioId = idUsuario;
                detalle.CarritoCompraId = idCarrito;
                detalle.ImporteTotal = subtotal;
                detalle.Fecha_Compra = fecha;
                detalle.EstadoCompraId = 1;
                detalle.DireccionEntregar = usuario.Direccion;
               // Response.Write("📄 Intentando generar la factura en PDF...<br/>");
                detalle.RutaFactura = GenerarPDF();


                //detalleManager.Agregar(detalle);
                try
                {
                    detalleManager.Agregar(detalle);
                }
                catch (Exception ex)
                {
                    Response.Write($"❌ Error en Agregar detalle: {ex.Message}<br/>");
                }

                ArticuloManager articuloManager = new ArticuloManager();

                var articulosUnicos = listaArticulo
                    .GroupBy(a => a.Id_Articulo)
                    .Select(g => g.First()) 
                    .ToList();

                foreach (var articulo in articulosUnicos)
                {
                   // Response.Write($"Procesando artículo ID: {articulo.Id_Articulo} - Nombre: {articulo.Nombre}<br/>");
      
                    var tallesDelArticulo = diccionarioTalles
                        .Where(kv => kv.Key.StartsWith(articulo.Id_Articulo + "-"))
                        .ToDictionary(kv => kv.Key, kv => kv.Value);

                    if (tallesDelArticulo.Count == 0)
                    {
                        lblMensaje.Text = $"Error: No se encontró un talle asociado para el artículo {articulo.Nombre}.";
                        lblMensaje.CssClass = "text-danger";
                        lblMensaje.Visible = true;
                        Response.Write($"❌ No se encontró talle para Artículo ID: {articulo.Id_Articulo}<br/>");
                        return;
                    }

                    foreach (var kvp in tallesDelArticulo)
                    {
                        string claveArticuloTalle = kvp.Key; 
                        int idTalle = kvp.Value;

                        if (!diccionarioCantidades.TryGetValue(claveArticuloTalle, out int cantidadVendida))
                        {
                            Response.Write($"NO se encontró cantidad para Artículo ID: {articulo.Id_Articulo} - Talle: {idTalle}<br/>");
                            continue;
                        }

                        int stockDisponible = articuloManager.ObtenerStockPorTalle(articulo.Id_Articulo, idTalle);

                      //  Response.Write($"Articulo: {articulo.Id_Articulo}, Talle: {idTalle}, Stock Disponible: {stockDisponible}, Cantidad Vendida: {cantidadVendida}<br/>");

                        if (stockDisponible >= cantidadVendida)
                        {
                            articuloManager.ActualizarStockTalle(articulo.Id_Articulo, idTalle, cantidadVendida);
                        }
                        else
                        {
                            Response.Write($" ERROR al ejecutar sp_ActualizarStockTalle<br/>");
                            lblMensaje.Text = $"No hay suficiente stock para el artículo {articulo.Nombre}, Talle {idTalle}. Stock disponible: {stockDisponible}, Cantidad requerida: {cantidadVendida}.";
                            lblMensaje.CssClass = "text-danger";
                            lblMensaje.Visible = true;
                            return;
                        }
                    }
                }


                Session["ListaArticulos"] = null;
                Session["DiccionarioCantidades"] = null;
                Session["DiccionarioTalles"] = null;
                  listaArticulo = null;
                  diccionarioCantidades = null;
                  usuario = null;
                //Session.Clear();


                lblMensaje.Text = "Pago realizado con éxito.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {

                lblMensaje.Text = "Error al procesar el pago: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }

        }

        private void CargarResumenCompra()
        {
            List<Articulo> listaArticulo = Session["ListaArticulos"] as List<Articulo> ?? new List<Articulo>();
            Dictionary<string, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<string, int> ?? new Dictionary<string, int>();
            Dictionary<string, int> diccionarioTalles = Session["DiccionarioTalles"] as Dictionary<string, int> ?? new Dictionary<string, int>();

            if (listaArticulo.Count == 0 || diccionarioCantidades.Count == 0)
            {
                lblTotal.Text = "No hay artículos en el carrito.";
                return;
            }

            var resumenCompra = diccionarioCantidades
                .Select(kv =>
                {
                    string[] partes = kv.Key.Split('-'); 
                    string idArticuloStr = partes[0]; 
                    int idTalle = int.Parse(partes[1]); 

                    Articulo articulo = listaArticulo.FirstOrDefault(a => a.Id_Articulo.ToString() == idArticuloStr);

                    if (articulo != null)
                    {
                        return new
                        {
                            articulo.Nombre,
                            Cantidad = kv.Value, 
                            Subtotal = articulo.Precio * kv.Value
                        };
                    }
                    return null;
                })
                .Where(item => item != null) 
                .ToList();

            rptResumenCompra.DataSource = resumenCompra;
            rptResumenCompra.DataBind();

            decimal total = resumenCompra.Sum(item => item.Subtotal);
            lblTotal.Text = $"${total:N2}";
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