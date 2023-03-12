using Microsoft.Ajax.Utilities;
using OR_Project.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OR_Project.Controllers
{
    public class ChiSquareController : Controller
    {
        // GET: ChiSquare
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ChiSquareModel collection)
        {
            try { 
            int[] interArrivalObservedFreq = ChiSquareFormulas.chiSquareObservedFreqs(collection.interArrivalArray, collection.numOfBins);
            int[] serviceTimeObservedFreq = ChiSquareFormulas.chiSquareObservedFreqs(collection.serviceTimeArray, collection.numOfBins);
            int OBSum1 = interArrivalObservedFreq.Sum();
            int OBSum2 = serviceTimeObservedFreq.Sum();

            dynamic[] interArrivalData = ChiSquareFormulas.MLE(interArrivalObservedFreq);
            float[] interArrivalMLE = interArrivalData[0];
            float interObservedSum = interArrivalData[1];
            float calculatedLambdaForInterArrivalTime = (float)(interObservedSum / OBSum1);
            dynamic[] serviceTimeData = ChiSquareFormulas.MLE(serviceTimeObservedFreq);
            float[] serviceTimeMLE = serviceTimeData[0];
            float serviceObservedSum = serviceTimeData[1];
            float calculatedLambdaForServiceTime = (float)(serviceObservedSum / OBSum2);

            float[] InterArrivalprobabilities = new float[collection.numOfBins];
            float[] ServiceTimeprobabilities = new float[collection.numOfBins];

            for(int i = 0; i<collection.numOfBins; i++)
            {
                InterArrivalprobabilities[i] = ChiSquareFormulas.probabilityDistribution(calculatedLambdaForInterArrivalTime, i);
                ServiceTimeprobabilities[i] = ChiSquareFormulas.probabilityDistribution(calculatedLambdaForServiceTime, i);
            }

            dynamic[] interArrivalData1 = ChiSquareFormulas.getExpectedFrequenciesAndSummation(OBSum1, InterArrivalprobabilities);
            float[] interArrivalExpectedFrequencies = interArrivalData1[0];
            float sumOfInterArrivalExpected = interArrivalData1[1];
            dynamic[] serviceTimeData1 = ChiSquareFormulas.getExpectedFrequenciesAndSummation(OBSum2, ServiceTimeprobabilities);
            float[] serviceTimeExpectedFrequencies = serviceTimeData1[0];
            float sumOserviceTimeExpected = serviceTimeData1[1];

            float interArrivalChiSquare = ChiSquareFormulas.ChiSqaure(interArrivalExpectedFrequencies, interArrivalObservedFreq);
            float serviceTimeChiSquare = ChiSquareFormulas.ChiSqaure(serviceTimeExpectedFrequencies, serviceTimeObservedFreq);

            float[] myDataa = new float[2];
            myDataa[0] = interArrivalChiSquare;
            myDataa[1] = serviceTimeChiSquare;
            TempData["IAChiSquare"] = interArrivalChiSquare;
            TempData["STChiSquare"] = serviceTimeChiSquare;
            


            return Json(myDataa, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex){
                var message = ex.Message;
                return null;
            }
        }
    }
}