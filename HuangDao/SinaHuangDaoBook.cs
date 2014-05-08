using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;


namespace AstroSpider
{
    public class SinaHLEntryEx : Entry
    {
        char[] m_sepCharset = { ' ', '、' };
        string m_entryName = null;
        int m_idxOfVal = -1;
        string m_value = null;
        string[] m_values = null;

        public SinaHLEntryEx()
        { }

        public SinaHLEntryEx(string en, string xpath, int iVal = -1)
        {
            m_entryName = en;
            m_idxOfVal = iVal;
            base.xpath = xpath;
        }

        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// 重载基类 Entry 的同名函数，对val 值作进一步处理
        /// </summary>
        /// <returns>经过处理后的 m_value 值 </returns>
        override protected string parseValue()
        {
            base.val = base.parseValue(); // 使用基类的函数提取 val 值
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

    public class SinaHLDayEx
    {
        public SinaHLEntryEx m_date;          // 日期 
        public SinaHLEntryEx m_solarDate;     // 公历
        public SinaHLEntryEx m_lunarDate;     // 农历
        public SinaHLEntryEx m_yearOrder;     // 岁次
        public SinaHLEntryEx m_zodiac;        // 生肖
        public SinaHLEntryEx m_monthOrder;    // 月次
        public SinaHLEntryEx m_dayOrder;      // 日次
        public SinaHLEntryEx m_birthGod;      // 日胎神占方
        public SinaHLEntryEx m_fiveElem;      // 五行
        public SinaHLEntryEx m_collide;       // 冲
        public SinaHLEntryEx m_pengAvoid;     // 彭祖百忌
        public SinaHLEntryEx m_goodAngelYi;   // 吉神宜趋
        public SinaHLEntryEx m_evilAngelJi;   // 凶神宜忌
        public SinaHLEntryEx m_Yi;            // 宜
        public SinaHLEntryEx m_Ji;            // 忌

        public SinaHLDayEx() {
            m_date = new SinaHLEntryEx();
            m_solarDate = new SinaHLEntryEx();
            m_lunarDate = new SinaHLEntryEx();
            m_yearOrder = new SinaHLEntryEx(); 
            m_zodiac = new SinaHLEntryEx();
            m_monthOrder = new SinaHLEntryEx();
            m_dayOrder = new SinaHLEntryEx();
            m_birthGod = new SinaHLEntryEx();
            m_fiveElem = new SinaHLEntryEx();
            m_collide = new SinaHLEntryEx();
            m_pengAvoid = new SinaHLEntryEx();
            m_goodAngelYi = new SinaHLEntryEx();
            m_evilAngelJi = new SinaHLEntryEx();
            m_Yi = new SinaHLEntryEx();
            m_Ji = new SinaHLEntryEx();
        }

        public SinaHLDayEx(ZHtmlParser pr)
        {
            m_date = new SinaHLEntryEx("公历", "//*[@id=\"con01-0\"]/div[2]/div[2]/p/text()", 0);
            m_solarDate = new SinaHLEntryEx("公历", "//*[@id=\"con01-0\"]/div[2]/div[2]/p/text()", 0);
            m_lunarDate = new SinaHLEntryEx("农历", "//*[@id=\"con01-0\"]/div[2]/div[2]/p/text()", 1);
            m_yearOrder = new SinaHLEntryEx("岁次", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[1]/td[2]/text()", 0); // 注意：xpath 里的 tbody 须忽略！！！
            m_zodiac = new SinaHLEntryEx("岁次", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[1]/td[2]/text()", 1);
            m_monthOrder = new SinaHLEntryEx("岁次", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[1]/td[2]/text()", 2);
            m_dayOrder = new SinaHLEntryEx("岁次", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[1]/td[2]/text()", 3);
            m_birthGod = new SinaHLEntryEx("日胎神占方", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[2]/td[2]/text()");
            m_fiveElem = new SinaHLEntryEx("五行", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[3]/td[2]/text()");
            m_collide = new SinaHLEntryEx("冲", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[4]/td[2]/text()");
            m_pengAvoid = new SinaHLEntryEx("彭祖百忌", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[5]/td[2]/text()");
            m_goodAngelYi = new SinaHLEntryEx("吉神宜趋", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[6]/td[2]/text()");
            m_evilAngelJi = new SinaHLEntryEx("凶神宜忌", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[8]/td[2]/text()");
            m_Yi = new SinaHLEntryEx("宜", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[7]/td[2]/text()");
            m_Ji = new SinaHLEntryEx("忌", "//*[@id=\"con01-0\"]/div[2]/div[2]/table/tr[9]/td[2]/text()");

            // 为Entry 实例的 Parser 对象赋值，将会同时完成相应数值的解析
            m_date.Parser = pr;
            m_solarDate.Parser = pr;
            m_lunarDate.Parser = pr;
            m_yearOrder.Parser = pr;
            m_zodiac.Parser = pr;
            m_monthOrder.Parser = pr;
            m_dayOrder.Parser = pr;
            m_birthGod.Parser = pr;
            m_fiveElem.Parser = pr;
            m_collide.Parser = pr;
            m_pengAvoid.Parser = pr;
            m_goodAngelYi.Parser = pr;
            m_evilAngelJi.Parser = pr;
            m_Yi.Parser = pr;
            m_Ji.Parser = pr;

        }
    }

    public class SinaHDDB
    {
        string m_connString = null;
        MySqlConnection m_connSql = null;

        string connStringBuilder(string host, int port, string dbname, string username, string password, string charset)
        {
            string cs = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};CharacterSet={5}",
                host, port, dbname, username, password, charset);
            return cs;
        }

        public bool initDb()
        {
            bool result = true;

            const string db_host = "admin.yun03.yhosts.com";
            const int db_port = 3306;
            const string db_name = "wenyue365";
            const string db_user = "wenyue365";
            const string db_pass = "wenyue365$$$";
            const string db_charset = "utf8"; // this value is query from the DB

            if (m_connSql == null) // Open DB connection when it is null
            {
                try
                {
                    m_connString = connStringBuilder(db_host, db_port, db_name, db_user, db_pass, db_charset);
                    m_connSql = new MySqlConnection(m_connString);

                    m_connSql.Open();
                }
                catch (MySqlException e)
                {
                    result = false;
                    m_connSql = null;
                }
            }

            return result;
        }

        public void closeDb()
        {
            if (m_connSql != null)
            {
                m_connSql.Close();
                m_connSql = null;
            }
        }

        public bool saveToDb(SinaHLDayEx shl)
        {
            bool result = false;

            try
            {
                DateTime solarDate = DateTime.Parse(shl.m_date.Value);

                string cmdText = string.Format(
                    "INSERT INTO wy_sinahuangli(solarDate, soloarDate_str, lunarDate, yearOrder, zodiac, monthOrder, dayOrder, birthGod, fiveElem, collide, pengAvoid, goodAngelYi, evilAngelJi, yi, ji) " +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                    solarDate.ToShortDateString(), shl.m_solarDate.Value, shl.m_lunarDate.Value, shl.m_yearOrder.Value, shl.m_zodiac.Value, shl.m_monthOrder.Value, shl.m_dayOrder.Value,
                    shl.m_birthGod.Value, shl.m_fiveElem.Value, shl.m_collide.Value, shl.m_pengAvoid.Value, shl.m_goodAngelYi.Value, shl.m_evilAngelJi.Value, shl.m_Yi.Value, shl.m_Ji.Value);

                Debug.WriteLine(cmdText);

                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
                cmdSql.CommandType = CommandType.Text;

                if (cmdSql.ExecuteNonQuery() == 1)
                {
                    result = true;
                }
            }
            catch (FormatException fx)
            {
                // Do nothing 
            }

            return result;
        }

    }
    class SinaHuangDaoBook
    {
#if DEBUG
        const string _SINAHUANGDAOBOOK_DIR_ = @"C:\Temp\SinaHuangDaoBook";
#else
        const string _SINAHUANGDAOBOOK_DIR_ = "HuangDaoBook";
#endif
        const string _OUTFILE_PREFIX_ = "SinaHuangDaoBook";
        const int m_startPage = 0; // Huangdao book of 2014 year
        const int m_endPage = 365;

        WebClient m_wclient = new WebClient();
        String m_url = "http://astro.sina.com.cn/jian/hdrl/{0:D4}-{1:D2}-{2:D2}.shtml";

        String m_strHtml;

        public SinaHuangDaoBook()
        {
            if (!Directory.Exists(_SINAHUANGDAOBOOK_DIR_))
            {
                Directory.CreateDirectory(_SINAHUANGDAOBOOK_DIR_);
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
                catch(WebException we)
                {
                    // Do nothing, try next url
                }
                catch(IOException ie)
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
                ZHtmlParser pr = new ZHtmlParser(m_strHtml);
                Entry entry = new Entry(pr);
                entry.xpath = "//*[@id=\"con01-0\"]/div[2]/div[2]/p/text()";

                string[] values = entry.val.Split(' ');
                string fex = values[0];

                filename = string.Format("{0}\\{1}.{2}.{3}", _SINAHUANGDAOBOOK_DIR_, _OUTFILE_PREFIX_, fex, "html");
            }
            else
            {
                filename = string.Format("{0}\\{1}.{2}.{3}", _SINAHUANGDAOBOOK_DIR_, _OUTFILE_PREFIX_, pageIdx, "html");
            }

            return filename;
        }



        /*
        class SinaHLEntry
        {
            protected string m_entryName;
            public string EntryName
            {
                get { return m_entryName; }
                set { m_entryName = value; }
            }
            protected bool m_valid = false;
            public bool Valid
            {
                get { return m_valid; }
                set { m_valid = value; }
            }

            protected string m_strline;

            virtual public string StrLine
            {
                get { return m_strline; }
                set
                {
                    m_strline = value;

                    if (m_strline != null && m_strline != "")
                    {
                        removeTags();
                        abstractFields();
                        abstractValues();

                        if (m_strline != null && (m_value != null && m_value != ""))
                        {
                            m_valid = true;
                        }
                        else
                        {
                            m_valid = false;
                        }
                    }
                }
            }
            string m_field;

            public string FieldName
            {
                get { return m_field; }
                set { m_field = value; }
            }

            protected string m_value;

            public string Value
            {
                get { return m_value; }
                set { m_value = value; }
            }

            protected string[] m_values;

            public string[] Values
            {
                get { return m_values; }
                set { m_values = value; }
            }

            public SinaHLEntry() { }
            public SinaHLEntry(string entryName)
            {
                m_entryName = entryName;
            }

            char[] sepchars = { '：' }; // 用于分隔字段名称和字段值的字符 全角冒号
            char[] sepchars_value = { '　' }; // 用于分隔"值"字段的字符 全角空格

            const char tag_start_char = '<';
            const char tag_end_char = '>';
            private void removeTags()
            {
                int tag_start_idx = 0;
                int tag_end_idx = 0;

                do
                {
                    tag_start_idx = m_strline.IndexOf(tag_start_char);
                    tag_end_idx = m_strline.IndexOf(tag_end_char);

                    if (tag_start_idx < tag_end_idx)
                    {
                        string tag_str = m_strline.Substring(tag_start_idx, tag_end_idx - tag_start_idx + 1);
                        m_strline = m_strline.Replace(tag_str, " ");
                    }

                } while (tag_start_idx >= 0 && tag_end_idx >= 0 && (tag_end_idx > tag_start_idx));

                m_strline = m_strline.Trim();
            }

            private void abstractFields()
            {
                string[] ss = m_strline.Split(sepchars);
                if (ss.Length == 2)
                {
                    m_field = ss[0];
                    m_value = ss[1];
                }
                else
                {
                    m_field = m_entryName;
                    m_value = m_strline;
                }
            }

            private void abstractValues()
            {
                if (m_value != null)
                {
                    string[] ss = m_value.Split(sepchars_value);
                    if (ss.Length > 0)
                    {
                        m_values = ss;
                    }
                }
            }
        };

        class SinaHLDateTimeEntry : SinaHLEntry
        {
            Regex rx = null;

            public SinaHLDateTimeEntry(string entryName)
                : base(entryName)
            {

            }

            override public string StrLine
            {
                set
                {
                    m_strline = value;

                    rx = new Regex(@"\d{4}\-\d{2}\-\d{2}", RegexOptions.Compiled | RegexOptions.Singleline);
                    Match mc = rx.Match(value);

                    if (mc != null && mc.Groups.Count == 1)
                    {
                        m_value = mc.Groups[0].ToString();
                    } 
                }
                get
                {
                    return m_strline;
                }
            }
        }
        class SinaHLZodiacEntry : SinaHLEntry
        {
            public SinaHLZodiacEntry(string entryName)
                : base(entryName)
            {

            }

            override public string StrLine
            {
                set
                {
                    base.StrLine = value;  // 调用基类函数解析字符串
                    const string subEntryName = "生肖";
                    foreach (string v in m_values)
                    {
                        if (v.IndexOf(subEntryName) >= 0)
                        {
                            m_value = v.Substring(3);
                        }
                    }
                }
                get
                {
                    return m_strline;
                }
            }
        }

        class SinaHLYearOrderEntry : SinaHLEntry
        {
            public SinaHLYearOrderEntry(string entryName)
                : base(entryName){}

            override public string StrLine
            {
                set
                {
                    base.StrLine = value;  // 调用基类函数解析字符串

                    const string subEntryName = "年";
                    foreach (string v in m_values)
                    {
                        if (v.IndexOf(subEntryName) >= 0)
                        {
                            m_value = v.Substring(0,2);
                        }
                    }
                }
                get
                {
                    return m_strline;
                }
            }
        }

        class SinaHLMonthOrderEntry : SinaHLEntry
        {
            public SinaHLMonthOrderEntry(string entryName)
                : base(entryName){}
            override public string StrLine
            {
                set
                {
                    base.StrLine = value;  // 调用基类函数解析字符串
                    const string subEntryName = "月";
                    foreach (string v in m_values)
                    {
                        if (v.IndexOf(subEntryName) >= 0)
                        {
                            m_value = v.Substring(0,2);
                        }
                    }
                }
                get
                {
                    return m_strline;
                }
            }
        }
        class SinaHLDayOrderEntry : SinaHLEntry
        {
            public SinaHLDayOrderEntry(string entryName)
                : base(entryName){}
            override public string StrLine
            {
                set
                {
                    base.StrLine = value;  // 调用基类函数解析字符串
                    const string subEntryName = "日";
                    foreach (string v in m_values)
                    {
                        if (v.IndexOf(subEntryName) >= 0)
                        {
                            m_value = v.Substring(0,2);
                        }
                    }
                }
                get
                {
                    return m_strline;
                }
            }
        }
        class SinaHLLunarEntry : SinaHLEntry
        {
            public SinaHLLunarEntry(string entryName)
                : base(entryName){}
            override public string StrLine
            {
                set
                {
                    base.StrLine = value;  // 调用基类函数解析字符串

                    foreach (string v in m_values)
                    {
                        if (v.IndexOf(m_entryName) >= 0)
                        {
                            m_value = v;
                        }
                    }
                }
                get
                {
                    return m_strline;
                }
            }
        }
        class SinaHLDay
        {
            public SinaHLEntry m_date;          // 日期 
            public SinaHLEntry m_solarDate;     // 公历
            public SinaHLEntry m_lunarDate;     // 农历
            public SinaHLEntry m_yearOrder;     // 岁次
            public SinaHLEntry m_zodiac;        // 生肖
            public SinaHLEntry m_monthOrder;    // 月次
            public SinaHLEntry m_dayOrder;      // 日次
            public SinaHLEntry m_birthGod;      // 日胎神占方
            public SinaHLEntry m_fiveElem;      // 五行
            public SinaHLEntry m_collide;       // 冲
            public SinaHLEntry m_pengAvoid;     // 彭祖百忌
            public SinaHLEntry m_goodAngelYi;   // 吉神宜趋
            public SinaHLEntry m_evilAngelJi;   // 凶神宜忌
            public SinaHLEntry m_Yi;            // 宜
            public SinaHLEntry m_Ji;            // 忌
            public bool m_valid;
            int m_validCount = 0;

            public SinaHLDay()
            {
                m_date = new SinaHLDateTimeEntry("黄道日历");    // 注意：这里名称不是[日期]
                m_solarDate = new SinaHLEntry("公元");           // 注意：这里名称不是[公历]
                m_lunarDate = new SinaHLLunarEntry("农历");
                m_yearOrder = new SinaHLYearOrderEntry("岁次");
                m_zodiac = new SinaHLZodiacEntry("生肖");
                m_monthOrder = new SinaHLMonthOrderEntry("岁次");
                m_dayOrder = new SinaHLDayOrderEntry("岁次");
                m_birthGod = new SinaHLEntry("日胎神占方");
                m_fiveElem = new SinaHLEntry("五行");
                m_collide = new SinaHLEntry("冲");
                m_pengAvoid = new SinaHLEntry("彭祖百忌");
                m_goodAngelYi = new SinaHLEntry("吉神宜趋");
                m_evilAngelJi = new SinaHLEntry("凶神宜忌");
                m_Yi = new SinaHLEntry("宜");
                m_Ji = new SinaHLEntry("忌");
            }

            public bool parseField(string line)
            {
                bool bResult = false;

                if (line == null)
                {
                    return bResult;
                }

                if (line.IndexOf(m_date.EntryName) >= 0)
                {
                    m_date.StrLine = line;
                }
                else if (line.IndexOf(m_solarDate.EntryName) >= 0)
                {
                    m_solarDate.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_lunarDate.EntryName) >= 0)
                {
                    m_lunarDate.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_yearOrder.EntryName) >= 0)
                {
                    m_yearOrder.StrLine = line;
                    m_validCount++;

                    m_zodiac.StrLine = line;
                    m_validCount++;

                    m_monthOrder.StrLine = line;
                    m_validCount++;

                    m_dayOrder.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_birthGod.EntryName) >= 0)
                {
                    m_birthGod.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_fiveElem.EntryName) >= 0)
                {
                    m_fiveElem.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_collide.EntryName) >= 0)
                {
                    m_collide.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_pengAvoid.EntryName) >= 0)
                {
                    m_pengAvoid.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_goodAngelYi.EntryName) >= 0)
                {
                    m_goodAngelYi.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_evilAngelJi.EntryName) >= 0)
                {
                    m_evilAngelJi.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_Yi.EntryName) >= 0)
                {
                    m_Yi.StrLine = line;
                    m_validCount++;
                }
                else if (line.IndexOf(m_Ji.EntryName) >= 0)
                {
                    m_Ji.StrLine = line;
                    m_validCount++;
                }

                m_valid = (m_validCount) > 0 ? true : false;
                bResult = m_valid;

                return bResult;
            }
        }
        public int parseHuangdaoInfo(string dirpath)
        {
            int nResult = 0;

            const int start_line = 150;
            const int line_count = 23;

            var filenames = Directory.EnumerateFiles(dirpath);
            foreach (string fn in filenames)
            {
                FileStream fs = new FileStream(fn, FileMode.Open);
                if (fs != null)
                {
                    TextReader tr = new StreamReader(fs);

                    for (int i = 0; i < start_line; i++)
                    {
                        tr.ReadLine(); // Skip specified lines
                    }

                    SinaHLDay hlday = new SinaHLDay();

                    for (int i = 0; i < line_count; i++)
                    {
                        hlday.parseField(tr.ReadLine());

                    }

                    if (hlday.m_valid)
                    {
                        saveToDb(hlday);

                    }

                    tr.Close();
                }

                fs.Close();
            }

            return nResult;

        }
        */

        public int parseHLDayFiles(string dirpath)
        {
            int nResult = 0;

            SinaHDDB db = new SinaHDDB();
            db.initDb();

            string[] filenames = Directory.GetFiles(dirpath);
            foreach (string fn in filenames)
            {
                FileStream fs = new FileStream(fn, FileMode.Open);
                if (fs != null)
                {
                    TextReader tr = new StreamReader(fs);
                    string strHtml = tr.ReadToEnd();
                    ZHtmlParser htmlParser = new ZHtmlParser(strHtml);
                    SinaHLDayEx hlday = new SinaHLDayEx(htmlParser);

                    db.saveToDb(hlday);

                    tr.Close();
                }
                fs.Close();
            }
            db.closeDb();
            return nResult;
        }

        internal bool Go()
        {
            bool r = false;

            // readWebData();

            parseHLDayFiles(_SINAHUANGDAOBOOK_DIR_);
            
            //saveData();

            return r;
        }
    }
}
