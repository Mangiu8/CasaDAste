<%@ Page
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="BackOffice.aspx.cs"
    Inherits="CasaDAste.BackOffice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>BackOffice</h2>
    <asp:Label ID="Label1" runat="server" Text="Benvenuto nel tuo backoffice" />
    <br />
    <asp:Label ID="Label2" runat="server"  />
    <div id="modifyingFields" class="d-flex flex-column">
        <asp:Label ID="Labelimg" Text="Aggiungi l'immagine del tuo schiavo" runat="server" />
        <asp:TextBox ID="immagine" runat="server"  />
        <asp:Label ID="Labelnome" Text="Aggiungi il nome del tuo schiavo" runat="server" />
        <asp:TextBox ID="nome" runat="server" />
        <asp:Label ID="Labelprezzo" Text="Aggiungi il suo prezzo" runat="server" />
        <asp:TextBox ID="prezzo" runat="server"/>
        <asp:Label ID="Labelqnt" Text="Aggiungi la quantita" runat="server" />
        <asp:TextBox ID="quantita" runat="server" />
        <asp:Label ID="Labelrazza" Text="Aggiungi la sua razza" runat="server" />
        <asp:TextBox ID="razza" runat="server" />
        <asp:TextBox ID="idPerFavore" runat="server" class="d-none"></asp:TextBox>
        <asp:Label  ID="Labeldescrizione" Text="Aggiungi la descrizione del tuo schiavo" runat="server" />
        <Textarea ID="descrizione" runat="server" />
        <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-success my-4" runat="server" Text="Aggiungi" OnClick="BtnUpdate_Click" CommandArgument='<%# Eval("IDProdott") %>'></asp:LinkButton>
    </div>

    <asp:GridView
        ID="gridViewBackOffice"
        runat="server"
        BorderWidth="1px"
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
                        class="btn btn-primary backButton"
                        Text="Modifica"
                        OnClick="LnkEdit_Click"
                        CommandArgument='<%# Eval("IDProdott") %>'>
                    </asp:LinkButton>
                    <asp:LinkButton
                        ID="BtnDelete"
                        runat="server"
                        class="btn btn-danger backButton"
                        Text="Elimina"
                        OnClick="BtnDelete_Click"
                        CommandArgument='<%# Eval("IDProdott") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>


    </asp:GridView>

</asp:Content>
