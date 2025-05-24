using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Customers_and_Promos
{
    public class Profile_MasterModel : PageModel
    {
        ProfileMasterController obj = new ProfileMasterController();

        [BindProperty]
        public clsProfileMaster DType { get; set; }

        public string ConnStr { get; set; }
        public string cmp_id { get; set; }

        public ActionResult OnGet(int? id)
        {

            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }


            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            if (id == null)
            {
                return Page();
            }
            else
            {

                HttpContext.Session.SetString("profile_id", HttpContext.Request.Query["ID"].ToString());
                DType = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);

                if (DType == null)
                {
                    return NotFound();
                }

                return Page();
            }
        }


        [HttpPost("Save")]
        public ActionResult OnPost()
        {
            DType.profile_id = Convert.ToInt32(HttpContext.Session.GetString("profile_id"));
            DType.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (DType.profile_id == null || DType.profile_id == 0)
            {
                DType.profile_id = 0;
                obj.Insert(DType, ConnStr);
            }
            else
            {
                obj.Update(DType, ConnStr);
            }

            HttpContext.Session.Remove("profile_id");
            return RedirectToPage("/Customers_and_Promos/Profile_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("profile_id");
            return RedirectToPage("/Customers_and_Promos/Profile_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("profile_id");
            return RedirectToPage("/Customers_and_Promos/Profile_List");
        }
    }
}