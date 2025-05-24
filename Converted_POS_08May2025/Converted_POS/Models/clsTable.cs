using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsTable
    {
        public int table_id { get; set; }
        public string name { get; set; }
        public int location_id { get; set; }
        public string location_name { get; set; }
        public int min_cover { get; set; }
        public int max_cover { get; set; }
        public bool is_active { get; set; }
        public bool is_open { get; set; }
        public int shorting_no { get; set; }
        public int cmp_id { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public int login_id { get; set; }
        public string mac_id { get; set; }
        public string venue_id { get; set; }
        public string machine_id { get; set; }
        public string Table_name { get; set; }
        public int MinCover { get; set; }
        public int MaxCover { get; set; }
        public string AllowedJoin { get; set; }
        public int TableNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public int SortingNo { get; set; }
    }
}
