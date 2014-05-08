using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace AstroSpider
{
   
    public class ZHtmlParser
    {
        // htmlDcoument 对象用来访问 Html文档s
        HtmlAgilityPack.HtmlDocument m_htmlDoc = new HtmlAgilityPack.HtmlDocument();

        public ZHtmlParser() { }
        public ZHtmlParser(string strHtml)
        {
            load(strHtml);
        }
        public void load(string strHtml)
        {
            // 加载Html文档
            m_htmlDoc.LoadHtml(strHtml);
        }

        static int ierr = 0;

        public string getValue(string xpath)
        {
            string str = null;
            if (xpath != null && xpath != "")
            {
                HtmlNode node = m_htmlDoc.DocumentNode.SelectSingleNode(xpath);
                if (node != null) { 
                    str = node.OuterHtml;
                    str = str.Trim();
                }
                else
                {
                    str = string.Format("error_{0}", ierr++);
                }
            }

            return str;
        }
    }

    public class Entry
    {
        public Entry(ZHtmlParser pr)
        {
            _parser = pr;
        }

        public Entry()
        {

        }

        private ZHtmlParser _parser = null;

        virtual protected string parseValue()
        {
            return _parser.getValue(_xpath);
        }

        public ZHtmlParser Parser
        {
            get { return _parser; }
            set { _parser = value;
                if (_parser != null) // 设置 ZHtmlParser 对象的时候，重新提取 val  
                {
                    val = parseValue();
                }
            }
        }

        public string val;
        private string _xpath;

        public string xpath
        {
            get { return _xpath; }
            set
            {
                _xpath = value;
                if (_parser != null) // 设置 xpath 的时候，重新提取 val  
                {
                    val = parseValue();
                }
            }
        }
    }

}
