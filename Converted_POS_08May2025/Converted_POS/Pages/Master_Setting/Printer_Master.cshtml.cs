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
    public class Printer_MasterModel : PageModel
    {
        PrinterController obj = new PrinterController();

        [BindProperty]
        public clsPrinter printer { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> DTTable { get; set; }
        public byte? SelectedValue { get; set; }

        //public SelectList Dept_cate { get; set; }
        public ActionResult OnGet(int? id, byte? Product)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            if (id.HasValue)
            {
                HttpContext.Session.SetString("printer_id", id.Value.ToString());
                printer = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr);


                if (printer == null)
                {
                    return NotFound();
                }
            }

            //DTTable = obj.GetLocation(Convert.ToInt32(cmp_id), ConnStr);
            return Page();
        }
        [HttpPost("Save")]
        public ActionResult OnPost(string ipAddress, byte? Product, clsPrinter printer)
        {
            printer.printer_id = Convert.ToInt32(HttpContext.Session.GetString("printer_id"));
            printer.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            byte? selectedValue = printer.is_product_small_large;

            if (selectedValue.HasValue)
            {
                printer.is_product_small_large = selectedValue.Value;
            }
            else
            {
                ModelState.AddModelError("Product", "Product size is required.");
                return Page(); // Or return an appropriate view
            }

            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (printer.printer_id == null || printer.printer_id == 0)
            {
                printer.printer_id = 0;
                obj.Insert(printer, ConnStr, ip_address);
            }
            else
            {
                obj.Update(printer, ConnStr, ip_address);
            }

            HttpContext.Session.Remove("printer_id");
            return RedirectToPage("/Master_Setting/Printer_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("printer_id");
            return RedirectToPage("/Master_Setting/Printer_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("printer_id");
            return RedirectToPage("/Master_Setting/Printer_List");
        }
    }
}
