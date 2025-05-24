using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
 
using Microsoft.AspNetCore.Mvc.RazorPages;
 

using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace Converted_POS
{
    public class LocationController :Controller
    {

        private readonly IConfiguration _configuration;

        public LocationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<clsLocation> SelectAll(int? c_id, string conn)
        {
            List<clsLocation> list = new List<clsLocation>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsLocation location = new clsLocation();
                    
                    location.location_id = Convert.ToInt32(rdr["location_id"]);
                    location.name = rdr["name"].ToString();
                    location.code = rdr["code"].ToString();
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        location.is_active = true;
                    }
                    else
                    {
                        location.is_active = false;
                    }
                    location.City_Name = rdr["cname"].ToString();
                    location.venue_name = rdr["venue_name"].ToString();
                    if (rdr["till_auto_log_off"].ToString() == "Yes")
                    {
                        location.till_auto_log_off = true;
                    }
                    else
                    {
                        location.till_auto_log_off = false;
                    }
                   
                    list.Add(location);
                }
                con.Close();
            }
            return list;
        }
        public List<SelectListItem> GetVenue(int? c_id, string conn)
        {
            List<SelectListItem> Venue = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp_active", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Venue.Add(new SelectListItem
                    {
                        Value = rdr["venue_id"].ToString(),
                        Text = rdr["venue_name"].ToString()
                    });
                }

                con.Close();
            }

            return Venue;
        }
        public List<SelectListItem> GetCountry(int? c_id, string conn)
        {
            List<SelectListItem> country = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Countries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    country.Add(new SelectListItem
                    {
                        Value = rdr["Country_id"].ToString(),
                        Text = rdr["CountryName"].ToString()
                    });
                }

                con.Close();
            }

            return country;
        }
        public List<SelectListItem> GetState(int cmpId, string connStr, int? country_id)
        {
            List<SelectListItem> state = new List<SelectListItem>();

            if (!country_id.HasValue)
            {
                return state; // Return an empty list or handle accordingly
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_States", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@country_id", country_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    state.Add(new SelectListItem
                    {
                        Value = rdr["State_Id"].ToString(),
                        Text = rdr["StateName"].ToString()
                    });
                }
                con.Close();
            }
            return state;
        }


        public List<SelectListItem> GetCity(int cmpId, string connStr, int stateId)
        {
            List<SelectListItem> city = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_City", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@state_id", stateId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    city.Add(new SelectListItem
                    {
                        Value = rdr["City_id"].ToString(),
                        Text = rdr["CityName"].ToString()
                    });
                }

                con.Close();
            }

            return city;
        }

        public clsLocation Select(int cmpId, int locationId, string connStr, HttpContext httpContext)
        {
            clsLocation location = new clsLocation();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Location", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@cmp_id", cmpId);
                cmd.Parameters.AddWithValue("@location_id", locationId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {


                     
                    location = new clsLocation
                    {
                        location_id = Convert.ToInt32(rdr["location_id"]),
                        name = rdr["name"].ToString(),
                        code = rdr["code"].ToString(),
                        address = rdr["address"].ToString(),
                        country_id = Convert.ToInt32(rdr["Country_id"].ToString()),
                        state_Id = Convert.ToInt32(rdr["state_id"].ToString()),
                        city_ID = Convert.ToInt32(rdr["city_id"].ToString()),
                        venue_id = Convert.ToInt32(rdr["venue_id"].ToString()),
                        onlinepayment = rdr["onlinepayment"].ToString(),
                        theme = rdr["theme"].ToString(),
                        judo_id = rdr["judo_id"].ToString(),
                        judo_token = rdr["judo_token"].ToString(),
                        judo_secret = rdr["judo_secret"].ToString(),
                        Email = rdr["LoctionEmail"].ToString(),
                        LocationEmail = rdr["LoctionEmail"].ToString(),
                        LocationContact = rdr["LocationContact"].ToString(),
                        Contact = rdr["Contact"].ToString(),
                        min_amount = Convert.ToInt32(rdr["min_amount"]),
                        is_delivery = rdr["is_delivery"].ToString() == "Yes",
                        websales_check_schedule = rdr["websales_check_schedule"].ToString() == "Yes",
                        schedule_required = rdr["schedule_required"].ToString() == "Yes",
                        schedule_cnc = rdr["schedule_cnc"].ToString() == "Yes",
                        betweenSlot = rdr["HourlySlot"].ToString() == "Yes",
                        cashback = rdr["cashback"].ToString() == "Yes",
                        click_and_collect = rdr["click_and_collect"].ToString() == "Yes",
                        srv_charge_delivery = rdr["srv_charge_delivery"].ToString() == "Yes",
                        Order_table = rdr["Order_table"].ToString() == "Yes",
                        is_active = rdr["Active"].ToString() == "Yes",
                        is_email = rdr["is_email"].ToString() == "Yes",
                        Is_Email_APK = rdr["Is_Email_APK"].ToString() == "Yes",
                        is_skippay = rdr["is_skippay"].ToString() == "Yes",
                        till_auto_log_off = rdr["till_auto_log_off"].ToString() == "Yes",
                        is_GetCovers = rdr["is_GetCovers"].ToString() == "Yes",
                        delivery_charges = Convert.ToInt32(rdr["delivery_charges"]),
                        service_charges = Convert.ToInt32(rdr["service_charges"]),
                        BG_Color = rdr["BG_Color"].ToString(),
                        Font_Color = rdr["Font_Color"].ToString(),
                        Body_Color = rdr["Body_Color"].ToString(),
                        CustomPay_Id = rdr["CustomPay_Id"].ToString(),
                        CustomPay_Secret = rdr["CustomPay_Secret"].ToString(),
                        CustomPay_Token = rdr["CustomPay_Token"].ToString(),
                        CustomPay_Base64 = rdr["CustomPay_Base64"].ToString(),
                        start_date = rdr["start_date"].ToString(),
                        end_date = rdr["end_date"].ToString(),
                        CustomPay_url = rdr["CustomPay_url"].ToString(),
                        clientid = rdr["clientid"].ToString(),
                        clientsecret = rdr["clientsecret"].ToString(),
                        redirect_uri = rdr["redirect_uri"].ToString(),
                        future_booking_days = Convert.ToInt32(rdr["future_booking_days"]),
                        message_delivery = rdr["message_delivery"].ToString(),
                        message_order_table = rdr["message_order_table"].ToString(),
                        header_reciept = rdr["header_reciept"].ToString(),
                        TipAS = rdr["TipAS"].ToString(),
                        GC_Btn_img_typ = rdr["GC_Btn_img_typ"].ToString(),
                        GC_Btn_txt_clr = rdr["GC_Btn_txt_clr"].ToString(),
                        GC_Btn_stl = rdr["GC_Btn_stl"].ToString(),
                        GC_Btn_bkgd_clr = rdr["GC_Btn_bkgd_clr"].ToString(),
                        GC_Btn_fnt_size = rdr["GC_Btn_fnt_size"].ToString(),
                        GC_Btn_img_typ_pos = rdr["GC_Btn_img_typ_pos"].ToString(),
                        C_Btn_bkgd_clr = rdr["C_Btn_bkgd_clr"].ToString(),
                        C_Btn_txt_clr = rdr["C_Btn_txt_clr"].ToString(),
                        C_Btn_stl = rdr["C_Btn_stl"].ToString(),
                        C_Btn_img_typ = rdr["C_Btn_img_typ"].ToString(),
                        C_Btn_fnt_size = rdr["C_Btn_fnt_size"].ToString(),
                        C_Btn_img_typ_pos = rdr["C_Btn_img_typ_pos"].ToString(),
                        P_Btn_bkgd_clr = rdr["P_Btn_bkgd_clr"].ToString(),
                        P_Btn_txt_clr = rdr["P_Btn_txt_clr"].ToString(),
                        P_Btn_stl = rdr["P_Btn_stl"].ToString(),
                        P_Btn_img_typ = rdr["P_Btn_img_typ"].ToString(),
                        P_Btn_fnt_size = rdr["P_Btn_fnt_size"].ToString(),
                        P_Btn_img_typ_pos = rdr["P_Btn_img_typ_pos"].ToString(),
                        login_src_bkgd_clr = rdr["login_src_bkgd_clr"].ToString(),
                        Till_Phn_no = rdr["Till_Phn_no"].ToString(),
                        Till_url = rdr["Till_url"].ToString(),
                        login_src_login_btn = rdr["login_src_login_btn"].ToString(),
                      
                        POS_logo = rdr["POS_logo"] as byte[],
                        Click_Collect_image = rdr["Click_Collect_image"] as byte[],
                        Delivery_image = rdr["Delivery_image"] as byte[],
                        OrderAtTable_image = rdr["OrderAtTable_image"] as byte[],
                        L_image = rdr["L_image"] as byte[],
                        GraphicViewBackground = rdr["GraphicViewBackground"] as byte[],
                        GraphicViewTable = rdr["GraphicViewTable"] as byte[]

                         

                        //CashflowId = rdr["cashflow_id"].ToString(),
                        // CashflowUrl = rdr["cashflow_url"].ToString(),
                        //CashflowApiKey = rdr["cashflow_api_key"].ToString()
                    //     if (location != null)
                    //{
                    //    // Bind text areas
                    //    //editor.InnerText = location.message_order_table ?? "";
                    //    //editor1.InnerText = location.message_delivery ?? "";
                    //    //editor2.InnerText = location.message_order_table ?? "";

                    //    // Bind color inputs
                    //    BG_Color.Value = rdr["BG_Color"].ToString() ?? "#ffffff"; // Default to white if null
                    //    fontColor.Value = location.Font_Color ?? "#000000";     // Default to black if null
                    //    bodyColor.Value = location.Body_Color ?? "#ffffff";    // Default to white if null
                    //}


                };
                }
                con.Close();
            }
             
            return location;
        }
        public void Insert(clsLocation location, string conn, int? country_id, int? state_Id, int? city_ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Location", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", location.location_id);
                cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
                cmd.Parameters.AddWithValue("@code", location.code);
                cmd.Parameters.AddWithValue("@name", location.name);
                cmd.Parameters.AddWithValue("@address", location.address);
                cmd.Parameters.AddWithValue("@country_id",  country_id);
                cmd.Parameters.AddWithValue("@state_id",  state_Id);
                cmd.Parameters.AddWithValue("@city_id", city_ID);
                cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
                cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
                cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
                cmd.Parameters.AddWithValue("@is_active", location.is_active);
                cmd.Parameters.AddWithValue("@cashback", location.cashback);
                cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
                cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
                cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
                cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
                cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
                cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
                cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
                cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
                cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
                cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
                cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
                cmd.Parameters.AddWithValue("@is_email", location.is_email);
                cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
                
                cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
                cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
                cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
                cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
                cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);


                    string startTimeFormatted = !string.IsNullOrEmpty(location.start_date)? location.start_date : "00:00"; // Default to "00:00" if empty or null

                    string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";  

                    cmd.Parameters.AddWithValue("@start_date",startTimeFormatted);
                cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
                cmd.Parameters.AddWithValue("@Email", location.Email);
                cmd.Parameters.AddWithValue("@Contact", location.Contact);
                    //string selectedColor = Request.Form["txt_clr_GC"];
                    //if (string.IsNullOrEmpty(selectedColor))
                    //{
                    //    // Handle missing color (e.g., use a default color)
                    //    selectedColor = "#000000"; // Default black
                    //}
                    cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);
                    if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
                    {
                        location.BG_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
                    {
                        location.Font_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
                    {
                        location.Body_Color = "#0000FF";
                    }
                    cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
                cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
                cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
                cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
                cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
                cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
                cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
                cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

                // Add optional parameters
                

                //if (location.OrderAtTable_image != null)
                //{
                //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
                //}

                //if (location.L_image != null)
                //{
                //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
                //}

                cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
                cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
                //cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
                cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
                cmd.Parameters.AddWithValue("@theme", location.theme);
                cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

                //if (location.GraphicViewBackground != null)
                //{
                //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
                //}

                //if (location.GraphicViewTable != null)
                //{
                //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
                //}

                cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
                cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
                cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
                cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
                cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
                cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
                cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
                cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

                cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
                cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
                cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
                cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
                cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
                cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
                cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
                cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
                cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

                cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
                cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
                cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
                cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
               // cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

                cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
                cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
                cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
                cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
                cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

                cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
                cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
                cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
                cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
                cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
                //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

                cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
                cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
                cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
                //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

                cmd.Parameters.AddWithValue("@login_src_bkgd_clr", location.login_src_bkgd_clr);
                cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
                cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);               //////////////////////////////////wanna add this fields 
                cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);

                //if (location.POS_logo != null)
                //{
                //    cmd.Parameters.AddWithValue("@POS_logo", location.POS_logo);
                //}

                    cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;
                    
                    //if (location.Click_Collect_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
                    //}

                    //if (location.Delivery_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
                    //}

                    cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;


                    cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;
                    
                    cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;
                    
                    cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;
                    
                    cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;
                    
                    cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;
                    
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public void Update(clsLocation location, string conn, int? country_id, int? state_Id, int? city_ID,int location_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Location", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", location.location_id);
                    cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
                    cmd.Parameters.AddWithValue("@code", location.code);
                    cmd.Parameters.AddWithValue("@name", location.name);
                    cmd.Parameters.AddWithValue("@address", location.address);
                    cmd.Parameters.AddWithValue("@country_id", country_id);
                    cmd.Parameters.AddWithValue("@state_id", state_Id);
                    cmd.Parameters.AddWithValue("@city_id", city_ID);
                    cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
                    cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                    //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                    cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
                    cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
                    cmd.Parameters.AddWithValue("@is_active", location.is_active);
                    cmd.Parameters.AddWithValue("@cashback", location.cashback);
                    cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
                    cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
                    cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
                    cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
                    cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
                    cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
                    cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
                    cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
                    cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
                    cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
                    cmd.Parameters.AddWithValue("@is_email", location.is_email);

                    cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
                    cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
                    cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
                    cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
                    cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);
                    string startTimeFormatted = !string.IsNullOrEmpty(location.start_date) ? location.start_date : "00:00"; // Default to "00:00" if empty or null

                    string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";

                    cmd.Parameters.AddWithValue("@start_date", startTimeFormatted);
                    cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
                    cmd.Parameters.AddWithValue("@Email", location.Email);
                    cmd.Parameters.AddWithValue("@Contact", location.Contact);


                    if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
                    {
                        location.BG_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
                    {
                        location.Font_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
                    {
                        location.Body_Color = "#0000FF";
                    }
                    cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
                    cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
                    cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
                    cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
                    cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
                    cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
                    cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
                    cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

                    // Add optional parameters
                    //if (location.Click_Collect_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
                    //}

                    //if (location.Delivery_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
                    //}

                    //if (location.OrderAtTable_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
                    //}

                    //if (location.L_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
                    //}

                    cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
                    cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
                    cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
                    cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
                    cmd.Parameters.AddWithValue("@theme", location.theme);
                    cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

                    //if (location.GraphicViewBackground != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
                    //}

                    //if (location.GraphicViewTable != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
                    //}

                    cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
                    cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
                    cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
                    cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
                    cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
                    cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
                    cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
                    cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

                    cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
                    cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
                    cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
                    cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
                    cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
                    cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
                    cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
                    cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
                    cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

                    cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
                    cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

                    cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
                    cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

                    cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
                    cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
                    //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

                    cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
                    cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
                    cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
                    //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

                    cmd.Parameters.AddWithValue("@login_src_bkgd_clr", "");   //////////////////////////////////wanna add this fields 
                    cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
                    cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);
                    cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);
                    cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;
                    //if (Request?.Form != null && Request?.Form.Files != null)
                    //{
                    //    // For Click and Collect image
                    //    if (Request.Form.Files["CACFile"] != null && Request.Form.Files["CACFile"].Length > 0)
                    //    {
                    //        var file = Request.Form.Files["CACFile"];
                    //        using (var ms = new MemoryStream())
                    //        {
                    //            file.CopyTo(ms);
                    //            location.Click_Collect_image = ms.ToArray(); // Save the new file
                    //        }
                    //    }
                    //    else if (location.Click_Collect_image == null)
                    //    {
                    //        // Retain the old image if no new image is uploaded
                    //        var base64Image = ViewData["Base64CACImage"]?.ToString();
                    //        if (!string.IsNullOrEmpty(base64Image))
                    //        {
                    //            location.Click_Collect_image = Convert.FromBase64String(base64Image);
                    //        }
                    //    }

                    //    // Repeat similar code for other image fields
                    //    if (Request.Form.Files["DeliveryimageFile"] != null && Request.Form.Files["DeliveryimageFile"].Length > 0)
                    //    {
                    //        var file = Request.Form.Files["DeliveryimageFile"];
                    //        using (var ms = new MemoryStream())
                    //        {
                    //            file.CopyTo(ms);
                    //            location.Delivery_image = ms.ToArray();
                    //        }
                    //    }
                    //    else if (location.Delivery_image == null)
                    //    {
                    //        var base64Image = ViewData["Base64DeliveryImage"]?.ToString();
                    //        if (!string.IsNullOrEmpty(base64Image))
                    //        {
                    //            location.Delivery_image = Convert.FromBase64String(base64Image);
                    //        }
                    //    }

                    //    // Repeat for other images like Order At Table image, ULD image, etc.
                    //}





                    cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;

                    cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;

                    cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;

                    cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;

                    cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;
                    cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public void Delete(clsLocation location, string connStr, string ip_address)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Location", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", location.location_id);
                    cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
                    cmd.Parameters.AddWithValue("@code", location.code);
                    cmd.Parameters.AddWithValue("@name", location.name);
                    cmd.Parameters.AddWithValue("@address", location.address);
                    cmd.Parameters.AddWithValue("@country_id", location.country_id);
                    cmd.Parameters.AddWithValue("@state_id", location.state_Id);
                    cmd.Parameters.AddWithValue("@city_id", location.city_ID);
                    cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
                    cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                    //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
                    cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
                    cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
                    cmd.Parameters.AddWithValue("@is_active", location.is_active);
                    cmd.Parameters.AddWithValue("@cashback", location.cashback);
                    cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
                    cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
                    cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
                    cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
                    cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
                    cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
                    cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
                    cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
                    cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
                    cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
                    cmd.Parameters.AddWithValue("@is_email", location.is_email);

                    cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
                    cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
                    cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
                    cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
                    cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);
                    string startTimeFormatted = !string.IsNullOrEmpty(location.start_date) ? location.start_date : "00:00"; // Default to "00:00" if empty or null

                    string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";

                    cmd.Parameters.AddWithValue("@start_date", startTimeFormatted);
                    cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
                    cmd.Parameters.AddWithValue("@Email", location.Email);
                    cmd.Parameters.AddWithValue("@Contact", location.Contact);


                    if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
                    {
                        location.BG_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
                    {
                        location.Font_Color = "#0000FF";
                    }

                    if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
                    {
                        location.Body_Color = "#0000FF";
                    }
                    cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
                    cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
                    cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
                    cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
                    cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
                    cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
                    cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
                    cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

                    // Add optional parameters
                    //if (location.Click_Collect_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
                    //}

                    //if (location.Delivery_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
                    //}

                    //if (location.OrderAtTable_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
                    //}

                    //if (location.L_image != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
                    //}

                    cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
                    cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
                    cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
                    cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
                    cmd.Parameters.AddWithValue("@theme", location.theme);
                    cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

                    //if (location.GraphicViewBackground != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
                    //}

                    //if (location.GraphicViewTable != null)
                    //{
                    //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
                    //}

                    cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
                    cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
                    cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
                    cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
                    cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
                    cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
                    cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
                    cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

                    cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
                    cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
                    cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
                    cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
                    cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
                    cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
                    cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
                    cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
                    cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

                    cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
                    cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

                    cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
                    cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

                    cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
                    cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
                    cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
                    cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
                    cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
                    //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

                    cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
                    cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
                    cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
                    //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

                    cmd.Parameters.AddWithValue("@login_src_bkgd_clr", "");   //////////////////////////////////wanna add this fields 
                    cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
                    cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);
                    cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);
                    cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;
                    //if (Request?.Form != null && Request?.Form.Files != null)
                    //{
                    //    // For Click and Collect image
                    //    if (Request.Form.Files["CACFile"] != null && Request.Form.Files["CACFile"].Length > 0)
                    //    {
                    //        var file = Request.Form.Files["CACFile"];
                    //        using (var ms = new MemoryStream())
                    //        {
                    //            file.CopyTo(ms);
                    //            location.Click_Collect_image = ms.ToArray(); // Save the new file
                    //        }
                    //    }
                    //    else if (location.Click_Collect_image == null)
                    //    {
                    //        // Retain the old image if no new image is uploaded
                    //        var base64Image = ViewData["Base64CACImage"]?.ToString();
                    //        if (!string.IsNullOrEmpty(base64Image))
                    //        {
                    //            location.Click_Collect_image = Convert.FromBase64String(base64Image);
                    //        }
                    //    }

                    //    // Repeat similar code for other image fields
                    //    if (Request.Form.Files["DeliveryimageFile"] != null && Request.Form.Files["DeliveryimageFile"].Length > 0)
                    //    {
                    //        var file = Request.Form.Files["DeliveryimageFile"];
                    //        using (var ms = new MemoryStream())
                    //        {
                    //            file.CopyTo(ms);
                    //            location.Delivery_image = ms.ToArray();
                    //        }
                    //    }
                    //    else if (location.Delivery_image == null)
                    //    {
                    //        var base64Image = ViewData["Base64DeliveryImage"]?.ToString();
                    //        if (!string.IsNullOrEmpty(base64Image))
                    //        {
                    //            location.Delivery_image = Convert.FromBase64String(base64Image);
                    //        }
                    //    }

                    //    // Repeat for other images like Order At Table image, ULD image, etc.
                    //}





                    cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;

                    cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;

                    cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;

                    cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;

                    cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;
                    cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                    cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }


    }
}
