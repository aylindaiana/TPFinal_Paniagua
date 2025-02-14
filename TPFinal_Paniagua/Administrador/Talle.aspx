<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Talle.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Talle" %>
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

        .search-box {
            margin: 20px auto;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .search-box input {
            border: 1px solid #ffd700;
            border-radius: 20px;
            padding: 8px 12px;
            width: 300px;
        }

        .search-box input:focus {
            outline: none;
            border-color: #ff7f50;
            box-shadow: 0 0 5px rgba(255, 127, 80, 0.5);
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            margin: 10px 5px;
            padding: 10px 15px;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .table {
            width: 90%;
            margin: 20px auto;
            border-collapse: collapse;
            text-align: left;
        }

        .table th {
            background-color: #ff7f50;
            color: white;
            text-align: center;
            padding: 10px;
        }

        .table td {
            padding: 10px;
            border: 1px solid #ffd700;
        }

        .table-striped tr:nth-child(even) {
            background-color: #fff3e0;
        }

        .table-striped tr:hover {
            background-color: #ffe5d0;
        }

        .text-center {
            text-align: center;
        }

        .command-button {
            font-size: 1.2rem;
            color: #fff;
            background-color: #ff7f50;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
        }

        .command-button:hover {
            background-color: #ff4500;
        }

        .command-button i {
            font-size: 1rem;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>ADMINISTRAR TALLES</h1>

    <div class="search-box">
        <asp:TextBox ID="txtBuscar" CssClass="form-control" runat="server" placeholder="Buscador"  ></asp:TextBox>
        <asp:Button ID="btnBuscarTodos" CssClass="btn btn-custom" runat="server" Text="Buscar" OnClick="btnBuscarTodos_Click" />
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="dgvTalles" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Talle" CssClass="table table-striped table-bordered table-hover" OnSelectedIndexChanged="dgvTalle_SelectedIndexChanged" HeaderStyle-CssClass="table-dark" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Id_Talle" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:CheckBoxField HeaderText="Estado" DataField="Estado" SortExpression="Estado" />
                    <asp:TemplateField>
                        <ItemTemplate>

                        <asp:ImageButton ID="btnEditar" runat="server" CssClass="command-button" ImageUrl="https://cdn.jsdelivr.net/npm/bootstrap-icons/icons/pencil.svg" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="text-center">
        <asp:Button ID="btnAgregar" CssClass="btn btn-custom" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnVolver" CssClass="btn btn-custom" runat="server" Text="Volver" OnClick="btnVolver_Click" />
    </div>
</asp:Content>
