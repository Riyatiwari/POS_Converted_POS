using Converted_POS;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Pages.forms
{
    public class Product_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        ProductController obj = new ProductController();
        [BindProperty]
        public clsProduct al { get; set; }
        [BindProperty]
        public List<clsProduct> product { get; set; }
        public List<clsProduct> Products { get; set; }
        [BindProperty]
        public bool isActive { get; set; }
        [BindProperty]
        public string hiddenIsActive { get; set; }
        public string cmpID { get; set; }
        public string TranType { get; set; }
        public List<clsProduct> Ingredients { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null || HttpContext.Session.GetString("conString") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");
            TranType = HttpContext.Session.GetString("Tran_Type");
            isActive = true;
            product = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, isActive).ToList();
            Products = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, isActive).ToList();
            isActive = true;
            Initialize(isActive);
            return Page();
        }

        public IActionResult OnPost()
        {
           
            ConnStr = HttpContext.Session.GetString("conString");

            if (string.IsNullOrEmpty(ConnStr) || HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            //Console.WriteLine($"Name: {product.Name}");
           // Console.WriteLine($"CategoryId: {product.CategoryId}");
           // Console.WriteLine($"DepartmentId: {product.DepartmentId}");
            cmpID = HttpContext.Session.GetString("cmp_id");
            TranType = HttpContext.Session.GetString("Tran_Type");
            isActive = hiddenIsActive == "true";
            // Fetch records based on the checkbox state
            product = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, isActive).ToList();
            Products = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, isActive).ToList();

            return Page();  
        }
        private IActionResult Initialize(bool defaultIsActive)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");
            TranType = HttpContext.Session.GetString("Tran_Type");
            // Fetch products based on the checkbox state
            product = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, defaultIsActive).ToList();
            return Page();
        }
        public ActionResult OnGetDelete(int? id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            al = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                al.ProductId = id;
                al.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(al, ConnStr);
            }

            return RedirectToPage("/Configuration/Product_List");
        }
        public ActionResult OnGetActAsync(int? id, string tranType)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            if (cmpID == null || ConnStr == null)
            {
                return RedirectToPage("/SignIn");
            }

            var product = new clsProduct
            {
                cmp_id = Convert.ToInt32(cmpID),
                ProductId = id
            };

            //tranType = tranType ?? "A";
            // Determine the current 'is_active' value
            // You might need to fetch the current status before calling Act if it is not passed
            byte isActive = (tranType == "A") ? (byte)1 : (byte)0;
            List<clsProduct> productList = new List<clsProduct> { product };
            obj.Act(productList, ConnStr, tranType);

            return RedirectToPage("/Configuration/Product_List");
        }

    }
}
