using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsDevice
    {
        [Required(ErrorMessage = "Device ID is required.")]
        public int? device_id { get; set; }
        [Required]
        public string name { get; set; }
        public string code { get; set; }
        public string serial_no { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public int cmp_id { get; set; }
        public int login_id { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        public int Device_Type_id { get; set; }
        public string Device_Type_Name { get; set; }
        //[Required(ErrorMessage = "Device Category is required.")]
        public int deviceCategoryId { get; set; }
        [Required(ErrorMessage = "Device Type is required.")]
        public int deviceTypeId { get; set; }
        public bool is_active { get; set; }
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
        public string printer_ip_address { get; set; }
        public int port { get; set; }
        public string network_type { get; set; }
        public int vender_id { get; set; }
        public string budrate { get; set; }
        public string device_name { get; set; }
        public int width { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string bluetooth_name { get; set; }
        public string application_profile_id { get; set; }
        public string service_key { get; set; }
        public int? Device_SubType { get; set; }
        public string Device_SubTypeName { get; set; }
        public int device_category { get; set; }
        public int DeviceCategoryId { get; set; }
        public List<SelectListItem> DTDeviceCategory { get; set; }
        public List<SelectListItem> DTDeviceType { get; set; }
        public List<SelectListItem> MachineList { get; set; }
        public int? SelectedDeviceCategoryId { get; set; }  // Nullable to handle cases where it's not selected
        public int? SelectedDeviceTypeId { get; set; }
        public int? SelectedTillId { get; set; }

    }
}
