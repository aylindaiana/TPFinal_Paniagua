using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Paniagua.Compra
{
    public partial class Pagar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí podrías inicializar valores si es necesario
            }
        }

        protected void Pago_CheckedChanged(object sender, EventArgs e)
        {
            txtNumeroTarjeta.Enabled = rbtnCredito.Checked || rbtnDebito.Checked;
            txtNombreTitular.Enabled = txtNumeroTarjeta.Enabled;
            txtVencimiento.Enabled = txtNumeroTarjeta.Enabled;
            txtCVV.Enabled = txtNumeroTarjeta.Enabled;
        }

        protected void Envio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAcordar.Checked)
            {

                lblCodigoPostal.Visible = true;
                codigoPostal.Visible = true;
            }
            else if (rbtnDomicilio.Checked) 
            {
                lblCodigoPostal.Visible = false;
                codigoPostal.Visible = false;
            }
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || txtNumeroTarjeta.Text.Length != 16)
            {
                // Mostrar mensaje de error o manejarlo como prefieras
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreTitular.Text))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVencimiento.Text))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCVV.Text) || txtCVV.Text.Length != 3)
            {
                return;
            }

            // Aquí iría el código para procesar el pago

            Response.Redirect("Confirmacion.aspx"); // Redirigir a una página de confirmación
        }
    }
}