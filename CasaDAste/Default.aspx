<%@ Page
    Title="Home Page"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="CasaDAste._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="LoginDiv">
        <div id="Container">
            <h1>Benvenuto su Casa D'Aste!</h1>
            <div>
                <h3>Qui trovi i migliori schiavi sul mercato</h3>
                <p>Accedi e dai un occhiatta alle nostre offerte</p>
            </div>
            <div id="FormDiv">
                <asp:TextBox CssClass="formLog" ID="TextBox1" runat="server" placeholder="Username"></asp:TextBox>

                <asp:TextBox CssClass="formLog" ID="TextBox2" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>

                <asp:Button CssClass="btn btn-info border-1 BOTTONE" ID="Button1" runat="server" Text="Accedi" OnClick="Button1_Click" />
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
