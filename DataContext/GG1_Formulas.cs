using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class GG1_Formulas
    {
        public static float[] GG1(float min_s, float max_s, float min_i, float max_i, int c)
        {
            float lamda = 1 / ((min_i + max_i) / 2);
            float meu = 1 / ((min_s + max_s) / 2);
            float variance_s = BasicFormulas.Calc_Variance(min_s, max_s);
            float variance_a = BasicFormulas.Calc_Variance(min_i, max_i);
            float p = lamda / (meu * c);
            float lq_gg1 = LQ_GG1(p, lamda,meu,variance_s,variance_a);
            float wq_gg1 = BasicFormulas.Calc_WQ(lq_gg1, lamda);
            float w = BasicFormulas.Calc_W(wq_gg1, meu);
            float l = BasicFormulas.Calc_L(w, lamda);
            float idle = BasicFormulas.Calc_Idle(p);
            float[] arr = new float[5];
            arr[0] = lq_gg1;
            arr[1] = wq_gg1;
            arr[2] = w;
            arr[3] = l;
            arr[4] = idle;
            return arr;
        }
        public static float LQ_GG1(float rho, float lamda,  float meu, float variance_s,float variance_a)
        {
            float Ca = variance_a / (float)Math.Pow(1 / lamda, 2);
            float Cs = variance_s / (float)Math.Pow(1 / meu, 2);
            float rho_sq = (float)Math.Pow(rho, 2);
            float lq = ((rho_sq) * (1 + Cs) * (Ca + (rho_sq * Cs))) / (2 * (1 - rho) * (1 + (rho_sq * Cs)));
            return lq;
        }
    }
}