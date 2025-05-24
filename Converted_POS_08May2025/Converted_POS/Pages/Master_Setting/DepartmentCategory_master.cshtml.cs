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
    public class DepartmentCategory_masterModel : PageModel
    {
        DepartmentCategoryController obj = new DepartmentCategoryController();

        [BindProperty]
        public clsDepartmentCategory DType { get; set; }

        public string ConnStr { get; set; }
        public string cmpID { get; set; }

        public ActionResult OnGet(int? id)
        {

            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            if (id == null)
            {
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("department_category_id", HttpContext.Request.Query["ID"].ToString());
                DType = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

                if (DType == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }

        [HttpPost("Save")]
        public ActionResult OnPost()
        {
            DType.department_category_id = Convert.ToInt32(HttpContext.Session.GetString("department_category_id"));
            DType.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (DType.department_category_id == null || DType.department_category_id == 0)
            {
                DType.department_category_id = 0;
                obj.Insert(DType, ConnStr);
            }
            else
            {
                obj.Update(DType, ConnStr);
            }

            HttpContext.Session.Remove("department_category_id");
            return RedirectToPage("/Master_Setting/DepartmentCategory_list");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("department_category_id");
            return RedirectToPage("/Master_Setting/DepartmentCategory_master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("department_category_id");
            return RedirectToPage("/Master_Setting/DepartmentCategory_list");
        }

    }
}