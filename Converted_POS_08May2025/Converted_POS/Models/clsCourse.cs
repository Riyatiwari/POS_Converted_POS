using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsCourse
    {
        public int Course_id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Cmp_id { get; set; }
        public string Ip_Address { get; set; }
        public int Login_id { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modify_Date { get; set; }
        public int courses_category_id { get; set; }
        public string courses_category_name { get; set; }
        public bool is_CheckSlot { get; set; }
    }
}
