using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Informe : System.Web.UI.Page
    {
        ArticuloManager manager = new ArticuloManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack)
            {
                CargarTotales();
                CargarGrids();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Navegacion.aspx");
        }
        protected void dgvArticulosMenores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulosMenores.PageIndex = e.NewPageIndex;
            CargarGrids();
        }
        protected void dgvArticulosMayores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulosMayores.PageIndex = e.NewPageIndex;
            CargarGrids();
        }

        //Funciones:

        private void CargarTotales()
        {
            lblArticulosActivos.Text = manager.CantidadArticulosActivos().ToString();
            lblClientesActivos.Text = manager.CantidadClientesActivos().ToString();
            lblEmpleadosActivos.Text = manager.CantidadEmpleadosActivos().ToString();
            lblTotalAcumulado.Text = manager.ImportePrecioTotal().ToString("C");
        }


        private void CargarGrids()
        {
            dgvArticulosMenores.DataSource = manager.ListarMenor40();
            dgvArticulosMenores.DataBind();

            dgvArticulosMayores.DataSource = manager.ListarMayor50();
            dgvArticulosMayores.DataBind();
        }
        public void chequearUsuarios()
        {
            if (Session["AccesoId"] == null)
            {
                Response.Redirect("/Ingreso.aspx");
            }
            else
            {
                int idAcceso = Convert.ToInt32(Session["AccesoId"]);

                if (idAcceso != 1 && idAcceso != 2)
                {
                    Response.Redirect("/Error.aspx");
                    return;
                }
            }
        }
    }
}