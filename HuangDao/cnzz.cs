using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuangDao {
    class CNZZ
    {
        private int SiteId = 1253138335; /* ÍøÕ¾ ID */
        private const string ImageDomain = "c.cnzz.com";
        public CNZZ( int SiteId = 0)
        {
            if (SiteId != 0)
            {
                this.SiteId = SiteId;
            }
        }

        public void setup(Page thisPage)
        {
            Image cnzzImg = new Image();
            cnzzImg.ImageUrl = TrackPageView();
            cnzzImg.Width = 0;
            cnzzImg.Height = 0;
            cnzzImg.Style.Add("display", "none");
            thisPage.Controls.Add(cnzzImg);
        }
        private string TrackPageView()
        {
            HttpRequest request = HttpContext.Current.Request;
            string scheme = request != null ? request.IsSecureConnection ? "https" : "http" : "http";
            string referer = request != null && request.UrlReferrer != null && "" != request.UrlReferrer.ToString() ? request.UrlReferrer.ToString() : "";
            String rnd = new Random().Next(0x7fffffff).ToString();
            return scheme + "://" + CNZZ.ImageDomain + "/wapstat.php" + "?siteid=" + this.SiteId + "&r=" + HttpUtility.UrlEncode(referer) + "&rnd=" + rnd;
        }
    }
}
