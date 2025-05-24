using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages
{
    public class IndexModel : PageModel
    {
        public string cmpID { get; set; }

        public IActionResult OnGet()
        {
            cmpID = HttpContext.Session.GetString("cmp_id");

            if (cmpID is null)
            {
                return RedirectToPage("/SignIn");
            }
            else
            {
                return Page();
            }

        }
    }
}
