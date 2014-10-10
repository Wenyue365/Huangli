using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace HuangDao.Modules
{
    public class UrlRouter
    {
#if DEBUG
        public static string s_baseUrl = "~/";
#else
        static string s_baseUrl = "~/";
#endif
        private static string getEventName(string req)
        {
            Regex reEvent = new Regex(@"/(\w+)\.[(aspx)|(do)|(html)|(htm)]", RegexOptions.IgnoreCase);
            string str = null;

            Match mch = reEvent.Match(req);
            if (mch.Length > 0)
            {
                str = mch.Groups[1].Value;
            }

            return str;
        }

        private static string getDateString(string req)
        {
            Regex reDate = new Regex(@"/(\d{4}\-\d{1,2}\-\d{1,2})\.[(aspx)|(html)|(htm)|(asp)]", RegexOptions.IgnoreCase);
            string str = null;

            Match mch = reDate.Match(req);
            if (mch.Length > 0)
            {
                str = mch.Groups[1].Value;
            }

            return str;
        }

        private static string getUrlOfEvent(string strEvent)
        {

            string resPath = null;

            switch (strEvent)
            {
                case "宜忌":
                    resPath = "divine.aspx";
                    break;
                case "测算":
                    resPath = "divine.aspx";
                    break;
                case "姻缘":
                    resPath = "love.aspx";
                    break;
                default:
                    resPath = null;
                    break;
            }

            return (resPath == null) ? null : (s_baseUrl + resPath);
        }

        private static string getUrlOfDate(string strDate)
        {
            string baseUrl = "~/huangli.aspx?hld=";

            return (baseUrl + strDate);
        }

        private static string getFolderName(string req)
        {
            string folderName = null;
            char[] sepChars = {'.'};

            int start = req.LastIndexOf('/');
            int end = req.LastIndexOfAny(sepChars);
            end = (end == -1) ? req.Length : end;

            if (end > 0 && end > start)
            {
                folderName = req.Substring(start+1, end - start-1);
            }

            return folderName;
        }

        private static string getDefaultUrlOfFolder(string fldName)
        {
            string resPath = null;

            switch (fldName)
            {
                case "love":
                case "yinyuan":
                case "hunyin":
                case "hunjia":
                case "jiaqu":
                case "jiehun":
                case "姻缘":
                case "婚嫁":
                case "嫁娶":
                case "结婚":
                    resPath = "love.aspx";
                    break;

                case "yiji":
                case "huangdaojiri":
                case "jixiong":
                    resPath = "besttodo.aspx";
                    break;

                case "suanming":
                case "cesuan":
                case "mingli":
                case "haoma":
                case "number":
                case "xingming":
                case "chepai":
                case "mingzi":
                case "号码":
                case "车牌":
                case "手机":
                case "数字":
                case "姓名":
                case "名字":
                case "命理":
                case "算命":
                    resPath = "divine.aspx";
                    break;

                case "nongli":
                case "gongli":
                case "yinli":
                case "yangli":
                case "huangli":
                case "农历":
                case "公历":
                case "阴历":
                case "阳历":
                    resPath = "huangli.aspx";
                    break;
            }

            return (resPath == null) ? null : (s_baseUrl + resPath);
        }

        public static string getUrl(string req)
        {
            string trgUrl = null;
            string strDate = getDateString(req);
            if (strDate != null)
            {
                trgUrl = getUrlOfDate(strDate);
            }
            else
            {
                string fldName = getFolderName(req);
                if (fldName != null)
                {
                    trgUrl = getDefaultUrlOfFolder(fldName);
                }
                else
                {
                    string strEvent = getEventName(req);
                    if (strEvent != null)
                    {
                        trgUrl = getUrlOfEvent(strEvent);
                    }
                }
            }

            return trgUrl;
        }
    }
}