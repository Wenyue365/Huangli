<
!--
function submitchecken() {
    if (document.ceshi.xing.value == "") {
        alert("请输入姓氏。");
        document.ceshi.xing.focus();
        return false;
    }
    if (document.ceshi.xing.value.length > 2) {
        alert("姓氏输入出错,不能多于2字。");
        document.ceshi.xing.focus();
        return false;
    }
    
    if (document.ceshi.ming.value == "") {
        alert("请输入名。");
        document.ceshi.ming.focus();
        return false;
    }
    if (document.ceshi.ming.value.length > 3) {
        alert("名字输入出错,不能多于3字。");
        document.ceshi.ming.focus();
        return false;
    }
    if (document.ceshi.nian.value.length != 4) {
        alert("年的位数出错了,必须为4位。");
        document.ceshi.nian.focus();
        return false;
    }
    
    re = "请重新输入！";
    y = document.ceshi.nian.value;
    m = document.ceshi.yue.value;
    d = document.ceshi.ri.value;
    
    if (y == "" || y < 1901 || y > 2050) {
        alert("年应在1901和2050之间。" + re);
        document.ceshi.nian.focus();
        return false;
    }
    if (m > 12 || m < 1) {
        alert("月应在1与12之间。" + re);
        document.ceshi.yue.focus();
        return false;
    }
    if (d > 31 || d < 1) {
        alert("日应在1与31之间。" + re);
        document.ceshi.ri.focus();
        return false;
    }
    if ((m == 4 || m == 6 || m == 9 || m == 11) && d > 30) {
        alert(m + "月只有30天。" + re);
        document.ceshi.ri.focus();
        return false;
    }
    if (y % 4 != 0 && m == 2 && d > 28) {
        alert(y + "年是平年，2月只有28天。" + re);
        document.ceshi.ri.focus();
        return false;
    }
    if (m == 2 && d > 29) {
        alert(y + "年是闰年，2月只有29天。" + re);
        document.ceshi.ri.focus();
        return false;
    }
    return true;
}
function Nian2Option() {
    for (I = 1922; I < 2015; I++) {
        if (I == 1980) {
            document.write('<option value=' + I + ' selected>' + I + '</option>');
        } else {
            document.write('<option value=' + I + '>' + I + '</option>');
        }
    }
}
function Yue2Option() {
    for (I = 1; I < 13; I++) {
        document.write('<option value=' + I + '>' + I + '</option>');
    }
}
function Ri2Option() {
    for (I = 1; I < 32; I++) {
        document.write('<option value=' + I + '>' + I + '</option>');
    }
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null)
        return unescape(arr[2]);
    return null;

}
function module_top_menu() {
    document.writeln(" | <a href='http://pro.17caifu.com/xiangpi.html?pid=14' target='_blank' rel='nofollow'><font color=red><u>八字终生运程<\/u><\/font><\/a>");
    document.writeln(" | <a href='http://pro.17caifu.com/hehun.html?pid=14' target='_blank' rel='nofollow'>八字合婚<\/a>");
    
    var xing = getCookie('xing');
    var ming = getCookie('ming');
    
    if (xing != null && xing != "" && ming != null && ming != "") {
        document.writeln(" | <a href=\"\/logout.php\">重新测算<\/a>");
    }
}
function module_love_1x4() {
    
    document.writeln("<script type=\"text\/javascript\">\/*640*60，仅文字，创建于2011-3-1*\/ var cpro_id = \'u394275\';<\/script>");
    document.writeln("<script src=\"http:\/\/cpro.baidu.com\/cpro\/ui\/c.js\" type=\"text\/javascript\"><\/script>");

}

function module_660x30() {
    document.write('<a href="http://www.sheup.com/xingming_dafen.php" target="_blank"><img src="/images/ky/ky_660x30.gif" width="660"></a>');
}

