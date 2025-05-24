using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : Controller
    {

        private readonly IConfiguration _configuration;

        public FunctionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        DataTable dt = new DataTable();
        public IEnumerable<clsFunction> SelectAll(int? c_id, string conn)
        { 
            List<clsFunction> list = new List<clsFunction>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_MainFunction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsFunction Function = new clsFunction();

                    Function.mainfunction_id = Convert.ToInt32(rdr["mainfunction_id"]);
                    ViewData["mainfunction_id"] = Function.mainfunction_id;
                    Function.name = rdr["name"].ToString();
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        Function.is_active = true;
                    }
                    else
                    {
                        Function.is_active = false;
                    }
                    //Function.is_active = rdr["is_active"].ToString() == "Yes";
                    //string Active = Function.is_active ? "Yes" : "No";

                    Function.Till_name = rdr["Till_name"].ToString();
                    Function.TMachineId = rdr["machine_id"] != DBNull.Value ? Convert.ToInt32(rdr["machine_id"].ToString()) : 0;
                    ViewData["MachineId"] = Function.TMachineId;

                    list.Add(Function);
                }
                con.Close();
            }
            return list;
        } 
        public clsFunction Select(int mainfunction_id, int cmpId,  string connStr)
        {
            clsFunction Function = new clsFunction();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                DataTable dtMainFunction = BindSwapMachineFunction(mainfunction_id, cmpId, connStr);
                DataTable Edit_dt =  SelectFunction_Details(mainfunction_id, cmpId, connStr);




                //if (rdr.Read())
                //{
                if (dtMainFunction.Rows.Count > 0)
                {



                    Function.Fname = dtMainFunction.Rows[0]["name"].ToString();
                    Function.TMachineId = Convert.ToInt32(dtMainFunction.Rows[0]["machine_id"]);
                    GetTill(cmpId, connStr, Convert.ToInt32(dtMainFunction.Rows[0]["machine_id"]));
                    Function.is_active = dtMainFunction.Rows[0]["is_active"].ToString() == "Yes";
                    if (mainfunction_id !=0)
                    {
                        GetMachine(Convert.ToInt32(cmpId), connStr, Convert.ToInt32(dtMainFunction.Rows[0]["machine_id"]));
                    }
                    //else
                    //{
                    //    GetMachine(Convert.ToInt32(cmpId), connStr, Convert.ToInt32(dtMainFunction.Rows[0]["machine_id"]));
                    //}
                  

                    //Function.BigButton = Edit_dt["big_button"].ToString() == "Yes"; // Map to BigButton
                    //Function.IsGroupByDept = Edit_dt["is_groupby_dept"].ToString() == "Yes"; // Grouping logic
                    //Function.MachineId = Convert.ToInt32(Edit_dt["machine_id"]);
                    // Grouping logic
                }
                ViewData["MachineId"] = Function.TMachineId;



                //row["name"] = Function.name.ToString();
                //row["code"] = Function.Code; // Assuming you need to assign Code as well
                //row["is_active"] = Function.is_active ? 1 : 0; // Map boolean to integer
                //row["cmp_id"] = cmpId;
                //row["big_button"] = 0; // Default value, adjust accordingly
                //row["Function_Id"] = 0;
                //dt.Rows.Add(row);



                ViewData["Data_dt"] = dt;
                    // Define columns for the data table
                    dt.Columns.Add("id", typeof(int));
                    dt.Columns.Add("cmp_id", typeof(int));
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("code", typeof(string));
                    dt.Columns.Add("caption_type", typeof(string));
                    dt.Columns.Add("is_active", typeof(int));
                    dt.Columns.Add("ip_address", typeof(string));
                    dt.Columns.Add("login_id", typeof(int));
                    dt.Columns.Add("item", typeof(decimal));
                    dt.Columns.Add("back_color", typeof(string));
                    dt.Columns.Add("for_color", typeof(string));
                    dt.Columns.Add("machine_id", typeof(int));
                    dt.Columns.Add("big_button", typeof(int));
                    dt.Columns.Add("Payment_id", typeof(int));
                    dt.Columns.Add("pay_type", typeof(int));
                    dt.Columns.Add("pay_sub_type", typeof(string));
                    dt.Columns.Add("is_groupby_dept", typeof(int));
                    dt.Columns.Add("is_groupby_course", typeof(int));
                    dt.Columns.Add("dept_id", typeof(string));
                    dt.Columns.Add("course_id", typeof(string));
                    dt.Columns.Add("Panel_Id", typeof(int));
                    dt.Columns.Add("Parent_id", typeof(int));
                    dt.Columns.Add("Is_Parent", typeof(int));
                    dt.Columns.Add("Function_Id", typeof(int));
                    dt.Columns.Add("platformAdd", typeof(string));
                    dt.Columns.Add("clientToken", typeof(string));
                    dt.Columns.Add("accessToken", typeof(string));
                    dt.Columns.Add("serviceid", typeof(string));
                    dt.Columns.Add("EOHOAmount", typeof(decimal));
                    dt.Columns.Add("tax_id", typeof(int));
                    dt.Columns.Add("Total_Value", typeof(decimal));
                    dt.Columns.Add("Sales_Handling_Fee", typeof(decimal));
                    dt.Columns.Add("Payment_Handling_Fee", typeof(decimal));
                    dt.Columns.Add("Tax_Amount", typeof(decimal));
                    dt.Columns.Add("profile_id", typeof(int));
                    dt.Columns.Add("DefaultDateTime", typeof(string));
                    dt.Columns.Add("ZR_VenueID", typeof(int));
                    dt.Columns.Add("ZR_LocationID", typeof(int));
                    dt.Columns.Add("ZR_TillID", typeof(string));
                    dt.Columns.Add("CardPayType", typeof(int));

                //---------------------------------------------------------------------------------------BigButton code --
                //for (int i = 1; i <= 30; i++)
                //{
                //    DataRow row = dt.NewRow();
                //    row["id"] = i;
                //    row["code"] = "F" + i;
                //    row["name"] = "Button " + i;
                //    row["is_active"] = 1;
                //    row["back_color"] = "#1B7BBD";
                //    row["for_color"] = "#FFFFFF";
                //    row["big_button"] = (i % 2 == 0 ? 1 : 0);   

                //    dt.Rows.Add(row);
                //}


                //---------------------------------------------------------------------------------------BigButton code -- 
                // Fill data into the table
                string val1 = "F";
                    for (int i = 1; i <= 30; i++)
                    {
                        DataRow row = dt.NewRow();
                        row["id"] = i;
                        row["cmp_id"] = 0;
                        row["name"] = "";
                        row["code"] = val1 + i.ToString();
                        row["caption_type"] = "";
                        row["is_active"] = 1;
                        row["ip_address"] = "";
                        row["login_id"] = 0;
                        row["item"] = 0.00m;
                        row["back_color"] = "#1B7BBD";
                        row["for_color"] = "#FFFFFF";
                        row["machine_id"] = 0;
                        row["big_button"] = 0;
                        row["Payment_id"] = 0;
                        row["pay_type"] = 0;
                        row["pay_sub_type"] = "";
                        row["is_groupby_dept"] = 0;
                        row["is_groupby_course"] = 0;
                        row["dept_id"] = 0;
                        row["course_id"] = 0;
                        row["Panel_Id"] = 0;
                        row["Parent_id"] = 0;
                        row["Is_Parent"] = 0;
                        row["Function_Id"] = 0;
                        row["platformAdd"] = "";
                        row["clientToken"] = "";
                        row["accessToken"] = "";
                        row["serviceid"] = "";
                        row["EOHOAmount"] = 0;
                        row["tax_id"] = 0;
                        row["Total_Value"] = 0;
                        row["Sales_Handling_Fee"] = 0;
                        row["Payment_Handling_Fee"] = 0;
                        row["Tax_Amount"] = 0;
                        row["profile_id"] = 0;
                        row["DefaultDateTime"] = "";
                        row["ZR_VenueID"] = 0;
                        row["ZR_LocationID"] = 0;
                        row["ZR_TillID"] = "";
                        row["CardPayType"] = 0;

                        dt.Rows.Add(row);
                    }

                // Store data in ViewState
                ViewData["Data_dt"] = dt;

                    // Update rows in dt with data from rdr
                    foreach (DataRow row in Edit_dt.Rows)
                    {
                        foreach (DataRow row1 in dt.Rows)
                        {
                            if (row1["code"].ToString() == row["code"].ToString())
                            {
                                row1["cmp_id"] = row["cmp_id"];
                                row1["name"] = row["name"];
                                row1["code"] = row["code"];
                                row1["caption_type"] = row["caption_type"];
                                row1["is_active"] = row["is_active"];
                                row1["ip_address"] = row["ip_address"];
                                row1["login_id"] = row["login_id"];
                                row1["item"] = row["item"];
                                row1["back_color"] = row["back_color"];
                                row1["for_color"] = row["for_color"];
                                row1["machine_id"] = row["machine_id"];
                                row1["big_button"] = row["big_button"];
                                row1["Payment_id"] = row["Payment_id"];
                                row1["pay_type"] = row["pay_type"];
                                row1["pay_sub_type"] = row["pay_sub_type"];
                                row1["is_groupby_dept"] = row["is_groupby_dept"];
                                row1["is_groupby_course"] = row["is_groupby_course"];
                                row1["dept_id"] = row["dept_id"];
                                row1["course_id"] = row["course_id"];
                                row1["Panel_Id"] = row["Panel_Id"];
                                row1["Parent_id"] = row["Parent_id"];
                                row1["Is_Parent"] = row["Parent_id"];
                                row1["Function_Id"] = row["Function_Id"];
                                row1["platformAdd"] = row["platformAdd"].ToString();
                                row1["clientToken"] = row["clientToken"].ToString();
                                row1["accessToken"] = row["accessToken"].ToString();
                                row1["serviceid"] = row["serviceid"].ToString();
                                row1["EOHOAmount"] = row["EOHOAmount"];
                                row1["tax_id"] = row["tax_id"];
                                row1["Total_Value"] = row["Total_Value"];
                                row1["Sales_Handling_Fee"] = row["Sales_Handling_Fee"];
                                row1["Payment_Handling_Fee"] = row["Payment_Handling_Fee"];
                                row1["Tax_Amount"] = row["Tax_Amount"];
                                row1["profile_id"] = row["profile_id"];
                                row1["DefaultDateTime"] = row["DefaultDateTime"].ToString();
                                row1["ZR_VenueID"] = Convert.ToInt32(row["ZR_VenueID"]);
                                row1["ZR_LocationID"] = Convert.ToInt32(row["ZR_LocationID"]);
                                row1["ZR_TillID"] = row["ZR_TillID"].ToString();
                                row1["CardPayType"] = Convert.ToInt32(row["CardPayType"]);
                            }
                        }
                    }

                ViewBag.Data_dt = dt;



                if (mainfunction_id != null && mainfunction_id != 0)
                {
                    Function.dt = ViewData["Data_dt"] as DataTable;
                }
                else
                {
                    Function.dt = dt;
                }
                Function.mainfunction_id = mainfunction_id;
                string serializedDataTable = JsonConvert.SerializeObject(dt);
                ViewBag.Data_dt = JsonConvert.SerializeObject(dt);
                //TempData["Data_dt"] = JsonConvert.SerializeObject(dt);
                //HttpContext.Session.SetString("Data_dt", JsonConvert.SerializeObject(dt));
                //ViewData["Data_dt"] =  dt;
                // ViewData["Data_dt"] = JsonConvert.SerializeObject(dt);
                ViewData["Data_dt"] = serializedDataTable;
                //HttpContext.Session.SetString("Data_dt", JsonConvert.SerializeObject(dt));
                //foreach (DataRow row1 in dt.Rows)
                //    {
                //        int Code = Convert.ToInt32(row1["code"].ToString().Replace("F", "")) + 1;

                //        if (Convert.ToInt32(row1["big_button"]) == 1)
                //        {
                //        //switch (codeNumber)
                //        //{
                //        //    case 2:
                //        //        btn_7by7_2.Visible = false;
                //        //        btn_7by7_1.Attributes["style"] = "margin-top: 10px; width: 180%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;";
                //        //        break;
                //        //    case 30:
                //        //        btn_7by7_30.Visible = false;
                //        //        break;
                //        //    default:
                //        //        break;
                //        //}
                //    }

                //    // Set button names and colors if 'name' is not empty
                //    //if (!string.IsNullOrEmpty(row["name"].ToString()))
                //    //{
                //    //    switch (row["code"].ToString())
                //    //    {
                //    //        case "F1":
                //    //            SetName(btn_7by7_1, row["name"].ToString(), row["back_color"].ToString(), row["for_color"].ToString());
                //    //            break;
                //    //        case "F2":
                //    //            SetName(btn_7by7_2, row["name"].ToString(), row["back_color"].ToString(), row["for_color"].ToString());
                //    //            break;
                //    //        case "F3":
                //    //            SetName(btn_7by7_3, row["name"].ToString(), row["back_color"].ToString(), row["for_color"].ToString());
                //    //            break;
                //    //        // ... Continue up to F30
                //    //        case "F30":
                //    //            SetName(btn_7by7_30, row["name"].ToString(), row["back_color"].ToString(), row["for_color"].ToString());
                //    //            break;
                //    //        default:
                //    //            break;
                //    //    }
                //    //}
                // }

                ////Function = new clsFunction
                ////    {
                ////        mainfunction_id = Convert.ToInt32(rdr["location_id"]),
                ////        name = rdr["name"].ToString(),

                ////        is_active = rdr["is_active"].ToString() == "Yes",
                ////        BigButton = rdr["big_button"].ToString() == "Yes",
                ////        IsGroupByDept = rdr["is_groupby_dept"].ToString() == "Yes"




                ////    };
                ////}
                con.Close();
            }

            //ViewData["Data_dt"] = JsonConvert.SerializeObject(dt);
            // HttpContext.Session.SetString("Data_dt", JsonConvert.SerializeObject(dt));
            return Function;
        }


        //public IActionResult YourMethodName(int? parentId)
        //{

        //}
        [HttpPost("SelectedPanelChanged")]
        public IActionResult SelectedPanelChanged([FromBody] clsFunction request)
        {
            clsFunction Function = new clsFunction();
            DataTable dt = new DataTable();
            string str = HttpContext.Session.GetString("conString");
            int mainfunction_id = Convert.ToInt32(HttpContext.Session.GetString("mainfunction_id"));

            // Initialize dt1 and namesDataTable outside of the try block
            DataTable dt1 = new DataTable();
            DataTable namesDataTable = new DataTable();
            namesDataTable.Columns.Add("name", typeof(string));

            // Initialize dt1 structure outside try block
            dt1.Columns.Add("id", typeof(int));
            dt1.Columns.Add("cmp_id", typeof(int));
            dt1.Columns.Add("name", typeof(string));
            dt1.Columns.Add("code", typeof(string));
            dt1.Columns.Add("caption_type", typeof(string));
            dt1.Columns.Add("is_active", typeof(int));
            dt1.Columns.Add("ip_address", typeof(string));
            dt1.Columns.Add("login_id", typeof(int));
            dt1.Columns.Add("item", typeof(decimal));
            dt1.Columns.Add("back_color", typeof(string));
            dt1.Columns.Add("for_color", typeof(string));
            dt1.Columns.Add("machine_id", typeof(int));
            dt1.Columns.Add("big_button", typeof(int));
            dt1.Columns.Add("Payment_id", typeof(int));
            dt1.Columns.Add("pay_type", typeof(int));
            dt1.Columns.Add("pay_sub_type", typeof(string));
            dt1.Columns.Add("is_groupby_dept", typeof(int));
            dt1.Columns.Add("is_groupby_course", typeof(int));
            dt1.Columns.Add("dept_id", typeof(string));
            dt1.Columns.Add("course_id", typeof(string));
            dt1.Columns.Add("Panel_Id", typeof(int));
            dt1.Columns.Add("Parent_id", typeof(int));
            dt1.Columns.Add("Is_Parent", typeof(int));
            dt1.Columns.Add("Function_Id", typeof(int));
            dt1.Columns.Add("platformAdd", typeof(string));
            dt1.Columns.Add("clientToken", typeof(string));
            dt1.Columns.Add("accessToken", typeof(string));
            dt1.Columns.Add("serviceid", typeof(string));
            dt1.Columns.Add("EOHOAmount", typeof(decimal));
            dt1.Columns.Add("tax_id", typeof(int));
            dt1.Columns.Add("Total_Value", typeof(decimal));
            dt1.Columns.Add("Sales_Handling_Fee", typeof(decimal));
            dt1.Columns.Add("Payment_Handling_Fee", typeof(decimal));
            dt1.Columns.Add("Tax_Amount", typeof(decimal));
            dt1.Columns.Add("profile_id", typeof(int));
            dt1.Columns.Add("DefaultDateTime", typeof(string));
            dt1.Columns.Add("ZR_VenueID", typeof(int));
            dt1.Columns.Add("ZR_LocationID", typeof(int));
            dt1.Columns.Add("ZR_TillID", typeof(string));
            dt1.Columns.Add("CardPayType", typeof(int));

            if (request.selectedPanelId <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid panel ID." });
            }

            try
            {
                // Setting button styles using a helper function (SetName) //////////////////// Added in the 17-Apr-2025
                SetButtonStyle("btn_7by7_1", "F1", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_2", "F2", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_3", "F3", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_4", "F4", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_5", "F5", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_6", "F6", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_7", "F7", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_8", "F8", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_9", "F9", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_10", "F10", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_11", "F11", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_12", "F12", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_13", "F13", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_14", "F14", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_15", "F15", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_16", "F16", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_17", "F17", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_18", "F18", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_20", "F20", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_21", "F21", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_22", "F22", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_23", "F23", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_24", "F24", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_25", "F25", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_26", "F26", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_27", "F27", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_28", "F28", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_29", "F29", "#1B7BBD", "#FFFFFF");
                SetButtonStyle("btn_7by7_30", "F30", "#1B7BBD", "#FFFFFF");
              

                if (mainfunction_id != null)
                {
                    Function.CmpId = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                    Function.Panel_Id = Convert.ToInt32(request.selectedPanelId);

                    // Fetch panel function details
                    DataTable Edit_dt = SelectPanelFunction(Function.Panel_Id, Convert.ToInt32(mainfunction_id), Function.CmpId, str);

                    // Add rows for 30 buttons
                    string val2 = "F";
                    for (int i = 1; i <= 30; i++)
                    {
                        DataRow row = dt1.NewRow();
                        row["id"] = i;
                        row["cmp_id"] = 0;
                        row["name"] = "";
                        row["code"] = val2 + i.ToString();
                        row["caption_type"] = "";
                        row["is_active"] = 1;
                        row["ip_address"] = "";
                        row["login_id"] = 0;
                        row["item"] = 0.00m;
                        row["back_color"] = "#1B7BBD";
                        row["for_color"] = "#FFFFFF";
                        row["machine_id"] = 0;
                        row["big_button"] = 0;
                        row["Payment_id"] = 0;
                        row["pay_type"] = 0;
                        row["pay_sub_type"] = "";
                        row["is_groupby_dept"] = 0;
                        row["is_groupby_course"] = 0;
                        row["dept_id"] = "0";
                        row["course_id"] = "0";
                        row["Panel_Id"] = 0;
                        row["Parent_id"] = Convert.ToInt32(request.selectedPanelId);
                        row["Is_Parent"] = 0;
                        row["Function_Id"] = 0;
                        row["platformAdd"] = "";
                        row["clientToken"] = "";
                        row["accessToken"] = "";
                        row["serviceid"] = "";
                        row["EOHOAmount"] = 0;
                        row["tax_id"] = 0;
                        row["Total_Value"] = 0;
                        row["Sales_Handling_Fee"] = 0;
                        row["Payment_Handling_Fee"] = 0;
                        row["Tax_Amount"] = 0;
                        row["profile_id"] = 0;
                        row["DefaultDateTime"] = "";
                        row["ZR_VenueID"] = 0;
                        row["ZR_LocationID"] = 0;
                        row["ZR_TillID"] = "";
                        row["CardPayType"] = 0;

                        dt1.Rows.Add(row);
                    }

                    // Merging Edit_dt (retrieved from DB) into dt1
                    foreach (DataRow row in Edit_dt.Rows)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            if (row1["code"].ToString() == row["code"].ToString() && Convert.ToInt32(row1["Parent_id"]) == Convert.ToInt32(request.selectedPanelId))
                            {
                                row1["cmp_id"] = row["cmp_id"];
                                row1["name"] = row["name"];
                                row1["code"] = row["code"];
                                row1["caption_type"] = row["caption_type"];
                                row1["is_active"] = row["is_active"];
                                row1["ip_address"] = row["ip_address"];
                                row1["login_id"] = row["login_id"];
                                row1["item"] = row["item"];
                                row1["back_color"] = row["back_color"];
                                row1["for_color"] = row["for_color"];
                                row1["machine_id"] = row["machine_id"];
                                row1["big_button"] = row["big_button"];
                                row1["Payment_id"] = row["Payment_id"];
                                row1["pay_type"] = row["pay_type"];
                                row1["pay_sub_type"] = row["pay_sub_type"];
                                row1["is_groupby_dept"] = row["is_groupby_dept"];
                                row1["is_groupby_course"] = row["is_groupby_course"];
                                row1["dept_id"] = row["dept_id"];
                                row1["course_id"] = row["course_id"];
                                row1["Panel_Id"] = row["Panel_Id"];
                                row1["Parent_id"] = row["Parent_id"];
                                row1["Is_Parent"] = row["Parent_id"];
                                row1["Function_Id"] = row["Function_Id"];
                                row1["platformAdd"] = row["platformAdd"].ToString();
                                row1["clientToken"] = row["clientToken"].ToString();
                                row1["accessToken"] = row["accessToken"].ToString();
                                row1["serviceid"] = row["serviceid"].ToString();
                                row1["EOHOAmount"] = row["EOHOAmount"].ToString();
                                row1["tax_id"] = row["tax_id"].ToString();
                                row1["Total_Value"] = Convert.ToDecimal(row["Total_Value"]);
                                row1["Sales_Handling_Fee"] = Convert.ToDecimal(row["Sales_Handling_Fee"]);
                                row1["Payment_Handling_Fee"] = Convert.ToDecimal(row["Payment_Handling_Fee"]);
                                row1["Tax_Amount"] = Convert.ToDecimal(row["Tax_Amount"]);
                                row1["profile_id"] = Convert.ToInt32(row["profile_id"]);
                                row1["DefaultDateTime"] = row["DefaultDateTime"].ToString();
                                row1["ZR_VenueID"] = Convert.ToInt32(row["ZR_VenueID"]);
                                row1["ZR_LocationID"] = Convert.ToInt32(row["ZR_LocationID"]);
                                row1["ZR_TillID"] = row["ZR_TillID"].ToString();
                                row1["CardPayType"] = Convert.ToInt32(row["CardPayType"]);

                                namesDataTable.Rows.Add(row1["name"].ToString());
                            }
                        }
                    }

                    if (namesDataTable.Rows.Count == 0)
                    {
                        // Log or debug to check why it's empty
                    }

                    ViewData["NamesDataTable"] = namesDataTable;
                    ViewData["Data_dt"] = dt1;
                }
                var namesList = namesDataTable.AsEnumerable()
                                         .Select(row => new { name = row.Field<string>("name") })
                                         .ToList();
                return Ok(new
                {
                    success = true,
                    message = "Panel changed successfully",
                    namesDataTable = namesDataTable,
                    dataDt = dt1
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error: " + ex.Message });
            }
        }
        //[HttpPost]
        //public JsonResult SaveSelectedData([FromBody] clsFunction requestData)
        //{
        //    try
        //    {
        //        foreach (var item in requestData.SelectedData)
        //        {
        //            // Save each item to database or file
        //            Console.WriteLine($"Saving: {item.Name} ({item.Code})");
        //        }

        //        return Json(new { success = true, message = "Data saved successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Error: " + ex.Message });
        //    }
        //}

        private void SetButtonStyle(string buttonId, string buttonText, string backColor, string foreColor)
        {
            // Update ViewData for each button to pass to the view
            ViewData[buttonId] = new { Text = buttonText, BackColor = backColor, ForeColor = foreColor };
        }
        
        public DataTable BindSwapMachineFunction(int id, int? c_id, string conn)
        {
            DataTable dtMainFunction = new DataTable();  
            try
            {
                 
                using (SqlConnection con = new SqlConnection(conn))
                {
                    
                    SqlCommand cmd1 = new SqlCommand("Get_M_MainFunction", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                     
                    cmd1.Parameters.AddWithValue("@cmp_id", c_id);
                    cmd1.Parameters.AddWithValue("@mainfunction_id", id);

                    
                    con.Open();
 
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                     

                    // Load the data directly into the DataTable (no need for rdr1.Read())
                    //dtMainFunction.Load(rdr1);
                    // Read the data
                    if (rdr1.HasRows)
                    {
                        // Populate the DataTable with the results from the reader
                        dtMainFunction.Load(rdr1);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error while binding swap machine function: " + ex.Message);
            }

            // Return the populated DataTable
            return dtMainFunction;
        }   
       public DataTable SelectPanelFunction(int panelID,int id, int? c_id, string conn)
        {
            DataTable dtPanelFunction = new DataTable();  
            try
            {
                 
                using (SqlConnection con = new SqlConnection(conn))
                {
                    
                    SqlCommand cmd1 = new SqlCommand("Get_M_PanelFunction", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    }; 
                    cmd1.Parameters.AddWithValue("@cmp_id", c_id);
                    cmd1.Parameters.AddWithValue("@Panel_id", panelID);
                    cmd1.Parameters.AddWithValue("@mainfunction_id", id);
                     
                    con.Open(); 
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                      
                    if (rdr1.HasRows)
                    {
                        dtPanelFunction.Load(rdr1);
                    }
                }
            }
            catch (Exception ex)
            {
                 
                Console.WriteLine("Error while binding swap machine function: " + ex.Message);
            }

             
            return dtPanelFunction;
        }
        

        public DataTable SelectFunction_Details(int id, int? c_id,  string conn)
        {
            DataTable Function_Details = new DataTable(); // Initialize an empty DataTable
            try
            {
                // Create a new SQL connection
                using (SqlConnection con = new SqlConnection(conn))
                {
               
                    SqlCommand cmd1 = new SqlCommand("Get_M_Function_Details", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                 
                    cmd1.Parameters.AddWithValue("@cmp_id", c_id);
                    cmd1.Parameters.AddWithValue("@mainfunction_id", id);

                    // Open the connection
                    con.Open();

                    // Execute the reader
                    SqlDataReader rdr1 = cmd1.ExecuteReader();

                    // Read the data
                    if (rdr1.HasRows)
                    {
                        // Populate the DataTable with the results from the reader
                        Function_Details.Load(rdr1);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error while binding swap machine function: " + ex.Message);
            }

            // Return the populated DataTable
            return Function_Details;
        }
        //public DataTable BindSwapMachineFunction(int? c_id, int id, string conn)
        //{
        //    clsFunction Function = new clsFunction();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        // If dtMainFunction has rows, set up the controls

        //        SqlCommand cmd1 = new SqlCommand("Get_M_MainFunction", con)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd1.Parameters.AddWithValue("@cmp_id", c_id);
        //        cmd1.Parameters.AddWithValue("@mainfunction_id", id);


        //        SqlDataReader rdr1 = cmd1.ExecuteReader();

        //        if (rdr1.Read())
        //        {


        //            return dtMainFunction As DataTable = rdr1;
        //            //Function = new clsFunction
        //            //{
        //            //    mainfunction_id = Convert.ToInt32(rdr1["location_id"]),
        //            //    name = rdr1["name"].ToString(),
        //            //    is_active = rdr1["is_active"].ToString() == "Yes", // Correctly map the is_active
        //            //    BigButton = rdr1["big_button"].ToString() == "Yes", // Map to BigButton
        //            //    IsGroupByDept = rdr1["is_groupby_dept"].ToString() == "Yes", // Grouping logic
        //            //    MachineId = Convert.ToInt32(rdr1["machine_id"])
        //            //    // Grouping logic
        //            //};
        //            //ViewData["MachineId"] = Function.MachineId;
        //            //DataRow row = dt.NewRow();

        //            //row["name"] = Function.name.ToString();
        //            //row["code"] = Function.Code; // Assuming you need to assign Code as well
        //            //row["is_active"] = Function.is_active ? 1 : 0; // Map boolean to integer
        //            //row["cmp_id"] = c_id;
        //            //row["big_button"] = 0; // Default value, adjust accordingly
        //            //row["Function_Id"] = 0;
        //            //dt.Rows.Add(row);


        //            //GetTill(Convert.ToInt32(c_id), conn);

        //            //GetMachine(Convert.ToInt32(c_id), conn);


        //            //dt.Rows.Add(row);
        //             }
        //        }


        //}

        //public void BindFunctionSwap(clsFunction Function,int cmp_id ,string str)
        //{
        //    string hfSwapStatus = Request.Form["hfSwapStatus"];
        //    try
        //    {
        //        if (hfSwapStatus == "1")
        //        {
        //            Function.CmpId = Convert.ToInt32(cmp_id);
        //Function.MachineId = Convert.ToInt32(RadSwapMachine.SelectedValue);
        //DataTable dtMainFunctionId = ConvertListToDataTable(BindFunctionSwap(cmp_id, str));

        //if (dtMainFunctionId.Rows.Count > 0)
        //{
        //    Function.CmpId = Convert.ToInt32(Session["cmp_id"]);
        //    Function.mainfunction_id = Convert.ToInt32(dtMainFunctionId.Rows[0]["mainfunction_id"].ToString());
        //    DataTable dtMainFunction = SelectMainFunction(int cmp_id, string str);
        //    DataTable editDt = SelectFunctionDetails();

        //    if (dtMainFunction.Rows.Count > 0)
        //    {
        //        txtMainFun.Text = dtMainFunction.Rows[0]["name"].ToString();
        //        RadMachine.ClearSelection();
        //        BindMachineFunction(RadMachine, Convert.ToInt32(Session["cmp_id"]), Convert.ToInt32(dtMainFunction.Rows[0]["machine_id"].ToString()));
        //        DataTable dtMainFunction = Function.SelectMainFunction();
        //        DataTable editDt = Function.SelectFunctionDetails();

        //        // ChkMainActive.Checked = dtMainFunction.Rows[0]["is_active"].ToString() == "Yes";
        //        bool isChecked = Request.Form["ChkMainActive"] == "on";

        //        // Initialize DataTable with required columns
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("id", typeof(int));
        //        dt.Columns.Add("cmp_id", typeof(int));
        //        dt.Columns.Add("name", typeof(string));
        //        dt.Columns.Add("code", typeof(string));
        //        dt.Columns.Add("caption_type", typeof(string));
        //        dt.Columns.Add("is_active", typeof(int));
        //        dt.Columns.Add("ip_address", typeof(string));
        //        dt.Columns.Add("login_id", typeof(int));
        //        dt.Columns.Add("item", typeof(decimal));
        //        dt.Columns.Add("back_color", typeof(string));
        //        dt.Columns.Add("for_color", typeof(string));
        //        dt.Columns.Add("machine_id", typeof(int));
        //        dt.Columns.Add("big_button", typeof(int));
        //        dt.Columns.Add("Payment_id", typeof(int));
        //        dt.Columns.Add("pay_type", typeof(int));
        //        dt.Columns.Add("pay_sub_type", typeof(string));
        //        dt.Columns.Add("is_groupby_dept", typeof(int));
        //        dt.Columns.Add("is_groupby_course", typeof(int));
        //        dt.Columns.Add("dept_id", typeof(string));
        //        dt.Columns.Add("course_id", typeof(string));
        //        dt.Columns.Add("Panel_Id", typeof(int));
        //        dt.Columns.Add("Parent_id", typeof(int));
        //        dt.Columns.Add("Is_Parent", typeof(int));
        //        dt.Columns.Add("Function_Id", typeof(int));

        //        // Add rows to DataTable
        //        string val1 = "F";
        //        for (int i = 1; i <= 30; i++)
        //        {
        //            DataRow row = dt.NewRow();
        //            row["id"] = i;
        //            row["cmp_id"] = 0;
        //            row["name"] = "";
        //            row["code"] = val1 + i.ToString();
        //            row["caption_type"] = "";
        //            row["is_active"] = 1;
        //            row["ip_address"] = "";
        //            row["login_id"] = 0;
        //            row["item"] = 0.00m;
        //            row["back_color"] = "#1B7BBD";
        //            row["for_color"] = "#FFFFFF";
        //            row["machine_id"] = 0;
        //            row["big_button"] = 0;
        //            row["Payment_id"] = 0;
        //            row["pay_type"] = 0;
        //            row["pay_sub_type"] = "";
        //            row["is_groupby_dept"] = 0;
        //            row["is_groupby_course"] = 0;
        //            row["dept_id"] = 0;
        //            row["course_id"] = 0;
        //            row["Panel_Id"] = 0;
        //            row["Parent_id"] = 0;
        //            row["Is_Parent"] = 0;
        //            row["Function_Id"] = 0;
        //            dt.Rows.Add(row);
        //        }

        //        ViewData["Data_dt"] = dt;

        //        // Map Edit data to DataTable rows
        //        foreach (DataRow row in editDt.Rows)
        //        {
        //            foreach (DataRow row1 in dt.Rows)
        //            {
        //                if (row1["code"].ToString() == row["code"].ToString())
        //                {
        //                    row1["cmp_id"] = row["cmp_id"];
        //                    row1["name"] = row["name"];
        //                    row1["code"] = row["code"];
        //                    row1["caption_type"] = row["caption_type"];
        //                    row1["is_active"] = row["is_active"];
        //                    row1["ip_address"] = row["ip_address"];
        //                    row1["login_id"] = row["login_id"];
        //                    row1["item"] = row["item"];
        //                    row1["back_color"] = row["back_color"];
        //                    row1["for_color"] = row["for_color"];
        //                    row1["machine_id"] = row["machine_id"];
        //                    row1["big_button"] = row["big_button"];
        //                    row1["Payment_id"] = row["Payment_id"];
        //                    row1["pay_type"] = row["pay_type"];
        //                    row1["pay_sub_type"] = row["pay_sub_type"];
        //                    row1["is_groupby_dept"] = row["is_groupby_dept"];
        //                    row1["is_groupby_course"] = row["is_groupby_course"];
        //                    row1["dept_id"] = row["dept_id"];
        //                    row1["course_id"] = row["course_id"];
        //                    row1["Panel_Id"] = row["Panel_Id"];
        //                    row1["Parent_id"] = row["Parent_id"];
        //                    row1["Is_Parent"] = row["Parent_id"];
        //                    row1["Function_Id"] = row["Function_Id"];
        //                }
        //            }
        //        }

        //        ViewData["Data_dt"] = dt;

        //        foreach (DataRow row1 in dt.Rows)
        //        {
        //            int code = Convert.ToInt32(row1["code"].ToString().Replace("F", "")) + 1;

        //            // Update button visibility and styles
        //            if (row1["big_button"].ToString() == "1")
        //            {
        //                // Example logic for buttons
        //                if (code == 2)
        //                {
        //                    //btn_7by7_2.Visible = false;
        //                    //btn_7by7_1.Attributes["style"] = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;";
        //                }
        //                // Continue with the remaining logic
        //            }
        //        }
        //                }
        //            }
        //        }
        //        else if (hfSwapStatus == "0")
        //        {
        //            // Logic for hf_swapstatus = 0
        //        }
        //        else
        //        {
        //            // Logic for other cases
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}





        public DataTable BindFunctionSwap(int? c_id, int id,string conn)
        {
            List<SelectListItem> MainMachine = new List<SelectListItem>();
            int MachineID = id;// Default value if nothing is set

            if (ViewData["MachineId"] != null)
            {
                int.TryParse(ViewData["MachineId"].ToString(), out MachineID);
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_MainFunction_Swap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@machine_id", MachineID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;

                con.Close();
            }

         
        }

        public List<SelectListItem> GetMachine(int? c_id, string conn, int MachineID)
        {
            List<SelectListItem> Machine = new List<SelectListItem>();
            //int MachineID=0;// Default value if nothing is set

            if (ViewData["MachineId"] != null)
            {

                int.TryParse(ViewData["MachineId"].ToString(), out MachineID);
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Machine_By_Function", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@machine_id", MachineID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Machine.Add(new SelectListItem
                    {
                        Value = rdr["machine_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return Machine;
        }

        public List<SelectListItem> GetTax(int? c_id, string conn)
        {
            List<SelectListItem> Tax = new List<SelectListItem>();
            //int MachineID=0;// Default value if nothing is set

           

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Tax_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                 

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tax.Add(new SelectListItem
                    {
                        Value = rdr["Tax_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return Tax;
        }

        //public List<SelectListItem> GetMachine(int? c_id, string conn)
        //{
        //    List<SelectListItem> Machine = new List<SelectListItem>();
        //    int MachineID = 0;
        //    if (ViewData["MachineId"] != null)
        //    {
        //        int.TryParse(ViewData["MachineId"].ToString(), out MachineID);
        //    }
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_SwapMachine_By_Function", con);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@cmp_id", c_id); 
        //        cmd.Parameters.AddWithValue("@machine_id", MachineID);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            Machine.Add(new SelectListItem
        //            {
        //                Value = rdr["machine_id"].ToString(),
        //                Text = rdr["name"].ToString()
        //            });
        //        }

        //        con.Close();
        //    }

        //    return Machine;
        //}    
        public List<SelectListItem> GetTill(int? c_id, string conn,int? TmachineID)
        {
            List<SelectListItem> Till = new List<SelectListItem>();
            //int MachineID = Convert.ToInt32(ViewData["MachineId"]);
            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Machine_By_Function", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@machine_id", TmachineID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader(); 

                while (rdr.Read())
                {
                    Till.Add(new SelectListItem
                    {
                        Value = rdr["machine_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return Till;
        }


        //public clsLocation Select(int cmpId, int locationId, string connStr, HttpContext httpContext)
        //{
        //    clsLocation location = new clsLocation();

        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Location", con)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.AddWithValue("@cmp_id", cmpId);
        //        cmd.Parameters.AddWithValue("@location_id", locationId);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        if (rdr.Read())
        //        { 
        //            location = new clsLocation
        //            {
        //            };
        //        }
        //        con.Close();
        //    }

        //    return location;
        //}

       
        public List<SelectListItem> SelectEditPanelByCmp(int? c_id,  int mainfunction_id ,string conn)
        {
            List<SelectListItem> Panel = new List<SelectListItem>();
            //int mainfunctionID = Convert.ToInt32(ViewData["mainfunction_id"]);
            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Edit_Panel_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id); 
                cmd.Parameters.AddWithValue("@mainfunction_id", mainfunction_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Panel.Add(new SelectListItem
                    {
                        Value = rdr["Parent_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return Panel;
        } 
        public List<SelectListItem> SelectParentFunctionByCmp(int? c_id, string conn)
        {
            List<SelectListItem> Parent = new List<SelectListItem>();
            
            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Parent_Function_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id); 
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Parent.Add(new SelectListItem
                    {
                        Value = rdr["function_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return Parent;
        }
        public List<SelectListItem> SelectFunctionByCmp(int? c_id, string conn)
        {
            List<SelectListItem> Function = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Function_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                 
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr["name"].ToString();

                    // Filter out any name containing 'Dance'
                    if (!name.Contains("Dance", StringComparison.OrdinalIgnoreCase))
                    {
                            Function.Add(new SelectListItem
                        {
                            Value = rdr["name"].ToString(),
                            Text = rdr["name"].ToString()
                        });
                    }
                }

                con.Close();
            }

            return Function;
        }


        //public List<SelectListItem> DepSelect_By_Cmp(int? c_id, string conn)
        //{
        //    List<SelectListItem> dept = new List<SelectListItem>();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {


        //        SqlCommand cmd = new SqlCommand("Get_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);
        //        //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {


        //            // Filter out any name containing 'Dance'

        //            dept.Add(new SelectListItem
        //                {
        //                    Value = rdr["Department_id"].ToString(),
        //                    Text = rdr["Name"].ToString()
        //                });
                     
        //        }

        //        con.Close();
        //    }

        //    return dept;
        //}
    
        public List<SelectListItem> SelectPayment(int? c_id, string conn)
        {
            List<SelectListItem> Payment = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Payment_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Payment.Add(new SelectListItem
                        {
                            Value = rdr["Payment_id"].ToString(),
                            Text = rdr["Name"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Payment;
        }
       
        
        public List<SelectListItem> SelectLocation(int? c_id, int? venue_id, string conn)
        {
            List<SelectListItem> Loaction = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Location_By_Venue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@Venue_id", venue_id);

                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Loaction.Add(new SelectListItem
                        {
                            Value = rdr["location_Id"].ToString(),
                            Text = rdr["name"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Loaction;
        }
        
        public List<SelectListItem> SelectVenueByCmp(int? c_id, string conn)
        {
            List<SelectListItem> Venue = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

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
        
        public List<SelectListItem> SelectCardPaymentTyp(int? c_id, string conn)
        {
            List<SelectListItem> Venue = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_CardPayType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Venue.Add(new SelectListItem
                        {
                            Value = rdr["CardPayType_Id"].ToString(),
                            Text = rdr["CardName"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Venue;
        }


        //--------------------------------------------------------------------------------------------------------------------
        //[HttpPost]
        //public IActionResult BindFunctionSwap([FromBody] BindFunctionSwapModel swapModel)
        //{
        //    try
        //    {
        //        if (swapModel.SwapStatus == 1)
        //        {
        //            int cmpId = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
        //            int mainfunction_id = Convert.ToInt32(HttpContext.Session.GetString("mainfunction_id"));
        //            string str = HttpContext.Session.GetString("conString");
        //            int machineId = swapModel.SelectedMachineId;

        //            var dtMainFunctionId = BindFunctionSwap(cmpId, Convert.ToInt32(mainfunction_id), str);
        //            if (dtMainFunctionId != null && dtMainFunctionId.Any())
        //            {
        //                //int mainFunctionId = dtMainFunctionId.First().MainFunctionId;

        //                var dtMainFunction = SelectFunctionByCmp(cmpId, mainfunction_id, str);
        //                var editDt = SelectFunction_Details( mainfunction_id, cmpId, str);       //------------------------------------Do it tomorrowwwwwwwww

        //                if (dtMainFunction != null && dtMainFunction.Any())
        //                {
        //                    var mainFunction = dtMainFunction.First();

        //                    // Update Main Function TextBox via ViewModel or return it in JSON
        //                    // For simplicity, we'll skip updating txtMainFun here

        //                    // Initialize buttons
        //                    var buttons = InitializeButtons();

        //                    // Merge edit data with buttons
        //                    foreach (var editRow in editDt)
        //                    {
        //                        var button = buttons.FirstOrDefault(b => b.Code.Equals(editRow.Code, StringComparison.OrdinalIgnoreCase));
        //                        if (button != null)
        //                        {
        //                            button.CmpId = editRow.CmpId;
        //                            button.Name = editRow.Name;
        //                            button.CaptionType = editRow.CaptionType;
        //                            button.is_active = editRow.IsActive;
        //                            button.BackColor = editRow.BackColor;
        //                            button.ForColor = editRow.ForColor;
        //                            button.BigButton = editRow.BigButton;
        //                            // Update other properties as needed
        //                        }
        //                    }

        //                    // Update button visibility and styles based on BigButton flag
        //                    foreach (var button in buttons)
        //                    {
        //                        if (button.BigButton == 1)
        //                        {
        //                            button.Visible = false;
        //                            button.Style = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;";
        //                        }
        //                        else
        //                        {
        //                            button.Visible = true;
        //                            button.Style = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;";
        //                        }
        //                    }

        //                    return Json(new { success = true, buttons });
        //                }
        //            }
        //        }
        //        // Handle other SwapStatus values if needed
        //        return Json(new { success = false, message = "Invalid swap status or no data found." });
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new { success = false, message = "An error occurred while processing your request." });
        //    }
        //}


        [HttpPost]
        public IActionResult Delete(clsFunction func, string connStr)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("P_M_MainFunction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", func.CmpId);
                    cmd.Parameters.AddWithValue("@machine_id", func.TMachineId);
                    cmd.Parameters.AddWithValue("@mainfunction_id", func.mainfunction_id);
                    cmd.Parameters.AddWithValue("@name", func.Fname);

                    cmd.Parameters.AddWithValue("@is_active", func.is_active);
                    cmd.Parameters.AddWithValue("@ip_address", func.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", 0);

                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    //newFunctionId = Convert.ToInt32(cmd.ExecuteScalar());
                    //func.mainfunction_id = newFunctionId;
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return Ok("Function data deleted successfully.");
        }

    //    private List<clsFunction> InitializeButtons()
    //    {
    //        var buttons = new List<ButtonModel>();
    //        string val = "F";

    //        for (int i = 1; i <= 30; i++)
    //        {
    //            buttons.Add(new ButtonModel
    //            {
    //                Id = $"btn_F{i}",
    //                Code = $"{val}{i}",
    //                Name = $"{val}{i}",
    //                BackColor = "#1B7BBD",
    //                ForColor = "#FFFFFF",
    //                Visible = true,
    //                Style = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;",
    //                ParentId = 0, // This will be updated based on selectedPanelId
    //                BigButton = 0
    //            });
    //        }

    //        return buttons;
    //    }
    //}

    // Additional Models





        //--------------------------------------------------------------------------------------------------------------------
    //public class DeleteItemModel
    //{
    //    public int FunctionId { get; set; }
    //}

        public void Insert(clsFunction func, string conn, string ip_address)
        {
            func.ip_address = ip_address;
            int newFunctionId = 0;
            //int key_map_id = 0;
            //List<int> insertedMachineIds = new List<int>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open(); // Open the connection once at the beginning

                using (SqlCommand cmd = new SqlCommand("P_M_MainFunction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", func.CmpId);
                    cmd.Parameters.AddWithValue("@machine_id", func.TMachineId);
                    cmd.Parameters.AddWithValue("@mainfunction_id", func.mainfunction_id);
                    cmd.Parameters.AddWithValue("@name", func.Fname); 
                   
                    cmd.Parameters.AddWithValue("@is_active", func.is_active);
                    cmd.Parameters.AddWithValue("@ip_address", func.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", 0);
                    
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                  newFunctionId = Convert.ToInt32(cmd.ExecuteScalar());
                    func.mainfunction_id = newFunctionId;
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
         }

        public void UpdateMainFunction(int? mainfunction_id,clsFunction func, string conn, string ip_address)
        {
            func.ip_address = ip_address;
            
            //int key_map_id = 0;
            //List<int> insertedMachineIds = new List<int>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open(); // Open the connection once at the beginning

                using (SqlCommand cmd = new SqlCommand("P_M_MainFunction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", func.CmpId);
                    cmd.Parameters.AddWithValue("@machine_id", func.TMachineId);
                    cmd.Parameters.AddWithValue("@mainfunction_id", mainfunction_id);
                    cmd.Parameters.AddWithValue("@name", func.Fname);

                    cmd.Parameters.AddWithValue("@is_active", func.is_active);
                    cmd.Parameters.AddWithValue("@ip_address", func.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", 0);

                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                   
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public class FunctionSaveModel
        {
            public string buttonId { get; set; }
            public string name { get; set; }
            public string back_color { get; set; }
            public string for_color { get; set; }
            public string caption_type { get; set; }
            public string platformAdd { get; set; }
           
        }
      

        [HttpPost]
        public IActionResult SaveUpdatedData([FromBody] clsFunction funcData)
        {
            if (funcData == null)
            {
                return BadRequest("Invalid data received.");
            }

            string conn = HttpContext.Session.GetString("conString"); 

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("P_M_Function", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Map all parameters from the model to the stored procedure parameters
                        cmd.Parameters.AddWithValue("@function_id", funcData.FunctionId);
                        cmd.Parameters.AddWithValue("@cmp_id", funcData.CmpId);
                        cmd.Parameters.AddWithValue("@name", funcData.Name);
                        cmd.Parameters.AddWithValue("@code", funcData.Code);
                        cmd.Parameters.AddWithValue("@caption_type", funcData.CaptionType);
                        cmd.Parameters.AddWithValue("@is_active", funcData.is_active1);
                        cmd.Parameters.AddWithValue("@shorting_no", funcData.ShortingNo);
                        cmd.Parameters.AddWithValue("@ip_address", funcData.IpAddress);
                        cmd.Parameters.AddWithValue("@login_id", funcData.LoginId);
                        cmd.Parameters.AddWithValue("@item", funcData.Amount);
                        cmd.Parameters.AddWithValue("@back_color", funcData.BackColor);
                        cmd.Parameters.AddWithValue("@for_color", funcData.ForColor);
                        cmd.Parameters.AddWithValue("@machine_id", funcData.TMachineId);
                        cmd.Parameters.AddWithValue("@big_button", funcData.BigButton);
                        cmd.Parameters.AddWithValue("@Payment_id", funcData.PaymentId);
                        cmd.Parameters.AddWithValue("@pay_type", funcData.PayType);
                        cmd.Parameters.AddWithValue("@pay_sub_type", funcData.PaySubType);
                        cmd.Parameters.AddWithValue("@is_groupby_dept", funcData.IsGroupByDept);
                        cmd.Parameters.AddWithValue("@is_groupby_course", funcData.IsGroupByCourse);
                        cmd.Parameters.AddWithValue("@dept_id", funcData.DeptId);
                        cmd.Parameters.AddWithValue("@course_id", funcData.CourseId);
                        cmd.Parameters.AddWithValue("@Panel_id", funcData.selectedPanelId);
                        cmd.Parameters.AddWithValue("@Parent_id", funcData.ParentId);
                        cmd.Parameters.AddWithValue("@mainfunction_id", funcData.mainfunction_id);
                        cmd.Parameters.AddWithValue("@platformAdd", funcData.PlatformAdd);
                        cmd.Parameters.AddWithValue("@clientToken", funcData.ClientToken);
                        cmd.Parameters.AddWithValue("@accessToken", funcData.AccessToken);
                        cmd.Parameters.AddWithValue("@serviceid", funcData.ServiceId);
                        cmd.Parameters.AddWithValue("@tax_id", funcData.TaxId);
                        cmd.Parameters.AddWithValue("@EOHelpOut_max_amount_each", funcData.EOHelpOutMaxAmountEach);
                        cmd.Parameters.AddWithValue("@Payment_Handling_Fee", funcData.PaymentHandlingFee);
                        cmd.Parameters.AddWithValue("@Sales_Handling_Fee", funcData.SalesHandlingFee);
                        cmd.Parameters.AddWithValue("@Tax_Amount", funcData.TaxAmount);
                        cmd.Parameters.AddWithValue("@Total_Value", funcData.TotalValue);
                        cmd.Parameters.AddWithValue("@profile_id", funcData.ProfileId);
                        cmd.Parameters.AddWithValue("@DefaultDateTime", funcData.DefaultDateTime);
                        cmd.Parameters.AddWithValue("@ZR_VenueID", funcData.ZrVenueId);
                        cmd.Parameters.AddWithValue("@ZR_LocationID", funcData.ZrLocationId);
                        cmd.Parameters.AddWithValue("@ZR_TillID", funcData.ZrTillId);
                        cmd.Parameters.AddWithValue("@CardPayType", funcData.CardPayType);
                        cmd.Parameters.AddWithValue("@tran_Type", "U");  // Assuming "U" stands for update

                        // Execute stored procedure
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Data saved successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        public void InsertFunctionDetails(clsFunction func, string conn, string ip_address,string buttonId,int Function_Id)
        {
            func.ip_address = ip_address;
            int newFunctionId = 0;
            ButtonRequestModel btnRequest = new ButtonRequestModel { };
            //int key_map_id = 0;

            //List<int> insertedMachineIds = new List<int>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open(); // Open the connection once at the beginning
                //ButtonRequestModel btnRequest = new ButtonRequestModel
                //{
                //    Code = btnRequest.buttonId;
                //    //    Property1 = HttpContext.Request.Form["Property1"],
                //    //    Property2 = HttpContext.Request.Form["Property2"],

                //};
               // HandleButtonClick(btnRequest);
                using (SqlCommand cmd = new SqlCommand("P_M_Function", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    Console.WriteLine($"DeptId received: {func.DeptId}");
                    Console.WriteLine($"CourseId received: {func.CourseId}");
                    cmd.Parameters.AddWithValue("@function_id", 0);
                    cmd.Parameters.AddWithValue("@cmp_id", func.CmpId);
                    cmd.Parameters.AddWithValue("@name", func.name);
                    cmd.Parameters.AddWithValue("@code", buttonId ?? string.Empty);
                    cmd.Parameters.AddWithValue("@caption_type",func.CaptionType);
                    cmd.Parameters.AddWithValue("@is_active", func.is_active1);
                    cmd.Parameters.AddWithValue("@shorting_no", func.ShortingNo);
                    cmd.Parameters.AddWithValue("@ip_address", func.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", 0);
                    cmd.Parameters.AddWithValue("@item", func.Amount);
                    cmd.Parameters.AddWithValue("@back_color", func.BackColor);
                    cmd.Parameters.AddWithValue("@for_color", func.ForColor);
                    cmd.Parameters.AddWithValue("@machine_id", func.TMachineId);
                    cmd.Parameters.AddWithValue("@big_button", func.BigButton);
                    cmd.Parameters.AddWithValue("@Payment_id", func.PaymentId);
                    cmd.Parameters.AddWithValue("@pay_type", func.PayType);
                    cmd.Parameters.AddWithValue("@pay_sub_type", func.PaySubType);
                    cmd.Parameters.AddWithValue("@is_groupby_dept", func.IsGroupByDept);
                    cmd.Parameters.AddWithValue("@is_groupby_course", func.IsGroupByCourse);
                    cmd.Parameters.AddWithValue("@dept_id", func.DeptId ?? string.Empty);
                    cmd.Parameters.AddWithValue("@course_id", func.CourseId ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Panel_id", func.Panel_Id);
                    cmd.Parameters.AddWithValue("@Parent_id", func.ParentId);
                    cmd.Parameters.AddWithValue("@mainfunction_id", func.mainfunction_id);
                    cmd.Parameters.AddWithValue("@platformAdd", func.PlatformAdd);
                    cmd.Parameters.AddWithValue("@clientToken", func.ClientToken);
                    cmd.Parameters.AddWithValue("@accessToken", func.AccessToken);
                    cmd.Parameters.AddWithValue("@serviceid", func.ServiceId);
                    cmd.Parameters.AddWithValue("@tax_id", func.TaxId);
                    cmd.Parameters.AddWithValue("@EOHelpOut_max_amount_each", func.EOHelpOutMaxAmountEach);
                    cmd.Parameters.AddWithValue("@Payment_Handling_Fee", func.PaymentHandlingFee);
                    cmd.Parameters.AddWithValue("@Sales_Handling_Fee", func.SalesHandlingFee);
                    cmd.Parameters.AddWithValue("@Tax_Amount", func.TaxAmount);
                    cmd.Parameters.AddWithValue("@Total_Value", func.TotalValue);
                    cmd.Parameters.AddWithValue("@profile_id", func.ProfileId);
                    cmd.Parameters.AddWithValue("@DefaultDateTime", func.DefaultDateTime + func.ExpireDateTime);
                    cmd.Parameters.AddWithValue("@ZR_VenueID", func.ZrVenueId);
                    cmd.Parameters.AddWithValue("@ZR_LocationID", func.ZrLocationId);
                    cmd.Parameters.AddWithValue("@ZR_TillID", func.ZrTillId);
                    cmd.Parameters.AddWithValue("@CardPayType", func.CardPayType);
                    cmd.Parameters.AddWithValue("@tran_Type", "I");

                    
                    //newFunctionId = Convert.ToInt32(cmd.ExecuteScalar());
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
       
        public void updateFunctionDetails(clsFunction func, string conn, string ip_address,string Button_Id,int Function_Id)
        {
            func.ip_address = ip_address;
            int newFunctionId = 0;
            //int key_map_id = 0;
            //List<int> insertedMachineIds = new List<int>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open(); // Open the connection once at the beginning
                //ButtonRequestModel btnRequest = new ButtonRequestModel
                //{
                 
                //    Property1 = HttpContext.Request.Form["Property1"],
                //    Property2 = HttpContext.Request.Form["Property2"],
                
                //};
               // HandleButtonClick(btnRequest);
                using (SqlCommand cmd = new SqlCommand("P_M_Function", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@function_id", Function_Id);
                    cmd.Parameters.AddWithValue("@cmp_id", func.CmpId);
                    cmd.Parameters.AddWithValue("@name", func.name);
                    cmd.Parameters.AddWithValue("@code", Button_Id);
                    cmd.Parameters.AddWithValue("@caption_type", func.CaptionType);
                    cmd.Parameters.AddWithValue("@is_active", func.is_active1);
                    cmd.Parameters.AddWithValue("@shorting_no", func.ShortingNo);
                    cmd.Parameters.AddWithValue("@ip_address", func.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", 0);
                    cmd.Parameters.AddWithValue("@item", func.Item);
                    cmd.Parameters.AddWithValue("@back_color", func.BackColor);
                    cmd.Parameters.AddWithValue("@for_color", func.ForColor);
                    cmd.Parameters.AddWithValue("@machine_id", func.TMachineId);
                    cmd.Parameters.AddWithValue("@big_button", func.BigButton);
                    cmd.Parameters.AddWithValue("@Payment_id", func.PaymentId);
                    cmd.Parameters.AddWithValue("@pay_type", func.PayType);
                    cmd.Parameters.AddWithValue("@pay_sub_type", func.PaySubType);
                    cmd.Parameters.AddWithValue("@is_groupby_dept", func.IsGroupByDept);
                    cmd.Parameters.AddWithValue("@is_groupby_course", func.IsGroupByCourse);
                    cmd.Parameters.AddWithValue("@dept_id", func.DeptId);
                    cmd.Parameters.AddWithValue("@course_id", func.CourseId);
                    cmd.Parameters.AddWithValue("@Panel_id", func.Panel_Id);
                    cmd.Parameters.AddWithValue("@Parent_id", func.ParentId);
                    cmd.Parameters.AddWithValue("@mainfunction_id", func.mainfunction_id);
                    cmd.Parameters.AddWithValue("@platformAdd", func.PlatformAdd);
                    cmd.Parameters.AddWithValue("@clientToken", func.ClientToken);
                    cmd.Parameters.AddWithValue("@accessToken", func.AccessToken);
                    cmd.Parameters.AddWithValue("@serviceid", func.ServiceId);
                    cmd.Parameters.AddWithValue("@tax_id", func.TaxId);
                    cmd.Parameters.AddWithValue("@EOHelpOut_max_amount_each", func.EOHelpOutMaxAmountEach);
                    cmd.Parameters.AddWithValue("@Payment_Handling_Fee", func.PaymentHandlingFee);
                    cmd.Parameters.AddWithValue("@Sales_Handling_Fee", func.SalesHandlingFee);
                    cmd.Parameters.AddWithValue("@Tax_Amount", func.TaxAmount);
                    cmd.Parameters.AddWithValue("@Total_Value", func.TotalValue);
                    cmd.Parameters.AddWithValue("@profile_id", func.ProfileId);
                    cmd.Parameters.AddWithValue("@DefaultDateTime", func.DefaultDateTime+"_"+func.ExpireDateTime);
                    cmd.Parameters.AddWithValue("@ZR_VenueID", func.ZrVenueId);
                    cmd.Parameters.AddWithValue("@ZR_LocationID", func.ZrLocationId);
                    cmd.Parameters.AddWithValue("@ZR_TillID", func.ZrTillId);
                    cmd.Parameters.AddWithValue("@CardPayType", func.CardPayType);
                    //cmd.Parameters.AddWithValue("@ExpireDateTime", func.ExpireDateTime);
                    cmd.Parameters.AddWithValue("@tran_Type", "U");

                    
                    //newFunctionId = Convert.ToInt32(cmd.ExecuteScalar());
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        

        [HttpPost("HandleButtonClick")]

         //public JsonResult HandleButtonClick([FromBody] ButtonRequestModel request)
        public JsonResult HandleButtonClick([FromBody] ButtonRequestModel request, string additionalParameter)
        {
            //if (request == null)
            //{
            //    return Json(new { error = "Invalid data received" });
            //}
            //Console.WriteLine("Received Button Code: " + request.buttonId);
            //return Json(new { buttonCode = request.buttonId });
        
            DataTable Data_dt = new DataTable();
            string buttonId = request.buttonId;
            string name = request.name;
            string back_color = request.back_color;    // From 'backColor'
            string for_color = request.for_color;     // From 'foreColor'
            string is_active = request.is_active;
            string big_button = request.big_button;
            string Function_Id = request.Function_Id;
            string Panel_Id = request.Panel_Id;
            string caption_type = request.caption_type;
            string item = request.item;
            string PlatformAdd = request.PlatformAdd;
            string Dept_Id = request.Dept_Id;
            string Couse_Id = request.Couse_Id;
            string DefaultDateTime = request.DefaultDateTime;
            string ProfileId = request.ProfileId;
            bool IsGroupByCourse = request.IsGroupByCourse;
            bool IsGroupByDept = request.IsGroupByCourse;
            string parentId = request.parentId;
            Data_dt.Columns.Add("code", typeof(string));
            Data_dt.Columns.Add("name", typeof(string));
            Data_dt.Columns.Add("back_color", typeof(string));
            Data_dt.Columns.Add("for_color", typeof(string));
            Data_dt.Columns.Add("is_active", typeof(string));
            Data_dt.Columns.Add("big_button", typeof(string));
            Data_dt.Columns.Add("Function_Id", typeof(string));
            Data_dt.Columns.Add("ProfileId", typeof(string));
            Data_dt.Columns.Add("DefaultDateTime", typeof(string));
            Data_dt.Columns.Add("Panel_Id", typeof(string));
            Data_dt.Columns.Add("caption_type", typeof(string));
            Data_dt.Columns.Add("item", typeof(string));
            Data_dt.Columns.Add("PlatformAdd", typeof(string));
            Data_dt.Columns.Add("Dept_Id", typeof(string));
            Data_dt.Columns.Add("Couse_Id", typeof(string));
            Data_dt.Columns.Add("IsGroupByCourse", typeof(string));
            Data_dt.Columns.Add("IsGroupByDept", typeof(string));
            Data_dt.Columns.Add("parentId", typeof(string));
            DataRow row1 = Data_dt.NewRow();
            row1["code"] = request.buttonId;
            row1["name"] = request.name;
            string button_Id = request.buttonId.ToString();
            //HttpContext.Session.SetString("buttonId", button_Id);
            row1["back_color"] = request.back_color;
            row1["for_color"] = request.for_color;
            row1["is_active"] = request.is_active;
            row1["big_button"] = request.big_button;
            row1["Function_Id"] = request.Function_Id;
            row1["Panel_Id"] = request.Panel_Id;
            row1["caption_type"] = request.caption_type;
            row1["item"] = request.item;
            row1["PlatformAdd"] = request.PlatformAdd;
            row1["Dept_Id"] = request.Dept_Id;
            row1["Couse_Id"] = request.Couse_Id;
            row1["DefaultDateTime"] = request.DefaultDateTime;
            row1["ProfileId"] = request.ProfileId;
            row1["IsGroupByCourse"] = request.IsGroupByCourse;
            row1["IsGroupByDept"] = request.IsGroupByDept;
            row1["parentId"] = request.parentId;
            Console.WriteLine("Received Button Code: " + request.buttonId);
            
            Console.WriteLine("Received Button Name: " + request.name);
            Console.WriteLine("Received Button Color: " + request.for_color);
            Console.WriteLine("Received Button BackColor: " + request.back_color);
            Console.WriteLine("Received Button IsActive: " + request.is_active);
            Console.WriteLine("Received Button BigButton: " + request.big_button);
            Console.WriteLine("Received Button BigButton: " + request.Function_Id);
            Console.WriteLine("Received Button BigButton: " + request.Panel_Id);
            Console.WriteLine("Received Button Caption Type: " + request.caption_type);
            Console.WriteLine("Received Button Item: " + request.item);
            Console.WriteLine("Received Button PlatformAdd: " + request.PlatformAdd);
            Console.WriteLine("Received Button Dept_Id: " + request.Dept_Id);
            Console.WriteLine("Received Button parentId: " + request.parentId);
            Data_dt.Rows.Add(row1);
         

            DataTable dt1 = new DataTable();
             button_Id = request.buttonId.ToString();
            string namee = request.namee;
            dt1.Columns.Add("code", typeof(string));
            dt1.Columns.Add("name", typeof(string));
            DataRow row2 = dt1.NewRow();
            row2["code"] = request.button_Id;
            ViewData["Code"]= request.buttonId;
            row2["name"] = request.namee;
            dt1.Rows.Add(row2);
            // Serialize and save the DataTable to the session (if  needed for later use)
            string serializedData = JsonConvert.SerializeObject(Data_dt);
            HttpContext.Session.SetString("Data_dt", serializedData);
            HttpContext.Session.SetString("Function_Id", Function_Id);
            string serialized_Data = JsonConvert.SerializeObject(dt1);
            HttpContext.Session.SetString("Data_dt", serializedData);
            if (string.IsNullOrEmpty(serializedData))
            {
                return Json(new { error = "Data not available" });
            }
            DataTable Data_dtt = JsonConvert.DeserializeObject<DataTable>(serializedData);
            DataTable message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
            switch (buttonId)
            {
                case "F1":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId); // Pass DataTable as a parameter
                    break;
                case "F2":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F7":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F8":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F9":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F10":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F11":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F12":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F13":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F14":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F15":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F16":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F17":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                case "F18":
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
                default:
                    message = BindData(buttonId, name, for_color, back_color, is_active, big_button, Function_Id, Panel_Id, caption_type, item, Data_dt, namee, PlatformAdd, Dept_Id, Couse_Id, IsGroupByCourse, IsGroupByDept, ProfileId, DefaultDateTime, parentId);
                    break;
            }

            return Json(new
            {
                buttonCode = request.buttonId,
                name = request.name,
                Parent_id = request.Panel_Id,
                back_color = request.back_color,
                for_color = request.for_color,
                is_active = request.is_active,
                big_button = request.big_button,
                Function_Id = request.Function_Id,
                caption_type = request.caption_type,
                item = request.item,
                PlatformAdd = request.PlatformAdd,
                Dept_Id = request.Dept_Id,
                Couse_Id = request.Couse_Id,
                IsGroupByCourse = request.IsGroupByCourse,
                IsGroupByDept = request.IsGroupByDept,
                parentId = request.parentId,
                ProfileId = request.ProfileId
            });

        }


        public DataTable BindData(string buttonId, string name,string for_color, string back_color, string is_active, string big_button,string Function_Id,string Panel_Id, string caption_type,string item, DataTable Data_dt, string namee,string PlatformAdd,string Dept_Id,string Couse_Id, bool IsGroupByCourse, bool IsGroupByDept, string ProfileId, string DefaultDateTime, string parentId)
        {
            //DataTable Data_dt = ViewData["Data_dt"] as DataTable;
            //string str = HttpContext.Session.GetString("conString");
            //clsFunction Function = new clsFunction();
            DataTable result = new DataTable();
            result.Columns.Add("code", typeof(string));
            result.Columns.Add("name", typeof(string));
            result.Columns.Add("back_color", typeof(string));
            result.Columns.Add("for_color", typeof(string));
            result.Columns.Add("is_active", typeof(string));
            result.Columns.Add("big_button", typeof(string));
            result.Columns.Add("Function_Id", typeof(string));
            result.Columns.Add("Panel_Id", typeof(string));
            result.Columns.Add("caption_type", typeof(string));
            result.Columns.Add("item", typeof(string));
            result.Columns.Add("PlatformAdd", typeof(string));
            result.Columns.Add("Dept_Id", typeof(string));
            result.Columns.Add("Couse_Id", typeof(string));
            result.Columns.Add("DefaultDateTime", typeof(string));
            result.Columns.Add("ProfileId", typeof(string));
            result.Columns.Add("parentId", typeof(string));
            result.Columns.Add("IsGroupByCourse", typeof(bool));
            result.Columns.Add("IsGroupByDept", typeof(bool));
            
           
            //string str = HttpContext.Session.GetString("conString");
            //clsFunction Function = new clsFunction();
            foreach (DataRow row in Data_dt.Rows)
            {
                if (row["code"].ToString() == buttonId && row["name"].ToString() == name)
                {
                    // Create a new DataRow in the result DataTable
                    DataRow newRow = result.NewRow();

                    // Populate the new DataRow with the values from the matching row
                    newRow["code"] = row["code"];
                    newRow["name"] = row["name"];
                    newRow["back_color"] = row["back_color"];
                    newRow["for_color"] = row["for_color"];
                    newRow["is_active"] = row["is_active"];
                    newRow["big_button"] = row["big_button"];
                    newRow["Function_Id"] = row["Function_Id"];
                    newRow["Panel_Id"] = row["Panel_Id"];
                    newRow["caption_type"] = row["caption_type"];
                    newRow["item"] = row["item"];
                    newRow["PlatformAdd"] = row["PlatformAdd"];
                    newRow["Dept_Id"] = row["Dept_Id"];
                    newRow["Couse_Id"] = row["Couse_Id"];
                    newRow["ProfileId"] = row["ProfileId"];
                    newRow["DefaultDateTime"] = row["DefaultDateTime"];
                    newRow["IsGroupByCourse"] = row["IsGroupByCourse"];
                    newRow["IsGroupByDept"] = row["IsGroupByDept"];
                    newRow["parentId"] = row["parentId"];
                  

                    // Add the populated DataRow to the result DataTable
                    result.Rows.Add(newRow);
                }
            }


            return result;

        }
     


        public List<SelectListItem> DepSelect_By_Cmp(int? c_id, string conn)
        {
            List<SelectListItem> dept = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    dept.Add(new SelectListItem
                        {
                            Value = rdr["Department_id"].ToString(),
                            Text = rdr["Name"].ToString()
                        });
                     
                }

                con.Close();
            }

            return dept;
        }
        public List<SelectListItem> CourseSelect_By_Cmp(int? c_id, string conn)
        {
            List<SelectListItem> Course = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Course.Add(new SelectListItem
                        {
                            Value = rdr["Course_id"].ToString(),
                            Text = rdr["Name"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Course;
        }
      
        public List<SelectListItem> SelectProfile(int? c_id, string conn)
        {
            List<SelectListItem> Profile = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Profile.Add(new SelectListItem
                        {
                            Value = rdr["profile_id"].ToString(),
                            Text = rdr["profile_name"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Profile;
        }
        
        public List<SelectListItem> SelectPricesByCmp(int? c_id, string conn)
        {
            List<SelectListItem> Prices = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {


                SqlCommand cmd = new SqlCommand("Get_PricesBy_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@mainfunction_id", mainfunctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    // Filter out any name containing 'Dance'

                    Prices.Add(new SelectListItem
                        {
                            Value = rdr["Product_Price_Id"].ToString(),
                            Text = rdr["PPrice"].ToString()
                        });
                     
                }

                con.Close();
            }

            return Prices;
        }
        
     
        //--------------------------------------------------------------------------------------------------------------------
        //[HttpPost]
        //public IActionResult BindFunctionSwap([FromBody] BindFunctionSwapModel swapModel)
        //{
        //    try
        //    {
        //        if (swapModel.SwapStatus == 1)
        //        {
        //            int cmpId = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
        //            int mainfunction_id = Convert.ToInt32(HttpContext.Session.GetString("mainfunction_id"));
        //            string str = HttpContext.Session.GetString("conString");
        //            int machineId = swapModel.SelectedMachineId;

        //            var dtMainFunctionId = BindFunctionSwap(cmpId, Convert.ToInt32(mainfunction_id), str);
        //            if (dtMainFunctionId != null && dtMainFunctionId.Any())
        //            {
        //                //int mainFunctionId = dtMainFunctionId.First().MainFunctionId;

        //                var dtMainFunction = SelectFunctionByCmp(cmpId, mainfunction_id, str);
        //                var editDt = SelectFunction_Details( mainfunction_id, cmpId, str);       //------------------------------------Do it tomorrowwwwwwwww

        //                if (dtMainFunction != null && dtMainFunction.Any())
        //                {
        //                    var mainFunction = dtMainFunction.First();

        //                    // Update Main Function TextBox via ViewModel or return it in JSON
        //                    // For simplicity, we'll skip updating txtMainFun here

        //                    // Initialize buttons
        //                    var buttons = InitializeButtons();

        //                    // Merge edit data with buttons
        //                    foreach (var editRow in editDt)
        //                    {
        //                        var button = buttons.FirstOrDefault(b => b.Code.Equals(editRow.Code, StringComparison.OrdinalIgnoreCase));
        //                        if (button != null)
        //                        {
        //                            button.CmpId = editRow.CmpId;
        //                            button.Name = editRow.Name;
        //                            button.CaptionType = editRow.CaptionType;
        //                            button.is_active = editRow.IsActive;
        //                            button.BackColor = editRow.BackColor;
        //                            button.ForColor = editRow.ForColor;
        //                            button.BigButton = editRow.BigButton;
        //                            // Update other properties as needed
        //                        }
        //                    }

        //                    // Update button visibility and styles based on BigButton flag
        //                    foreach (var button in buttons)
        //                    {
        //                        if (button.BigButton == 1)
        //                        {
        //                            button.Visible = false;
        //                            button.Style = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;";
        //                        }
        //                        else
        //                        {
        //                            button.Visible = true;
        //                            button.Style = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;";
        //                        }
        //                    }

        //                    return Json(new { success = true, buttons });
        //                }
        //            }
        //        }
        //        // Handle other SwapStatus values if needed
        //        return Json(new { success = false, message = "Invalid swap status or no data found." });
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new { success = false, message = "An error occurred while processing your request." });
        //    }
        //}

         
    //    private List<clsFunction> InitializeButtons()
    //    {
    //        var buttons = new List<ButtonModel>();
    //        string val = "F";

    //        for (int i = 1; i <= 30; i++)
    //        {
    //            buttons.Add(new ButtonModel
    //            {
    //                Id = $"btn_F{i}",
    //                Code = $"{val}{i}",
    //                Name = $"{val}{i}",
    //                BackColor = "#1B7BBD",
    //                ForColor = "#FFFFFF",
    //                Visible = true,
    //                Style = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;",
    //                ParentId = 0, // This will be updated based on selectedPanelId
    //                BigButton = 0
    //            });
    //        }

    //        return buttons;
    //    }
    //}

    // Additional Models





        //--------------------------------------------------------------------------------------------------------------------
    //public class DeleteItemModel
    //{
    //    public int FunctionId { get; set; }
    //}
     
     
       
        //[HttpPost]
        //public IActionResult Radeditpanel_SelectedIndexChanged(int cmp_id,int mainfunctionID,string str)
        //{
        //    try
        //    {
        //        // Initialize buttons data
        //        var buttons = InitializeButtons();

        //        // Check if mainfunction_id exists in session
        //        if (HttpContext.Session.GetInt32("mainfunction_id") != null)
        //        {
        //            int cmpId = HttpContext.Session.GetInt32("cmp_id") ?? 0;
        //            int mainFunctionId = HttpContext.Session.GetInt32("mainfunction_id") ?? 0;

        //            // Fetch panel functions from the service
        //            var editData = SelectEditPanelByCmp(cmp_id, mainfunctionID, str);

        //            // Merge editData with buttons
        //            foreach (var editRow in editData)
        //            {
        //                var button = buttons.FirstOrDefault(b => b.Code.Equals(editRow.GetHashCode, StringComparison.OrdinalIgnoreCase));
        //                if (button != null)
        //                {
        //                    button.CmpId = editRow.cmp_id;
        //                    button.Name = editRow.Name;
        //                    button.CaptionType = editRow.CaptionType;
        //                    button.IsActive = editRow.IsActive;
        //                    button.BackColor = editRow.BackColor;
        //                    button.ForColor = editRow.ForColor;
        //                    button.BigButton = editRow.BigButton;
        //                    // Update other properties as needed
        //                }
        //            }

        //            // Update button visibility and styles based on big_button flag
        //            foreach (var button in buttons)
        //            {
        //                if (button.BigButton == 1)
        //                {
        //                    button.Visible = false;
        //                    button.Style = "margin-top: 10px; width: 100%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;";
        //                }
        //                else
        //                {
        //                    button.Visible = true;
        //                    button.Style = "margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;";
        //                }
        //            }

        //            // Return the updated buttons as JSON
        //            return Json(new { success = true, buttons });
        //        }
        //        else
        //        {
        //            // Handle case when mainfunction_id is null
        //            return Json(new { success = false, message = "Main Function ID is not set." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error

        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}


        //public clsLocation Select(int cmpId, int locationId, string connStr, HttpContext httpContext)
        //{
        //    clsLocation location = new clsLocation();

        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Location", con)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.AddWithValue("@cmp_id", cmpId);
        //        cmd.Parameters.AddWithValue("@location_id", locationId);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        if (rdr.Read())
        //        {


        //            //location.country_id = Convert.ToInt32(rdr["Country_id"].ToString());
        //            //location.state_Id = Convert.ToInt32(rdr["state_id"].ToString());
        //            //location.city_ID = Convert.ToInt32(rdr["city_id"].ToString());
        //            location = new clsLocation
        //            {
        //                location_id = Convert.ToInt32(rdr["location_id"]),
        //                name = rdr["name"].ToString(),
        //                code = rdr["code"].ToString(),
        //                address = rdr["address"].ToString(),
        //                country_id = Convert.ToInt32(rdr["Country_id"].ToString()),
        //                state_Id = Convert.ToInt32(rdr["state_id"].ToString()),
        //                city_ID = Convert.ToInt32(rdr["city_id"].ToString()),
        //                venue_id = Convert.ToInt32(rdr["venue_id"].ToString()),
        //                onlinepayment = rdr["onlinepayment"].ToString(),
        //                theme = rdr["theme"].ToString(),
        //                judo_id = rdr["judo_id"].ToString(),
        //                judo_token = rdr["judo_token"].ToString(),
        //                judo_secret = rdr["judo_secret"].ToString(),
        //                Email = rdr["LoctionEmail"].ToString(),
        //                LocationEmail = rdr["LoctionEmail"].ToString(),
        //                LocationContact = rdr["LocationContact"].ToString(),
        //                Contact = rdr["Contact"].ToString(),
        //                min_amount = Convert.ToInt32(rdr["min_amount"]),
        //                is_delivery = rdr["is_delivery"].ToString() == "Yes",
        //                websales_check_schedule = rdr["websales_check_schedule"].ToString() == "Yes",
        //                schedule_required = rdr["schedule_required"].ToString() == "Yes",
        //                schedule_cnc = rdr["schedule_cnc"].ToString() == "Yes",
        //                betweenSlot = rdr["HourlySlot"].ToString() == "Yes",
        //                cashback = rdr["cashback"].ToString() == "Yes",
        //                click_and_collect = rdr["click_and_collect"].ToString() == "Yes",
        //                srv_charge_delivery = rdr["srv_charge_delivery"].ToString() == "Yes",
        //                Order_table = rdr["Order_table"].ToString() == "Yes",
        //                is_active = rdr["Active"].ToString() == "Yes",
        //                is_email = rdr["is_email"].ToString() == "Yes",
        //                Is_Email_APK = rdr["Is_Email_APK"].ToString() == "Yes",
        //                is_skippay = rdr["is_skippay"].ToString() == "Yes",
        //                till_auto_log_off = rdr["till_auto_log_off"].ToString() == "Yes",
        //                is_GetCovers = rdr["is_GetCovers"].ToString() == "Yes",
        //                delivery_charges = Convert.ToInt32(rdr["delivery_charges"]),
        //                service_charges = Convert.ToInt32(rdr["service_charges"]),
        //                BG_Color = rdr["BG_Color"].ToString(),
        //                Font_Color = rdr["Font_Color"].ToString(),
        //                Body_Color = rdr["Body_Color"].ToString(),
        //                CustomPay_Id = rdr["CustomPay_Id"].ToString(),
        //                CustomPay_Secret = rdr["CustomPay_Secret"].ToString(),
        //                CustomPay_Token = rdr["CustomPay_Token"].ToString(),
        //                CustomPay_Base64 = rdr["CustomPay_Base64"].ToString(),
        //                start_date = rdr["start_date"].ToString(),
        //                end_date = rdr["end_date"].ToString(),
        //                CustomPay_url = rdr["CustomPay_url"].ToString(),
        //                clientid = rdr["clientid"].ToString(),
        //                clientsecret = rdr["clientsecret"].ToString(),
        //                redirect_uri = rdr["redirect_uri"].ToString(),
        //                future_booking_days = Convert.ToInt32(rdr["future_booking_days"]),
        //                message_delivery = rdr["message_delivery"].ToString(),
        //                message_order_table = rdr["message_order_table"].ToString(),
        //                header_reciept = rdr["header_reciept"].ToString(),
        //                TipAS = rdr["TipAS"].ToString(),
        //                GC_Btn_img_typ = rdr["GC_Btn_img_typ"].ToString(),
        //                GC_Btn_txt_clr = rdr["GC_Btn_txt_clr"].ToString(),
        //                GC_Btn_stl = rdr["GC_Btn_stl"].ToString(),
        //                GC_Btn_bkgd_clr = rdr["GC_Btn_bkgd_clr"].ToString(),
        //                GC_Btn_fnt_size = rdr["GC_Btn_fnt_size"].ToString(),
        //                GC_Btn_img_typ_pos = rdr["GC_Btn_img_typ_pos"].ToString(),
        //                C_Btn_bkgd_clr = rdr["C_Btn_bkgd_clr"].ToString(),
        //                C_Btn_txt_clr = rdr["C_Btn_txt_clr"].ToString(),
        //                C_Btn_stl = rdr["C_Btn_stl"].ToString(),
        //                C_Btn_img_typ = rdr["C_Btn_img_typ"].ToString(),
        //                C_Btn_fnt_size = rdr["C_Btn_fnt_size"].ToString(),
        //                C_Btn_img_typ_pos = rdr["C_Btn_img_typ_pos"].ToString(),
        //                P_Btn_bkgd_clr = rdr["P_Btn_bkgd_clr"].ToString(),
        //                P_Btn_txt_clr = rdr["P_Btn_txt_clr"].ToString(),
        //                P_Btn_stl = rdr["P_Btn_stl"].ToString(),
        //                P_Btn_img_typ = rdr["P_Btn_img_typ"].ToString(),
        //                P_Btn_fnt_size = rdr["P_Btn_fnt_size"].ToString(),
        //                P_Btn_img_typ_pos = rdr["P_Btn_img_typ_pos"].ToString(),
        //                login_src_bkgd_clr = rdr["login_src_bkgd_clr"].ToString(),
        //                Till_Phn_no = rdr["Till_Phn_no"].ToString(),
        //                Till_url = rdr["Till_url"].ToString(),
        //                login_src_login_btn = rdr["login_src_login_btn"].ToString(),

        //                POS_logo = rdr["POS_logo"] as byte[],
        //                Click_Collect_image = rdr["Click_Collect_image"] as byte[],
        //                Delivery_image = rdr["Delivery_image"] as byte[],
        //                OrderAtTable_image = rdr["OrderAtTable_image"] as byte[],
        //                L_image = rdr["L_image"] as byte[],
        //                GraphicViewBackground = rdr["GraphicViewBackground"] as byte[],
        //                GraphicViewTable = rdr["GraphicViewTable"] as byte[]



        //                //CashflowId = rdr["cashflow_id"].ToString(),
        //                // CashflowUrl = rdr["cashflow_url"].ToString(),
        //                //CashflowApiKey = rdr["cashflow_api_key"].ToString()
        //            //     if (location != null)
        //            //{
        //            //    // Bind text areas
        //            //    //editor.InnerText = location.message_order_table ?? "";
        //            //    //editor1.InnerText = location.message_delivery ?? "";
        //            //    //editor2.InnerText = location.message_order_table ?? "";

        //            //    // Bind color inputs
        //            //    BG_Color.Value = rdr["BG_Color"].ToString() ?? "#ffffff"; // Default to white if null
        //            //    fontColor.Value = location.Font_Color ?? "#000000";     // Default to black if null
        //            //    bodyColor.Value = location.Body_Color ?? "#ffffff";    // Default to white if null
        //            //}


        //        };
        //        }
        //        con.Close();
        //    }

        //    return location;
        //}
        //public void Insert(clsLocation location, string conn, int? country_id, int? state_Id, int? city_ID)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Location", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@location_id", location.location_id);
        //        cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
        //        cmd.Parameters.AddWithValue("@code", location.code);
        //        cmd.Parameters.AddWithValue("@name", location.name);
        //        cmd.Parameters.AddWithValue("@address", location.address);
        //        cmd.Parameters.AddWithValue("@country_id",  country_id);
        //        cmd.Parameters.AddWithValue("@state_id",  state_Id);
        //        cmd.Parameters.AddWithValue("@city_id", city_ID);
        //        cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
        //        cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //        //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //        cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
        //        cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
        //        cmd.Parameters.AddWithValue("@is_active", location.is_active);
        //        cmd.Parameters.AddWithValue("@cashback", location.cashback);
        //        cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
        //        cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
        //        cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
        //        cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
        //        cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
        //        cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
        //        cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
        //        cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
        //        cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
        //        cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
        //        cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
        //        cmd.Parameters.AddWithValue("@is_email", location.is_email);
        //        cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);

        //        cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
        //        cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
        //        cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
        //        cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
        //        cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);


        //            string startTimeFormatted = !string.IsNullOrEmpty(location.start_date)? location.start_date : "00:00"; // Default to "00:00" if empty or null

        //            string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";  

        //            cmd.Parameters.AddWithValue("@start_date",startTimeFormatted);
        //        cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
        //        cmd.Parameters.AddWithValue("@Email", location.Email);
        //        cmd.Parameters.AddWithValue("@Contact", location.Contact);
        //            //string selectedColor = Request.Form["txt_clr_GC"];
        //            //if (string.IsNullOrEmpty(selectedColor))
        //            //{
        //            //    // Handle missing color (e.g., use a default color)
        //            //    selectedColor = "#000000"; // Default black
        //            //}
        //            cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);
        //            if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
        //            {
        //                location.BG_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
        //            {
        //                location.Font_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
        //            {
        //                location.Body_Color = "#0000FF";
        //            }
        //            cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
        //        cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
        //        cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
        //        cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
        //        cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
        //        cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
        //        cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
        //        cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

        //        // Add optional parameters


        //        //if (location.OrderAtTable_image != null)
        //        //{
        //        //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
        //        //}

        //        //if (location.L_image != null)
        //        //{
        //        //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
        //        //}

        //        cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
        //        cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
        //        //cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
        //        cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
        //        cmd.Parameters.AddWithValue("@theme", location.theme);
        //        cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

        //        //if (location.GraphicViewBackground != null)
        //        //{
        //        //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
        //        //}

        //        //if (location.GraphicViewTable != null)
        //        //{
        //        //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
        //        //}

        //        cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
        //        cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
        //        cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
        //        cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
        //        cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
        //        cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
        //        cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
        //        cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

        //        cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
        //        cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
        //        cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
        //        cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
        //        cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
        //        cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
        //        cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
        //        cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
        //        cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

        //        cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
        //        cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
        //        cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
        //        cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
        //       // cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

        //        cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
        //        cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
        //        cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
        //        cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
        //        cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

        //        cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
        //        cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
        //        cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
        //        cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
        //        cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
        //        //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

        //        cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
        //        cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
        //        cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
        //        //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

        //        cmd.Parameters.AddWithValue("@login_src_bkgd_clr", location.login_src_bkgd_clr);
        //        cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
        //        cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);               //////////////////////////////////wanna add this fields 
        //        cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);

        //        //if (location.POS_logo != null)
        //        //{
        //        //    cmd.Parameters.AddWithValue("@POS_logo", location.POS_logo);
        //        //}

        //            cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;

        //            //if (location.Click_Collect_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
        //            //}

        //            //if (location.Delivery_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
        //            //}

        //            cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;


        //            cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;

        //            cmd.Parameters.AddWithValue("@Tran_Type", "I");
        //            con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }

        //}

        //public void Update(clsLocation location, string conn, int? country_id, int? state_Id, int? city_ID,int location_id)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(conn))
        //        {
        //            SqlCommand cmd = new SqlCommand("P_M_Location", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@location_id", location.location_id);
        //            cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
        //            cmd.Parameters.AddWithValue("@code", location.code);
        //            cmd.Parameters.AddWithValue("@name", location.name);
        //            cmd.Parameters.AddWithValue("@address", location.address);
        //            cmd.Parameters.AddWithValue("@country_id", country_id);
        //            cmd.Parameters.AddWithValue("@state_id", state_Id);
        //            cmd.Parameters.AddWithValue("@city_id", city_ID);
        //            cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
        //            cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //            //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //            cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
        //            cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
        //            cmd.Parameters.AddWithValue("@is_active", location.is_active);
        //            cmd.Parameters.AddWithValue("@cashback", location.cashback);
        //            cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
        //            cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
        //            cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
        //            cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
        //            cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
        //            cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
        //            cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
        //            cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
        //            cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
        //            cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
        //            cmd.Parameters.AddWithValue("@is_email", location.is_email);

        //            cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
        //            cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
        //            cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
        //            cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
        //            cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);
        //            string startTimeFormatted = !string.IsNullOrEmpty(location.start_date) ? location.start_date : "00:00"; // Default to "00:00" if empty or null

        //            string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";

        //            cmd.Parameters.AddWithValue("@start_date", startTimeFormatted);
        //            cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
        //            cmd.Parameters.AddWithValue("@Email", location.Email);
        //            cmd.Parameters.AddWithValue("@Contact", location.Contact);


        //            if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
        //            {
        //                location.BG_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
        //            {
        //                location.Font_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
        //            {
        //                location.Body_Color = "#0000FF";
        //            }
        //            cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
        //            cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
        //            cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
        //            cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
        //            cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
        //            cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
        //            cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
        //            cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

        //            // Add optional parameters
        //            //if (location.Click_Collect_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
        //            //}

        //            //if (location.Delivery_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
        //            //}

        //            //if (location.OrderAtTable_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
        //            //}

        //            //if (location.L_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
        //            //}

        //            cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
        //            cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
        //            cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
        //            cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
        //            cmd.Parameters.AddWithValue("@theme", location.theme);
        //            cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

        //            //if (location.GraphicViewBackground != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
        //            //}

        //            //if (location.GraphicViewTable != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
        //            //}

        //            cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
        //            cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
        //            cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
        //            cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
        //            cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
        //            cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
        //            cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
        //            cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

        //            cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
        //            cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
        //            cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
        //            cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
        //            cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
        //            cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
        //            cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
        //            cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
        //            cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

        //            cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
        //            cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

        //            cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
        //            cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

        //            cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
        //            cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
        //            //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

        //            cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
        //            cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
        //            cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
        //            //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

        //            cmd.Parameters.AddWithValue("@login_src_bkgd_clr", "");   //////////////////////////////////wanna add this fields 
        //            cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
        //            cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);
        //            cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);
        //            cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;
        //            //if (Request?.Form != null && Request?.Form.Files != null)
        //            //{
        //            //    // For Click and Collect image
        //            //    if (Request.Form.Files["CACFile"] != null && Request.Form.Files["CACFile"].Length > 0)
        //            //    {
        //            //        var file = Request.Form.Files["CACFile"];
        //            //        using (var ms = new MemoryStream())
        //            //        {
        //            //            file.CopyTo(ms);
        //            //            location.Click_Collect_image = ms.ToArray(); // Save the new file
        //            //        }
        //            //    }
        //            //    else if (location.Click_Collect_image == null)
        //            //    {
        //            //        // Retain the old image if no new image is uploaded
        //            //        var base64Image = ViewData["Base64CACImage"]?.ToString();
        //            //        if (!string.IsNullOrEmpty(base64Image))
        //            //        {
        //            //            location.Click_Collect_image = Convert.FromBase64String(base64Image);
        //            //        }
        //            //    }

        //            //    // Repeat similar code for other image fields
        //            //    if (Request.Form.Files["DeliveryimageFile"] != null && Request.Form.Files["DeliveryimageFile"].Length > 0)
        //            //    {
        //            //        var file = Request.Form.Files["DeliveryimageFile"];
        //            //        using (var ms = new MemoryStream())
        //            //        {
        //            //            file.CopyTo(ms);
        //            //            location.Delivery_image = ms.ToArray();
        //            //        }
        //            //    }
        //            //    else if (location.Delivery_image == null)
        //            //    {
        //            //        var base64Image = ViewData["Base64DeliveryImage"]?.ToString();
        //            //        if (!string.IsNullOrEmpty(base64Image))
        //            //        {
        //            //            location.Delivery_image = Convert.FromBase64String(base64Image);
        //            //        }
        //            //    }

        //            //    // Repeat for other images like Order At Table image, ULD image, etc.
        //            //}





        //            cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;
        //            cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;

        //            cmd.Parameters.AddWithValue("@Tran_Type", "U");
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }

        //}

        //public void Delete(clsLocation location, string connStr, string ip_address)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connStr))
        //        {
        //            SqlCommand cmd = new SqlCommand("P_M_Location", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@location_id", location.location_id);
        //            cmd.Parameters.AddWithValue("@cmp_id", location.cmp_id);
        //            cmd.Parameters.AddWithValue("@code", location.code);
        //            cmd.Parameters.AddWithValue("@name", location.name);
        //            cmd.Parameters.AddWithValue("@address", location.address);
        //            cmd.Parameters.AddWithValue("@country_id", location.country_id);
        //            cmd.Parameters.AddWithValue("@state_id", location.state_Id);
        //            cmd.Parameters.AddWithValue("@city_id", location.city_ID);
        //            cmd.Parameters.AddWithValue("@ip_address", location.ip_address);
        //            cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //            //cmd.Parameters.AddWithValue("@venue_id", location.venue_id);
        //            cmd.Parameters.AddWithValue("@till_auto_log_off", location.till_auto_log_off);
        //            cmd.Parameters.AddWithValue("@machine_id", location.machine_id);
        //            cmd.Parameters.AddWithValue("@is_active", location.is_active);
        //            cmd.Parameters.AddWithValue("@cashback", location.cashback);
        //            cmd.Parameters.AddWithValue("@srv_charge_clickcollect", location.click_and_collect);
        //            cmd.Parameters.AddWithValue("@srv_charge_kiosk", location.srv_charge_kiosk);
        //            cmd.Parameters.AddWithValue("@srv_charge_order", location.Order_table);
        //            cmd.Parameters.AddWithValue("@srv_charge_delivery", location.srv_charge_delivery);
        //            cmd.Parameters.AddWithValue("@min_payment", location.min_amount);
        //            cmd.Parameters.AddWithValue("@is_delivery", location.is_delivery);
        //            cmd.Parameters.AddWithValue("@judo_id", location.judo_id);
        //            cmd.Parameters.AddWithValue("@judo_token", location.judo_token);
        //            cmd.Parameters.AddWithValue("@judo_secret", location.judo_secret);
        //            cmd.Parameters.AddWithValue("@is_skipPay", location.is_skippay);
        //            cmd.Parameters.AddWithValue("@is_email", location.is_email);

        //            cmd.Parameters.AddWithValue("@cashflow_id", location.CashflowId);
        //            cmd.Parameters.AddWithValue("@cashflow_url", location.CashflowUrl);
        //            cmd.Parameters.AddWithValue("@cashflow_api_key", location.CashflowApiKey);
        //            cmd.Parameters.AddWithValue("@onlinepayment", location.onlinepayment);
        //            cmd.Parameters.AddWithValue("@websales_check_schedule", location.websales_check_schedule);
        //            string startTimeFormatted = !string.IsNullOrEmpty(location.start_date) ? location.start_date : "00:00"; // Default to "00:00" if empty or null

        //            string endTimeFormatted = !string.IsNullOrEmpty(location.end_date) ? location.end_date : "00:00";

        //            cmd.Parameters.AddWithValue("@start_date", startTimeFormatted);
        //            cmd.Parameters.AddWithValue("@end_date", endTimeFormatted);
        //            cmd.Parameters.AddWithValue("@Email", location.Email);
        //            cmd.Parameters.AddWithValue("@Contact", location.Contact);


        //            if (string.IsNullOrEmpty(location.BG_Color) || location.BG_Color == "#000000")
        //            {
        //                location.BG_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Font_Color) || location.Font_Color == "#000000")
        //            {
        //                location.Font_Color = "#0000FF";
        //            }

        //            if (string.IsNullOrEmpty(location.Body_Color) || location.Body_Color == "#000000")
        //            {
        //                location.Body_Color = "#0000FF";
        //            }
        //            cmd.Parameters.AddWithValue("@BG_Color", location.BG_Color);
        //            cmd.Parameters.AddWithValue("@Font_Color", location.Font_Color);
        //            cmd.Parameters.AddWithValue("@Body_Color", location.Body_Color);
        //            cmd.Parameters.AddWithValue("@header_reciept", location.header_reciept);
        //            cmd.Parameters.AddWithValue("@message_delivery", location.message_delivery);
        //            cmd.Parameters.AddWithValue("@message_order_table", location.message_order_table);
        //            cmd.Parameters.AddWithValue("@schedule_required", location.schedule_required);
        //            cmd.Parameters.AddWithValue("@schedule_cnc", location.schedule_cnc);

        //            // Add optional parameters
        //            //if (location.Click_Collect_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Click_Collect_image", location.Click_Collect_image);
        //            //}

        //            //if (location.Delivery_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@Delivery_image", location.Delivery_image);
        //            //}

        //            //if (location.OrderAtTable_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@OrderAtTable_image", location.OrderAtTable_image);
        //            //}

        //            //if (location.L_image != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@L_image", location.L_image);
        //            //}

        //            cmd.Parameters.AddWithValue("@click_and_collect", location.click_and_collect);
        //            cmd.Parameters.AddWithValue("@Order_table", location.Order_table);
        //            cmd.Parameters.AddWithValue("@delivery_charges", location.delivery_charges);
        //            cmd.Parameters.AddWithValue("@service_charges", location.service_charges);
        //            cmd.Parameters.AddWithValue("@theme", location.theme);
        //            cmd.Parameters.AddWithValue("@tipAs", location.TipAS);

        //            //if (location.GraphicViewBackground != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@GraphicViewBackground", location.GraphicViewBackground);
        //            //}

        //            //if (location.GraphicViewTable != null)
        //            //{
        //            //    cmd.Parameters.AddWithValue("@GraphicViewTable", location.GraphicViewTable);
        //            //}

        //            cmd.Parameters.AddWithValue("@HourlySlot", location.HourlySlot);
        //            cmd.Parameters.AddWithValue("@CustomPay_id", location.CustomPayId);
        //            cmd.Parameters.AddWithValue("@CustomPay_secret", location.CustomPaySecret);
        //            cmd.Parameters.AddWithValue("@CustomPay_token", location.CustomPayToken);
        //            cmd.Parameters.AddWithValue("@CustomPay_Base64", location.CustomPayBase64);
        //            cmd.Parameters.AddWithValue("@CustomPay_url", location.CustomPayUrl);
        //            cmd.Parameters.AddWithValue("@Is_Email_APK", location.Is_Email_APK);
        //            cmd.Parameters.AddWithValue("@Is_GetCovers", location.is_GetCovers);

        //            cmd.Parameters.AddWithValue("@Table_As_Box", "Table");
        //            cmd.Parameters.AddWithValue("@Client_ID", location.clientid);
        //            cmd.Parameters.AddWithValue("@clientsecret", location.clientsecret);
        //            cmd.Parameters.AddWithValue("@redirect_uri", location.redirect_uri);
        //            cmd.Parameters.AddWithValue("@gtway_MerchantID", location.gtway_MerchantID);
        //            cmd.Parameters.AddWithValue("@gtway_StoreID", location.gtway_StoreID);
        //            cmd.Parameters.AddWithValue("@gtway_LocationID", location.gtway_LocationID);
        //            cmd.Parameters.AddWithValue("@gtway_StoreName", location.gtway_StoreName);
        //            cmd.Parameters.AddWithValue("@gtway_LocationName", location.gtway_LocationName);

        //            cmd.Parameters.AddWithValue("@GC_Btn_stl", location.GC_Btn_stl);
        //            cmd.Parameters.AddWithValue("@GC_Btn_img_typ", location.GC_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@GC_Btn_fnt_size", location.GC_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@GC_Btn_bkgd_clr", location.GC_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@GC_Btn_txt_clr", location.GC_Btn_txt_clr);

        //            cmd.Parameters.AddWithValue("@C_Btn_stl", location.C_Btn_stl);
        //            cmd.Parameters.AddWithValue("@C_Btn_img_typ", location.C_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@C_Btn_fnt_size", location.C_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@C_Btn_bkgd_clr", location.C_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@C_Btn_txt_clr", location.C_Btn_txt_clr);

        //            cmd.Parameters.AddWithValue("@P_Btn_stl", location.P_Btn_stl);
        //            cmd.Parameters.AddWithValue("@P_Btn_img_typ", location.P_Btn_img_typ);
        //            cmd.Parameters.AddWithValue("@P_Btn_fnt_size", location.P_Btn_fnt_size);
        //            cmd.Parameters.AddWithValue("@P_Btn_bkgd_clr", location.P_Btn_bkgd_clr);
        //            cmd.Parameters.AddWithValue("@P_Btn_txt_clr", location.P_Btn_txt_clr);
        //            //cmd.Parameters.AddWithValue("@LocationContact", location.LocationContact);

        //            cmd.Parameters.AddWithValue("@GC_Btn_img_typ_pos", location.GC_Btn_img_typ_pos);
        //            cmd.Parameters.AddWithValue("@C_Btn_img_typ_pos", location.C_Btn_img_typ_pos);
        //            cmd.Parameters.AddWithValue("@P_Btn_img_typ_pos", location.P_Btn_img_typ_pos);
        //            //cmd.Parameters.AddWithValue("@LocationEmail", location.LocationEmail);

        //            cmd.Parameters.AddWithValue("@login_src_bkgd_clr", "");   //////////////////////////////////wanna add this fields 
        //            cmd.Parameters.AddWithValue("@login_src_login_btn", location.login_src_login_btn);
        //            cmd.Parameters.AddWithValue("@Till_url", string.IsNullOrEmpty(location.Till_url) ? "" : location.Till_url);
        //            cmd.Parameters.AddWithValue("@Till_Phn_no", location.Till_Phn_no);
        //            cmd.Parameters.Add("@Click_Collect_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Click_Collect_image"].Value = (object)location.Click_Collect_image ?? DBNull.Value;
        //            //if (Request?.Form != null && Request?.Form.Files != null)
        //            //{
        //            //    // For Click and Collect image
        //            //    if (Request.Form.Files["CACFile"] != null && Request.Form.Files["CACFile"].Length > 0)
        //            //    {
        //            //        var file = Request.Form.Files["CACFile"];
        //            //        using (var ms = new MemoryStream())
        //            //        {
        //            //            file.CopyTo(ms);
        //            //            location.Click_Collect_image = ms.ToArray(); // Save the new file
        //            //        }
        //            //    }
        //            //    else if (location.Click_Collect_image == null)
        //            //    {
        //            //        // Retain the old image if no new image is uploaded
        //            //        var base64Image = ViewData["Base64CACImage"]?.ToString();
        //            //        if (!string.IsNullOrEmpty(base64Image))
        //            //        {
        //            //            location.Click_Collect_image = Convert.FromBase64String(base64Image);
        //            //        }
        //            //    }

        //            //    // Repeat similar code for other image fields
        //            //    if (Request.Form.Files["DeliveryimageFile"] != null && Request.Form.Files["DeliveryimageFile"].Length > 0)
        //            //    {
        //            //        var file = Request.Form.Files["DeliveryimageFile"];
        //            //        using (var ms = new MemoryStream())
        //            //        {
        //            //            file.CopyTo(ms);
        //            //            location.Delivery_image = ms.ToArray();
        //            //        }
        //            //    }
        //            //    else if (location.Delivery_image == null)
        //            //    {
        //            //        var base64Image = ViewData["Base64DeliveryImage"]?.ToString();
        //            //        if (!string.IsNullOrEmpty(base64Image))
        //            //        {
        //            //            location.Delivery_image = Convert.FromBase64String(base64Image);
        //            //        }
        //            //    }

        //            //    // Repeat for other images like Order At Table image, ULD image, etc.
        //            //}





        //            cmd.Parameters.Add("@Delivery_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@Delivery_image"].Value = (object)location.Delivery_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@OrderAtTable_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@OrderAtTable_image"].Value = (object)location.OrderAtTable_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@L_image", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@L_image"].Value = (object)location.L_image ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewBackground", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewBackground"].Value = (object)location.GraphicViewBackground ?? DBNull.Value;

        //            cmd.Parameters.Add("@GraphicViewTable", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@GraphicViewTable"].Value = (object)location.GraphicViewTable ?? DBNull.Value;
        //            cmd.Parameters.Add("@POS_logo", SqlDbType.VarBinary); // Ensure you're using the right SQL type
        //            cmd.Parameters["@POS_logo"].Value = (object)location.POS_logo ?? DBNull.Value;

        //            cmd.Parameters.AddWithValue("@Tran_Type", "D");
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }

        //}


    }
}
