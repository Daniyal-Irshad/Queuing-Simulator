using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class ChiSquareModel
    {
        public int numOfBins { get; set; }
        public int[] interArrivalArray { get; set; }
        public int[] serviceTimeArray { get; set; }

    }
}