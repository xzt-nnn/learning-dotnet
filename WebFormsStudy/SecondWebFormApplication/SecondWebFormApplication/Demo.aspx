<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="SecondWebFormApplication.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>这是我的第一个页面</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnClick" runat="server" Text="点我试试" OnClick="btnClick_Click" />
            <br />
            <!-- Label 就是用来展示文本的，默认看不见，等后台代码给它赋值才显示内容 -->
            <asp:Label ID="lblTip" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnClick_Click" />
            <br />
            <asp:Label ID="subTip" runat="server" Text=""></asp:Label>
         </div>
        <div>

        </div>
    </form>
</body>
</html>
