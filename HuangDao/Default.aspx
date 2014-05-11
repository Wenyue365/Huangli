<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HuangDao._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>黄道吉日</title>
    <script type="text/javascript" name="baidu-tc-cerfication" src="http://apps.bdimg.com/cloudaapi/lightapp.js#49864d04284941d7c34281555b923388"></script><script type="text/javascript">window.bd && bd._qdc && bd._qdc.init({ app_id: '55224d2be49bce6da1435b51' });</script>
    <link href="./css/huangli.css" rel="stylesheet" />
    <link href="./css/default.css" rel="stylesheet" />
    <script src="jslib/mootools-core-1.4.5-full-nocompat-yc.js"></script>
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

       <script type="text/javascript">
           //本页需要的js基础函数
           // Browser check
           // var Browser = new Object(); // 这个对象实例会与 mootools 的内置 Browser 对象冲突

           Browser.ua = window.navigator.userAgent.toLowerCase();
           Browser.ie = /msie/.test(Browser.ua);
           Browser.moz = /gecko/.test(Browser.ua);

           //JsLoader
           var JsLoader = {
               load: function (sUrl, fCallback) {
                   var _script = document.createElement("script");
                   _script.setAttribute("type", "text/javascript");
                   _script.setAttribute("src", sUrl);
                   document.getElementsByTagName("head")[0].appendChild(_script);

                   if (Browser.ie) {
                       _script.onreadystatechange = function () {
                           if (this.readyState == "loaded" || this.readyState == "complete") {
                               fCallback();
                           }
                       };
                   }
                   else if (Browser.moz) {
                       _script.onload = function () {
                           fCallback();
                       };
                   }
                   else {
                       fCallback();
                   }
               }
           };
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
           //指定生肖
           function setRedirectCzod(v) {
               if (v == "" || v == "0") {
                   alert("请选择您的生肖");
                   return;
               }
               setcookie('default_shengxiao', v, true, "/");
               alert("指定成功，下次打开查询时系统会显示您现在指定的生肖！");
           }
           //初始化指定生肖
           function initRedirectCzod() {
               //var v=getcookie('default_shengxiao');
               var czod_id = getcookie('default_shengxiao');
               if (czod_id == "") {
                   return;
               }
               if (czod_id >= 1 && czod_id <= 12) {
                   document.getElementById("query_czod_bar").value = czod_id;
               }
           }
