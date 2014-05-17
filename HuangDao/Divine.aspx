<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Divine.aspx.cs" Inherits="HuangDao.Divine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta content="width=device-width,user-scalable=no" name="viewport" />
<meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/frame.css" rel="stylesheet" />
<link href="css/divine.css" rel="stylesheet" />

    <title>测算</title>
</head>
<body>
    <div class="body_container">
    <form id="form1" runat="server">
    <div>
    <div id="TitleToolBar">
            <div id="divUserIcon" class="user_icon"><img src="./images/user_default_male.png" /></div>
            <div id="divUserInfo" class="user_info"><span>生辰</span><span>2000年3月3日</span></div>
        </div>
        <div id="DivineTypesBar" class="divine_type_bar">
            <ul>
                <li>
                    <span>号码</span>
                </li>
                <li>
                    <span>名字</span>
                </li>
                <li>
                    <span>婚姻</span>
                </li>
                <li>
                    <span>+</span>
                </li>
            </ul>
        </div>
        <div id="divDivineInputBox">
            <div class="input_box">
                <input type="text" value="输入电话号码/车牌号码/银行账号"/>
            </div>
            <div class="button_box">
                <input type="button"  value="算一算"/>
            </div>
        </div>
        <div id="divineResultList">
            <ul>
                <li>
                    <div class="title"><span>宜</span></div>
                    <div class="events_list"><span></span></div>
                </li>
                <li>
                    <div class="title bad"><span>忌</span></div>
                    <div class="events_list"><span></span></div>
                </li>
                <li>
                    <div class="title"><span>宜</span></div>
                    <div class="events_list"><span></span></div>
                </li>
                <li>
                    <div class="title bad"><span>忌</span></div>
                    <div class="events_list"><span></span></div>
                </li>
            </ul>
        </div>
        <div id="AdvertiseBar">
            <span>广告</span>
        </div>
    </form>

    </div>
    </div>

        <!--START : Footer -->
            <div class="bottom_nav_toolbar" id="bottomNavToolbar">
            <ul>
                <li><a href="./Default.aspx">黄历</a></li>
                <li><a href="./BestTodo.aspx">宜忌</a></li>
                <li><a href="./Divine.aspx">测算</a></li>
                <li><a href="./Settings.aspx">设置</a></li>
            </ul>
        </div>
    <!--END : Footer -->
</body>
</html>
