<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPFinal_Paniagua.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        h1 {
            font-family: 'Comic Sans MS', sans-serif;
            color: #ff7f50;
            font-size: 2.5rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2); 
            font-weight: bold;
            margin-bottom: 20px;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1.1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .card {
            border-radius: 20px;
            padding: 20px;
            border: 2px dashed #ffd700;
            background-color: #fffdf5;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
        }

        .row {
            margin-bottom: 15px; 
        }

        .form-control {
            border: 1px solid #ffd700;
            border-radius: 10px;
            padding: 10px;
        }

        .form-control:focus {
            border-color: #ff7f50;
            box-shadow: 0 0 5px rgba(255, 127, 80, 0.5);
        }

        .text-danger {
            font-size: 0.9rem;
            color: #ff4d4f;
        }

        a.text-decoration-none {
            color: #ff7f50;
            font-weight: bold;
            text-decoration: none;
        }

        a.text-decoration-none:hover {
            color: #ff4500;
            text-decoration: underline;
        }
    </style>

    <div class="container mt-5">
        <div class="text-center">
            <h1 class="text-uppercase" style="font-family: 'Comic Sans MS', sans-serif; color: #ff7f50; font-size: 2.5rem;">EDITAR PERFIL</h1>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg" style="border: 2px dashed #ffd700;">
                    <div class="card-body" style="background-color: #fffdf5;">
                        <div class="mb-3 position-relative" >
                            <asp:TextBox ID="txtId" CssClass="form-control" runat="server" placeholder="Id" ReadOnly="true"></asp:TextBox>
                            <asp:TextBox ID="txtAcceso" CssClass="form-control" runat="server" placeholder="Acceso"></asp:TextBox>

                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre"></asp:TextBox>
                            <asp:Label ID="lblMensaje" CssClass="text-success position-absolute" runat="server" Text="" style="top: 0; right: 0; width: 260px;"></asp:Label>

                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" placeholder="Apellido"></asp:TextBox>
                        </div>
                        <div class="mb-3 ">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Correo Electrónico"></asp:TextBox>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" ></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:TextBox ID="txtLocalidad" CssClass="form-control" runat="server" placeholder="Localidad"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" placeholder="Dirección"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" placeholder="Telefono"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                            </div>
                             <div class="col-md-6 mb-3">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmacionPassword" type="password" placeholder="Confirmar Contraseña"></asp:TextBox>
                            </div>
                        </div>

                        <div class="d-grid gap-2 justify-content-center">
                            <asp:Button ID="btnActualizar" CssClass="btn btn-custom" runat="server" Text="Confirmar la Edición" OnClick="btnActualizar_Click" />
                        </div>
                        <div class="mt-3 text-center">
                            <asp:Button runat="server" CssClass="btn btn-custom" ID="btnCancelar" Text="Cancelar Edición" OnClick="btnCancelar_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
