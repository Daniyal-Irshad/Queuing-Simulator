using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class PerformanceMeasure
    {
        [Required]
        public int NoOfServers { get; set; }
        [Required]
        public string IATDis { get; set; }
        [Required]
        public float MIAT { get; set; }
        public string STDis { get; set; }
        [Required]
        public float MST { get; set; }
        [Required]
        public float Min_S { get; set; }
        [Required]
        public float Min_I { get; set; }
        [Required]
        public float Max_S { get; set; }
        [Required]
        public float Max_I { get; set; }
        public string BtnName { get; set; }
    }
}