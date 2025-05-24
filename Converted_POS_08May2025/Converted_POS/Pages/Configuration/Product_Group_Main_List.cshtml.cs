using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Pages.Configuration
{
    public class Product_Group_Main_ListModel : PageModel
    {
        ProductGroupMainController obj = new ProductGroupMainController();

        [BindProperty]
        public clsProductGroupMain DT { get; set; }

        [BindProperty]
        public List<clsProductGroupMain> DType_PGML { get; set; }

        public string ConnStr { get; set; }

        public string cmpID { get; set; }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            DType_PGML = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
            
            return Page();
        }

        public IActionResult OnGetDelete(int? id, string ipAddress)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            DT = obj.SelectS(Convert.ToInt32(cmpID), id , ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                DT.maincategory_id = id;
                DT.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(DT, ConnStr, ip_address);
            }

            return RedirectToPage("/Configuration/Product_Group_Main_List");
        }
    }
}