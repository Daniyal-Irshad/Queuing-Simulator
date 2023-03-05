using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class MG1_Formulas
    {
        public static float[] MG1(float lamda, float min_s,float max_s, int c)
        {
            lamda = 1 / lamda;
            float meu = 1 / ((min_s+max_s)/2);
            float variance_s = BasicFormulas.Calc_Variance(min_s, max_s);
            float p = lamda / (meu * c);
            float lq_mg1 = LQ_MG1(p,lamda,variance_s);
            float wq_mg1 = BasicFormulas.Calc_WQ(lq_mg1, lamda);
            float w = BasicFormulas.Calc_W(wq_mg1, meu);
            float l = BasicFormulas.Calc_L(w, lamda);
            float idle = BasicFormulas.Calc_Idle(p);
            float[] arr = new float[5];
            arr[0] = lq_mg1;
            arr[1] = wq_mg1;
            arr[2] = w;
            arr[3] = l;
            arr[4] = idle;
            return arr;
        }
        public static float LQ_MG1(float rho,float lamda,float variance_s)
        {
            float lq =  (((float)Math.Pow(lamda,2)*variance_s) + (float)Math.Pow(rho,2)/(2*(1-rho)));
            return lq;
        }
    }
}