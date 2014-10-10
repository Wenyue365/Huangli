<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseDates.aspx.cs" Inherits="HuangDao.chooseDates" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="css/chooseDates.css" rel="stylesheet" />
    <title>择吉日</title>
</head>
<body>
    <style type="text/css">

    </style>
    <input id="CurrentDate" type="hidden" />
    <form id="frmChooseDates" runat="server">
        <div class="yiji_word_panel">
            <div id="hl_word">
                <asp:Label runat="server" Text="宜忌" ID="lbWord"></asp:Label>
            </div>
            <div id="hl_word_abstract" class="hl_word_abstract">
                <asp:Label runat="server" Text="宜忌说明" ID="lbDetail"></asp:Label>
            </div>
        </div>
        <div id="hl_month">
            <div>
            <asp:Label runat="server" ID="solarMonth" Text="4月"></asp:Label></div>
            <div>
                <asp:Label runat="server" ID="lunarMonth" Text="二○一四年七月初八"></asp:Label>
                <asp:Label runat="server" ID="AncientDate" Text="甲午年辛未月丙午日"></asp:Label>
            </div>
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
        <div class="yiji_switch_bar">
            <div id="btnYi" class="button yi_color"><span>宜</span></div>
            <div id="btJi" class="button ji_color"><span>忌</span></div>
        </div>
        <div id="ctl_calendar" runat="server"></div>
    </form>
</body>
</html>
