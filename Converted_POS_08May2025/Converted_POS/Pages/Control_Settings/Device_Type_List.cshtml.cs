using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Control_Settings
{
    public class Device_Type_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        Device_TypeController obj = new Device_TypeController();

        [BindProperty]
        public clsDevice_Type DT { get; set; }

        [BindProperty]
        public List<clsDevice_Type> DType { get; set; }

        public string cmpID { get; set; }

        public ActionResult OnGet()
        {

            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            DType = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }

        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            DT = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                DT.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));

                DT.Device_Type_id = id;
                obj.Delete(DT, ConnStr);
            }

            return RedirectToPage("/Control_Settings/Device_Type_List");
        }
    }
}