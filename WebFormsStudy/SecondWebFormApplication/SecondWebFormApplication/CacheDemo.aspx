<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheDemo.aspx.cs" Inherits="SecondWebFormApplication.CacheDemo" %>

<%-- 页面缓存标签 --%>
<%@ OutputCache Duration="10" VaryByParam="None" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cache缓存演示</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>缓存测试</h3>
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnClearCache" runat="server" Text="清空缓存" OnClick="btnClearCache_Click" />
        </div>
    </form>
</body>
</html>
