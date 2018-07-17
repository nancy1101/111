using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using TEST.Models;

namespace TEST.Controllers {
    public class HomeController : Controller {
      
   
        public IActionResult Index() 
        {
            Contract.Ensures(Contract.Result<IActionResult>() != null);

            //關閉憑證驗證
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                               
            string uri = "https://api-search.sit.kkday.com/v1/search/prod?lang=en&currency=USD";                                       
                       
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            //加上header的key值
            request.Headers.Add("x-auth-key", "kkdaysearchapi_Rfd_fsg+x+TcJy");
           
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();           

            String htmlString;

            using (var reader = new StreamReader(response.GetResponseStream())) 
            {
                htmlString = reader.ReadToEnd();

            }

            //解決中文編碼問題
            //var result = Regex.Unescape(htmlString);

            Object json = JsonConvert.DeserializeObject(htmlString);

            ViewData["ApiJson"] = json;

            return View(); 
        }

       






        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

       

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
