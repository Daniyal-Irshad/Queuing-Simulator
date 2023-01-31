using OR_Project.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace OR_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Details(PerformanceMeasure collection)
        {

                if (collection.IATDis == "M" && collection.STDis == "M")
                {
                    if (collection.NoOfServers == 1)
                    {
                        //MM1
                        float[] data = MM1_Formulas.MM1(collection.MIAT, collection.MST,collection.NoOfServers);
                        ViewBag.Head = "MM" + collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                    else
                    {
                        //MM2
                        float[] data = MMC_Formulas.MMC(collection.MIAT, collection.MST, collection.NoOfServers);
                        ViewBag.Head = "MM" + collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                }
                else if (collection.IATDis == "M" && collection.STDis == "G")
                {
                    if (collection.NoOfServers == 1)
                    {
                        //MG1
                        float[] data = MG1_Formulas.MG1(collection.MIAT, collection.MST,collection.Variance_S, collection.NoOfServers);
                        ViewBag.Head = "MG"+collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                    else
                    {
                        //MG2
                        float[] data = GGC_Formulas.GGC(collection.MIAT, collection.MST, collection.Variance_S, collection.Variance_IA, collection.NoOfServers, collection.IATDis);
                        ViewBag.Head = "MG" + collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                }
                else
                {
                    if (collection.NoOfServers == 1)
                    {
                        //GG1
                        float[] data = GG1_Formulas.GG1(collection.MIAT, collection.MST, collection.Variance_S,collection.Variance_IA, collection.NoOfServers);
                        ViewBag.Head = "GG" + collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                    else
                    {
                        //GG2
                        float[] data = GGC_Formulas.GGC(collection.MIAT, collection.MST, collection.Variance_S, collection.Variance_IA, collection.NoOfServers, collection.IATDis);
                        ViewBag.Head = "GG" + collection.NoOfServers;
                        ViewBag.Data = data;
                        return View();
                    }
                }
            

            return View();
        }

    }
}