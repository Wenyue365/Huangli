<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HuangDao.Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta content="width=device-width,user-scalable=no" name="viewport" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link href="css/Settings.css" rel="stylesheet" />
    <title>设置</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="TitleToolBar">
            <div id="divUserIcon" class="user_icon"><img src="./images/user_default_male.png" /></div>
            <div id="divUserInfo" class="user_info"><span>生辰</span><span>2000年3月3日</span></div>
        </div>

        <div id="divUserDetail" class="user_detail">
            <ul>
                <li>
                    <span>头像</span><span></span>
                </li>
                <li>
                    <span>昵称</span><span></span>
                </li>
                <li>
                    <span>邮箱</span><span></span>
                </li>
                <li>
                    <span>生辰</span><span></span>
                </li>
                <li>
                    <div class="friends">
                        <div id="divFriendsListTitle" class="friends_list_title"><span>好友</span></div>
                        <div id="divFriendsList" class="friends_list">
                            <ul>
                                <li><span>好友1</span></li>
                                <li><span>好友2</span></li>
                                <li><span>好友3</span></li>
                            </ul>
                        </div>
                    </div>
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
