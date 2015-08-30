using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighTrustAppWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ICachingProvider _cache = new CachingProvider();
        [SharePointContextFilter]
        public ActionResult Index()
        {
            

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    ViewBag.SiteName = _cache.GetFromCache<string>("SiteTitle", () =>
                    {
                        return GetSiteTitle(clientContext);
                    });
                }
            }

            return View();
        }

        private string GetSiteTitle(ClientContext clientContext)
        {
            Web spWeb = clientContext.Web;
            clientContext.Load(spWeb, web => web.Title);
            clientContext.ExecuteQuery();
            return spWeb.Title;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
