/* 根据ID 获取HTML 元素 */
function $E(objId) {
    if (!objId) { return null; }
    if (document.getElementById) {
        return eval('document.getElementById("' + objId + '")');
    } else if (document.layers) {
        return eval("document.layers['" + objId + "']");
    } else {
        return eval('document.all.' + objId);
    }
}

//-----------------------------------------------------
// 代码段： 实现输宜、忌事件的提示 
//-----------------------------------------------------

// ------------------------------------------------------
// 函数：创建输入提示框, 向提示框填入数据, 并设置事件响应函数
// pmtBoxId : string, 提示框HTML元素的 id 
// schBoxId : string, 与提示框绑定的输入框的 id
// jsnKws : string[], 提示框中需要显示的宜、忌事件数组
//
function fillSearchPromptData(pmtBoxId, schBoxId, jsnKws, eventClick) {
    var prnNode = $E(pmtBoxId);

    // remove all child-nodes
    while (pmtBoxId.firstChild) {
        prnNode.removeChild(pmtBoxId.firstChild);
    }

    var span = null;

    // onclick 事件处理（函数）对象
    var onclickYijiEventHandler = function (ev) {
        var shb = $E(schBoxId);
        shb.value = this.innerHTML;
        // 隐藏提示框
        showPromptBox(pmtBoxId, false);
    }

    var len = jsnKws.length;
    len = len > 24 ? 24 : len;

    for (var i = 0; i < len; i++) {
        span = document.createElement("span");
        span.innerHTML = jsnKws[i];
        span.addEventListener("click", onclickYijiEventHandler);
        
        if (eventClick != null){
            span.addEventListener("click", eventClick);
        }
        prnNode.appendChild(span);
    }
}

// 函数：初始化输入框的外观及行为
function initSearchBox(schBoxId, defKw, clsFocus, clsBlur) {
    initInputBoxCtrl(schBoxId, defKw, clsFocus, clsBlur);
}

// 函数：用于获取元素的 CSS 的 padding, margin 等属性值
function getCssValue(elm, propName) {
    var v = window.getComputedStyle(elm, null).getPropertyValue(propName);
    return parseInt(v);
}

// 函数 : 初始化提示框的外观及行为
function initPromptBox(pmtBoxId, schBoxId) {
    var pmb = $E(pmtBoxId);
    var shb = $E(schBoxId);

    // 调整提示框的位置
    pmb.style.left = shb.offsetLeft + "px";
    pmb.style.top = (shb.offsetTop + shb.offsetHeight) + "px";
    pmb.style.display = "none"; // 初始化时不显示提示框

    // 当用户点击页面的其他区域时，隐藏输入提示框
    document.body.addEventListener("click", function (e) {
        if (e.target.id != shb.id && e.target.id != pmb.id){
            showPromptBox(pmtBoxId, false);
        }
    });

    shb.addEventListener("blur",
    function (e) {
        // showPromptBox(pmtBoxId, false);
    },
    false);

    // 当searchBox 获得焦点时，显示提示框
    shb.addEventListener("focus",
    function (e) {
            showPromptBox(pmtBoxId, true);
    },
    false);

}
// 函数：显示/隐藏 提示框
function showPromptBox(pmtBoxId, bShow) {
    var pmb = $E(pmtBoxId);
    if (bShow) {
        pmb.style.visibility = "hidden";
        pmb.style.display = "block";
        // 根据当前宜忌事件排列调整提示框大小 （4行x6列）
        var span = pmb.childNodes[0];
        var spanWidth = span.offsetWidth  + getCssValue(span, "margin") * 2;
        pmb.style.width = spanWidth * 6 + "px";
        pmb.style.visibility = "visible";
    }
    else {
        pmb.style.display = "none";
    }
}

//----------------------------------------

// 显示或者隐藏 loading 图标
function showLoading(prnNode, b) {
    if (prnNode == null) {
        prnNode = document.getElementsByTagName("body")[0];
    }
    if (b) {
        var img = document.createElement("img");
        img.id = "loadingIcon";
        img.src = "images/loading.gif";
        img.style.position = "absolute";
        img.style.left = (prnNode.scrollWidth - img.width) / 2 + "px";
        img.style.right = (prnNode.scrollHeight - img.height) / 2 + "px";
        prnNode.appendChild(img);
    }
    else {
        var img = $E("loadingIcon");
        if (img != null) {
            img.style.display = "none";
            prnNode.removeChild(img);
        }
    }
}

