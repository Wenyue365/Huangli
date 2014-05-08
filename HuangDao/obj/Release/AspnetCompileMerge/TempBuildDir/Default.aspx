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
    <link href="./huangli.css" rel="stylesheet" />
    <style type="text/css">
         #keywords_yi {
                    clear:both;
                }
                #keywords_yi span{
                    background-color:#feecec;
                    color:#ff0000;
                    margin:1px 4px 3px 0px;
                    padding:8px 6px 6px 6px;
                    font-size:0.8em;
                    font-weight:bold;
                    display:block;
                    float:left;
                    line-height:1em;
                }

                #keywords_yi span:hover{
                    background-color:#ff0000;
                    color:#feecec;
                }

                #keywords_ji {
                    clear:both;
                }
                #keywords_ji span{
                    background-color:#dee6ee;
                    color:#0051a2;
                    margin:1px 4px 3px 0px;
                    padding:8px 6px 6px 6px;
                    font-size:0.8em;
                    font-weight:bold;
                    display:block;
                    float:left;
                    line-height:1em;
                }
                #keywords_ji span:hover {
                    background-color:#0051a2;
                    color:#dee6ee;
                }
                .content_list li {
                    clear:both;
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
    <div class="rgtCol rgtMod" style="float:left; ">
    <div id="hlcx">
        <div class="hd modTitle5">
            <h2><a href="#" id="btn_hl" >万年黄历</a></h2>
            <h2><a href="./BestToDo.aspx" id="btn_yi" >宜</a></h2>
            <h2><a href="./BadToDo.aspx" id="btn_ji" >忌</a></h2>
        </div>
        <div class="bd modBody2">
          <div class="cont">
            <dl>
              <dd> <b>日期:</b><strong>
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
                当前日期
                 </span></strong>
			  <ul class="content_list">
			  	<li><b>农历:</b><span id="hlNongLi" title="二○一四年三月十五  甲午年戊辰月乙卯日">二○一四年三月十五  甲午年戊辰月乙卯日</span></li>
				<li>
					<strong class="yi"> 宜</strong>
                            <div id="keywords_yi"><!--宜--></div>
				</li>
				<li>
					<strong class="ji"> 忌</strong>
				    <div id="keywords_ji"><!-- 忌--></div>
                </li>
			  </ul>
                </dd>
            </dl>
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
                        //数据展示
                        //字数大于13便成2行，30成3行
                        $("hlNongLi").title = hlInfo["F4"];
                        if (hlInfo["F4"].length > 30) {
                            $("hlNongLi").innerHTML = hlInfo["F4"];//.substr(0, 32) + "...";//字数做限制，最多2行
                        } else {
                            $("hlNongLi").innerHTML = hlInfo["F4"];
                        }

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
    initHLQuery();
    loadhlInfo();
   
</script>
         
</body>
</html>
