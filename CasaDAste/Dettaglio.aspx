<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettaglio.aspx.cs" Inherits="CasaDAste.Dettaglio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card mb-3"">
            <div class="row g-0 manifestoWanted rounded-3 align-items-center">
                <div class="col-md-4 p-3 ">
                    <img id="imgProdotto" runat="server" src="..." class="img-fluid rounded-3" alt="...">
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 id="nomeProdotto" runat="server" class="card-title display-3 fw-semibold"></h5>
                        <p id="descrizioneProdotto" runat="server" class="card-text fw-semibold fs-3"></p>
                        <p id="razzaProdotto" runat="server" class="card-text fw-semibold text-black-50 fs-4"></p>
                        <p id="prezzoProdotto" runat="server" class="card-text fw-semibold fs-3"></p>
                        <div class="d-flex mb-5 align-items-center">
                            <p class="me-3 mb-0">Seleziona la quantita' desiderata </p>
                            <asp:TextBox ID="quantita" class="me-3" TextMode="Number" Min="1" Max="1000" runat="server"></asp:TextBox>
                            <asp:Button ID="AddToCart" runat="server" Text="Aggiungi al carrello" class="btn btn-primary" OnClick="AddToCart_Click"/>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
