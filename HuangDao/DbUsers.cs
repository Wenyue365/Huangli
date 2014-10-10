using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuangDao
{

    public class UserInfo
    {
        public int id; // User ID
        public string name;
        public string ip;
        public string loc;
    }

    public class DbUsers
    {
        string insert_sql_fmt = "INSERT INTO `wy_users` (`user_id`, `email`, `user_name`, `password`, `question`, `answer`, `sex`, `address_id`, `reg_time`, `last_login`, `last_time`, `last_ip`, `visit_count`, `user_rank`, `is_special`, `parent_id`, `flag`, `alias`, `mobile_phone`) VALUES " +
                            "('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";

        public string insert_sql = null;

        public string user_id;
        public string email;
        public string user_name;
        public string password;
        public string question;
        public string answer;
        public string sex;
        public string address_id;
        public string reg_time;
        public string last_login;
        public string last_time;
        public string last_ip;
        public string visit_count;
        public string user_rank;
        public string is_special;
        public string parent_id;
        public string flag;
        public string alias;
        public string mobile_phone;

        string createInsertSql()
        {
            insert_sql = string.Format(insert_sql_fmt, user_id, email, user_name, password, question, answer, sex, address_id, reg_time, last_login, last_time, last_ip, visit_count, user_rank, is_special, parent_id, flag, alias, mobile_phone);

            return insert_sql;
        }

        public bool addUser(UserInfo ui)
        {
            return false;
        }
        
        public bool deleteUser(string userId)
        {
            return false;
        }

        public bool updateUser(UserInfo ui)
        {
            return false;
        }

        public bool isExistedUser(string userName)
        {
            return false;
        }

        public UserInfo getUser(string userId)
        {
            UserInfo ui = new UserInfo();

            return ui;
        }

        public string login(string useName, string password)
        {
            string token = "";

            return token;
        }

        public string  regUser(UserInfo ui)
        {
            string token = "";

            if (!isExistedUser(ui.name))
            {
                addUser(ui);
            }

            return token;
        }


    }
}