using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages.Master_Setting
{
    public class Course_MasterModel : PageModel
    {

        CourseController obj = new CourseController();

        [BindProperty]
        public clsCourse course { get; set; }

        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> DTCourseCategory { get; set; }
        public int? Selectedcoursescategoryid { get; set; }
        public ActionResult OnGet(int? id, int course_id, int? courses_category_id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");
            DTCourseCategory = obj.GetCourseCategory(Convert.ToInt32(cmp_id), ConnStr, course_id);
            Selectedcoursescategoryid = courses_category_id ?? HttpContext.Session.GetInt32("selectedcoursescategoryid");
            int? selectedcoursescategoryid = courses_category_id ?? HttpContext.Session.GetInt32("courses_category_id");
            courses_category_id = Selectedcoursescategoryid;
            if (id == null)
            {
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("course_id", HttpContext.Request.Query["ID"].ToString());
                course = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr, courses_category_id);

                if (course == null)
                {
                    return NotFound();
                }
                
                return Page();
            }
        }
        [HttpPost("Save")]
        public ActionResult OnPost(string ipAddress, int? courses_category_id)
        {
            course.Course_id = Convert.ToInt32(HttpContext.Session.GetString("course_id"));
            course.Cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Selectedcoursescategoryid = courses_category_id ?? HttpContext.Session.GetInt32("selectedcoursescategoryid");
            int? selectedcoursescategoryid = courses_category_id ?? HttpContext.Session.GetInt32("courses_category_id");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            courses_category_id = Selectedcoursescategoryid;
            if (course.Course_id == null || course.Course_id == 0)
            {
                course.Course_id = 0;
                obj.Insert(course, ConnStr, ip_address, courses_category_id);
            }
            else
            {
                obj.Update(course, ConnStr, ip_address, courses_category_id);
            }

            HttpContext.Session.Remove("course_id");
            return RedirectToPage("/Master_Setting/Course_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("course_id");
            return RedirectToPage("/Master_Setting/Course_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("course_id");
            return RedirectToPage("/Master_Setting/Course_List");
        }
    }
}
