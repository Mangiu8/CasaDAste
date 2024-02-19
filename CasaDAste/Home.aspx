<%@ Page
    Title="Home Page"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Home.aspx.cs"
    Inherits="CasaDAste.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <!-- ciao, fammi una home page di un e-commerce con un form di ricerca -->
        <h1>Benvenuto su Casa d'Aste</h1>
        <h2>Il tuo portale schiavi</h2>
        <h3>Effettua una ricerca</h3>
        <input type="text" id="txtRicerca" runat="server" />
        <input type="submit" value="Cerca" />
        <!-- adesso facciamo una lista di prodotti -->
        <h3>Prodotti in vendita</h3>
        <!-- qui ci va il repeater -->
        <div class="container">
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 manifestoWanted">
                                <div class="imgWantedContainer">
                                <img
                                    class=""
                                    style="width: 200px"
                                    src='<%# Eval("Img") %>'
                                    alt="Immagine dello Schiavo">
                                </div>
                                <div class="card-body bodyWanted">
                                    <h5 class="card-title fw-bolder fs-3"><%# Eval("Nome") %></h5>
                                    <p class="card-text"><%# Eval("Prezzo") %></p>
                                    <p class="card-text"><%# Eval("Razza") %></p>
                                    <asp:Button ID="Dettaglio" runat="server" Text="Mostra Dettaglio" CssClass="btn btn-primary" CommandArgument='<%# Eval("IDProdott") %>' OnCommand="Dettaglio_Command" />                                                                             
                                    <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="Aggiungi al carrello" OnClick="Button1_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