//------------------------------------------------------------------------
// 类：维护按钮的状态
//
function ButtonState(btnObj, st) {
    this.bindBtn = function (btnObj) {
        this._btnObj = btnObj;
    }
    this.setBtnState = function (st) {
        st = (st == null) ? 0 : st;
        if (st == this._state) {
            return false; // Same state as it is, skip it
        }
        else {
            this._state = st;
        }
        switch (st) {
            case 0:
                removeClass(this._btnObj, "button_checked");
                break;
            case 1:
                addClass(this._btnObj, "button_checked");
                break;
        }
    }
    // -- 初始化类成员
    this.bindBtn(btnObj);
    this.setBtnState(st);
}

// -----------------------------------------------------------------------
// 类：用于维护按钮，支持按钮的互斥
//
// 静态成员变量
ButtonGroup.prototype._btnStates = new Array();
ButtonGroup.prototype.getButtons = function () { return ButtonGroup.prototype._btnStates; }

function ButtonGroup() {
    // 成员函数：把一个按钮对象添加到静态成员数据(buttons)
    this.addBtn = function (btnStateObj) {
        ButtonGroup.prototype._btnStates.push(btnStateObj);
        // 设置 on-click 事件处理函数
        btnStateObj._btnObj.addEventListener("click", function (e) {
            btns = ButtonGroup.prototype._btnStates;
            var len = btns.length;
            // 移除 buttont_checked 状态（风格）
            for (var i = 0; i < len; i++) {
                var b = btns[i];
                if (b._btnObj.id == this.id) {
                    b.setBtnState(1); // 当前被点击的按钮设置为选中（checked）
                }
                else {
                    b.setBtnState(0); // 其他按钮设置为未选中
                }
            }
        });
    }
}

// 函数：将指定按钮加入到“按钮组”
function initButtonGroup(btnId /*btnId2, btnId3, ...*/) {
    var len = arguments.length;
    var btnGroup = new ButtonGroup();
    for (var i = 0; i < len; i++) {
        var btn = $E(arguments[i]);
        var btnState = new ButtonState(btn);
        btnGroup.addBtn(btnState);
    }
}

//------------------------------------------------------------------------
// 类：封装黄历宜忌数据操作
// onReady : onload 事件处理函数
function HuangliYiji(data_path, year, month, onReady) {
    /** 类的成员变量 **/
    this.dataPath = data_path;
    this.year = year;
    this.month = month;
    this.hlDB = null;
    this.onReady = onReady;
    // 通过创建 script 节点，加载 JS 文件数据
    this.loadDataFile = function () {
        // document.writeln("<scri"+"pt src='"+ this.dataPath +"' type='text/javascript'></sc"+"ript>");
        var htmlHead = document.getElementsByTagName("head")[0];
        var scriptElm = document.getElementById("scriptHangliData");

        if (htmlHead != null) {
            // 必须移除原节点，重新添加，否则JS 脚本不会重新加载
            if (scriptElm != null) {
                htmlHead.removeChild(scriptElm);
            }

            scriptElm = document.createElement("script");
            scriptElm.id = "scriptHangliData";
            scriptElm.src = this.dataPath;
            scriptElm.onload = this.onReady;
            htmlHead.appendChild(scriptElm);
        }
    }

    this.getHuangLiDataObject = function () {
        if (typeof(HuangLiDataObject)=="undefined") {
            return null;
        }
        return HuangLiDataObject;
    }

    // d : number 当前月份的指定一天 0~30
    this.getYiEvents = function (d) {
        var yiIdx = 2; /* 宜 字段*/
        var dayIdx = d;

        this.hlDB = this.getHuangLiDataObject();

        if (d >= this.hlDB.length) return null;

        return this.hlDB[dayIdx][yiIdx].value;
    }
    // d : number 当前月份的指定一天 0~30
    this.getJiEvents = function (d) {
        var jiIdx = 3;
        var dayIdx = d;

        this.hlDB = this.getHuangLiDataObject();
        if (d >= this.hlDB.length) return null;

        return this.hlDB[dayIdx][jiIdx].value;
    }
    // event : string ，宜事件名称
    // return :　Array
    this.getYiDates = function (event) {
        var yiIdx = 2; /* 宜 字段*/
        var dates = new Array(31);
        this.hlDB = this.getHuangLiDataObject();
        if (this.hlDB != null) {
            var len = this.hlDB.length;
            for (var i = 0; i < len; i++) {
                dates[i] = (this.hlDB[i][yiIdx].value.indexOf(event) >= 0) ? true : false;
            }
        }
        return dates;
    }

    this.getJiDates = function (event) {
        var jiIdx = 3; /* 忌 字段*/
        var dates = new Array(31);
        this.hlDB = this.getHuangLiDataObject();
        if (this.hlDB != null) {
            var len = this.hlDB.length;
            for (var i = 0; i < len; i++) {
                dates[i] = (this.hlDB[i][jiIdx].value.indexOf(event) >= 0) ? true : false;
            }
        }
        return dates;
    }

    /** 类的初始化代码 **/
    if (data_path && year && month) {
        this.loadDataFile(this.onReady);
    }
    else /* 如果未指定构造函的参数，则仅关联已加载的Yi Ji 数据 */
    {
        this.hlDB = this.getHuangLiDataObject();
    }
}

