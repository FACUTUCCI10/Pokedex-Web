<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Pokedex_APP._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentplaceholder1" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h1>hola!</h1>
    <p>llegaste a la pokedex web</p>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <%
            foreach (dominio.pokemon poke in listapokemon)
            {

         %>       
            
  <div class="col">
    <div class="card">
      <img src="<%: poke.urlImagen %>" class="card-img-top" alt="...">
      <div class="card-body">
        <h5 class="card-title"><%:poke.nombre %>e</h5>
        <p class="card-text"><%:poke.descripcion %></p>
      </div>
    </div>
  </div>

 <% } %>


 <asp:Repeater runat="server" ID="Repetidor" >
      <ItemTemplate>
     <div class="col">
                <div class="card">
                    <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%#Eval("Nombre") %></h5>
                        <p class="card-text"><%#Eval("Descripcion") %></p>
                        <a href="DetallePokemon.aspx?id=<%#Eval("Id") %>">Ver Detalle</a>
                       
                    </div>
                </div>
            </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>