<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CookieDemo.aspx.cs" Inherits="SecondWebFormApplication.CookieDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtMsg" runat="server" placeholder="输入要存储到cookie中的内容"></asp:TextBox>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="写入Cookie" OnClick="btnSave_Click" />
            <asp:Button ID="btnRead" runat="server" Text="读取Cookie" OnClick="cookieRead_Click" />
            <asp:Button ID="btnDel" runat="server" Text="删除Cookie" OnClick="cookieDel_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Blue"></asp:Label>
        </div>
    </form>
</body>
</html>
