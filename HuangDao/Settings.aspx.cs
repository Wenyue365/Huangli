using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuangDao
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 网站访问情况统计
            CNZZ cnzz = new CNZZ();
            cnzz.setup(this.Page);
        }
    }
}