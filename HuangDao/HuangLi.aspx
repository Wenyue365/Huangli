<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuangLi.aspx.cs" Inherits="HuangDao.HuangLi" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <title>问曰 - 黄历</title>
    <script type="text/javascript" name="baidu-tc-cerfication" src="http://apps.bdimg.com/cloudaapi/lightapp.js#49864d04284941d7c34281555b923388"></script><script type="text/javascript">window.bd && bd._qdc && bd._qdc.init({ app_id: '55224d2be49bce6da1435b51' });</script>
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="jslib/lzclib.js"></script>
    <script type="text/javascript" src="jslib/Solar2Lunar-mini.js"></script>
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/frame.css" rel="stylesheet" />
    <link href="./css/huangli.css" rel="stylesheet" />
    <link href="./css/HuangLi2.css" rel="stylesheet" />
    <style type="text/css">

    .cls_solardate{
	clear:both;
    padding-left: 3px;
    margin-bottom:3px;
    line-height:1em;
	color:#f99478;
    }

    .cls_solardate select {
	    color:#f99478;
	    border-color:#f99478;
        border-width:1px;
	    padding-left:3px;
    }

    .cls_lunardate{
	    clear:both;
        padding-left: 3px;
        margin-bottom:3px;
        line-height:1em;
	    color:#f99478;
    }

.cls_lunardate span{
	display:inline-block;
	padding:4px 0px 4px 0px;
	margin:3px;
}

.cls_lunardate span:hover{
    background-color: #feecec;
    color:white;
}

#hlNongLi_AC {
	color:#77ade3;
	font-weight:normal;
	font-size:0.6em;
}

#hlNongLi_AC:hover{
    background-color: #dee6ee;
    color:white;
}

/* -----  default.css -------*/
.content_list li {
    clear: both;
    display: inline-block;
    margin-bottom: 11px;
}

.cls_headerctrl {
   height:60px;
   padding:3px;
   font-size:2em;
   font-weight:bold;
   margin-bottom:10px;
   margin-left:5px;
   display:inline-block;

}
.cls_headerctrl span {
    display:inline-block;
    float:left;
    padding-top:8px;
    padding-left:8px;
    padding-right:8px;
    padding-bottom:3px;
    margin-bottom:8px;
}
.cls_currdatectrl{
    margin-left:8px;
    height:40px;
    display:inline-block;
}
.cls_currdatectrl span{
    display:inline-block;
    background-color:#fff0c2;
}

#btn_hl{
    background-color: #ffcc33;
    font-size:1em;
}
#btn_hl:hover {
    background-color: rgb(255, 255, 0);
}
#btn_yi{
    display:inline-block;
}
#btn_ji{
    display:inline-block;
}

#btn_leftarrow {
background-image: url("./images/hd_pics.png");
background-position: 0px 0px;
background-repeat: no-repeat;
background-color: transparent;
color: transparent;
}

#btn_rightarrow{
background-image: url("./images/hd_pics.png");
background-position: -97px 0px;
background-repeat: no-repeat;
background-color: transparent;
color: transparent;
}

