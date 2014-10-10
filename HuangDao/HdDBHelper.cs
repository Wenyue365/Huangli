using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using AstroSpider;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace HuangDao
{
    public class HdDBHelper
    {
        static  string db_host = "admin.yun03.yhosts.com";
        static  int db_port = 3306;
        static  string db_name = "wenyue365";
        static  string db_user = "wenyue365";
        static  string db_pass = "wenyue365$$$";
        static  string db_charset = "utf8"; // this value is query from the DB

        static int m_openedCount = 0;
        string m_connString = null;
        MySqlConnection m_connSql = null;

        public MySqlConnection ConnSql
        {
            get {
                initDb();
                return m_connSql;
            }
        }

        static string s_logfilename;
        static string s_data_path;

        public HdDBHelper()
        {
            s_logfilename = string.Format("db_log_{0}.log", DateTime.Now.ToLongDateString());
            s_data_path = HttpContext.Current.Server.MapPath("~/log/" + s_logfilename);

            initDb(); 
        }

        ~HdDBHelper() // Destructors cannot be called. They are invoked automatically
        {
            closeDB(); // Release the MySQL connection resource
        }

        public static string getConnectionStr(){
            return connStringBuilder(db_host, db_port, db_name, db_user, db_pass, db_charset);
        }
        public static string connStringBuilder(string host, int port, string dbname, string username, string password, string charset)
        {
            string cs = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};CharacterSet={5};Pooling=True;",
                host, port, dbname, username, password, charset);
            return cs;
        }
        private bool initDb()
        {
            bool result = true;
            try
            {
                if (m_connSql == null) // Open DB connection when it is null
                {
                    m_connString = connStringBuilder(db_host, db_port, db_name, db_user, db_pass, db_charset);
                    m_connSql = new MySqlConnection(m_connString);

                    m_connSql.Open();
                    m_openedCount++;

                    Writelog(string.Format("Open MySQL DB ({0}) connection successfully.", m_openedCount));
                }
                else if (m_connSql.State != ConnectionState.Open)// Re-Open the DB connection when it is not OPEN
                {
                    m_connSql.Open();
                    m_openedCount++;

                    Writelog(string.Format("Open MySQL DB ({0}) connection successfully.[ConnectionState.Open]", m_openedCount));
                }
            }
            catch (MySqlException e)
            {
                result = false;

                m_connSql = null;

                Writelog("initDb : " + e.Message);
            }

            return result;
        }

        public void closeDB()
        {
            if (m_connSql != null)
            {
                m_connSql.Close();
                m_connSql = null;

                m_openedCount--;
                Writelog(string.Format("Closed MySQL DB ({0}) connection successfully.", m_openedCount));
            }
        }
        public bool saveToDb(TXHuangDaoDay hdd)
        {
            bool result = false;

            string cmdText = string.Format("INSERT INTO wy_huangli(fid, showtime, lunerdate, goodtodo, badtodo) VALUES(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\')",
                             hdd.FID, hdd.ShowTime, hdd.LunerDate, hdd.GoodToDo, hdd.BadToDo);

            MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
            cmdSql.CommandType = CommandType.Text;
            cmdSql.CommandTimeout = 200;

            if (cmdSql.ExecuteNonQuery() == 1)
            {
                result = true;
            }

            return result;
        }

        // 线程工作函数：保存用户访问信息到数据库
        // This thread procedure performs the task.
        static void thprocSaveIPLocation(Object stateInfo)
        {
            UserInfo ui = (UserInfo)stateInfo;

            string cmdText = string.Format("INSERT INTO wy_userlog(userid, username, ip, loc, lastUpdateTime) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                           ui.id, ui.name, ui.ip, ui.loc, DateTime.Now);

            string connString = null;
            MySqlConnection connSql = null;

            try
            {
                // 打开数据库连接
                connString = connStringBuilder(db_host, db_port, db_name, db_user, db_pass, db_charset);
                connSql = new MySqlConnection(connString);
                connSql.Open();
                Writelog("thprocSaveIPLocation : Open MySQL DB connection successfully.");

                MySqlCommand cmdSql = new MySqlCommand(cmdText, connSql);
                cmdSql.CommandType = CommandType.Text;
                cmdSql.CommandTimeout = 200;

                if (cmdSql.ExecuteNonQuery() != 1)
                {
                    Writelog("Error : thprocSaveIPLocation - cmdSql.ExecuteNonQuery()");
                }

                connSql.Close();
            }
            catch (MySqlException e)
            {
                Writelog("thprocSaveIPLocation： MySqlException : " + e.Message);
            }
        }

        /// <summary>
        /// 函数：采用异步方式保存用户访问信息（日志）
        /// </summary>
        /// <param name="id">用户ID，缺省为 0</param>
        /// <param name="username">用户名， 缺省为 Visitor</param>
        /// <param name="ip">用户发起访问的源IP 地址</param>
        /// <param name="loc">IP 地址的归属地</param>
        /// <returns></returns>
        public bool saveToDbAsync(int id, string username, string ip, string loc)
        {
            bool result = false;

            // 初始化工作函数的参数
            UserInfo ui = new UserInfo();
            ui.id   = id;
            ui.name = username;
            ui.ip   = ip;
            ui.loc  = loc;

            // 使用 ThreadPool 完成异步保存操作
            if (ThreadPool.QueueUserWorkItem(new WaitCallback(thprocSaveIPLocation), ui))
            {
                result = true;
            }
            return result;
        }

        public TXHuangDaoDay selectHlData(string where_clause)
        {
            TXHuangDaoDay hdd = null;

            try
            {
                // Get record count
                string cmdText = "SELECT count(*) FROM wy_huangli " + where_clause;
                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

                int row_count = (int)cmdSql.ExecuteScalar();

                if (row_count > 0)
                {
                    TXHuangDaoDay[] hdds = new TXHuangDaoDay[row_count];

                    cmdText = "SELECT * FROM wy_huangli " + where_clause;
                    cmdSql.CommandText = cmdText;

                    MySqlDataReader sqlReader = cmdSql.ExecuteReader();
                    for (int i = 0; (i < row_count) && sqlReader.Read(); i++)
                    {
                        hdds[i].FID = sqlReader.GetString("fid");
                        hdds[i].ShowTime = sqlReader.GetDateTime("showtime");
                        hdds[i].LunerDate = sqlReader.GetString("lunerdate");
                        hdds[i].GoodToDo = sqlReader.GetString("goodtodo");
                        hdds[i].BadToDo = sqlReader.GetString("badtodo");
                    }

                    sqlReader.Close();
                }

            }
            catch (MySqlException ex)
            {
                hdd = null;
            }

            return hdd;
        }

        public string getHlYiDates(DateTime start_date, DateTime end_date, string yi_word)
        {
            string jsn_yiDates = null;

            try
            {
                string cmdText = string.Format("SELECT * FROM `wy_huangli`WHERE showtime > DATE_FORMAT( '{0}', '%Y/%c/%e' )  AND showtime < DATE_FORMAT( '{1}', '%Y/%c/%e' )  AND goodtodo LIKE '%{2}%'",
                    start_date.ToShortDateString(), end_date.ToShortDateString(), yi_word);
                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

                MySqlDataReader sqlReader = cmdSql.ExecuteReader();
                jsn_yiDates = "{[";
                string s = "";
                for (int i = 0; sqlReader.Read(); i++)
                {
                    s = sqlReader.GetDateTime("showtime").ToShortDateString();
                    jsn_yiDates += s + ",";
                }

                jsn_yiDates = jsn_yiDates.TrimEnd(',');
                jsn_yiDates += "]}";

                sqlReader.Close();
            }
            catch (MySqlException ex)
            {
                jsn_yiDates = null;
            }

            return jsn_yiDates;
        }

        public string getLunarDate(int year, int month, int day)
        {
            string strLunar = null;

            DateTime solarDate = new DateTime(year, month, day);

            try
            {
                string cmdText = string.Format("SELECT * FROM `wy_huangli`WHERE DATE_FORMAT(showtime, '%Y/%c/%e' ) = DATE_FORMAT( '{0}', '%Y/%c/%e' )",
                    solarDate);
                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

               
                MySqlDataReader sqlReader = cmdSql.ExecuteReader();

                if (sqlReader.Read())
                {
                    strLunar = sqlReader.GetString("lunerdate");
                }

                sqlReader.Close();

            }
            catch (MySqlException ex)
            {
                strLunar = null;
            }

            return strLunar;
        }

        public SinaHLDayEx getSinaHlInfo(int year, int month, int day)
        {
            DateTime solarDate = new DateTime(year, month, day);
            SinaHLDayEx hld = null;

            try
            {
                string cmdText = string.Format("SELECT * FROM wy_sinahuangli WHERE DATE_FORMAT(solarDate, '%Y/%c/%e' ) = DATE_FORMAT( '{0}', '%Y/%c/%e' )",
                    solarDate);

                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

                MySqlDataReader sqlReader = cmdSql.ExecuteReader();
                if (sqlReader.Read())
                {
                    hld = new SinaHLDayEx();

                    hld.m_solarDate  .Value = sqlReader.GetString("solarDate");
                    hld.m_lunarDate  .Value = sqlReader.GetString("lunarDate");
                    hld.m_yearOrder  .Value = sqlReader.GetString("yearOrder");
                    hld.m_zodiac     .Value = sqlReader.GetString("zodiac");
                    hld.m_monthOrder .Value = sqlReader.GetString("monthOrder");
                    hld.m_dayOrder   .Value = sqlReader.GetString("dayOrder");
                    hld.m_birthGod   .Value = sqlReader.GetString("birthGod");
                    hld.m_fiveElem   .Value = sqlReader.GetString("fiveElem");
                    hld.m_collide    .Value = sqlReader.GetString("collide");
                    hld.m_pengAvoid  .Value = sqlReader.GetString("pengAvoid");
                    hld.m_goodAngelYi.Value = sqlReader.GetString("goodAngelYi");
                    hld.m_evilAngelJi.Value = sqlReader.GetString("evilAngelJi");
                    hld.m_Yi         .Value = sqlReader.GetString("yi");
                    hld.m_Ji         .Value = sqlReader.GetString("ji");

                }
                sqlReader.Close(); // 必须关闭
                cmdSql = null;
                sqlReader = null;
            }
            catch (MySqlException ex)
            {
                hld = null;
                
                Writelog("getSinaHlInfo : " + ex.Message);
            }

            return hld;
        }

        public static void Writelog(string log_str)
        {
            try
            {
                FileStream fs = new FileStream(s_data_path, FileMode.OpenOrCreate | FileMode.Append);
                if (fs != null)
                {
                    TextWriter tr = new StreamWriter(fs, Encoding.UTF8);

                    tr.WriteLine(DateTime.Now.ToLongTimeString() + ": " + log_str);

                    tr.Close();
                    fs.Close();
                }
            }
            catch (IOException ie)
            {
                Debug.Write(ie.Message);
            }
        }

        public int saveToDb(LaoHLDayEx lhlday)
        {
            int nSaved = 0;

            for (int i = 0; i < lhlday.Length; i++)
            {
                string cmdText = string.Format("INSERT INTO wy_laohuangli " +
                    "(curr_date, ancient_hour, ancient_hour_fullname, solar_time_start, solar_time_end, " +
                    "star_god, straight_confict, good_ill_luck, zodiac_timed, good_god, ill_god, " +
                    "well_timed, bad_timed, fiveElem_timed, conflict_orientation, happy_god, fortune_god) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', " +
                    "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}','{16}')",
                    lhlday.m_curr_date.Value,
                    lhlday.m_ancient_hour[i].Value,
                    lhlday.m_ancient_hour_fullname[i].Value,
                    lhlday.m_solar_time_start[i].Value,
                    lhlday.m_solar_time_end[i].Value,
                    lhlday.m_star_god[i].Value,
                    lhlday.m_straight_confict[i].Value,
                    lhlday.m_good_ill_luck[i].Value,
                    lhlday.m_zodiac_timed[i].Value,
                    lhlday.m_good_god[i].Value,
                    lhlday.m_ill_god[i].Value,
                    lhlday.m_well_timed[i].Value,
                    lhlday.m_bad_timed[i].Value,
                    lhlday.m_fiveElem_timed[i].Value,
                    lhlday.m_conflict_orientation[i].Value,
                    lhlday.m_happy_god[i].Value,
                    lhlday.m_fortune_god[i].Value);

                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

                if (cmdSql.ExecuteNonQuery() == 1)
                {
                    nSaved++;
                }
            }
          
            return nSaved;
        }
		
        internal LaoHLHour getLaoHLHour(int year, int month, int day, int hour)
        {
            DateTime currDate = new DateTime(year, month, day, hour, 0, 0);
            LaoHLHour hlHour = null;

            try
            {
                string cmdText = string.Format(
                    "SELECT * " + 
                    "FROM (SELECT *, Hour(STR_TO_DATE(solar_time_start, '%H:%i:%s')) AS st, " + 
                    "Hour(STR_TO_DATE(solar_time_end, '%H:%i:%s')) AS et, " +
                    "Hour(DATE_FORMAT('{0}', '%H:%i:%s')) AS tt " +
                    "FROM wy_laohuangli " +
                    "WHERE STR_TO_DATE(curr_date, '%Y年%c月%e日') = DATE_FORMAT('{0}', '%Y-%m-%d') " +
                    ") tb " +
                    "WHERE (st <= tt AND et >= tt) OR (tt <= st and tt >= et)", currDate);
                MySqlCommand cmdSql = new MySqlCommand(cmdText, ConnSql);
                cmdSql.CommandType = CommandType.Text;

                MySqlDataReader sqlReader = cmdSql.ExecuteReader();
                if (sqlReader.Read())
                {
                    hlHour = new LaoHLHour();
                    hlHour.m_curr_date = DateTime.Parse(sqlReader.GetString("curr_date"));
                    hlHour.m_ancient_hour = sqlReader.GetString("ancient_hour");
                    hlHour.m_ancient_hour_fullname = sqlReader.GetString("ancient_hour_fullname");
                    hlHour.m_solar_time_start = sqlReader.GetString("solar_time_start");
                    hlHour.m_solar_time_end = sqlReader.GetString("solar_time_end");
                    hlHour.m_star_god = sqlReader.GetString("star_god");
                    hlHour.m_straight_confict = sqlReader.GetString("straight_confict");
                    hlHour.m_good_ill_luck = sqlReader.GetString("good_ill_luck");
                    hlHour.m_zodiac_timed = sqlReader.GetString("zodiac_timed");
                    hlHour.m_good_god = sqlReader.GetString("good_god");
                    hlHour.m_ill_god = sqlReader.GetString("ill_god");
                    hlHour.m_well_timed = sqlReader.GetString("well_timed");
                    hlHour.m_bad_timed = sqlReader.GetString("bad_timed");
                    hlHour.m_fiveElem_timed = sqlReader.GetString("fiveElem_timed");
                    hlHour.m_conflict_orientation = sqlReader.GetString("conflict_orientation");
                    hlHour.m_happy_god = sqlReader.GetString("happy_god");
                    hlHour.m_fortune_god = sqlReader.GetString("fortune_god");
                }
                sqlReader.Close(); // 必须关闭
                cmdSql = null;
                sqlReader = null;
            }
            catch (MySqlException ex)
            {
                hlHour = null;

                Writelog("getLaoHLHour : " + ex.Message);
            }

            return hlHour;
        }
    }
}