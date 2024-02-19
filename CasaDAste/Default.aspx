<%@ Page 
    Title="Home Page" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" 
    Inherits="CasaDAste._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="TextBox1" runat="server" placeholder="Username"></asp:TextBox>
   
    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
       

    <asp:Button ID="Button1" runat="server" Text="Accedi" OnClick="Button1_Click" />
    <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>
