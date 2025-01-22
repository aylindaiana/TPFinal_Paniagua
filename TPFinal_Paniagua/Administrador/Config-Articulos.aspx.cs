using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Config_Articulos : System.Web.UI.Page
    {
        ArticuloManager manager = new ArticuloManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        { 
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        { 
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Articulos.aspx");
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