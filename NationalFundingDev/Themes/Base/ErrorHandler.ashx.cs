using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalFundingDev.Themes.Base
{
    /// <summary>
    /// Summary description for ErrorHandler
    /// </summary>
    public class ErrorHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string location;
#if DEBUG
            location = @"\\IGSKIACWVMi02\devsiftaroot\Temp\error.txt";
#else
            location = @"\\IGSKIACWVMi01\siftaroot\Temp\error.txt";
#endif

            System.IO.File.WriteAllText(location, context.Request["message"]);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}