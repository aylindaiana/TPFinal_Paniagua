<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="TPFinal_Paniagua.CambiarPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <style>
       body {
           background-color: #fffdf5;
           font-family: 'Comic Sans MS', sans-serif;
           margin: 0;
           height: 100vh;
       }

       h1 {
           font-family: 'Comic Sans MS', sans-serif;
           color: #ff7f50;
           font-size: 2.5rem;
           text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
           font-weight: bold;
           margin-bottom: 20px;
       }

       .btn-custom {
           background-color: #ff7f50;
           color: white;
           border-radius: 20px;
           font-size: 1.1rem;
           font-weight: bold;
           transition: background-color 0.3s ease;
       }

       .btn-custom:hover {
           background-color: #ff4500;
       }

       .card {
           border-radius: 20px;
           padding: 20px;
           border: 2px dashed #ffd700;
           background-color: #fffdf5;
           box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
           display: flex;
           justify-content: center;
           align-items: center;
       }

       .form-control {
           border: 1px solid #ffd700;
           border-radius: 10px;
           padding: 10px;
       }

       .form-control:focus {
           border-color: #ff7f50;
           box-shadow: 0 0 5px rgba(255, 127, 80, 0.5);
       }

       .text-danger {
           font-size: 0.9rem;
           color: #ff4d4f;
       }
   </style>

   <div class="container mt-5">
       <div class="text-center">
           <h1 class="text-uppercase" style="color: #ff7f50;">Cambiar Contraseña</h1>
       </div>
       <div class="row justify-content-center">
           <div class="col-md-6">
               <div class="card shadow-lg">
                   <div class="card-body">
                       <div class="mb-3">
                           <asp:Label runat="server" AssociatedControlID="txtPasswordActual" CssClass="form-label" Text="Contraseña Actual:" />
                           <asp:TextBox ID="txtPasswordActual" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña Actual"></asp:TextBox>
                       </div>
                       <div class="mb-3">
                           <asp:Label runat="server" AssociatedControlID="txtNuevaPassword" CssClass="form-label" Text="Nueva Contraseña:" />
                           <asp:TextBox ID="txtNuevaPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Nueva Contraseña"></asp:TextBox>
                       </div>
                       <div class="mb-3">
                           <asp:Label runat="server" AssociatedControlID="txtConfirmarPassword" CssClass="form-label" Text="Confirmar Nueva Contraseña:" />
                           <asp:TextBox ID="txtConfirmarPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Confirmar Contraseña"></asp:TextBox>
                       </div>
                       <div class="d-grid gap-2 justify-content-center">
                           <asp:Button ID="btnCambiar" CssClass="btn btn-custom" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiar_Click" />
                       </div>
                       <div class="mt-3 text-center">
                           <asp:Label ID="lblMensaje" CssClass="text-danger" runat="server" Text=""></asp:Label>
                       </div>
                   </div>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
