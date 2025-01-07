using Dominio;
using Manager;
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
            if (Request.QueryString["logout"] == "true")
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("~/Ingreso.aspx");
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioManager manager = new UsuarioManager();

            try
            {
                usuario.Email = txtUsername.Text;
                usuario.Pass = txtPassword.Text;
                if (manager.Login(usuario))
                {
                    if(!usuario.Estado)
                    {
                        lblError.Text = "Su usuario se encuentra inactivo.";
                    }
                    if (usuario != null && usuario.IdAcceso > 0)
                    {
                            Session["usuario"] = usuario;
                            Session["AccesoId"] = usuario.IdAcceso;
                            

                            lblError.Visible = false;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "Iniciaste session correctamente!";

                            Response.Redirect("Inicio.aspx", false);
                    }
                    else
                    {
                         lblError.Text = "Error al iniciar sesión. Usuario no válido.";
                    }
                } 
                else {
                    lblSuccess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Usuario o Contraseña invalidos. Porfavor, Intente nuevamente.";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

    }
}