using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Converted_POS.Pages.Master_Setting
{
    public class Machine_MasterModel : PageModel
    {
        MachineController obj = new MachineController();

        public static IConfiguration _configuration;
        public Machine_MasterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ConnStr { get; set; }
        public string Con_ConnStr { get; set; }
        public string cmp_id { get; set; }
        public string machine_id { get; set; }
        [BindProperty]
        public clsMachine machine { get; set; }

        [BindProperty]
        public List<SelectListItem> DTypeMachine { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> TillList { get; set; }
        public List<SelectListItem> Printers { get; set; }
        public List<SelectListItem> ChkKeymap { get; set; }
        public List<SelectListItem> ShortingNo { get; set; }
        public List<SelectListItem> Devices { get; set; }
        public List<SelectListItem> Keymaps { get; set; }
        public List<SelectListItem> FunctionList { get; set; }
        public List<SelectListItem> DeviceMappingModel { get; set; }
        public List<KeymapModel> KeymapModel { get; set; }
        //public List<DeviceMappingModel> DeviceMappingModel { get; set; }
        //[BindProperty]
        //public List<int> SelectedDeviceIds { get; set; }
        public List<SelectListItem> DTDevice { get; set; }
        public Dictionary<int, int?> SelectedDevices { get; set; } // Key = PrinterId, Value = DeviceId

        // Existing properties of your model

        public ActionResult OnGet(int? id, int function_id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            //if (function_id == 0)
            //{
            //    // Maybe log this or fallback to a default function_id or a value from the database
            //    function_id = GetFunctionIdFromDatabase(id);
            //}

            //functionId = Convert.ToInt32(HttpContext.Session.GetString("function_id"));




            //var selectedFunction = HttpContext.Session.GetString("selectedFunction"); // Get the function from query parameter

            //// Store the selected function in ViewData to access in Razor view
            //if (!string.IsNullOrEmpty(selectedFunction))
            //{
            //    ViewData["SelectedFunction"] = selectedFunction;
            //}


            //var selectedDeviceIds = Request.Query["selectedDevices"]; // Assuming this is passed as a query parameter
            //List<int> selectedDeviceList = new List<int>();

            //if (!string.IsNullOrEmpty(selectedDeviceIds))
            //{
            //    // Assuming multiple device IDs are passed as comma-separated values
            //    selectedDeviceList = selectedDeviceIds.ToString().Split(',').Select(int.Parse).ToList();
            //}
            //ViewData["SelectedDevices"] = selectedDeviceList;

            LoadDropdowns();
            if (id == null)
            {
                return Page();
            }
            else
            {


                HttpContext.Session.SetString("machine_id", HttpContext.Request.Query["ID"].ToString());

                machine = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr, function_id, Convert.ToInt32(machine_id));

                if (machine == null)
                {
                    return NotFound();
                }
                if (machine.second_screen_image_1 == null || machine.second_screen_image_1.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64Image1 = ConvertImageToBase64String(machine.second_screen_image_1);
                    ViewData["Base64Image1"] = $"data:image/png;base64,{base64Image1}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName1"] = machine.second_screen_image_1;

                if (machine.second_screen_image_2 == null || machine.second_screen_image_2.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64Image2 = ConvertImageToBase64String(machine.second_screen_image_2);
                    ViewData["Base64Image2"] = $"data:image/png;base64,{base64Image2}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName2"] = machine.second_screen_image_2;

                if (machine.sunmi_video_path != null && machine.sunmi_video_path.Length > 0)
                {
                    // Read the video file from the path (assuming sunmi_video_path is a valid file path)
                    byte[] videoBytes = System.IO.File.ReadAllBytes(machine.sunmi_video_path.ToString());
                    string base64Video = Convert.ToBase64String(videoBytes);
                    ViewData["Base64Video1"] = $"data:video/mp4;base64,{base64Video}";
                }

                ViewData["VideoFileName1"] = machine.sunmi_video_path;
                //Dictionary<int, int> selectedDevices = GetSelectedDevicesForPrinters(); // This method fetches selected devices for printers
                //ViewData["SelectedDevices"] = selectedDevices;
                var selectedDevices = GetSelectedDevicesForMachine(machine.machine_id??0);
                //foreach (var printer in Printers)
                //{
                //    selectedDevices[printer.PrinterId] = printer.SelectedDeviceId; // Populate this with actual data
                //}
                ViewData["SelectedDevices"] = selectedDevices;
                Dictionary<int, int> selectedDevicesForMachine = GetDevicesForMachine(machine.machine_id??0);
                ViewData["Devices"] = selectedDevicesForMachine;
                ViewData["SelectedDevices"] = selectedDevicesForMachine;
                //Devices = GetDevices(ConnStr);
                //ViewData["Devices"] = Devices;
                //ViewData["SelectedDevices"] = selectedDevices;

                var selectedKeymaps = GetKeymapForMachine(machine.machine_id??0); // This method should fetch the selected keymaps for this machine
                var shortingNumbers = GetShortingNumForMachine(machine.machine_id??0);
                ViewData["SelectedKeymaps"] = selectedKeymaps;
                ViewData["ShortingNumbers"] = shortingNumbers;

                ViewData["FunctionId"] = machine.function_id;
                return Page();
            }
        }

        private List<SelectListItem> GetTill(string connStr)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_M_till", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int TillId = Convert.ToInt32(dr["id"]); // Extract country_id
                            list.Add(new SelectListItem
                            {
                                Value = TillId.ToString(),
                                Text = dr["name"].ToString(),

                            });

                            //List<SelectListItem> statesForCountry = GetStatesForCountry(countryId, constr);
                            //statesDictionary.Add(countryId, statesForCountry);
                        }
                    }
                }
            }
            //ViewBag.CountryList = list;
            return list;
        }

        private int GetFunctionIdFromDatabase(int? id)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "SELECT function_id FROM M_Till_MainFunction WHERE till_id = @till_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@till_id", id);

                con.Open();
                var result = cmd.ExecuteScalar();
                con.Close();

                return result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }

        private Dictionary<int, int> GetDevicesForMachine(int machineId)
        {
            Dictionary<int, int> devicesForMachine = new Dictionary<int, int>();

            string query = "SELECT printer_id, device_id FROM M_Machine_Details WHERE machine_id = @machineId";

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@machineId", machineId);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int printerId = Convert.ToInt32(rdr["printer_id"]);
                        int deviceId = Convert.ToInt32(rdr["device_id"]);

                        // Add the device-printer mapping to the dictionary
                        devicesForMachine[printerId] = deviceId;
                    }
                }
                con.Close();
            }

            return devicesForMachine;
        }

        //private Dictionary<int, int> GetSelectedDevicesForPrinters()
        //{
        //    throw new NotImplementedException();
        //}

        private Dictionary<int, int> GetSelectedDevicesForMachine(int machineId)
        {
            var SelectedDevices = new Dictionary<int, int>();

            // Example: Query the database for the shorting numbers associated with this machine and its keymaps
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "select Printer_id , Device_id from M_Machine_Details WHERE machine_id = @machine_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@machine_id", machineId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) // Check if rows exist
                        {
                            while (reader.Read())
                            {
                                int printerId = Convert.ToInt32(reader["Printer_id"]);
                                int deviceId = Convert.ToInt32(reader["Device_id"]);
                                Console.WriteLine($"PrinterId: {printerId}, DeviceId: {deviceId}"); // Log the data
                                SelectedDevices[printerId] = deviceId;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows returned for the given machine_id");
                        }
                    }

                    con.Close();
                }
            }

            return SelectedDevices;
        }

        private Dictionary<int, int?> GetShortingNumForMachine(int machineId)
        {
            var shortingNumbers = new Dictionary<int, int?>();

            // Example: Query the database for the shorting numbers associated with this machine and its keymaps
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "SELECT keymap_id, shorting_num FROM M_Till_Keymap WHERE machine_id = @machine_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@machine_id", machineId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int keymapId = Convert.ToInt32(reader["keymap_id"]);
                            int shortingNum = Convert.ToInt32(reader["shorting_num"]);
                            shortingNumbers[keymapId] = shortingNum; // Store the shorting number for each keymap_id
                        }
                    }
                    con.Close();
                }
            }

            return shortingNumbers;
        }

        private Dictionary<int, bool> GetKeymapForMachine(int machineId)
        {
            var keymaps = new Dictionary<int, bool>();

            // Example: Query the database for the keymaps associated with this machine
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "SELECT keymap_id FROM M_Till_Keymap WHERE machine_id = @machine_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@machine_id", machineId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int keymapId = (int)reader.GetDecimal(0); // Assuming keymap_id is the first column
                            keymaps[keymapId] = true; // Mark the keymap as selected
                        }
                    }
                    con.Close();
                }
            }

            return keymaps;
        }

        private string ConvertImageToBase64String(byte[] videoData)
        {
            if (videoData != null && videoData.Length > 0)
            {
                return Convert.ToBase64String(videoData); // Convert byte[] to Base64 string
            }
            return null;
        }
        private void LoadDropdowns()
        {
            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");
            DTypeMachine = new List<SelectListItem>
    {
        new SelectListItem { Text = "Kinetic Saturn", Value = "Kinetic Saturn" },
        new SelectListItem { Text = "iMin D1", Value = "iMin D1" },
        new SelectListItem { Text = "iMin D4", Value = "iMin D4" },
        new SelectListItem { Text = "iMin Falcon", Value = "iMin Falcon" },
        new SelectListItem { Text = "Sunmi D2", Value = "Sunmi D2" },
        new SelectListItem { Text = "Sunmi T2", Value = "Sunmi T2" },
        new SelectListItem { Text = "Sunmi T2s", Value = "Sunmi T2s" },
        new SelectListItem { Text = "PAX A920", Value = "PAX A920" },
        new SelectListItem { Text = "iMin Swan", Value = "iMin Swan" },
        new SelectListItem { Text = "Sunmi T2SLite", Value = "Sunmi T2SLite" }
    };
            LocationList = GetLocation(ConnStr);
            TillList = GetTill(ConnStr);
            FunctionList = GetFunction(ConnStr);
            DeviceMappingModel = GetDevices(ConnStr);
            Printers = GetPrinters(ConnStr);
            //ChkKeymap = GetKeymap(ConnStr);
            KeymapModel = GetKeymap(ConnStr);

        }

            //[HttpPost("Save")]
            //public ActionResult OnPost(IFormFile ImageFile1, IFormFile ImageFile2, IFormFile VideoFile, string ImageFileName1, string ImageFileName2, string VideoFileName/*Dictionary<string, int> deviceNames*//*, Dictionary<int, int> selectedDevices,*/, Dictionary<int, bool> selectedKeymaps, Dictionary<int, int> shortingNumbers/*,Dictionary<int, int> deviceMappings*/, int function_id, int machine_id/*, int TillId,*/ /*int cmp_id, int machine_id*/)
            //{
            [HttpPost("Save")]
        public ActionResult OnPost(
            IFormFile ImageFile1,
            IFormFile ImageFile2,
            IFormFile VideoFile,
            string ImageFileName1,
            string ImageFileName2,
            string VideoFileName,
            Dictionary<int, int> selectedDevices,   // Bind selected devices dictionary
            Dictionary<int, bool> selectedKeymaps,
            Dictionary<int, int?> shortingNumbers,
            int? function_id,
            string Receipt_Header, // Added to capture receipt header
            string Receipt_Footer)  // Added to capture receipt footer)
        {
            //machine.Receipt_Header = Receipt_Header;
            //machine.Receipt_Footer = Receipt_Footer;


            if (selectedKeymaps == null)
            {
                selectedKeymaps = new Dictionary<int, bool>();  // Initialize as empty dictionary
            }

            // Ensure that shortingNumbers is initialized as an empty dictionary if it's null
            if (shortingNumbers == null)
            {
                shortingNumbers = new Dictionary<int, int?>();  // Initialize as empty dictionary
            }

            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging  
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    var errors = kvp.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
                    }
                }
            }
            if (selectedKeymaps == null)
            {
                selectedKeymaps = new Dictionary<int, bool>();  // Default to an empty dictionary
            }
            if (selectedKeymaps != null)
            {
                foreach (var keymapEntry in selectedKeymaps)
                {
                     
                    if (keymapEntry.Value == null)
                    {
                        selectedKeymaps[keymapEntry.Key] = false;
                    }
                }
            }
            if (shortingNumbers == null)
            {
                shortingNumbers = new Dictionary<int, int?>(); 
            }
            string sessionValue = HttpContext.Session.GetString("machine_id")??"0";
            int machID= HttpContext.Session.GetString("machine_id") != null
                        ? Convert.ToInt32(HttpContext.Session.GetString("machine_id"))
                        : 0;
            machine.machine_id = machID;
            if (int.TryParse(sessionValue, out int parsedId))
            {
                machine.machine_id = parsedId;
            }
            else
            {
                machine.machine_id = 0; // or 0 if you prefer
            }
            machine.Mainip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            Con_ConnStr = _configuration.GetConnectionString("POS_ControllerConnection");
            machine.machine_id = Convert.ToInt32(HttpContext.Session.GetString("machine_id"));
            machine.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            machine.function_id = function_id??0;
            Console.WriteLine("machine_id raw value: " + Request.Form["machine.machine_id"]);
            Con_ConnStr = _configuration.GetConnectionString("POS_ControllerConnection");
            ConnStr = HttpContext.Session.GetString("conString");
            bool hasError = false;

            if (string.IsNullOrEmpty(machine.name))
            {
                ModelState.AddModelError("machine.name", "Name field is required.");
                hasError = true;
            }

            if (string.IsNullOrEmpty(machine.ip_address))
            {
                ModelState.AddModelError("machine.ip_address", "IP Address is required.");
                hasError = true;
            }

            if (!machine.location_id.HasValue || machine.location_id == 0)
            {
                ModelState.AddModelError("machine.location_id", "Please select a valid location.");
                hasError = true;
            }
            if (string.IsNullOrEmpty(machine.name))
            {
                ModelState.AddModelError("machine.name", "Name field is required.");
            }
            if (string.IsNullOrEmpty(machine.ip_address))
            {
                ModelState.AddModelError("machine.ip_address", "IP Address is required.");
            }
            if (!machine.location_id.HasValue || machine.location_id <= 0)
            {
                ModelState.AddModelError("machine.location_id", "Please select a valid location.");
            }

            if (hasError)
            {
                 
                ModelState.AddModelError(string.Empty, "Please fill all required fields.");
                LoadDropdowns();
                return Page();

            }

            if (selectedKeymaps != null && selectedKeymaps.Any())
            {
                foreach (var keymapEntry in selectedKeymaps)

                {
                    int keymapId = Convert.ToInt32(keymapEntry.Key); // Extract keymapId from the dictionary key
                    bool isChecked = keymapEntry.Value;

                    //int shortingNum = shortingNumbers.ContainsKey(keymapId) ? shortingNumbers[keymapId] : 0;
                    //int tillId = GetTillId(keymapId, ConnStr);
                    // Check if the checkbox was selected (true or false)
                    if (keymapEntry.Value)
                    {
                        // Retrieve the shorting number associated with this keymapId
                        //int shortingNum = shortingNumbers.ContainsKey(keymapEntry.Key) ? shortingNumbers[keymapEntry.Key] : 0;

      
                        int shortingNum = 0;
                        if (shortingNumbers != null && shortingNumbers.ContainsKey(keymapEntry.Key) && shortingNumbers[keymapEntry.Key].HasValue)
                        {
                            shortingNum = shortingNumbers[keymapEntry.Key].Value;
                        }
                        //int shortingNum = shortingNumbers.ContainsKey(keymapEntry.Key) && shortingNumbers[keymapEntry.Key].HasValue
                        //? shortingNumbers[keymapEntry.Key].Value
                        //: 0;
                        int tillId = GetTillId(keymapId, ConnStr);
                        if (isChecked)
                        {

                            // Check if the keymap exists or needs to be inserted/updated
                            if (KeymapExists(keymapId, tillId)) // Assuming this method checks if the keymap exists
                            {
                                // Update the keymap
                                InsertKeymap(machine.cmp_id, machine.machine_id??0, keymapId, tillId, shortingNum);
                                //UpdateKeymap(machine.cmp_id, machine.machine_id, keymapId, tillId, shortingNum);
                            }
                            else
                            {
                                // Insert the keymap
                                UpdateKeymap(machine.cmp_id, machine.machine_id??0, keymapId, tillId, shortingNum);
                            }
                        }
                        else
                        {
                            // Handle the unselected (unchecked) checkboxes here
                            // You might want to either:
                            // 1. Remove the keymap if it is no longer needed
                            // 2. Update it with a default value or status
                            // Here's an example where we mark it as inactive/removed
                            if (KeymapExists(keymapId, tillId))
                            {
                                // Update the keymap for unselected checkbox (perhaps marking it as inactive or deleting)
                                // Example: Mark it as inactive (you may update based on your logic)
                                UpdateKeymap(machine.cmp_id, machine.machine_id??0, keymapId, tillId, shortingNum);
                            }
                        }
                    }
                }
            }

           

            //List<DeviceMappingModel> selectedDevicesForMachineList = new List<DeviceMappingModel>();
            //foreach (var deviceEntry in selectedDevices)
            //{
            //    int printerId = Convert.ToInt32(deviceEntry.Key);  // Printer ID
            //    int deviceId = deviceEntry.Value; // Selected Device ID

            //    if (deviceId != 0) // If a device is selected (i.e., deviceId is not 0)
            //    {
            //        //obj.Insert(machine, ConnStr, function_id);
            //        // Insert the device-printer mapping (you can call your insert method here)
            //        InsertDevice(machine.cmp_id, machine.machine_id??0, printerId, deviceId, machine.machine_detail_id);
            //        SelectPrinterDeviceByMachine(machine.cmp_id,machine.machine_id??0);
            //        selectedDevicesForMachineList.Add(new DeviceMappingModel
            //        {
            //            PrinterId = printerId,
            //            DeviceId = deviceId
            //        });
            //        //selectedDevicesForMachine[printerId] = deviceId;
            //    }
            //}
            //ViewData["Devices"] = selectedDevicesForMachineList;

            //foreach (var printer in selectedDevices)
            //{
            //    // printer.Key = Printer ID
            //    // printer.Value = Selected Device ID

            //    //SaveSelectedDeviceForPrinter(printer.Key, printer.Value);
            //}
            if (ImageFile1 != null && ImageFile1.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ImageFile1.CopyToAsync(memoryStream); // Asynchronously copy the file
                    machine.second_screen_image_1 = memoryStream.ToArray(); // Convert to byte array
                    machine.ImageFileName1 = ImageFile1.FileName; // Store the file name for Image 1
                }
            }
            else if (!string.IsNullOrEmpty(ImageFileName1))
            {
                // If no file was uploaded, use the base64 string data (for existing image)
                byte[] existingImage1 = Convert.FromBase64String(ImageFileName1.Replace("data:image/png;base64,", ""));
                machine.second_screen_image_1 = existingImage1;
            }
            else
            {
                machine.second_screen_image_1 = null; // If no image exists, set to null
            }
            if (ImageFile2 != null && ImageFile2.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ImageFile2.CopyToAsync(memoryStream); // Asynchronously copy the file
                    machine.second_screen_image_2 = memoryStream.ToArray(); // Convert to byte array
                    machine.ImageFileName2 = ImageFile2.FileName; // Store the file name for Image 2
                }
            }
            else if (!string.IsNullOrEmpty(ImageFileName2))
            {
                // If no file was uploaded, use the base64 string data (for existing image)
                byte[] existingImage2 = Convert.FromBase64String(ImageFileName2.Replace("data:image/png;base64,", ""));
                machine.second_screen_image_2 = existingImage2;
            }
            else
            {
                machine.second_screen_image_2 = null; // If no image exists, set to null
            }

            if (machine.machine_id == null || machine.machine_id == 0)
            {
                machine.machine_id = 0;
                obj.Insert(machine, ConnStr, function_id??0, VideoFile);
                if (selectedKeymaps != null)
                {
                    foreach (var keymapEntry in selectedKeymaps)
                    {
                        int keymapId = Convert.ToInt32(keymapEntry.Key); // Extract keymapId from the dictionary key
                        bool isChecked = keymapEntry.Value;

                        //int shortingNum = shortingNumbers.ContainsKey(keymapId) ? shortingNumbers[keymapId] : 0;
                        //int tillId = GetTillId(keymapId, ConnStr);
                        // Check if the checkbox was selected (true or false)
                        if (keymapEntry.Value)
                        {
                            // Retrieve the shorting number associated with this keymapId
                          //  int shortingNum = shortingNumbers.ContainsKey(keymapEntry.Key) ? shortingNumbers[keymapEntry.Key] : 0;
                            int shortingNum = shortingNumbers.ContainsKey(keymapEntry.Key) && shortingNumbers[keymapEntry.Key].HasValue
                             ? shortingNumbers[keymapEntry.Key].Value
                             : 0;

                            decimal amount = 0;
                            string paymentMode = "";
                            decimal balance = 0;

                            if (decimal.TryParse(Request.Form["amount"], out decimal amt))
                                amount = amt;

                            paymentMode = Request.Form["paymentMode"].ToString();

                            if (decimal.TryParse(Request.Form["balance"], out decimal bal))
                                balance = bal;

                            // Ab aap database mein insert/update kar sakte ho
                            //InsertPayment(machine.machine_id, amount, paymentMode, balance);
                            int tillId = GetTillId(keymapId, ConnStr);
                            if (isChecked)
                            {

                                // Check if the keymap exists or needs to be inserted/updated
                                if (KeymapExists(keymapId, tillId)) // Assuming this method checks if the keymap exists
                                {
                                    // Update the keymap
                                    InsertKeymap(machine.cmp_id, machine.machine_id??0, keymapId, tillId, shortingNum);
                                    //UpdateKeymap(machine.cmp_id, machine.machine_id, keymapId, tillId, shortingNum);
                                }
                                else
                                {
                                    // Insert the keymap
                                    UpdateKeymap(machine.cmp_id,machine.machine_id??0, keymapId, tillId, shortingNum);
                                }
                            }
                        }
                    }
                }
                    InsertFunction(machine.cmp_id, machine.till_id, machine.machine_id??0, function_id??0, ConnStr);
                foreach (var deviceEntry in selectedDevices)
                {
                    int printerId = Convert.ToInt32(deviceEntry.Key);
                    int deviceId = deviceEntry.Value;
                    InsertDevice(machine.cmp_id, machine.machine_id??0, printerId, deviceId, machine.machine_detail_id);
                }
                //obj.Insert(machine, ConnStr, function_id);
            }
            else
            {
                obj.Update(machine, ConnStr, function_id??0, VideoFile);
                UpdateFunction(machine.cmp_id, machine.till_id,  machine.machine_id??0, function_id??0, ConnStr);
                GetSelectedDevicesForMachine(machine.machine_id??0);
                foreach (var deviceEntry in selectedDevices)
                {
                    int printerId = Convert.ToInt32(deviceEntry.Key);
                    int deviceId = deviceEntry.Value;
                    InsertDevice(machine.cmp_id, machine.machine_id ?? 0, printerId, deviceId, machine.machine_detail_id);
                }
            }

            HttpContext.Session.Remove("machine_id");
            return RedirectToPage("/Master_Setting/Machine_List");
        }

        //private void RegisterQr(clsMachine machine, string connStr)
        //{
        //    var storeUuid = HttpContext.Session.GetString("conStoreuuid") ?? string.Empty;

        //    // Check if the storeUuid is empty, "null", or "0", and generate a new one if necessary
        //    if (string.IsNullOrEmpty(storeUuid) || storeUuid.ToLower() == "null" || storeUuid == "0")
        //    {
        //        var enableGuid = Guid.NewGuid().ToString();
        //        using (SqlConnection con = new SqlConnection(connStr))
        //        {
        //            SqlCommand cmd = new SqlCommand("Storeuuid_enable", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            // Add parameters to the SqlCommand correctly
        //            cmd.Parameters.Add(new SqlParameter("@store_name", HttpContext.Session.GetString("Storename")));
        //            cmd.Parameters.Add(new SqlParameter("@store_uuid", enableGuid));

        //            con.Open();  // Open the connection
        //            cmd.ExecuteNonQuery(); // Execute the stored procedure
        //            con.Close(); // Close the connection
        //        }

        //        // Save the new store UUID in the session
        //        HttpContext.Session.SetString("conStoreuuid", enableGuid);
        //    }

        //    // Generate new UUIDs for machine and till
        //    var machineUuid = Guid.NewGuid().ToString();
        //    var tillUuid = Guid.NewGuid().ToString();

        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_QR_Register", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        // Add parameters to the SqlCommand correctly
        //        var venueId = HttpContext.Session.GetString("venue_id");
        //        if (int.TryParse(venueId, out int venueIdInt))
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@venue_id", venueIdInt));
        //        }
        //        else
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@venue_id", DBNull.Value)); // Handle invalid input (null/empty)
        //        }

        //        // Ensure that machine_id is an integer (if it should be an integer)
        //        cmd.Parameters.Add(new SqlParameter("@machine_id", machine.machine_id));

        //        // Ensure that location_id is an integer (if it should be an integer)
        //        var locationIdString = HttpContext.Request.Form["LocationId"];
        //        if (int.TryParse(locationIdString, out int locationIdInt))
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@location_id", locationIdInt));
        //        }
        //        else
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@location_id", DBNull.Value)); // Handle invalid input (null/empty)
        //        }

        //        cmd.Parameters.Add(new SqlParameter("@TillUUID", tillUuid));
        //        cmd.Parameters.Add(new SqlParameter("@StoreUUID", HttpContext.Session.GetString("StoreUUID")));
        //        cmd.Parameters.Add(new SqlParameter("@ExistingTillUUID", DBNull.Value));

        //        con.Open();  // Open the connection
        //        cmd.ExecuteNonQuery(); // Execute the stored procedure
        //        con.Close(); // Close the connection
        //    }

        //    // Optionally, handle the result of the stored procedure if needed

        //    // Update the machine entity to include the generated UUIDs and save it
        //    machine.TillUUID = tillUuid;
        //    //machine.MachineUUID = machineUuid;

        //    // Assuming you want to update the machine in the database or further processing.
        //    //_context.Machines.Update(machine); // Update the machine record in the database
        //    //_context.SaveChanges(); // Save the changes to the database
        //}


        //private void Insert(clsMachine machine, string connStr, int function_id, string base64Video)
        //{
        //    int newMachineId;
        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Machine", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // Add other parameters as you already have them
        //        cmd.Parameters.AddWithValue("@cmp_id", machine.cmp_id);
        //        cmd.Parameters.AddWithValue("@machine_id ", machine.machine_id);
        //        cmd.Parameters.AddWithValue("@name", machine.name);
        //        cmd.Parameters.AddWithValue("@serial_no", machine.serial_no);
        //        cmd.Parameters.AddWithValue("@mac_address", machine.mac_address);
        //        cmd.Parameters.AddWithValue("@model", machine.model);
        //        cmd.Parameters.AddWithValue("@code", "0");
        //        cmd.Parameters.AddWithValue("@ip_address", string.IsNullOrEmpty(machine.ip_address) ? DBNull.Value : (object)machine.ip_address);
        //        cmd.Parameters.AddWithValue("@login_id", machine.login_id);
        //        cmd.Parameters.AddWithValue("@location_id", machine.location_id);
        //        cmd.Parameters.AddWithValue("@hardware_type", machine.hardware_type);
        //        cmd.Parameters.AddWithValue("@is_active", machine.is_active);
        //        cmd.Parameters.AddWithValue("@is_assign", machine.is_assign);
        //        cmd.Parameters.AddWithValue("@till_server", machine.till_server);
        //        cmd.Parameters.AddWithValue("@is_master_self", machine.is_master_self);
        //        cmd.Parameters.AddWithValue("@extraSurcharge", machine.extraSurcharge);
        //        cmd.Parameters.AddWithValue("@onlytabletrans", machine.Only_table_trans);
        //        cmd.Parameters.AddWithValue("@AutoSurcharge", machine.AutoSurcharge);
        //        cmd.Parameters.AddWithValue("@NoCashDrawer", machine.NoCashDrawer);
        //        cmd.Parameters.AddWithValue("@ReSync_Request", machine.ReSync_Request);
        //        cmd.Parameters.AddWithValue("@Is_PrintServer", machine.Is_PrintServer);
        //        cmd.Parameters.AddWithValue("@Service_Controller_Start", machine.Service_Controller_Start);
        //        cmd.Parameters.AddWithValue("@Service_Websale_print", machine.Service_Websale_print);
        //        cmd.Parameters.AddWithValue("@Service_Print_Share", machine.Service_Print_Share);
        //        cmd.Parameters.AddWithValue("@Service_print_Share_Other_Till", machine.Service_print_Share_Other_Till);
        //        cmd.Parameters.AddWithValue("@Is_ServiceBooking", machine.Is_ServiceBooking);
        //        cmd.Parameters.AddWithValue("@Is_OnlineZreport", machine.Is_OnlineZreport);
        //        cmd.Parameters.AddWithValue("@ElinaTran", machine.ElinaTran);
        //        cmd.Parameters.AddWithValue("@QuickTran", machine.QuickTran);
        //        cmd.Parameters.AddWithValue("@table_sharing", machine.table_sharing);
        //        cmd.Parameters.AddWithValue("@printer_sharing", machine.printer_sharing);
        //        cmd.Parameters.AddWithValue("@back_to_main_function_on_till", machine.back_to_main_function_on_till);
        //        cmd.Parameters.AddWithValue("@Is_NoLogout", machine.Is_NoLogout);
        //        cmd.Parameters.AddWithValue("@poslite", machine.poslite);
        //        cmd.Parameters.AddWithValue("@gtway_TID", machine.gtway_TID);
        //        cmd.Parameters.AddWithValue("@sync_time", machine.sync_time);
        //        cmd.Parameters.AddWithValue("@SecondScreenImage1", machine.second_screen_image_1);
        //        cmd.Parameters.AddWithValue("@SecondScreenImage2", machine.second_screen_image_2);
        //        cmd.Parameters.AddWithValue("@sunmi_video_path", base64Video);

        //        // Here we add the video data directly
        //        //if (videoData != null && videoData.Length > 0)
        //        //{
        //        //    cmd.Parameters.Add("@sunmi_video_path", SqlDbType.VarBinary).Value = videoData;
        //        //}
        //        //else
        //        //{
        //        //    cmd.Parameters.AddWithValue("@sunmi_video_path", DBNull.Value);
        //        //}

        //        cmd.Parameters.AddWithValue("@Tran_Type", "I");

        //        con.Open();
        //        //newMachineId = Convert.ToInt32(cmd.ExecuteScalar());
        //        //machine.machine_id = newMachineId;
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        private void UpdateFunction(int cmp_id, int till_id, int machine_id, int function_id, string connStr)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_MainFunction_check", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@function_id", function_id);
                //cmd.Parameters.AddWithValue("@Machine_Detail_Id", machine_detail_id);
                //cmd.Parameters.AddWithValue("@Printer_id", printerId);
                //cmd.Parameters.AddWithValue("@Device_id", deviceId);
                //cmd.Parameters.AddWithValue("@login_id", 0);
                //cmd.Parameters.AddWithValue("@ip_address", "192.168.1.1");
                //cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                cmd.Parameters.AddWithValue("@till_id", till_id);
                //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "U"); // Assuming "I" for Insert

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //private void SaveSelectedDeviceForPrinter(string key, int value)
        //{
        //    throw new NotImplementedException();
        //}

        private void InsertFunction(int cmp_id, int till_id, int machine_id, int function_id, string connStr)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_MainFunction_check", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@function_id", function_id);
                //cmd.Parameters.AddWithValue("@Machine_Detail_Id", machine_detail_id);
                //cmd.Parameters.AddWithValue("@Printer_id", printerId);
                //cmd.Parameters.AddWithValue("@Device_id", deviceId);
                //cmd.Parameters.AddWithValue("@login_id", 0);
                //cmd.Parameters.AddWithValue("@ip_address", "192.168.1.1");
                //cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                cmd.Parameters.AddWithValue("@till_id", till_id);
                //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //private void InsertFunction(int cmp_id, int TillId, int machine_id, int function_id)
        //{
        //    using (SqlConnection con = new SqlConnection(ConnStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_MainFunction_check", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
        //        cmd.Parameters.AddWithValue("@machine_id", machine_id);
        //        cmd.Parameters.AddWithValue("@function_id", function_id);
        //        //cmd.Parameters.AddWithValue("@Machine_Detail_Id", machine_detail_id);
        //        //cmd.Parameters.AddWithValue("@Printer_id", printerId);
        //        //cmd.Parameters.AddWithValue("@Device_id", deviceId);
        //        //cmd.Parameters.AddWithValue("@login_id", 0);
        //        //cmd.Parameters.AddWithValue("@ip_address", "192.168.1.1");
        //        //cmd.Parameters.AddWithValue("@keymap_id", keymapId);
        //        cmd.Parameters.AddWithValue("@till_id", TillId);
        //        //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        private List<object> SelectPrinterDeviceByMachine(int cmp_id, int machine_id)
        {
            List<object> devicePrinterMappings = new List<object>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Get_PrinterDevice_By_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);

                //cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    bool hasRows = rdr.HasRows;
                    Console.WriteLine($"Has rows: {hasRows}");
                    while (rdr.Read())
                    {
                        // Add each row to the devicePrinterMappings list
                        devicePrinterMappings.Add(new
                        {
                            MachineId = Convert.ToInt32(rdr["Machine_id"]),
                            PrinterId = Convert.ToInt32(rdr["Printer_id"]),
                            DeviceId = Convert.ToInt32(rdr["Device_id"])
                        });
                    }
                }
                //cmd.ExecuteNonQuery();
                con.Close();
            }
            return devicePrinterMappings;
        }

        private void InsertDevice(int cmp_id, int machine_id, int printerId, int deviceId, int machine_detail_id)
        {
            int newMachineId;
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Machine_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@Machine_Detail_Id", machine_detail_id);
                cmd.Parameters.AddWithValue("@Printer_id", printerId);
                cmd.Parameters.AddWithValue("@Device_id", deviceId);
                cmd.Parameters.AddWithValue("@login_id", 0);
                cmd.Parameters.AddWithValue("@ip_address",machine.Mainip_address);
                //cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                //cmd.Parameters.AddWithValue("@till_id", tillId);
                //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void UpdateDevice(int cmp_id, int machine_id, int printerId, int deviceId, int machine_detail_id)
        {
            int newMachineId;
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Machine_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@Machine_Detail_Id", machine_detail_id);
                cmd.Parameters.AddWithValue("@Printer_id", printerId);
                cmd.Parameters.AddWithValue("@Device_id", deviceId);
                cmd.Parameters.AddWithValue("@login_id", 0);
                cmd.Parameters.AddWithValue("@ip_address", machine.Mainip_address);
                //cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                //cmd.Parameters.AddWithValue("@till_id", tillId);
                //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "U"); // Assuming "I" for Insert

                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private bool KeymapExists(int keymapId, int tillId)
        {
            bool exists = false;
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "SELECT COUNT(1) FROM M_Till_Keymap WHERE keymap_id = @keymapId and till_id = @tillId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@keymapId", keymapId);
                    cmd.Parameters.AddWithValue("@tillId", tillId);
                    con.Open();
                    exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    con.Close();
                }
            }
            return exists;

        }



        private int GetTillId(int keymapId, string connStr)
        {
            int tillId = 0;
            int shorting_num = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Query to fetch till_id for the provided keymapId
                string query = "SELECT * FROM M_Till_Keymap WHERE keymap_id = @keymapId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keymapId", keymapId);

                    // Execute the query and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            tillId = Convert.ToInt32(reader["till_id"]);
                            shorting_num = Convert.ToInt32(reader["shorting_num"]);
                        }
                    }
                }
            }

            return tillId;
        }
        public void UpdateKeymap(int cmp_id, int machine_id, int keymapId, int tillId, int shortingNum)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_keymap_check", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                cmd.Parameters.AddWithValue("@till_id", tillId);
                cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "U"); // Assuming "I" for Insert

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void InsertKeymap(int cmp_id, int machine_id, int keymapId, int tillId, int shortingNum)
        {
            int till_Id = GetTillIdByMachineId(machine_id, keymapId); // Retrieve the tillId based on machine_id

            // If tillId is found, proceed with insertion
            if (tillId != -1)
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                {
                    SqlCommand cmd = new SqlCommand("P_M_keymap_check", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                    cmd.Parameters.AddWithValue("@machine_id", machine_id);
                    cmd.Parameters.AddWithValue("@keymap_id", keymapId);
                    cmd.Parameters.AddWithValue("@till_id", till_Id); // Now using the fetched tillId
                    cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                // Handle case when tillId is not found (could log an error or throw an exception)
                Console.WriteLine("tillId not found for the given machine_id");
            }
        }

        private int GetTillIdByMachineId(int machine_id, int keymapId)
        {
            int tillId = -1; // Default value if tillId is not found

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "SELECT till_id FROM M_Till_Keymap WHERE machine_id = @machine_id and keymap_id = @keymap_id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@machine_id", machine_id);
                    cmd.Parameters.AddWithValue("@keymap_id", keymapId);

                    con.Open();
                    var result = cmd.ExecuteScalar();
                    con.Close();

                    if (result != null && result != DBNull.Value)
                    {
                        tillId = Convert.ToInt32(result);
                    }
                }
            }

            return tillId;
        }

        private List<KeymapModel> GetKeymap(string connStr)
        {
            List<KeymapModel> list = new List<KeymapModel>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_M_KeyMapTill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    //cmd.Parameters.Add(new SqlParameter("@machine_id", SqlDbType.Int)).Value = 0;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows) // Check if there are rows returned
                        {
                            while (dr.Read())
                            {
                                KeymapModel keymap = new KeymapModel
                                {
                                    keymapId = Convert.ToInt32(dr["key_map_id"]), // Get the keymap ID
                                    Text = dr["name"].ToString(),  // Get the keymap name (or text)
                                                                   //shortingNum = dr["shorting_num"] != DBNull.Value ? Convert.ToInt32(dr["shorting_num"]) : 0,  // Set to 0 if no value
                                                                   //IsChecked = selectedKeymaps.ContainsKey(Convert.ToInt32(dr["key_map_id"])) ? selectedKeymaps[Convert.ToInt32(dr["key_map_id"])] : false
                                };

                                list.Add(keymap);
                            }
                        }
                        else
                        {
                            // Log or handle the case where no printers are returned
                            Console.WriteLine("No printers found.");
                        }
                    }
                }
            }

            return list;
        }

        private List<SelectListItem> GetPrinters(string connStr)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_Printer_Device_Detail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    //cmd.Parameters.Add(new SqlParameter("@machine_id", SqlDbType.Int)).Value = 0;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows) // Check if there are rows returned
                        {
                            while (dr.Read())
                            {
                                int printer_id = Convert.ToInt32(dr["printer_id"]);
                                string printerName = dr["Printer"].ToString();

                                // Check if data is being returned
                                Console.WriteLine($"Printer ID: {printer_id}, Printer Name: {printerName}");

                                list.Add(new SelectListItem
                                {
                                    Value = printer_id.ToString(),
                                    Text = printerName
                                });
                            }
                        }
                        else
                        {
                            // Log or handle the case where no printers are returned
                            Console.WriteLine("No printers found.");
                        }
                    }
                }
            }

            return list;
        }
        private List<SelectListItem> GetDevices(string connStr)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_Device_Detail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows) // Check if there are rows returned
                        {
                            while (dr.Read())
                            {
                                SelectListItem DeviceMapping = new SelectListItem
                                {

                                    Value = dr["device_id"].ToString(),  // The device ID will be the Value
                                    Text = dr["Device"].ToString()    // Get the keymap name (or text)
                                                                      //shortingNum = dr["shorting_num"] != DBNull.Value ? Convert.ToInt32(dr["shorting_num"]) : 0,  // Set to 0 if no value
                                                                      //IsChecked = selectedKeymaps.ContainsKey(Convert.ToInt32(dr["key_map_id"])) ? selectedKeymaps[Convert.ToInt32(dr["key_map_id"])] : false
                                };

                                list.Add(DeviceMapping);
                            }
                        }
                        else
                        {
                            // Log or handle the case where no printers are returned
                            Console.WriteLine("No printers found.");
                        }
                    }
                }
            }
            //ViewBag.CountryList = list;
            ViewData["Devices"] = list;

            return list;
        }

        private List<SelectListItem> GetFunction(string connStr)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_M_FunctionMap", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    //cmd.Parameters.Add(new SqlParameter("@machine_id", SqlDbType.Int)).Value = 0;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int mainfunction_id = Convert.ToInt32(dr["mainfunction_id"]); // Extract country_id
                            list.Add(new SelectListItem
                            {
                                Value = mainfunction_id.ToString(),
                                Text = dr["name"].ToString(),
                            });

                            //List<SelectListItem> statesForCountry = GetStatesForCountry(countryId, constr);
                            //statesDictionary.Add(countryId, statesForCountry);
                        }
                    }
                }
            }
            //ViewBag.CountryList = list;
            return list;
        }

        private List<SelectListItem> GetLocation(string connStr)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_Location_By_Cmp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the @cmp_id parameter to the SqlCommand
                    cmd.Parameters.Add(new SqlParameter("@cmp_id", SqlDbType.Int)).Value = cmp_id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int locationId = Convert.ToInt32(dr["location_id"]); // Extract country_id
                            list.Add(new SelectListItem
                            {
                                Value = locationId.ToString(),
                                Text = dr["name"].ToString(),

                            });

                            //List<SelectListItem> statesForCountry = GetStatesForCountry(countryId, constr);
                            //statesDictionary.Add(countryId, statesForCountry);
                        }
                    }
                }
            }
            //ViewBag.CountryList = list;
            return list;
        }


        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("machine_id");
            return RedirectToPage("/Master_Setting/Machine_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("machine_id");
            return RedirectToPage("/Master_Setting/Machine_List");
        }
    }
}
