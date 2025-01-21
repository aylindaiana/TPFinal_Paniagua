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

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        table thead {
            background-color: #ff7f50;
            color: white;
        }

        table th, table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        table td.red {
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

    <div class="carrito-container">
        <h1>Carrito de Compras</h1>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Precio</th>
                    <th>Cantidad Seleccionada</th>
                    <th>Cantidad Disponible</th>
                    <th>Subtotal</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCarrito" runat="server" OnItemCommand="repCarrito_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Id_Articulo") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td>$ <%# Eval("Precio", "{0:N2}") %></td>
                            <td>
                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="quantity-input" Text='<%# Eval("Cantidad") %>' Width="30px" />
                            </td>
                            <td class="red"><%# Eval("CantidadDisponible") %></td>
                            <td>$ <%# Eval("Subtotal", "{0:N2}") %></td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="+" CssClass="btn-action btn-add" CommandArgument='<%# Eval("Id") %>'  />
                                <asp:Button ID="btnRemove" runat="server" Text="-" CssClass="btn-action btn-remove" CommandArgument='<%# Eval("Id") %>'  />
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn-action btn-delete" CommandArgument='<%# Eval("Id") %>'  />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div class="total">
            Total: $<asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
        </div>
        <div class="btn-container">
            <a href="/Productos.aspx" class="btn-back">Seguir Comprando</a>
            <asp:LinkButton ID="btnPay" runat="server" CssClass="btn-pay" OnClick="btnPay_Click">Pagar</asp:LinkButton>
        </div>
    </div>
</asp:Content>
