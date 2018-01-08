using System;
using System.Diagnostics;

namespace MemoryLeaker.Models
{
    public class MemoryDetails
    {
        public int WorkingSetMB
        {
            get { return (int)(Process.GetCurrentProcess().WorkingSet64 / 1024); }
        }

        public int GCTotalMemoryMB
        {
            get { return (int)(GC.GetTotalMemory(false) / 1024); }
        }
    }
}