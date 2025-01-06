<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Navegacion.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Navegacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="admin-dashboard container mt-5 p-4 shadow rounded">
        <h2 class="text-center text-white mb-4">Tablero para gestión de Productos</h2>

        <div class="d-flex flex-column align-items-center gap-3">

            <asp:Button Text="Administrar Usuarios" CssClass="btn admin-button" ID="btnUsuarios" OnClick="btnUsuarios_Click" runat="server" Visible="false" />

            <asp:Button Text="Administrar Categorias" CssClass="btn admin-button" ID="btnCategorias" OnClick="btnCategorias_Click" runat="server" />

            <asp:Button Text="Administrar Tipos" CssClass="btn admin-button" ID="btnTipos" OnClick="btnTipos_Click" runat="server" />

            <asp:Button Text="Administrar Prendas " CssClass="btn admin-button" ID="btnPrendas" OnClick="btnPrendas_Click" runat="server" />

            <asp:Button Text="Administrar Compras" CssClass="btn admin-button" ID="btnCompra" OnClick="btnCompra_Click" runat="server" />

            <asp:Button Text="Informe Completo" CssClass="btn admin-button" ID="btnInforme" OnClick="btnInforme_Click" runat="server" Visible="false" />

        </div>
    </div>
</asp:Content>
