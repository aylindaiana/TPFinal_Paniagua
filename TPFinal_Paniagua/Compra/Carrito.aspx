<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPFinal_Paniagua.Compra.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }

        .carrito-container {
            max-width: 1000px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
            border: 2px dashed #ffd700;
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h1 {
            font-size: 1.8rem;
            color: #333;
            text-align: center;
            margin-bottom: 20px;
        }

        .gridview-container {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .gridview-container th {
            background-color: #ff7f50;
            color: white;
            padding: 10px;
        }

        .gridview-container td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .red {
            color: #ff4500;
            font-weight: bold;
        }

        .btn-action {
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            font-size: 0.9rem;
            color: white;
            border-radius: 5px;
        }

        .btn-add {
            background-color: #ffd700;
        }

        .btn-add:hover {
            background-color: #ffeb3b;
        }

        .btn-remove {
            background-color: #ff7f50;
        }

        .btn-remove:hover {
            background-color: #ff4500;
        }

        .btn-delete {
            background-color: #e74c3c;
        }

        .btn-delete:hover {
            background-color: #c0392b;
        }

        .total {
            font-size: 1.5rem;
            color: #333;
            text-align: right;
        }

        .btn-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .btn-back {
            padding: 10px 15px;
            background-color: #ff7f50;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }

        .btn-back:hover {
            background-color: #ff4500;
        }

        .btn-pay {
            padding: 10px 15px;
            background-color: #28a745;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }

        .btn-pay:hover {
            background-color: #218838;
        }
    </style>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="carrito-container">
        <h1>Carrito de Compras</h1>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
        <asp:GridView ID="dgvCarrito" runat="server" CssClass="gridview-container" AutoGenerateColumns="False" OnRowCommand="dgvCarrito_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id_Articulo" HeaderText="ID" ReadOnly="True" SortExpression="Id"  />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Cantidad Seleccionada">
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" CssClass="quantity-input" Text='<%# Eval("Cantidad") %>' Width="40px" > </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StockMaximo" HeaderText="Cantidad Disponible" ItemStyle-CssClass="red" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Subtotal", "{0:C}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnAdd" runat="server" Text="+" CssClass="btn-action btn-add" CommandName="Aumentar" CommandArgument='<%# Eval("Id_Articulo") %>' />
                        <asp:Button ID="btnRemove" runat="server" Text="-" CssClass="btn-action btn-remove" CommandName="Disminuir" CommandArgument='<%# Eval("Id_Articulo") %>' />
                        <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn-action btn-delete" CommandName="Eliminar" CommandArgument='<%# Eval("Id_Articulo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="total">
             <asp:Label ID="lblTotal" runat="server"></asp:Label>
        </div>
          <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        <div class="btn-container">
            <a href="/Productos.aspx" class="btn-back">Seguir Comprando</a>
            <asp:LinkButton ID="btnPay" runat="server" CssClass="btn-pay" OnClick="btnPay_Click">Pagar</asp:LinkButton>
        </div>
          </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
