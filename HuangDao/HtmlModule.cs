using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuangDao.Modules
{
    /// <summary>
    /// 自定义的 HTTP 请求处理模块，用于处理关于 HTML 类型资源的请求
    /// </summary>
    public class HtmlModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Application_BeginRequest);
            context.EndRequest += new EventHandler(Application_EndRequest);
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;
            response.Write("这是来自自定义HttpModule中有BeginRequest");
        }

        public void Application_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;
            response.Write("这是来自自定义HttpModule中有EndRequest");
        }
        
    }

    public class HtmlHandler : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context,
               string requestType, String url, String pathTranslated)
        {
            IHttpHandler handlerToReturn;
            if ("get" == context.Request.RequestType.ToLower())
            {
                handlerToReturn = new HelloWorldHandler();
            }
            else if ("post" == context.Request.RequestType.ToLower())
            {
                handlerToReturn = new HelloWorldAsyncHandler();
            }
            else
            {
                handlerToReturn = null;
            }
            return handlerToReturn;
        }
        public void ReleaseHandler(IHttpHandler handler)
        {
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    /* 在 Web.config 文件中配置 httpHandlers 节点，例：
    <httpHandlers>
        <add verb="*" path="*.aspx" type="jwtLib.JwtHandler2.jwtLi"/>
    </httpHandlers>
    */

    class HelloWorldHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }

    class HelloWorldAsyncHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}