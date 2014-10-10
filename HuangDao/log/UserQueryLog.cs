using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

namespace HuangDao.log
{

    public class UserQueryLog
    {
        public class QueryInfo
        {
            public UserInfo ui;
            public string url;
            public string queryString;
            public DateTime queryTime;

        }

        // 线程工作函数：保存用户访问信息到数据库
        // This thread procedure performs the task.
        static void thprocSaveIPLocation(Object stateInfo)
        {
            QueryInfo qi = (QueryInfo)stateInfo;

            string cmdText = string.Format("INSERT INTO 'wy_userquerylog'('userId', 'userIp', 'url', 'queryString', 'queryTime') VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                           qi.ui.id, qi.ui.ip, qi.url, qi.queryString, qi.queryTime);

            string connString = null;
            MySqlConnection connSql = null;

            try
            {
                // 打开数据库连接
                connString = HdDBHelper.getConnectionStr();
                connSql = new MySqlConnection(connString);
                connSql.Open();
                HdDBHelper.Writelog("thprocSaveIPLocation : Open MySQL DB connection successfully.");

                MySqlCommand cmdSql = new MySqlCommand(cmdText, connSql);
                cmdSql.CommandType = CommandType.Text;
                cmdSql.CommandTimeout = 200;

                if (cmdSql.ExecuteNonQuery() != 1)
                {
                    HdDBHelper.Writelog("Error : thprocSaveIPLocation - cmdSql.ExecuteNonQuery()");
                }

                connSql.Close();
            }
            catch (MySqlException e)
            {
                HdDBHelper.Writelog("thprocSaveIPLocation： MySqlException : " + e.Message);
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
            ui.id = id;
            ui.name = username;
            ui.ip = ip;
            ui.loc = loc;

            QueryInfo qi = new QueryInfo();
            qi.ui = ui;
            qi.url = "";
            qi.queryString = "";
            qi.queryTime = DateTime.Now;

            // 使用 ThreadPool 完成异步保存操作
            if (ThreadPool.QueueUserWorkItem(new WaitCallback(thprocSaveIPLocation), qi))
            {
                result = true;
            }
            return result;
        }
    }
}