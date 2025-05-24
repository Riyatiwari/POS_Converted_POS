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
    public class Product_Price_Master_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        ProductPriceController obj = new ProductPriceController();

        [BindProperty]
        public clsProductPrice price { get; set; }

        [BindProperty]
        public List<clsProductPrice> priceList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            priceList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id, string ipAddress, int? country_id, int? state_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            price = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, country_id, state_id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                price.Product_Price_Id = id;
                price.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(price, ConnStr, ip_address);
            }
            return RedirectToPage("/Master_Setting/Product_Price_Master_List");
        }
    }
}
