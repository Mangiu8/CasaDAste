<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="CasaDAste.Carrello1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rptCarrello" runat="server">
    <ItemTemplate>
        <div class="cart-item">
            <h4><%# Eval("Nome") %></h4>
            <p>Razza: <%# Eval("Razza") %></p>
            <p>Prezzo: <%# Eval("Prezzo", "{0:C}") %></p>
        </div>
    </ItemTemplate>
</asp:Repeater>

        </div>
    </form>
</body>
</html>