/* 获取IP 地址归属地 */



/* 使用JS 获取 GET 方法的 QueryString 
*  先初始化  
*  QueryString.Initial();  
*  例:获得名为deliverQty的参数值  
*  var deliverQty = parseInt(QueryString.GetValue('deliverQty'));
*/
var QueryString = {
    data: {},
    Initial: function () {
        var aPairs, aTmp;
        var queryString = new String(window.location.search);
        queryString = queryString.substr(1, queryString.length); //remove   "?"     
        aPairs = queryString.split("&");
        for (var i = 0; i < aPairs.length; i++) {
            aTmp = aPairs[i].split("=");
            this.data[aTmp[0]] = aTmp[1];
        }
    },
    GetValue: function (key) {
        return this.data[key];
    }
}

/* 工具函数：截取指定长度的字附串 
*  by Jenseng.
*/
function trimString(str, len) {
    if (str != null && str.length > len) {
        str = str.substr(0, len);
        // str = str + "...";
    }
    return str;
}

// 函数：数字格式化输出
function FormatInt(num, len) {
    var fmt = "000000000";
    var strNum = num.toString();
    if (strNum.length < len) {
        strNum = fmt + strNum;
        strNum = strNum.substring(strNum.length - len);
    }
    return strNum;
}

// 元素 CSS 的操作
function hasClass(ele, cls) {
    return ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(ele, cls) {
    if (!this.hasClass(ele, cls)) ele.className += " " + cls;
}

function removeClass(ele, cls) {
    if (hasClass(ele, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        ele.className = ele.className.replace(reg, ' ');
    }
}

/* 函数： 用于从 XML 文档对象中提取指定标签的值
*  by Jenseng.
*/
function $T(xml, tagName) {
    node = xml.getElementsByTagName(tagName);
    if (node != null) {
        node = node[0];
    }
    if (node.childElementCount > 0) {
        node = node.getElementsByTagName("Value");
        if (node != null) {
            node = node[0];
        }
    }

    return node.textContent; // 返回节点的文本内容，注：innerHTML 属于在一些浏览器上无效
}

/* 初始化日期时间控件 */
function initDateTimeCtrl(yearCtrl_id, monthCtrl_id, dayCtrl_id, hourCtrl_id, minCtrl_id) {
    var startDate = new Date(1940, 1, 1, 0, 0, 0, 0);
    var endDate = new Date(2020, 12, 31, 23, 59, 59, 999);

    var yc = $(yearCtrl_id);
    var mc = $(monthCtrl_id);
    var dc = $(dayCtrl_id);
    var hc = $(hourCtrl_id);
    var nc = $(minCtrl_id);

    // Year Control 
    var year_span = endDate.getFullYear() - startDate.getFullYear();
    var opn_array = new Array(year_span);
    for (i = 0; i < year_span; i++) {
        opn_array[i] = new Option(startDate.getFullYear() + i + "年", startDate.getFullYear() + i);
        if (opn_array[i].value == 1989) opn_array[i].selected = true; // Set default selected value
    }
    yc.append(opn_array);

    // Month Control
    var month_span = 12;
    var opn_array = new Array(month_span);
    for (i = 0; i < month_span; i++) {
        opn_array[i] = new Option(i + 1 + "月", i + 1);
        if (opn_array[i].value == 9) opn_array[i].selected = true; // Set default selected value
    }
    mc.append(opn_array);

    // Day Control
    var day_span = 31;
    var opn_array = new Array(day_span);
    for (i = 0; i < day_span; i++) {
        opn_array[i] = new Option(i + 1 + "日", i + 1);
        if (opn_array[i].value == 9) opn_array[i].selected = true; // Set default selected value
    }
    dc.append(opn_array);

    // Hour Control
    var hour_span = 24;
    var opn_array = new Array(hour_span);
    for (i = 0; i < hour_span; i++) {
        opn_array[i] = new Option(i + "时", i);
        if (opn_array[i].value == 9) opn_array[i].selected = true; // Set default selected value
    }

    hc.append(opn_array); // Note: the 'add' method can not work.

    // Minute Control
    var min_span = 60;
    var opn_array = new Array(min_span);
    for (i = 0; i < min_span; i++) {
        opn_array[i] = new Option(i + "分", i);
        if (opn_array[i].value == 9) opn_array[i].selected = true; // Set default selected value
    }

    nc.append(opn_array); // Note: the 'add' method can not work.
}

/* 搜索输入框获得焦点 */
function onfocus_inputCtrl(bx) {
    if (bx != null) {
        inputBox = $(bx);

        if (inputBox.val() == inputBox.attr("title")) {
            inputBox.val(""); // Clear the value
            inputBox.css("color","#BD0000");
        }
    }
}
/* 搜索输入框失去焦点 */
function onblur_InputCtrl(bx) {
    if (bx != null) {
        inputBox = $(bx);

        if (inputBox.val() == "") {
            inputBox.val(inputBox.attr("title"));
            inputBox.css("color","grey");
        }
    }
}


/* FUNCTION
* ipbId : string, ID of the input-box
* promptText : string, prompt text for input-box when user input nothing
* clsNameFocus : string, css class name for input-box with focus
* clsNameBlur : string, css class name for input-box lost focus
* by Jenseng.liu
*/

function initInputBoxCtrl(ipbId, promptText, clsNameFocus, clsNameBlur) {
    var ipbCtrl = $E(ipbId);
    ipbCtrl.promptText = promptText;
    ipbCtrl.value = promptText;

    ipbCtrl.onclick = function () {
        if (this.value == this.promptText) {
            this.value = ""; // Clear the value
            if (clsNameFocus) {
                addClass(this, clsNameFocus);
                if (clsNameBlur) {
                    removeClass(this, clsNameBlur);
                }
            }
        }
    }

    ipbCtrl.onblur = function () {
        if (this.value == "") {
            this.value = this.promptText; // Clear the value
            if (clsNameFocus) {
                removeClass(this, clsNameFocus);
                if (clsNameBlur) {
                    addClass(this, clsNameBlur);
                }
            }
        }
    }
}

/*//shortcut method
var $ = function (s) {
    return (typeof s == "object") ? s : document.getElementById(s);
};
*/

/*===========================================================================*/
/* 类：封装生成黄历所需的属性和方法
*
*
*/
var HlCalendarClass = {
    createNew: function () {
        var c = {}; // 空对象
        c.parentNode = null;
        c.bestDates = null;
        c.badDates = null;

        /* 填充日历控件 
		*  dateNodes  : JQuery Object Array 日历
		*
		*/
        c._fillCalendar = function (dateNodes, ym, bShowBest, bShowBad) {
            var td = new Date();
            var d = new Date(ym.getFullYear(), ym.getMonth(), 1); /* 计算出日历上需显示的第 1 天*/
            var offset_days = d.getDay() - 1; // 每当月的 1 号对应星期几，即为偏移日期
            offset_days = offset_days >= 0 ? offset_days : 6; // 由于周日时 getDay()返回值为0，所要需要进一步处理

            d.setDate(d.getDate() - offset_days);

            var lkbtn = null;
            for (var i = 0; i < 5; i++) {
                for (var j = 0; j < 7; j++) {
                    lkbtn = dateNodes[i * 7 + j];

                    if (d.getMonth() == ym.getMonth()) {
                        lkbtn.innerHTML = d.getDate();
                        lkbtn.href = "huangli.aspx?" + getQueryStr(d);
                        // 设置宜/忌的显示样式
                        if (bShowBest) {
                            lkbtn.className = this._isBestDate(d) ? "calendar_day yi_day" : "calendar_day"
                        }
                        if (bShowBad){
                            lkbtn.className = this._isBadDate(d) ? "calendar_day ji_day" : "calendar_day"
                        }
                        // 标识"今天"
                        if (d.getMonth() == td.getMonth() && d.getDate() == td.getDate())
                        {
                            lkbtn.className += " today";
                        }
                    }
                    else {
                        lkbtn.innerHTML = d.getDate();
                        lkbtn.className = "calendar_day invalid_day";
                    }

                    d.setDate(d.getDate() + 1); // Forward to next day of this month
                }
            }
        }

        /* 获取当月的“宜”日期*/
        c._getBestDates = function (ym) {
            return c.bestDates;
        }

        /* 设置当月的“宜”日期*/
        c.setBestDates = function (days) {
            c.bestDates = days;
        }

        /* 获取当月的“忌”日期*/
        c._getBadDates = function (ym) {
            return c.badtDates;
        }
        /* 设置当月的“忌”日期 */
        c.setBadDates = function (days){
            c.badDates = days;
        }

        c._isBestDate = function (date) {
            return this.bestDates[date.getDate()-1];
        }

        c._isBadDate = function (date) {
             return this.badDates[date.getDate()-1];
        }

        /* 重新生成日历组件 */
        c.RebuildCalendar = function (ym, bShowBest, bShowBad) {
            var dateNodes = $(".calendar_day");
            dateNodes.removeClass("yi_day");
            dateNodes.removeClass("ji_day");
            dateNodes.removeClass("invalid_day");

            this._fillCalendar(dateNodes, ym, bShowBest, bShowBad);
        }
        return c;
    }/* END OF createNew() 函数 */
};

/*===== 以下函数可能已经被废弃================================================================*/
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