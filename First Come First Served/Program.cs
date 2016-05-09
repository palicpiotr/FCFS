using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace First_Come_First_Served
{
    class Program
    {
        static void Main(string[] args)
        {
            Process process = new Process();
            string filePath = Environment.CurrentDirectory.ToString();
            process.FileExisting(filePath);
            string fileText = File.ReadAllText(filePath + @"\Processes.txt");
            Console.WriteLine("Before sorting the list of processes: ");
            process.ListProcesses(process.DataList(filePath, fileText));
            Console.WriteLine("After sorting the list of processes");
            process.ListProcesses(process.ProcessSorting(process.DataList(filePath, fileText)));
            Console.WriteLine("Result:");
            process.ResultFCFS(process.ProcessSorting(process.DataList(filePath, fileText)));
        }
    }
}
