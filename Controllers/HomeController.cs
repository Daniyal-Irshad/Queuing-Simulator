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
                if (collection.NoOfServers == 1 && collection.MIAT != 0 && collection.MST != 0)
                {
                    //MM1
                    float[] data = MM1_Formulas.MM1(collection.MIAT, collection.MST, collection.NoOfServers);
                    ViewBag.Head = "MM" + collection.NoOfServers;
                    ViewBag.Data = data;
                    return View();
                }
                else if ((collection.NoOfServers == 2 || collection.NoOfServers == 3 || collection.NoOfServers == 4) && collection.MIAT != 0 && collection.MST != 0)
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
                if (collection.NoOfServers == 1 && collection.MIAT != 0 && collection.Min_S != 0 && collection.Max_S != 0)
                {
                    //MG1
                    float[] data = MG1_Formulas.MG1(collection.MIAT,collection.Min_S, collection.Max_S, collection.NoOfServers);
                    ViewBag.Head = "MG"+collection.NoOfServers;
                    ViewBag.Data = data;
                    return View();
                }
                else if ((collection.NoOfServers == 2 || collection.NoOfServers == 3 || collection.NoOfServers == 4) && collection.MIAT != 0 && collection.Min_S != 0 && collection.Max_S != 0)
                {
                    //MG2
                    float[] data = GGC_Formulas.GGC(collection.MIAT, collection.Min_S, collection.Max_S, collection.Min_I, collection.Max_I, collection.NoOfServers, collection.IATDis);
                    ViewBag.Head = "MG" + collection.NoOfServers;
                    ViewBag.Data = data;
                    return View();
                }
            }
            else if (collection.IATDis == "G" && collection.STDis == "G")
            {
                if (collection.NoOfServers == 1 && collection.Min_S != 0 && collection.Max_S != 0 && collection.Min_I != 0 && collection.Max_I != 0)
                {
                    //GG1
                    float[] data = GG1_Formulas.GG1(collection.Min_S, collection.Max_S, collection.Min_I, collection.Max_I, collection.NoOfServers);
                    ViewBag.Head = "GG" + collection.NoOfServers;
                    ViewBag.Data = data;
                    return View();
                }
                else if ((collection.NoOfServers == 2 || collection.NoOfServers == 3 || collection.NoOfServers == 4) && collection.Min_S != 0 && collection.Max_S != 0 && collection.Min_I != 0 && collection.Max_I != 0)
                {
                    //GG2
                    float[] data = GGC_Formulas.GGC(collection.MIAT, collection.Min_S, collection.Max_S, collection.Min_I, collection.Max_I, collection.NoOfServers, collection.IATDis);
                    ViewBag.Head = "GG" + collection.NoOfServers;
                    ViewBag.Data = data;
                    return View();
                }
            }

            TempData["Failure"] = "*Fill all the fields !";
            return RedirectToAction("Index");
        }

    }
}