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
    <link href="./css/default.css" rel="stylesheet" />
    <link href="./css/huangli.css" rel="stylesheet" />
    <link href="./css/default.css" rel="stylesheet" />
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
           //Browser check
           var Browser = new Object();

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
    <div class="bd modBody">
    <div style="float:left; ">
    <div id="hlcx">
        <div class="cls_headerctrl">
            <span id="btn_hl" >万年黄历</span>
            <div class="cls_currdatectrl">
                <span id="btn_leftarrow"><</span>
                <span id="lkbtn_solardate" >1日</span>
                <span id="btn_rightarrow">></span>
            </div>
           <!-- <span><a href="./BestToDo.aspx" id="btn_yi" >宜</a></span>
            <span><a href="./BadToDo.aspx" id="btn_ji" >忌</a></span> -->
        </div>

        <div class="bd modBody2" style="clear:both">
          <div class="cont">
                              <div class="cls_solardate"> 
                <b>日期:</b><strong>
                <select id="hlyear" class="w50" onchange="onchange_SelectYear()">
							<option value="2006">2006</option>
							<option value="2007">2007</option>
							<option value="2008">2008</option>
							<option value="2009">2009</option>
							<option value="2010">2010</option>
                            <option value="2011">2011</option>
                            <option value="2012">2012</option>
                            <option value="2013">2013</option>
                            <option value="2014">2014</option>
						</select>
                <select id="hlmonth" class="w50" onchange="onchange_SelectMonth()">
							<option value="1">01</option>
							<option value="2">02</option>
							<option value="3">03</option>
							<option value="4">04</option>
							<option value="5">05</option>
							<option value="6">06</option>
							<option value="7">07</option>
							<option value="8">08</option>
							<option value="9">09</option>
							<option value="10">10</option>
							<option value="11">11</option>
							<option value="12">12</option>
						</select>
                <select id="hlday" class="w50" onchange="onchange_SelectDay()">
							<option value="1">01</option>
							<option value="2">02</option>
							<option value="3">03</option>
							<option value="4">04</option>
							<option value="5">05</option>
							<option value="6">06</option>
							<option value="7">07</option>
							<option value="8">08</option>
							<option value="9">09</option>
							<option value="10">10</option>
							<option value="11">11</option>
							<option value="12">12</option>
							<option value="13">13</option>
							<option value="14">14</option>
							<option value="15">15</option>
							<option value="16">16</option>
							<option value="17">17</option>
							<option value="18">18</option>
							<option value="19">19</option>
							<option value="20">20</option>
							<option value="21">21</option>
							<option value="22">22</option>
							<option value="23">23</option>
							<option value="24">24</option>
							<option value="25">25</option>
							<option value="26">26</option>
							<option value="27">27</option>
							<option value="28">28</option>
							<option value="29">29</option>
							<option value="30">30</option>
							<option value="31">31</option>
						</select>
                  <span id="hlShowTime" style="visibility:hidden">
                     <!-- 当前日期 -->
                 </span></strong>

                </div>
                <div class="cls_lunardate">
                  <b>农历:</b>
                  <strong>
                      <span id="hlNongLi" title="二○一四年三月十五">
                          <!-- 农历日期 -->
                      </span>
                      <span id="hlNongLi_AC" title="甲午年戊辰月乙卯日">
                          <!-- 农历日期古代记法 -->
                      </span>
                  </strong>
                </div>
			  <ul class="content_list">
                  <li>
                      <strong class="yi">宜</strong>
                      <div id="keywords_yi">
                          <!--宜-->
                      </div>
                  </li>
                  <li>
                      <strong class="ji">忌</strong>
                      <div id="keywords_ji">
                          <!-- 忌-->
                      </div>
                  </li>
			  </ul>
               </div>
            </div>
        </div>
      </div>
    </div>

<script type="text/javascript">
    //shortcut method
    var $ = function (s) {
        return (typeof s == "object") ? s : document.getElementById(s);
    };

</script>
<script type="text/javascript">
/* Event handlers */
    /* by Jenseng */

    function onchange_SelectYear() {
        loadhlInfo();
    }

    function onchange_SelectMonth() {
        loadhlInfo();
    }

    function onchange_SelectDay() {
        loadhlInfo();
    }

</script>
<script type="text/javascript">

    // 初始化当前日期按钮 
    function initCurrDate()
    {
        var now = new Date();
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();

        var lkbtn_solardate = $("lkbtn_solardate");
        lkbtn_solardate.innerHTML = day + "日";
        lkbtn_solardate.onclick = function(){}
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

    //拉取黄历数据
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
                        //字数大于13便成2行，30成3行
                        var arrLunarDates = hlInfo["F4"].split(" ");
                        var lunar_date = arrLunarDates[0];
                        var lunar_date_ac = arrLunarDates[2];

                        $("hlNongLi").title = lunar_date;
                        $("hlNongLi").innerHTML = lunar_date;

                        $("hlNongLi_AC").title = lunar_date_ac;
                        $("hlNongLi_AC").innerHTML = lunar_date_ac;

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
    initHLQuery();
    loadhlInfo();
   
</script>
         
</body>
</html>
