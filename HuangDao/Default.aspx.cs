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
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HDWebservices hdSvcs = new HDWebservices();
            DateTime td = DateTime.Now;

            SinaHLDayEx hld = hdSvcs.getSinaHlInfo(td.Year, td.Month, td.Day);
            Debug.WriteLine(string.Format("Get Sina Huangli Info :{0}, {1}, {2}", td.Year, td.Month, td.Day));

            if (hld != null)
            {
                xLunarDate.InnerText = hld.m_lunarDate.Value;
                xAncientYear.InnerText = hld.m_yearOrder.Value;
                xAncientMonth.InnerText = hld.m_monthOrder.Value;
                xAncientDay.InnerText = hld.m_dayOrder.Value;

                CreateEventsListCtrls(xFiveElemEventsList, hld.m_fiveElem.Value);
                CreateEventsListCtrls(xCollideEventsList, hld.m_collide.Value);
                CreateEventsListCtrls(xPengAvoidEventsList, hld.m_pengAvoid.Value);
                CreateEventsListCtrls(xGoodAngelYiEventsList, hld.m_goodAngelYi.Value);
                CreateEventsListCtrls(xEvilAngelJiEventsList, hld.m_evilAngelJi.Value);
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
            }

            return strYiEvents;
        }
        private void CreateEventsListCtrls(HtmlControl parentCtrl, string strEvents)
        {
            char[] sprCharsets = { ' ' };
            string[] arrEventsList = strEvents.Split(sprCharsets);

            foreach (string s in arrEventsList)
            {
                HyperLink lnk = new HyperLink();
                lnk.Text = s.Trim();
                parentCtrl.Controls.Add(lnk);
            }
        }
    }
}