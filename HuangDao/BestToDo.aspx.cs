using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HuangDao
{
    public partial class BestToDo : System.Web.UI.Page
    {
        string qs_word;
        string qs_month;
        string qs_year;
        char[] trim_chars = { '{', '}', '[', ']' };

        protected void Page_Load(object sender, EventArgs e)
        {
            // 网站访问情况统计
            CNZZ cnzz = new CNZZ();
            cnzz.setup(this.Page);

            // 增加 SEO 信息
            this.Title = SEOHelper.getPageTile("宜忌");
            SEOHelper.initMeta(this.Keywords, this.Description);

            DateTime cm = DateTime.Now;

            qs_word = Request.QueryString["hlw"];
            if (qs_word == null)
            {
                qs_word = "嫁娶";
            }

            qs_month = Request.QueryString["hlm"];
            qs_year = Request.QueryString["hly"];

            HDWebservices hdSvc = new HDWebservices();

            // 设置搜索关键字
            defaultKeyword.Value = qs_word;

            // 显示当前（新历）月份
            if (qs_year != null && qs_month != null)
            {
                try
                {
                    cm = DateTime.Parse(/*qs_year + "-" + */ qs_month);
                }
                catch (FormatException fx)
                {
                    Debug.WriteLine(fx.Message);
                }
            }

            if (cm != null)
            {
                solarMonth.Text = cm.ToString("yyyy年M月");
                ltlPrevMonth.Text = cm.AddMonths(-1).ToString("M月");
                ltlNextMonth.Text = cm.AddMonths(+1).ToString("M月");
            }
            else
            {
                solarMonth.Visible = false;
            }
                // 1. 查询指定时间段内候选日期
                DateTime start_date = DateTime.Now;
                DateTime end_date;

                start_date = start_date.AddDays(1 - start_date.Day);
                end_date = start_date.AddMonths(1);

                string strDates = hdSvc.getHlYiDates(start_date, end_date, qs_word);

                // 2. 将查询结果日期（字符串）转换为日期（整型）数组
                strDates = strDates.Trim(trim_chars);
                string[] arrDate = strDates.Split(',');

                int[] selectedDates = new int[arrDate.Length];

                for (int i = 0; i < selectedDates.Length; i++)
                {
                    DateTime d;
                    if (DateTime.TryParse(arrDate[i], out d))
                    {
                        selectedDates[i] = d.Day;
                    }
                }

                // 生成日历控件
                HtmlGenericControl parentNode = ctl_calendar;
                GenerateCalendar(parentNode, cm, selectedDates);

                this.currentYearMonth.Value = cm.ToString("yyyy/MM/dd");
        }
        
        private void GenerateCalendar(HtmlControl parentNode, DateTime mn, int[] selectedDates)
        {
            DateTime td = DateTime.Now;
            DateTime d = new DateTime(mn.Year, mn.Month, 1);
            int offset_days = (int)d.DayOfWeek; // cast enum type to an integer
            d = d.AddDays(-offset_days+1);

            // 日期（周）表头
            char[] arrWeekHeaders = { '一', '二', '三', '四', '五', '六', '日' };
            Panel divWeekHeader = new Panel();
            divWeekHeader.CssClass = "calendar_week";
            parentNode.Controls.Add(divWeekHeader);

            for (int i = 0; i < 7; i++)
            {
                LinkButton lkbtn = new LinkButton();
                lkbtn.Text = arrWeekHeaders[i].ToString();
                lkbtn.CssClass = "calendar_header";
                divWeekHeader.Controls.Add(lkbtn);
            }

            int[,] Month = new int[5, 7];
            for (int i = 0; i < 5; i++)
            {
                Panel divWeek = new Panel();
                divWeek.CssClass = "calendar_week";
                parentNode.Controls.Add(divWeek);

                for (int j = 0; j < 7; j++)
                {
                    LinkButton lkbtn = new LinkButton();
                    lkbtn.CssClass = "calendar_day";


                    // 判断是否为选中的“宜”日
                    bool bFoundSelected = false;
                    for (int m = 0; m < selectedDates.Length; m++)
                    {
                        if (d.Day == selectedDates[m])
                        {
                            bFoundSelected = true;
                            break;
                        }
                    }

                    if (d.Month == mn.Month) // 当月的日期
                    {
                        lkbtn.Text = d.Day.ToString();
                        // lkbtn.PostBackUrl = "huangli.aspx?hld=" + d.ToString("yyyy-MM-dd");

                        // 判断是否为今天
                        if (d.Day == td.Day && d.Month == td.Month)
                        {
                            // lkbtn.CssClass += " today";
                        }
                        
                        if (bFoundSelected) // 只标记当月的日期
                        {
                            // lkbtn.CssClass += " selected_day";
                        }
                    }
                    else
                    {
                        lkbtn.Text = d.Day.ToString();
                        // lkbtn.CssClass += " invalid_day";
                    }

                    d = d.AddDays(1); // Forward to next day of this month

                    divWeek.Controls.Add(lkbtn);
                }
            }

        }
    }
}