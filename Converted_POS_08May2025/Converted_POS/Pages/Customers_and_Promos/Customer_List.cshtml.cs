using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.forms
{
    public class Customer_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        CustomerController obj = new CustomerController();

        [BindProperty]
        public clsCustomer Customer { get; set; }

        [BindProperty]
        public List<clsCustomer> CustomerList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            CustomerList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id, string ipAddress, int? country_id, int? state_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Customer = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, country_id, state_id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                Customer.customer_id = id;
                Customer.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(Customer, ConnStr, ip_address);
            }

            return RedirectToPage("/Customers_and_Promos/Customer_List");
        }
    }
}
