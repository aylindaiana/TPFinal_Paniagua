<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="confirmacion.aspx.cs" Inherits="TPFinal_Paniagua.confirmacion" %>
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

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 10px;
        }

        .btn-custom {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            font-size: 1rem;
            font-weight: bold;
            transition: background-color 0.3s ease;
            padding: 10px 15px;
        }

        .btn-custom:hover {
            background-color: #ff4500;
        }

        .text-danger {
            margin-top: 15px;
            font-weight: bold;
            text-align: center;
        }
    </style>
    <h1>
        ¡Muchas gracias tu compra se realizó con exito!</h1>
        <div class="btn-container">
        <asp:Button Text="Volver al Inicio" ID="btnInicio" CssClass="btn btn-custom" OnClick="btnInicio_Click" runat="server" />
    </div>
</asp:Content>
