using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;

namespace OR_Project.DataContext
{
    public class SimulationFormulas
    {

        public struct myObj
        {
            public int interArrival;
            public int serviceTime;
            public int arrival;
            public int startTime;
            public int endTime;
            public int waitTime;
            public int turnaroundTime;
            public int server;
        }
        public static int GenerateRandomExponential(float u)
        {
            Random random = new Random();
            return (int)((u*-1) * Math.Log(random.NextDouble()));
        }
        public static double CommulativeFrequency(float lamda, int x)
        {
            double e = 2.718281828;
            double ans = (double)((Math.Pow(e, (lamda * -1)) * (Math.Pow(lamda, x))) / GGC_Formulas.factorial(x));
            return ans;
        }
        public static int[] InterArrivalCol(double[] arr)
        {
            int[] interArrivalArr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 0)
                {
                    interArrivalArr[i] = 0;
                }
                else
                {
                    Random rand = new Random();
                    var b = rand.NextDouble();
                    //interArrivalArr[i] = arr.FindIndex();
                    var a = arr.ToList().FindIndex(x => b - x > 0);
                    interArrivalArr[i] = a;

                }
            }
            return interArrivalArr;
        }
        public static int[] CommulativeFrequencyGenerate(float lamda)
        {
            List<double> comArr = new List<double>();
            int i = 0;
            bool check = true;
            while (check)
            {
                if (i == 0)
                {
                    comArr.Add(CommulativeFrequency(lamda,i));
                }
                else
                {
                    comArr.Add(CommulativeFrequency(lamda, i) + (double)comArr[i - 1]);
                }
                if ((double)comArr[i] >= 1)
                {
                    check = false;
                }
                i++;
            }
            var myArr = comArr.ToArray();
            int[] interArrivalArr = new int[myArr.Length];
            Random rand = new Random();
            for(int j = 0; j < myArr.Length; j++)
            {
                interArrivalArr[j] = rand.Next(1, myArr.Length - 1);
            }
            interArrivalArr[0] = 0;
            //int[] array = InterArrivalCol(myArr);
            return interArrivalArr;
        }
        public static dynamic[] GenerateSimulation(int[] interArrivals, int[] serviceTimes, int NoOfServers)
        {
            int[] arrivals = CalculateArrivalsFromInterArrivals(interArrivals);
            int[] servers = new int[NoOfServers];
            servers = Enumerable.Repeat(0,NoOfServers).ToArray();
            dynamic[] customers = new dynamic[interArrivals.Length];
            for(int j=0; j<arrivals.Length;j++)
            {
                int serverNum = 0;
                for(int i = 0; i < servers.Length-1; i++)
                {
                    if(servers[i] > servers[i + 1])
                    {
                        serverNum = i + 1;
                    }
                }
                myObj obj = new myObj();
                obj.interArrival = interArrivals[j];
                obj.serviceTime = serviceTimes[j];
                obj.arrival = arrivals[j];
                obj.startTime = arrivals[j] < servers[serverNum] ? servers[serverNum] : arrivals[j];
                obj.endTime = obj.startTime + serviceTimes[j];
                obj.waitTime = obj.startTime - arrivals[j];
                obj.turnaroundTime = obj.endTime - arrivals[j];
                obj.server = serverNum+1;
                servers[serverNum] += serviceTimes[j];
                customers[j] = obj;
                
            }
            return customers;
        }
        public static int[] CalculateArrivalsFromInterArrivals(int[] interArrivals)
        {
            int[] arrivals = new int[interArrivals.Length];
            for(var i = 0; i < interArrivals.Length; i++)
            {
                if (i == 0)
                {
                    arrivals[i] = interArrivals[i];
                }
                else
                {
                    arrivals[i] = arrivals[i - 1] + interArrivals[i];
                }
            }
            return arrivals;
        }
    }
}