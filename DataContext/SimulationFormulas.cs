using System;
using System.Collections.Generic;
using System.Linq;
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
            //public int interArrival { get; set; }
            //public int serviceTime { get; set; }
            //public int arrival { get; set; }
            //public int startTime { get; set; }
            //public int endTime { get; set; }
            //public int waitTime { get; set; }
            //public int turnaroundTime { get; set; }
            //public int server { get; set; }
        }
        public static int GenerateRandomExponential(float u)
        {
            Random random = new Random();
            return (int)((u*-1) * Math.Log(random.NextDouble()));
            //return (int)(u * Math.Log(random.Next(10)));
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