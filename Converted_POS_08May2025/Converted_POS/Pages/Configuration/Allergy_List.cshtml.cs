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
    public class Allergy_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        AllergyController obj = new AllergyController();

        [BindProperty]
        public clsAllergy al { get; set; }

        [BindProperty]
        public List<clsAllergy> allergy { get; set; }

        public string cmpID { get; set; }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            allergy = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }

       
        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            al = obj.Select(Convert.ToInt32(cmpID),id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                al.allergy_id = id;
                al.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(al, ConnStr);
            }

            return RedirectToPage("/Configuration/Allergy_List");
        }
    }
}