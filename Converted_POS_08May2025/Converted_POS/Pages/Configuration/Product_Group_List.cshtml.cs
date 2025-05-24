using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
 

namespace Converted_POS.Pages.Configuration
{
    public class Product_Group_ListModel : PageModel
    {
        ProductGroupController obj = new ProductGroupController();

        [BindProperty]
        public clsProductGroup DT { get; set; }

        [BindProperty]
        public List<clsProductGroup> DType { get; set; }
        [BindProperty]
        public List<clsProductGroup> DType_PGL { get; set; }

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

            DType = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
            DType_PGL = obj.SelectM(Convert.ToInt32(cmpID), ConnStr).ToList();
            obj.AssignTill(ConnStr);
            //ViewBag.GrpCatList = DTy;
           ViewData["DTy"] = DType_PGL;
            //TempData["List"] = DTy;
            return Page();

            
            //return RedirectToPage("/BackOffice/Product_Group_Master");
        }
        public ActionResult OnGetDelete(int? id, string ipAddress)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            DT = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                DT.category_id = (int)id;
                DT.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(DT, ConnStr, ip_address);
            }

            return RedirectToPage("/Configuration/Product_Group_List");
        }
    }
}