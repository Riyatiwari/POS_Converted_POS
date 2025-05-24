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
    public class DepartmentCategory_listModel : PageModel
    {

        DepartmentCategoryController obj = new DepartmentCategoryController();

        [BindProperty]
        public clsDepartmentCategory DT { get; set; }

        [BindProperty]
        public List<clsDepartmentCategory> DType { get; set; }

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

            DType = obj.SelectAll(Convert.ToInt32(cmpID) , ConnStr).ToList();

            return Page();
        }

        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            DT = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                DT.department_category_id = id;
                DT.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(DT, ConnStr);
            }

            return RedirectToPage("/Master_Setting/DepartmentCategory_list");
        }
    }
}