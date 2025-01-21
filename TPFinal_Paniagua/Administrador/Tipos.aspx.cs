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
    public partial class Tipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack)
            {
                TipoManager manager = new TipoManager();
                Session.Add("listarTipos", manager.ListarTodos());
                dgvTipo.DataSource = Session["listarTipos"];
                dgvTipo.DataBind();
            }
        }

        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

            List<Tipo> listar = (List<Tipo>)Session["listarTipos"];
            List<Tipo> buscarlist = listar.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvTipo.DataSource = buscarlist;
            dgvTipo.DataBind();
            txtBuscar.Text = string.Empty;
        }
        protected void dgvTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvTipo.SelectedDataKey.Value.ToString();

            Response.Redirect("~/Administrador/Config-Tipo.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Config-Tipo.aspx");
        }
        //Funciones:
        public void chequearUsuarios()
        {
            if (Session["idRol"] == null)
            {
                Response.Redirect("/Ingreso.aspx");
            }
            else
            {
                int idRol = Convert.ToInt32(Session["idRol"]);

                if (idRol != 1 && idRol != 2)
                {
                    Response.Redirect("/Error.aspx");
                    return;
                }
            }
        }
    }
}