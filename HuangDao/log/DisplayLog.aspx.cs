using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuangDao.log
{
    public partial class DisplayLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }

            DateTime dt;

            if (this.fieldCurrentDateTime.Value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParse(fieldCurrentDateTime.Value, out dt))
                {
                    dt = DateTime.Now;
                }
            }

            loadLogFile(dt);
            
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.fieldCurrentDateTime.Value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParse(fieldCurrentDateTime.Value, out dt))
                {
                    dt = DateTime.Now;
                }
            }

            dt = dt.AddDays(-1);
            this.fieldCurrentDateTime.Value = dt.ToLongDateString();

            loadLogFile(dt);
            
        }

        private void loadLogFile(DateTime dt)
        {
            string filename = string.Format("db_log_{0}.log", dt.ToLongDateString());

            this.spanCurrentFileName.Text = filename;
            this.fieldCurrentDateTime.Value = dt.ToLongDateString();

            string data_path = HttpContext.Current.Server.MapPath("~/log/" + filename);

            try
            {
                FileStream fs = new FileStream(data_path, FileMode.Open);
                TextReader tr = new StreamReader(fs, Encoding.UTF8);
                string logstr = tr.ReadToEnd();
                txbLogFileContent.Text = logstr;

                tr.Close();
                fs.Close();
            }
            catch (IOException ie)
            {
                this.txbLogFileContent.Text = "Can not read file : " + data_path + "\r\n" + ie.Message;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.fieldCurrentDateTime.Value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParse(fieldCurrentDateTime.Value, out dt))
                {
                    dt = DateTime.Now;
                }
            }

            dt = dt.AddDays(1);
            this.fieldCurrentDateTime.Value = dt.ToLongDateString();

            string filename = string.Format("db_log_{0}.log", dt.ToLongDateString());

            this.spanCurrentFileName.Text = filename;
            this.fieldCurrentDateTime.Value = dt.ToLongDateString();

            loadLogFile(dt);            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.fieldCurrentDateTime.Value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParse(fieldCurrentDateTime.Value, out dt))
                {
                    dt = DateTime.Now;
                }
            }
            loadLogFile(dt);  
        }
    }
}