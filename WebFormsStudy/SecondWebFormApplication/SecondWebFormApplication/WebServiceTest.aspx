<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebServiceTest.aspx.cs" Inherits="SecondWebFormApplication.WebServiceTest" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebService 测试</title>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    </form>

    <script>
// @ts-nocheck
$(function () {
    $.ajax({
        url: "CityService.asmx/GetCityList",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{}",
        success: function (res) {
            console.log(res.d);
        },
        error: function (xhr) {
            console.log(xhr.status, xhr.responseText);
        }
    });
});

$(function () {
    var postData = {
        Province: "广东",
        Name: "肇庆"
    };
    $.ajax({
        url: "/api/city/add",
        type: "Post",
        contentType: "application/json; charset=utf-8", // 必须声明是json
        data: JSON.stringify(postData), // 对象转json字符串
        success: function (res) {
            console.log(res);
            alert(res.msg);
        },
        error: function (err) {
            console.log(err);
        }
    })
})
    </script>
</body>
</html>
