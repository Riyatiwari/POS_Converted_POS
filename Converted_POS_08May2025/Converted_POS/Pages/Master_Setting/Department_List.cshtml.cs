using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Master_Setting
{
    public class Department_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        DepartmentController obj = new DepartmentController();

        [BindProperty]
        public clsDepartment dept { get; set; }

        [BindProperty]
        public List<clsDepartment> dept_list { get; set; }

        public string cmpID { get; set; }

        public ActionResult OnGet()
        {

            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");

            }
            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            dept_list = obj.SelectAll(Convert.ToInt32(cmpID), 0, ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            dept = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                dept.Department_id = id;
                dept.Cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(dept, ConnStr);
            }

            return RedirectToPage("/Master_Setting/Department_List");
        }
    }
}