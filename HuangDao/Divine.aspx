<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Divine.aspx.cs" Inherits="HuangDao.Divine" %>

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
    <link href="css/divine.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/jquery.xml2json.js"></script>
    <script src="jslib/lzclib.js"></script>
    <script src="jslib/city.js"></script>
    <title>测算</title>
</head>
<body>
    <div class="body_container">
    <div class="container">
    <form id="form1" runat="server">
        <asp:HiddenField id="UserIP" runat="server"/>
        <asp:HiddenField id="currentDate" runat="server" />
        <asp:HiddenField id="defaultSearchType" runat="server" />
    <div id="TitleToolBar" class="title_bar_panel">
            <div id="divUserIcon" class="user_icon"><img src="./images/user_default_male.png" /></div>
            <div id="divUserInfo" class="user_info"><span>生辰</span><span>2000年3月3日</span></div>
        </div>
        <div id="DivineTypesBar" class="divine_type_bar">
            <ul>
                <li class="selected_tab" id="schtypeNumber">
                    <span onclick="onclick_SearchType(this)">号码</span>
                </li>
                <li  id="schtypeName">
                    <span onclick="onclick_SearchType(this)">名字</span>
                </li>
                <li id="schtypeMarrage">
                    <span onclick="onclick_SearchType(this)">婚姻</span>
                </li>
                <li>
                    <span>+</span>
                </li>
            </ul>
            <script type="text/javascript">
              
            </script>
        </div>
        <div id="divDivineInputBox">
            <div id="numberInfoPanel" class="input_box">
                <input type="text" id="inputBoxNumber" class="prompt_text" />
            </div>
            <div id="nameInfoPanel" class="input_box" style="display:none">
                <input type="text" id="inputBoxName" class="prompt_text" />
            </div>
            <div id="coupleInfoPanel" class="couple_info_panel" style="display:none">
            <div class="panel_man">
                <span class="info_input_title">男方出生地及生辰</span>
            <!--BEGIN: 地区选择 A -->
            <div>
                <select name="prov" id="s1" class="kw2">
                    <option value="省份">省份</option>
                </select> <select name="city" id="s2" class="kw2">
                    <option value="地市">地市</option>
                </select> <select name="county" id="s3" class="kw2">
                    <option value="区县">区县</option>
                </select>
            </div>
            <!--出生年月日时辰选择 A-->
            <div>
                <ul>
                    <li>
                        <select name="nian1" id="nian1" class="kw1"></select>
                    </li>
                    <li>
                        <select name="yue1" id="yue1" class="kw1"></select>
                    </li>
                    <li>
                        <select name="ri1" id="ri1" class="kw1"></select>
                    </li>
                    <li>
                        <select name="shi1" id="shi1" class="kw1"></select>
                    </li>
                    <li>
                        <select name="fen1" id="fen1" class="kw1"></select>
                    </li>
                </ul>
            </div>
               <!--男方姓名-->
                <input type="text" id="manName" class="man_name" value="男方姓名"  title="男方姓名" onfocus="onfocus_inputCtrl(this)" onblur="onblur_InputCtrl(this)"/>
            </div>
            <div class="panel_woman">
                <span class="info_input_title">女方出生地及生辰</span>
            <!--BEGIN: 地区选择 B -->
            <div>
                <select name="prov2" id="s12" class="kw2">
                    <option value="省份">省份</option>
                </select>
                <select name="city2" id="s22" class="kw2">
                    <option value="地市">地市</option>
                </select>
                <select name="county2" id="s32" class="kw2">
                    <option value="区县">区县</option>
                </select>
            </div>
            <!--出生年月日时辰选择 B-->
            <div>
                <ul>
                    <li>
                        <select name="nian2" id="nian2" class="kw2"></select>
                    </li>
                    <li>
                        <select name="yue2" id="yue2" class="kw2"></select>
                    </li>
                    <li>
                        <select name="ri2" id="ri2" class="kw2"></select>
                    </li>
                    <li>
                        <select name="shi2" id="shi2" class="kw2"></select>
                    </li>
                    <li>
                        <select name="fen2" id="fen2" class="kw2"></select>
                    </li>
                </ul>
            </div>
                <!--女方姓名-->
                <input type="text" id="womanName" class="woman_name" value="女方姓名" title="女方姓名" onfocus="onfocus_inputCtrl(this)" onblur="onblur_InputCtrl(this)"/>
            </div>
        </div>
    <script type="text/javascript">
        setup(); /* 初始化地区选择控件 */

        initDateTimeCtrl('#nian1', '#yue1', '#ri1', '#shi1', '#fen1');
        initDateTimeCtrl('#nian2', '#yue2', '#ri2', '#shi2', '#fen2');

        initInputBoxCtrl("inputBoxNumber", "电话号码/车牌号码");
        initInputBoxCtrl("inputBoxName", "输入姓名");


    </script>
            <div class="button_box">
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

            // 事件处理函数：切换搜索分类
            function onclick_SearchType(tab) {
                // 设置当前选中的分类
                var DivineTypesBar = $("#DivineTypesBar");
                DivineTypesBar.data("selectedTabName", tab.innerHTML);
                // 取消 DivineTypesBar 的所有样式
                $("#DivineTypesBar ul li").removeClass("selected_tab");

                // 切换搜索分类类（界面）
                switchSearchType(tab.parentNode);

                // 清除此前的计算结果
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }
            }

            // 函数：测算号码
            function calcNumber(number)
            {
                // Remove all child-elements
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }

                // Set data value will be post
                var post_data = number;

                // Success event handler
                function onSuccHandle(data) {
                    showLoading(null, false);

                    var divCalcResult = $("#divCalcResult");
                    var calcResult = $.xml2json(data);
                    var ulCalcResult = document.createElement("ul");
                    for (var i = 0; i < calcResult._Entries.EntryBase.length; i++) {
                        var liCalResult = document.createElement("li");
                        var spanName = document.createElement("span");
                        var spanValue = document.createElement("span");

                        spanName.innerHTML = calcResult._Entries.EntryBase[i]._name;
                        spanValue.innerHTML = calcResult._Entries.EntryBase[i]._value;

                        liCalResult.appendChild(spanName);
                        liCalResult.appendChild(spanValue);
                        ulCalcResult.appendChild(liCalResult);
                    }

                    divCalcResult.append(ulCalcResult); // This a JQuery object !
                }

                // Call Webservice : calcCarNumber
                var baseUrl = location.href;
                var i = baseUrl.lastIndexOf("/");
                baseUrl = baseUrl.substr(0, i);
                baseUrl = baseUrl + "/HuangdaoWS.asmx/calcCarNumber";

                showLoading(null, true);

                $.ajax({
                    url: baseUrl,
                    type: 'POST',
                    dataType: 'xml',
                    data: { carNum: post_data }, /* 设定carNum 的值 */
                    success: onSuccHandle
                }); /* End of AJAX Request */

            } // END 函数：计算用户输入的号码
            // 函数：测算名字
            function calcName(fn, ln)
            {
                // Check if it is a valid car-number
                // Check if it is a valid phone-number

                // Remove all child-elements
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }

                // Success event handler
                function onSuccHandle(data) {
                    showLoading(null, false);
                    var divCalcResult = $("#divCalcResult");
                    var calcResult = $.xml2json(data);
                    var ulCalcResult = document.createElement("ul");
                    for (var i = 0; i < calcResult._Entries.EntryBase.length; i++) {
                        var liCalResult = document.createElement("li");
                        var spanName = document.createElement("span");
                        var spanValue = document.createElement("span");

                        spanName.innerHTML = calcResult._Entries.EntryBase[i]._name;
                        spanValue.innerHTML = calcResult._Entries.EntryBase[i]._value;

                        liCalResult.appendChild(spanName);
                        liCalResult.appendChild(spanValue);
                        ulCalcResult.appendChild(liCalResult);
                    }

                    divCalcResult.append(ulCalcResult); // This a JQuery object !
                }

                // Call Webservice : calcCarNumber
                var baseUrl = location.href;
                var i = baseUrl.lastIndexOf("/");
                baseUrl = baseUrl.substr(0, i);
                baseUrl = baseUrl + "/HuangdaoWS.asmx/calcName";

                // Set data value will be post
                var PostData = { firstName: fn, lastName: ln };

                showLoading(null, true);

                $.ajax({
                    url: baseUrl,
                    type: 'POST',
                    dataType: 'xml',
                    data: PostData, /* 设定carNum 的值 */
                    success: onSuccHandle
                }); /* End of AJAX Request */

            }
            // 函数：测算姻缘
            function calcCouple(mi, wi) {

                // Remove all child-elements
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }

                // 请求参数串
                var str1 = json2str(mi);
                var str2 = json2str(wi);

                // Call Webservice : calcCouple
                var baseUrl = location.href;
                var i = baseUrl.lastIndexOf("/");
                baseUrl = baseUrl.substr(0, i);
                baseUrl = baseUrl + "/HuangdaoWS.asmx/calcCouple";

                // Set data value will be post
                var PostData = { qrystrCoupleInfo: (str1 + "&" + str2 )};

                $.ajax({
                    url: baseUrl,
                    type: 'POST',
                    dataType: 'xml',
                    data: PostData, /* 设定参数的值 */
                    success: onSuccHandle
                }); /* End of AJAX Request */

                function onSuccHandle(rspData) {
                    var divCalcResult = $("#divCalcResult");
                    var calcResult = $.xml2json(rspData);
                    var ulCalcResult = document.createElement("ul");
                    for (var i = 0; i < calcResult._Entries.EntryBase.length; i++) {
                        var liCalResult = document.createElement("li");
                        var spanName = document.createElement("span");
                        var spanValue = document.createElement("span");

                        spanName.innerHTML = calcResult._Entries.EntryBase[i]._name;
                        spanValue.innerHTML = calcResult._Entries.EntryBase[i]._value;

                        liCalResult.appendChild(spanName);
                        liCalResult.appendChild(spanValue);
                        ulCalcResult.appendChild(liCalResult);
                    }

                    divCalcResult.append(ulCalcResult); // This a JQuery object !
                }
            }

            function onclick_Calc()
            {
                var DivineTypesBar = $("#DivineTypesBar");
                var selectedTabName = DivineTypesBar.data("selectedTabName");

                if (selectedTabName == null) selectedTabName = "号码"; // 如果未选择任何 Tab，缺省为“号码”

                if (selectedTabName == "号码")
                {
                    var ipb = $E("inputBoxNumber");
                    // Check if there is valid query string 
                    if (ipb == null || ipb.value == "") {
                        InputSearchKeyword.focus();
                        return;
                    }

                    calcNumber(ipb.value);
                }
                else if (selectedTabName == "名字")
                {
                    var ipb = $E("inputBoxName");
                    // Check if there is valid query string 
                    if (ipb == null || ipb.value == "") {
                        InputSearchKeyword.focus();
                        return;
                    }
                    
                    calcName(ipb.value.substr(0, 1), ipb.value.substr(1, 2));
                }
                else if (selectedTabName == "婚姻")
                {
                    var ManInfo = { n: "1980", y: "08", r:"08", s: "08", f: "08", nn: "男", d1: "广东省", d2: "广州市", d3: "越秀区", xm: "张三" };
                    var WomanInfo = { n2: "1980", y2: "08", r2: "08", s2: "08", f2: "08", nn2: "女", d12: "广东省", d22: "广州市", d32: "越秀区", xm2: "李四" };

                    ManInfo.n = $("#nian1").val();
                    ManInfo.y = FormatInt($("#yue1").val(),2);
                    ManInfo.r = FormatInt($("#ri1").val(),2);
                    ManInfo.s = FormatInt($("#shi1").val(),2);
                    ManInfo.f = FormatInt($("#fen1").val(),2);
                    ManInfo.d1 = $("#s1").val();
                    ManInfo.d2 = $("#s2").val();
                    ManInfo.d3 = $("#s3").val();
                    ManInfo.xm = $("#manName").val();

                    WomanInfo.n2 = $("#nian2").val();
                    WomanInfo.y2 = FormatInt($("#yue2").val(),2);
                    WomanInfo.r2 = FormatInt($("#ri2").val(),2);
                    WomanInfo.s2 = FormatInt($("#shi2").val(),2);
                    WomanInfo.f2 = FormatInt($("#fen2").val(),2);
                    WomanInfo.d12 = $("#s12").val();
                    WomanInfo.d22 = $("#s22").val();
                    WomanInfo.d32 = $("#s32").val();
                    WomanInfo.xm2 = $("#womanName").val();

                    calcCouple(ManInfo, WomanInfo);
                }

            }/* END : onclick_CalcCarNumber */

            // selTab : object, html element 
            function switchSearchType(selTab)
            {
                var SearchTypes =
                    [{ id: "schtypeNumber", typeName: "号码", tip: "电话号码/车牌号码", relElmIds: ["numberInfoPanel"] },
                        { id: "schtypeName", typeName: "名字", tip: "输入姓名", relElmIds: ["nameInfoPanel"] },
                        { id: "schtypeMarrage", typeName: "婚姻", tip: "电话号码/车牌号码", relElmIds: ["coupleInfoPanel"] }];

                // 清除此前的计算结果
                var divCalcResult = document.getElementById("divCalcResult");
                while (divCalcResult.firstChild) {
                    divCalcResult.removeChild(divCalcResult.firstChild);
                }

                var len = SearchTypes.length;
                for (var i = 0; i < len; i++)
                {
                    var stype = $E(SearchTypes[i].id);
                    if (SearchTypes[i].id == selTab.id)
                    {
                        addClass(stype, "selected_tab");
                        for(var k=0; k < SearchTypes[i].relElmIds.length; k++)
                        {
                            var rle = $E(SearchTypes[i].relElmIds[k]);
                            if (rle) rle.style.display = "block";
                        }
                    }
                    else
                    {
                        removeClass(stype, "selected_tab");
                        for (var k = 0; k < SearchTypes[i].relElmIds.length; k++) {
                            var rle = $E(SearchTypes[i].relElmIds[k]);
                            if (rle) rle.style.display = "none";
                        }
                    }
                }
            }

            function initPage()
            {
                // 设置默认测算分类
                var dst = $E("defaultSearchType");
                if (dst != null)
                {

                }
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
