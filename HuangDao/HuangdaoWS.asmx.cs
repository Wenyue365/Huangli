using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AstroSpider;
using System.IO;
using System.Diagnostics;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Serialization;

namespace HuangDao
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://wenyue365.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class HDWebservices : System.Web.Services.WebService
    {
        [WebMethod]
        public TXHuangDaoDay getHuangdaoOfDay(DateTime dt)
        {
            TXHuangDaoDay hdd = new TXHuangDaoDay();

            string xml = "";

            string xmlFileName = "./data/TxHuangDaoBook.2014-4-18.html";
            try
            {
                FileStream fs = new FileStream(xmlFileName, FileMode.Open);
                TextReader rd = new StreamReader(fs);

                xml = rd.ReadToEnd();
            }
            catch(FileLoadException e)
            {
                Debug.Write(e.Message);
            }

            hdd.DeserializeFromXML(xml);

            return hdd;
        }
       
        [WebMethod]
        public string getWordAbstract(string word)
        {
            string ast = null;
            string data_path = HttpContext.Current.Server.MapPath("~/HuangLiData/" + "YiJiData.txt");
            FileStream fs = new FileStream(data_path, FileMode.Open);
            if (fs != null)
            {
                TextReader trd = new StreamReader(fs);
                for (string strLine = strLine = trd.ReadLine(); strLine != null; strLine = trd.ReadLine())
                {
                    if (strLine.IndexOf(word) >= 0)
                    {
                        ast = strLine.Substring(word.Length + 1);
                        break;
                    }
                }
                trd.Close();
                fs.Close();
            }

            return ast;
        }

        [WebMethod]
        public string getShiChengInfo(int hour)
        {
            string sc = null;
            string data_path = HttpContext.Current.Server.MapPath("~/HuangLiData/" + "ShiCheng.txt");
            FileStream fs = new FileStream(data_path, FileMode.Open);
            if (fs != null)
            {
                int[,] hours = new int[12, 3]{{23,00,01},{01,02,03},{03,04,05},{05,06,07},{07,08,09},{09,10,11},
                {11,12,13},{13,14,15},{15,16,17},{17,18,19},{19,20,21},{21,22,23}};

                int shicheng_idx = ((hour+1)%24)/2;
                int i = 0;
                TextReader trd = new StreamReader(fs);
                for (string strLine = strLine = trd.ReadLine(); strLine != null; strLine = trd.ReadLine())
                {
                    if (i == shicheng_idx)
                    {
                        char[] sepCharsets =  {','};
                        string[] values = strLine.Split(sepCharsets);
                        sc = values[3].Trim();
                        break;
                    }

                    i++;
                }
                trd.Close();
                fs.Close();
            }

            return sc;
        }
        [WebMethod]
        public string getHlYiDates(DateTime start_date, DateTime end_date, string yi_word)
        {
            string jsn_yi_dates = null;
            HdDBHelper db = new HdDBHelper();
            if (db != null)
            {
                jsn_yi_dates = db.getHlYiDates(start_date, end_date, yi_word);

                db.Close();
            }

            return jsn_yi_dates;
        }
        
        [WebMethod]
        public string getLunarDate(int year, int month, int day)
        {
            string jsn_lunar = null;
             HdDBHelper db = new HdDBHelper();
             if (db != null)
             {
                 jsn_lunar = db.getLunarDate(year, month, day);
                 db.Close();
             }

            return jsn_lunar;
        }
        
        [WebMethod]
        public SinaHLDayEx getSinaHlInfo(int year, int month, int day)
        {
            SinaHLDayEx hld = null;
            HdDBHelper db = new HdDBHelper();
            if (db != null)
            {
                hld = db.getSinaHlInfo(year, month, day);
                db.Close();
            }

            return hld;
        }

        [WebMethod]
        public LaoHLHour getLaoHLHour(int year, int month, int day, int hour)
        {
            LaoHLHour hlHour = null;
            HdDBHelper db = new HdDBHelper();
            if (db != null)
            {
                hlHour = db.getLaoHLHour(year, month, day, hour);
                db.Close();
            }

            /*string strXML = null;
            XmlSerializer xmlSerializer = new XmlSerializer(hlHour.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, hlHour);
            strXML = textWriter.ToString();

            return strXML;*/

            return hlHour;
        }
    }
}
