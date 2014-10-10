using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuangDao
{
    public partial class Love : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 网站访问情况统计
            CNZZ cnzz = new CNZZ();
            cnzz.setup(this.Page);

            // 增加 SEO 信息
            this.Title = SEOHelper.getPageTile("姻缘");
            SEOHelper.initMeta(this.Keywords, this.Description);


            string ipAddress = Page.Request.UserHostAddress; //取到用户的IP地址
            UserIP.Value = ipAddress;

            //String user_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //UserIP.Value = user_IP;
        }
    }
}