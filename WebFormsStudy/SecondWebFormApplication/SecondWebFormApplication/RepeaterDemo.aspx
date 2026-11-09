<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepeaterDemo.aspx.cs" Inherits="SecondWebFormApplication.RepeaterDemo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Repeater分页演示</title>
    <style>
        .card-item{
            border:1px solid #eee;
            border-radius:8px;
            padding:12px;
            margin:8px 0;
            background:#fff;
        }
        .alt-item{
            background:#f6f9ff;
        }
        .pager{margin-top:20px;display:flex;gap:12px;align-items:center;}
        .pager a{padding:4px 12px;border:1px solid #ccc;border-radius:4px;text-decoration:none;color:#333;}
        .pager a[disabled]{color:#999;background:#f5f5f5;cursor:not-allowed;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:700px;margin:30px auto;">
            <h3>城市列表-Repeater（手写分页）</h3>
            <asp:Repeater ID="rptCity" runat="server">
                <HeaderTemplate>
                    <div style="font-weight:bold;font-size:16px;margin-bottom:10px;">城市卡片列表</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="card-item">
                        Id：<%# Eval("Id") %>
                        ｜省份：<%# Eval("Province") %>
                        ｜城市：<%# Eval("Name") %>
                        <asp:LinkButton ID="lnkDetail" runat="server" CommandArgument='<%# Eval("Id") %>' OnCommand="rptCity_Command">查看详情</asp:LinkButton>
                    </div>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="card-item alt-item">
                        Id：<%# Eval("Id") %>
                        ｜省份：<%# Eval("Province") %>
                        ｜城市：<%# Eval("Name") %>
                        <asp:LinkButton ID="lnkDetail" runat="server" CommandArgument='<%# Eval("Id") %>' OnCommand="rptCity_Command">查看详情</asp:LinkButton>
                    </div>
                </AlternatingItemTemplate>
                <SeparatorTemplate>
                    <hr style="border:0;border-top:1px dashed #ccc;margin:6px 0;">
                </SeparatorTemplate>
                <FooterTemplate>
                    <div style="margin-top:12px;color:#666;">=====列表结束=====</div>
                </FooterTemplate>
            </asp:Repeater>

            <%--分页区域，和GridView的分页控件一模一样--%>
            <div class="pager">
                <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">上一页</asp:LinkButton>
                <asp:Label ID="lblPageInfo" runat="server"></asp:Label>
                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">下一页</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
