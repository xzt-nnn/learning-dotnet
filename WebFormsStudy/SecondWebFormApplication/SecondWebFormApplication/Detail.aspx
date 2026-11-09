<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SecondWebFormApplication.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>城市详情</title>
    <style>
        * { box-sizing: border-box; }
        body {
            margin: 0;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 40px 20px;
            font-family: "Microsoft YaHei", "Segoe UI", sans-serif;
            background: linear-gradient(135deg, #eef4ff 0%, #e8f7f3 100%);
            color: #2d3748;
        }
        .card {
            width: 430px;
            background: #fff;
            border-radius: 14px;
            box-shadow: 0 12px 32px rgba(31, 61, 122, .16);
            padding: 30px 32px 26px;
            animation: fadeUp .45s ease both;
        }
        @keyframes fadeUp {
            from { opacity: 0; transform: translateY(16px); }
            to   { opacity: 1; transform: translateY(0); }
        }
        .card h3 {
            margin: 0 0 20px;
            padding-left: 12px;
            font-size: 19px;
            color: #1b4b91;
            border-left: 5px solid #3a7bd5;
        }
        .msg {
            display: block;
            margin-bottom: 12px;
            color: #d94040;
            font-size: 14px;
        }
        .detail-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 13px 16px;
            margin-bottom: 12px;
            border-radius: 8px;
            background: #f6f9fe;
            transition: background .2s, transform .2s;
        }
        .detail-row:hover {
            background: #eef4ff;
            transform: translateX(4px);
        }
        .detail-row .label { color: #6b778f; font-size: 14px; }
        .detail-row .value {
            color: #1b4b91;
            font-size: 15px;
            font-weight: 600;
        }
        .btn-back {
            width: 100%;
            margin-top: 10px;
            padding: 11px 0;
            border: none;
            border-radius: 8px;
            background: linear-gradient(135deg, #4a90e2, #2f6fd0);
            color: #fff;
            font-size: 15px;
            cursor: pointer;
            transition: opacity .2s, transform .1s, box-shadow .2s;
        }
        .btn-back:hover {
            opacity: .9;
            box-shadow: 0 8px 18px rgba(47, 111, 208, .35);
        }
        .btn-back:active { transform: translateY(1px); }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <h3>城市详情</h3>
            <asp:Label ID="lblMsg" runat="server" CssClass="msg"></asp:Label>

            <div class="detail-row">
                <span class="label">省份</span>
                <asp:Label ID="lblProvince" runat="server" CssClass="value"></asp:Label>
            </div>
            <div class="detail-row">
                <span class="label">城市</span>
                <asp:Label ID="lblName" runat="server" CssClass="value"></asp:Label>
            </div>

            <asp:Button ID="btnBack" runat="server" Text="返回列表" CssClass="btn-back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
