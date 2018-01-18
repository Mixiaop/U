<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedisCachingTests.aspx.cs" Inherits="U.Tests.Web.Caching.RedisCachingTests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnGet" Text="获取用户" />
    <asp:Button runat="server" ID="btnUpdate" Text="更新用户" />
    </div>
    </form>
</body>
</html>
