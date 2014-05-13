<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Divine.aspx.cs" Inherits="HuangDao.Divine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>测算</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="TitleToolBar"></div>
        <div id="divUserIcon" class="user_icon"><img src=""/></div>
        <div><span>生辰</span><span>2000年3月3日</span></div>
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
            <div >
                <input type="text" value="输入电话号码/车牌号码/银行账号"/>
            </div>
            <div>
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
                    <div class="title"><span>忌</span></div>
                    <div class="events_list"><span></span></div>
                </li>
                <li>
                    <div class="title"><span>宜</span></div>
                    <div class="events_list"><span></span></div>
                </li>
                <li>
                    <div class="title"><span>忌</span></div>
                    <div class="events_list"><span></span></div>
                </li>
            </ul>
        </div>
        <div id="AdvertiseBar">
            <span>广告</span>
        </div>
    </div>
    </form>
</body>
</html>
