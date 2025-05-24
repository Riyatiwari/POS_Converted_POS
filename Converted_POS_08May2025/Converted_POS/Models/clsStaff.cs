using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsStaff
    {
        public int? staff_id { get; set; }
        public int cmp_id { get; set; }
        public string staff_code { get; set; }
        public string managercode { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime? joining_date { get; set; }
        public int branch_id { get; set; }
        public int department_id { get; set; }
        public int designation_id { get; set; }
        public string address { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int city_id { get; set; }
        public int national_id { get; set; }
        public string contact_no { get; set; }
        public string email { get; set; }
        public DateTime? last_working_date { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public string function_id { get; set; }
        public bool is_active { get; set; }
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
        public string ip_address { get; set; }
        public int login_id { get; set; }
        public bool till_active { get; set; }
        public string photo { get; set; }
        public string mac_id { get; set; }
        public string other_id { get; set; }
        public int machine_id { get; set; }
        public string Authentication_Code { get; set; }
        public string till_code { get; set; }
        public bool is_trainee { get; set; }
        [Required]
        public int m_staff_id { get; set; }
        public string venue_id { get; set; }
        public string StoreUUID { get; set; }
        public string UserUUID { get; set; }
    }
}
