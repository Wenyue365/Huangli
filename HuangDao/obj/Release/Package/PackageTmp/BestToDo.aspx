<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BestToDo.aspx.cs" Inherits="HuangDao.BestToDo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <script src="calendar.js"></script>
    <link href="./huangli.css" rel="stylesheet" />
    <script src="jslib/mootools-core-1.4.5-full-nocompat-yc.js"></script>
    <title>宜</title>
</head>
<body>
     <style type="text/css">
                #hlShowTime{

                }
                #keywords_yi {
                    clear:both;
                }
                #keywords_yi span{
                    background-color:#feecec;
                    color:#ff0000;
                    margin:4px 5px 4px 0px;
                    padding:5px 5px 3px 5px;
                    font-size:0.8em;
                    font-weight:bold;
                    display:block;
                    float:left;
                }
                #btn_yi:hover {
                    color: #000;
                    background-color: #FF0;
                }
                #btn_yi {
                    color: #FFF;
                    background-color: #FF7474;
                    font-size:1.5em;
                    padding: 3px 5px;
                    width:32px;
                    height:32px;
                }
                #txbDetail{
                    min-width:150px;
                    min-height:50px;
                    border:solid 1px;
                    border-color:#FEECEC;
                    padding:0.2em;
                    font-size:0.8em;
                    color:#FF7474;
                }
                #btnNextMonth{


                }
                .main_panel{
                    margin:9px 9px;

                }
                .yi_title{
                    margin-bottom:3px;
                    height:50px;
                }
                .yi_title div{
                    float:left;
                }
                .show_time{
                    font-size:0.8em;
                    margin-left:8px;
                    padding:0.3em;
                    color:red;
                    background-color:#feecec;
                }
                .keyword_detail{
                    clear:both;
                }
            </style>
    <form id="form1" runat="server">
        <div class="main_panel">
        <div class="yi_title">
            <div id="btn_yi" onclick="onclick_Yi()" ><a href="#" >宜</a></div>
            <div class="show_time"><span id="hlShowTime">2014年4月</span></div>
            <div class="show_time"><span id="btnPrevMonth" onclick="onclick_PrevMonth()" style="display:none">上月</span></div>
            <div class="show_time"><span id="btnNextMonth" onclick="onclick_NextMonth()">下月</span></div>
            <script type="text/javascript">
                function onclick_NextMonth()
                {
                    var btnCurrMonth = $("hlShowTime");
                    var currDate = btnCurrMonth.d;
                    if (currDate != null)
                    {
                        currDate.setMonth(currDate.getMonth()+1, currDate.getDate());
                        btnCurrMonth.innerHTML = currDate.getFullYear() + "年" + (currDate.getMonth() + 1) + "月";
                        btnCurrMonth.d = currDate;

                        loadHlInfoEx(currDate.getFullYear(), currDate.getMonth() + 1, currDate.getDate());

                        $("btnPrevMonth").setStyle("display", "block");
                    }
                }

                function onclick_PrevMonth()
                {
                    var btnCurrMonth = $("hlShowTime");
                    var currDate = btnCurrMonth.d;
                    if (currDate != null) {
                        currDate.setMonth(currDate.getMonth()-1, currDate.getDate());
                        btnCurrMonth.innerHTML = currDate.getFullYear() + "年" + (currDate.getMonth() + 1) + "月";
                        btnCurrMonth.d = currDate;

                        loadHlInfoEx(currDate.getFullYear(), currDate.getMonth() + 1 , currDate.getDate());
                    }
                }
            </script>
        </div>
                       <!--开始：黄道名词列表 -->     
        <div id="keywords_yi">
            <!--span id="hlYi">安居</span!-->
           
        </div>
            <!--结束：黄道名词列表 -->
            <!--开始：黄历名词解释 -->
            <div class="keyword_detail">
                <div id="txbDetail" ></div>
            </div>
            <script type="text/javascript">
                /* Add onclick event handlers to words */

            </script>
            <!--结束：黄历名词解释 -->

           <!--开始：日历控件-->
            <style type="text/css">
                #CalBody {
                    width: 168px;
                    margin-top: 12px;
                }
            </style>
            <div id="CalBody">
                <script type="text/javascript">InitCalendar();</script>    
                </div>
            <!-- 结束：日历控件 -->

            </div>
    </form>

    <script type="text/javascript">
        //本页需要的js基础函数
        //Browser check
        /*var Browser = new Object();*/

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

        //拉取黄历数据
        function loadhlInfo() {
            var urlFID = 'http://cgi.data.astro.qq.com/astro/query.php?act=hl';
            var newDate = new Date();
            hlyear = newDate.getFullYear();
            hlmonth = newDate.getMonth() + 1;
            hlday = newDate.getDate();

            urlFID += '&qyear=' + hlyear + '&qmonth=' + hlmonth + '&qday=' + hlday;

            JsLoader.load(urlFID, function () {
                //拿到FID
                if (typeof hlJsonFID != "undefined" && hlJsonFID != "") {
                    var urlJson = 'http://data.astro.qq.com/hl/' + Math.floor(hlJsonFID / 1000) + "/" + hlJsonFID + "/info.js";

                    JsLoader.load(urlJson, function () {
                        if (typeof hlInfo != "undefined") {
                            $("hlShowTime").innerHTML = hlInfo["F1"] + "年" + hlInfo["F2"] + "月"; 
                            var currDate = new Date();
                            currDate.setYear(parseInt(hlInfo["F1"],10), parseInt(hlInfo["F2"],10), parseInt(hlInfo["F3"],10));

                            $("hlShowTime").d = currDate;

                            //显示 “宜” 最多2行
                            /*$("hlcx")
                            $("hlYi").title = hlInfo["F6"];
                            if (hlInfo["F6"].length > 30) {
                                $("hlYi").innerHTML = hlInfo["F6"].substr(0, 32) + "...";//字数做限制，最多2行
                            } else {
                                $("hlYi").innerHTML = hlInfo["F6"];
                            }
                            */
                            var oKeywords = $("keywords_yi");
                            var arrYi = new Array();
                            var strYi = hlInfo["F6"];
                            arrYi = strYi.split(" ");

                            for (var i = 0; i < arrYi.length; i++) {
                                var oSpan = document.createElement("span");
                                oSpan.innerHTML = arrYi[i];
                                oSpan.onclick = onclick_word;
                                oKeywords.appendChild(oSpan);
                            }

                        } else {
                            alert("暂无该数据，稍后填充，请随时关注最新运势。");
                        }
                    });

                } else {
                    alert("暂无该数据，稍后填充，请随时关注最新运势。");
                }
            });
        }

        /* 拉取指定日期的黄历数据 */
        function loadHlInfoEx(hlyear, hlmonth, hlday ) {
            var urlFID = 'http://cgi.data.astro.qq.com/astro/query.php?act=hl';
            urlFID += '&qyear=' + hlyear + '&qmonth=' + hlmonth + '&qday=' + hlday;

            JsLoader.load(urlFID, function () {
                //拿到FID
                if (typeof hlJsonFID != "undefined" && hlJsonFID != "") {
                    var urlJson = 'http://data.astro.qq.com/hl/' + Math.floor(hlJsonFID / 1000) + "/" + hlJsonFID + "/info.js";

                    JsLoader.load(urlJson, function () {
                        if (typeof hlInfo != "undefined") {
                           
                            var oKeywords = $("keywords_yi");
                            var arrYi = new Array();
                            var strYi = hlInfo["F6"];
                            arrYi = strYi.split(" ");

                            // remove all child elements
                            oKeywords.empty();


                            for (var i = 0; i < arrYi.length; i++) {
                                var oSpan = document.createElement("span");
                                oSpan.innerHTML = arrYi[i];
                                oSpan.onclick = onclick_word;
                                oKeywords.appendChild(oSpan);
                            }

                        } else {
                            alert("暂无该数据，稍后填充，请随时关注最新运势。");
                        }
                    });

                } else {
                    alert("暂无该数据，稍后填充，请随时关注最新运势。");
                }
            });
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
        /* 初始化页面数据 */
        loadhlInfo();

        /* 初始化黄历名词解释 */
        //getWordAbstract('祭祀');

    </script>
</body>
</html>
