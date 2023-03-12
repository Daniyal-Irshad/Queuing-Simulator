using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OR_Project.DataContext
{
    public class ChiSquareFormulas
    {
        public static int[] chiSquareObservedFreqs(int[] myArray, int numOfBins)
        {
            var MinVal = myArray.Min();
            var MaxVal = myArray.Max();
            float binWidth = (float)(MaxVal - MinVal) / numOfBins;
            int[] observedFrequency = new int[numOfBins];
            foreach (int item in myArray)
            {
                int binIndex = (int)Math.Floor((item - MinVal) / binWidth);
                if(binIndex < numOfBins)
                {
                    observedFrequency[binIndex]++;
                }
            }
            return observedFrequency;
        }

        public static dynamic[] MLE(int[] observedFreq)
        {
            float[] MLEs = new float[observedFreq.Length];
            float sum = 0;
            for(int i = 0; i < MLEs.Length; i++)
            {
                MLEs[i] = (float)(observedFreq[i] * i);
                sum += MLEs[i];
            }
            dynamic myData = new dynamic[2];
            myData[0] = MLEs;
            myData[1] = sum;
            return myData;
        }

        public static float probabilityDistribution(float lamda, int x)
        {
            float ans = (float)((Math.Pow(Math.E, (lamda * -1)) * (Math.Pow(lamda, x))) / GGC_Formulas.factorial(x));
            return ans;
        }

        public static dynamic[] getExpectedFrequenciesAndSummation(int OBSum, float[] probabilities)
        {
            float sum = 0;
            float[] expectedFreq = new float[probabilities.Length];
            for(int i=0;i<probabilities.Length; i++)
            {
                expectedFreq[i] = (float)(probabilities[i] * OBSum);
                sum+=expectedFreq[i];
            }
            dynamic myData = new dynamic[2];
            myData[0] = expectedFreq;
            myData[1] = sum;
            return myData;
        }

        public static float ChiSqaure(float[] expectedFreqArray, int[] observedFreqArray)
        {
            float chiSquare = 0;
            for(int i = 0; i < expectedFreqArray.Length; i++)
            {
                chiSquare += (float)(Math.Pow(Math.Abs(observedFreqArray[i] - expectedFreqArray[i]), 2) / expectedFreqArray[i]);
            }
            return chiSquare;
        }

    }
}