using System.Configuration;
using System.Web.Mvc;

namespace AwesomeWebApp.Controllers
{
    public class CdnDemoController : Controller
    {
        // GET: CdnDemo
        public ActionResult Index()
        {
            ViewBag.CDNBase = ConfigurationManager.AppSettings["WEBAPP_CDNBASE_URL"];
            return View();
        }
    }
}