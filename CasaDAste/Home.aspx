﻿<%@ Page
    Title="Home Page"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Home.aspx.cs"
    Inherits="CasaDAste.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="overflow-auto">
        <!-- ciao, fammi una home page di un e-commerce con un form di ricerca -->
        <h1 class="display-3">Benvenuto su Casa d'Aste</h1>
        <h2 class="display-5">Il tuo portale schiavi</h2>
        <div class="d-flex my-3">
            <input type="text" class="form-control me-2" id="txtRicerca" runat="server" placeholder="Cerca il tuo schiavo" />
            <input type="submit" class="p-2 btn btn-primary" value="Cerca" />
        </div>
        
        <!-- adesso facciamo una lista di prodotti -->
        <h3 class="display-5 mt-3">Schiavi in vendita</h3>
        <!-- qui ci va il repeater -->
        <div class="container">
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 manifestoWanted shadow rounded-3">
                                
                                <img
                                    class="mx-auto p-2"
                                    
                                    src='<%# Eval("Img") %>'
                                    alt="Immagine dello Schiavo">
                              
                                <div class="card-body d-flex flex-column justify-content-between text-center">
                                    <h5 class="card-title fw-bolder display-5 "><%# Eval("Nome") %></h5>
                                    <p class="card-text fs-3 fw-semibold"><%# Eval("Prezzo") %> Berries</p>
                                    <p class="card-text fs-3 fw-semibold">Razza: <%# Eval("Razza") %></p>
                                    <div class="d-flex flex-wrap justify-content-between">
                                        <asp:Button ID="Dettaglio" runat="server" Text="Mostra Dettaglio" CssClass="btn btn-light rounded-pill px-3 fw-semibold" CommandArgument='<%# Eval("IDProdott") %>' OnCommand="Dettaglio_Command" />                                                                             
                                        <asp:Button ID="Button1" class="btn btn-success rounded-pill px-3 fw-semibold" runat="server" Text="Aggiungi al carrello" OnClick="Button1_Click" />
                                    </div>
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
