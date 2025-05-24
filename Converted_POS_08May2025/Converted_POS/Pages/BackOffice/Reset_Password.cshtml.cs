using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Converted_POS.Pages.BackOffice
{
    public class Reset_PasswordModel : PageModel
    {
        [BindProperty]
        public clsSignIn signIn { get; set; }

        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            return Page();
        }
    }
}