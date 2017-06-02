<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iis.aspx.cs" Inherits="U.Tests.Web.Utilities.iis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button runat="server" ID="btnAddDomain" Text="添加域名" />
        <asp:Button runat="server" ID="btnRemoveDomain" Text="移除域名" />
        <asp:Button runat="server" ID="btnRestart" Text="重启应用" />
        <asp:Button runat="server" ID="btnDomainList" Text="域名列表" />
    </div>
    </form>
</body>
</html>
