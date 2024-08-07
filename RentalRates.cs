using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    public class RentalRates
    {
        public decimal DailyRate { get; set; }
        public bool IsValid()
        {
            return DailyRate >= 0;
        }
    }
}
