<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="CasaDAste.Carrello1" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1 class="mb-3">Ecco la tua lista di selezione schiavi😉</h1>
        <asp:Repeater ID="rptCarrello" runat="server">
            <ItemTemplate>
                <div class="cart-item d-flex justify-content-between align-items-center">
                    <img width="100" class="rounded-circle border-2 " src='<%# Eval("Immagine") %>' alt="immagine schaivo" />
                    <h4 class="mx-2"><%# Eval("Nome") %></h4>
                    <div>
                        <p>Razza: <%# Eval("Razza") %></p>
                        <p>Prezzo: <%# Eval("Prezzo", "{0:C}") %></p>
                    </div>
                    <asp:Button ID="btnElimina" runat="server" CommandName="Elimina" CommandArgument='<%# Eval("Nome") %>' Text="Elimina" CssClass="btn btn-danger ms-auto" OnCommand="btnElimina_Command" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <asp:Label ID="lblTotale" runat="server" Text="Totale: 0" />
    </div>
</asp:Content>
