<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BestToDo.aspx.cs" Inherits="HuangDao.BestToDo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta id="Keywords" content="问曰 黄历 农历 黄道吉日 姻缘 姓名" runat="server"/>
    <meta id="Description" content="问曰黄历为提供简洁的公历,农历查询,姓名,号码测算,姻缘测算" runat="server" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta property="wb:webmaster" content="68bd9bcdb2c1e7d0" />
    <meta name="360-site-verification" content="a0583e054aa7cf6a9b1cc365210ff7e4" />
    <meta name="shenma-site-verification" content="dedaac9ab6b26c675eb0b5b2a25a97fd" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="jslib/lzclib.js"></script>
    <script src="HuangLiData/YijiEvents.js"></script>
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/frame.css" rel="stylesheet" />
    <link href="css/BestToDo.css" rel="stylesheet" />
    <title>宜</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="defaultKeyword" runat="server" />
        <asp:HiddenField ID="currentYearMonth" runat="server" />
        <input type="hidden" id="schType" />
        <div class="body_container">
            <div class="container">
                <div id="TitleToolBar" class="title_bar_panel">
                    <%--<div id="divUserIcon" class="user_icon">
                        <img src="./images/user_default_male.png" /></div>
                    <div id="divUserInfo" class="user_info"><span>生辰</span><span>2000年3月3日</span></div>--%>
                </div>
                <div id="divSearchBar" class="search_bar">
                    <div class="input_search_box">
                        <input type="search" id="inputSearch"  class="search_input_box" readonly="readonly" />
                    </div>
                     <div id="searchPromptBox" class="search_prompt_box"></div>
                    <div id="divEventTypeBar" class="event_type_bar">
                        <div id="btnYiSearch" class="search_btn yi" onclick="onclick_btnSearchYi()"><a>宜</a></div>
                        <div id="btnJiSearch" class="search_btn ji" onclick="onclick_btnSearchJi()"><a>忌</a></div>
                    </div>
                </div>
                <!--开始：黄道名词列表 -->     
                <div id="divKeywordYi" runat="server" class="keyword_list">
                <!--span id="hlYi">安居</span!-->
           
                </div>
                <!--结束：黄道名词列表 -->
                <div class="year_month_spin_bar">
                    <div id="divMonthBarLeft" class="month_bar left" onclick="onclick_prevMonth()">
                        <ul>
                            <li>&lt;</li>
                            <li><asp:Literal ID="ltlPrevMonth" runat="server">7月</asp:Literal></li>
                        </ul>
                        </div>
                        <div id="divYearBar" class="year_bar">
                            <ul>
                                <li>
                                    <asp:Literal ID="solarYear" runat="server"></asp:Literal>
                                    <asp:Literal ID="solarMonth" runat="server"></asp:Literal>
                                </li>
                            </ul>
                        </div>
                        <div id="divMonthBarRight" class="month_bar right" onclick="onclick_nextMonth()">
                            <ul>
                                <li><asp:Literal ID="ltlNextMonth" runat="server">9月</asp:Literal></li>
                                <li><a>&gt;</a></li>
                            </ul>
                        </div>
            </div>
                 <div id="ctl_calendar" class="calendar" runat="server">
                     <!-- 在这里插入日历控件 -->
                 </div>
            </div>
            <!--开始：黄历名词解释 -->
            <div class="keyword_detail">
                <div id="eventExplain" class="event_explain"></div>
            </div>
        </div>
    </form>

    <!-- START: 底部导航栏 -->
    <div class="bottom_nav_toolbar" id="bottomNavToolbar">
        <ul>
            <li><a href="./huangli.aspx">黄历</a></li>
            <li><a href="./bestTodo.aspx">宜忌</a></li>
            <li><a href="./divine.aspx">测算</a></li>
            <li><a href="./love.aspx">姻缘</a></li>
        </ul>
    </div>
    <!-- END: 底部导航栏 -->
    <script type="text/javascript">
        //本页需要的js基础函数

        //取cookie的值
        function getcookie(name) {
            var my_cookie = document.cookie;
            var start = my_cookie.indexOf(name + "@astro.lady.qq.com" + "=");
            if (start == -1) return '';

            start += name.length + 19; //1 stands of '='

            var end = my_cookie.indexOf(";", start);
            if (end == -1) end = my_cookie.length;
            return my_cookie.substr(start, end - start);
        }
        //设置cookie
        function setcookie(name, value, open, path) {
            var nextyear = new Date();
            if (open) {
                nextyear.setFullYear(nextyear.getFullYear() + 1);
            } else {
                nextyear.setFullYear(1970);
            }
            document.cookie = name + "@astro.lady.qq.com" + "=" + value + "; expires=" + nextyear.toGMTString() + "; path=" + path;
        }

        /* 函数：获取黄历名词解释 */
        function getWordAbstract(w)
        {
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
                    $("txbDetail").set("text", obj.d);
                },
                onFailure: function () {
                    $("txbDetail").set("text", "request error");
                },
                onComplete: function () {
                    $("txbDetail").set("text", "end request");
                }
            });

            request.send();
        }

        function onclick_word()
        {
           getWordAbstract(this.innerHTML);
        }

        /* 获取当前月份 */
        function getCurrentMonth() {
            var cym = $E("currentYearMonth");
            var cm = new Date(cym.value);

            return cm;
        }

        /* 设置当前月份 */
        function setCurrentMonth(m) {
            var cym = $E("currentYearMonth");
            cym.value = new Date(m);
        }

        /* 显示下一月 */
        function showPrevMonth(m) {
            $E("divMonthBarLeft").children[0].children[1].innerHTML = (m.getMonth()+1) + "月";
        }
        /* 显示上一月 */
        function showNextMonth(m) {
            $E("divMonthBarRight").children[0].children[0].innerHTML = (m.getMonth()+1) + "月";
        }

        /* 在当前月份显示元素（#divYearBar/ul/li/text）上显示“当前月份” */
        function showCurrentMonthYear(m) {
            $E("divYearBar").children[0].children[0].innerHTML = m.getFullYear() + "年" + (m.getMonth() + 1) + "月";

            var pvm = new Date(m);
            pvm.setMonth(pvm.getMonth() - 1);
            showPrevMonth(pvm);

            var nxm = new Date(m);
            nxm.setMonth(nxm.getMonth() + 1);
            showNextMonth(nxm);
        }

        /* 获取请求字符串 
        *  d : Date 日期对象 
        */
        function getQueryStr(d) {
            // return ("hly=" + d.getFullYear() + "&hlm=" + d.getMonth() + "&hld=" + d.getDay());
            return ("hld=" + d.getFullYear() + "-" + (d.getMonth()+1) + "-" + d.getDate());
        }

        function onclick_prevMonth() {
            var m = getCurrentMonth();
            var kw = $E("inputSearch").value;
            m.setMonth(m.getMonth() - 1);

            var hlmdata = loadHuangliMonthData(m, function () {
                var hld = new HuangliYiji(); /* 不带参数的初始化 HuangliYiJi 实例 */
                g_cldr.setBestDates(hld.getYiDates(kw));

                var bShowYi = (getSearchType() == "yi") ? true : false;
                var bShowJi = (getSearchType() == "ji") ? true : false;
                g_cldr.RebuildCalendar(m, bShowYi, bShowJi);

                setCurrentMonth(m);
                showCurrentMonthYear(m);
            });
        }

        function onclick_nextMonth() {
            var m = getCurrentMonth();
            var kw = $E("inputSearch").value;
            m.setMonth(m.getMonth() + 1);

            var hlmdata = loadHuangliMonthData(m, function () {
                var hld = new HuangliYiji(); /* 不带参数的初始化 HuangliYiJi 实例 */
                g_cldr.setBestDates(hld.getYiDates(kw));

                var bShowYi = (getSearchType() == "yi") ? true : false;
                var bShowJi = (getSearchType() == "ji") ? true : false;
                g_cldr.RebuildCalendar(m, bShowYi, bShowJi);

                setCurrentMonth(m);
                showCurrentMonthYear(m);
            });
        }

        function setSearchType(st) {
            $E("schType").value = st;
        }
        function getSearchType() {
            return $E("schType").value;
        }

        function onclick_btnSearchYi() {
            ipsh = $E("inputSearch");

            var hld = new HuangliYiji(); /* 不带参数的初始化 HuangliYiJi 实例 */
            g_cldr = HlCalendarClass.createNew();
            g_cldr.setBestDates(hld.getYiDates(ipsh.value));

            var m = getCurrentMonth();
            g_cldr.RebuildCalendar(m, true, false);
            setSearchType("yi");
        }

        function onclick_btnSearchJi() {
            ipsh = $E("inputSearch");

            var hld = new HuangliYiji(); /* 不带参数的初始化 HuangliYiJi 实例 */
            g_cldr = HlCalendarClass.createNew();
            g_cldr.setBadDates(hld.getJiDates(ipsh.value));

            var m = getCurrentMonth();
            g_cldr.RebuildCalendar(m, false, true);
            setSearchType("ji");
        }

        function getHuangliDataFileName(year, month) {
            // 构造 2 位的月份字符串
            var fmt_month = '0' + month;
            fmt_month = fmt_month.substr(-2, 2);

            // 拼装Huangli 数据文件名称
            var baseUrl = "http://wenyue365.cn/huangdao/huanglidata/huangli/";

            var p = baseUrl + year + "-" + fmt_month + ".js";
            return p;
        }

        // 加载指定月份的黄历数据（JS文件）
        function loadHuangliMonthData(m, onReady) {
            var filePath = getHuangliDataFileName(m.getFullYear(), m.getMonth()+1);
            var hldata = new HuangliYiji(filePath, m.getFullYear(), m.getMonth()+1, onReady);

            return hldata;
        }

        function initPage()
        {
            QueryString.Initial(); // 初始化 QueryString 对象，用于处理查询参数

            dfkw = $E("defaultKeyword");
            ipsh = $E("inputSearch");
            evnExpl = $E("eventExplain");

            if (dfkw.value != null) {
                ipsh.value = dfkw.value; /* 设置缺省搜索关键字 */
                ipsh.title = dfkw.value;

                // 显示缺省关键字的解释
                var yjEvn = new YijiEventExplain("g_yijiEventsExplain");
                var expl = yjEvn.getEventExplain(ipsh.value);
                if (expl != null) {
                    evnExpl.innerHTML = ipsh.value + "：" + expl + "。";
                }
                else {
                    evnExpl.innerHTML = "";
                }
            }

            // 加载缺省的黄历数据（JS文件）
            var m = new Date();

            loadHuangliMonthData(m, function (e) {
                ipsh = $E("inputSearch");

                var hld = new HuangliYiji(); /* 不带参数的初始化 HuangliYiJi 实例 */
                g_cldr = HlCalendarClass.createNew();
                g_cldr.setBestDates(hld.getYiDates(ipsh.value));
                g_cldr.setBadDates(hld.getJiDates(ipsh.value));

                if (QueryString.GetValue("et") == "ji"){ // 根据查询参数，确定显示宜/忌日期
                    g_cldr.RebuildCalendar(m, false, true);
                    setSearchType("ji");
                }
                else {
                    g_cldr.RebuildCalendar(m, true, false);
                    setSearchType("yi");
                }
            })

            // 初始化按钮组
            var btnGroup = new ButtonGroup();
            btnGroup.addBtn(new ButtonState($E("btnYiSearch"), 1));
            btnGroup.addBtn(new ButtonState($E("btnJiSearch"), 0));

            // 初始化宜忌事件输入框及提示框
            initSearchBox("inputSearch", dfkw.value, "inputbox_focus", "inputbox_blur");
            initPromptBox("searchPromptBox", "inputSearch");

            // 事件处理函数：用户选中提示框中的宜/忌项
            function onclick_PromptEvent() {
                if (getSearchType() == "yi") {
                    onclick_btnSearchYi();
                }
                else {
                    onclick_btnSearchJi();
                }

                // 显示宜忌项的解释
                var ipsh = $E("inputSearch");
                var yjEvn = new YijiEventExplain("g_yijiEventsExplain");
                var expl = yjEvn.getEventExplain(ipsh.value);
                if (expl != null) {
                    evnExpl.innerHTML = ipsh.value + "：" + expl + "。";
                }
                else {
                    evnExpl.innerHTML = "";
                }
            }

            var jsnKws = g_yijiEvents;
            if (jsnKws) {
                fillSearchPromptData("searchPromptBox", "inputSearch", jsnKws, onclick_PromptEvent);
            }
        }

        /*---------- MAIN ---------*/
        // 定义全局变理/对象
        var g_cldr = null;
        var g_hldata = null;

        //: 判断网页是否加载完成   
        document.onreadystatechange = function () {
            if (document.readyState == "complete") {
                /* 初始化黄历名词解释 */
                initPage();
            }
        }
    </script> 
</body>
</html>
