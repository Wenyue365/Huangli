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

namespace HuangDao
{
    public class HdDBHelper
    {
        const string db_host = "admin.yun03.yhosts.com";
        const int db_port = 3306;
        const string db_name = "wenyue365";
        const string db_user = "wenyue365";
        const string db_pass = "wenyue365$$$";
        const string db_charset = "utf8"; // this value is query from the DB

        static int ref_count = 0;
        static string m_connString = null;
        static MySqlConnection m_connSql = null;

        public HdDBHelper()
        {
            ref_count++;

            initDb(); 
        }

        ~HdDBHelper() // Destructors cannot be called. They are invoked automatically
        {
            --ref_count;

            if (ref_count == 0)
            {
                Close(); // Release the MySQL connection resource
            }
        }

        string connStringBuilder(string host, int port, string dbname, string username, string password, string charset)
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

                    Writelog("Open MySQL DB connection successfully.");
                }
                else if (m_connSql.State != ConnectionState.Open)// Re-Open the DB connection when it is not OPEN
                {
                    m_connSql.Open();
                }
            }
            catch (MySqlException e)
            {
                result = false;
                m_connSql = null;

                Writelog(e.Message);
            }

            return result;
        }
        public void Close()
        {
            if (m_connSql != null)
            {
                m_connSql.Close();
                m_connSql = null;

                Writelog("Closed MySQL DB connection successfully.");
            }
        }
        public bool saveToDb(TXHuangDaoDay hdd)
        {
            bool result = false;

            string cmdText = string.Format("INSERT INTO wy_huangli(fid, showtime, lunerdate, goodtodo, badtodo) VALUES(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\')",
                             hdd.FID, hdd.ShowTime, hdd.LunerDate, hdd.GoodToDo, hdd.BadToDo);

            MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
            cmdSql.CommandType = CommandType.Text;
            cmdSql.CommandTimeout = 200;

            if (cmdSql.ExecuteNonQuery() == 1)
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
                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
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
                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
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
            string jsn_lunar = null;

            DateTime solarDate = new DateTime(year, month, day);

            try
            {
                string cmdText = string.Format("SELECT * FROM `wy_huangli`WHERE DATE_FORMAT(showtime, '%Y/%c/%e' ) = DATE_FORMAT( '{0}', '%Y/%c/%e' )",
                    solarDate);
                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
                cmdSql.CommandType = CommandType.Text;

                MySqlDataReader sqlReader = cmdSql.ExecuteReader();
                jsn_lunar = "{[";
                string s = "";
                for (int i = 0; sqlReader.Read(); i++)
                {
                    s = sqlReader.GetString("lunerdate");
                    jsn_lunar += s + ",";
                }

                jsn_lunar = jsn_lunar.TrimEnd(',');
                jsn_lunar += "]}";

                sqlReader.Close();

            }
            catch (MySqlException ex)
            {
                jsn_lunar = null;
            }

            return jsn_lunar;
        }

        public SinaHLDayEx getSinaHlInfo(int year, int month, int day)
        {
            DateTime solarDate = new DateTime(year, month, day);
            SinaHLDayEx hld = null;

            try
            {
                string cmdText = string.Format("SELECT * FROM `wy_sinahuangli`WHERE DATE_FORMAT(solarDate, '%Y/%c/%e' ) = DATE_FORMAT( '{0}', '%Y/%c/%e' )",
                    solarDate);
                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
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
                
                Writelog(ex.Message);
            }

            return hld;
        }

        static void Writelog(string log_str)
        {
            string logfilename = string.Format("db_log_{0}.log",DateTime.Now.ToLongDateString());
            string data_path = HttpContext.Current.Server.MapPath("~/log/" + logfilename);

            try
            {
                FileStream fs = new FileStream(data_path, FileMode.OpenOrCreate|FileMode.Append);
                if (fs != null)
                {
                    TextWriter tr = new StreamWriter(fs, Encoding.UTF8);

                    tr.WriteLine(DateTime.Now.ToLongTimeString() + ": " + log_str);

                    tr.Close();
                    fs.Close();
                }
            }
            catch(IOException ie)
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

                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
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
                MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
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

                Writelog(ex.Message);
            }

            return hlHour;
        }
    }
}