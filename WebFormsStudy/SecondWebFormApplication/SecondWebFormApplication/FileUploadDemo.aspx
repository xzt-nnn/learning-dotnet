<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadDemo.aspx.cs" Inherits="SecondWebFormApplication.FileUploadDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文件上传demo</title>
    <style>
        .tip{color:red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>文件上传</h3>
            <asp:FileUpload ID="fuFile" runat="server" />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="上传文件" OnClick="btnUpload_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server" CssClass="tip"></asp:Label>
        </div>
    </form>
</body>
</html>
