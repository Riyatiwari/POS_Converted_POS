using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.BackOffice
{
    public class Profile_ListModel : PageModel
    {
        ProfileMasterController obj = new ProfileMasterController();

        [BindProperty]
        public clsProfileMaster pm { get; set; }

        [BindProperty]
        public List<clsProfileMaster> Profile { get; set; }

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

            Profile = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();

        }

        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            pm = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                pm.profile_id = id;
                pm.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(pm, ConnStr);
            }

            return RedirectToPage("/Customers_and_Promos/Profile_List");
        }
    }
}