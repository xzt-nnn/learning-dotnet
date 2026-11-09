<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBox.aspx.cs" Inherits="SecondWebFormApplication.CheckBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>复选框测试</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>选择你喜欢的城市：</p>
            <asp:CheckBox ID="chkGuangzhou" runat="server" Text="广州" />
            <asp:CheckBox ID="chkZhaoqing" runat="server" Text="肇庆" />
            <asp:CheckBox ID="chkHangzhou" runat="server" Text="杭州" />
            <br />
            <asp:RadioButton ID="rdoMale" runat="server" GroupName="Gender" Text="男" />
            <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Gender" Text="女" />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
            <br />
            <asp:Label ID="lblTip" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
