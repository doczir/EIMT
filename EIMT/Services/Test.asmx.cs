using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EIMT.Services
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [WebService(Namespace = "http://eimt.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(DateTime d)
        {
            
            return "Hello World" + d;
        }
    }
}
