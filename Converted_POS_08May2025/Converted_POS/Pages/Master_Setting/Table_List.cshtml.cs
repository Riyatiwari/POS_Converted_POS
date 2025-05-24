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
    public class Table_ListModel : PageModel
    {
        public string ConnStr { get; set; }

        TableController obj = new TableController();

        [BindProperty]
        public clsTable Table { get; set; }

        [BindProperty]
        public List<clsTable> TableList { get; set; }

        public string cmpID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            TableList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id, string ipAddress)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Table = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                Table.table_id = id;
                Table.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(Table, ConnStr, ip_address);
            }

            return RedirectToPage("/Master_Setting/Table_List");
        }
    }
}
