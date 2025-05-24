using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using QRCoder;


namespace Converted_POS.Pages.Utility
{
    public class QR_ListModel : PageModel
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration to access appsettings.json
        public QR_ListModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [BindProperty]
        public string machine_id { get; set; }

        [BindProperty]
        public string venue_id { get; set; }

        [BindProperty]
        public string location_id { get; set; }

        [BindProperty]
        public string machineName { get; set; }
        public string ConnStr { get; set; }

        QRController obj = new QRController();

        [BindProperty]
        public clsQR qr { get; set; }

        [BindProperty]
        public List<clsQR> QR { get; set; }

        public string cmpID { get; set; }
        public string GeneratedCode { get; set; }
        public DateTime? ValidTime { get; set; }
        public IActionResult OnGet(string tillUUID)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");

            if (TempData["TillUUID"] != null)
            {
                tillUUID = TempData["TillUUID"].ToString();
            }

            QR = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, tillUUID);

            var qrForThisTill = QR.FirstOrDefault(q => q.TillUUID == tillUUID);

            foreach (var qr in QR)
            {
                if (!string.IsNullOrEmpty(qr.TillUUID))
                {
                    // Call the GenerateCode method for each TillUUID (similar to SelectAll call)
                    // Here, you simulate a request to the GenerateCode action
                    //var generatedCode = GenerateCode(qr.TillUUID).GeneratedCode;

                    // Optionally store or use the generated code, for example, saving to a property
                    //qr.GeneratedCode = generatedCode.ToString(); // Assuming you add a GeneratedCode property in clsQR
                    var qrText = $"{qr.TillUUID}";  //$"{qr.TillUUID}-{qr.machine_name}";
                                                    // Pass TillUUID (or any other relevant text you want) as qrText
                    var qrCodeBytes = GenerateQRCodeFromText(qrText); // Use TillUUID or any other relevant text
                    var qrCodeLink = GenerateQRCodeLinkFromText(qrText);
                    qr.QRCodeLink = qrCodeLink;
                    // Assuming you store qrCodeBytes in your object (e.g., qr.QRCodeBytes)
                    qr.QRCodeBytes = qrCodeBytes; // Store the generated QR code bytes
                }
                else
                {
                    // Optionally set a default or handle the case where TillUUID is null
                    qr.QRCodeBytes = null; // No QR code generated
                }
                //if (string.IsNullOrEmpty(qrText))
                //{
                //    ViewBag.ErrorMessage = "Please enter text to generate QR Code.";
                //    return View();
                //}

                //return View("QR_List", qrCodeBytes);
            }

            return Page();
        }

        public IActionResult OnPost(string generateCodeButton, string machineName, string machineId, string locationName, string locationId, string venueName, string venueId)
        {
            //if (string.IsNullOrEmpty(generateCodeButton))
            //{
            //    return Page(); // or return an error if the TillUUID is missing
            //}
            //string tillUUID = TempData["TillUUID"] as string;
            Console.WriteLine($"LocationId: {locationId}, VenueId: {venueId}");

            // Check if any of the values are null and handle accordingly
            if (string.IsNullOrEmpty(locationId) || string.IsNullOrEmpty(venueId))
            {
                // Handle the case where the values are missing
                Console.WriteLine("LocationId or VenueId is null");
            }

            TempData["MachineName"] = machineName;
            TempData["MachineId"] = machineId;
            TempData["LocationName"] = locationName;
            TempData["LocationId"] = locationId;
            TempData["VenueName"] = venueName;
            TempData["VenueId"] = venueId;
            int machineIdInt = Convert.ToInt32(machineId);

            string tillUUID = generateCodeButton;
            // Call the GenerateCode method to generate the new code
            var qrr = new clsQR { TillUUID = tillUUID, machine_id = machineIdInt, location_id = int.Parse(locationId), venue_id = int.Parse(venueId) }; // Create an instance of clsQR with TillUUID
            OnPostGenerateQR(qrr, tillUUID, machineName, machineId, locationId, venueId);


            //var QRGenerate = GenerateQR(qr);
            string till_UUIDD = tillUUID;
            TempData["TillUUID"] = qrr.TillUUID;
            var result = GenerateCode(tillUUID);  // Assuming this returns a dynamic object with GeneratedCode and ValidTime
            TempData["QRCodeBytes"] = qrr.QRCodeBytes;
            TempData["QRCodeLink"] = qrr.QRCodeLink;
            // Store the generated code and valid time in the Model properties
            var generatedCode = result.GeneratedCode;
            var validTime = result.ValidTime;

            // Pass the generated code back to the model to bind it in the Razor view
            cmpID = HttpContext.Session.GetString("cmp_id");
            ConnStr = HttpContext.Session.GetString("conString");
            QR = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, qrr.TillUUID, machineName); // Reload QR list

            // Check if the QR list is null or empty
            if (QR == null || !QR.Any())
            {
                Console.WriteLine("No QR records found.");
                return Page(); // Handle the error or show a message
            }

            // Find the QR record for the given TillUUID
            var qrRecord = QR.FirstOrDefault(qr => qr.TillUUID == tillUUID);

            if (qrRecord != null)
            {
                // Update the QR record with the generated code and valid time
                qrRecord.GeneratedCode = generatedCode.ToString();
                qrRecord.ValidTime = validTime;

                // Optionally, regenerate the QR code image based on the generated code
                var qrCodeBytes = GenerateQRCodeFromText(generatedCode.ToString());
                var qrCodeLink = GenerateQRCodeLinkFromText(tillUUID);
                qrRecord.QRCodeLink = qrCodeLink; // Use the generated code as the QR content
                qrRecord.QRCodeBytes = qrCodeBytes;
            }
            foreach (var qr in QR)
            {
                //var qrInstance = new clsQR { TillUUID = qr.TillUUID };
                // Check if TillUUID is not null or empty before generating the QR code
                if (!string.IsNullOrEmpty(qr.TillUUID))
                {
                    if (!string.IsNullOrEmpty(qr.GeneratedCode))  // If there is a generated code, create QR code
                    {
                        var qrCodeBytes = GenerateQRCodeFromText(qr.GeneratedCode.ToString());
                        var qrCodeLink = GenerateQRCodeLinkFromText(qr.TillUUID); // Generate QR for the code
                        qr.QRCodeBytes = qrCodeBytes;
                        qr.QRCodeLink = qrCodeLink;
                    }
                    else
                    {
                        // Optionally, you could generate a QR code for TillUUID if no generated code exists
                        var qrCodeBytes = GenerateQRCodeFromText(qr.TillUUID);
                        var qrCodeLink = GenerateQRCodeLinkFromText(qr.TillUUID); // Generate QR for TillUUID
                        qr.QRCodeBytes = qrCodeBytes;
                        qr.QRCodeLink = qrCodeLink;
                    }
                }
                else
                {
                    // If TillUUID is null or empty, skip QR code generation
                    qr.QRCodeBytes = null;
                    qr.QRCodeLink = null;
                }
            }

            return Page();
        }
        public JsonResult OnPostGenerateCode(string tillUUID)
        {
            if (string.IsNullOrEmpty(tillUUID))
            {
                return new JsonResult(new { success = false, message = "TillUUID is required." });
            }

            // Call the GenerateCode method to generate a new code
            var generatedCode = GenerateCode(tillUUID); // GenerateCode method you have
            var validTime = DateTime.Now.AddMinutes(30); // Code valid for 30 minutes

            // Return the generated code and validity time as a JSON response
            return new JsonResult(new
            {
                success = true,
                generatedCode = generatedCode,
                validTime = validTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        [HttpPost]
        public IActionResult OnPostGenerateQR(clsQR qr, string tillUUID, string machineName, string machineId, string locationId, string venueId)
        {
            try
            {
                // Now you have all the parameters needed for generating the QR code
                var qrr = new clsQR
                {
                    TillUUID = tillUUID,
                    machine_name = machineName,
                    //location_name = locationName,
                    //venue_name = venueName
                };


                // Get or generate StoreUUID
                string storeUUID = HttpContext.Session.GetString("conStoreuuid");

                // If StoreUUID is not set, generate a new one
                if (string.IsNullOrEmpty(storeUUID) || storeUUID.ToLower() == "null" || storeUUID == "0")
                {
                    storeUUID = Guid.NewGuid().ToString();  // Generate a new UUID for the store
                    HttpContext.Session.SetString("conStoreuuid", storeUUID);  // Store it in the session
                    HttpContext.Session.SetString("StoreUUID", storeUUID);
                }

                // Generate a new TillUUID
                string till_UUID = Guid.NewGuid().ToString();  // Create a new UUID for the Till

                // Debug log for the generated TillUUID
                Console.WriteLine($"Generated TillUUID: {tillUUID}");

                // Get connection string from the configuration
                string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");

                // Use the connection to interact with the database
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the stored procedure for QR code registration
                    using (var command = new SqlCommand("P_M_QR_Register", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure (ensure correct data types)
                        command.Parameters.AddWithValue("@venue_id", venueId);
                        command.Parameters.AddWithValue("@machine_id", machineId);
                        command.Parameters.AddWithValue("@location_id", locationId);
                        //command.Parameters.AddWithValue("@name", machineName);
                        command.Parameters.AddWithValue("@TillUUID", till_UUID);  // Pass the generated TillUUID
                        command.Parameters.AddWithValue("@StoreUUID", storeUUID);
                        //command.Parameters.AddWithValue("@Valid",Valid);

                        //.......................................entry Done but without genetatedCode and Valid..........................................
                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@ExistingTillUUID", SqlDbType.NVarChar, 200)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        int rowsAffected = command.ExecuteNonQuery();
                        Debug.WriteLine("P_M_QR_Register :rows affected : " + rowsAffected);
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("QR Code inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows affected. Check the stored procedure.");
                        }

                        qr.TillUUID = till_UUID;
                        TempData["TillUUID"] = till_UUID;
                        TempData["MachineName"] = machineName;
                        // Execute the stored procedure
                        // If successful, generate the QR code
                        string cmp_id = HttpContext.Session.GetString("cmp_id");
                        int login_id = Convert.ToInt32(HttpContext.Session.GetString("login_id"));
                        string name = HttpContext.Session.GetString("name");
                        bool qrGenerated = generateQR(cmp_id, login_id, till_UUID, machineName, machineId, locationId, venueId);

                        var qrCodeBytes = GenerateQRCodeFromText(till_UUID);  // Generate QR for TillUUID
                        var qrCodeLink = GenerateQRCodeLinkFromText(till_UUID);  // Generate QR code link

                        // Assign QR code bytes and link to the model
                        qr.QRCodeBytes = qrCodeBytes;
                        TempData["QRCodeBytes"] = qrCodeBytes;
                        qr.QRCodeLink = qrCodeLink;
                        TempData["QRCodeLink"] = qrCodeLink;
                    }
                    cmpID = HttpContext.Session.GetString("cmp_id");
                    ConnStr = HttpContext.Session.GetString("conString");

                    QR = obj.SelectAll(Convert.ToInt32(cmpID), ConnStr, tillUUID);

                    var qrForThisTill = QR.FirstOrDefault(q => q.TillUUID == tillUUID);

                    foreach (var qr1 in QR)
                    {
                        if (!string.IsNullOrEmpty(qr1.TillUUID))
                        {
                            // Call the GenerateCode method for each TillUUID (similar to SelectAll call)
                            // Here, you simulate a request to the GenerateCode action
                            //var generatedCode = GenerateCode(qr.TillUUID).GeneratedCode;

                            // Optionally store or use the generated code, for example, saving to a property
                            //qr.GeneratedCode = generatedCode.ToString(); // Assuming you add a GeneratedCode property in clsQR
                            var qrText = $"{qr1.TillUUID}";  //$"{qr.TillUUID}-{qr.machine_name}";
                                                             // Pass TillUUID (or any other relevant text you want) as qrText
                            var qrCodeBytes = GenerateQRCodeFromText(qrText); // Use TillUUID or any other relevant text
                            var qrCodeLink = GenerateQRCodeLinkFromText(qrText);
                            qr1.QRCodeLink = qrCodeLink;
                            // Assuming you store qrCodeBytes in your object (e.g., qr.QRCodeBytes)
                            qr1.QRCodeBytes = qrCodeBytes; // Store the generated QR code bytes
                        }
                        else
                        {
                            // Optionally set a default or handle the case where TillUUID is null
                            qr1.QRCodeBytes = null; // No QR code generated
                        }
                        //if (string.IsNullOrEmpty(qrText))
                        //{
                        //    ViewBag.ErrorMessage = "Please enter text to generate QR Code.";
                        //    return View();
                        //}

                        //return View("QR_List", qrCodeBytes);
                    }
                }

                return Page(); // Return the updated page
            }
            catch (Exception ex)
            {
                // Log the exception and show error message
                TempData["ErrorMessage"] = "An error occurred while generating the QR code.";
                return Page(); // Return with error message
            }
        }

        public bool generateQR(string cmp_id, int login_id, string TillUUID, string machineName, string machineId, string locationId, string venueId)
        {
            try
            {
                // Get the connection string
                string connectionString = _configuration.GetConnectionString("POS_Connection");


                // Open a SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Set up the command to call the InsertStoredProcedure (InsertMachineData)
                    using (SqlCommand command = new SqlCommand("P_M_generateforQR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@name", machineName);
                        command.Parameters.AddWithValue("@machine_id", machineId);
                        command.Parameters.AddWithValue("@location_id", locationId);
                        command.Parameters.AddWithValue("@login_id", login_id);
                        command.Parameters.AddWithValue("@cmp_id", cmp_id);
                        command.Parameters.AddWithValue("@TillUUID", TillUUID);
                        //command.Parameters.AddWithValue("@Valid");

                        // Execute the command (this will insert the data into the database)
                        int rowsAffected = command.ExecuteNonQuery(); // Returns the number of rows affected

                        // Check if the insert was successful
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your actual logging mechanism)
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }
        public byte[] GenerateQRCodeFromText(string tillUUID)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(tillUUID, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20); // Adjust the size here (20)
            Debug.WriteLine("---------------------------" + qrCodeImage);
            return (qrCodeImage);
        }

        public byte[] GenerateQRCodeLinkFromText(string tillUUID)
        {
            // Replace with your actual URL structure
            string url = $"http://{tillUUID}";
            // Create the QR code data from the link
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q); // You can adjust ECC level (Error Correction Level) here

            // Create the QR code image from the data
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20); // Adjust the size here (20 is a scaling factor)

            // Convert the image to byte array and return it
            return (qrCodeImage);
        }
        public byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);  // Save the bitmap as a PNG image
                return ms.ToArray();  // Return the byte array
            }
        }
        private byte[] GenerateQRCodeUrl(string tillUUID)
        {
            if (!string.IsNullOrEmpty(tillUUID))
            {
                // Generate the QR code image(Bitmap) based on TillUUID
                //var qrCodeImage = GenerateQRCodeImage(tillUUID); // This returns Bitmap

                // if (qrCodeImage != null)
                // {
                //     // Convert the Bitmap to a Base64 string
                //     return ConvertQRCodeToBytes(qrCodeImage); // Convert to byte array
                // }
            }
            return null;
        }

        private byte[] ConvertQRCodeToBytes(Bitmap qrCodeImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Save image as PNG in memory
                return ms.ToArray(); // Return byte array
            }
        }

        public byte[] ConvertQRCodeToBase64(Bitmap qrCode)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                qrCode.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Save image as PNG in memory
                return ms.ToArray(); // Return byte array
            }
        }


        private dynamic GenerateCode(string tillUUID)
        {
            // Simulate the same logic as in your GenerateCode action

            var generatedCode = GenerateRandomCode();
            var validTime = DateTime.Now.AddMinutes(30);
            //TimeSpan remainingTime = validTime1 - DateTime.Now;
            //int validTime = (int)remainingTime.TotalMinutes;

            // Store the generated code in the database (similar to your controller's logic)
            string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");

            // Open a SQL connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("P_M_generateforCode_controller", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Generate_code", generatedCode);
                command.Parameters.AddWithValue("@TillUUID", tillUUID);
                command.Parameters.AddWithValue("@Valid", validTime);
                int statusOfQRDb = command.ExecuteNonQuery();
                Debug.WriteLine("statusOfQRDb" + statusOfQRDb);

            }

            // Return the generated code and valid time as a dynamic object
            return new { GeneratedCode = generatedCode, ValidTime = validTime };
        }

        private int GenerateRandomCode()
        {
            var random = new Random();
            return random.Next(100000, 999999);
        }

    }
}
