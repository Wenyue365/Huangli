using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HuangDao
{
    public partial class chooseDates : System.Web.UI.Page
    {
        string qs_word;
        string qs_month;

        protected void Page_Load(object sender, EventArgs e)
        {
            qs_word = Request.QueryString["hlw"];
            qs_month = Request.QueryString["hlm"];
#if DEBUG
            //hl_word = "嫁娶";
#endif
            char[] trim_chars = { '{', '}', '[', ']' };

            if (qs_word != null)
            {
                HDWebservices hdSvc = new HDWebservices();

                // 显示宜忌名称
                lbWord.Text = qs_word;

                // 显示当前（新历）月份
                if (qs_month != null)
                {
                    try
                    {
                        DateTime cm = DateTime.Parse(qs_month);
                        if (cm != null)
                        {
                            solarMonth.Text = cm.ToString("yyyy年M月");
                        }
                    }
                    catch (FormatException fx)
                    {
                        solarMonth.Visible = false;
                    }

                    // 显示当前（农历）月份
                    try
                    {
                        DateTime cm = DateTime.Parse(qs_month);
                        if (cm != null)
                        {
                            string strText = hdSvc.getLunarDate(cm.Year, cm.Month, cm.Day);
                            if (strText.Length > 0)
                            {
                                string[] arrText = strText.Split(' ');
                                if (arrText.Length > 2)
                                {
                                    lunarMonth.Text = arrText[0].Trim(trim_chars);
                                    AncientDate.Text = arrText[2].Trim(trim_chars);
                                }
                                else
                                {
                                    lunarMonth.Text = strText.Trim(trim_chars);
                                    AncientDate.Visible = false; // set this element invisible, but it still occupy its place 
                                }
                            }
                        }
                    }
                    catch (FormatException fx)
                    {
                        lunarMonth.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("/default.aspx");
                }


                // 显示宜忌的解释
                lbDetail.Text = hdSvc.getWordAbstract(qs_word);
                if (lbDetail.Text == null || lbDetail.Text == "")
                {
                    lbDetail.Visible = false;
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

                // 生成日历
                DateTime mn;

                if (!DateTime.TryParse(qs_month, out mn))
                {
                    mn = DateTime.Now;
                }

                HtmlGenericControl parentNode = ctl_calendar;
                GenerateCalendar(parentNode, mn, selectedDates);
            }
            else
            {
                Response.Redirect("/huangli.aspx");
            }
        }
        
        /// <summary>
        /// 生成日历控件
        /// </summary>
        /// <param name="parentNode">日历控件的父节点</param>
        /// <param name="mn">日历控件显示的日期（年月）</param>
        /// <param name="selectedDates">需要设置为“选中”状态的日期</param>
        private void GenerateCalendar(HtmlControl parentNode, DateTime mn, int[] selectedDates)
        {
            DateTime d = new DateTime(mn.Year, mn.Month, 1);
            int start_day = (int)d.DayOfWeek; // cast enum type to an integer

            // 1. 日期（周）表头
            char[] arrWeekHeaders = {'一', '二', '三', '四', '五', '六', '日' };
            Panel divWeekHeader = new Panel();
            divWeekHeader.CssClass = "calendar_week";
            parentNode.Controls.Add(divWeekHeader);

            for(int i = 0; i < 7; i++)
            {
                LinkButton lkbtn = new LinkButton();
                lkbtn.Text = arrWeekHeaders[i].ToString() ;
                lkbtn.CssClass = "calendar_header";
                divWeekHeader.Controls.Add(lkbtn);
            }

            // 2. 整月的日期单元
            int[,] Month = new int[5, 7]; // N.B. 二维数组
            for (int i = 0; i < 5; i++) // 每一周显示为一行，一共 5 行
            {
                Panel divWeek = new Panel();
                divWeek.CssClass = "calendar_week";
                parentNode.Controls.Add(divWeek);
 
                for(int j = 0; j < 7; j++) // 每一周 7 天
                {
                    LinkButton lkbtn = new LinkButton();

                    if ((--start_day) <= 0 && (d.Month == mn.Month))
                    { 
                        lkbtn.Text = d.Day.ToString() ;
                        lkbtn.PostBackUrl = "huangli.aspx?hld=" + d.ToString("yyyy-MM-dd");

                        // 设置候选日期（设置样式的 class 属性）
                        bool bFoundSelected = false;
                        for (int m = 0; m < selectedDates.Length; m++)
                        {
                            if (d.Day == selectedDates[m])
                            {
                                bFoundSelected = true;
                                break;
                            }
                        }
                        
                        if (bFoundSelected)
                        {
                            lkbtn.CssClass = "calendar_day selected_day";
                        }
                        else
                        {
                            lkbtn.CssClass = "calendar_day";
                        }
                        
                        d = d.AddDays(1); // Forward to next day of this month
                    }
                    else // 当月该单元格无相应的日期
                    {
                        lkbtn.Text = "*";
                        lkbtn.CssClass = "calendar_day invalid_day";
                    }

                    divWeek.Controls.Add(lkbtn);
                }
            }
                
        }

    }
}