function module_bannerA() {
    
    var how_many_ads = 4;
    var now = new Date()
    var sec = now.getSeconds()
    var ad = sec % how_many_ads;
    ad += 1;
    if (ad == 1) {
        url = "http://pro.17caifu.com/2014.html?pid=14";
        alt = "【详批您在2014年（马年）的流年运势！】";
        img_src = "http://www.sheup.com/images/120608/120608_600x90_1.gif";
    }
    if (ad == 2) {
        url = "http://pro.17caifu.com/xiangpi.html?pid=14";
        alt = "【八字终身运程详批】";
        img_src = "http://www.sheup.com/images/120608/120608_600x80_2.gif";
    }
    if (ad == 3) {
        url = "http://pro.17caifu.com/hehun.html?pid=14";
        alt = "【生辰八字合婚，测试你们是不是天生一对！】";
        img_src = "http://www.sheup.com/images/120608/120608_600x80_3.gif";
    }
    if (ad == 4) {
        url = "http://pro.17caifu.com/peidui.html?pid=14";
        alt = "【姓名爱情魔法配对】";
        img_src = "http://www.sheup.com/images/120608/120608_600x80_4.gif";
    }
    document.write('<a href="' + url + '" target="_blank" title="' + alt + '"><img src="' + img_src + '" width="600"></a>');

}

function module_benmingfo() {
    
    document.writeln("<script type=\"text\/javascript\"> \/*640*60，创建于2010-11-16*\/ var cpro_id = \'u280964\';<\/script>");
    document.writeln("<script type=\"text\/javascript\" src=\"http:\/\/cpro.baidu.com\/cpro\/ui\/c.js\"><\/script>");

}

function module_baidu_640x60() {
    
    document.writeln("<script type=\"text\/javascript\"> \/*640*60，创建于2010-11-16*\/ var cpro_id = \'u280964\';<\/script>");
    document.writeln("<script type=\"text\/javascript\" src=\"http:\/\/cpro.baidu.com\/cpro\/ui\/c.js\"><\/script>");

}
function module_cc_300x250() {
    
    document.writeln("<script charset=\"gbk\" src=\"http://p.tanx.com/ex?i=mm_10024608_2290070_9662481\"></script>");

}

function module_bannerB() {
    
    document.writeln("<ul>");
    document.writeln("<li><a href='http://pro.17caifu.com/2014.html?pid=14' target='_blank'><font color=#3300FF>2014年马年运程<\/font><\/a><\/li>");
    document.writeln("<li><a href='http://pro.17caifu.com/peidui.html?pid=14' target='_blank'><font color=Fuchsia>爱情配对测试<\/font><\/a><\/li>");
    document.writeln("<li><a href='http://pro.17caifu.com/toushi.html?pid=14' target='_blank'><font color=red>八字透视另一半<\/font><\/a><\/li>");
    document.writeln("<li><a href='http://pro.17caifu.com/xiangpi.html?pid=14' target='_blank'><font color=#993300>八字终身详批<\/font><\/a><\/li>");
    document.writeln("<li><a href='http://pro.17caifu.com/taohua.html?pid=14' target='_blank'><font color=green>八字桃花命书<\/font><\/a><\/li>");
    document.writeln("<li><a href='http://pro.17caifu.com/hehun.html?pid=14' target='_blank'>在线八字合婚<\/a><\/li>");
    document.writeln("<li><a href='http://www.sheup.com/action.php?action=CheckCount&id=1' target='_blank'>本命佛开运改运<\/a><\/li>");
    document.writeln("<\/ul>");

}

function module_foot_link() {
    
    document.writeln(" | <a href='http://www.sheup.com/action.php?action=CheckCount&id=2' target='_blank' rel='nofollow'>给我们留言<\/a> | ");
    document.writeln("<a href='http://www.sheup.com/duty.php' rel='nofollow'>使用三藏算命前必读<\/a> | ");
    document.writeln("<a href='http://www.sheup.com/plug/disk_url.php' target='_blank' rel='nofollow'><font color='#cc6633'>下载三藏算命到桌面<\/font><\/a>");
    
    document.writeln("<script type=\"text\/javascript\" src=\"http:\/\/js.tongji.linezing.com\/2826510\/tongji.js\"><\/script>");
    document.writeln("<noscript>");
    document.writeln("<a href=\"http:\/\/www.linezing.com\"><img src=\"http:\/\/img.tongji.linezing.com\/2826510\/tongji.gif\"\/><\/a>");
    document.writeln("<\/noscript>");
    
    document.body.oncopy = function() {
        setTimeout(function() {
            var text = clipboardData.getData("text");
            if (text) {
                text = text + "\r\n来源：" + location.href;
                clipboardData.setData("text", text);
            }
        }, 100)
    }

}

