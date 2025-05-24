using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Pages.Configuration
{
    public class Product_Group_Main_MasterModel : PageModel
    {
        ProductGroupMainController obj = new ProductGroupMainController();

        [BindProperty]
        public clsProductGroupMain DType { get; set; }

        [BindProperty]
        public List<clsProductGroupMain> DType_LCPGM { get; set; }
        public List<SelectListItem> DTdepartment { get; set; }
        public string ConnStr { get; set; }
        public string cmpID { get; set; }

        public ActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            DType = DType ?? new clsProductGroupMain();
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            var forwardedIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            DType.ip_address = !string.IsNullOrEmpty(forwardedIp) ? forwardedIp : HttpContext.Connection.RemoteIpAddress?.ToString();
            DTdepartment = obj.GetDepartment(Convert.ToInt32(cmpID), ConnStr);
            DType_LCPGM = obj.Index(Convert.ToInt32(cmpID), ConnStr).ToList();
            int sortingNumber = obj.GenerateSortingNumber(Convert.ToInt32(cmpID), "Group Category", ConnStr);
            DType.sorting_no = sortingNumber;

            if (id == null)
            {
                return Page();
            }
            else
            {

                HttpContext.Session.SetString("category_id", HttpContext.Request.Query["ID"].ToString());
                DType = obj.SelectS(Convert.ToInt32(cmpID), id, ConnStr);

                if (DType == null)
                {
                    return NotFound();
                }
                if (DType.Aimage == null || DType.Aimage.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64Image = ConvertImageToBase64String(DType.Aimage);
                    ViewData["Base64Image"] = $"data:image/png;base64,{base64Image}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = DType.Aimage;
                return Page();
            }
        }

        private string ConvertImageToBase64String(byte[] aimage)
        {
            if (aimage == null || aimage.Length == 0)
            {
                return null; // Handle case where no image is present
            }
            return Convert.ToBase64String(aimage);
        }

        [HttpPost("Save")]
        public ActionResult OnPost(IFormFile AimageFile, string ipAddress, string ImageFileName)
        {
            DType.category_id = Convert.ToInt32(HttpContext.Session.GetString("category_id"));
            DType.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (AimageFile != null && AimageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    AimageFile.CopyToAsync(memoryStream); // Asynchronously copy the file
                    DType.Aimage = memoryStream.ToArray(); // Convert to byte array
                    DType.ImageFileName = AimageFile.FileName; // Store the file name
                }
            }
            else
            {
                //DType.Aimage = null; // Handle no image uploaded
                //DType.ImageFileName = null; // Handle no file name

                if (!string.IsNullOrEmpty(ImageFileName))
                {
                    // Decode the base64 image data and save it
                    byte[] existingImage = Convert.FromBase64String(ImageFileName.Replace("data:image/png;base64,", ""));
                    DType.Aimage = existingImage;
                }
                else
                {
                    DType.Aimage = null; // If no image exists (i.e., no base64 string), set it to null
                }
            }

            if (DType.category_id == null || DType.category_id == 0)
            {
                DType.category_id = 0;
                obj.Insert(DType, ConnStr, ip_address);
                TempData["SuccessMessage"] = "Record inserted successfully!";
            }
            else
            {
                obj.Update(DType, ConnStr, ip_address);
                TempData["SuccessMessage"] = "Record updated successfully!";
            }

            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_Main_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_Main_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_Main_List");
        }

    }
}