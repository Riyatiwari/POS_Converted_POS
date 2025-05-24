using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsTax
    {
        public int Tax_id { get; set; }
        public int cmp_id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Mode { get; set; }
        [Required]
        public int Value { get; set; }
        public bool Is_active {get;set;}
        public string ip_address { get; set; }
        public string Active
        {
            get { return Is_active ? "Yes" : "No"; }
        }
        [Required]
        public DateTime Effective_Date { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Modified_date { get; set; }
        public int Login_id { get; set; }
        public string Ip_address { get; set; }
        public string mac_id { get; set; }
        public int machine_id { get; set; }
        public int venue_id { get; set; }
    }
}
