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
        /// <param name="crushRate"></param>
        /// <returns></returns>
        public ActionResult CrushApp(float crushRate = 0)
        {
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CRUSH_RATE"]) && crushRate == 0.0)
            {
                float.TryParse(WebConfigurationManager.AppSettings["CRUSH_RATE"].ToString(), out crushRate);
            }

            double realCrushRate = 1 - crushRate;

            Random rnd = new Random();
            double d = rnd.NextDouble();

            if (d > realCrushRate)
            {
                int x = 0;
                int y = 1;
                int z = y / x; // generate div by zero exception that will not get cought 
            }

            SetViewBag(); 
            ViewBag.realCrushRate = d; //override default crush rate
            return View("Index");
        }

        /// <summary>
        /// extract app setting and connection string for display
        /// </summary>
        private void SetViewBag()
        {
            try
            {

                //get crush rate app setting (sticky per slot)
                float crushRate = 0;
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CRUSH_RATE"]))
                {
                    float.TryParse(WebConfigurationManager.AppSettings["CRUSH_RATE"].ToString(), out crushRate);
                    ViewBag.crushRate = crushRate;
                }

                // get connection string for DB1 (sticky per slot)
                if (WebConfigurationManager.ConnectionStrings["DATABASE1"] != null)
                {
                    ViewBag.databaseConnectionString = WebConfigurationManager.ConnectionStrings["DATABASE1"].ConnectionString;
                }

                // get none sticky setting
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["NONE_STICK_SETTING"]))
                {
                    ViewBag.noneStickSetting = WebConfigurationManager.AppSettings["NONE_STICK_SETTING"].ToString();
                }

                // set default crush rate for display
                ViewBag.realCrushRate = 0.0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}