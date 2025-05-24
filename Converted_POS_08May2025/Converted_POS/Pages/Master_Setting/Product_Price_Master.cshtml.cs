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
    public class Product_Price_MasterModel : PageModel
    {
        ProductPriceController obj = new ProductPriceController();

        [BindProperty]
        public clsProductPrice price { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public int? SelectedProductPriceId { get; set; }

        public ActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            if (id == null)
            {
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("Product_Price_Id", HttpContext.Request.Query["ID"].ToString());
                price = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);

                if (price == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }
        [HttpPost("Save")]
        public ActionResult OnPost(string ipAddress)
        {
            price.Product_Price_Id = Convert.ToInt32(HttpContext.Session.GetString("Product_Price_Id"));
            price.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            ConnStr = HttpContext.Session.GetString("conString");
            //SelectedProductPriceId = Product_Price_Id ?? HttpContext.Session.GetInt32("selectedProductPriceId");
            //int? selectedProductPriceId = Product_Price_Id ?? HttpContext.Session.GetInt32("Product_Price_Id");


            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Product_Price_Id = SelectedProductPriceId;
            if (price.Product_Price_Id == null || price.Product_Price_Id == 0)
            {
                price.Product_Price_Id = 0;
                obj.Insert(price, ConnStr, ip_address);
            }
            else
            {
                obj.Update(price, ConnStr, ip_address);
            }

            HttpContext.Session.Remove("Product_Price_Id");
            return RedirectToPage("/Master_Setting/Product_Price_Master_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("Product_Price_Id");
            return RedirectToPage("/Master_Setting/Product_Price_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("Product_Price_Id");
            return RedirectToPage("/Master_Setting/Product_Price_Master_List");
        }
    }
}
