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
    public partial class MiCompra : System.Web.UI.Page
    {
        DetalleManager detalleManager = new DetalleManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ReconocerUsuario();
                CargarMisCompras();
                /*
                if (Session["PDF_Descarga"] != null)
                {
                    string nombreArchivo = Session["PDF_Descarga"].ToString();
                    string rutaRelativa = "~/Facturas/" + nombreArchivo;
                    foreach (GridViewRow row in dgvMisCompras.Rows)
                    {
                        var lnkFactura = (HyperLink)row.FindControl("lnkFactura");
                        if (lnkFactura != null && !string.IsNullOrEmpty(rutaRelativa))
                        {
                            lnkFactura.NavigateUrl = rutaRelativa;
                            lnkFactura.Visible = true;
                        }
                    }
                }*/
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx");
        }
        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

        }
        //Funciones:
        private void CargarMisCompras()
        {
            try
            {
                //Usuario usuario = Session["usuarioActual"] as Usuario;
                Usuario usuario = (Usuario)Session["usuarioActual"];
                
                if (usuario == null)
                {
                    Response.Redirect("~/Ingreso.aspx");
                    return;
                }

                var compras = detalleManager.ObtenerUsuarioPorCompra(usuario.Id_Usuario); 
                if (compras != null && compras.Any())
                {
                    dgvMisCompras.DataSource = compras;
                    dgvMisCompras.DataBind();
                }
                else
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "No tienes compras registradas.";
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al cargar las compras: " + ex.Message;
                lblMensaje.CssClass = "text-danger"; 
                lblMensaje.Visible = true;
            }
        }
        public void ReconocerUsuario()
        {
            Usuario usuario = (Usuario)Session["usuarioActual"];

            if (usuario == null)
            {
                
                lblMensaje.Text = "No se encontró al usuario en la sesión.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                Response.Redirect("~/Ingreso.aspx");
            }
            else
            {
             //   lblMensaje.Text = "Usuario encontrado en la sesión: " + usuario.Nombre;
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;
            }
        }

    }
}