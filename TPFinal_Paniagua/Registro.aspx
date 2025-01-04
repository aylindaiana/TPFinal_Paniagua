<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPFinal_Paniagua.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1rem;
            font-weight: bold;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .card {
            border-radius: 20px;
            padding: 20px;
            border: 2px dashed #ffd700;
        }
    </style>
    <div class="container mt-5">
        <div class="text-center">
            <h1 class="text-uppercase" style="font-family: 'Comic Sans MS', sans-serif; color: #ff7f50; font-size: 2.5rem;">Registrarme</h1>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg" style="border: 2px dashed #ffd700;">
                    <div class="card-body" style="background-color: #fffdf5;">
                        <div class="mb-3">
                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" placeholder="Apellido"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Correo Electrónico"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" placeholder="Dirección"></asp:TextBox>
                        </div>
                        <div class="d-grid gap-2">
                            <asp:Button ID="btnRegistrar" CssClass="btn btn-custom" runat="server" Text="create your account" OnClick="btnRegistrar_Click" />
                        </div>
                        <div class="mt-3 text-center">
                            <asp:Label ID="lblMensaje" CssClass="text-danger" runat="server" Text=""></asp:Label>
                            <p class="mt-3">
                                <a href="Ingreso.aspx" class="text-decoration-none" style="color: #ff7f50;">¿Ya sos parte? Ir al Login</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