</script>

    <form id="form1" runat="server">
    </form>
    <div class="container">
        <div class="calender_panel">
            <div class="solar_calender">
                <!-- 公历年月日 -->
                <div class="solar_year_month"><span id="xSolarYear" runat="server"></span>年<span id="xSolarMonth" runat="server"></span>月</div>
                <div class="solar_date" id="xSolarDateDiv" onclick="return onclick_solarDate(event)"><span id="xSolarDate" runat="server"></span></div>
                <div class="weekday">星期<span id="xWeekday" runat="server"></span></div>
            </div>
            <div class="ancient_calender">
                <!--星期及古代历法-->
                <div class="lunar_month_day"> <span id="xLunarDate" runat="server"></span></div>
                <div class="ancient_ymdt">
                    <!--古历的年月日时及时宜、时忌-->
                    <div class="img_div"><img class="zodiac" alt="马年" src="./images/zodiac_horse.png" /></div>
                    <div class="ancient_year_month">
                        <div class="ancient_year"><span id="xAncientYear" runat="server"></span></div>
                        <div class="ancient_month"><span id="xAncientMonth" runat="server"></span></div>
                    </div>
                    <div class="ancient_day_time">
                        <div class="ancient_day"><span id="xAncientDay" runat="server"></span></div>
                        <div class="ancient_time"><span id="xAcientTime" runat="server"></span></div>
                    </div></div>
                <div class="ancient_yiji">
                    <div class="yi_time"><span id="xYiTime" runat="server"></span>时宜</div>
                    <div class="ji_time"><span id="xJiTime" runat="server"></span>时忌</div>
                </div>
            </div>
        </div>
        
        <div class="yi_container">
            <div class="title">宜</div>
            <div class="events_list"><span id="xYiEventsList" runat="server"></span></div>
        </div>
        <div class="ji_container">
            <div class="title bad">忌</div>
            <div class="events_list bad"><span id="xJiEventsList" runat="server"></span></div>
        </div>
        <div class="good_angel_yi">
            <div class="title">吉神宜趋</div>
            <div class="events_list"><span id="xGoodAngelYiEventsList" runat="server"></span></div>
        </div>
        <div class="bad_angel_ji">
            <div class="title bad">凶神忌趋</div>
            <div class="events_list bad"><span id="xEvilAngelJiEventsList" runat="server"></span></div>
        </div>
        <div class="peng_ji">
            <div class="title">彭祖百忌</div>
            <div class="events_list"><span id="xPengAvoidEventsList" runat="server"></span></div>
        </div>
        <div class="collide">
            <div class="title">冲</div>
            <div class="events_list"><span id="xCollideEventsList" runat="server"></span></div>
        </div>
        <div class="five_elem">
            <div class="title">五行</div>
            <div class="events_list"><span id="xFiveElemEventsList" runat="server"></span></div>
        </div>
    </div>

    <script type="text/javascript">
        /* 函数：从WY服务器获取SINA黄历信息 */
        function getSinaHlInfo(y, m, d) {
            var baseUrl = location.href;
            var i = baseUrl.lastIndexOf("/");
            baseUrl = baseUrl.substr(0, i);
            baseUrl = baseUrl + "/HuangdaoWS.asmx/getSinaHlInfo";

            var onSuccess_Handler = function (responseText) {
                var obj = JSON.decode(responseText);
                // 初始化页面元素的数据
                if (obj != null){
                    var hld = obj.d;
                    $("xLunarDate") .innerHTML =hld.m_lunarDate.Value;
                    $("xAncientYear") .innerHTML =hld.m_yearOrder.Value;
                    $("xAncientMonth") .innerHTML =hld.m_monthOrder.Value;
                    $("xAncientDay") .innerHTML =hld.m_dayOrder.Value;
                    CreateEventsListCtrls($("xFiveElemEventsList"),hld.m_fiveElem.Value);
                    CreateEventsListCtrls($("xCollideEventsList"),hld.m_collide.Value);
                    CreateEventsListCtrls($("xPengAvoidEventsList"),hld.m_pengAvoid.Value);
                    CreateEventsListCtrls($("xGoodAngelYiEventsList"),hld.m_goodAngelYi.Value);
                    CreateEventsListCtrls($("xEvilAngelJiEventsList"),hld.m_evilAngelJi.Value);
                    CreateEventsListCtrls($("xYiEventsList"),hld.m_Yi.Value);
                    CreateEventsListCtrls($("xJiEventsList"),hld.m_Ji.Value);
                    // hld.m_birthGod.Value
                    // hld.m_solarDate.Value
                    // hld.m_zodiac.Value
                }
            }

            var onFailure_Handler = function () {
                // $("lbDetail").set("text", "request error");
            }

            var onCompleted_Handler = function () {
                // 
            }

            var request = new Request({
                url: baseUrl,
                method: "post",
                data: JSON.encode({ year:y, month:m, day:d }),
                headers: { "Content-Type": "application/json; charset=utf8" },
                onSuccess: onSuccess_Handler,
                onFailure: onFailure_Handler,
                onComplete: onCompleted_Handler
            });

            request.send();
        }

        function CreateEventsListCtrls(parentCtrl, strEvents)
        {
            // Remove the old events
            var  children = parentCtrl.childNodes;
            for (var i = 0; i < children.length; i++)
            {
                parentCtrl.removeChild(children[i]);
            }

            // Add events
            var arrEventsList = strEvents.split(' ');

            for (var i=0; i < arrEventsList.length; i++)
            {
                var lnk = document.createElement("a");
                lnk.innerText = arrEventsList[i];
                parentCtrl.appendChild(lnk);
            }
        }

        /* 设置Events-list的onclick 函数 */
        function setEvents_OnClickHandler(y, m, d){
            var queryDate = y + "-" + m + "-" + d;
            var lnks = $$(".events_list a");
            for (var i = 0; i < lnks.length; i++) {
                lnks[i].onclick = function onclick_event() { window.location = "chooseDates.aspx?hlw=" + this.innerHTML + "&" + "hlm=" + queryDate; };
            }
        }
        </script>

<script type="text/javascript">
    //shortcut method
    var $ = function (s) {
        return (typeof s == "object") ? s : document.getElementById(s);
    };

</script>

