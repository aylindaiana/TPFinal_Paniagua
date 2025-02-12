<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductosTipo.aspx.cs" Inherits="TPFinal_Paniagua.ProductosTipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        .breadcrumb {
            font-size: 1rem;
            margin: 20px 0;
            padding: 0 20px;
            color: #333;
        }

        .breadcrumb a {
            text-decoration: none;
            color: #ff7f50;
        }

        .breadcrumb a:hover {
            text-decoration: underline;
        }

        .content-container {
            display: flex;
            gap: 20px;
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        .sidebar {
            flex: 0 0 30%; 
            max-width: 30%;
        }

        .categories {
            background-color: #fffdf5;
            border: 2px dashed #ffd700;
            border-radius: 15px;
            padding: 20px;
        }

        .categories h3 {
            color: #ff7f50;
            font-weight: bold;
            font-size: 1.2rem;
            margin-bottom: 15px;
        }

        .categories ul {
            list-style: none;
            padding: 0;
        }

        .categories ul li {
            margin-bottom: 10px;
        }

        .categories ul li a {
            text-decoration: none;
            color: #333;
            font-weight: bold;
        }

        .categories ul li a:hover {
            color: #ff4500;
        }

        .products {
            flex: 1; 
        }

        .products-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
            gap: 20px;
        }

        .product-card {
            background-color: #fffdf5;
            border: 2px dashed #ffd700;
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .product-card:hover {
            transform: scale(1.05);
            box-shadow: 0px 6px 12px rgba(0, 0, 0, 0.2);
        }

        .product-image {
            width: 100%;
            height: auto;
            display: block;
        }

        .product-details {
            padding: 10px;
            text-align: center;
        }

        .product-name {
            font-size: 1rem;
            font-weight: bold;
            color: #ff7f50;
            margin-bottom: 5px;
        }

        .product-price {
            font-size: 0.9rem;
            color: #333;
        }
    </style>

    <div class="breadcrumb">
        <a href="Inicio.aspx">Inicio</a> // <a href="Productos.aspx"> Productos</a>
    </div>


    <div class="content-container">
        <div class="sidebar">
            <div class="categories">
                <h3>Tipos</h3>
                        <ul class="list-group list-group-flush">
                            <asp:Repeater ID="repTipos" runat="server">
                                <ItemTemplate>
                                    <li class="list-group-item">
                                        <asp:LinkButton ID="lnkTipo" runat="server" CssClass="text-decoration-none text-dark fw-semibold"
                                            CommandArgument='<%# Eval("Id_Tipo") %>'
                                            OnClick="filtrarPorTipo_Click">
                                <%# Eval("Nombre") %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
            </div>
        </div>

        <div class="products">
            <div class="products-grid">
                <asp:Repeater ID="repRepetidor" runat="server"  OnItemDataBound="repRepetidor_ItemDataBound">
                    <ItemTemplate>
                        <div class="product-card">
                            <asp:Image ID="imgProducto" runat="server" CssClass="product-image" Visible="false" />
                            <asp:Repeater ID="repImagenes" runat="server">
                                <ItemTemplate>
                                    <img src='<%# Eval("UrlImagen") %>' class="product-image" alt="Imagen del producto" />
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="product-details">
                                <div class="product-name"><%# Eval("Nombre") %></div>
                                <div class="product-price">$ <%# Eval("Precio") %></div>
                                <asp:LinkButton
                                    ID="btnVerDetalle"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    CommandArgument='<%# Eval("Id_Articulo") %>'
                                    OnClick="btnVerDetalle_Click">
                                    COMPRAR
                                </asp:LinkButton>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </div>
</asp:Content>
