<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Config-Articulos.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Config_Articulos" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Editar Articulo</h1>

<div class="form-container">
    <div class="row">
        <div class="col-md-6 mb-3">
            <asp:TextBox runat="server" ID="txtId_Articulo" CssClass="form-control" placeholder="ID" ReadOnly="true" />
            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Nombre" />
            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Descripcion" />
            <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" placeholder="Precio" />
        </div>

        <div class="col-md-6 mb-3">
            <asp:TextBox runat="server" ID="txtStock" CssClass="form-control" placeholder="Stock" />
            <label for="ddlCategoria" class="form-label">Categoria: </label>
            <asp:DropDownList ID="ddlCategoria" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>
            <label for="ddlCategoria" class="form-label">Tipo: </label>
            <asp:DropDownList ID="ddlTipo" CssClass="form-control" runat="server"></asp:DropDownList>

        </div>


        <div class="col-md-6 mb-3">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenURL" class="form-label">Url de Imagen</label>
                        <asp:TextBox ID="txtImagenURL" runat="server" AutoPostBack="true"  CssClass="form-control" OnTextChanged="txtImagenUrl_TextChanged"></asp:TextBox>

                        <asp:Button runat="server" ID="btnAgregarImagen" Text="Agregar Imagen" CssClass="btn btn-custom" OnClick="btnAgregarImagen_Click" />
                    </div>

                    <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                        runat="server" ID="imgPreview" Width="100%" />

                    <asp:ListBox ID="lstImagenes" runat="server" CssClass="form-control" Height="100px"></asp:ListBox>
                    <asp:Button runat="server" ID="btnEliminarImagen" Text="Eliminar Imagen" CssClass="btn btn-custom mt-2" OnClick="btnEliminarImagen_Click" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>

    <div class="btn-container">
        <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-custom" OnClick="btnAceptar_Click" runat="server" />
        <asp:Button Text="Cancelar" ID="btnCancelar" CssClass="btn btn-custom" OnClick="btnCancelar_Click" runat="server" />
        <asp:Button Text="Deshabilitar" ID="btnDeshabilitar" CssClass="btn btn-custom" OnClick="btnDeshabilitar_Click" runat="server" />
    </div>

    
</div>

</asp:Content>
