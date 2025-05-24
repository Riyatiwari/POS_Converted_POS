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
    public class Device_ListModel : PageModel
    {
        DeviceController obj = new DeviceController();
        public string ConnStr { get; set; }
        public string cmpID { get; set; }
        [BindProperty]
        public clsDevice DT { get; set; }
        [BindProperty]
        public List<clsDevice> DeviceList { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            DeviceList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();

            return Page();
        }
        public ActionResult OnGetDelete(int id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            DT = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                DT.device_id = id;
                DT.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(DT, ConnStr);
            }

            return RedirectToPage("/Master_Setting/Device_List");
        }
    }
}
