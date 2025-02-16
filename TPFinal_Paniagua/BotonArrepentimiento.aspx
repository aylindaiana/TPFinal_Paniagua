<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BotonArrepentimiento.aspx.cs" Inherits="TPFinal_Paniagua.BotonArrepentimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        h1 {
            color: #ff7f50;
            text-align: center;
            font-weight: bold;
            font-size: 2rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
            margin-bottom: 20px;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1.1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            padding: 10px 20px;
            width: 100%;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .card {
            border-radius: 10px;
            padding: 20px;
            border: 2px solid #ffd700;
            background-color: #fffdf5;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .form-control {
            border: 1px solid #ffd700;
            border-radius: 10px;
            padding: 10px;
            margin-bottom: 10px;
        }
    </style>

    <div class="container mt-5">
        <h1>Solicitud: Cancelación de Compra</h1>
        <p class="text-center">Por favor, completa el siguiente formulario para solicitar la cancelación de tu compra.</p>
        <div class="row justify-content-center">
                    <p class="text-center text-secondary">
            La solicitud tendrá validez si se realiza dentro de los plazos determinados en la 
            <a href="https://www.boletinoficial.gob.ar/detalleAviso/primera/227839/20200408" target="_blank">Resolución 424/2020</a> 
            y no se trate de productos exceptuados.
        </p>
            <div class="col-md-8">
                <div class="card">
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre completo" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Email (con el que se realizó la compra)" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" placeholder="Teléfono" MaxLength="15"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtNumeroOrden" CssClass="form-control" runat="server" placeholder="Número de Factura" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:TextBox ID="txtRazones" CssClass="form-control" runat="server" placeholder="Razón de la devolucion" MaxLength="300"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtObs" CssClass="form-control" runat="server" placeholder="Otras Observaciones" MaxLength="400"></asp:TextBox>
                        </div>

                    </div>
                        <div class="d-grid">
                            <asp:Button ID="btnEnviar" CssClass="btn btn-custom" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
