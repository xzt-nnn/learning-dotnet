<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidatorDemo.aspx.cs" Inherits="SecondWebFormApplication.ValidatorDemo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>验证控件练习</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>用户注册表单</h4>
            用户名：
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server"
                ControlToValidate="txtName"
                ErrorMessage="用户名不能为空"
                Display="Dynamic"
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br /><br />

            年龄：
            <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
            <asp:RangeValidator ID="rvAge" runat="server"
                ControlToValidate="txtAge"
                Type="Integer"
                MinimumValue="1"
                MaximumValue="120"
                ErrorMessage="年龄必须是1~120之间的数字"
                Display="Dynamic"
                ForeColor="Red">
            </asp:RangeValidator>
            <br /><br />

            邮箱：
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                ControlToValidate="txtEmail"
                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                ErrorMessage="邮箱格式不正确"
                Display="Dynamic"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
            <br /><br />

            密码：
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            确认密码：
            <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="cvPwd" runat="server"
                ControlToValidate="txtPwd2"
                ControlToCompare="txtPwd"
                ErrorMessage="两次输入密码不一致"
                Display="Dynamic"
                ForeColor="Red">
            </asp:CompareValidator>
            <br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="提交注册" OnClick="btnSubmit_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>

            <%--汇总所有错误消息，放这里--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
