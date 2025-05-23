﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Administrador
{
    public partial class Navegacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool admin = true;

                if (Session["AccesoId"] == null)
                {
                    Response.Redirect("~/Ingreso.aspx");
                }
                int idAcceso = (int)Session["AccesoId"];

                if (idAcceso == 1)
                {
                    btnUsuarios.Visible = admin;
                    btnInforme.Visible = admin;
                }
                if (idAcceso == 2)
                {
                  //  btnInforme.Visible = admin;
                }
            }
        }
        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/UsuariosGeneral.aspx", false);
        }
        protected void btnCategorias_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Categorias.aspx");
        }
        protected void btnTipos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Tipos.aspx");
        }

        protected void btnPrendas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Articulos.aspx");
        }

        protected void btnCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/CompraDetallada.aspx");
        }

        protected void btnInforme_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Informe.aspx");
        }
        protected void btnTalle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Talle.aspx");
        }
    }
}