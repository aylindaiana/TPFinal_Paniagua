using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Talle : System.Web.UI.Page
    {
        TalleManager manager = new TalleManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack)
            {
                TalleManager manager = new TalleManager();
                Session.Add("listarTalles", manager.ListarTodos());
                dgvTalles.DataSource = Session["listarTalles"];
                dgvTalles.DataBind();
            }
        }

        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

            List<Talles> listar = (List<Talles>)Session["listarTalles"];
            List<Talles> buscarlist = listar.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvTalles.DataSource = buscarlist;
            dgvTalles.DataBind();
            txtBuscar.Text = string.Empty;
        }
        protected void dgvTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvTalles.SelectedDataKey.Value.ToString();

            Response.Redirect("~/Administrador/Config-Talle.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Navegacion.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Config-Talle.aspx");
        }
        //Funciones:
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