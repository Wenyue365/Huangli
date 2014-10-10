using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstroSpider;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace HuangDao
{
    public partial class HuangLi : System.Web.UI.Page
    {
        string qs_date;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 网站访问情况统计
            CNZZ cnzz = new CNZZ();
            cnzz.setup(this.Page);

            // 增加 SEO 信息
            this.Title = SEOHelper.getPageTile("黄历");
            SEOHelper.initMeta(this.Keywords, this.Description);

            DateTime td = DateTime.Now;

            // 尝试从QueryString 中获取页面需要显示的日期信息
            qs_date= Request.QueryString["hld"];
            if (qs_date != null)
            {
                DateTime.TryParse(qs_date, out td);
            }

            HDWebservices hdSvcs = new HDWebservices();

            SinaHLDayEx hld = hdSvcs.getSinaHlInfo(td.Year, td.Month, td.Day);
            Debug.WriteLine(string.Format("Get Sina Huangli Info :{0}, {1}, {2}", td.Year, td.Month, td.Day));
            
            if (hld != null)
            {
                xLunarDate.InnerText = hld.m_lunarDate.Value;
                xAncientYear.InnerText = hld.m_yearOrder.Value;
                xAncientMonth.InnerText = hld.m_monthOrder.Value;
                xAncientDay.InnerText = hld.m_dayOrder.Value;

                CreateEventsListCtrls(xYiEventsList, hld.m_Yi.Value);
                CreateEventsListCtrls(xJiEventsList, hld.m_Ji.Value);
            }

            xAcientTime.InnerText  = hdSvcs.getShiChengInfo(DateTime.Now.Hour);

            xYiTime.InnerText = getYiTime(hdSvcs, td);
            xJiTime.InnerText = getJiTime(hdSvcs, td);
        }

        private string getJiTime(HDWebservices hdSvcs, DateTime curr_dt)
        {
            string strJiEvents = null;

            LaoHLHour lhHour = hdSvcs.getLaoHLHour(curr_dt.Year, curr_dt.Month, curr_dt.Day, curr_dt.Hour);

            if (lhHour != null)
            { 
                strJiEvents = lhHour.m_bad_timed;

                if (lhHour.m_bad_timed.Length > 8)
                {
                    strJiEvents = strJiEvents.Substring(0, 8);
                    //strJiEvents += "...";
                }
            }
            return strJiEvents;
        }

        private string getYiTime(HDWebservices hdSvcs, DateTime curr_dt)
        {
            string strYiEvents = null;
            LaoHLHour lhHour = hdSvcs.getLaoHLHour(curr_dt.Year, curr_dt.Month, curr_dt.Day, curr_dt.Hour);

            if (lhHour != null)
            {
                strYiEvents = lhHour.m_well_timed;
                if (lhHour.m_well_timed.Length > 8)
                {
                    strYiEvents = strYiEvents.Substring(0, 8);
                    //strYiEvents += "...";
                }
            }
            return strYiEvents;
        }
        private void CreateEventsListCtrls(HtmlControl parentCtrl, string strEvents)
        {
            int MAX_EVENT_COUNT = 18;

            char[] sprCharsets = { ' ' };
            string[] arrEventsList = strEvents.Split(sprCharsets);

            int cnt = 0;
            foreach (string s in arrEventsList)
            {
                if (s.IndexOf('*') == -1)
                {
                    if (++cnt < MAX_EVENT_COUNT)
                    {
                        HyperLink lnk = new HyperLink();
                        lnk.Text = s.Trim();
                        parentCtrl.Controls.Add(lnk);
                    }
                }
            }
        }
    }
}