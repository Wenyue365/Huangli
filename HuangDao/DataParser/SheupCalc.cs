using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Diagnostics;
using LaoHuangLi;
using System.Text.RegularExpressions; 

namespace DataParsers
{
    /// <summary>
    /// 类：封装 www.sheup.com 的缘分测试页面的类
    /// 从 PageBase 和 Page 基类派生
    /// </summary>
    [XmlInclude(typeof(PageBase))]
    [XmlInclude(typeof(Page))]
    public class SheupCoupleCalcPage : Page
    {
        /// <summary>
        /// 提取字段值：截取名称后的所有字符
        /// </summary>
        /// <param name="rw_val"></param>
        /// <returns></returns>
        string ValFmtr_RemoveName(string rw_val)
        {
            return rw_val.Substring(4);
        }

        Entry CarNuber = null;
        Entry GoodLuckIndex = null;
        Entry Description = null;
        Entry LuckOrNot = null;
        Entry LuckSign = null;
        Entry NumberMeaning = null;

        public SheupCoupleCalcPage()
        { 
        }

        public SheupCoupleCalcPage(string page_url, PostData postData)
            : base(page_url, postData) /* 调用基类的构造函数使用POST 方法请求HTML页面 */
        {
        }

        /// <summary>
        /// 设置HTML页面请求参数的各项参数
        /// </summary>
        protected override void PreLoadHtml()
        {
            // 初始化页面请求参数
            m_Referer = "http://www.sheup.com/xingmingyuanfen.php";
            m_Cookie = "lzstat_uv=29206830872136713749|2826510; PHPSESSID=97edac456b7ceb446f70abcfb5d577c3; lzstat_ss=2533402588_1_1410801789_2826510";
        }

        /// <summary>
        /// 重载函数：在这里初始化 Entry 成员
        /// </summary>
        override protected void initPageEntry()
        {
            this.addEntry(new Entry("男方", "/html/body/div/div[2]/div[2]/div[5]/p[1]/p[1]/strong[1]/text()"));
            this.addEntry(new Entry("女方", "/html/body/div/div[2]/div[2]/div[5]/p[1]/p[1]/strong[2]/text()"));
            this.addEntry(new Entry("批语", "/html[1]/body[1]/div[1]/div[2]/div[2]/div[5]/p[1]/p[2]/strong[1]/text()"));
            this.addEntry(new Entry("缘分指数", "/html/body/div/div[2]/div[2]/div[5]/p[1]/p[3]/text()", yfIndex_valFmtter));
            this.addEntry(new Entry("解说.1", "/html/body/div/div[2]/div[2]/div[5]/p[1]/p[4]/text()"));
            this.addEntry(new Entry("解说.2", "/html/body/div/div[2]/div[2]/div[5]/p[1]/p[5]/text()"));
        }

        private string yfIndex_valFmtter(string raw_value)
        {
            string val = null;
            Regex re = new Regex(@"(\d+%)");
            Match mch = re.Match(raw_value);

            if (mch != null)
            {
                val = mch.Value;
            }
            return val;
        }
    }

}