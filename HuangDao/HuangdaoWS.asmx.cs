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
using System.Text;
using System.Text.RegularExpressions;

using LaoHuangLi;
using DataParsers;



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
        ~HDWebservices()
        {

        }

        [WebMethod]
        public string HelloWorld(string data)
        {
            return string.Format("{0} : Call Hello World Successfully.", data);
        }
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
                db.closeDB();
            }

            return jsn_yi_dates;
        }
        
        [WebMethod]
        public string getLunarDate(int year, int month, int day)
        {
            string strLunar = null;
             HdDBHelper db = new HdDBHelper();
             if (db != null)
             {
                 strLunar = db.getLunarDate(year, month, day);
                 db.closeDB();
             }

            return strLunar;
        }
        
        [WebMethod]
        public SinaHLDayEx getSinaHlInfo(int year, int month, int day)
        {
            SinaHLDayEx hld = null;
            HdDBHelper db = new HdDBHelper();
            if (db != null)
            {
                hld = db.getSinaHlInfo(year, month, day);
                db.closeDB();
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
                db.closeDB();
            }

            /*string strXML = null;
            XmlSerializer xmlSerializer = new XmlSerializer(hlHour.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, hlHour);
            strXML = textWriter.ToString();

            return strXML;*/

            return hlHour;
        }

        [WebMethod]
        public PageBase calcCarNumber(string carNum)
        {
            string page_url = string.Format("http://laohuangli.net/chepaihaoma.aspx?s={0}", carNum);

            CalcNumberPage calcPage = new CalcNumberPage(page_url);
            PageBase pg = new PageBase();

            pg.Clone(calcPage);

            return pg;
        }

        [WebMethod]
        public PageBase calcName(string firstName, string lastName)
        {
            const string page_url = "http://laohuangli.net/xingmingceshi.aspx?s1={0}&s2={1}";
            string fName = HttpUtility.UrlEncode(firstName, Encoding.GetEncoding("GB2312"));
            string lName = HttpUtility.UrlEncode(lastName, Encoding.GetEncoding("GB2312"));
            string[] prms;
            if (lastName.Length > 2)
            {
                prms = new string[] { firstName, lastName.Substring(0, 1), lastName.Substring(1, 2) }; // 只取名字的 2 个字符进行测算
            }
            else
            {
                prms = new string[] { firstName, lastName };
            }

            CalcNamePage calcNamePage = new CalcNamePage(string.Format(page_url, fName, lName), prms);

            PageBase pg = new PageBase();

            pg.Clone(calcNamePage);

            return pg;
        }
        /// <summary>
        /// 函数：将字符串中的非 ASCII 字符转为 ESCAPE 字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string EncodeToGB2312(string str)
        {
            string tmpStr = null;

            foreach (char c in str)
            {
                if (c > 256)
                {
                    tmpStr += HttpUtility.UrlEncode(c.ToString(), Encoding.GetEncoding("GB2312"));
                }
                else
                {
                    tmpStr += c;
                }
            }

            return tmpStr;
        }

        public class ParamPair 
        {
            public string name;
            public string value;

            public ParamPair(string name,string value)
            {
                this.name = name;
                this.value = value;
            }
        }

        public class Params 
        {
            public List<ParamPair> ParamList = new List<ParamPair>();
            public void Add(string name, string value){
                this.ParamList.Add(new ParamPair(name, value));
            }

            // 函数：组装一个 QueryString
            internal string toQueryString()
            {
                string qystr = null;
                foreach(ParamPair p in this.ParamList)
                {       
                    if (qystr != null)
                    {
                        qystr += "&" + p.name +"="+ p.value;
                    }
                    else 
                    {
                        qystr = p.name +"="+ p.value; 
                    }
                }
                return qystr;
            }

            // // 函数：组装一个 JSON 字符串
            internal string toJSONString()
            {
                string jsnstr = "{";

                foreach(ParamPair p in this.ParamList)
                {
                    jsnstr += string.Format("'{0}':'{1}'", p.name, p.value);
                }
                
                jsnstr += "}";

                return jsnstr;
            }
        }

        [WebMethod]
        public PageBase calcCouple(string qrystrCoupleInfo)
        {
            const string base_url = "http://laohuangli.net/suanming/peidui.aspx";
            //const string referer_url = "http://laohuangli.net/suanming/peidui.html";
            //const string cookies_str = "bdshare_firstime=1403276609702; 1405221000|10882837|82082|0|0|0=1405221000%7C10882837%7C82082%7C0%7C0%7C0; amvid=8529d5f7dc5838e61f425997306019a6; AJSTAT_ok_pages=2; AJSTAT_ok_times=15";
#if DEBUG_OFFLINE
            qrystrCoupleInfo = "n=1973&y=02&r=07&s=05&f=30&nn=%C4%D0&d1=%C9%CF%BA%A3&d2=%C9%CF%BA%A3&d3=%BB%C6%C6%D6%C7%F8&xm=%CF%EE%D3%F0&n2=1972&y2=04&r2=08&s2=06&f2=10&nn2=%C5%AE&d12=%CC%EC%BD%F2&d22=%CC%EC%BD%F2&d32=%BA%CD%C6%BD%C7%F8&xm2=%C1%F5%B0%EE";
#endif
            qrystrCoupleInfo = EncodeToGB2312(qrystrCoupleInfo);
            
            string target_url = base_url + "?" + qrystrCoupleInfo;
            string[] queryParams = new string[] { qrystrCoupleInfo }; /* Create string array for parameters*/

            CalcCouplePage calcNamePage = new CalcCouplePage(target_url, queryParams);

            PageBase pg = new PageBase();

            pg.Clone(calcNamePage);

            return pg;
        }

        [WebMethod]
        public string queryIPLocation(string qryIP)
        {
            string strLocation = null;
            // 准备数据

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;   

            // 设置 HTTP 请求头字段值
            wc.Headers.Add("Content-Type","text/javascript;charset=gbk");     
            //wc.Headers.Add("Connection","Keep-Alive");
            wc.Headers.Add("Referer","http://www.baidu.com");
            wc.Headers.Add("cookies","BAIDUID=DB86C90830068DF4C8BC1902ECCDF511:FG=1&BDUSS=mZuZmM3fjY4fjA5bHU2WGhTMEgyVVd0eGd1TDZwWURDYWthc0x4STUycEp1MmhUQVFBQUFBJCQAAAAAAAAAAAEAAADwDnEBWmVuY2VuZ0xpdQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEkuQVNJLkFTc&BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0&H_PS_PSSID=7692_7305_7508_1444_7571_7801_6996_7729_7780_6506_6017_7673_7607_7799_7632_6888_7415_7688_7909_7475");

            // 设置 QueryString 参数
            wc.QueryString.Add("query", qryIP);
            wc.QueryString.Add("co","");
            wc.QueryString.Add("resource_id","6006");
            wc.QueryString.Add("t","1406374537389");
            wc.QueryString.Add("ie","utf8");
            wc.QueryString.Add("oe","gbk");
            wc.QueryString.Add("format","json");
            wc.QueryString.Add("tn","baidu");
            wc.QueryString.Add("cb","op_aladdin_callback");
            wc.QueryString.Add("cb","jQuery110202710218361038101_1406374502948");
            wc.QueryString.Add("_","1406374502957");

            string base_url = "http://opendata.baidu.com/api.php";
            string strResp = wc.DownloadString(base_url);

            if (strResp != null)
            {
                Regex re = new Regex(@"\""location\"":\""(?<loc>([\w\s]+))\""\,", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (re != null)
                {
                    Match mch = re.Match(strResp);
                    if (mch != null && mch.Groups.Count > 0)
                    {
                        strLocation = mch.Groups[1].Value;
                    }
                }
            }

            // 采用（单独线程）异步方式保存用户访问地址到数据库
            HdDBHelper db = new HdDBHelper();
            db.saveToDbAsync(0, "Tester", qryIP, strLocation);
            db.closeDB();
   
            return strLocation;
        }
        
        [WebMethod]
        public PageBase SheupCalcCouple(string manName, string womanName)
        {
            const string base_url = "http://www.sheup.com/xingmingyuanfen.php";

            PostData postData = new PostData();
            postData.Add("namea", manName);
            postData.Add("nameb", womanName);
            postData.Add("Submit", "%D0%D5%C3%FB%D4%B5%B7%D6%C5%E4%B6%D4");
 
            SheupCoupleCalcPage sheupPage = new SheupCoupleCalcPage(base_url, postData);

            PageBase pg = new PageBase();

            pg.Clone(sheupPage);

            return pg;
        }
    }
}
