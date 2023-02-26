using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OR_Project.DataContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;
using static OR_Project.DataContext.SimulationFormulas;

namespace OR_Project.Controllers
{
    public class SimulationController : Controller
    {
        // GET: Simulation
        public ActionResult Index()
        {
            return View();
        }
        public struct myObj2
        {
            public int interArrival;
            public int serviceTime;
            public int arrival;
            public int startTime;
            public int endTime;
            public int waitTime;
            public int turnaroundTime;
            public int server;
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
            //public static dynamic[] TheData = SimulationData;

            float[] data = MMC_Formulas.MMC(collection.MIAT, collection.MST, collection.NoOfServers);
            TempData["HeadEX"] = "MM" + collection.NoOfServers;
            TempData["DataaEX"] = SimulationData;
            dynamic[] myDataa = new dynamic[2];
            myDataa[0] = SimulationData;
            myDataa[1] = data;

            return Json(myDataa,JsonRequestBehavior.AllowGet);
        }



        public ActionResult JSON()
        {
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();

            var data = from item in TempData["DataaEX"] as IEnumerable<dynamic>
                       select item;
            var i = 1;
            foreach (var item in data)
            {
                var cust = "C" + i;
                dataPoints1.Add(new DataPoint(cust, item.serviceTime));
                dataPoints2.Add(new DataPoint(cust, item.turnaroundTime));
                dataPoints3.Add(new DataPoint(cust, item.waitTime));
                i++;
            }

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);

            return View();

        }
    }
}