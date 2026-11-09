<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SecondWebFormApplication.Main" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>主页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>欢迎进入系统</h3>
            <asp:Label ID="lblUser" runat="server"></asp:Label>
            <br/>
            <asp:Button ID="btnLogout" runat="server" Text="退出登录" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
