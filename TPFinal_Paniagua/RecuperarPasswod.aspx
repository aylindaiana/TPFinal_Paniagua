<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarPasswod.aspx.cs" Inherits="TPFinal_Paniagua.RecuperarPasswod" %>
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
            margin-bottom: 20px;
        }

        .form-container {
            max-width: 500px;
            margin: 0 auto;
            padding: 30px;
            background-color: #fff;
            border-radius: 8px;
            border: 1px solid #ffd700;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1.1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            padding: 10px 20px;
            display: block;
            width: 100%;
            text-align: center;
            margin-top: 20px;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .form-control {
            border: 1px solid #ffd700;
            border-radius: 10px;
            padding: 12px;
            margin-bottom: 15px;
            width: 100%;
        }

        .text-danger {
            color: red;
            margin-top: 15px;
            font-weight: bold;
            text-align: center;
        }
    </style>

    <div class="container mt-5">
        <h1>Recuperar Contraseña</h1>
        <div class="form-container">
            
            <asp:Label ID="lblInstrucciones" runat="server" Text="Ingrese su correo electrónico para recibir instrucciones para recuperar su contraseña." CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Correo electrónico" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="Correo electrónico inválido." CssClass="text-danger" Display="Dynamic" />
            
            <asp:Button ID="btnRecuperar" runat="server" CssClass="btn-custom" Text="Recuperar Contraseña" OnClick="btnRecuperar_Click" />
        </div>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>
</asp:Content>
