<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterChild.aspx.cs" Inherits="SecondWebFormApplication.MasterChild" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>这是子页面独有的内容</h3>
    <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>
    <asp:Button ID="btnTest" runat="server" Text="点我" OnClick="btnTest_Click" />
    <br/>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
