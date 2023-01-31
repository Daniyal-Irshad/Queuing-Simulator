using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class MMC_Formulas
    {
        public static float[] MMC(float lamda, float meu, int c)
        {
            lamda = 1 / lamda;
            meu = 1 / meu;
            float p = lamda / (meu * c);
            float po = Calc_Po(p, c);
            float lq_mmc = LQ_MMC(p, lamda, meu, po, c);
            float wq_mmc = BasicFormulas.Calc_WQ(lq_mmc, lamda);
            float w = BasicFormulas.Calc_W(wq_mmc, meu);
            float l = BasicFormulas.Calc_L(w, lamda);
            float idle = BasicFormulas.Calc_Idle(p);
            float[] arr = new float[5];
            arr[0] = lq_mmc;
            arr[1] = wq_mmc;
            arr[2] = w;
            arr[3] = l;
            arr[4] = idle;
            return arr;
        }
        public static float LQ_MMC(float rho, float lamda, float meu, float po, int c)
        {
            float lq = (po * (float)(Math.Pow(lamda / meu,c) * rho) / (factorial(c) * (float)Math.Pow(1 - rho,2)));
            return lq;
        }
        public static int factorial(int num)
        {
            int res = num;
            if (num == 0 || num == 1)
            {
                return 1;
            }
            else
            {
                while (num > 1)
                {
                    num--;
                    res *= num;
                }
                return res;
            }
        }
        public static float Calc_Po(float rho,int c)
        {
            float summation_value = 0;
            for(int i = 0; i < c; i++)
            {
                summation_value += (((float)(Math.Pow(rho * c, i))) / (factorial(i)));
            }
            float po = 1/(summation_value + (((float)(Math.Pow(rho * c, c))) / (factorial(c)*(1-rho))));
            return po;
        }
    }
}