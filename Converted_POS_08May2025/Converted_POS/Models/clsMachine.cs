using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsMachine
    {

        public int? machine_id { get; set; }
        [Required(ErrorMessage = "Name field is required.")]
        public string name { get; set; }
        public string serial_no { get; set; }
        public string model { get; set; }
        public string code { get; set; }
        public string mac_address { get; set; }
        public int login_id { get; set; }
      
        public int cmp_id { get; set; }
        
        [Required(ErrorMessage = "IP Address is required.")]
        public string ip_address { get; set; } 
        public string Mainip_address { get; set; } 
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid location.")]
        public int? location_id { get; set; }        
        public string location_name { get; set; }
        public bool is_assign { get; set; }
        public String isassign { get; set; }
        public bool is_minipos { get; set; }
        public bool is_active { get; set; }
        public String isactive { get; set; }
        public string Receipt_Header { get; set; }
        public string Receipt_Footer { get; set; }
        public bool is_master_self { get; set; }
        public string tillip_address { get; set; }
        public bool till_server { get; set; }
        public bool table_sharing { get; set; }
        public bool printer_sharing { get; set; }
        public string sync_time { get; set; }
        public bool back_to_main_function_on_till { get; set; }
        public bool extraSurcharge { get; set; }
        public bool Only_table_trans { get; set; }
        public bool AutoSurcharge { get; set; }
        public bool AutoSurchargeTables { get; set; }
        public bool AutoSurchargeNonTables { get; set; }
        public bool AutoSurchargeCloakroom { get; set; }
        public bool AutoSurchargeChips { get; set; }
        public bool TillRequest { get; set; }
        public bool KioskRequest { get; set; }
        public bool NoCashDrawer { get; set; }
        public bool ReSync_Request { get; set; }
        public bool Service_Controller_Start { get; set; }
        public bool Service_Websale_print { get; set; }
        public bool Service_Print_Share { get; set; }
        public bool Service_print_Share_Other_Till { get; set; }
        public bool Is_NoLogout { get; set; }
        public bool Is_PrintServer { get; set; }
        public bool Is_ServiceBooking { get; set; }
        public bool Is_OnlineZreport { get; set; }
        public bool IsKiosk { get; set; }
        public int? TblTranLimit { get; set; }
        public string gtway_TID { get; set; }
        public bool QuickTran { get; set; }
        public bool kitchenPrint { get; set; }
        public bool ReceiptPrint { get; set; }
        public bool ElinaTran { get; set; }
        public bool is_sunmi_second_screen { get; set; }
        public Byte[] second_screen_image_1 { get; set; }
        public Byte[] second_screen_image_2 { get; set; }
        public IFormFile sunmi_video_path { get; set; }
        public string sunmi_video_path_db { get; set; }
        public string ImageFileName1 { get; set; }
        public string ImageFileName2 { get; set; }
        public string VideoFileName { get; set; } // Store the file name
        public string VideoFilePath { get; set; }
        public bool poslite { get; set; }
        public int? hardware_type { get; set; }
        public string TillUUID { get; set; }
        public int Generate_code { get; set; }
        public DateTime Valid { get; set; }
        public int machine_detail_id { get; set; }      
        public int till_id { get; set; }
        public int? master_till { get; set; }

        public int? function_id { get; set; }

        public int Keymap_id { get; set; }
        //public int device_id { get; set; }
        public List<int> device_id { get; set; }
        public List<KeymapModel> KeymapModel { get; set; }
        public List<DeviceMappingModel> DeviceMappingModel { get; set; }
    }
}
