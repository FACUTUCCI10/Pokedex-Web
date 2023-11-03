<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="pokemonlist.aspx.cs" Inherits="Pokedex_APP.pokemonlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentplaceholder1" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h1>lista pokemons</h1>
    <asp:GridView ID="DGVpokemons" CssClass="table" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
        OnSelectedIndexChanged="DGVpokemons_SelectedIndexChanged"
        OnSelectedIndexChanging="DGVpokemons_SelectedIndexChanging" 
        onpageindexchanging="DGVpokemons_PageIndexChanging" AllowPaging="true"  PageSize="5" >
        <Columns>
            <asp:BoundField HeaderText="nombre" DataField="nombre" />
            <asp:BoundField HeaderText="tipo" DataField="tipo.descripcion" />
            <asp:BoundField HeaderText="numero" DataField="numero" />
            <asp:BoundField HeaderText="debilidad" DataField="debilidad.descripcion" />
            <asp:CommandField HeaderText="Accion" ShowSelectButton="true" selectText="✍️​" />
        </Columns>
    </asp:GridView>
    <a href="formularioPokemon.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
