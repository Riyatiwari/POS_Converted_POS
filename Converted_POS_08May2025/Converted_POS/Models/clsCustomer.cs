using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsCustomer
    {
        public int customer_id { get; set; }
        public int cmp_id { get; set; }
        public int Address_ID { get; set; }
        public int AccountID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string contact_no { get; set; }
        public string address { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int city_id { get; set; }
        public string postal_code { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set;}
        public DateTime DateTimeExpiry { get; set; }
        public float bounus_point { get; set; }
        public long CardNumber { get; set; }
        public bool Is_credit { get; set; }
        public bool is_active { get; set; }
        public string ip_address { get; set; }
        public int login_id { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        public string other_id { get; set; }
        public int machine_id { get; set; }
        public int profile_id { get; set; }
        public int Price_level_id { get; set; }
        public Byte[] CustomerProfile { get; set; }
        public Byte[] CustomerProfileFile { get; set; }
        public string ImageFileName { get; internal set; }
    }
}
