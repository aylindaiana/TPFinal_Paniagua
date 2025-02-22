﻿using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            txtId_Articulo.Enabled = false;
            chequearUsuarios();

                if (!IsPostBack)
                {
                    if (ViewState["Imagenes"] == null)
                    {
                        ViewState["Imagenes"] = new List<string>();
                    }

                CategoriaManager categoria = new CategoriaManager();
                    List<Categoria> list = categoria.ListarTodos();
                    ddlCategoria.DataSource = list;
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Id_Categoria";
                    ddlCategoria.DataBind();
   

                    TipoManager tipo = new TipoManager();
                    List<Tipo> lista = tipo.ListarTodos();
                    ddlTipo.DataSource = lista;
                    ddlTipo.DataTextField = "Nombre";
                    ddlTipo.DataValueField = "Id_Tipo";
                    ddlTipo.DataBind();

        }
                
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (!string.IsNullOrEmpty(id) && !IsPostBack)
                {
                    int idArticulo = int.Parse(id);
                    CargarTalles(idArticulo);

                 //   TalleManager talleManager = new TalleManager();
                 //   List<Talles> testTalles = talleManager.ObtenerStockPorTalle(idArticulo);
                  //  lblMensaje.Text += "<br/>Talles encontrados en BD: " + testTalles.Count;
                  //  lblMensaje.Visible = true;


                Articulo articulo = manager.ListarArticulosTodos().Find(x => x.Id_Articulo == int.Parse(id));
                    if (articulo != null)
                    {
                        txtId_Articulo.Text = articulo.Id_Articulo.ToString();
                        txtNombre.Text = articulo.Nombre;
                        txtDescripcion.Text = articulo.Descripcion;
                        txtPrecio.Text = articulo.Precio.ToString();
                        txtStock.Text = articulo.Stock.ToString();
                    
                        ddlCategoria.SelectedValue = articulo.CategoriaId.ToString();
                        ddlTipo.SelectedValue = articulo.TipoId.ToString();
                    //  imgPreview.ImageUrl = articulo.ImagenURL;
                    

                    if (articulo.Imagenes.Count > 0)
                    {
                        imgPreview.ImageUrl = articulo.Imagenes[0].UrlImagen; // Muestra la primera imagen
                    }

                    // txtImagenURL.Text = articulo.ImagenURL;
                    // imgArticulo.ImageUrl = articulo.ImagenURL;
                    ImagenesManager imagenManager = new ImagenesManager();
                        List<Imagenes> imagenes = imagenManager.ListarPorArticulo(articulo.Id_Articulo);
                        ViewState["Imagenes"] = imagenes.Select(img => img.UrlImagen).ToList();
                        CargarListaImagenes();
                    if (!articulo.Estado)
                        {
                            btnDeshabilitar.Text = "Reactivar";
                        }
                    }
                }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            string errores = ValidarFormulario();

            if (!string.IsNullOrEmpty(errores))
            {
                lblMensaje.Text = errores;
                lblMensaje.Visible = true;
                return;
            }
            Articulo articulo = new Articulo();
            try
            {
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
               // articulo.Stock = int.Parse(txtStock.Text);
                articulo.CategoriaId = int.Parse(ddlCategoria.SelectedValue);
                articulo.TipoId = int.Parse(ddlTipo.SelectedValue) ;

                List<Talles> listaTalles = new List<Talles>();
                int stockTotal= 0;

                foreach (RepeaterItem item in rptTalles.Items)
                {
                    HiddenField hfIdTalle = (HiddenField)item.FindControl("hfIdTalle");
                    TextBox txtStock = (TextBox)item.FindControl("txtStockTalle");

                    int idTalle = 0, stock = 0;
                    bool idValido = int.TryParse(hfIdTalle.Value, out idTalle);
                    bool stockValido = int.TryParse(txtStock.Text, out stock);

                   // lblMensaje.Text += $"Talle ID: {idTalle} - Stock: {stock} - Validez: {idValido}/{stockValido}<br/>";

                    if (idValido && stockValido)
                    {
                        listaTalles.Add(new Talles { Id_Talle = idTalle, Stock = stock });
                        stockTotal += stock;
                    }
                    else
                    {
                        stockTotal = 0;
                       // lblMensaje.Text += $"ERROR - Datos inválidos para Talle ID: {hfIdTalle.Value}, Stock: {txtStock.Text}<br/>";
                    }
                }
              //  lblMensaje.Text += $"DEBUG - Stock total calculado: {stockTotal}<br/>";
              //  lblMensaje.Visible = true;

                articulo.Talles = listaTalles;
                articulo.Stock = stockTotal;
                
                /*
                if (listaTalles.Count == 0)
                {
                    lblMensaje.Text += "⚠️ ERROR - No se detectaron talles con stock válido.<br/>";
                    return; 
                }*/
                if (listaTalles.Count == 0)
                {
                    articulo.Stock = 0; // Si no hay talles, stock en 0
                }
                else
                {
                    articulo.Stock = stockTotal; // Si hay talles, usa el stock total calculado
                }

                // articulo.ImagenURL = txtImagenURL.Text;
                if (Request.QueryString["id"] != null)
                {
                    int idArticulo = int.Parse(Request.QueryString["id"]);
                    Articulo articuloExistente = manager.ListarArticulosTodos().Find(x => x.Id_Articulo == idArticulo);

                    if (articuloExistente != null)
                    {
                        articulo.Estado = articuloExistente.Estado;
                    }
                }
                else
                {
                    articulo.Estado = true; 
                }
                //_--------gurado cambios----------

                if (Request.QueryString["id"] != null)
                {

                    articulo.Id_Articulo = int.Parse(txtId_Articulo.Text);
                    manager.Modificar(articulo);

                    lblMensaje.Text = "Su mofificacion se realizó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    ImagenesManager imagenManager = new ImagenesManager();
                    //------------------
                    List<Imagenes> imagenesExistentes = imagenManager.ListarPorArticulo(articulo.Id_Articulo);
                    //--------------------
                    List<string> imagenes = (List<string>)ViewState["Imagenes"];
                    foreach (string url in imagenes)
                    {
                        //  imagenManager.Guardar(new Imagenes { ArticuloId = articulo.Id_Articulo, UrlImagen = url });
                        if (!imagenesExistentes.Any(img => img.UrlImagen == url))
                        {
                            imagenManager.Guardar(new Imagenes { ArticuloId = articulo.Id_Articulo, UrlImagen = url });
                        }

                    }
                    TalleManager talleManager = new TalleManager();
                    foreach (Talles talle in listaTalles)
                    {
                        talleManager.AsociarStockArticuloTalle(articulo.Id_Articulo, talle.Id_Talle, talle.Stock);
                        Debug.WriteLine($"Asociando stock: ArticuloId = {articulo.Id_Articulo}, TalleId = {talle.Id_Talle}, Stock = {talle.Stock}");
                    }

                    Response.Redirect("~/Administrador/Articulos.aspx");
                }
                else
                {
                    // manager.Agregar(articulo);
                    articulo.Id_Articulo = manager.Agregar(articulo);
                    lblMensaje.Text = "Su usuario se agregó exitosamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    if (articulo.Id_Articulo > 0) 
                    {
                        ImagenesManager imagenManager = new ImagenesManager();
                        List<string> imagenes = (List<string>)ViewState["Imagenes"];
                        foreach (string url in imagenes)
                        {
                            imagenManager.Guardar(new Imagenes { ArticuloId = articulo.Id_Articulo, UrlImagen = url });
                        }

                        TalleManager talleManager = new TalleManager();
                        foreach (Talles talle in listaTalles)
                        {
                            talleManager.AsociarStockArticuloTalle(articulo.Id_Articulo, talle.Id_Talle, talle.Stock);
                        }

                        Response.Redirect("~/Administrador/Articulos.aspx");
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void CargarTalles(int idArticulo)
        {
            TalleManager talleManager = new TalleManager();
            List<Talles> talles = talleManager.ObtenerStockPorTalle(idArticulo);

            lblMensaje.Text += "<br/>CargarTalles: " + talles.Count + " talles cargados";

            rptTalles.DataSource = talles;
            rptTalles.DataBind();

            // Verifica que los valores de stock sean correctos
            foreach (var talle in talles)
            {
                Debug.WriteLine($"Talle: {talle.Nombre}, Stock: {talle.Stock}");
            }
        }


        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

            int categoriaId = int.Parse(ddlCategoria.SelectedValue);

            TipoManager tipoManager = new TipoManager();
            List<Tipo> tipos = tipoManager.ObtenerTiposPorCategoria(categoriaId);

            ddlTipo.DataSource = tipos;
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "Id_Tipo";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(0, new ListItem("Selecciona un tipo", "0"));
        }

        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {

                ArticuloManager manager = new ArticuloManager();

                if (btnDeshabilitar.Text == "Reactivar")
                {
                    manager.Reactivar(int.Parse(txtId_Articulo.Text));
                    Response.Redirect("~/Administrador/Articulos.aspx");
                }
                else

                {
                    manager.Desactivar(int.Parse(txtId_Articulo.Text));
                }
                Response.Redirect("~/Administrador/Articulos.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);

                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrador/Articulos.aspx");
        }
        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPreview.ImageUrl = txtImagenURL.Text.Trim();
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtImagenURL.Text))
            {
                List<string> imagenes = (List<string>)ViewState["Imagenes"];
                imagenes.Add(txtImagenURL.Text);

                ViewState["Imagenes"] = imagenes;
                CargarListaImagenes();

                imgPreview.ImageUrl = txtImagenURL.Text.Trim();

                // imgPreview.ImageUrl = txtImagenURL.Text;
                // imgPreview.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
            }
        }
        protected void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedIndex != -1) 
            {
                List<string> imagenes = (List<string>)ViewState["Imagenes"];
                string imagenAEliminar = imagenes[lstImagenes.SelectedIndex];

                ImagenesManager imagenManager = new ImagenesManager();
                imagenManager.Eliminar(imagenAEliminar); 

                imagenes.RemoveAt(lstImagenes.SelectedIndex);
                ViewState["Imagenes"] = imagenes;
                CargarListaImagenes(); 

                if (imagenes.Count > 0)
                {
                    imgPreview.ImageUrl = imagenes[imagenes.Count - 1]; 
                }
                else
                {
                    imgPreview.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"; 
                }
            }
        }


        //Funciones:
        private void CargarListaImagenes()
        {
            lstImagenes.Items.Clear();
            List<string> imagenes = (List<string>)ViewState["Imagenes"];

            foreach (string url in imagenes)
            {
                lstImagenes.Items.Add(new ListItem(url, url)); // Agrega la URL al ListBox
            }

            if (imagenes.Count > 0)
            {
                imgPreview.ImageUrl = imagenes[0]; // Muestra la primera imagen
            }
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

        private void CargarTipo()
        {
            TipoManager manager = new TipoManager();
            List<Tipo> list = manager.ListarTodos();
            ddlTipo.DataSource = list;
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "Id_Tipo";
            ddlTipo.DataBind();
        }
        private void CargarCategoria()
        {
            CategoriaManager manager = new CategoriaManager();
            List<Categoria> list = manager.ListarTodos();
            ddlCategoria.DataSource = list;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id_Cateogria";
            ddlCategoria.DataBind();
        }

        private string ValidarFormulario()
        {
            StringBuilder errores = new StringBuilder();
            decimal precio;
            int stock;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errores.AppendLine("El nombre es obligatorio.<br/>");
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errores.AppendLine("El apellido es obligatorio.<br/>");
            }
         //   if (string.IsNullOrWhiteSpace(txtImagenURL.Text))
           // {
             //   errores.AppendLine("Debe agregar una contraseña.<br/>");
            //}
            if (!decimal.TryParse(txtPrecio.Text, out precio) || precio <= 0)
            {
                errores.AppendLine("El Precio solo debe contener números y debe ser válido. <br/>");
            }

            if (string.IsNullOrEmpty(ddlCategoria.SelectedValue))
            {
                errores.AppendLine("Debe seleccionar una categoría.<br/>");
            }
            if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
            {
                errores.AppendLine("Debe seleccionar un tipo.<br/>");
            }
            return errores.ToString();

        }

    }
}