<script type="text/javascript">
    // 处理用户跳转到前一天或下一天

    function goNextDay(i) {
        var xSolarYear = $("xSolarYear");
        var xSolarMonth = $("xSolarMonth");
        var xSolarDate = $("xSolarDate");

        var y = parseInt(xSolarYear.innerHTML, 10);
        var m = parseInt(xSolarMonth.innerHTML, 10);
        var d = parseInt(xSolarDate.innerHTML, 10);
        var nextDay = d + i;

        initSolarDate(y, m, nextDay);

        getSinaHlInfo(y, m, nextDay);
    }
    function onclick_solarDate(event) {
        var x = event.pageX - $('xSolarDateDiv').offsetLeft;
        var y = event.pageY - $('xSolarDateDiv').offsetTop;
        var Area = $("xSolarDateDiv");
        if (x > Area.offsetWidth / 2) {
            // mouse click on right area
            goNextDay(1);
        }
        else if (x < Area.offsetWidth / 2) {
            // mouse click on left area
            goNextDay(-1);
        }
    }
    
    // $("xSolarDateDiv").addEvent("onclick", onclick_solarDate);

    // 初始化当前日期按钮 
    function initCurrDate()
    {
        var now = new Date();
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var date = now.getDate();
        var week_day = now.getDay();

        var xSolarDate = $("xSolarDate");
        xSolarDate.innerHTML = date;
        xSolarDate.onclick = function () { }

        var xSolarYear = $("xSolarYear");
        xSolarYear.innerHTML = year;

        var xSolarMonth = $("xSolarMonth");
        xSolarMonth.innerHTML = month;

        var arrWeekdays = ["日","一","二","三","四","五","六"];
        var xWeekday = $("xWeekday");
        xWeekday.innerHTML = arrWeekdays[week_day];

        setEvents_OnClickHandler(year, month, date);
    }

    // 设置当前的公历时间
    function initSolarDate(y, m, d)
    {
        var dt = new Date(y, m-1, d);

        var xSolarYear = $("xSolarYear");
        xSolarYear.innerHTML = dt.getFullYear();

        var xSolarMonth = $("xSolarMonth");
        xSolarMonth.innerHTML = dt.getMonth()+1;

        var xSolarDate = $("xSolarDate");
        xSolarDate.innerHTML = dt.getDate();

        var arrWeekdays = ["日","一", "二", "三", "四", "五", "六"];
        var xWeekday = $("xWeekday");
        xWeekday.innerHTML = arrWeekdays[dt.getDay()];

        setEvents_OnClickHandler(dt.getFullYear(), dt.getMonth(), dt.getDate());
    }


    //根据时间初始化下拉框
    function initHLQuery() {
        var now = new Date();
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();

        //年
        var hl_year = $("hlyear");
        //目前是在2006-2010范围内
        if (year >= 2006 && year <= 2014) {
            hl_year.value = year;
        }
        //月
        var hl_month = $("hlmonth");
        if (month >= 1 && month <= 12) {
            hl_month.value = month;
        }
        //日
        var hl_day = $("hlday");
        if (day >= 1 && day <= 31) {
            hl_day.value = day;
        }
    }

    
    //从Tencent拉取黄历数据
    function loadhlInfo() {
        var urlFID = 'http://cgi.data.astro.qq.com/astro/query.php?act=hl';
        urlFID += '&qyear=' + $("hlyear").value + '&qmonth=' + $("hlmonth").value + '&qday=' + $("hlday").value;

        JsLoader.load(urlFID, function () {
            //拿到FID
            if (typeof hlJsonFID != "undefined" && hlJsonFID != "") {
                var urlJson = 'http://data.astro.qq.com/hl/' + Math.floor(hlJsonFID / 1000) + "/" + hlJsonFID + "/info.js";

                // 请求黄历详情数据
                JsLoader.load(urlJson, function () {
                    if (typeof hlInfo != "undefined") {
                        var showtime =  hlInfo["F1"] + "-" + hlInfo["F2"] + "-" + hlInfo["F3"];
                        // 农历日期数据展示
                        var arrLunarDates = hlInfo["F4"].split(" ");
                        var lunar_date = arrLunarDates[0];
                        var lunar_date_ac = arrLunarDates[2];

                        $("hlNongLi").title = lunar_date;
                        $("hlNongLi").innerHTML = lunar_date;

                        $("hlNongLi_AC").title = lunar_date_ac;
                        $("hlNongLi_AC").innerHTML = lunar_date_ac;

                        $("xLunarDate").innerHTML = lunar_date;

                        //宜、忌 加起来为3行，优先显示 “宜” 最多2行

                            var oKeywords = $("keywords_yi");
                            var arrYi = new Array();
                            var strYi = hlInfo["F6"];
                            arrYi = strYi.split(" ");

                            for (var i = 0; i < arrYi.length; i++) {
                                var oSpan = document.createElement("span");
                                oSpan.innerHTML = arrYi[i];
                                oSpan.onclick = function onclick_word() { window.location = "chooseDates.aspx?hlw=" + this.innerHTML + "&" + "hlm=" + showtime; };
                                oKeywords.appendChild(oSpan);
                            }

                        //处理 忌
                        var oKeywords = $("keywords_ji");
                        var arrYi = new Array();
                        var strYi = hlInfo["F7"];
                        arrYi = strYi.split(" ");

                        for (var i = 0; i < arrYi.length; i++) {
                            var oSpan = document.createElement("span");
                            oSpan.innerHTML = arrYi[i];
                            oSpan.onclick = function onclick_word() { window.location = "chooseDates.aspx?hlw=" + this.innerHTML + "&" + "hlm=" + showtime; };
                            oKeywords.appendChild(oSpan);
                        }
                      
                        
                    } else {
                        alert("暂无该数据，稍后填充，请随时关注最新运势。");
                    }

                }); //结束：请求黄历详情数据

            } else {
                alert("暂无该数据，稍后填充，请随时关注最新运势。");
            }
        });
    }
   
    //初始化操作
    initCurrDate();
    //initHLQuery();
   
</script>
         
</body>
</html>
