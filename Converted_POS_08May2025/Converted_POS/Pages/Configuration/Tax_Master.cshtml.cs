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
    public class Tax_MasterModel : PageModel
    {
        TaxController obj = new TaxController();

        [BindProperty]
        public clsTax tax { get; set; }

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
                HttpContext.Session.SetString("tax_id", HttpContext.Request.Query["ID"].ToString());
                tax = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);

                if (tax == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }
        [HttpPost("Save")]
        public ActionResult OnPost(string ipAddress)
        {
            tax.Tax_id = Convert.ToInt32(HttpContext.Session.GetString("tax_id"));
            tax.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string selectedMode = tax.Mode; // This will contain the selected value from the dropdown

            // Perform your logic with selectedMode here
            if (selectedMode == "SELECT")
            {
                // Your logic for SELECT
            }
            else if (selectedMode == "%")
            {
                // Your logic for "%"
            }
            else if (selectedMode == "Amt")
            {
                // Your logic for Amt
            }

            if (tax.Tax_id == null || tax.Tax_id == 0)
            {
                tax.Tax_id = 0;
                obj.Insert(tax, ConnStr, ip_address);
            }
            else
            {
                obj.Update(tax, ConnStr, ip_address);
            }

            HttpContext.Session.Remove("Tax_id");
            return RedirectToPage("/Configuration/Tax_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("Tax_id");
            return RedirectToPage("/Configuration/Tax_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("Tax_id");
            return RedirectToPage("/Configuration/Tax_List");
        }
    }
}

