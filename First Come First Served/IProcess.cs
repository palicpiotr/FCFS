using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Come_First_Served
{
    interface IProcess
    {
        string FileExisting(string filePath);
        List<Process> DataList(string filePath, string FileText);
        void ListProcesses(List<Process> processes);
        List<Process> ProcessSorting(List<Process> processes);
        List<Process> ResultFCFS(List<Process> processes);
       
    }
}
