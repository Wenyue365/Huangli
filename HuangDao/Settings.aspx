<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HuangDao.Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta content="width=device-width,user-scalable=no" name="viewport" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link href="css/base.css" rel="stylesheet" />
<link href="css/frame.css" rel="stylesheet" />
<link href="css/Settings.css" rel="stylesheet" />
    <title>设置</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="body_container">
        <div class="container">
        <div id="TitleToolBar">
            <div id="divUserIcon" class="user_icon"><img src="./images/user_default_male.png" /></div>
            <div id="divUserInfo" class="user_info"><span>生辰</span><span>2000年3月3日</span></div>
        </div>

        <div id="divUserDetail" class="user_detail">
            <ul>
                <li>
                    <span>头像</span><span class="detail">点击设置头像</span>
                </li>
                <li>
                    <span>昵称</span><span class="detail">小黄</span>
                </li>
                <li>
                    <span>邮箱</span><span class="detail">xiaohuang@gmail.com</span>
                </li>
                <li>
                    <span>生辰</span><span class="detail">1980年8月8日</span><span class="detail">上午10:00</span>
                </li>
                <li>
                    <div class="friends">
                        <div id="divFriendsListTitle" class="friends_list_title"><span>好友</span></div>
                        <div id="divFriendsList" class="friends_list">
                            <ul>
                                <li><span>小白</span></li>
                                <li><span>小黑</span></li>
                                <li><span>小红</span></li>
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
        </div>
    </form>
    <!--START : Footer -->
            <div class="bottom_nav_toolbar" id="bottomNavToolbar">
            <ul>
                <li><a href="./Huangli.aspx">黄历</a></li>
                <li><a href="./BestTodo.aspx">宜忌</a></li>
                <li><a href="./Divine.aspx">测算</a></li>
                <li><a href="./Settings.aspx">设置</a></li>
            </ul>
        </div>
    <!--END : Footer -->

</body>
</html>
