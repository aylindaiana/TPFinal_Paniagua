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