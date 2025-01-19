<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleCompra.aspx.cs" Inherits="TPFinal_Paniagua.Compra.DetalleCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        .breadcrumb {
            font-size: 0.9rem;
            margin-bottom: 20px;
            color: #333;
        }

        .breadcrumb a {
            text-decoration: none;
            color: #ff7f50;
        }

        .breadcrumb a:hover {
            text-decoration: underline;
        }

        .detalle-container {
            max-width: 900px;
            margin: 0 auto;
            background-color: #ffffff;
            border: 1px solid #ddd;
            border-radius: 15px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            display: flex;
            gap: 20px;
        }

        .images-sidebar {
            flex: 0 0 15%;
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .thumbnail {
            width: 100%;
            border: 2px solid #ffd700;
            border-radius: 10px;
            cursor: pointer;
            transition: transform 0.2s ease, border-color 0.2s ease;
        }

        .thumbnail:hover {
            transform: scale(1.1);
            border-color: #ff7f50;
        }

        .main-image-container {
            flex: 0 0 50%;
            text-align: center;
        }

        .main-image {
            width: 100%;
            max-width: 500px;
            border: 2px solid #ffd700;
            border-radius: 15px;
        }

        .product-details {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 15px;
        }

        .product-title {
            font-size: 2rem;
            color: #ff7f50;
            font-weight: bold;
        }

        .product-price {
            font-size: 2rem;
            color: #ff7f50;
            font-weight: bold;
            text-align: center;
            margin-bottom: 20px;
        }

        .discount-price {
            font-size: 1rem;
            text-decoration: line-through;
            color: #aaa;
            margin-left: 10px;
        }

        .discount-badge {
            font-size: 0.9rem;
            background-color: #ff4500;
            color: #fff;
            padding: 2px 5px;
            border-radius: 5px;
            margin-left: 10px;
        }

        .quantity-selector {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .quantity-selector button {
            background-color: #ffd700;
            border: none;
            font-size: 1rem;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 5px;
        }

        .quantity-selector button:hover {
            background-color: #ffeb3b;
        }

        .btn-container {
            margin-top: 20px;
        }

        .btn-add {
            width: 100%;
            padding: 15px;
            background-color: #ff7f50;
            color: #fff;
            font-size: 1rem;
            font-weight: bold;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-add:hover {
            background-color: #ff4500;
        }

        .btn-back {
            margin-top: 10px;
            text-align: center;
            color: #ff7f50;
            text-decoration: underline;
            cursor: pointer;
        }

        .btn-back:hover {
            color: #ff4500;
        }
    </style>

    <div class="breadcrumb">
        <a href="Inicio.aspx">Inicio</a> // <a href="Productos.aspx">Productos</a> 
    </div>

    <div class="detalle-container">
        <div class="images-sidebar">
            <asp:Repeater ID="repImagenes" runat="server">
                <ItemTemplate>
                    <asp:Image
                        ID="imgThumbnail"
                        runat="server"
                        CssClass="thumbnail"
                        ImageUrl='<%# Eval("ImagenURL") %>'
                        AlternateText='<%# Eval("Nombre") %>'
                        OnClick="thumbnail_Click" />
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <div class="main-image-container">
            <asp:Image
                ID="imgArticulo"
                runat="server"
                CssClass="product-image"
                ImageUrl='<%# string.IsNullOrWhiteSpace(Eval("ImagenURL") as string) ? "https://via.placeholder.com/200" : Eval("ImagenURL") %>'
                AlternateText='<%# Eval("Nombre") %>' />
        </div>

        <div class="product-details">
            <asp:Label ID="lblId" runat="server" CssClass="product-title" > <%# Eval("Id_Articulo") %></asp:Label>

            <asp:Label ID="lblNombre" runat="server" CssClass="product-title" > <%# Eval("Nombre") %></asp:Label>

            <div>
                <asp:Label ID="lblPrecio" runat="server" CssClass="product-price" Text='<%# "Precio: $" + Eval("Precio") %>'></asp:Label>
                <asp:Label ID="lblCategoria" runat="server" CssClass="discount-badge" ><%# Eval("CategoriaId") %></asp:Label>
                <asp:Label ID="lblTipo" runat="server" CssClass="discount-badge" ><%# Eval("TiposId") %></asp:Label>
            </div>

            <asp:Label ID="lblDescripcion" runat="server" CssClass="product-description" ><%# Eval("Descripcion") %></asp:Label>

            <div class="quantity-selector">
                <button type="button" onclick="decreaseQuantity()">-</button>
                <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="quantity-input" Width="30" />
                <button type="button" onclick="increaseQuantity()">+</button>
            </div>

            <div class="btn-container">
                <asp:Button ID="btnAgregarCarrito" runat="server" CssClass="btn-add" Text="Agregar al carrito" OnClick="btnAgregarCarrito_Click" />
            </div>

            <div>
                <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn-back" OnClick="btnVolver_Click">Volver</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
