using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Converted_POS.Pages.forms
{
    public class Function_MasterModel : PageModel
    {
        
        private readonly IConfiguration _configuration;
        private readonly FunctionController obj;
        public Function_MasterModel(IConfiguration configuration)
        {
            _configuration = configuration;
            obj = new FunctionController(_configuration);
        }
        [BindProperty]
        public clsFunction Function { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> DTMachine { get; set; }
        public List<SelectListItem> DTTill { get; set; }
        public List<SelectListItem> DTPanel { get; set; }
        public List<SelectListItem> DTParent { get; set; }
        public List<SelectListItem> DTFunction { get; set; }
        public List<SelectListItem> DTTax { get; set; }
        public List<SelectListItem> DTdepartment { get; set; }
        public List<SelectListItem> DTCourse{ get; set; }
        public List<SelectListItem> DTPayment{ get; set; }
        public List<SelectListItem> DTProfile{ get; set; }
        public List<SelectListItem> DTPricelevel{ get; set; }
        public List<SelectListItem> DTLocation{ get; set; }
        public List<SelectListItem> DTVenue{ get; set; }
        public List<SelectListItem> DTPamentType { get; set; }

        public List<SelectListItem> DTAccessBackOfficeRole { get; set; }
        public IActionResult OnGet(int? id)
        {  
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            HttpContext.Session.SetString("mainfunction_id", (id ?? 0).ToString());
            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");
            BindFunctionSwap(Convert.ToInt32(cmp_id), ConnStr);
            if (Function == null)
            {
                Function = new clsFunction(); 
            }

            // Now safely access MachineId
            int TMachineId = Function.TMachineId ?? 0;
            int machineId = Function.TMachineId ?? 0;
            Function.mainfunction_id = Convert.ToInt32(id ?? 0);

            DTMachine = obj.GetMachine(Convert.ToInt32(cmp_id), ConnStr, machineId) ?? new List<SelectListItem>();
            DTTax = obj.GetTax(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTTill = obj.GetTill(Convert.ToInt32(cmp_id), ConnStr, TMachineId) ?? new List<SelectListItem>();
            int main = Convert.ToInt32(HttpContext.Session.GetString("mainfunction_id"));
            DTPanel = obj.SelectEditPanelByCmp(Convert.ToInt32(cmp_id), Convert.ToInt32(id), ConnStr) ?? new List<SelectListItem>();
            DTParent = obj.SelectParentFunctionByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTFunction = obj.SelectFunctionByCmp(Convert.ToInt32(cmp_id),  ConnStr) ?? new List<SelectListItem>();
            DTdepartment = obj.DepSelect_By_Cmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTCourse = obj.CourseSelect_By_Cmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTPayment = obj.SelectPayment(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTProfile = obj.SelectProfile(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTPricelevel = obj.SelectPricesByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            //DTLocation = obj.SelectLocation(Convert.ToInt32(Function.ZrVenueId),Convert.ToInt32(Function?.ZrVenueId), ConnStr) ?? new List<SelectListItem>();
            DTVenue = obj.SelectVenueByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTPamentType = obj.SelectCardPaymentTyp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();

            Function = new clsFunction
            {
               // till_code = nextTillCode
            };

            Function = obj.Select(Convert.ToInt32((id ?? 0).ToString()), Convert.ToInt32(cmp_id), ConnStr);
            if (id.HasValue)
            {
                HttpContext.Session.SetString("mainfunction_id", id.Value.ToString());
                DataTable DT1= (DataTable)ViewData["Data_dt"];
           
                Function = obj.Select(Convert.ToInt32(id.Value.ToString()), Convert.ToInt32(cmp_id),ConnStr);
                if (Function == null)
                {
                    return NotFound();
                }

                var dig = ViewData["Data_dt"] as string;
                ViewData["Data_dt"] = Function.dt as DataTable;
                if (ViewData["Data_dt"] is DataTable dt && dt.Rows.Count > 0)
                {
                    Function.Mfunction_id = Convert.ToInt32(dt.Rows[0]["function_id"].ToString());
                 
                }
                var serializedData = HttpContext.Session.GetString("Data_dt");
            }
            string sessionFunctionId = HttpContext.Session.GetString("mainfunction_id");
            

            Function.mainfunction_id = (int?)int.Parse(sessionFunctionId);
           
            int? selectedFunctionId = string.IsNullOrEmpty(sessionFunctionId) ? Function.mainfunction_id : (int?)int.Parse(sessionFunctionId);

            // Use the selectedCategoryId for further logic
            if (selectedFunctionId.HasValue)
            {
                Function.TMachineId = selectedFunctionId.Value;
            }
            if (id != null)
            {
                Function = obj.Select(Convert.ToInt32((id ?? 0).ToString()), Convert.ToInt32(cmp_id), ConnStr);

            }
            // DTAccessBackOfficeRole = obj.GetAccessRole(Convert.ToInt32(cmp_id), ConnStr);
            ViewData["Function"] = Function;
            //var dataJson = ViewBag.Data_dt;
            return Page();
        }

        //[HttpPost]
        //public JsonResult OnPostSaveSelectedData([FromBody] SaveDataModel requestData)
        //{
        //    try
        //    {
        //        foreach (var item in requestData.SelectedData)
        //        {
        //            // Save each item to database or file
        //            Console.WriteLine($"Saving: {item.Name} ({item.Code})");
        //        }

        //        return new JsonResult(new { success = true, message = "Data saved successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { success = false, message = "Error: " + ex.Message });
        //    }
        //}

        //// Model Class
        //public class SaveDataModel
        //{
        //    public List<ButtonDataModel> SelectedData { get; set; }
        //}

        //public class ButtonDataModel
        //{
        //    public string Code { get; set; }
        //    public string Name { get; set; }
        //    public string Back_Color { get; set; }
        //    public string For_Color { get; set; }
        //    public string Is_Active { get; set; }
        //    public string Big_Button { get; set; }
        //    public string Function_Id { get; set; }
        //    public string Caption_Type { get; set; }
        //    public string Item { get; set; }
        //}

        [HttpPost("Save")]
        public IActionResult OnPost(int?  id)
        {
            Function.mainfunction_id = Convert.ToInt32(HttpContext.Session.GetString("mainfunction_id"));
            Function.CmpId = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            DTMachine = obj.GetMachine(Convert.ToInt32(Function.CmpId), ConnStr, Convert.ToInt32(Function.MachineId)) ?? new List<SelectListItem>();
            DTTax = obj.GetTax(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTTill = obj.GetTill(Convert.ToInt32(Function.CmpId), ConnStr, Convert.ToInt32(Function.TMachineId)) ?? new List<SelectListItem>();
            ViewData["DTTill"] = DTTill;
            Function.DTTill = DTTill;
            DTPanel = obj.SelectEditPanelByCmp(Convert.ToInt32(Function.CmpId), Convert.ToInt32(Function.mainfunction_id) , ConnStr) ?? new List<SelectListItem>();
            DTParent = obj.SelectParentFunctionByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTFunction = obj.SelectFunctionByCmp(Convert.ToInt32(cmp_id),   ConnStr) ?? new List<SelectListItem>();
            DTPayment = obj.SelectPayment(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTProfile = obj.SelectProfile(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTPricelevel = obj.SelectPricesByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
           // DTLocation = obj.SelectLocation(Convert.ToInt32(cmp_id),Convert.ToInt32(Function.ZrVenueId), ConnStr) ?? new List<SelectListItem>();
            DTVenue = obj.SelectVenueByCmp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            DTPamentType = obj.SelectCardPaymentTyp(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Function.Function_Id = HttpContext.Session?.GetString("Function_Id") ?? string.Empty;
            if (Function.mainfunction_id == null || Function.mainfunction_id == 0)
            {
                Function.mainfunction_id = 0;
                obj.Insert(Function, ConnStr ,ip_address);
                Function.mainfunction_id = Function.mainfunction_id;
                return RedirectToPage("/Configuration/Function_Master", new { id = Function.mainfunction_id });
                Function.Code = HttpContext.Request.Form["ButtonID"].ToString();
                string buttonId = HttpContext.Session?.GetString("buttonId") ?? string.Empty;
                int Function_Id = Convert.ToInt32(HttpContext.Session?.GetString("Function_Id"));
                obj.InsertFunctionDetails(Function, ConnStr, ip_address, buttonId, Function_Id);
                HttpContext.Session.Remove("buttonId");
            }
            else
            {
                //Function.Code = HttpContext.Request.Form["ButtonID"].ToString();
                //obj.UpdateMainFunction(Function.mainfunction_id, Function, ConnStr, ip_address);
                //obj.InsertFunctionDetails(Function, ConnStr, ip_address);
                if (Function.Function_Id == null || Function.Function_Id == "0")
                {

                    //obj.Insert(Function, ConnStr, ip_address);
                    //Function.mainfunction_id = Function.mainfunction_id;
                    //return RedirectToPage("/Configuration/Function_Master", new { id = Function.mainfunction_id });
                    //Function.Code = HttpContext.Request.Form["ButtonID"].ToString();
                    string buttonId = HttpContext.Session?.GetString("buttonId") ?? string.Empty;
                    int Function_Id = Convert.ToInt32(HttpContext.Session?.GetString("Function_Id"));
                    obj.InsertFunctionDetails(Function, ConnStr, ip_address, buttonId, Function_Id);
                    HttpContext.Session.Remove("buttonId");
                    HttpContext.Session.Remove("Function_Id");
                }
                else
                {
                    int Function_Id = Convert.ToInt32(HttpContext.Session?.GetString("Function_Id"));
                    string buttonId = HttpContext.Session?.GetString("buttonId") ?? string.Empty;
                    obj.updateFunctionDetails(Function, ConnStr, ip_address, buttonId, Function_Id);
                    HttpContext.Session.Remove("buttonId");
                    HttpContext.Session.Remove("Function_Id");
                }
                   
            }

          //  HttpContext.Session.Remove("mainfunction_id");

            //return RedirectToPage("/Configuration/Function_Master", new { id = Function.mainfunction_id });

           return RedirectToPage("/Configuration/Function_List");
        }

       
        public IEnumerable<clsFunction> BindFunctionSwap(int? c_id, string conn)
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

                    Function.Till_name = rdr["Till_name"].ToString();
                   // Function.DeptId = rdr["department_id"].ToString();
                    Function.TMachineId = rdr["machine_id"] != DBNull.Value ? Convert.ToInt32(rdr["machine_id"].ToString()) : 0;
                    ViewData["MachineId"] = Function.TMachineId;

                    list.Add(Function);
                }
                con.Close();
            }
            return list;
        } 
        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("mainfunction_id");
            return RedirectToPage("/Configuration/Function_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("mainfunction_id");
            return RedirectToPage("/Configuration/Function_List");
        }
    }
}
