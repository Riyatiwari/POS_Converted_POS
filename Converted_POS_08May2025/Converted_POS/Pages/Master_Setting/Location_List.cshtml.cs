using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Converted_POS.Pages.Master_Setting
{
    public class Location_ListModel : PageModel
    {
        

        private readonly IConfiguration _configuration;
        private readonly LocationController obj;


        [BindProperty]
        public clsLocation Location { get; set; }
        public Location_ListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            obj = new LocationController(_configuration);
        }

        [BindProperty]
        public List<clsLocation> LocationList { get; set; }
        public string cmpID { get; set; }
        public string ConnStr { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            //var storeName = HttpContext.Session.GetString("Lstore");
            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            LocationList = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr).ToList();
            foreach (var location in LocationList)
            {
                // Fetching storeName from hidden field (if required)
                
                var storeName = HttpContext.Session.GetString("Lstore");
                // Use gtway_StoreName if available
                var wposUrl = _configuration["AppSettings:wpos_URL"];
                if (!string.IsNullOrEmpty(location.gtway_StoreName))
                {
                    location.WebSalesLink = $"{wposUrl}?SName={location.gtway_StoreName}&sv={storeName}&cv={cmpID}&lv={location.location_id}";
                }
                else
                {
                    location.WebSalesLink = $"{wposUrl}?sv={storeName}&cv={cmpID}&lv={location.location_id}";
                }
            }

            return Page();
        }
        public ActionResult OnGetDelete(int id, string ipAddress)
        {
            
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Location = obj.Select(Convert.ToInt32(cmpID), id, ConnStr, HttpContext);
            
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != null)
            {
                Location.location_id = id;
                Location.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                obj.Delete(Location, ConnStr, ip_address);
            }

            return RedirectToPage("/Master_Setting/Location_List");
        }
    }
}
