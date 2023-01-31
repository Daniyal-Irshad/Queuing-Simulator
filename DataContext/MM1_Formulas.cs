using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class MM1_Formulas
    {
        public static float[] MM1(float lamda, float meu,int c)
        {
            lamda = 1 / lamda;
            meu = 1 / meu;
            float p = lamda / (meu*c);
            float lq_mm1 = LQ_MM1(p);
            float wq_mm1 = BasicFormulas.Calc_WQ(lq_mm1, lamda);
            float w = BasicFormulas.Calc_W(wq_mm1, meu);
            float l = BasicFormulas.Calc_L(w, lamda);
            float idle = BasicFormulas.Calc_Idle(p);
            float[] arr = new float[5];
            arr[0] = lq_mm1;
            arr[1] = wq_mm1;
            arr[2] = w;
            arr[3] = l;
            arr[4] = idle;
            return arr;
        }
        public static float LQ_MM1(float rho)
        {
            float lq = (rho * rho) / (1 - rho);
            return lq;
        }
    }
}