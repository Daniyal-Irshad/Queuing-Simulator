using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class BasicFormulas
    {
        public static float Calc_WQ(float lq, float lamda)
        {
            float Wq = lq / lamda;
            return Wq;
        }
        public static float Calc_W(float Wq, float meu)
        {
            float W = Wq + (1 / meu);
            return W;
        }
        public static float Calc_L(float W, float lamda)
        {
            float L = lamda * W;
            return L;
        }
        public static float Calc_Idle(float rho)
        {
            float Idle = 1 - rho;
            return Idle;
        }

    }
}