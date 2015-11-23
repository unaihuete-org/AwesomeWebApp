using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;

namespace AwesomeWebApp.Controllers
{
    public class StagingSlotsController : Controller
    {
        // GET: StagingSlots
        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="crashRate"></param>
        /// <returns></returns>
        public ActionResult CrashApp(float crashRate = 0)
        {
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CRASH_RATE"]) && crashRate == 0.0)
            {
                float.TryParse(WebConfigurationManager.AppSettings["CRASH_RATE"].ToString(), out crashRate);
            }

            double realCrashRate = 1 - crashRate;

            Random rnd = new Random();
            double d = rnd.NextDouble();

            if (d > realCrashRate)
            {
                int x = 0;
                int y = 1;
                int z = y / x; // generate div by zero exception that will not get cought 
            }

            SetViewBag(); 
            ViewBag.realCrashRate = d; //override default crush rate
            return View("Index");
        }

        /// <summary>
        /// extract app setting and connection string for display
        /// </summary>
        private void SetViewBag()
        {
            try
            {

                //get crash rate app setting (sticky per slot)
                float crashRate = 0;
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CRASH_RATE"]))
                {
                    float.TryParse(WebConfigurationManager.AppSettings["CRASH_RATE"].ToString(), out crashRate);
                    ViewBag.crashRate = crashRate;
                }

                // get connection string for DB1 (sticky per slot)
                if (WebConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"] != null)
                {
                    ViewBag.databaseConnectionString = WebConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString;
                }

                // get none sticky setting
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["NONE_STICK_SETTING"]))
                {
                    ViewBag.noneStickSetting = WebConfigurationManager.AppSettings["NONE_STICK_SETTING"].ToString();
                }

                // set default crash rate for display
                ViewBag.realCrashRate = 0.0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}