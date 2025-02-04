<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="TPFinal_Paniagua.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }
        
        /* Estilo de la sección principal */
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

        /* Botones anterior/siguiente */
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
            position: relative; /* Para ubicar la etiqueta 2x1 */
            overflow: hidden;
            transition: transform 0.2s, box-shadow 0.2s;
        }
        .card:hover {
            transform: scale(1.02);
            box-shadow: 0 6px 12px rgba(0,0,0,0.15);
        }

        /* etiqueta 2x1 o descuento */
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
            <asp:Repeater ID="repOfertas" runat="server">
                <ItemTemplate>
                    <div class="col-3">
                        <div class="card">
                            <span class="badge-oferta">2x1</span>
                            
                            <img src='<%# Eval("ImagenUrl") %>' alt='<%# Eval("Nombre") %>' class="card-img-top" />

                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                
                                <p class="precio-anterior">
                                    8999
                                </p>
                                
                                <p class="precio-oferta">
                                    <%# String.Format("{0:C}", Eval("Precio")) %>
                                </p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>


</asp:Content>