#lkbtn_solardate{
    border-radius:8px;
    padding-left:12px;
    padding-top:7px;
    padding-bottom:5px;
    font-family:Consolas;
}
</style>
</head>
<body>

    <div class="body_container">
        <div class="calender_panel">
            <div class="solar_calender">
                <!-- 公历年月日 -->
                <div class="solar_year_month">
                    <span id="xSolarYear" runat="server"></span>年
                    <span id="xSolarMonth" runat="server"></span>月
                    <span id="xSolarDate" runat="server"></span>日 
                    星期<span id="xWeekday" runat="server"></span>
                </div>
                <div class="solar_date" id="xLunarDateDiv" onclick="return onclick_LunarDate(event)"><span id="xLunarDay">初八</span></div>

            </div>
            <div class="ancient_calender">
                <!--星期及古代历法-->
                <div class="lunar_month_day"> <span id="xLunarDate" runat="server"></span></div>
                <div class="ancient_ymdt">
                    <!--古历的年月日时及时宜、时忌-->
                    <%--<div class="img_div"><img class="zodiac" alt="马年" src="./images/zodiac_horse.png" /></div>--%>
                    <div class="ancient_year_month">
                        <div class="ancient_year"><span id="xAncientYear" runat="server"></span></div>
                        <div class="ancient_month"><span id="xAncientMonth" runat="server"></span></div>
                    </div>
                    <div class="ancient_day_time">
                        <div class="ancient_day"><span id="xAncientDay" runat="server"></span></div>
                        <div class="ancient_time"><span id="xAcientTime" runat="server"></span></div>
                    </div></div>
                <div class="ancient_yiji">
                    <div class="yi_time"><span>时宜</span><span id="xYiTime" runat="server"></span></div>
                    <div class="ji_time"><span>时忌</span><span id="xJiTime" runat="server"></span></div>
                </div>
            </div>
        </div>
        
        <div class="yi_container">
            <div class="title single_line">宜</div>
            <div class="events_list"><span id="xYiEventsList" runat="server"></span></div>
        </div>
        <div class="ji_container">
            <div class="title bad single_line">忌</div>
            <div class="events_list bad"><span id="xJiEventsList" runat="server"></span></div>
        </div>
        <div class="bottom_container">

        </div>
    </div>
    <!-- 底部导航栏 -->
    <div class="bottom_nav_toolbar" id="bottomNavToolbar">
        <ul>
            <li><a href="./huangli.aspx">黄历</a></li>
            <li><a href="./BestTodo.aspx">宜忌</a></li>
            <li><a href="./Divine.aspx">测算</a></li>
            <li><a href="./love.aspx">姻缘</a></li>
        </ul>
    </div>

    <script type="text/javascript">
 
        /*函数：从WY 服务器获取LoaHuangLi 的时辰宜忌信息*/
        function getLaoHLHourInfo(y, m, d, h) {
            var baseUrl = location.href;
            var i = baseUrl.lastIndexOf("/");
            baseUrl = baseUrl.substr(0, i);
            baseUrl = baseUrl + "/HuangdaoWS.asmx/getLaoHLHour";

            // 定义事件处理函数：请求数据成功
            function onSuccess_Handler(xml) {
                // 初始化页面元素的数据
                if (xml != null) {
                    $E("xYiTime").innerHTML = trimString($T(xml, "m_well_timed"), 8);
                    $E("xJiTime").innerHTML = trimString($T(xml, "m_bad_timed"), 8);
                }
            }

            $.ajax({
                url: baseUrl,
                type: 'POST',
                dataType: "xml",
                data: {"year": y, "month": m, "day": d, "hour": h }, /* POST DATA */
                success: onSuccess_Handler
            }); /* End of AJAX Request */
        }

        /* 函数：从WY服务器获取SINA黄历信息 */
        function getSinaHlInfo(y, m, d) {
            var baseUrl = location.href;
            var i = baseUrl.lastIndexOf("/");
            baseUrl = baseUrl.substr(0, i);
            baseUrl = baseUrl + "/HuangdaoWS.asmx/getSinaHlInfo";

            function onSuccess_Handler(xml) {
                // 初始化页面元素的数据
                if (xml != null) {
                    $E("xLunarDate").innerHTML = $T(xml, "m_lunarDate");
                    $E("xAncientYear").innerHTML = $T(xml, "m_yearOrder");
                    $E("xAncientMonth").innerHTML = $T(xml, "m_monthOrder");
                    $E("xAncientDay").innerHTML = $T(xml, "m_dayOrder");

                    CreateEventsListCtrls($E("xYiEventsList"), $T(xml, "m_Yi"));
                    CreateEventsListCtrls($E("xJiEventsList"), $T(xml, "m_Ji"));

                    setEvents_OnClickHandler(y, m, d);
                }
            }

            $.ajax({
                url: baseUrl,
                type: 'POST',
                dataType: 'xml',
                data: { "year": y, "month": m, "day": d }, /* 设定carNum 的值 */
                success: onSuccess_Handler
            }); /* End of AJAX Request */
        }

        function sortEvents(events)
        {
            for(var i=0, len = events.length; i < len; i++)
            {
                // 把包含 * 的元素移动到数组最后端（右端）
                events.sort(function (x, y) { return x.indexOf('*') >= 0; });
                // 删除含 * 的元素
                for (var i = 0, len = events.length; i < len; i++) {
                    if (events[i].indexOf('*') >= 0) {
                        events.length = i;
                        break;
                    }
                }
            }
            return events;
        }

        function CreateEventsListCtrls(parentCtrl, strEvents)
        {
            var MAX_EVENT_COUNT = 18;
            // Remove the old events
            while (parentCtrl.firstChild)
            {
                parentCtrl.removeChild(parentCtrl.firstChild);
            }

            // Add events
            var arrEventsList = strEvents.split(' ');
            arrEventsList = sortEvents(arrEventsList);

            for (var i = 0; i < arrEventsList.length && i < MAX_EVENT_COUNT; i++)
            {
                var lnk = document.createElement("a");
                if (lnk.innerText != null)
                {
                    lnk.innerText = arrEventsList[i];
                }
                else
                {
                    lnk.innerHTML = arrEventsList[i];
                }
                parentCtrl.appendChild(lnk);
            }
        }

        /* 设置Events-list的onclick 函数 */
        function setEvents_OnClickHandler(y, m, d){
           /* var queryDate = y + "-" + m + "-" + d;
            var lnks = $(".events_list a");
            for (var i = 0; i < lnks.length; i++) {
                lnks[i].target_url = "BestToDo.aspx?hlw=" + lnks[i].innerHTML + "&" + "hlm=" + queryDate; 
                lnks[i].onclick = function onclick_event() {
                    window.location = this.target_url;
                };
            }
            */
            var yiLnks = $("#xYiEventsList a");
            var jiLnks = $("#xJiEventsList a");
            setCalendarClickEventHandler(yiLnks, y, m, d, false);
            setCalendarClickEventHandler(jiLnks, y, m, d, true);
        }

        function setCalendarClickEventHandler(lnks, y, m, d, bJi) {
            var queryDate = y + "-" + m + "-" + d;
            for (var i = 0; i < lnks.length; i++) {
                lnks[i].target_url = "BestToDo.aspx?hlw=" + lnks[i].innerHTML + "&" + "hlm=" + queryDate;
                if (bJi) {
                    lnks[i].target_url += "&et=ji";
                }
                lnks[i].onclick = function onclick_event() {
                    window.location = this.target_url;
                };
            }
        }

    </script>

    <script type="text/javascript">

