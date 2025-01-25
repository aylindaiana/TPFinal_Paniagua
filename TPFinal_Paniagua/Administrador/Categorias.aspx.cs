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
    public partial class Categorias : System.Web.UI.Page
    {
        CategoriaManager manager = new CategoriaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack)
            {
                CategoriaManager manager = new CategoriaManager();
                Session.Add("listarCategorias", manager.ListarTodos());
                dgvCategoria.DataSource = Session["listarCategorias"];
                dgvCategoria.DataBind();
            }
        }

        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {

            List<Categoria> listar = (List<Categoria>)Session["listarCategorias"];
            List<Categoria> buscarlist = listar.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvCategoria.DataSource = buscarlist;
            dgvCategoria.DataBind();
            txtBuscar.Text = string.Empty;
        }
        protected void dgvCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategoria.SelectedDataKey.Value.ToString();

            Response.Redirect("~/Administrador/Config-Categoria.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Navegacion.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Config-Categoria.aspx");
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