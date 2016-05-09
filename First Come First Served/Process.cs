using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace First_Come_First_Served
{
    class Process
    {
        #region переменные
        private string processName;
        private int arrivalTime;
        private int burst;
        private int wait;
        private int start;
        private int end;
        private int turnAround;
        private int priority;
        #endregion

        #region конструкторы
        public Process(string processName, int arrivalTime, int burst, int priority)
        {
            this.processName = processName;
            this.arrivalTime = arrivalTime;
            this.burst = burst;
            this.priority = priority;
        }

        public Process()
        {
        }
        #endregion

        #region проверка на существование файла Process.txt
        public string FileExisting(string filePath)
        {
            //файл с процессами будет располагаться в текущей директории проекта
            //то есть в c:\users\piotr\documents\visual studio 2015\Projects\First Come First Served\First Come First Served\bin\Debug           
            if (File.Exists(filePath + @"\Processes.txt"))
            {
                Console.WriteLine("ФАЙЛ С ПРОЦЕССАМИ В ДИРЕКТОРИИ {0} ", filePath + " СУЩЕСТВУЕТ!");
            }
            else
            {
                Console.WriteLine("ФАЙЛ С ПРОЦЕССАМИ В ДИРЕКТОРИИ {0} ", filePath + " НЕ СУЩЕСТВУЕТ");
            }
            return filePath;
        }
        #endregion

        #region запись содержимого файла в processes
        public List<Process> DataList(string filePath, string FileText)
        {
            string[] lines = FileText.Split('\n');
            List<Process> processes = new List<Process>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] tabs = lines[i].Split('\t');
                Process x = new Process(tabs[0], int.Parse(tabs[1]), int.Parse(tabs[2]), int.Parse(tabs[3]));
                processes.Add(x);
            }
            return processes;
        }
        #endregion

        #region вывод процессов из файла 
        public void ListProcesses(List<Process> processes)
        {
            Console.WriteLine("Process" + "\tArrival Time" + "\tCPU Burst" + "\tPriority");
            foreach (var items in processes)
            {
                Console.WriteLine(items.processName + "\t" + items.arrivalTime + "\t" + "\t" + items.burst + "\t" + "\t" + items.priority);
            }
        }
        #endregion

        #region сортировка процессов БЕЗ ПРИОРИТЕТА!!!
        public List<Process> ProcessSorting(List<Process> processes)
        {
            Process temp;
            for (int i = 0; i < processes.Count; i++)
            {
                for (int j = i + 1; j < processes.Count; j++)
                {
                    if (processes[i].arrivalTime > processes[j].arrivalTime || (processes[i].arrivalTime == processes[j].arrivalTime && processes[i].burst > processes[j].burst))
                    {
                        temp = processes[j];
                        processes[j] = processes[i];
                        processes[i] = temp;
                    }
                }
            }
            return processes;
        }
        #endregion

        #region применение алгоритма First Come First Served
        public List<Process> ResultFCFS(List<Process> processes)
        {
            int clock = 0, totalwait = 0, totalturnAround = 0;
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i].arrivalTime > clock)
                {
                    processes[i].start = processes[i].arrivalTime;
                    clock += processes[i].burst;
                }
                else
                {
                    if (i > 0)
                        processes[i].start = processes[i - 1].end;
                    clock += processes[i].burst;
                }
                if (processes[i].start > processes[i].arrivalTime)
                    processes[i].wait = processes[i].start - processes[i].arrivalTime;
                else processes[i].wait = 0;
                processes[i].end = processes[i].start + processes[i].burst;
                processes[i].turnAround = processes[i].wait + processes[i].burst;
                totalwait += processes[i].wait;
                totalturnAround += processes[i].turnAround;
            }
            Calculating_AWT_ATT(processes, totalwait, totalturnAround);
            return processes;
        }
        #endregion

        #region вычисления: AWT - среднее время ожидания и ATT = среднее полное время выполнения
        private static void Calculating_AWT_ATT(List<Process> processes, int totalwait, int totalturnAround)
        {
            Console.WriteLine("Name\tArrival\tBurst\tStart\tEnd\tWait\tturnaround");
            for (int i = 0; i < processes.Count; i++)
            {
                Console.Write(processes[i].processName + "\t" + processes[i].arrivalTime + "\t" + processes[i].burst + "\t" + processes[i].start + "\t" + processes[i].end + "\t" + processes[i].wait + "\t" + processes[i].turnAround);
                Console.WriteLine();
            }
            double att = 0, awt = 0;
            awt = (double)totalwait / (double)processes.Count;
            att = (double)totalturnAround / (double)processes.Count;
            Console.WriteLine("A.W.T= " + awt + "\t A.T.T= " + att);
            Console.ReadKey();
        }
        #endregion
    }
}
