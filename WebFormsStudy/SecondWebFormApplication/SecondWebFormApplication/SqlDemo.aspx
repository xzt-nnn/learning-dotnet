<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlDemo.aspx.cs" Inherits="SecondWebFormApplication.SqlDemo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SQL数据库演示</title>
    <style>
        body {
            margin: 0;
            padding: 40px 20px;
            font-family: "Microsoft YaHei", "Segoe UI", sans-serif;
            background: linear-gradient(135deg, #eef4ff 0%, #e8f7f3 100%);
            color: #2d3748;
        }
        .page { max-width: 920px; margin: 0 auto; }
        .card {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 6px 24px rgba(31, 61, 122, .12);
            padding: 24px 28px 28px;
            margin-bottom: 24px;
        }
        .card h3 {
            margin: 0 0 18px;
            padding-left: 12px;
            font-size: 18px;
            color: #1b4b91;
            border-left: 5px solid #3a7bd5;
        }
        .form-row { display: flex; align-items: center; flex-wrap: wrap; gap: 10px; }
        .form-row label { color: #55607a; font-size: 14px; }

        input[type=text], select {
            padding: 8px 12px;
            border: 1px solid #d5dceb;
            border-radius: 6px;
            font-size: 14px;
            outline: none;
            transition: border-color .2s, box-shadow .2s;
            background: #fff;
        }
        input[type=text]:focus, select:focus {
            border-color: #3a7bd5;
            box-shadow: 0 0 0 3px rgba(58, 123, 213, .15);
        }
        input[type=submit] {
            padding: 8px 18px;
            border: none;
            border-radius: 6px;
            background: linear-gradient(135deg, #4a90e2, #2f6fd0);
            color: #fff;
            font-size: 14px;
            cursor: pointer;
            transition: opacity .2s, transform .1s;
        }
        input[type=submit]:hover { opacity: .88; }
        input[type=submit]:active { transform: translateY(1px); }

        #gvDemo {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
            border-radius: 8px;
            overflow: hidden;
        }
        #gvDemo th {
            background: linear-gradient(135deg, #4a90e2, #2f6fd0);
            color: #fff;
            font-weight: 600;
            padding: 12px 14px;
            text-align: left;
        }
        #gvDemo td {
            padding: 10px 14px;
            border-bottom: 1px solid #edf1f8;
        }
        #gvDemo tr:nth-child(even) td { background: #f8fafd; }
        #gvDemo tr:hover td { background: #eef4ff; }
        #gvDemo td input[type=submit] {
            padding: 5px 14px;
            font-size: 12px;
            margin-right: 6px;
        }
        input.btn-edit { background: linear-gradient(135deg, #3ca374, #2b8a5e); }
        input.btn-save { background: linear-gradient(135deg, #3ca374, #2b8a5e); }
        input.btn-del  { background: linear-gradient(135deg, #f06a6a, #d94040); }
        input.btn-cancel { background: linear-gradient(135deg, #9aa5b8, #7b8698); }

        /* 查看详情：描边胶囊按钮 */
        #gvDemo .btn-view {
            display: inline-block;
            padding: 5px 14px;
            border: 1px solid #b9d2f5;
            border-radius: 14px;
            background: #f2f7ff;
            color: #2f6fd0;
            font-size: 12px;
            text-decoration: none;
            transition: all .2s;
        }
        #gvDemo .btn-view:hover {
            background: linear-gradient(135deg, #4a90e2, #2f6fd0);
            border-color: #2f6fd0;
            color: #fff;
            box-shadow: 0 5px 12px rgba(47, 111, 208, .3);
            transform: translateY(-1px);
        }
        #gvDemo .btn-view:active { transform: translateY(0); }

        .pager {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 16px;
            margin-top: 18px;
            font-size: 14px;
            color: #55607a;
        }
        .pager a {
            color: #3a7bd5;
            text-decoration: none;
            padding: 6px 16px;
            border: 1px solid #d5dceb;
            border-radius: 6px;
            background: #fff;
            transition: all .2s;
        }
        .pager a:hover { background: #eef4ff; border-color: #3a7bd5; }
        a[disabled] {                      /* LinkButton 禁用时的样式 */
            color: #b6bfd0 !important;
            background: #f5f7fb !important;
            border-color: #e6ebf5 !important;
            cursor: default;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div class="card">
                <h3>新增城市</h3>
                <div class="form-row">
                    <label>省份：</label><asp:TextBox ID="txtProvince" runat="server"></asp:TextBox>
                    <label>城市：</label><asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" Text="新增入库" CssClass="btn-add" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="card">
                <h3>城市列表</h3>
            <asp:GridView ID="gvDemo" runat="server" AutoGenerateColumns="false"
                DataKeyNames="Id" OnRowDeleting="gvDemo_RowDeleting"
                OnRowEditing="gvDemo_RowEditing" OnRowCancelingEdit="gvDemo_RowCancelingEdit" OnRowUpdating="gvDemo_RowUpdating" OnRowDataBound="gvDemo_RowDataBound"
                OnRowCommand="gvDemo_RowCommand"
                >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="编号" />
                    <%--<asp:BoundField DataField="Province" HeaderText="省份" />
                    <asp:BoundField DataField="Name" HeaderText="城市" />--%>
<%--                    <asp:templatefield headertext="省份">
                        <itemtemplate>
                            <%# eval("province") %>
                        </itemtemplate>
                        <edititemtemplate>
                            <asp:textbox id="txtprovinceedit" runat="server" text='<%# bind("province") %>'></asp:textbox>
                        </edititemtemplate>
                    </asp:templatefield>--%>

                    <asp:TemplateField HeaderText="省份">

                     <%--ItemTemplate`：正常浏览模式用的模板（不是编辑的时候）--%>
                    <ItemTemplate>
                        <%--Eval("Name")：表示只能看不能改--%>
                        <%--Bind（）：双向绑定，既能回显旧数据，又支持提交修改（TemplateField 编辑的时候用它）--%>
                        <%# Eval("Province") %>
                    </ItemTemplate>

                        <%--EditItemTemplate：只有编辑模式下才会显示的模板（编辑的时候）--%>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlProvinceEdit" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <%--HeaderText：表头--%>
                    <asp:TemplateField HeaderText="城市修改">
                        <ItemTemplate>
                            <%# Eval("Name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass="btn-edit" CommandName="Edit" />
                            <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn-del" OnClientClick="return confirm('确定删除？');" CommandName="Delete" />
                            <asp:LinkButton ID="btnView" runat="server" CssClass="btn-view" CommandName="View" CommandArgument='<%# Eval("Id") %>'>查看详情</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-view" OnClick="btnView_Click" CommandArgument='<%# Eval("Id") %>'>查看详情(QueryString)</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn-save" CommandName="Update" Text="保存" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn-cancel" CommandName="Cancel" Text="取消" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                <div class="pager">
                    <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">上一页</asp:LinkButton>
                    <asp:Label ID="lblPage" runat="server"></asp:Label>
                    <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">下一页</asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
    
</body>
</html>
