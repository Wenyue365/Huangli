<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chooseDates.aspx.cs" Inherits="HuangDao.chooseDates" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <script src="jslib/mootools-core-1.4.5-full-nocompat-yc.js"></script>
    <link href="css/chooseDates.css" rel="stylesheet" />
    <title>择吉日</title>
</head>
<body>
    <style type="text/css">

    </style>
    <form id="frmChooseDates" runat="server">
        <div id="hl_word">
        <asp:Label runat="server" Text="宜忌" ID="lbWord"></asp:Label>
        </div>
        <div id="hl_month">
            <div>
            <asp:Label runat="server" ID="solarMonth" Text="4月"></asp:Label></div>
            <div>
            <asp:Label runat="server" ID="lunarMonth" Text="四月"></asp:Label></div>

        </div>
        <div id="hl_word_abstract">
            <asp:Label runat="server" Text="宜忌说明" ID="lbDetail"></asp:Label>
        </div>
        <script type="text/javascript">
            /* 函数：获取黄历名词解释 */
            function getWordAbstract(w) {
                var baseUrl = location.href;
                var i = baseUrl.lastIndexOf("/");
                baseUrl = baseUrl.substr(0, i);
                baseUrl = baseUrl + "/HuangdaoWS.asmx/getWordAbstract";

                var request = new Request({
                    url: baseUrl,
                    method: "post",
                    data: JSON.encode({ word: w }),
                    headers: { "Content-Type": "application/json; charset=utf8" },
                    onSuccess: function (responseText) {
                        var obj = JSON.decode(responseText);
                        $("lbDetail").set("text", obj.d);
                    },
                    onFailure: function () {
                        $("lbDetail").set("text", "request error");
                    },
                    onComplete: function () {
                        $("lbDetail").set("text", "end request");
                    }
                });

                request.send();
            }
        </script>
        <div id="hl_dates" runat="server" style="display:none"></div>
        <div id="ctl_calendar" runat="server"></div>
    </form>
</body>
</html>
