using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuangDao
{
    public partial class Divine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 网站访问情况统计
            CNZZ cnzz = new CNZZ();
            cnzz.setup(this.Page);

            // 增加 SEO 信息
            this.Title = SEOHelper.getPageTile("测算");
            SEOHelper.initMeta(this.Keywords, this.Description);

            string qs_fn = Request.QueryString["fn"];
            if (qs_fn == "number")
            {
                defaultSearchType.Value = "0";
            }
            else if (qs_fn == "name")
            {
                defaultSearchType.Value = "1";
            }
            else if (qs_fn == "marrage")
            {
                defaultSearchType.Value = "2";
            }

            string ipAddress = Page.Request.UserHostAddress; //取到用户的IP地址
            UserIP.Value = ipAddress;

            //String user_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //UserIP.Value = user_IP;
        }
    }
}