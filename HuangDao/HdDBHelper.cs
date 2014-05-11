using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using AstroSpider;
using System.Data;
using System.Diagnostics;

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

        ~HdDBHelper()
        {
            if (--ref_count == 0)
            {
                closeDb(); // Release the MySQL connection resource
            }
        }

        string connStringBuilder(string host, int port, string dbname, string username, string password, string charset)
        {
            string cs = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};CharacterSet={5}",
                host, port, dbname, username, password, charset);
            return cs;
        }
        private bool initDb()
        {
            bool result = true;

            if (m_connSql == null) // Open DB connection when it is null
            {
                try
                {
                    m_connString = connStringBuilder(db_host, db_port, db_name, db_user, db_pass, db_charset);
                    m_connSql = new MySqlConnection(m_connString);

                    m_connSql.Open();

                    Debug.WriteLine(">> {0} : Create MySQL DB connection successfully.", DateTime.Now.ToShortTimeString());
                }
                catch (MySqlException e)
                {
                    result = false;
                    
                    m_connSql = null;
                }
            }

            if (m_connSql.State == ConnectionState.Closed)
            {
                m_connSql.Open();
            }

            return result;
        }
        private void closeDb()
        {
            if (m_connSql != null)
            {
                m_connSql.Close();
                m_connSql = null;

                Debug.WriteLine(">> {0} : Closed MySQL DB connection successfully.", DateTime.Now.ToShortTimeString());
            }
        }
        bool saveToDb(TXHuangDaoDay hdd)
        {
            bool result = false;
            if (!initDb())
            {
                return false;
            }

            string cmdText = string.Format("INSERT INTO wy_huangli(fid, showtime, lunerdate, goodtodo, badtodo) VALUES(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\')",
                             hdd.FID, hdd.ShowTime, hdd.LunerDate, hdd.GoodToDo, hdd.BadToDo);

            MySqlCommand cmdSql = new MySqlCommand(cmdText, m_connSql);
            cmdSql.CommandType = CommandType.Text;


            if (cmdSql.ExecuteNonQuery() == 1)
            {
                result = true;
            }

            return result;
        }

        public TXHuangDaoDay selectHlData(string where_clause)
        {
            if (!initDb())
            {
                return null;
            }

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
            if (!initDb())
            {
                return null;
            }

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
            if (!initDb())
            {
                return null;
            }
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
            if (!initDb())
            {
                return null;
            }

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
                Debug.Write(ex.Message);
            }

            return hld;
        }
    }
}