</script>

    <script type="text/javascript">
        // 处理用户跳转到前一天或下一天
        function goNextDay(i) {
            var xSolarYear = $E("xSolarYear");
            var xSolarMonth = $E("xSolarMonth");
            var xSolarDate = $E("xSolarDate");

            var y = parseInt(xSolarYear.innerHTML, 10);
            var m = parseInt(xSolarMonth.innerHTML, 10);
            var d = parseInt(xSolarDate.innerHTML, 10);
            var nextDay = d + i;

            // 1: load data from server
            getSinaHlInfo(y, m, nextDay);

            var curr_date = new Date();
            getLaoHLHourInfo(y, m, nextDay, curr_date.getHours());

            // 2: initialize page elements
            initSolarDate(y, m, nextDay);
            initLunarDare(y, m, nextDay);
        }

        function initLunarDare(y, m, d)
        {
            var lnif = GetLunarInfo(y, m, d);
            if (lnif != null)
            {
                $E("xLunarDay").innerHTML = lnif.day;
            }
        }

        function onclick_LunarDate(event) {
            var x = event.pageX - $E("xLunarDateDiv").offsetLeft;
            var y = event.pageY - $E("xLunarDateDiv").offsetTop;
            var Area = $E("xLunarDateDiv");
            if (x > Area.offsetWidth / 2) {
                // mouse click on right area
                goNextDay(1);
            }
            else if (x < Area.offsetWidth / 2) {
                // mouse click on left area
                goNextDay(-1);
            }
        }
        
        // 函数：用于获取指定ID 的元素对象
        function $E(id) {
            return document.getElementById(id);
        }

        function formatDateString(strDate) {
            if (strDate == null) return strDate;
            var elm = strDate.split('-');
            for (var i = 0; i < elm.length; i++) {
                if (elm[i].length < 2) {
                    elm[i] = '0' + elm[i];
                }
            }
            return (elm.join('-'));
        }

        // 初始化当前日期按钮 
        function initCurrDate()
        {
            var now = new Date();

            // Try to get initial date time from query-string
            QueryString.Initial();
            strDate = QueryString.GetValue("hld");

            // Try the get date from url
            if (strDate == null) {
                ptnDate = /\d{4}\-\d{1,2}\-\d{1,2}/;
                var grp = document.URL.match(ptnDate);
                if (grp != null) {
                    strDate = grp[0];
                }
            }

            if (strDate != null)
            {
                strDate = formatDateString(strDate);
                var tmpDate = new Date(strDate);
                if (!isNaN(tmpDate)) {
                    now = tmpDate;
                }
            }

            var year = now.getFullYear();
            var month = now.getMonth() + 1;
            var date = now.getDate();
            var week_day = now.getDay();

            var xSolarDate = $E("xSolarDate");
            xSolarDate.innerHTML = date;
            xSolarDate.onclick = function () { }

            var xSolarYear = $E("xSolarYear");
            xSolarYear.innerHTML = year;

            var xSolarMonth = $E("xSolarMonth");
            xSolarMonth.innerHTML = month;

            var arrWeekdays = ["日","一","二","三","四","五","六"];
            var xWeekday = $E("xWeekday");
            xWeekday.innerHTML = arrWeekdays[week_day];

            initLunarDare(year, month, date);

            setEvents_OnClickHandler(year, month, date);

        }

        // 设置当前的公历时间
        function initSolarDate(y, m, d)
        {
            var dt = new Date(y, m-1, d);

            var xSolarYear = $E("xSolarYear");
            xSolarYear.innerHTML = dt.getFullYear();
            xSolarYear.d = dt.getFullYear();

            var xSolarMonth = $E("xSolarMonth");
            xSolarMonth.innerHTML = dt.getMonth() + 1;
            xSolarMonth.d = dt.getMonth() + 1;

            var xSolarDate = $E("xSolarDate");
            xSolarDate.innerHTML = dt.getDate();
            xSolarDate.d = dt.getDate();

            var arrWeekdays = ["日","一", "二", "三", "四", "五", "六"];
            var xWeekday = $E("xWeekday");
            xWeekday.innerHTML = arrWeekdays[dt.getDay()];
            xWeekday.d = arrWeekdays[dt.getDay()];
        }

        //根据时间初始化下拉框
        function initHLQuery() {
            var now = new Date();
            var year = now.getFullYear();
            var month = now.getMonth() + 1;
            var day = now.getDate();

            //年
            var hl_year = $E("hlyear");
            //目前是在2006-2010范围内
            if (year >= 2006 && year <= 2014) {
                hl_year.value = year;
            }
            //月
            var hl_month = $E("hlmonth");
            if (month >= 1 && month <= 12) {
                hl_month.value = month;
            }
            //日
            var hl_day = $E("hlday");
            if (day >= 1 && day <= 31) {
                hl_day.value = day;
            }
        }
   
        //初始化操作
        initCurrDate();

   
    </script>
         
</body>
</html>
