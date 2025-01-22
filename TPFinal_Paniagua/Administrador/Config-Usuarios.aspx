<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Config-Usuarios.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Config_Usuarios" %>
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
        }

        .form-container {
            width: 90%;
            max-width: 600px;
            margin: 0 auto;
            background-color: #fff3e0;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
        }

        .form-container input, 
        .form-container select {
            border: 1px solid #ffd700;
            border-radius: 10px;
            padding: 10px 15px;
            width: 100%;
            margin-bottom: 15px;
        }

        .form-container input:focus,
        .form-container select:focus {
            outline: none;
            border-color: #ff7f50;
            box-shadow: 0 0 5px rgba(255, 127, 80, 0.5);
        }

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 10px;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            padding: 10px 15px;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .text-danger {
            margin-top: 15px;
            font-weight: bold;
            text-align: center;
        }
    </style>

    <h1>Editar Usuario</h1>

    <div class="form-container">
        <div class="row">
            <div class="mb-4">
                <asp:TextBox runat="server" ID="txtId_Usuario" CssClass="form-control" placeholder="ID" ReadOnly="true" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Nombre" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" placeholder="Apellido" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Correo" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Contraseña" />           

            </div>
        </div>
        <div class="row">
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" placeholder="Teléfono" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" placeholder="Dirección" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" ID="txtLocalidad" CssClass="form-control" placeholder="Localidad" />
            </div>
            <div class="col-md-6 mb-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" ></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3">
                <label for="ddlAcceso" class="form-label">Seleccionar el Acceso que tendrá: </label>
            </div>
            <div class="col-md-6 mb-3">
                <asp:DropDownList ID="ddlAcceso" CssClass="form-control" runat="server"> </asp:DropDownList>
            </div>
        </div>        
          
        <div class="btn-container">
            <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-custom" OnClick="btnAceptar_Click" runat="server" />
            <asp:Button Text="Cancelar" ID="btnCancelar" CssClass="btn btn-custom" OnClick="btnCancelar_Click" runat="server" />
            <asp:Button Text="Deshabilitar" ID="btnDeshabilitar" CssClass="btn btn-custom" OnClick="btnDeshabilitar_Click" runat="server" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>
</asp:Content>
