using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsSalesReport
    {
        public int sales_id { get; set; }
        public int cmp_id { get; set; }
        public int total_amount { get; set; }
        public int total_discount { get; set; }
        public int net_amount { get; set; }
        public string ip_address { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modify_date { get; set; }
        public int login_id { get; set; }
        public int change { get; set; }
        public int tax { get; set; }
        public string mac_id { get; set; }
        public int machine_id { get; set; }
        public int ref_id { get; set; }
        public int venue_id { get; set; }
        public int actual_total_amount { get; set; }
        public int temp_sale_id { get; set; }
        public bool is_return { get; set; }
        public bool mode { get; set; }
        public int value { get; set; }
        public int location_id { get; set; }
        public int input_amount { get; set; }
        public bool sale_type { get; set; }
        public bool is_table { get; set; }
        public DateTime Payment_Date { get; set; }
        public int Payment_Amount { get; set; }
        public string table_name { get; set; }
        public bool is_close { get; set; }
        public string Discount_mode { get; set; }
        public string discount_name { get; set; }
        public int table_created_machine_id { get; set; }
        public int table_close_machine_id { get; set; }
        public int Room_Payment_Number { get; set; }
        public string Room_Payment_Name { get; set; }
        public string web_name { get; set; }
        public string web_mobile { get; set; }
        public string web_email { get; set; }
        public string web_address { get; set; }
        public int is_deliver { get; set; }
        public string deliver_time { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string pcode { get; set; }
        public string Landmark { get; set; }
        public int cust_id { get; set; }
        public int redeem_bonus { get; set; }
        public string order_ref { get; set; }
        public string payment_ref { get; set; }
        public int surcharge_amount { get; set; }
        public int surcharge_value { get; set; }
        public string surcharge_name { get; set; }
        public int GrandTotal { get; set; }
        public int NoOfCovers { get; set; }
        public int EatOutAmount { get; set; }
        public int transaction_count { get; set; }
        public string voucher { get; set; }
        public int voucherId { get; set; }
        public int VoucherBalance { get; set; }
        public string shift_name { get; set; }
        public bool is_cash_table { get; set; }
        public int table_id { get; set; }
        public string Comments { get; set; }
        public int delivery_charges { get; set; }
        public string table_uuid { get; set; }
        public int tran_source { get; set; }
        public DateTime deliver_date { get; set; }
        public string Credit_pay_uuid { get; set; }
        public int sales_sub_type { get; set; }
        public bool Is_Email { get; set; }
        public string Original_table_UUID { get; set; }
        public string Transfered_Table_UUID { get; set; }
        public string Integrated_Terminal_ID { get; set; }
        public string Integrated_Merchant_ID { get; set; }
        public string Integrated_SaleType { get; set; }
        public string Integrated_Entry_Method { get; set; }
        public string Elina_Room_No { get; set; }
        public int TIP_AMOUNT { get; set; }
        public int CardPayType { get; set; }
        public string KINETIC_REF_NO { get; set; }
        public string pay_uuid { get; set; }
        public bool No_OfCover { get; set; }
        public string NON_TABLE_PART_PAYMENT { get; set; }




        

    }
}
