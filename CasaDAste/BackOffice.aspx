﻿<%@ Page
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="BackOffice.aspx.cs"
    Inherits="CasaDAste.BackOffice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>BackOffice</h2>
    <asp:Label Text="immagine" runat="server" />
    <asp:TextBox ID="immagine" runat="server" />  
    <asp:TextBox ID="nome" runat="server" />
    <asp:TextBox ID="descrizione" runat="server" />
    <asp:TextBox ID="prezzo" runat="server" />
    <asp:TextBox ID="quantita" runat="server" />
    <asp:TextBox ID="razza" runat="server" />
    <asp:TextBox ID="idPerFavore" runat="server" class="d-none"></asp:TextBox>
   
    <asp:LinkButton ID="BtnUpdate" runat="server" Text="Aggiorna" OnClick="BtnUpdate_Click" CommandArgument='<%# Eval("IDProdott") %>'></asp:LinkButton>
    
    <asp:GridView
        ID="gridViewBackOffice"
        runat="server"
        BorderWidth="1px"
        BorderStyle="Solid"
        BorderColor="Black"
        CellPadding="5"
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="IDProdott" HeaderText="iDProdott" />
            <asp:BoundField DataField="Img" HeaderText="Img" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Descrizione" HeaderText="Descrizione" />
            <asp:BoundField DataField="Prezzo" HeaderText="Prezzo" />
            <asp:BoundField DataField="QuantitaDisponibile" HeaderText="QuantitaDisponibile" />
            <asp:BoundField DataField="Razza" HeaderText="Razza" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton
                        ID="LnkEdit"
                        runat="server"
                        class="btn btn-primary"
                        Text="Modifica"
                        OnClick="LnkEdit_Click"
                        CommandArgument='<%# Eval("IDProdott") %>'>
                    </asp:LinkButton>
                    <asp:LinkButton
                        ID="BtnDelete"
                        runat="server"
                        class="btn btn-danger"
                        Text="Elimina"
                        OnClick="BtnDelete_Click"
                        CommandArgument='<%# Eval("IDProdott") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>


    </asp:GridView>

</asp:Content>
