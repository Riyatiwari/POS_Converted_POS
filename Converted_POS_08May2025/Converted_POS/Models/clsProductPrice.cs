using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsProductPrice
    {
        public int Product_Price_Id { get; set; }
        public string PPrice { get; set; }
        public int cmp_id { get; set; }
        public int login_id { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public bool is_active { get; set; }
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
        public bool is_default { get; set; }
    }
}
