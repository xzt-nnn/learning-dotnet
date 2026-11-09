<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferDemo.aspx.cs" Inherits="SecondWebFormApplication.TransferDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            输入内容：<asp:TextBox ID="txtMsg" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnRedirect" runat="server" Text="Response.Redirect跳转" OnClick="btnRedirect_Click" />
            <asp:Button ID="btnTransfer" runat="server" Text="Server.Transfer跳转" OnClick="btnTransfer_Click" />
        </div>
    </form>
</body>
</html>
