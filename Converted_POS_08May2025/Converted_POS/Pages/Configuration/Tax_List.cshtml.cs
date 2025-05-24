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
    public class Tax_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        TaxController obj = new TaxController();

        [BindProperty]
        public clsTax tax { get; set; }

        [BindProperty]
        public List<clsTax> taxList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            taxList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id , string ipAddress)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            tax = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                tax.Tax_id = id;
                tax.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(tax, ConnStr,ip_address);
            }

            return RedirectToPage("/Configuration/Tax_List");
        }
    }
}
