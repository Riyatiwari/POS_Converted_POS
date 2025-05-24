using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Master_Setting
{
    public class Machine_ListModel : PageModel
    {
        public string ConnStr { get; set; }
        MachineController obj = new MachineController();
        [BindProperty]
        public clsMachine machine { get; set; }
        [BindProperty]
        public List<clsMachine> machineList { get; set; }
        public string cmpID { get; set; }
        public int storeUUID { get; set; }
        public string modelName { get; set; }
        public ActionResult OnGet()
        {
            if (machine == null)
            {
                machine = new clsMachine();
            }
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }


            machine.Mainip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";


            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");
            storeUUID = Convert.ToInt32(HttpContext.Session.GetString("storeUUID"));
            modelName = HttpContext.Session.GetString("model");
            machineList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
            //obj.SyncModelToController(ConnStr, Convert.ToInt32(cmpID), storeUUID, modelName);
            return Page();
        }
        public ActionResult OnGetDelete(int id, int function_id, int machine_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");

            machine = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, function_id, Convert.ToInt32(machine_id));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                machine.machine_id = id;
                machine.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));

                machine.Mainip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                obj.Delete(machine, ConnStr, machine.Mainip_address);
            }

            return RedirectToPage("/Master_Setting/Machine_List");
        }
    }
}
