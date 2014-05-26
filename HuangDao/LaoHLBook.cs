using HuangDao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AstroSpider
{
    // Class of LaoHuangLi Entry
    public class LaoHLEntryEx : Entry
    {
        // Definition of Extra customized parser
        public delegate string CustomParser(string strOldValue);

        char[] m_sepCharset = { ' ', '、' };
        string m_entryName = null;
        int m_idxOfVal = -1;
        string m_value = null;
        string[] m_values = null;


        // Declare a customized parser
        public CustomParser m_customParser = null;

        /// <summary>
        /// LaoHLEntryEx constructor
        /// </summary>
        /// <param name="en">Name of the Entry</param>
        /// <param name="xpath">XPATH for parsing value of this entry</param>
        /// <param name="iVal">Specified exact value of the entry within multiple values</param>
        public LaoHLEntryEx(string en, string xpath, int iVal = -1)
        {
            m_entryName = en;
            m_idxOfVal = iVal;
            base.xpath = xpath;
        }
        /// <summary>
        /// Get value of the entry
        /// </summary>
        public string Value
        {
            get { return m_value; }
        }

        override protected string parseValue()
        {
            base.val = base.parseValue(); // 使用基类的函数提取 val 值

            // Extra customized parsing
            if (m_customParser != null)
            {
                base.val = m_customParser(base.val);
            }

            if (m_idxOfVal >= 0)
            {
                if (base.val != null)         // 提取给定序号的子值，并保存于 m_value
                {
                    m_values = base.val.Split(m_sepCharset);
                    if (m_values.Length > m_idxOfVal)
                    {
                        m_value = m_values[m_idxOfVal];
                    }
                }
            }
            else
            {
                m_value = base.val;
            }
            return m_value;
        }
    }

    public class LaoHLHour
    {
        public DateTime m_curr_date;

        public string m_ancient_hour;
        public string m_ancient_hour_fullname;
        public string m_solar_time_start; 
        public string m_solar_time_end;
        public string m_star_god;
        public string m_straight_confict; 
        public string m_good_ill_luck;
        public string m_zodiac_timed;
        public string m_good_god;
        public string m_ill_god;
        public string m_well_timed; 
        public string m_bad_timed;
        public string m_fiveElem_timed;
        public string m_conflict_orientation;
        public string m_happy_god;
        public string m_fortune_god;
    }

    // Class of LaoHuangLi Page
    public class LaoHLDayEx
    {
        private const int m_length = 12;
        public int Length
        {
            get { return m_length; }
        }

        public LaoHLEntryEx m_curr_date;

        public LaoHLEntryEx[] m_ancient_hour = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_ancient_hour_fullname = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_solar_time_start = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_solar_time_end = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_star_god = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_straight_confict = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_good_ill_luck = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_zodiac_timed = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_good_god = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_ill_god = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_well_timed = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_bad_timed = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_fiveElem_timed = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_conflict_orientation = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_happy_god = new LaoHLEntryEx[m_length];
        public LaoHLEntryEx[] m_fortune_god = new LaoHLEntryEx[m_length];
        public LaoHLDayEx(ZHtmlParser pr)
        {

            // EXAMPlE: m_yearOrder = new LaoHLEntryEx("岁次", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[1]/td[2]/text()", 0); // 注意：xpath 里的 tbody 须忽略！！！

            m_curr_date = new LaoHLEntryEx("公历日期", "//*[@id=\"USkin_bk\"]/table[6]/tr/td[2]/div/table[5]/tr/td/text()");
            m_curr_date.m_customParser = getSolorDate;
            m_curr_date.Parser = pr;

            for (int i = 0; i < m_length; i++)
            {
                m_ancient_hour[i] = new LaoHLEntryEx("时辰简称", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[1]/td[{0}]/text()", i + 2));
                m_solar_time_start[i] = new LaoHLEntryEx("时刻开始", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[2]/td[{0}]/text()", i + 2), 0);
                m_solar_time_end[i] = new LaoHLEntryEx("时刻结束", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[2]/td[{0}]/text()", i + 2), 1);
                m_ancient_hour_fullname[i] = new LaoHLEntryEx("时辰全称", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[3]/td[{0}]/text()", i + 2));
                m_star_god[i] = new LaoHLEntryEx("星神", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[4]/td[{0}]/text()", i + 2));
                m_straight_confict[i] = new LaoHLEntryEx("正冲", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[5]/td[{0}]/text()", i + 2));
                m_good_ill_luck[i] = new LaoHLEntryEx("吉凶", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[6]/td[{0}]", i + 2));
                m_zodiac_timed[i] = new LaoHLEntryEx("生肖", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[7]/td[{0}]/text()", i + 2));
                m_good_god[i] = new LaoHLEntryEx("吉神", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[8]/td[{0}]/div", i + 2));
                m_ill_god[i] = new LaoHLEntryEx("凶煞", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[9]/td[{0}]/div", i + 2));
                m_well_timed[i] = new LaoHLEntryEx("时宜", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[10]/td[{0}]/div", i + 2));
                m_bad_timed[i] = new LaoHLEntryEx("时忌", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[11]/td[{0}]/div", i + 2));
                m_fiveElem_timed[i] = new LaoHLEntryEx("五行", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[12]/td[{0}]/text()", i + 2));
                m_conflict_orientation[i] = new LaoHLEntryEx("煞方", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[13]/td[{0}]/text()", i + 2));
                m_happy_god[i] = new LaoHLEntryEx("喜神", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[14]/td[{0}]/text()[1]", i + 2), 0);
                m_fortune_god[i] = new LaoHLEntryEx("财神", string.Format("//*[@id=\"USkin_bk\"]/table[14]/tr/td/div/table[1]/tr[14]/td[{0}]/text()[2]", i + 2), 0);

                // 额外处理
                m_good_ill_luck[i].m_customParser = getGoodIllLuck;

                // 为Entry 实例的 Parser 对象赋值，将会同时完成相应数值的解析
                m_ancient_hour[i].Parser = pr;
                m_ancient_hour_fullname[i].Parser = pr;
                m_solar_time_start[i].Parser = pr;
                m_solar_time_end[i].Parser = pr;
                m_star_god[i].Parser = pr;
                m_straight_confict[i].Parser = pr;
                m_good_ill_luck[i].Parser = pr;
                m_zodiac_timed[i].Parser = pr;
                m_good_god[i].Parser = pr;
                m_ill_god[i].Parser = pr;
                m_well_timed[i].Parser = pr;
                m_bad_timed[i].Parser = pr;
                m_fiveElem_timed[i].Parser = pr;
                m_conflict_orientation[i].Parser = pr;
                m_happy_god[i].Parser = pr;
                m_fortune_god[i].Parser = pr;
            }

        }
        // Extra processing for solor-date's value
        private string getSolorDate(string strOldValue)
        {
            string strVal = strOldValue;

            Regex rgx = new Regex(@"(?<currDate>(\d{4}年\s*\d{1,2}月\s*\d{1,2}日))", RegexOptions.Compiled | RegexOptions.Multiline);
            Match mc = rgx.Match(strOldValue);
            if (mc != null)
            {
                strVal = mc.Groups["currDate"].Value;
            }

            return strVal;
        }

        // Extra processing for good-luck-value's value
        private string getGoodIllLuck(string strOldValue)
        {
            string strVal = "吉";

            if (strOldValue.IndexOf("\"凶\"==\"吉\"") > 0)
            {
                strVal = "凶";
            }

            return strVal;
        }
    }
    
    /// <summary>
    ///  Class for managing LaoHuangLi Page data 
    ///  1. Download data from LaoHuangLi.net
    ///  2. Parse data 
    ///  3. Save to DB
    /// </summary>
    class LaoHuangDaoBook
    {
#if DEBUG
        const string _LAOHUANGDAOBOOK_DIR_ = @"C:\Temp\LaoHuangDaoBook";
#else
        const string _LAOHUANGDAOBOOK_DIR_ = "HuangDaoBook";
#endif
        const string _OUTFILE_PREFIX_ = "LaoHuangDaoBook";
        const int m_startPage = 0; // Huangdao book of 2014 year
        const int m_endPage = 365;

        WebClient m_wclient = new WebClient();
        String m_url = "http://laohuangli.net/{0}/{0}-{1}-{2}.html";

        String m_strHtml;

        public LaoHuangDaoBook()
        {
            if (!Directory.Exists(_LAOHUANGDAOBOOK_DIR_))
            {
                Directory.CreateDirectory(_LAOHUANGDAOBOOK_DIR_);
            }
        }

        private string getBookUrl(int pageIdx)
        {
            DateTime dt = new DateTime(2014, 1, 1);
            dt = dt.AddDays(pageIdx);
            string url = string.Format(m_url, dt.Year, dt.Month, dt.Day);
            return url;
        }

        private void readWebData()
        {
            int page_count = m_endPage - m_startPage + 1;

            for (int i = 0; i < page_count; i++)
            {
                string targetUrl = getBookUrl(i);
                Debug.WriteLine(targetUrl);

                try
                {
                    m_strHtml = m_wclient.DownloadString(targetUrl);
                    string tmp_filename = getOutputFileName(i);

                    FileStream fs = new FileStream(tmp_filename, FileMode.CreateNew);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));

                    sw.Write(m_strHtml);

                    sw.Close();
                    fs.Close();
                }
                catch (WebException we)
                {
                    // Do nothing, try next url
                }
                catch (IOException ie)
                {
                    // Do nothing, try next url
                }

            }
        }

        private string getOutputFileName(int pageIdx)
        {
            string filename = null;

            if (m_strHtml != null && m_strHtml != "")
            {

                filename = string.Format("{0}\\{1}.{2}.{3}", _LAOHUANGDAOBOOK_DIR_, _OUTFILE_PREFIX_, pageIdx, "html");
            }
            else
            {
                filename = string.Format("{0}\\{1}.{2}.{3}", _LAOHUANGDAOBOOK_DIR_, _OUTFILE_PREFIX_, pageIdx, "html");
            }

            return filename;
        }

        public int parseHLDayFiles(string dirpath)
        {
            int nResult = 0;

            HdDBHelper db = new HdDBHelper();

            string[] filenames = Directory.GetFiles(dirpath);
            foreach (string fn in filenames)
            {
                FileStream fs = new FileStream(fn, FileMode.Open);
                if (fs != null)
                {
                    TextReader tr = new StreamReader(fs);
                    string strHtml = tr.ReadToEnd();
                    ZHtmlParser htmlParser = new ZHtmlParser(strHtml);
                    LaoHLDayEx lhlday = new LaoHLDayEx(htmlParser);

                    db.saveToDb(lhlday);
                    
                    tr.Close();
                }
                fs.Close();
            }

            return nResult;
        }

        internal bool Go()
        {
            bool r = false;

            // readWebData();

            parseHLDayFiles(_LAOHUANGDAOBOOK_DIR_);

            //saveData();

            return r;
        }
    }
}
