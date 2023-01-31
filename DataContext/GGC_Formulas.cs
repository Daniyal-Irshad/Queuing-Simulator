using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OR_Project.DataContext
{
    public class GGC_Formulas
    {
        public static float[] GGC(float lamda, float meu, float variance_s, float variance_a, int c, string IAType)
        {
            lamda = 1 / lamda;
            meu = 1 / meu;
            float p = lamda / (meu * c);
            float[] data = LQ_WQ_GGC(p, lamda, meu, c, IAType, variance_s, variance_a);
            float w = BasicFormulas.Calc_W(data[1], meu);
            float l = BasicFormulas.Calc_L(w, lamda);
            float idle = BasicFormulas.Calc_Idle(p);
            float[] arr = new float[5];
            arr[0] = data[0]; //lq
            arr[1] = data[1]; //wq
            arr[2] = w;
            arr[3] = l;
            arr[4] = idle;
            return arr;
        }
        public static float[] LQ_WQ_GGC(float rho, float lamda, float meu, int c, string IAType, float variance_s, float variance_a)
        {
            float po = Calc_Po(rho, c);
            float mmc_lq = MMC_Formulas.LQ_MMC(rho, lamda, meu, po, c);
            float mmc_wq = BasicFormulas.Calc_WQ(mmc_lq, lamda);
            float[] arr = new float[2];

            if (IAType == "M")
            {
                float Ca = 1;
                float Cs = variance_s / (float)(Math.Pow(1 / meu, 2));
                float wq = (mmc_wq * ((Ca + Cs) / 2));
                float lq = wq * lamda;
                arr[0] = lq;
                arr[1] = wq;
                return arr;
            }
            else
            {
                float Ca = variance_a / (float)(Math.Pow(1 / lamda, 2));
                float Cs = variance_s / (float)(Math.Pow(1 / meu, 2));
                float wq = (mmc_wq * ((Ca + Cs) / 2));
                float lq = wq * lamda;
                arr[0] = lq;
                arr[1] = wq;
                return arr;
            }
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
        public static float Calc_Po(float rho, int c)
        {
            float summation_value = 0;
            for (int i = 0; i < c; i++)
            {
                summation_value += (((float)(Math.Pow(rho * c, i))) / (factorial(i)));
            }
            float po = 1 / (summation_value + (((float)(Math.Pow(rho * c, c))) / (factorial(c) * (1 - rho))));
            return po;
        }
    }
}