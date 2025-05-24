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
    public class Course_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        CourseController obj = new CourseController();

        [BindProperty]
        public clsCourse course { get; set; }

        [BindProperty]
        public List<clsCourse> courseList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            courseList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id, int course_id, string ipAddress, int courses_category_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            course = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, courses_category_id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                course.Course_id = id;
                course.Cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(course, ConnStr, ip_address);
            }

            return RedirectToPage("/Master_Setting/Course_List");
        }
    }
}
