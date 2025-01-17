<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPFinal_Paniagua.Productos" %>
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
            flex: 0 0 30%; /* Categorías ocupan el 30% */
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
            flex: 1; /* Ocupa el resto del espacio */
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

    <!-- Breadcrumb -->
    <div class="breadcrumb">
        <a href="Inicio.aspx">Inicio</a> . Productos
    </div>

    <!-- Contenedor Principal -->
    <div class="content-container">
        <!-- Categorías -->
        <div class="sidebar">
            <div class="categories">
                <h3>Categorías</h3>
                <ul>
                    <li><a href="#">Catsuit</a></li>
                    <li><a href="#">Vestidos</a></li>
                    <li><a href="#">Camisetas y Tops</a></li>
                    <li><a href="#">Polleras</a></li>
                    <li><a href="#">Camperas y Buzos</a></li>
                    <li><a href="#">Pantalones</a></li>
                    <li><a href="#">Conjuntos</a></li>
                </ul>
            </div>
        </div>

        <!-- Productos -->
        <div class="products">
            <div class="products-grid">
                <!-- Producto ejemplo -->
                <div class="product-card">
                    <img src="ruta-imagen1.jpg" alt="Conjunto Kira" class="product-image" />
                    <div class="product-details">
                        <div class="product-name">Conjunto Kira</div>
                        <div class="product-price">$37,500.00</div>
                    </div>
                </div>
                <div class="product-card">
                    <img src="ruta-imagen2.jpg" alt="Vestido Lisboa" class="product-image" />
                    <div class="product-details">
                        <div class="product-name">Vestido Lisboa</div>
                        <div class="product-price">$22,500.00</div>
                    </div>
                </div>
                <div class="product-card">
                    <img src="ruta-imagen3.jpg" alt="Catsuit Briana" class="product-image" />
                    <div class="product-details">
                        <div class="product-name">Catsuit Briana</div>
                        <div class="product-price">$23,800.00</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
