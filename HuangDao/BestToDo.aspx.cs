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
        protected void Page_Load(object sender, EventArgs e)
        {
            char[] trim_chars = { '{', '}', '[', ']' };

            string qs_word = Request.QueryString["hlw"];
            string qs_month = Request.QueryString["hlm"];
            DateTime curr_month = DateTime.Now;

            if (qs_month != null)
            {
                curr_month = DateTime.Parse(qs_month);
            }

            HDWebservices hdSvc = new HDWebservices();

            // 生成日历
            try
            {
                // 取当前月份
                DateTime start_date = curr_month;
                DateTime end_date;

                start_date = start_date.AddDays(1 - start_date.Day); // 当月的第 1 天
                end_date = start_date.AddMonths(1); // 当月的最后 1 天

                string strDates = hdSvc.getHlYiDates(start_date, end_date, qs_word);

                strDates = strDates.Trim(trim_chars);
                string[] arrDate = strDates.Split(',');

                HtmlGenericControl parentNode = ctl_calendar;

                int[] selectedDates = new int[arrDate.Length];
                for (int i = 0; i < selectedDates.Length; i++)
                {
                    DateTime d = DateTime.Parse(arrDate[i]);
                    selectedDates[i] = d.Day;
                }

                GenerateCalendar(parentNode, start_date, selectedDates);
            }
            catch (FormatException fx)
            {
                Debug.Write(fx.Message);
            }

        }

        private void GenerateCalendar(HtmlControl parentNode, DateTime mn, int[] selectedDates)
        {
            DateTime d = new DateTime(mn.Year, mn.Month, 1);
            int start_day = (int)d.DayOfWeek; // cast enum type to an integer

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

                    if ((--start_day) <= 0 && (d.Month == mn.Month))
                    {
                        lkbtn.Text = d.Day.ToString();

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
                    else
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