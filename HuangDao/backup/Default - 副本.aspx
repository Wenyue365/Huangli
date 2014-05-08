<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HuangDao._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="width=device-width,user-scalable=no" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes">

    <title>黄道吉日</title>
    <script type="text/javascript" name="baidu-tc-cerfication" src="http://apps.bdimg.com/cloudaapi/lightapp.js#49864d04284941d7c34281555b923388"></script><script type="text/javascript">window.bd && bd._qdc && bd._qdc.init({ app_id: '55224d2be49bce6da1435b51' });</script>
    <link href="./huangli.css" rel="stylesheet" />
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

<script type="text/javascript">


    //生肖运势查询;为了以下面的公用，加入对应标签id作为参数
    //与产品速查 chanpinsc.htm 公用改函数
    function czodYsSearch(type, objectID) {
        if (objectID == "" || objectID == null) {
            return;
        }
        var czodID = document.getElementById(objectID).value;

        if (czodID == "" || czodID == "0") {
            alert("请选择生肖");
            return;
        }

        var now = new Date();
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();

        //存储星座相关的信息
        var czodInfo = {
            "Week": "weekczod",
            "Nweek": "weekczod",
            "Month": "monthczod",
            "Year": "yearczod"
        };

        //按天更新，且不能让js缓存太久
        var randStr = "" + now.getFullYear() + "" + (now.getMonth() + 1) + "" + now.getDate();

        var urljs = "http://data.astro.qq.com/czodinfo/" + czodID + "/todayCzod.js?" + randStr;

        if (typeof todayCzod == "undefined") {
            JsLoader.load(urljs, function () {
                if (typeof todayCzod != "undefined") {
                    //获得对应的FID
                    var FID = todayCzod[type + "FID"];
                    if (FID == null || FID == "") {
                        alert("暂无该数据，稍后填充，请随时关注最新运势");
                    } else {
                        var urlPage = "http://data.astro.qq.com/" + czodInfo[type] + "/" + Math.floor(FID / 1000);
                        urlPage += "/" + FID + "/index.shtml";
                        //location.href = urlPage;
                        window.open(urlPage);
                    }
                }
            });
        } else {
            //数据已经拉取成功
            var FID = todayCzod[type + "FID"];
            if (FID == null || FID == "") {
                alert("暂无该数据，稍后填充，请随时关注最新运势");
            } else {
                var urlPage = "http://data.astro.qq.com/" + czodInfo[type] + "/" + Math.floor(FID / 1000);
                urlPage += "/" + FID + "/index.shtml";
                window.open(urlPage);
            }
        }
    }

    //拉取生肖对应的js,为了以下面的公用，加入对应标签id作为参数
    //与产品速查 chanpinsc.htm 公用改函数
    function loadCzodYs(objectID) {
        if (objectID == "" || objectID == null) {
            return;
        }
        var czodID = document.getElementById(objectID).value;

        if (czodID == "" || czodID == "0") {
            return;
        }

        var now = new Date();
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();

        //按天更新，且不能让js缓存太久
        var randStr = "" + now.getFullYear() + "" + (now.getMonth() + 1) + "" + now.getDate();

        var urljs = "http://data.astro.qq.com/czodinfo/" + czodID + "/todayCzod.js?" + randStr;

        JsLoader.load(urljs, function () { });
    }

    initRedirectCzod();
    // commented by lzc loadCzodYs("query_czod_bar"); 
