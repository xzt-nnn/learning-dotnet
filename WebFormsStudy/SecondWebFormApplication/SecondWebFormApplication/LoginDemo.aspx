<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginDemo.aspx.cs" Inherits="SecondWebFormApplication.LoginDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>登录</h4>
            账号：<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
            密码：<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:CheckBox ID="chkRemember" runat="server" Text="记住我" />
            <br />
            <asp:Button ID="LoginButton" runat="server" Text="登录" OnClick="btnLogin_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
