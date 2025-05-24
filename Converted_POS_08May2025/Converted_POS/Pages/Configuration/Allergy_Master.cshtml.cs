using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.forms
{
    public class Allergy_MasterModel : PageModel
    {
        AllergyController obj = new AllergyController();

        [BindProperty]
        public clsAllergy allergy { get; set; }

        public string ConnStr { get; set; }
        public string cmp_id { get; set; }

        public ActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            if (id == null)
            {
                return Page();
            }
            else
            {

                HttpContext.Session.SetString("allergy_id", HttpContext.Request.Query["ID"].ToString());
                allergy = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);
                
                if (allergy == null)
                {
                    return NotFound();
                }
                if (allergy.Aimage == null || allergy.Aimage.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64Image = ConvertImageToBase64String(allergy.Aimage);
                    ViewData["Base64Image"] = $"data:image/png;base64,{base64Image}";  // Store it in ViewData to use in the view
                }
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
        public ActionResult OnPost(IFormFile AimageFile)
        {
            allergy.allergy_id = Convert.ToInt32(HttpContext.Session.GetString("allergy_id"));
            allergy.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (AimageFile != null && AimageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    AimageFile.CopyToAsync(memoryStream); // Asynchronously copy the file
                    allergy.Aimage = memoryStream.ToArray(); // Convert to byte array
                    allergy.ImageFileName = AimageFile.FileName; // Store the file name
                }
            }
            else
            {
                allergy.Aimage = null; // Handle no image uploaded
                allergy.ImageFileName = null; // Handle no file name
            }
            if (allergy.allergy_id == null || allergy.allergy_id == 0)
            {
                allergy.allergy_id = 0;
                obj.Insert(allergy, ConnStr);
                TempData["SuccessMessage"] = "Record inserted successfully!";
            }
            else
            {
                obj.Update(allergy, ConnStr);
                TempData["SuccessMessage"] = "Record updated successfully!";
            }

            HttpContext.Session.Remove("allergy_id");
            return RedirectToPage("/Configuration/Allergy_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("allergy_id");
            return RedirectToPage("/Configuration/Allergy_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("allergy_id");
            return RedirectToPage("/Configuration/Allergy_List");
        }
    }
}