function module_nolanding() {
    
    document.writeln("<script type=\"text\/javascript\">");
    document.writeln("<!--");
    document.writeln("google_ad_client = \"pub-0471031752338283\";");
    document.writeln("\/* 468x60, 创建于 08-6-20 *\/");
    document.writeln("google_ad_slot = \"1529982896\";");
    document.writeln("google_ad_width = 468;");
    document.writeln("google_ad_height = 60;");
    document.writeln("\/\/-->");
    document.writeln("<\/script>");
    document.writeln("<script type=\"text\/javascript\"");
    document.writeln("src=\"http:\/\/pagead2.googlesyndication.com\/pagead\/show_ads.js\">");
    document.writeln("<\/script>");

}

function module_300x300_1() {
    
    document.writeln("<script type=\"text\/javascript\">");
    document.writeln("<!--");
    document.writeln("google_ad_client = \"pub-0471031752338283\";");
    document.writeln("\/\/300x250, 创建于 07-11-19");
    document.writeln("google_ad_slot = \"5242604702\";");
    document.writeln("google_ad_width = 300;");
    document.writeln("google_ad_height = 250;");
    document.writeln("\/\/--><\/script>");
    document.writeln("<script type=\"text\/javascript\"");
    document.writeln("src=\"http:\/\/pagead2.googlesyndication.com\/pagead\/show_ads.js\">");
    document.writeln("<\/script>");

}

function module_300x300_2() {
    
    document.writeln("<script type=\"text\/javascript\">\/*300*280，创建于2011-1-1*\/ var cpro_id = \'u337474\';<\/script>");
    document.writeln("<script src=\"http:\/\/cpro.baidu.com\/cpro\/ui\/c.js\" type=\"text\/javascript\"><\/script>");

}

function module_share() {
    
    document.writeln("<!-- Baidu Button BEGIN -->");
    document.writeln("    <div id=\"bdshare\" class=\"bdshare_t bds_tools get-codes-bdshare\">");
    document.writeln("        <span class=\"bds_more\">分享到：<\/span>");
    document.writeln("        <a class=\"bds_qzone\">QQ空间<\/a>");
    document.writeln("        <a class=\"bds_tsina\">新浪微博<\/a>");
    document.writeln("        <a class=\"bds_tqq\">腾讯微博<\/a>");
    document.writeln("        <a class=\"bds_renren\">人人网<\/a>");
    document.writeln("        <a class=\"bds_baidu\">百度搜藏<\/a>");
    document.writeln("    <\/div>");
    document.writeln("<script type=\"text\/javascript\" id=\"bdshare_js\" data=\"type=tools&amp;uid=374288\" ><\/script>");
    document.writeln("<script type=\"text\/javascript\" id=\"bdshell_js\"><\/script>");
    document.writeln("<script type=\"text\/javascript\">");
    document.writeln("	document.getElementById(\"bdshell_js\").src = \"http:\/\/bdimg.share.baidu.com\/static\/js\/shell_v2.js?t=\" + new Date().getHours();");
    document.writeln("<\/script>");
    document.writeln("<!-- Baidu Button END -->");

}

function module_pay_qiming() {
    
    document.writeln('【<a href="http://pro.17caifu.com/2014.html?pid=14" target="_blank">2014运势精批</a>】');

}
function Rand_Text() {
    var how_many_ads = 4;
    var now = new Date()
    var sec = now.getSeconds()
    var ad = sec % how_many_ads;
    ad += 1;
    if (ad == 1) {
        url = "http://pro.17caifu.com/hehun.html?pid=14";
        alt = "超准的八字合婚，你们是不是天生一对？";
    }
    if (ad == 2) {
        url = "http://pro.17caifu.com/peidui.html?pid=14";
        alt = "经典姓名爱情魔方配对，情侣缘分测试！";
    }
    if (ad == 3) {
        url = "http://pro.17caifu.com/xiangpi.html?pid=14";
        alt = "八字终身运势详批，犹如大师亲临！";
    }
    if (ad == 4) {
        url = "http://pro.17caifu.com/2014.html?pid=14";
        alt = "八字紫薇详批您2014年（马年）的运程！";
    }
    document.write('<a href="' + url + '" target="_blank"><font color="#009933">' + alt + '</font></a>');
}
//-->