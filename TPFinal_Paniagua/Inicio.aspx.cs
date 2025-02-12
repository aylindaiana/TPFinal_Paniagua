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
    public partial class Inicio : System.Web.UI.Page
    {
        private List<Articulo> allOfertas;

        private const int PageSize = 4;

        private int CurrentPage
        {
            get { return (int)(ViewState["CurrentPage"] ?? 0); }
            set { ViewState["CurrentPage"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarOfertas();
                //enlaza los datos al Repeater
                BindData();
            }
            else
            {
                allOfertas = (List<Articulo>)ViewState["AllOfertas"];
            }
        }

        private void CargarOfertas()
        {
            ArticuloManager articuloManager = new ArticuloManager();
            allOfertas = articuloManager.ListarArticulosActivos(); 

            ViewState["AllOfertas"] = allOfertas;
        }
        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idArticulo = btn.CommandArgument.ToString();

            Session.Add("ArticuloId", idArticulo);

            Response.Redirect("/Compra/DetalleCompra.aspx");
        }

        private void BindData()
        {
            int totalItems = allOfertas.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            int skip = CurrentPage * PageSize;
            var pagedData = allOfertas.Skip(skip).Take(PageSize).ToList();

            repOfertas.DataSource = pagedData;
            repOfertas.DataBind();

            lblPageInfo.Text = $"{CurrentPage + 1} / {totalPages}";

            btnAnterior.Enabled = CurrentPage > 0;
            btnSiguiente.Enabled = CurrentPage < (totalPages - 1);
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            BindData();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            BindData();
        }


    }
}