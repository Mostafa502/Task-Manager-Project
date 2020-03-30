using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace cocv
{
    class Program
    {
        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;
        static void Main(string[] args)
        {
            try
            {
                PrintProcessList();
                Console.WriteLine(@"Enter P if you want change process priority
      k if you want to kill process 
      press any other key to out ....");
                char ch = char.Parse(Console.ReadLine());
                while (ch == 'k' || ch == 'K' || ch == 'p' || ch == 'P')
                {
                    if (ch == 'k' || ch == 'K')
                    {
                        KillProcess();
                    }
                    else if (ch == 'p' || ch == 'P')
                    {
                        ChangeProcessPriority();
                    }
                    Console.WriteLine();
                    Console.WriteLine(@"Enter : P if you want change process priority
        k if you want to kill process ");
                    ch = char.Parse(Console.ReadLine());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void PrintProcessList()
        {
            try
            {
                Process[] processlist = Process.GetProcesses();
                double x = 0;

                foreach (Process p in processlist)
                {

                    if (lastTime == null)
                    {
                        lastTime = DateTime.Now;
                        lastTotalProcessorTime = p.TotalProcessorTime;
                    }
                    else
                    {
                        if (p.Id != 0)
                        {
                            curTime = DateTime.Now;
                            curTotalProcessorTime = p.TotalProcessorTime;

                            Thread.Sleep(150);

                            lastTime = DateTime.Now;
                            lastTotalProcessorTime = p.TotalProcessorTime;

                            double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds)
                                / curTime.Subtract(lastTime).TotalMilliseconds
                                / Convert.ToDouble(Environment.ProcessorCount);

                            x += CPUUsage;

                            //if (CPUUsage > 0.0)
                            Console.WriteLine("{0}           CPU: {1:0.0}%     {2}", p.Id, CPUUsage * 100, p.ProcessName);
                        }
                        else
                        {
                            x = x * 100;
                            Console.WriteLine("{0}           CPU: {1:0.0}%     {2}", p.Id, (100 - x), p.ProcessName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void KillProcess()
        {
            try
            {
                Console.Write("Enter process id you want to kill it : ");
                int z = int.Parse(Console.ReadLine());
                Process p = Process.GetProcessById(z);
                p.Kill();
                Console.WriteLine("Process {0} has been killed ,,, ", p.ProcessName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PrintProcessList();
        }
        public static void ChangeProcessPriority()
        {
            try {
            Console.Write("Enter process id you want to change it's priority : ");
            int z = int.Parse(Console.ReadLine());
            Console.Write(@"Enter number of priority you want :
      1-AboveNormal
      2-BelowNormal
      3-High
      4-Idle
      5-Normal
      6-RealTime
");

                using (Process p = Process.GetProcessById(z))
                {
                    Console.Write("Enter Number -> ");
                    int pro = int.Parse(Console.ReadLine());
                    if (pro == 1)
                        p.PriorityClass = ProcessPriorityClass.AboveNormal;
                    else if (pro == 2)
                        p.PriorityClass = ProcessPriorityClass.BelowNormal;
                    else if (pro == 3)
                        p.PriorityClass = ProcessPriorityClass.High;
                    else if (pro == 4)
                        p.PriorityClass = ProcessPriorityClass.Idle;
                    else if (pro == 5)
                        p.PriorityClass = ProcessPriorityClass.Normal;
                    else if (pro == 6)
                        p.PriorityClass = ProcessPriorityClass.RealTime;
                    else
                        Console.WriteLine("Priority not changed ... ");
                    if (pro > 0 && pro < 7)
                        Console.WriteLine("Process {0} has been updated it's priority ,,, ", p.ProcessName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PrintProcessList();
        }
    }
}
