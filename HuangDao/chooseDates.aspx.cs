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
        protected void Page_Load(object sender, EventArgs e)
        {
            string qs_word = Request.QueryString["hlw"];
            string qs_month = Request.QueryString["hlm"];
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
                            lunarMonth.Text = hdSvc.getLunarDate(cm.Year, cm.Month, cm.Day);
                            lunarMonth.Text = lunarMonth.Text.Trim(trim_chars);
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


           
                // 开始： 显示候选日期
                DateTime start_date = DateTime.Now;
                DateTime end_date;

                start_date = start_date.AddDays(1 - start_date.Day);
                end_date = start_date.AddMonths(1);

                                
                string strDates = hdSvc.getHlYiDates(start_date, end_date, qs_word);

                strDates = strDates.Trim(trim_chars);
                string[] arrDate = strDates.Split(',');

                // hl_dates.Style.Add("display", "none");
                /*
                foreach (string d in arrDate)
                {
                    string st = "";

                    try
                    {
                        DateTime dt = DateTime.Parse(d);
                        st = dt.ToLongDateString();
                    }
                    catch (FormatException fx)
                    {
                        st = d;
                    }

                    if (d != null)
                    {
                        Label lb = new Label();
                        lb.Text = st;

                        hl_dates.Controls.Add(lb);
                    }

                }
                // 结束：显示候选日期
*/

                // 生成日历
                try
                {
                    HtmlGenericControl parentNode = ctl_calendar;
                    DateTime mn = DateTime.Parse(qs_month);

                    int[] selectedDates = new int[arrDate.Length];
                    for(int i=0; i < selectedDates.Length; i++)
                    {
                        DateTime d = DateTime.Parse(arrDate[i]);
                        selectedDates[i] = d.Day;
                    }


                    GenerateCalendar(parentNode, mn, selectedDates);
                }
                catch (FormatException fx)
                {
                    // do nothing.
                }
            }
            else
            {
                Response.Redirect("/default.aspx");
            }

        }

        private void GenerateCalendar(HtmlControl parentNode, DateTime mn, int[] selectedDates)
        {
            DateTime d = new DateTime(mn.Year, mn.Month, 1);
            int start_day = (int)d.DayOfWeek; // cast enum type to an integer

            // 日期（周）表头
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

            int[,] Month = new int[5, 7];
            for (int i = 0; i < 5; i++)
            {
                Panel divWeek = new Panel();
                divWeek.CssClass = "calendar_week";
                parentNode.Controls.Add(divWeek);
 
                for(int j = 0; j < 7; j++)
                {
                    LinkButton lkbtn = new LinkButton();

                    if ((--start_day) <= 0 && (d.Month == mn.Month))
                    { 
                        lkbtn.Text = d.Day.ToString() ;
                        
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