</script>
<!--new星座定制by annan#tencent.com-->
<script type="text/javascript">

    function changeMenu(msg, sequ) {
        if (!msg) return;
        var name = msg;
        setcookie('xz_name', name, true, '/');
        for (var j = 0; j < 12; j++) {
            document.getElementById('xzToday' + j).style.display = 'none';
            document.getElementById('xzWeek' + j).style.display = 'none';
            document.getElementById('xzMonth' + j).style.display = 'none';
            document.getElementById('xzYear' + j).style.display = 'none';
            if (j == sequ) {

                document.getElementById('xzToday' + j).style.display = 'block';
                document.getElementById('xzWeek' + j).style.display = 'block';
                document.getElementById('xzMonth' + j).style.display = 'block';
                document.getElementById('xzYear' + j).style.display = 'block';
            }
        }
    }

    function dzxzFn() {

        var clast = getcookie('xz_name') || '';
        if (clast == '') {
            var now = new Date();
            var month = now.getMonth() + 1;
            var day = now.getDate();

            switch (month) {
                case 3:
                    if (day <= 20) {
                        clast = 'xzn11';
                    } else {
                        clast = 'xzn0';
                    }
                    break;
                case 4:
                    if (day <= 20) {
                        clast = 'xzn0';
                    } else {
                        clast = 'xzn1';
                    }
                    break;
                case 5:
                    if (day <= 20) {
                        clast = 'xzn1';
                    } else {
                        clast = 'xzn2';
                    }
                    break;
                case 6:
                    if (day <= 21) {
                        clast = 'xzn2';
                    } else {
                        clast = 'xzn3';
                    }
                    break;
                case 7:
                    if (day <= 22) {
                        clast = 'xzn3';
                    } else {
                        clast = 'xzn4';
                    }
                    break;
                case 8:
                    if (day <= 22) {
                        clast = 'xzn4';
                    } else {
                        clast = 'xzn5';
                    }
                    break;
                case 9:
                    if (day <= 22) {
                        clast = 'xzn5';
                    } else {
                        clast = 'xzn6';
                    }
                    break;
                case 10:
                    if (day <= 22) {
                        clast = 'xzn6';
                    } else {
                        clast = 'xzn7';
                    }
                    break;
                case 11:
                    if (day <= 21) {
                        clast = 'xzn7';
                    } else {
                        clast = 'xzn8';
                    }
                    break;
                case 12:
                    if (day <= 21) {
                        clast = 'xzn8';
                    } else {
                        clast = 'xzn9';
                    }
                    break;
                case 1:
                    if (day <= 19) {
                        clast = 'xzn9';
                    } else {
                        clast = 'xzn10';
                    }
                    break;
                case 2:
                    if (day <= 20) {
                        clast = 'xzn10';
                    } else {
                        clast = 'xzn11';
                    }
                    break;
                default:

            }


        }

        if (clast) {
            document.getElementById(clast).selected = true;
            var num = clast.slice(3);
            for (var j = 0; j < 12; j++) {
                document.getElementById('xzToday' + j).style.display = 'none';
                document.getElementById('xzWeek' + j).style.display = 'none';
                document.getElementById('xzMonth' + j).style.display = 'none';
                document.getElementById('xzYear' + j).style.display = 'none';
                if (j == num) {
                    document.getElementById('xzToday' + j).style.display = 'block';
                    document.getElementById('xzWeek' + j).style.display = 'block';
                    document.getElementById('xzMonth' + j).style.display = 'block';
                    document.getElementById('xzYear' + j).style.display = 'block';
                }
            }
        }
    }

    // commented by lzc dzxzFn();
</script>
      </div>
    </div>
    <!--广告-->
    
    </div>
	<script>try { QosS.topSpan("astro_index", 5); } catch (e) { }</script>
	<script src="http://mat1.gtimg.com/joke/js/comm/Qfast.js" type="text/javascript"></script>
<script type="text/javascript">
    Qfast.add('common', { path: "http://mat1.gtimg.com/news/base2011/ued_common.js", type: "js" });
    Qfast.add('focus', { path: "http://mat1.gtimg.com/news/base2011/foucs/ued_foucs.js", type: "js", requires: ['common'] });
    Qfast.add('tabcard', { path: "http://news.qq.com/base2011/ued_tabcard.js", type: "js", requires: ['common'] });
    Qfast.add('scroll', { path: "http://news.qq.com/base2011/ued_scroll.js", type: "js", requires: ['common'] });
