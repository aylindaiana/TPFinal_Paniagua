using Dominio;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class UsuariosGeneral : System.Web.UI.Page
    {
        UsuarioManager manager = new UsuarioManager();
        Usuario usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
            if (!IsPostBack)
            {
                // CargarUsuarios();
                UsuarioManager manager = new UsuarioManager();
                Session.Add("listarUsuarios", manager.Listar());
                dgvUsuarios.DataSource = Session["listarUsuarios"];
                dgvUsuarios.DataBind();
            }
        }
        protected void btnBuscarTodos_Click(object sender, EventArgs e)
        {
            
            List<Usuario> listar = (List<Usuario>)Session["listarUsuarios"];
            List<Usuario> buscarlist = listar.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()) || x.Apellido.ToUpper().Contains(txtBuscar.Text.ToUpper()) );
            dgvUsuarios.DataSource = buscarlist;
            dgvUsuarios.DataBind();
            txtBuscar.Text = string.Empty;
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();

            Response.Redirect("~/Administrador/Config-Usuarios.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Navegacion.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Config-Usuarios.aspx");
        }

        //Funciones:

        public void CargarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            lista = manager.Listar();
            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();

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