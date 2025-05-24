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
    public class Printer_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        PrinterController obj = new PrinterController();

        [BindProperty]
        public clsPrinter Printer { get; set; }

        [BindProperty]
        public List<clsPrinter> PrinterList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            PrinterList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id, string ipAddress)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Printer = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                Printer.printer_id = id;
                Printer.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(Printer, ConnStr, ip_address);
            }

            return RedirectToPage("/Master_Setting/Printer_List");
        }
    }
}
