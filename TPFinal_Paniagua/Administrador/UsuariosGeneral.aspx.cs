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
    public partial class UsuariosGeneral : System.Web.UI.Page
    {
        UsuarioManager manager = new UsuarioManager();
        Usuario usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text;
            List<Usuario> lista = manager.Listar();
            lista = lista.Where(u => u.Nombre.Contains(filtro) || u.Email.Contains(filtro)).ToList();
            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();

        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();

            Response.Redirect("/AdminUsuarios.aspx?id=" + id);

        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministracionGeneral.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminUsuarios.aspx");
        }

        //Funciones:

        public void CargarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            lista = manager.Listar();
            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();

        }       
    }
}