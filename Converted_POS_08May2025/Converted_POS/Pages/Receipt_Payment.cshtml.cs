using System;
 
using Microsoft.AspNetCore.Mvc;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Text;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Converted_POS.Pages
{
    public class Receipt_Payment : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Receipt_Payment(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                //_connectionString = _configuration.GetConnectionString("YourConnectionStringName");
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (log, rethrow, etc.)
                throw new Exception("Error initializing Receipt", ex);
            }
        }

        [BindProperty]
        public string ReceiptContent { get; set; }
        public string LinkPay { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Store { get; set; }

        [BindProperty(SupportsGet = true)]
        public string table_uuid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Refid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Tid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Cv{ get; set; }
        [BindProperty(SupportsGet = true)]
        public int Lid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty(SupportsGet = true)]
        public double Amount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Payuuid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Uemail { get; set; }
      

        // Property to hold the generated Pay by Link URL
       // public string LinkPay { get; set; }
        public void OnGet(string table_uuid, string store, double amount, int refid, int tid, int lid, int cv, string Payuuid, int Lid,int AccountID)
        {
            try
            {
               

                Store = store;
                table_uuid = table_uuid;
                Refid = refid;
                Tid = tid;
                lid = Lid;
                Cv = cv;
                Amount = amount;
                Payuuid = Payuuid;
                AccountID = AccountID;
                
                //string tableuuid = HttpContext.Session.GetString("table_uuid");
                //string refids = HttpContext.Session.GetString("refid");
                //string tids = HttpContext.Session.GetString("tid");
                //string lids = HttpContext.Session.GetString("Lid");
                //string cids = HttpContext.Session.GetString("cid");
                //string amounts = HttpContext.Session.GetString("amount");
                //string payuuids = HttpContext.Session.GetString("payuuid");
                //string accountids = HttpContext.Session.GetString("AccountID");

         

                //string store = Store;
                //string tableUuid = TableUuid;
                // int refid = Refid;
                //int tid = Tid;
                //int cid = Cid;
                //int lid = Lid;
                //int accountId = AccountId;
                // double amount = Amount;
                // string pay_Uuid = Pay_Uuid;
                // string uemail = Uemail;

                LinkPay = $"https://Live.mytenderpos.com/PaybyLink.aspx?Amount={amount}&refid={refid}&store={store}&tid={tid}&accoutid={AccountID}&payuuid={Payuuid}&lid={lid}&cv={cv}";
                CheckStore(store);


               //string tableUuid = Request.Query["tableuuid"];

               // CheckStore(Request.Query["store"].ToString());
                // string table_uuid = Request.Query["tableuuid"].ToString();
                string connectionString = HttpContext.Session.GetString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    using (SqlCommand command = new SqlCommand("Get_credit_sales_for_email_transactionUUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@sales_id", table_uuid);
                        DataTable dtS = new DataTable();

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dtS);
                        }

                        if (dtS.Rows.Count == 0)
                        {
                            ViewData["ShowPrintButton"] = false;
                            ViewData["ReceiptContent"] = "Receipt will be available in a few minutes.";
                            ViewData["TotalAmount"] = amount;

                        }
                        else
                        {
                            for (int sales = 0; sales <= 0; sales++)
                                {
                                    if (Convert.ToDouble(dtS.Rows[sales]["total_amount"].ToString()) > 0)
                                    {
                                        StringBuilder builder = new StringBuilder();

                                        builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>");
                                        builder.Append("<div id='printr' style='width:auto; color:#000000; font-family:verdana;border:1px solid  #ccc;'>");
                                
                                    if (dtS.Rows[sales]["L_image"].ToString() != "")
                                        {
                                            builder.Append("<div style='width:100%;height:100px; margin-top: 10px; text-align:center;'>");

                                            byte[] bytes = (byte[])dtS.Rows[sales]["L_image"];
                                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                            string imageUrl = "data:image/jpg;base64," + base64String;
                                            


                                            int imageWidth = 200;  
                                            int imageHeight = 100; 

                                            // Using the base64 string directly in the image src attribute
                                            builder.Append("<img width='" + imageWidth + "' height='" + imageHeight + "' src='" + imageUrl + "' alt='Logo'/>");

                                            builder.Append("</div>");
                                            ViewData["image"] = $"<img class='circular-image'  width='{imageWidth}' height='{imageHeight}' src='{imageUrl}' alt='Logo'/>";
                                        }
                                        else
                                        {
                                            builder.Append("<div style='width:100%;height:100px;background-color:#000000;color: " + dtS.Rows[sales]["Font_Color"].ToString() + " '>");
                                            builder.Append("<table style='width:100%; font-family:verdana;background-color:#000000;color: " + dtS.Rows[sales]["Font_Color"].ToString() + "'>");
                                            builder.Append("<tr><td>&nbsp;</td></tr>");
                                            builder.Append("<tr><td style='text-Align:center;'>");
                                           // builder.Append("<img width='100%' height='100px' src ='http://testing.mytenderpos.com/LocationImageHandler.ashx?lid=" + dtS.Rows[sales]["Location_id"].ToString() + "&sv=" + HttpContext.Session.GetString("store").ToString() + "' alt='Logo'/>");
                                            //builder.Append(dtS.Rows[sales]["company_name"].ToString());
                                            // builder.Append("</td></tr>");
                                            // builder.Append("<tr><td>");
                                            // builder.Append(" Phone : " + dtS.Rows[sales]["cmp_contact"].ToString() + " <br/> E-Mail : " + dtS.Rows[sales]["cmp_email"].ToString() + "");
                                            builder.Append("</td></tr>");
                                            builder.Append("<tr><td>&nbsp;</td></tr>");
                                            builder.Append("</table>");
                                            builder.Append(" </div>");
                                        }

                                        //LogHelper.Error("Table_transaction:Check 1");

                                        //builder.Append("<table style='width:100%;height:150px; color:#000000;margin:5px;font-family:verdana;'>");
                                       // builder.Append("<tr><td>");
                                       //// builder.Append("Dear Customer, ");
                                       // builder.Append("</td></tr>");
                                       // builder.Append("<tr><td>");
                                       //// builder.Append("Thank you for placing order with us. ");
                                       // builder.Append("</td></tr>");
                                        //builder.Append("<tr><td>");
                                        //builder.Append("Order Confirmation order# " + dtS.Rows[sales]["order_num"].ToString() + " (" + dtS.Rows[sales]["created_date"].ToString() + ")");
                                       // builder.Append("</td></tr>");
                                        //builder.Append("</table>");

                                        builder.Append("<table style='width:100%;font-family:verdana;font-size:12px;' cellpadding='0px' cellspacing='0px'>");
                                        builder.Append("<tr> <td ></td><td ></td> </tr>");

                                        builder.Append("<tr style='height: 50px;'><td colspan='2' style='text-align:center;'><b>" + dtS.Rows[sales]["company_name"].ToString() + "<br/>" + dtS.Rows[sales]["location_name"].ToString() + "</b></td></tr>");  //"<br/>" + dtS.Rows[0]["name"].ToString() +
                                        builder.Append("<tr><td colspan='2' style='border-bottom :1px dashed #000000;border-top:1px dashed #000000;'>");
                                        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>");
                                        builder.Append("<tr><td width='25%'>VAT NUMBER</td>");
                                        builder.Append("<td>:</td>");
                                        builder.Append("<td>" + dtS.Rows[sales]["Vat_No"].ToString() + "</td>");
                                        builder.Append(" </tr></table> </tr>");
                                        builder.Append("<tr><td colspan='2'> <table width='100%'>");

                                        //builder.Append("<tr><td >Till Name </td>");
                                        //builder.Append("<td>:</td>");
                                        //builder.Append("<td >" + Session["store"].ToString() + "</td></tr>");
                                        builder.Append("<tr><td width='25%'>Date & Time</td>");
                                        builder.Append("<td>:</td>");
                                        builder.Append("<td >" + dtS.Rows[sales]["created_date"].ToString() + "</td></tr>");

                                        builder.Append("<tr><td width='25%'>Served By </td>");
                                        builder.Append("<td>:</td>");
                                        builder.Append("<td >" + dtS.Rows[sales]["StaffName"].ToString() + "</td></tr>");
                                        builder.Append("<tr><td width='25%'>Order# </td>");
                                        builder.Append("<td>:</td>");
                                        builder.Append("<td >" + dtS.Rows[sales]["order_num"].ToString() + "</td></tr>");

                                        //LogHelper.Error("Table_transaction:Check 2");

                                        if (dtS.Rows[sales]["cust_name"].ToString() != "")
                                        {
                                            builder.Append("<tr><td width='25%'>Customer Name</td><td>:</td> <td >" + dtS.Rows[sales]["cust_name"].ToString() + "</td></tr>");
                                        }
                                        if (dtS.Rows[sales]["Mobile"].ToString() != "")
                                        {
                                            builder.Append("<tr><td width='25%'>Mobile</td><td>:</td> <td >" + dtS.Rows[sales]["Mobile"].ToString() + "</td></tr>");
                                        }
                                        builder.Append("<tr><td width='25%'>Email</td><td>:</td> <td >" + dtS.Rows[sales]["cust_email"].ToString() + "</td></tr>");

                                        if (dtS.Rows[sales]["cust_address"].ToString() != "")
                                        {
                                            builder.Append("<tr><td width='25%'>Address</td><td>:</td> <td >" + dtS.Rows[sales]["cust_address"].ToString() + "</td></tr>");
                                        }
                                        if (dtS.Rows[sales]["table_name"].ToString() != "")
                                        {
                                            builder.Append("<tr><td width='25%'>Table </td><td>:</td> <td >" + dtS.Rows[sales]["table_name"].ToString() + "</td></tr>");
                                        }

                                        //LogHelper.Error("Table_transaction:Check 3");

                                        builder.Append("<tr><td>&nbsp;</td></tr>");

                                        builder.Append("<tr><td Colspan='3' style='text-align:Center'><b>Tax Invoice</b></td></tr>");
                                        builder.Append("</table></td></tr>");
                                        builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;border-top:1px dashed #000000'>");
                                        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>");

                                        double sub_total = 0;
                                        double group_total = 0;
                                        double sub_total_tax = 0;
                                        double change = 0;

                                        DataTable dtResult = new DataTable(); // Initialize DataTable to hold the result
                                                                              //using (SqlConnection connection = new SqlConnection("your_connection_string_here"))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("Get_tsales_for_email_new", connection))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@cmp_id", Convert.ToDouble(dtS.Rows[sales]["cmp_id"].ToString()));
                                                cmd.Parameters.AddWithValue("@sales_id", Convert.ToDouble(dtS.Rows[sales]["sales_id"].ToString()));

                                                
                                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                                {
                                                    adapter.Fill(dtResult); // Fill DataTable with results from stored procedure
                                                }
                                            }
                                        }

                                        if (dtResult.Rows.Count > 0)
                                        {
                                            // LogHelper.Error("Table_transaction:Check 4 : count");

                                            string last_dept = "";

                                            for (int index = 0; index < dtResult.Rows.Count; index++)
                                            {
                                                if ((index == 0 || last_dept != dtResult.Rows[index]["deptCategory"].ToString()) && dtResult.Rows[index]["Is_Condiment"].ToString() == "0")
                                                {
                                                    last_dept = dtResult.Rows[index]["deptCategory"].ToString();

                                                    if (index != 0)
                                                    {

                                                        builder.Append("<tr> <td width='70%' style='border-top:1px dashed #000000'><b>Sub Total</b></td>");
                                                        builder.Append("<td width='5%' style='border-top:1px dashed #000000'></td><td width='25%' style ='text-align:right; border-top:1px dashed #000000'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>");

                                                        group_total = 0;
                                                    }


                                                    builder.Append("<tr><td width='70%'><b>" + last_dept + "</b></td> <td width='5%'> </td> <td width='25%'></td></tr>");
                                                }


                                                builder.Append("<tr><td>" + dtResult.Rows[index]["ProductName"].ToString() + " ");
                                                if (!string.IsNullOrEmpty(dtResult.Rows[index]["SizeName"].ToString()))
                                                {
                                                    builder.Append("<br/>(" + dtResult.Rows[index]["SizeName"].ToString() + ")</td>");
                                                }

                                                builder.Append("<td>" + dtResult.Rows[index]["quntity"].ToString() + "</td>");
                                                builder.Append("<td style='text-align:right;'>" + String.Format("{0:0.00}", Convert.ToDouble(dtResult.Rows[index]["sales_total_amount"].ToString())) + "</td></tr>");

                                                if (!string.IsNullOrEmpty(dtResult.Rows[index]["condiment"].ToString()))
                                                {
                                                    builder.Append("<tr><td>- " + dtResult.Rows[index]["condiment"].ToString() + "</td><td></td><td style='text-align:right;'></td></tr>");
                                                }
                                                group_total += Convert.ToDouble(dtResult.Rows[index]["sales_total_amount"].ToString());
                                                sub_total += Convert.ToDouble(dtResult.Rows[index]["sales_total_amount"].ToString());
                                                sub_total_tax += Convert.ToDouble(dtResult.Rows[index]["tax"].ToString());
                                            }
                                        }


                                        if (group_total != 0)
                                        {
                                            builder.Append("<tr><td width='70%' style='border-top:1px dashed #000000'><b>Sub Total</b></td>");
                                            builder.Append("<td width='5%' style='border-top:1px dashed #000000'></td><td width='25%' style='text-align:right; border-top:1px dashed #000000'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>");
                                        }

                                        builder.Append("<tr><td>&nbsp;</td></tr>");
                                        builder.Append("</table></td></tr>");
                                        builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;'>");
                                        builder.Append("<table width='100%'>");

                                        if (Convert.ToDouble(dtS.Rows[sales]["delivery_charges"]) > 0)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Delivery Charges</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(dtS.Rows[sales]["delivery_charges"])) + "</b></td></tr>");
                                            builder.Append("<tr><td width='70%'><b>Total</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Convert.ToDouble(dtS.Rows[sales]["delivery_charges"])) + "</b></td></tr>");
                                        }
                                        else
                                        {
                                            builder.Append("<tr><td width='70%'><b>Total</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>");
                                            builder.Append("<tr><td>&nbsp;</td></tr>");
                                        }

                                        if (Convert.ToDouble(dtS.Rows[sales]["balance"]) != 0)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Balance</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(dtS.Rows[sales]["balance"])) + "</b></td></tr>");
                                        }

                                        if (Convert.ToDouble(dtS.Rows[sales]["TIP_AMOUNT"]) != 0)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Tip Amount</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(dtS.Rows[sales]["TIP_AMOUNT"])) + "</b></td></tr>");
                                        }

                                        if (Convert.ToDouble(dtS.Rows[sales]["surcharge_amount"]) != 0)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Surcharge Amount</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(dtS.Rows[sales]["surcharge_amount"])) + "</b></td></tr>");
                                        }

                                        for (int p = 0; p < dtS.Rows.Count; p++)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(dtS.Rows[p]["payment_amount"]) + Convert.ToDouble(dtS.Rows[p]["change"])) + "</b></td></tr>");

                                            change += Convert.ToDouble(dtS.Rows[p]["change"]);
                                        }

                                        if (Convert.ToDouble(change) > 0)
                                        {
                                            builder.Append("<tr><td width='70%'><b>Change</b></td>");
                                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Convert.ToDouble(change)) + "</b></td></tr>");
                                        }

                                        builder.Append("<tr><td>&nbsp;</td></tr></table></td></tr>");

                                        builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>");
                                        builder.Append("<table width='100%' style='font-family:verdana;font-size:12px;'>");

                                        builder.Append("<tr><td width='70%'><b>Tax Information</b></td><td width='5%'></td><td width='25%'></td></tr>");

                                        builder.Append("<tr><td>&nbsp;</td></tr>");

                                        builder.Append("<tr><td width='70%'>VAT Total Sales</td><td width='5%'></td>");
                                        builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>");

                                        builder.Append("<tr><td width='70%'>VAT Tax Collection</td><td width='5%'></td>");
                                        builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total_tax) + "</b></td></tr>");

                                        builder.Append("<tr><td>&nbsp; </td></tr>");

                                        builder.Append("</table></td></tr>");

                                        if (!string.IsNullOrEmpty(dtS.Rows[sales]["Integrated_Terminal_ID"].ToString()) &&
                                            !string.IsNullOrEmpty(dtS.Rows[sales]["Integrated_Merchant_ID"].ToString()) &&
                                            !string.IsNullOrEmpty(dtS.Rows[sales]["Integrated_SaleType"].ToString()) &&
                                            !string.IsNullOrEmpty(dtS.Rows[sales]["Integrated_Entry_Method"].ToString()))
                                        {
                                            builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;'> <table width='100%' style='font-size: small;'>");

                                            builder.Append("<tr><td colspan='3' style='font-weight: bold; border-bottom:1px dashed #000000; text-align : center;'>CARDHOLDER COPY</td></tr>");

                                            builder.Append("<tr><td width='15%'>Terminal ID</td>");
                                            builder.Append("<td width='5%'>:</td>");
                                            builder.Append("<td>" + dtS.Rows[sales]["Integrated_Terminal_ID"].ToString() + "</td></tr>");

                                            builder.Append("<tr><td width='15%'>Merchant ID</td>");
                                            builder.Append("<td width='5%'>:</td>");
                                            builder.Append("<td>" + dtS.Rows[sales]["Integrated_Merchant_ID"].ToString() + "</td></tr>");

                                            builder.Append("<tr><td width='15%'>Sale Type</td>");
                                            builder.Append("<td width='5%'>:</td>");
                                            builder.Append("<td>" + dtS.Rows[sales]["Integrated_SaleType"].ToString() + "</td></tr>");

                                            builder.Append("<tr><td width='15%'>Entry Method</td>");
                                            builder.Append("<td width='5%'>:</td>");
                                            builder.Append("<td>" + dtS.Rows[sales]["Integrated_Entry_Method"].ToString() + "</td></tr>");

                                            builder.Append("<tr><td>&nbsp; </td></tr>");

                                            builder.Append("<tr><td colspan='3' style='text-align:center;'>Please debit account as shown</td></tr>");
                                            builder.Append("<tr><td colspan='3' style='text-align:center;'>Please retain for your records</td></tr>");
                                           
                                            builder.Append("<tr><td>&nbsp; </td></tr>");

                                            builder.Append("</table></td></tr>");
                                             
                                        }

                                        // LogHelper.Error("Table_transaction:Check 7");
                                        
                                        builder.Append("<tr style='height: 20px;'><td colspan ='2' style='text-align:center;'><b>Thanks For visiting us</b></td></tr><tr><td>&nbsp;</td></tr></table>");
                                        builder.Append(" <br /><br />");
                                        builder.Append("<div style='width:100%; height:56px; background-color: #000000; text-align: right; color: white;'>");
                                      //  builder.Append("<br /><br />");
                                        builder.Append("<span style='display: block; margin-bottom: 3px; margin-right:7px; color: white;'>Powered by</span>");
                                        builder.Append("<img width='144.4px;' style='float:right;'  height='50px' src ='http://testing.mytenderpos.com/Images/bg/logo.png' alt='footer'/>");
                                        builder.Append("</div>");

                                        builder.Append("</div>");
                                        builder.Append("</body></html>");
                                         
                                        ViewData["ReceiptContent"] = builder.ToString();
                                        ViewData["TotalAmount"] = amount;
                                    ViewData["ShowPrintButton"] = true;
                                }
                            }
                         
                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["ReceiptMessage"] = "An error occurred: " + ex.Message;
            }
        }




        public void CheckStore(string store)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");
                using (SqlConnection strcon = new SqlConnection(connectionString))
                {
                    strcon.Open();
                    string n = store;

                    SqlCommand cmd = new SqlCommand("Get_DB_Connection_Details", strcon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db_name", n);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    cmd.ExecuteNonQuery();

                    if (dt.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("db_server", dt.Rows[0]["db_server"].ToString());
                        HttpContext.Session.SetString("user_name", dt.Rows[0]["db_username"].ToString());
                        HttpContext.Session.SetString("password", Decode_data(dt.Rows[0]["db_password"].ToString()));
                        HttpContext.Session.SetString("db_name", dt.Rows[0]["db_name"].ToString());
                        HttpContext.Session.SetString("ConnectionString", "Data Source=" + dt.Rows[0]["db_server"] + ";Initial Catalog=" + dt.Rows[0]["db_name"] + ";User ID=" + dt.Rows[0]["db_username"] + ";Password=" + HttpContext.Session.GetString("password") + ";");
                    }

                    strcon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
                //LogHelper.Error("Scheduler:" + System.DateTime.Now.ToString() + ": " + ex.Message);
            }
        }

        public string Decode_data(string str)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(str));
            }
            catch (Exception ex)
            {
                return "";
            }



            //public void OnGet()
            //{








            //}



        }
    }
}