using System;
using System.Web;

namespace HuangDao
{
    
    public class HlModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            // context.LogRequest += new EventHandler(OnLogRequest);
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //HttpApplication httpApp = (HttpApplication)sender;
            //HttpContext context = httpApp.Context;

            //string filePath = context.Request.FilePath;
            //string fileExtension = VirtualPathUtility.GetExtension(filePath);
            //if (fileExtension.Equals("")) // Handle request without file-extension
            //{
            //    string trgUrl = HuangDao.Modules.UrlRouter.getUrl(httpApp.Context.Request.RawUrl);
            //    if (trgUrl != null)
            //    {
            //        httpApp.Context.RewritePath(trgUrl);
            //    }
            //}
            //else if (fileExtension.Equals(".html"))
            //{
            //    string trgUrl = HuangDao.Modules.UrlRouter.getUrl(httpApp.Context.Request.RawUrl);
            //    if (trgUrl != null)
            //    {
            //        httpApp.Context.RewritePath(trgUrl);
            //    }
            //}

            //else if (fileExtension.Equals(".do"))
            //{
            //    string trgUrl = HuangDao.Modules.UrlRouter.getUrl(httpApp.Context.Request.RawUrl);
            //    if (trgUrl != null)
            //    {
            //        httpApp.Context.RewritePath(trgUrl);
            //    }
            //}

        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
    }
}
