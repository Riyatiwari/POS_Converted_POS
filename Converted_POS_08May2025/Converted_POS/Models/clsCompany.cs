using System;

namespace Converted_POS.Models
{
    public class clsCompany
    {
        public int? Company_id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Starting_Date { get; set; } 
        public string Domain { get; set; }
        public string IP_Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public byte Logo { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Postal { get; set; }
        public string Website { get; set; }
        public string Contact { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Modify_Date { get; set; }
        public int Registration_no { get; set; }
        public int GST_VAT { get; set; }
        public int CST_VAT { get; set; }
        public int Service_tax_no { get; set; }
        public int Pan_no { get; set; }
        public string Branch_Name { get; set; }
        public string Synchronization { get; set; }
        public string Venue_Name { get; set; }
        public string mac_id { get; set; }
        public string Vat_No { get; set; }
        public string Receipt_Header { get; set; }
        public string Receipt_Footer { get; set; }
        public int log_off_time { get; set; }
        public int par_sale_par_operator { get; set; }
        public int Currency_id { get; set; }
        public string C_Timezone { get; set; }
        public string judo_id { get; set; }
        public string judo_token { get; set; }
        public string judo_secret { get; set; }
        public string week_start_day { get; set; }
        public bool show_chips { get; set; }
        public int BusinessDoneBy { get; set; }
        public bool Display_declaration { get; set; }
        public bool chk_duration { get; set; }
        public bool IsAddTax2 { get; set; }
        public bool IsExclusiveTax { get; set; }
        public bool IsPaymentGtway { get; set; }
        public string Store_UUID { get; set; }
        public int ProductType { get; set; }
        public string StoreUUID { get; set; }
    }
}
