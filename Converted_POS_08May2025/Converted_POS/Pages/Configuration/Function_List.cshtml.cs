using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Converted_POS.Pages.forms
{
    public class Function_ListModel : PageModel
    {
        public string ConnStr { get; set; }

       
        private readonly IConfiguration _configuration;
        private readonly FunctionController obj;
        public Function_ListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            obj = new FunctionController(_configuration);
        }

        [BindProperty]
        public clsFunction Fn { get; set; }
        [BindProperty]
        public List<clsFunction> Function { get; set; }
        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            Function = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
         
            return Page();
        }

        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            int  cmpID = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));

            Fn = obj.Select(Convert.ToInt32(id), Convert.ToInt32(cmpID), ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
               // Fn.Function_id = id;
                Fn.CmpId = cmpID;
                obj.Delete(Fn, ConnStr);
            }

            return RedirectToPage("/Configuration/Function_List");
        }
    }
}
