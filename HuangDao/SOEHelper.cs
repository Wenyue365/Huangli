using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace HuangDao
{
    public class SEOHelper
    {
        public static string getPageTile(string pageName)
        {
            string strDate = DateTime.Now.ToLongDateString();

            string strTitleTmpl = "问曰 {1} {0}老黄历查询 {0}择吉老黄历 {0}黄历 {0}农历";

            return string.Format(strTitleTmpl, strDate, pageName);
        }

        public static string getWebsiteKeywords()
        {
            string strDate = DateTime.Now.ToLongDateString();

            string strKeywordsTmpl = 
            "{0}日黄历,老黄历,老皇历,黄历,皇历,农历,吉日,老皇历," +
            "问曰老黄历,黄道吉日,{0}黄历查询," +
            "{0}老黄历查询,老黄历{0}," + 
            "{0},{0}农历,{0}农历查询,{0}日历";

            return string.Format(strKeywordsTmpl, strDate);
        }

        public static string getWebsiteDesciptions()
        {
            string strDate = DateTime.Now.ToLongDateString();
            string strYear = DateTime.Now.Year.ToString();
            string strDescriptionTmpl = 
                "问曰黄历提供老黄历查询，黄历每日吉凶宜忌查询、农历查询、黄道吉日查询、"+
                "时辰凶吉查询，提供免费搬家吉日查询、入宅吉日查询、结婚吉日查询、开业吉日查询等，"+
                "以及生肖属相运程分析，免费占卜算命，算卦占卜等。{0}黄历,"+
                "{0}黄历查询{1},{0}老黄历,老黄历{0},"+
                "{0}老黄历查询,{0}时辰吉凶宜忌";

            return string.Format(strDescriptionTmpl, strDate, strYear);
        }

        public static void initMeta(HtmlMeta htmlKeyword, HtmlMeta htmlDescription)
        {
            HtmlMeta[] metas = { htmlKeyword, htmlDescription };

            metas[0].Name = "keywords";
            metas[0].Content = SEOHelper.getWebsiteKeywords();

            metas[1].Name = "description";
            metas[1].Content = SEOHelper.getWebsiteDesciptions();
        }
    }
}