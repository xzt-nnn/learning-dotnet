<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="example.aspx.cs" Inherits="SecondWebFormApplication.example" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnAdd" runat="server" Text="点我+1" OnClick="btnAdd_Click" />
        </div>
    </form>
</body>
</html>
