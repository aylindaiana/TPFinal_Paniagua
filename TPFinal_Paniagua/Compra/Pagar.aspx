<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagar.aspx.cs" Inherits="TPFinal_Paniagua.Compra.Pagar" %>
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
            font-size: 2.5rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
            margin-bottom: 20px;
        }

        h4 {
            color: #ff7f50;
            font-weight: bold;
            margin-bottom: 15px;
            text-align: center;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1.1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            padding: 10px 20px;
            margin-top: 20px;
            display: block;
            width: 100%;
            text-align: center;
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
            padding: 12px;
            margin-bottom: 15px;
            width: 100%;
        }

        .form-check-input {
            margin-right: 10px;
            align-content: inherit;
            align-items: flex-start;
        }

        .form-check {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        .radio-container {
            border: 2px solid #ffd700;
            padding: 15px;
            border-radius: 10px;
            background-color: #fffdf5;
            margin-bottom: 15px;
        }

        .d-grid {
            margin-top: 20px;
        }
    </style>

    <div class="container mt-5">
        <h1>Realizar Pago</h1>
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">

                             <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                               
                                <h4>Método de Pago</h4>
                                <div class="radio-container">
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbtnCredito" runat="server" GroupName="Pago"  AutoPostBack="false" OnCheckedChanged="Pago_CheckedChanged" CssClass="form-check-input" />
                                        <label class="form-check-label">Tarjeta de Crédito</label>
                                    </div>
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbtnDebito" runat="server" GroupName="Pago"  AutoPostBack="false" OnCheckedChanged="Pago_CheckedChanged" CssClass="form-check-input" />
                                        <label class="form-check-label">Tarjeta de Débito</label>
                                    </div>
                                </div>
                                <asp:TextBox ID="txtNumeroTarjeta" CssClass="form-control" runat="server" placeholder="Número de tarjeta" MaxLength="16"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revNumeroTarjeta" runat="server" ControlToValidate="txtNumeroTarjeta" ValidationExpression="\d{16}" ErrorMessage="Número inválido" CssClass="text-danger" Display="Dynamic" />
                                
                                <asp:TextBox ID="txtNombreTitular" CssClass="form-control" runat="server" placeholder="Nombre del titular" MaxLength="50"></asp:TextBox>
                                
                                <asp:TextBox ID="txtVencimiento" CssClass="form-control" runat="server" placeholder="Vencimiento (MM/AA)" MaxLength="5"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revVencimiento" runat="server" ControlToValidate="txtVencimiento" ValidationExpression="^(0[1-9]|1[0-2])/[0-9]{2}$" ErrorMessage="Formato inválido (MM/AA)" CssClass="text-danger" Display="Dynamic" />
                                
                                <asp:TextBox ID="txtCVV" CssClass="form-control" runat="server" placeholder="CVV" MaxLength="3"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revCVV" runat="server" ControlToValidate="txtCVV" ValidationExpression="\d{3}" ErrorMessage="CVV inválido" CssClass="text-danger" Display="Dynamic" />
        
                                <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server" placeholder="Documento" MaxLength="8"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI" ValidationExpression="\d{8}" ErrorMessage="Formato de DNI inválido" CssClass="text-danger" Display="Dynamic" />
                            </div>
                            <div class="col-md-6">
                                <h4>Método de Envío</h4>
                                <div class="radio-container">
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbtnDomicilio" runat="server" GroupName="Envio" AutoPostBack="true" OnCheckedChanged="Envio_CheckedChanged" CssClass="form-check-input" />
                                        <label class="form-check-label">Envío a Domicilio</label>
                                    </div>
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbtnAcordar" runat="server" GroupName="Envio" AutoPostBack="true" OnCheckedChanged="Envio_CheckedChanged" CssClass="form-check-input" />
                                        <label class="form-check-label">Acordar con el Vendedor</label>
                                    </div>
                                </div>
                                <div class="form-check">
                                    <asp:CheckBox ID="chkFacturacion" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label">Utilizar mis datos de envío como datos de facturación</label>
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblCodigoPostal" CssClass="form-label" runat="server"
                                        Text="Código Postal" Visible="false"></asp:Label>
                                    <asp:TextBox ID="codigoPostal" runat="server" CssClass="form-control"
                                        Placeholder="Ingrese su código postal" Visible="false" MaxLength="4"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="codigoPostal" ValidationExpression="\d{4}" ErrorMessage="CP inválido" CssClass="text-danger" Display="Dynamic" />
                                </div>

                            </div>

                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnConfirmarPago" CssClass="btn btn-custom" runat="server" Text="Confirmar Pago" OnClick="btnConfirmarPago_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
