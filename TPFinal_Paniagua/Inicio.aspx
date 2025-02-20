<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="TPFinal_Paniagua.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }
        
        .ofertas-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        h2 {
            color: #ff7f50;
            text-align: center;
            font-weight: bold;
            font-size: 2rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
        }

        .paginacion {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            border: none;
            padding: 8px 16px;
        }
        .btn-custom:hover {
            background-color: #ff4500;
        }
        
        .row {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }
        .col-3 {
            flex: 0 0 calc(25% - 20px);
        }

        .card {
            border: 2px dashed #ffd700;
            background-color: #fffdf5;
            border-radius: 10px;
            position: relative;
            overflow: hidden;
            transition: transform 0.2s, box-shadow 0.2s;
        }
        .card:hover {
            transform: scale(1.02);
            box-shadow: 0 6px 12px rgba(0,0,0,0.15);
        }

        .badge-oferta {
            background-color: #ff7f50;
            color: white;
            position: absolute;
            top: 10px;
            left: 10px;
            font-size: 0.9rem;
            font-weight: bold;
            padding: 5px 10px;
            border-radius: 20px;
        }

        .card-img-top {
            width: 100%;
            height: auto;
        }

        .card-body {
            text-align: center;
            padding: 10px;
        }
        .card-title {
            color: #ff7f50;
            font-family: 'Comic Sans MS', sans-serif;
            margin-bottom: 5px;
        }
        .precio-anterior {
            text-decoration: line-through;
            color: #999;
            margin: 0;
        }
        .precio-oferta {
            color: #ff4500;
            font-weight: bold;
            font-size: 1.2rem;
            margin: 0;
        }

        .page-info {
            font-weight: bold;
            color: #333;
        }

        .lblMensaje {
            display: block;
            text-align: center;
            margin-bottom: 10px;
        }
        .info-container {
            max-width: 1200px;
            margin: 40px auto;
            padding: 20px;
            display: flex;
            justify-content: space-between;
            text-align: center;
        }

        .info-item {
            flex: 1;
            padding: 15px;
        }

        .info-item i {
            color: #ff7f50;
            margin-bottom: 10px;
        }

        .info-item h5 {
            font-size: 1.2rem;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .info-item p {
            color: #666;
            font-size: 0.9rem;
        }

    </style>

    <div class="ofertas-container">
        <h2>Ofertas Especiales</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="lblMensaje text-danger" Visible="false"></asp:Label>

        <div class="paginacion">
            <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="btn-custom" OnClick="btnAnterior_Click" />
            <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info" />
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn-custom" OnClick="btnSiguiente_Click" />
        </div>

        <div class="row">
            <asp:Repeater ID="repOfertas" runat="server" >
                <ItemTemplate>
                    <div class="col-3">
                        <div class="card">
                            <span class="badge-oferta">SALE</span>
                            
                    <asp:Image ID="imgProducto" runat="server" CssClass="card-img-top" 
                               ImageUrl='<%# Eval("Imagenes[0].UrlImagen") %>' alt="Imagen del producto" />


                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                
                       <!--        <p class="precio-anterior">8999</p>-->
                                
                                <p class="precio-oferta">
                                    <%# String.Format("{0:C}", Eval("Precio")) %>
                                </p>
                                 <asp:Button ID="btnDetalle" runat="server" Text="Ver Detalle" CssClass="btn-custom" 
                                OnClick="btnDetalle_Click" CommandArgument='<%# Eval("Id_Articulo") %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
        <div class="info-container">
        <div class="row">
            <div class="col-md-3 info-item">
                <i class="fa-solid fa-truck fa-2x"></i>
                <h5>Envío a todo Buenos Aires</h5>
                <p>También enviamos a CABA</p>
            </div>

            <div class="col-md-3 info-item">
                <i class="fa-solid fa-box-open fa-2x"></i>
                <h5>Producto Garantizado</h5>
                <p>La calidad que ofrecemos es superior en todo el mercado</p>
            </div>

            <div class="col-md-3 info-item">
                <i class="fa-brands fa-instagram fa-2x"></i>
                <h5>Seguinos en redes</h5>
                <p>Entérate de nuevos productos y descuentos especiales</p>
            </div>

            <div class="col-md-3 info-item">
                <i class="fa-solid fa-credit-card fa-2x"></i>
                <h5>Pagos 100% Seguro</h5>
                <p>Paga con múltiples formas de pago de manera eficaz, rápida y segura</p>
            </div>
        </div>
    </div>



</asp:Content>
