using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsLocation
    {
        public int location_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string code { get; set; }
        public int city_ID { get; set; }
        public string City_Name { get; set; }
        public int state_Id { get; set; }
        public int country_id { get; set; }
        public int cmp_id { get; set; }
        public int login_id { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        public string venue_name { get; set; }
        public bool till_auto_log_off { get; set; }
        public bool cashback { get; set; }
        public decimal service_charges { get; set; }
        public bool srv_charge_delivery { get; set; }
        public bool srv_charge_order { get; set; }
        public bool srv_charge_kiosk { get; set; }
        public string TillAutoLogOff
        {
            get { return till_auto_log_off ? "Yes" : "No"; }
        }
        public int machine_id { get; set; }
        public bool is_active { get; set; }
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
        public decimal min_amount { get; set; }
        public bool is_delivery { get; set; }
        public string judo_id { get; set; }
        public string judo_token { get; set; }
        public string judo_secret { get; set; }
        public string CashflowId { get; set; }
        public string CashflowUrl { get; set; }
        public string CashflowApiKey { get; set; }
        public string CustomPayId { get; set; }
        public string CustomPayToken { get; set; }
        public string CustomPaySecret { get; set; }
        public string CustomPayBase64 { get; set; }
        public string CustomPayUrl { get; set; }
        public bool is_skippay { get; set; }
        public bool is_email { get; set; }
        public string onln_opt_name { get; set; }
        public string url { get; set; }
        public string config_id { get; set; }
        public string api_key { get; set; }
        public string onlinepayment { get; set; }
        public bool websales_check_schedule { get; set; }
        public string Contact { get; set; }
        public string LocationContact { get; set; }
        public string Email { get; set; }
        public string LocationEmail { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string header_reciept { get; set; }
        public Byte[] L_image { get; set; }
        public string BG_Color { get; set; }
        public string Font_Color { get; set; }
        public string Body_Color { get; set; }
        public int delivery_charges { get; set; }
        public bool click_and_collect { get; set; }
        public bool Order_table { get; set; }
        public bool HourlySlot { get; set; }
        public Byte[] Click_Collect_image { get; set; }
        public Byte[] Delivery_image { get; set; }
        public Byte[] OrderAtTable_image { get; set; }
        public string theme { get; set; }
        public string message_delivery { get; set; }
        public string message_order_table { get; set; }
        public bool schedule_required { get; set; }
        public bool schedule_cnc { get; set; }
        public Byte[] GraphicViewBackground { get; set; }
        public Byte[] GraphicViewTable { get; set; }
        public int future_booking_days { get; set; }
        public bool betweenSlot { get; set; }
        public bool Is_Email_APK { get; set; }
       
        public string CustomPay_Id { get; set; }
        public string TipAS { get; set; }
        public string CustomPay_Token { get; set; }
        public string CustomPay_Secret { get; set; }
        public string CustomPay_Base64 { get; set; }
        public string CustomPay_url { get; set; }
        public string Till_Phn_no { get; set; }
        public string clientid { get; set; }
        public string clientsecret { get; set; }
        public string redirect_uri { get; set; }
        public string contactID { get; set; }
        public string Table_As_Box { get; set; }
        public string gtway_MerchantID { get; set; }
        public int gtway_StoreID { get; set; }
        public int gtway_LocationID { get; set; }
        public string gtway_StoreName { get; set; }
        public string gtway_LocationName { get; set; }
        public string GC_Btn_stl { get; set; }
        public string GC_Btn_img_typ { get; set; }
        public string GC_Btn_fnt_size { get; set; }
        public string GC_Btn_bkgd_clr { get; set; }
        public string GC_Btn_txt_clr { get; set; }
        public string C_Btn_stl { get; set; }
        public string C_Btn_img_typ { get; set; }
        public string C_Btn_fnt_size { get; set; }
        public string C_Btn_bkgd_clr { get; set; }
        public string C_Btn_txt_clr { get; set; }
        public string P_Btn_stl { get; set; }
        public string P_Btn_img_typ { get; set; }
        public string P_Btn_fnt_size { get; set; }
        public string P_Btn_bkgd_clr { get; set; }
        public string P_Btn_txt_clr { get; set; }
        public string GC_Btn_img_typ_pos { get; set; }
        public string C_Btn_img_typ_pos { get; set; }
        public string P_Btn_img_typ_pos { get; set; }
        public bool is_sunmi_second_screen { get; set; }
        public Byte[] second_screen_image_1 { get; set; }
        public Byte[] second_screen_image_2 { get; set; }
        public Byte[] second_screen_video { get; set; }
        public bool poslite { get; set; }
        public string sunmi_video_path { get; set; }
        public bool is_GetCovers { get; set; }
        public string login_src_bkgd_clr { get; set; }
        public string login_src_login_btn { get; set; }
        public string ImageFileName { get; set; }
        public string Till_url { get; set; }
        public string WebSalesLink { get; set; }

        public Byte[] POS_logo { get; set; }
        public List<SelectListItem> PaymentMethods { get; set; }
    }
}
