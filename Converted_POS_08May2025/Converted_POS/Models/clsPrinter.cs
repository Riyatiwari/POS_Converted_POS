using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsPrinter
    {
        public int printer_id { get; set; }
        public int cmp_id { get; set; }
        public string name { get; set; }
        public string Alias { get; set; }
        public bool is_active { get; set; }
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
        public bool portValue { get; set; }
        public string portV
        {
            get { return portValue ? "Yes" : "No"; }
        }
        
        public DateTime create_date { get; set; }
        public DateTime modify_date { get; set; }
        public string ip_address { get; set; }
        public int login_id { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        public int machine_id { get; set; }
        public string printer_ip_address { get; set; }
        public int port { get; set; }
        public string network_type { get; set; }
        public int vender_id { get; set; }
        public string budrate { get; set; }
        public string device_name { get; set; }
        public byte? is_product_small_large { get; set; }
        
        public enum ProductSize
        {
            Small = 0,
            Large = 1,
            ExtraLarge = 2
        }
        public byte? is_condiment_small_large { get; set; }
        public bool group_by { get; set; }
        public bool consile_date { get; set; }
        public int group_by_with { get; set; }
        public bool OrderFlag { get; set; }
    }
}
