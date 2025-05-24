using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsZReport
    {
        public int cmp_id { get; set; }
        public int venue_id { get; set; }
        public int staff_id { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int NumOfTran { get; set; }
        public int NoOfReturns { get; set; }
        public string receipt_header { get; set; }
       
    }
}
