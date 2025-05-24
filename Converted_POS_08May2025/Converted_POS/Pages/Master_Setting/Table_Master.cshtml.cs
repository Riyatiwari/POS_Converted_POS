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
    public class Table_MasterModel : PageModel
    {
        TableController obj = new TableController();

        [BindProperty]
        public clsTable table { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> DTTable { get; set; }

        //public SelectList Dept_cate { get; set; }
        public ActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            if (id.HasValue)
            {
                HttpContext.Session.SetString("table_id", id.Value.ToString());
                table = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr);


                if (table == null)
                {
                    return NotFound();
                }
            }
                
            DTTable = obj.GetLocation(Convert.ToInt32(cmp_id), ConnStr);
            return Page();
        }
        [HttpPost("Save")]
        public ActionResult OnPost(string ipAddress)
        {
            string id = HttpContext.Request.Query["id"].ToString();

            table.table_id = Convert.ToInt32(HttpContext.Session.GetString("table_id"));
            table.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            
            ConnStr = HttpContext.Session.GetString("conString");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (table.table_id == null || table.table_id == 0)
            {
                table.table_id = 0;
                obj.Insert(table, ConnStr, ip_address);
            }
            else
            {
                obj.Update(table, ConnStr, ip_address);
            }
            HttpContext.Session.Remove("table_id");
            //obj.Insert(DType, ConnStr);
            return RedirectToPage("/Master_Setting/Table_list");
        }


        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            return RedirectToPage("/Master_Setting/Table_master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            return RedirectToPage("/Master_Setting/Table_list");
        }
    }
}
