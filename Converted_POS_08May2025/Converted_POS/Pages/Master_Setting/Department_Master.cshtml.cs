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
    public class Department_MasterModel : PageModel
    {
        DepartmentController obj = new DepartmentController();

        [BindProperty]
        public clsDepartment DType { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public bool IsDepartmentInserted { get; set; } = true;
       
        public SelectList Dept_cate { get; set; }

        public ActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            Dept_cate = new SelectList(obj.bindDptCat(Convert.ToInt32(cmp_id), ConnStr) , "Department_category_id", "department_category_name");

            if (id == null)
            {
                return Page();
            }
            else
            {
                DType = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);


                if (DType == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }

        [HttpPost("Save")]
        public ActionResult OnPost(int id)
        {
           

            DType.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            DType.Department_category_id = Convert.ToInt32(Request.Form["Department_category_id"]);
            ConnStr = HttpContext.Session.GetString("conString");

             if (!ModelState.IsValid)
            {
                return Page();
            }


            if (DType.Department_id == null || DType.Department_id == 0)
            {
               
                bool isInserted = obj.Insert(DType, ConnStr);
                if (!isInserted)
                {
                    IsDepartmentInserted = false;  // Show alert: already exists
                    Dept_cate = new SelectList(obj.bindDptCat(Convert.ToInt32(cmp_id), ConnStr), "Department_category_id", "department_category_name");

                    return Page();
                }
            }
            else
            {
                 
                obj.Update(DType, ConnStr);
            }
            //if (DType.Department_id == null || DType.Department_id == 0)
            //{
            //    bool isInserted = obj.Insert(DType, ConnStr);
            //    if (!isInserted)
            //    {

            //        IsDepartmentInserted = false;
            //        return Page();
            //    }
            //}
           
            //if (id == null || id == "")
            //{
            //    DType.Department_id = 0;
            //    obj.Insert(DType, ConnStr);
            //}
            //else
            //{
            //    obj.Update(DType, ConnStr);
            //}

            //obj.Insert(DType, ConnStr);
            return RedirectToPage("/Master_Setting/Department_list");
        }


        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            return RedirectToPage("/Master_Setting/Department_master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            return RedirectToPage("/Master_Setting/Department_list");
        }
    }
}