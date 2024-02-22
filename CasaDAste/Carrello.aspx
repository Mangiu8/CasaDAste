<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="CasaDAste.Carrello1" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1 class="mb-4">Ecco la tua lista di selezione schiavi😉</h1>
        <asp:Repeater ID="rptCarrello" runat="server">
            <ItemTemplate>
                <div class="d-flex justify-content-between align-items-center mb-2 p-3 rounded-4 bg-cart">
                    <img width="100" height="100" class="rounded-circle border-2 " src='<%# Eval("Immagine") %>' alt="immagine schaivo" />                    
                    <div class="mx-4 my-2">
                        <h4><%# Eval("Nome") %></h4>
                        <p class="mb-0">Razza: <%# Eval("Razza") %></p>
                        <p class="mb-0">Prezzo: <%# Eval("Prezzo") %> Berries</p>
                    </div>
                    <asp:Button ID="btnElimina" runat="server" CommandName="Elimina" CommandArgument='<%# Eval("Nome") %>' Text="Elimina" CssClass="btn btn-danger ms-auto" OnCommand="btnElimina_Command" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <div class="text-end">
            <asp:Label CssClass="display-4" ID="lblTotale" runat="server" Text="Totale: 0" />
        </div>        
    </div>
</asp:Content>
