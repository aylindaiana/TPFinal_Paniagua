<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="TPFinal_Paniagua.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            background-color: #fffdf5;
            background-image: url('data:image/svg+xml;charset=utf-8,<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="none"><path fill="%23e4e2dc" d="M0 0h40v40H0z"/></svg>');
            background-size: 40px 40px;
        }

        .login-container {
            background-color: #ffffff;
            border-radius: 20px;
            padding: 2rem;
            border: 2px dashed #ffd700;
            max-width: 500px;
            margin: 2rem auto;
            text-align: center;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .login-logo {
            font-family: 'Arial', sans-serif;
            font-size: 2rem;
            font-weight: bold;
            color: #ff7f50;
            margin-bottom: 1rem;
            text-shadow: 1px 1px 0 #ffd700;
        }

        .login-input {
            border-radius: 15px;
            border: 2px solid #ffd700;
            padding: 0.5rem;
            font-size: 1rem;
            margin-bottom: 1rem;
        }

        .login-btn {
            background-color: #ff7f50;
            color: white;
            border-radius: 20px;
            padding: 0.5rem 1.5rem;
            border: none;
            font-size: 1rem;
            font-weight: bold;
            cursor: pointer;
        }

        .login-btn:hover {
            background-color: #ff4500;
        }

        .login-links {
            font-size: 0.9rem;
            margin-top: 1rem;
        }

        .login-links a {
            color: #ff7f50;
            text-decoration: none;
        }

        .login-links a:hover {
            text-decoration: underline;
        }

        .success-message {
            color: green;
            font-weight: bold;
            margin-top: 1rem;
        }

        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 1rem;
        }
    </style>

    <div class="login-container">
        <div class="login-logo">ROSE VIBES</div>


        <asp:Label ID="lblSuccess" runat="server" CssClass="success-message" Text="Account successfully created. Please log in." Visible="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" CssClass="error-message" Text="Invalid username or password. Please try again." Visible="false"></asp:Label>


        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control login-input" Placeholder="Usuario"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control login-input" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
        <div class="login-links">
            <a href="~/RecuperarPassword.aspx">¿Olvidaste la contraseña?</a> 
        </div> <br />
        <asp:Button ID="btnIngresar" runat="server" CssClass="login-btn" Text="Ingresar" OnClick="btnIngresar_Click" />

        <div class="login-links">
            <a href="~/Registro.aspx">¿No estás registrado? Registrate acá.</a>
        </div>
    </div>
</asp:Content>
