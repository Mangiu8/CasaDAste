<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettaglio.aspx.cs" Inherits="CasaDAste.Dettaglio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card mb-3"">
            <div class="row g-0">
                <div class="col-md-4">
                    <img id="imgProdotto" runat="server" src="..." class="img-fluid rounded-start" alt="...">
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 id="nomeProdotto" runat="server" class="card-title"></h5>
                        <p id="descrizioneProdotto" runat="server" class="card-text"></p>
                        <p id="razzaProdotto" runat="server" class="card-text"></p>
                        <p id="prezzoProdotto" runat="server" class="card-text"></p>
                        <p>Seleziona la quantita' desiderata </p>
                        <asp:TextBox ID="quantita" TextMode="Number" Min="1" Max="1000" runat="server"></asp:TextBox>
                       
                        <br />
                        <asp:Button ID="AddToCart" runat="server" Text="Aggiungi al carrello" class="btn btn-primary" OnClick="AddToCart_Click"/>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
