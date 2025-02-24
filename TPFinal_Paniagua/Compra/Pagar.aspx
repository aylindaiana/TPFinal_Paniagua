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
                            <div class="col-md-4">

                             <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                               
                                <h4>Método de Pago</h4>
                                <div class="radio-container">
                                        <div class="form-check">
                                            <asp:RadioButton ID="rbtnTransferencia" runat="server" GroupName="Pago" AutoPostBack="true" OnCheckedChanged="Pago_CheckedChanged" CssClass="form-check-input" />
                                            <label class="form-check-label">Transferencia</label>
                                        </div>
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbtnEfectivo" runat="server" GroupName="Pago" AutoPostBack="true" OnCheckedChanged="Pago_CheckedChanged" CssClass="form-check-input" />
                                        <label class="form-check-label">Efectivo</label>
                                    </div>
                                </div>

                                <div id="divEfectivo" runat="server" visible="false" class="text-center mt-3">
                                    <img src="/Img/pagofacil.PNG" alt="Pago Fácil" class="img-fluid" width="150" />
                                    <img src="/Img/rapipago.PNG" alt="Rapipago" class="img-fluid" width="150" />
                                </div>

                                <div id="divTransferencia" runat="server" visible="false">
                                    <asp:TextBox ID="txtAlias" CssClass="form-control" runat="server" placeholder="Alias" MaxLength="50"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevAlias" runat="server" ControlToValidate="txtAlias" ValidationExpression="^[a-zA-Z0-9._-]{3,50}$" ErrorMessage="Alias Inválido (solo letras, números, puntos, guiones, entre 3 y 50 caracteres)" CssClass="text-danger" Display="Dynamic" />

                                    <asp:TextBox ID="txtCBU" CssClass="form-control" runat="server" placeholder="CBU" MaxLength="22"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevCBU" runat="server" ControlToValidate="txtCBU" ValidationExpression="^\d{22}$" ErrorMessage="CBU debe contener exactamente 22 dígitos" CssClass="text-danger" Display="Dynamic" />

                                    <asp:TextBox ID="txtNombreCompleto" CssClass="form-control" runat="server" placeholder="Nombre Completo" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevNombreCompleto" runat="server" ControlToValidate="txtNombreCompleto" ValidationExpression="^[a-zA-ZÀ-ÿ\s]{3,100}$" ErrorMessage="Nombre Inválido (solo letras y espacios, entre 3 y 100 caracteres)" CssClass="text-danger" Display="Dynamic" />

                                    <h5>Subir Comprobante de Transferencia</h5>
                                    <asp:FileUpload ID="fuComprobante" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
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
                                    
                                    <asp:Label ID="lblEnvioDomicilio" CssClass="form-label" runat="server"
    Text="Código Postal" Visible="false">Se utilizaran sus datos de envío como datos de facturación. En caso de querer cambiar el domicilio de entrega, cambie sus datos antes de realizar la compra.</asp:Label>
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblCodigoPostal" CssClass="form-label" runat="server"
                                        Text="Código Postal" Visible="false"></asp:Label>
                                    <asp:TextBox ID="codigoPostal" runat="server" CssClass="form-control"
                                        Placeholder="Ingrese su código postal" Visible="false" MaxLength="4"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="codigoPostal" ValidationExpression="\d{4}" ErrorMessage="CP inválido" CssClass="text-danger" Display="Dynamic" />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="card">
                                    <div class="card-body">
                                        <h4>Resumen de la Compra</h4>
                                        <ul class="list-group">
                                            <asp:Repeater ID="rptResumenCompra" runat="server">
                                                <ItemTemplate>
                                                    <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        <%# Eval("Nombre") %>
                                                        <span class="badge bg-primary rounded-pill">x <%# Eval("Cantidad") %></span>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <hr>
                                        <h5 class="text-end">Total: <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"></asp:Label></h5>
                                    </div>
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
