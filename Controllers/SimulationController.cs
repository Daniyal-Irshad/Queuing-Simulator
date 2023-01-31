using OR_Project.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace OR_Project.Controllers
{
    public class SimulationController : Controller
    {
        // GET: Simulation
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(QueueSimulation collection)
        {
            float MeanInterArrival = collection.MIAT;
            float MeanServiceTime = collection.MST;
            int NoOfCustomers = collection.NoOfCustomers;
            int[] interArrivals = new int[NoOfCustomers];
            int[] serviceTimes = new int[NoOfCustomers];
            for (var i = 0; i < NoOfCustomers; i++)
            {
                Thread.Sleep(200);
                int interArrivalValue = SimulationFormulas.GenerateRandomExponential(MeanInterArrival);
                int serviceValue = SimulationFormulas.GenerateRandomExponential(MeanServiceTime);
                while (serviceValue == 0)
                {
                    serviceValue = SimulationFormulas.GenerateRandomExponential(MeanServiceTime);
                }
                while (interArrivalValue < 0)
                {
                    interArrivalValue = SimulationFormulas.GenerateRandomExponential(MeanInterArrival);
                }
                serviceTimes[i] = serviceValue;
                interArrivals[i] = interArrivalValue;
            }
            interArrivals[0] = 0;
            dynamic[] SimulationData = SimulationFormulas.GenerateSimulation(interArrivals, serviceTimes, collection.NoOfServers);
            ViewBag.Data = SimulationData;
            return Json(SimulationData,JsonRequestBehavior.AllowGet);
        }
    }
}