</script>
<!--焦点图-->

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <div class="bd modBody">
    <div class="rgtCol rgtMod" style="float:left">
    <div id="hlcx">
        <div class="hd modTitle5">
            <h2><a href="#" id="btn_hl" >万年黄历</a></h2>
            <h2><a href="#" id="btn_yi" >宜</a></h2>
            <h2><a href="#" id="btn_ji" >忌</a></h2>
        </div>
        <div class="bd modBody2">
          <div class="cont">
            <dl>
              
              <dd> <b>日期:</b><strong><span id="hlShowTime">2014-4-14</span></strong>
			  <ul>
			  	<li><b>农历:</b><span id="hlNongLi" title="二○一四年三月十五  甲午年戊辰月乙卯日">二○一四年三月十五  甲午年戊辰月乙卯日</span></li>
				<li>
					<strong class="yi"> 宜</strong><span id="hlYi" title="补垣塞穴">补垣塞穴</span>
				</li>
				<li>
					<strong class="ji"> 忌</strong><span id="hlJi" title="祈福 求嗣 上册受封 上表章 袭爵受封 会亲友 冠带 出行 上官赴任 临政亲民 结婚姻 纳采问名 嫁娶 进人口 移徙 安床 解除 剃头 整手足甲 求医疗病 裁衣 筑堤防 修造动土 竖柱上梁 修仓库 鼓铸 经络 酝酿 开市 立券 交易 纳财 开仓库出货财 修置产室 开渠穿井 安碓 修饰垣墙 平治道途 破屋坏垣 伐木 栽种 牧养 纳畜 破土 安葬 启攒">祈福 求嗣 上册受封 上表章 袭爵受封 会亲友 冠带 出...</span>
				</li>
			  </ul>
                </dd>

                <dt>
                <label>其他日期</label>
                <select id="hlyear" class="w50">
							<option value="2006">2006</option>
							<option value="2007">2007</option>
							<option value="2008">2008</option>
							<option value="2009">2009</option>
							<option value="2010">2010</option>
                            <option value="2011">2011</option>
                            <option value="2012">2012</option>
                            <option value="2013">2013</option>
                            <option value="2014">2014</option>
                            <option value="2014">2015</option>
                            <option value="2014">2016</option>
                            <option value="2014">2017</option>
                            <option value="2014">2018</option>
						</select>
                <select id="hlmonth" class="w50">
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
                <select id="hlday" class="w50">
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
                <a href="javascript:;" class="lookBtn" onclick="loadhlInfo(); return false;">查看</a> </dt>
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

                JsLoader.load(urlJson, function () {
                    if (typeof hlInfo != "undefined") {
                        $("hlShowTime").innerHTML = hlInfo["F1"] + "-" + hlInfo["F2"] + "-" + hlInfo["F3"];

                        //数据展示
                        //字数大于13便成2行，30成3行
                        $("hlNongLi").title = hlInfo["F4"];
                        if (hlInfo["F4"].length > 30) {
                            $("hlNongLi").innerHTML = hlInfo["F4"].substr(0, 28) + "...";//字数做限制，最多2行
                        } else {
                            $("hlNongLi").innerHTML = hlInfo["F4"];
                        }

                        //宜、忌 加起来为3行，优先显示 “宜” 最多2行
                        $("hlcx")
                        $("hlYi").title = hlInfo["F6"];
                        if (hlInfo["F6"].length > 30) {
                            $("hlYi").innerHTML = hlInfo["F6"].substr(0, 32) + "...";//字数做限制，最多2行
                        } else {
                            $("hlYi").innerHTML = hlInfo["F6"];
                        }

                        //处理 忌
                        $("hlJi").title = hlInfo["F7"];
                        if (hlInfo["F7"].length < 13) {
                            //只有一行的，直接输出
                            $("hlJi").innerHTML = hlInfo["F7"];
                        } else {
                            //F7多于1行
                            if (hlInfo["F6"].length > 13) {
                                //说明上面的是2行，这面只能是一行
                                $("hlJi").innerHTML = hlInfo["F7"].substr(0, 32) + "...";//字数做限制，最多1行
                            } else {
                                //F6 1行
                                if (hlInfo["F7"].length > 30) {
                                    //上面的是1行，这面的只能是1行
                                    //$("hlJi").title = hlInfo["F7"];
                                    $("hlJi").innerHTML = hlInfo["F7"].substr(0, 28) + "...";//字数做限制，最多2行
                                } else {
                                    //正合适2行
                                    $("hlJi").innerHTML = hlInfo["F7"];//字数做限制，最多2行
                                }
                            }
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

    //初始化操作
    initHLQuery();
    loadhlInfo();
   
</script>
         
</body>
</html>
