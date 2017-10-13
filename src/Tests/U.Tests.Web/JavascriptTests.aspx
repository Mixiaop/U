<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavascriptTests.aspx.cs" Inherits="U.Tests.Web.JavascriptTests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script src="http://staticv2.youzy.cn/lib-js/jquery/jquery1.7.2.min.js"></script>
    <script>
        var service = {};
        service.do = function () {
            var def = $.Deferred();
            setTimeout(function () {
                var result = "hello world";
                def.resolve(result);
            }, 2000);
            return def;
        }

        service.do().then(function (data) {
            alert(1);
        });
    </script>

</body>
</html>
