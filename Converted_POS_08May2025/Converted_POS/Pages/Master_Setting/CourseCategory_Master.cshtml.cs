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
    public class CourseCategory_MasterModel : PageModel
    {
        CourseCategoryController obj = new CourseCategoryController();
        public bool IsCategoryInserted { get; set; } = true;
        [BindProperty]
        public clsCourseCategory DType { get; set; }

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
                HttpContext.Session.SetString("course_category_id", HttpContext.Request.Query["ID"].ToString());
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
            DType.course_category_id = Convert.ToInt32(HttpContext.Session.GetString("course_category_id"));
            DType.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool isInserted = obj.Insert(DType, ConnStr);

            if (!isInserted)
            {
                // If the category was not inserted (i.e., it already exists), set the flag
                IsCategoryInserted = false;  // Category name already exists
                return Page();  // Return to the same page so the alert can be displayed
            }
            if (DType.course_category_id == null || DType.course_category_id == 0)
            {
                DType.course_category_id = 0;
                obj.Insert(DType, ConnStr);
            }
            else
            {
                obj.Update(DType, ConnStr);
            }

            HttpContext.Session.Remove("course_category_id");
            return RedirectToPage("/Master_Setting/CourseCategory_list");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("course_category_id");
            return RedirectToPage("/Master_Setting/CourseCategory_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("course_category_id");
            return RedirectToPage("/Master_Setting/CourseCategory_list");
        }
    }
}