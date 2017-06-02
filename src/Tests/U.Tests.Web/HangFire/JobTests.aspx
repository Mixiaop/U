<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobTests.aspx.cs" Inherits="U.Tests.Web.HangFire.JobTests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button runat="server" ID="btnOpen" Text="开启任务" />
    <asp:Button runat="server" ID="btnDelete" Text="删除任务" />
        <asp:HiddenField runat="server" ID="hidJobId" />
    </form>
</body>
</html>
