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
    public partial class Articulos : System.Web.UI.Page
    {
        ArticuloManager manager = new ArticuloManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if(!IsPostBack)
            {
                ArticuloManager manager = new ArticuloManager();
                Session.Add("listarArticulos", manager.ListarArticulosTodos());
                dgvArticulos.DataSource = Session["listarArticulos"];
                dgvArticulos.DataBind();
            }
        }

        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

            List<Articulo> listar = (List<Articulo>)Session["listarArticulos"];
            List<Articulo> buscarlist = listar.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()) || x.Descripcion.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvArticulos.DataSource = buscarlist;
            dgvArticulos.DataBind();
            txtBuscar.Text = string.Empty;
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();

            Response.Redirect("~/Administrador/Config-Articulos.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Config-Articulos.aspx");
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