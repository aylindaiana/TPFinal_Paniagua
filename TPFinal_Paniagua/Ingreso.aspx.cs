using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua
{
    public partial class Ingreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (username == "test" && password == "1234")
            {
                lblError.Visible = false;
                lblSuccess.Visible = true;
                lblSuccess.Text = "Iniciaste session correctamente!";

                Response.Redirect("/Inicio.aspx");
            }
            else
            {
                lblSuccess.Visible = false;
                lblError.Visible = true;
                lblError.Text = "Usuario o Contraseña invalidos. Porfavor, Intente nuevamente.";
            }
        }
    }
}