<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCity.aspx.cs" Inherits="SecondWebFormApplication.AjaxCity" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>城市管理-无刷新CRUD</title>
    <style>
        .item{margin:8px 0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%-- 必须ScriptManager，放在最前面 --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="upAll" runat="server">
            <ContentTemplate>
                <h3>新增城市</h3>
                <div class="item">
                    省份：<asp:TextBox ID="txtProvince" runat="server"></asp:TextBox>
                </div>
                <div class="item">
                    城市：<asp:TextBox ID="txtCityName" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" />
                <hr/>

                <h3>城市列表</h3>
                <asp:GridView ID="gvCity" runat="server" 
                    AutoGenerateColumns="false" 
                    DataKeyNames="Id"
                    OnRowDeleting="gvCity_RowDeleting"
                    OnRowEditing="gvCity_RowEditing"
                    OnRowUpdating="gvCity_RowUpdating"
                    OnRowCancelingEdit="gvCity_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="编号" ReadOnly="true"/>
                        <asp:BoundField DataField="province" HeaderText="省份" />
                        <asp:BoundField DataField="name" HeaderText="城市" />
                        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
