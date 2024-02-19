<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.cs" Inherits="CasaDAste.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
        <div>
            <!-- ciao, fammi una home page di un e-commerce con un form di ricerca -->
            <h1>Benvenuto su Casa d'Aste</h1>
            <h2>Il tuo portale di aste online</h2>
            <h3>Effettua una ricerca</h3>
            <input type="text" id="txtRicerca" runat="server" />
            <input type="submit" value="Cerca" />
            <!-- adesso facciamo una lista di prodotti -->
            <h3>Prodotti in vendita</h3>
            <!-- qui ci va il repeater -->
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card">
                            <img class="card-img-top" style="width: 200px" src='<%# Eval("Img") %>' alt="Immagine dello Schiavo">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nome") %></h5>
                                <p class="card-text"><%# Eval("Prezzo") %></p> 
                                <p class="card-text"><%# Eval("Razza") %></p> 
                                <asp:Button ID="Button1" runat="server" Text="Aggiungi al carrello" OnClick="Button1_Click" /> 
                                <asp:Button ID="Dettaglio" runat="server" Text="Mostra Dettaglio" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDProdott") %>' OnCommand="Dettaglio_Command" />                                              
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div> 
                </FooterTemplate>
            </asp:Repeater>

        </div>
    
  </asp:Content>

