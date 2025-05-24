using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.forms
{
    public class Staff_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        StaffController obj = new StaffController();

        [BindProperty]
        public clsStaff al { get; set; }
        [BindProperty]
        public List<clsStaff> staff { get; set; }
        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            staff = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
            obj.SyncTill(ConnStr, Convert.ToInt32(cmpID));
            return Page();
        }

        public ActionResult OnGetDelete(int? id, int? country_id, int? state_id, int? city_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            al = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, country_id, state_id, city_id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                al.staff_id = id;
                al.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(al, ConnStr);
            }

            return RedirectToPage("/Configuration/Staff_List");
        }
    }
}
