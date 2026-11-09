<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTriggerDemo.aspx.cs" Inherits="SecondWebFormApplication.AjaxTriggerDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>触发器Trigger演示</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <br /><br />
            <asp:Button ID="btnOutside" runat="server" Text="外部按钮，触发无刷新" OnClick="btnOutside_Click" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    面板内时间：<asp:Label ID="lblTime" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnOutside" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            
        </div>
    </form>
</body>
</html>
