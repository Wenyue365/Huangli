using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace AstroSpider
{
    class _JSONTXHuangDaoDay
    {
        public string FID = null;
        public string F1 = null;
        public string F2 = null;
        public string F3 = null;
        public string F4 = null;
        public string F5 = null;
        public string F6 = null;
        public string F7 = null;

    }

    public class TXHuangDaoDay
    {
        public string FID;
        public DateTime ShowTime;
        public string LunerDate;
        public string Astro;
        public string GoodToDo;
        public string BadToDo;
        public TXHuangDaoDay()
        {

        }

        public TXHuangDaoDay(string json)
        {
            if (json != null)
            { 
                 parse(json);
            }
        }
        
        public void parse(string json)
        {
            string js = null;
            int st = json.IndexOf('{');
            int ls = json.IndexOf('}');
            if (st > 0 && ls > 0 && ls > st)
            {
                js = json.Substring(st, ls - st + 1);

                _JSONTXHuangDaoDay jo = JsonConvert.DeserializeObject<_JSONTXHuangDaoDay>(js);

                FID = jo.FID;
                int year = int.Parse(jo.F1);
                int month = int.Parse(jo.F2);
                int day = int.Parse(jo.F3);
                ShowTime = new DateTime(year, month, day);

                LunerDate = jo.F4;
                Astro = jo.F5;
                GoodToDo = jo.F6;
                BadToDo = jo.F7;
            }
        }

        public string SerialToXML()
        {
            string xml = null;
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, this);
            xml = textWriter.ToString();
            
            return xml;
        }

        internal string SerialToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public TXHuangDaoDay DeserializeFromXML(string xml)
        {
            var serializer = new XmlSerializer(this.GetType());
            TXHuangDaoDay hdd = null;

            using (TextReader reader = new StringReader(xml))
            {
                hdd = (TXHuangDaoDay)serializer.Deserialize(reader);
            }

            return hdd;
        }
    }

    class TXHuangDaoBook
    {
        #if DEBUG
        const string _TXHUANGDAOBOOK_DIR_ = @"C:\Temp\TxHuangDaoBook";
#else
        const string _TXHUANGDAOBOOK_DIR_ = "HuangDaoBook";
#endif
        const string _OUTFILE_PREFIX_ = "TxHuangDaoBook";
        const int m_startPage = 4496;
        const int m_endPage = 4860;

        WebClient m_wclient = new WebClient();
        string m_url = "http://data.astro.qq.com/hl/";

        String m_strHtml = null;

        public TXHuangDaoBook()
        {
            if (!Directory.Exists(_TXHUANGDAOBOOK_DIR_))
            {
                Directory.CreateDirectory(_TXHUANGDAOBOOK_DIR_);
            }
        }

        private string getBookUrl(int pageIdx)
        {
            double hlJsonFID = m_startPage + pageIdx;

            string url = m_url + Math.Floor(hlJsonFID/1000) + "/" + hlJsonFID + "/info.js";

            return url;
        }

        private void readWebData()
        {
            int page_count = m_endPage - m_startPage + 1;
            DateTime dt = new DateTime(2014, 1, 1);

            for (int i = 0; i < page_count; i++)
            {
                dt = dt.AddDays(1);
                m_strHtml = m_wclient.DownloadString(getBookUrl(i));
                TXHuangDaoDay hdd = new TXHuangDaoDay(m_strHtml);
                
                string tmp_filename = getOutputFileName(dt.Year, dt.Month, dt.Day);

                FileStream fs = new FileStream(tmp_filename, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));

                // sw.Write(hdd.SerialToJSON());
                sw.Write(hdd.SerialToXML());

                sw.Close();
                fs.Close();
            }
        }

        private string getOutputFileName(int year, int month, int day)
        {

            string filename = null;

            if (m_strHtml != null && m_strHtml != "")
            {
                filename = string.Format("{0}\\{1}.{2}-{3}-{4}.{5}", _TXHUANGDAOBOOK_DIR_, _OUTFILE_PREFIX_, year, month, day, "html");
            }

            return filename;
        }

        internal bool Go()
        {
            bool r = false;

            readWebData();

            //parseWebData();

            //saveData();

            return r;
        }
    }
}
