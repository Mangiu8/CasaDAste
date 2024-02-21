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
                        <div class="d-flex align-items-center">
                            <p class="me-2 card-text fw-semibold fs-3 mb-0">Prezzo: </p>
                            <p id="prezzoProdotto" runat="server" class="card-text fw-semibold fs-3 mb-0"></p>
                            <p class="ms-2 card-text fw-semibold fs-3"> Berries</p>
                        </div>
                        
                        <div class="d-flex mb-5 align-items-center">
                            <p class="me-3 mb-0">Seleziona la quantita' desiderata </p>
                            <asp:TextBox ID="quantita" class="me-3" TextMode="Number" Min="1" Max="1000" runat="server"></asp:TextBox>
                            <asp:Button ID="AddToCart" runat="server" Text="Aggiungi al carrello" class="btn btn-primary" OnClick="AddToCart_Click"/>
                        </div>                       
                    </div>
                </div>
            </div>
        </div>
    <h3 class="display-4 mt-3 mb-2 fw-semibold">Altri schiavi che potrebbero interessarti</h3>
    <div class="row">
     <asp:Repeater ID="Repeater2" runat="server">
      <HeaderTemplate>
      </HeaderTemplate>
      <ItemTemplate>
          <div class="col-md-4 mb-4">
              <div class="card h-100 manifestoWanted shadow rounded-3">                                
                  <img class="mx-auto p-2" src='<%# String.IsNullOrEmpty(Eval("Img") as string) ? "img/default.jpg" : Eval("Img") %>' alt="Immagine dello Schiavo">                              
                  <div class="card-body d-flex flex-column justify-content-between text-center">
                      <h5 class="card-title fw-bolder display-5 "><%# Eval("Nome") %></h5>
                      <p class="card-text fs-3 fw-semibold"><%# string.Format("{0:0.00}", Eval("Prezzo")) %> Berries</p>
                      <p class="card-text fs-3 fw-semibold">Razza: <%# Eval("Razza") %></p>
                      <div class="d-flex flex-wrap justify-content-between">
                        <asp:Button ID="Dettaglio" runat="server" Text="Mostra Dettaglio" CssClass="btn btn-light rounded-pill px-3 fw-semibold" CommandArgument='<%# Eval("IDProdott") %>' OnCommand="Dettaglio_Command" />                                                                             
                        <asp:Button ID="Button1" CssClass="btn btn-success rounded-pill px-3 fw-semibold" runat="server" Text="Aggiungi al carrello" CommandName="AddToCart" CommandArgument='<%# Eval("Nome") + ";" + Eval("Razza") + ";" + Eval("Prezzo") + ";" + Eval("Img") %>' OnCommand="Button1_Command" />
                      </div>
                  </div>
              </div>
          </div>
      </ItemTemplate>
  </asp:Repeater>
        </div>
</asp:Content>
