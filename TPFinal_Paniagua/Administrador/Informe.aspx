<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informe.aspx.cs" Inherits="TPFinal_Paniagua.Administrador.Informe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #fffdf5;
            font-family: 'Comic Sans MS', sans-serif;
        }
        
        h2 {
            color: #ff7f50;
            text-align: center;
            font-weight: bold;
            font-size: 2.5rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
        }
        .dashboard-header {
            text-align: center;
            color: #ff7f50;
            font-size: 2.5rem;
            font-weight: bold;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
        }

        .card-container {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin: 20px;
        }

        .card {
            background-color: #ff7f50;
            color: white;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
            width: 200px;
            font-size: 1.2rem;
            font-weight: bold;
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.2);
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

        .grid-container {
            width: 90%;
            margin: auto;
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

        .total-container {
            text-align: right;
            margin: 20px;
            font-size: 1.5rem;
            font-weight: bold;
            color: #ff4500;
        }
    </style>

    <h1 class="dashboard-header">Informe Completo</h1>

    <div class="card-container">
        <div class="card"> Clientes  Activos: 
           << <asp:Label ID="lblClientesActivos" runat="server" /> >>
            </div>
        <div class="card">Empleados Activos: 
          <<  <asp:Label ID="lblEmpleadosActivos" runat="server" /> >>
        </div>
        <div class="card">Productos Activos: 
          <<  <asp:Label ID="lblArticulosActivos" runat="server" /> >>
        </div>
    </div>

    <div class="grid-container">
        <div class="row">
            <div class="col-md-6">
                <h2 class="text-center">Stock menores de 40</h2>
                <asp:GridView ID="dgvArticulosMenores" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" CssClass="table table-striped table-bordered table-hover" GridLines="None" OnPageIndexChanging="dgvArticulosMenores_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id_Articulo" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio Unidad" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-6">
                <h2 class="text-center">Stock mayores de 50</h2>
                <asp:GridView ID="dgvArticulosMayores" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" CssClass="table table-striped table-bordered table-hover" GridLines="None" OnPageIndexChanging="dgvArticulosMayores_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id_Articulo" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio Unidad" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="total-container">
        Total Acumulado: <asp:Label ID="lblTotalAcumulado" runat="server" />
    </div>
    <div class="text-center">
        <asp:Button ID="btnVolver" CssClass="btn btn-custom" runat="server" Text="Volver" OnClick="btnVolver_Click" />
    </div>
</asp:Content>
