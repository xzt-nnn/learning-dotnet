<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="..." Inherits="..." %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebService 测试</title>
    <!-- jQuery 放 head，先加载 -->
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 页面内容随意 -->
    </form>

    <!-- 你的调用代码放 body 末尾、</body> 之前 -->
    <script>
        $(function () {
            $.ajax({
                url: "CityService.asmx/GetCityList",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    console.log(res.d);   // WebService 的返回值在 d 里
                }
            });
        });
</script>
</body>
</html>