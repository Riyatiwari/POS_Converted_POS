using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Converted_POS.Models;
using System.Linq;


namespace Converted_POS.Pages.BackOffice
{
    public partial class Switch_Account : PageModel
    {
        [BindProperty]
        public List<UserEntry> UserEntries { get; set; }

        ClsDataccess oClsDataccess = new ClsDataccess();
        private readonly IConfiguration _configuration;
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Method == "GET")
            {
                OnGet();
            }
        }

        public Switch_Account(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class UserEntry
        {
            public string store_name { get; set; }
            public string store_UUID { get; set; }
            public string Username { get; set; }
        }
        public IActionResult YourActionMethod()
        {
            var viewModel = new clsSwitchAccount();
            viewModel.UserEntries = GetUsers();
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

        private List<UserEntry> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            UserEntries = GetUsers(Username);
            List<string> storeUUIDs = switchAccount_Click(Username);

            string storeUUIDsJson = JsonConvert.SerializeObject(storeUUIDs);
            HttpContext.Session.SetString("StoreUUIDs", storeUUIDsJson);
        }


        public List<string> switchAccount_Click(string Username)
        {
            List<string> storeUUIDs = new List<string>();
            try
            {
                string userEntriesJson = HttpContext.Session.GetString("UserEntries");
                List<UserEntry> userEntries = JsonConvert.DeserializeObject<List<UserEntry>>(userEntriesJson);

                foreach (var entry in userEntries)
                {
                    storeUUIDs.Add(entry.store_UUID);
                }
                List<string> storeUUID = userEntries.Select(entry => entry.store_UUID).ToList();

                //string storeUUID = HttpContext.Session.GetString("storeUUID_forswitch");
                string username = HttpContext.Session.GetString("Username");
                string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdd = new SqlCommand("Get_DB_Connection_switchAccount", con);
                    cmdd.CommandType = CommandType.StoredProcedure;
                    cmdd.Parameters.AddWithValue("@Username", username);
                    //string storeUUIDsString = string.Join(",", storeUUIDs);
                    //cmdd.Parameters.AddWithValue("@Store_UUID", storeUUID);
                    //cmdd.Parameters.AddWithValue("@Store_UUID", userEntriesJson);
                    //cmdd.Parameters.AddWithValue("@Store_UUID", HttpContext.Session.SetString("Store_UUID"));
                    //cmdd.Parameters.AddWithValue("@Store_UUID", HttpContext.Session.GetString("storeuuid_forswitch"));
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("db_server", dt.Rows[0]["db_server"].ToString());
                        HttpContext.Session.SetString("StoreId", dt.Rows[0]["Store_id"].ToString());
                        HttpContext.Session.SetString("user_name", dt.Rows[0]["db_username"].ToString());
                        HttpContext.Session.SetString("password", Decode_data(dt.Rows[0]["db_password"].ToString()));
                        HttpContext.Session.SetString("db_name", dt.Rows[0]["db_name"].ToString());
                        HttpContext.Session.SetString("conStoreuuid", dt.Rows[0]["store_uuid"].ToString());
                    }
                    else
                    {
                        //return Page();
                    }
                    string db_ser = HttpContext.Session.GetString("db_server").ToString();
                    string db_name = HttpContext.Session.GetString("db_name").ToString();
                    string db_psw = HttpContext.Session.GetString("password").ToString();
                    string db_uName = HttpContext.Session.GetString("user_name").ToString();

                    string conString = "Data Source=" + db_ser + ";Initial Catalog=" + db_name + ";User ID=" + db_uName + ";Password=" + db_psw + ";";

                    HttpContext.Session.SetString("conString", conString.ToString());

                    using (SqlConnection con1 = new SqlConnection(conString))
                    {
                        SqlCommand cmd = new SqlCommand("Get_Login", con1);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", Username);
                        //  cmd.Parameters.AddWithValue("@Password", Encrypt(signIn.Password));

                        con1.Open();
                        SqlDataAdapter adpp = new SqlDataAdapter(cmd);
                        DataTable dtt = new DataTable();
                        adpp.Fill(dtt);
                        cmd.ExecuteNonQuery();
                        con1.Close();
                        if (dtt.Rows.Count > 0)
                        {
                            HttpContext.Session.SetString("cmp_id", dtt.Rows[0]["Company_id"].ToString());
                            HttpContext.Session.SetString("cmp_name", dtt.Rows[0]["Name"].ToString());
                            HttpContext.Session.SetString("Login_id", dtt.Rows[0]["Login_id"].ToString());
                            HttpContext.Session.SetString("staff_role_id", dtt.Rows[0]["Staff_id"].ToString());
                            //HttpContext.Session.SetString("store", signIn.StoreName);
                            HttpContext.Session.SetString("show_chips", dtt.Rows[0]["Show_chips"].ToString());
                            HttpContext.Session.SetString("IsAddTax2", dtt.Rows[0]["IsAddTax2"].ToString());
                            HttpContext.Session.SetString("IsExclusiveTax", dtt.Rows[0]["IsExclusiveTax"].ToString());
                            HttpContext.Session.SetString("StoreUUID", dtt.Rows[0]["Store_uuid"].ToString());
                            HttpContext.Session.SetString("Storename", dtt.Rows[0]["C_Name"].ToString());
                            HttpContext.Session.SetString("Username", dtt.Rows[0]["email"].ToString());
                            //getList();

                            //return RedirectToPage("/BackOffice/Dashboard");

                        }
                        else
                        {
                            //return Page();
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserEntry entry = new UserEntry();
                                entry.store_name = reader["store_name"].ToString();
                                entry.store_UUID = reader["Store_UUID"].ToString();
                                //Session["storename_forswitch"] = entry.store_name;
                                //entry.storeuuid = reader["store_uuid"].ToString();
                                userEntries.Add(entry);
                                HttpContext.Session.SetString("storeUUID_forswitch" + userEntries.Count.ToString(), entry.store_UUID);
                                HttpContext.Session.SetString("store_name" + userEntries.Count.ToString(), entry.store_name);
                            }
                        }
                        HttpContext.Session.SetString("UserEntries", JsonConvert.SerializeObject(userEntries));
                    }
                }
                return storeUUIDs;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }

        }

        private string Decode_data(string v)
        {
            try
            {
                return Encoding.UTF8.GetString(System.Convert.FromBase64String(v));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult OnPost(string switchAccount, string Username)
        {
            string storeUUID = Request.Form["storeUUID"];
            string storeName = Request.Form["storeName"];

            HttpContext.Session.SetString("SelectedStoreUUID", storeUUID);
            HttpContext.Session.SetString("SelectedStoreName", storeName);
            string db_ser = HttpContext.Session.GetString("db_server").ToString();
            string db_name = HttpContext.Session.GetString("db_name").ToString();
            string db_psw = HttpContext.Session.GetString("password").ToString();
            string db_uName = HttpContext.Session.GetString("user_name").ToString();
            //switchAccount_Click(storeUUID);
            string conString = "Data Source=" + db_ser + ";Initial Catalog=" + storeName + ";User ID=" + db_uName + ";Password=" + db_psw + ";";

            HttpContext.Session.SetString("conString", conString.ToString());

            using (SqlConnection con1 = new SqlConnection(conString))
            {
                string username = HttpContext.Session.GetString("Username");
                SqlCommand cmd = new SqlCommand("Get_Login", con1);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                //  cmd.Parameters.AddWithValue("@Password", Encrypt(signIn.Password));

                con1.Open();
                SqlDataAdapter adpp = new SqlDataAdapter(cmd);
                DataTable dtt = new DataTable();
                adpp.Fill(dtt);
                cmd.ExecuteNonQuery();
                con1.Close();
                if (dtt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("cmp_id", dtt.Rows[0]["Company_id"].ToString());
                    HttpContext.Session.SetString("cmp_name", dtt.Rows[0]["Name"].ToString());
                    HttpContext.Session.SetString("Login_id", dtt.Rows[0]["Login_id"].ToString());
                    HttpContext.Session.SetString("staff_role_id", dtt.Rows[0]["Staff_id"].ToString());
                    //HttpContext.Session.SetString("store", signIn.StoreName);
                    HttpContext.Session.SetString("show_chips", dtt.Rows[0]["Show_chips"].ToString());
                    HttpContext.Session.SetString("IsAddTax2", dtt.Rows[0]["IsAddTax2"].ToString());
                    HttpContext.Session.SetString("IsExclusiveTax", dtt.Rows[0]["IsExclusiveTax"].ToString());
                    HttpContext.Session.SetString("StoreUUID", dtt.Rows[0]["Store_uuid"].ToString());
                    HttpContext.Session.SetString("Storename", dtt.Rows[0]["C_Name"].ToString());
                    HttpContext.Session.SetString("Username", dtt.Rows[0]["email"].ToString());
                    //getList();
                }

                string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdd = new SqlCommand("Get_DB_Connection_switchAccount", con);
                    cmdd.CommandType = CommandType.StoredProcedure;
                    cmdd.Parameters.AddWithValue("@Username", username);

                    cmdd.Parameters.AddWithValue("@Store_UUID", storeUUID);
                    //cmdd.Parameters.AddWithValue("@Store_UUID", userEntriesJson);
                    //cmdd.Parameters.AddWithValue("@Store_UUID", HttpContext.Session.SetString("Store_UUID"));
                    //cmdd.Parameters.AddWithValue("@Store_UUID", HttpContext.Session.GetString("storeuuid_forswitch"));
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    //string conString = "Data Source=" + db_ser + ";Initial Catalog=" + storeName + ";User ID=" + db_uName + ";Password=" + db_psw + ";";
                }
                    return RedirectToPage("/Dashboard");

            }
        }

        private List<UserEntry> GetUsers(string Username)
        {
            List<UserEntry> userEntries = new List<UserEntry>();
            string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");

            using (SqlConnection strcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserEntriesInfo", strcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", Username);

                    strcon.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserEntry entry = new UserEntry();
                            entry.store_name = reader["store_name"].ToString();
                            entry.store_UUID = reader["Store_UUID"].ToString();
                            //Session["storename_forswitch"] = entry.store_name;
                            //entry.storeuuid = reader["store_uuid"].ToString();
                            userEntries.Add(entry);
                            HttpContext.Session.SetString("storeUUID_forswitch" + userEntries.Count.ToString(), entry.store_UUID);
                            HttpContext.Session.SetString("store_name" + userEntries.Count.ToString(), entry.store_name);
                        }
                    }
                }
            }
            HttpContext.Session.SetString("UserEntries", JsonConvert.SerializeObject(userEntries));
            return userEntries;
        }

        private void BOokingLogin()
        {
            //try
            //{
            //    SqlConnection conn = new SqlConnection("ConnectionStrings");
            //    string que = "Select * from Operator Where OpNumber = '" + HttpContext.Session.GetString("UserEmail") + "'";
            //    DataSet ds = conn.SelectData(que);
            //    if (ds.Tables[0].Rows.Count == 1)
            //    {
            //        DataRow dr = ds.Tables[0].Rows[0];
            //        HttpContext.Session.SetInt32("UserID", int.Parse(dr["OperatorID"].ToString()));
            //        HttpContext.Session.SetString("UserName", dr["FirstName"].ToString() + " " + dr["LastName"].ToString());
            //        HttpContext.Session.SetInt32("Login", 1);
            //        int venueId = int.Parse(dr["AllowedVenueID"].ToString());

            //        //int venueId = Utils.getInteger(dr["AllowedVenueID"].ToString());
            //        if (venueId == 0)
            //        {
            //            HttpContext.Session.SetInt32("BookingType", 3);
            //        }
            //        else
            //        {
            //            //que = " SELECT BookingSettingsid, bookingtype, BS.StoreID FROM bookingsettings BS"
            //            //que += " INNER JOIN Store S ON S.StoreID = BS.StoreID"
            //            //que += " WHERE S.VenueId = '" + venueId + "'";

            //            ds = conn.SelectData(que);

            //            foreach (DataRow item in ds.Tables[0].Rows)
            //            {
            //                // Assuming DecryptBookingType is another decryption method
            //                item["bookingtype"] = DecryptBookingType(item["bookingtype"].ToString());

            //                //item["bookingtype"] = Utils.Decrypt(item["bookingtype"].ToString());
            //            }
            //            int maxVal = Convert.ToInt32(ds.Tables[0].Compute("Max(bookingtype)", ""));

            //            //int maxVal = Utils.getInteger(ds.Tables[0].Compute("Max(bookingtype)", ""));

            //            HttpContext.Session.SetInt32("BookingType", maxVal);
            //        }

            //        DataTable dtTab = oClsDataccess.Getdatatable("SELECT * FROM TabMaster");

            //        if (dtTab.Rows.Count > 0)
            //        {
            //            HttpContext.Session.SetInt32("TabID", 1);
            //        }
            //        else
            //        {
            //            HttpContext.Session.SetInt32("TabID", 0);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private object DecryptBookingType(string v)
        {
            throw new NotImplementedException();
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                string email;
                string Subject;
                if (HttpContext.Session.GetString("dt1") != null)
                {
                     
                    DataTable dt = new DataTable();  
                    string json = JsonConvert.SerializeObject(dt);
                    HttpContext.Session.SetString("dt1", json);

                    // Retrieving DataTable from Session
                    string jsonData = HttpContext.Session.GetString("dt1");
                    DataTable retrievedDt = null;
                    if (jsonData != null)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(jsonData);
                        using (var stream = new MemoryStream(data))
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                using (var jsonReader = new JsonTextReader(reader))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    retrievedDt = serializer.Deserialize<DataTable>(jsonReader);
                                }
                            }
                        }
                    }
                    builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>");
                    builder.Append("<div style='width:70%; color:#000000; font-family:verdana;'>");

                    builder.Append("<table style='width:100%;height:150px; color:#000000;margin:5px;font-family:verdana;'>");
                    builder.Append("<tr><td>");
                    builder.Append("Hello Support Team, ");
                    builder.Append("</td></tr>");
                    builder.Append("<tr><td></td></tr>");
                    builder.Append("<tr><td>");
                    builder.Append("Sync Issue");
                    builder.Append("</td></tr>");
                    builder.Append("<tr><td></td></tr>");
                    builder.Append("<tr><td><table border=1 width='80%' style='font-family:verdana;font-size:13px;border-collapse: collapse;'>");
                    builder.Append("<tr><td width='30%'><b>Issue Date</b></td> <td><b>Description</b></td></tr>");

                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        builder.Append("<tr><td width='40%'>" + dt.Rows[index]["for_date"].ToString() + "</td>");
                        builder.Append("<td>" + dt.Rows[index]["sync_desc"].ToString() + "</td></tr>");
                    }

                    builder.Append("</table></td></tr> ");
                    builder.Append("<tr><td></td></tr>");
                    builder.Append("<tr style='height: 20px;'><td> <b>Thanks, </b> <br/> TenderPOS. </td></tr>");

                    builder.Append("</table>");

                    builder.Append("</div>");
                    builder.Append("</body> </html>");

                    //email = "developer@technometrics.in";
                    email = "support@tenderpos.com";
                    //madhvanimitesh@gmail.com , mitesh.m@technometrics.in
                    //Subject = Session["cmp_name"].ToString() + "Record Sync Status";
                    Subject = HttpContext.Session.GetString("cmp_name") + "Record Sync Status";
                    MailTo_receipt(Convert.ToInt32(HttpContext.Session.GetInt32("cmp_id")), Convert.ToInt32(0), email, Subject, builder.ToString(), "", "mitesh@tenderpos.com", "");

                    //MailTo_receipt(Convert.ToInt32(HttpContext.Session.GetInt32("cmp_id")), Convert.ToInt32(0), email, Subject, builder.ToString(), "", "mitesh@tenderpos.com", "");
                    //MailTo_receipt(Convert.ToInt32(Session["cmp_id"]), Convert.ToInt32(0), email, Subject, builder.ToString(), "", "mitesh@tenderpos.com", "");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Mail sent successfully.');", true);

                    HttpContext.Session.Remove("dt1");
                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void MailTo_receipt(int v1, int v2, string email, string subject, string v3, string v4, string v5, string v6)
        {
            throw new NotImplementedException();
        }

        private class ScriptManager
        {
            internal static void RegisterStartupScript(Switch_Account switch_Account, Type type, string v1, string v2, bool v3)
            {
                throw new NotImplementedException();
            }
        }

        private class ClsDataccess
        {
            internal DataTable Getdatatable(string v)
            {
                throw new NotImplementedException();
            }
        }
    }
}
