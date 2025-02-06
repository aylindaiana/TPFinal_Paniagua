<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Navegacion.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Navegacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        .dashboard-container {
            background-color: #fffdf5;
            border: 2px dashed #ffd700;
            border-radius: 20px;
            padding: 40px;
            min-width: 600px; 
            max-width: 800px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
        }

        h1 {
            color: #ff7f50;
            font-size: 2.5rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
            font-weight: bold;
            margin-bottom: 30px;
        }

        .btn-orange {
            background-color: #ff7f50;
            color: white;
            font-size: 1.2rem;
            font-weight: bold;
            border: none;
            border-radius: 20px;
            padding: 10px 20px;
            transition: background-color 0.3s ease, transform 0.2s ease-in-out;
            width: 350px;
            margin: 0 auto;
        }

        .btn-orange:hover {
            background-color: #ff4500;
            transform: scale(1.05);
            cursor: pointer;
        }

        .btn-container {
            display: flex;
            flex-direction: column;
            gap: 15px;
        }
    </style>

    <div class="d-flex justify-content-center align-items-center vh-100">
        <div class="dashboard-container text-center">
            <h1>Tablero para una Gestión Global</h1>
            <div class="btn-container">
                <asp:Button Text="Administrar Usuarios" CssClass="btn btn-orange" ID="btnUsuarios" OnClick="btnUsuarios_Click" runat="server" Visible="false" />
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Button Text="Administrar Categorias" CssClass="btn btn-orange" ID="btnCategorias" OnClick="btnCategorias_Click" runat="server" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <asp:Button Text="Administrar Tipos" CssClass="btn btn-orange" ID="btnTipos" OnClick="btnTipos_Click" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Button Text="Administrar Articulos" CssClass="btn btn-orange" ID="btnPrendas" OnClick="btnPrendas_Click" runat="server" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <asp:Button Text="Administrar Compras" CssClass="btn btn-orange" ID="btnCompra" OnClick="btnCompra_Click" runat="server" />
                    </div>
                </div>
                <asp:Button Text="Informe Completo" CssClass="btn btn-orange" ID="btnInforme" OnClick="btnInforme_Click" runat="server" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
