using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class PerformanceMeasure
    {
        public int NoOfServers { get; set; }
        public string IATDis { get; set; }
        public float MIAT { get; set; }
        public string STDis { get; set; }
        public float MST { get; set; }
        public float Variance_S { get; set; }
        public float Variance_IA { get; set; }


        public string BtnName { get; set; }
    }
}