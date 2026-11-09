<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Target.aspx.cs" Inherits="SecondWebFormApplication.Target" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>目标页面</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>收到的数据：</h4>
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
