using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsAutoSyncRecored
    {
        public int AutoSync_Id { get; set; }
        public int cmp_id { get; set; }
        public int Venue_Id { get; set; }
        public string Venue_Name { get; set; }
        public int Location_Id { get; set; }
        public string Location_Name { get; set; }
        public int Till_Id { get; set; }
        public string Till_Name { get; set; }
        public int SyncBtn_No { get; set; }
        public bool SyncFlag { get; set; }
        public int login_id { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? modify_date { get; set; }
        public string Page_list { get; set; }
        public string Page_name { get; set; }
        public List<SelectListItem> Locations { get; set; }

    }
}
