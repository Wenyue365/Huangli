<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Love.aspx.cs" Inherits="HuangDao.Love" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta id="Keywords" content="问曰 黄历 农历 黄道吉日 姻缘 姓名" runat="server"/>
<meta id="Description" content="问曰黄历为提供简洁的公历,农历查询,姓名,号码测算,姻缘测算" runat="server" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta content="width=device-width,user-scalable=no" name="viewport" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta property="wb:webmaster" content="68bd9bcdb2c1e7d0" />
<meta name="360-site-verification" content="a0583e054aa7cf6a9b1cc365210ff7e4" />
<meta name="shenma-site-verification" content="dedaac9ab6b26c675eb0b5b2a25a97fd" />
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/frame.css" rel="stylesheet" />
    <link href="css/love.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="jslib/lzclib.js"></script>
    <title>姻缘</title>
</head>
<body>
    <div class="body_container">
    <div class="container">
    <form id="form1" runat="server">
        <asp:HiddenField id="UserIP" runat="server"/>
        <asp:HiddenField id="currentDate" runat="server" />
        <asp:HiddenField id="defaultSearchType" runat="server" />

        <div id="divDivineInputBox" class="div_input_panel">
            <div class="input_panel">
            <div class="panel_man">
               <!--男方姓名-->
                <input type="text" id="manName" class="man_name"/>
            </div>
            <div class="panel_woman">
                <!--女方姓名-->
                <input type="text" id="womanName" class="woman_name" />
            </div>
            </div>
            <div class="button_panel">
                <input type="button"  value="算一算" onclick="return onclick_Calc()"/>
            </div>
        </div>
        <div id="divCalcResult" class="calc_result">

        </div>

        <script type="text/javascript">
            // 函数：JSON 对象转 QueryString字符串形式
            function json2str(o) {
                var arr = [];
                for (var i in o) arr.push(i+ "=" + o[i]);
                return arr.join('&');
            }
            // 从 XML 文档对象中获取指定 Tag 的文本值
            function getXmlTagText(xmlDoc, tagName, i) {
                var str = null;
                var valueTag = xmlDoc.getElementsByTagName(tagName)[i];
                if (valueTag !== null) {
                    str = valueTag.innerHTML == null ? valueTag.textContent : valueTag.innerHTML;
                }

                return str;
            }

            // 函数：测算姻缘
            function calcCouple(mi, wi) {

                // Remove all child-elements
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }

                // Call Webservice : calcCouple
                var baseUrl = location.href;
                var i = baseUrl.lastIndexOf("/");
                baseUrl = baseUrl.substr(0, i);
                baseUrl = baseUrl + "/HuangdaoWS.asmx/SheupCalcCouple";

                // Set data value will be post
                var PostData = { manName: mi, womanName: wi };

                showLoading(null, true);
                $.ajax({
                    url: baseUrl,
                    type: 'POST',
                    dataType: 'xml',
                    data: json2str(PostData), /* 设定参数的值 */
                    success: onSuccHandle
                }); /* End of AJAX Request */

                function onSuccHandle(rspData) {
                    showLoading(null, false);

                    var divCalcResult = $E("divCalcResult");

                    var ulCalcResult = document.createElement("ul");
                    for (var i = 0; i < rspData.getElementsByTagName("_name").length; i++) {
                        var liCalResult = document.createElement("li");
                        var spanName = document.createElement("span");
                        var spanValue = document.createElement("span");

                        spanName.innerHTML = getXmlTagText(rspData, "_name", i);
                        spanValue.innerHTML = getXmlTagText(rspData, "_value", i);

                        liCalResult.appendChild(spanName);
                        liCalResult.appendChild(spanValue);
                        ulCalcResult.appendChild(liCalResult);
                    }

                    divCalcResult.appendChild(ulCalcResult); // This a JQuery object !
                }
            }

            function onclick_Calc()
            {
                var DivineTypesBar = $E("DivineTypesBar");
                var mName = $E("manName").value;
                var wName = $E("womanName").value;

                if (true)
                {
                    calcCouple(mName, wName);
                }

            }/* END : onclick_CalcCarNumber */

            function initPage()
            {
                initInputBoxCtrl("manName", "输入男方姓名");
                initInputBoxCtrl("womanName", "输入女方姓名");
            }

            /*********************************/
            initPage();

        </script>
        <div id="AdvertiseBar">
            <span>广告</span>
        </div>
    </form>

    </div>
    </div>

        <!--START : Footer -->
            <div class="bottom_nav_toolbar" id="bottomNavToolbar">
            <ul>
                <li><a href="./huangli.aspx">黄历</a></li>
                <li><a href="./BestTodo.aspx">宜忌</a></li>
                <li><a href="./Divine.aspx">测算</a></li>
                <li><a href="./love.aspx">姻缘</a></li>
            </ul>
        </div>
    <!--END : Footer -->

    
</body>
</html>
