using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPU.DORMITORY.BATCH
{
    class Program
    {
        static void Main(string[] args)
        {
            BatchUpdateStudentStatus batchUpdateStduentStatus = new BatchUpdateStudentStatus();
            batchUpdateStduentStatus.Start();
        }
    }
}
