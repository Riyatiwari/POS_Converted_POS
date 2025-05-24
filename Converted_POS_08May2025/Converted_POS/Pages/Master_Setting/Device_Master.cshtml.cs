using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Converted_POS.Pages.Master_Setting
{
    public class Device_MasterModel : PageModel
    {
        DeviceController obj = new DeviceController();

        [BindProperty]
        public clsDevice DTDevice { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public int Device_Type_id { get; set; }
        public List<SelectListItem> MachineList { get; set; }
        public List<SelectListItem> DTDeviceCategory { get; set; }
        public List<SelectListItem> DeviceList { get; set; }
        public List<SelectListItem> DTDeviceType { get; set; }
        public int? SelectedDeviceCategoryId { get; set; }
        public int? SelectedDeviceTypeId { get; set; }
        public int? SelectedTillId { get; set; }
        public int? deviceTillId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? deviceCategoryId { get; set; }
        public int? deviceTypeId { get; set; }
        public int Device_SubType { get; set; }
        public string network_type { get; set; }
        public List<SelectListItem> DeviceTypeCatList { get; set; }

        public IActionResult OnGet(int? id, int? deviceCategoryId, int deviceTypeId, int? deviceTillId, int selectedTillId, string device_id, string deviceType, string ConnStr, string network_type)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            ConnStr = HttpContext.Session.GetString("conString");
            int cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            var machines = GetMachine(cmp_id, ConnStr, deviceTillId ?? 0);

            ViewData["Machines"] = machines;
            ViewData["SelectedDeviceTillId"] = deviceTillId;
            //var model = new clsDevice();
            //if (id.HasValue)
            //{
            //    model.device_id = id.Value;
            //    var device = GetDeviceById(id.Value,device_id, cmp_id, ConnStr);
            //    if (device != null)
            //    {
            //        model.name = device.name;
            //        model.code = device.code;
            //        model.serial_no = device.serial_no;
            //        model.machine_id = device.machine_id;
            //        model.DeviceCategoryId = device.DeviceCategoryId;
            //        model.Device_Type_id = device.Device_Type_id;
            //        model.Device_SubType = device.Device_SubType;
            //        model.is_active = device.Active == "Yes";
            //        model.width = device.width;

            //        // Bind the device category and types
            //        model.DTDeviceCategory = (List<SelectListItem>)GetDeviceCategory(cmp_id, ConnStr);
            //        model.DTDeviceType = (List<SelectListItem>)GetDeviceType(cmp_id, model.DeviceCategoryId);

            //        // Other device-specific data like network types, IP address, etc.
            //        model.network_type = device.network_type;
            //        if (model.network_type == "Serial Port")
            //        {
            //            model.budrate = device.budrate;
            //            model.name = device.name;
            //        }
            //        else if (model.network_type == "USB")
            //        {
            //            model.vender_id = device.vender_id;
            //        }
            //        else if (model.network_type == "LAN")
            //        {
            //            model.ip_address = device.printer_ip_address;
            //            model.port = device.port;
            //        }
            //    }
            //}
            //bool isCashCamera = deviceTypeId == 12;

            //// Pass the isCashCamera variable to the Razor view using ViewData
            //ViewData["IsCashCamera"] = isCashCamera;
            //ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
            string userName = string.Empty;
            string password = string.Empty;
            string bluetoothName = string.Empty;
            string api = string.Empty;
            string serviceKey = string.Empty;
            string networktype = string.Empty;
            string devicesubtype = string.Empty;

            if (deviceTypeId == 10) // EVO
            {
                // Populate the fields with appropriate data (this is just an example)
                userName = "evoUser";
                password = "evoPass";
                bluetoothName = "EVO-BT";
                api = "EVO-AppID";
                serviceKey = "EVO-ServiceKey";
                ViewData["DivEvoVisible"] = true;
            }
            else if (deviceTypeId == 11) // PAY WORKS
            {
                userName = "payworksUser";
                password = "payworksPass";
                bluetoothName = "PayWorks-BT";
                api = "PayWorks-AppID";
                serviceKey = "PayWorks-ServiceKey";
                ViewData["DivEvoVisible"] = true;
            }
            else
            {
                // Hide the EVO fields for other device types
                ViewData["DivEvoVisible"] = false;
            }
            // Pass these values to the View or JavaScript
            ViewData["UserName"] = userName;
            ViewData["Password"] = password;
            ViewData["BluetoothName"] = bluetoothName;
            ViewData["AppProfileId"] = api;
            ViewData["ServiceKey"] = serviceKey;

            MachineList = obj.GetMachine(cmp_id, ConnStr, deviceTillId ?? 0);
            ViewData["Machines"] = machines;
            DTDeviceCategory = obj.GetDeviceCategory(Convert.ToInt32(cmp_id), ConnStr);
            //DTDeviceType = obj.GetDeviceType(Convert.ToInt32(cmp_id), deviceCategoryId.GetValueOrDefault(), ConnStr);
            // Set the selected Device Category from session or query parameter
            SelectedDeviceCategoryId = deviceCategoryId ?? HttpContext.Session.GetInt32("selectedDeviceCategoryId");
            bool isEvoOrPayWorks = false;
            bool isPaxCommunicator = false;
            bool isPrinter = false;
            bool isCashCamera = false;
            //string userName = string.Empty;
            //string password = string.Empty;
            //string bluetoothName = string.Empty;
            //string api = string.Empty;
            //string serviceKey = string.Empty;
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            string port = string.Empty;
            // Populate Device Type based on selected Device Category

            MachineList = obj.GetMachine(Convert.ToInt32(cmp_id), ConnStr, SelectedTillId ?? 0);

            SelectedTillId = deviceTillId ?? HttpContext.Session.GetInt32("selectedTillId");
            DTDeviceCategory = obj.GetDeviceCategory(Convert.ToInt32(cmp_id), ConnStr);

            DeviceList = obj.BindDevice(Convert.ToInt32(cmp_id), ConnStr, deviceTypeId,
        out isEvoOrPayWorks, out isPrinter, out isCashCamera, out isPaxCommunicator, out userName, out password, out bluetoothName, out api, out serviceKey, out ipAddress, out port, out devicesubtype, out networktype);

            if (isEvoOrPayWorks)
            {
                //ViewData["UserName"] = userName;
                //ViewData["Password"] = password;
                //ViewData["BluetoothName"] = bluetoothName;
                //ViewData["API"] = api;
                //ViewData["ServiceKey"] = serviceKey;
                ViewData["DivEvoVisible"] = true;
                ViewData["DeviceSubTypeVisible"] = false;

                TempData["IsEvoOrPayWorks"] = true;
                TempData["UserName"] = userName;
                TempData["Password"] = password;
                TempData["BluetoothName"] = bluetoothName;
                TempData["API"] = api;
                TempData["ServiceKey"] = serviceKey;


            }
            else
            {
                ViewData["DivEvoVisible"] = false;
                ViewData["DeviceSubTypeVisible"] = true;
            }

            //int? selectedCategoryId = deviceCategoryId ?? HttpContext.Session.GetInt32("state_id");
            if (SelectedDeviceCategoryId.HasValue)
            {
                DTDeviceType = obj.GetDeviceType(Convert.ToInt32(cmp_id), SelectedDeviceCategoryId.Value, SelectedTillId ?? 0, ConnStr);
            }
            //DeviceTypeCatList = obj.GetDeviceTypeCat(Convert.ToInt32(cmp_id), Convert.ToInt32(cat_id), ConnStr);
            if (id == null)
            {
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("device_id", HttpContext.Request.Query["ID"].ToString());
                DTDevice = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);



                DTDeviceType = obj.GetDeviceType(Convert.ToInt32(cmp_id), DTDevice.device_category, SelectedTillId ?? 0, ConnStr);
                if (DTDevice == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }

        public List<SelectListItem> GetMachine(int cmp_id, string connStr, int deviceTillId)
        {
            List<SelectListItem> machines = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Machine_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    machines.Add(new SelectListItem
                    {
                        Value = rdr["machine_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return machines;
        }
        public clsDevice GetDeviceById(int value, string device_id, string cmp_id, string connStr)
        {
            clsDevice device = new clsDevice();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@device_id", device_id);
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    device.device_id = Convert.ToInt32(rdr["device_id"]);
                    device.name = rdr["name"].ToString();
                    device.code = rdr["code"].ToString();
                    device.serial_no = rdr["serial_no"].ToString();
                    device.width = Convert.ToInt32(rdr["width"].ToString());
                    device.ip_address = rdr["ip_address"].ToString();
                    device.port = Convert.ToInt32(rdr["port"].ToString());
                    device.printer_ip_address = rdr["printer_ip_address"].ToString();
                    device.machine_name = rdr["machine_name"].ToString();
                    device.machine_id = Convert.ToInt32(rdr["machine_id"].ToString());
                    device.Device_Type_id = Convert.ToInt32(rdr["Device_Type_id"].ToString());
                    device.Device_Type_Name = rdr["Device_Name"].ToString();
                    device.device_category = Convert.ToInt32(rdr["device_category"].ToString());
                    if (rdr["Active"].ToString() == "1")
                    {
                        device.is_active = true;
                    }
                    else
                    {
                        device.is_active = false;
                    }
                }

                con.Close();
            }
            return device;
        }

        [HttpPost("Save")]
        public IActionResult OnPost(int? id, int? deviceCategoryId, int? deviceTypeId, int? deviceTillId, int? SelectedTillId, int? SelectedDeviceCategoryId, int? SelectedDeviceTypeId, string ConnStr, string network_type, string selectedDeviceSubType, string Printer_ipAddress = "123.456.1.10")
        {
            DTDevice.device_id = id ?? 0;
            DTDevice.device_id = Convert.ToInt32(HttpContext.Session.GetString("device_id"));
            DTDevice.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            ViewData["NetworkType"] = network_type;
            DTDevice.Device_SubType = Convert.ToInt32(selectedDeviceSubType);
            //var deviceList = GetDeviceList();
            // var deviceTypes = GetDeviceTypes();
            //foreach (var device in deviceList)
            //{
            //    var deviceType = deviceTypes.FirstOrDefault(dt => dt.deviceTypeId == device.Device_Type_id);
            //    if (deviceType != null)
            //    {
            //        device.Device_Type_Name = deviceType.Device_Type_Name; // Set the device type name for the device
            //    }
            //    else
            //    {
            //        device.Device_Type_Name = "Unknown"; // Default value if not found
            //    }
            //}

            //var deviceType = deviceTypes.FirstOrDefault(dt => dt.deviceTypeId == deviceTypeId);
            //if (deviceType != null)
            //{
            //    DTDevice.Device_Type_Name = deviceType.Device_Type_Name;  // Set the device type name
            //    ViewData["DeviceTypeName"] = deviceType.Device_Type_Name;
            //}
            //else
            //{
            //    DTDevice.Device_Type_Name = "Unknown";  // Default value if not found
            //}

            //string deviceTypeName = DeviceTypeMapping.GetDeviceTypeName(deviceTypeId ?? 0);

            // Set the device type name
            //DTDevice.Device_Type_Name = deviceTypeName;
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            string printer_ip_address = DTDevice.printer_ip_address;
            int port = DTDevice.port;
            bool isEvoOrPayWorks = false;
            bool isPaxCommunicator = false;
            bool isCashCamera = false;
            bool isPrinter = false;

            //if (deviceTypeId == 10) // Assuming 10 is the device type for Evo
            //{
            //    isEvoOrPayWorks = true;
            //}
            string userName = string.Empty;
            string password = string.Empty;
            string bluetoothName = string.Empty;
            string api = string.Empty;
            string serviceKey = string.Empty;
            string IpAddress = string.Empty;// HttpContext.Connection.RemoteIpAddress?.ToString();
            //string port = string.Empty;
            string networktype = string.Empty;
            string devicesubtype = string.Empty;

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (deviceCategoryId.HasValue)
            {
                DTDevice.device_category = deviceCategoryId.Value; // Assign to the device_category property
                ViewData["DeviceCategory"] = deviceCategoryId.Value;
            }

            SelectedDeviceCategoryId = deviceCategoryId ?? HttpContext.Session.GetInt32("selectedDeviceCategoryId");
            SelectedDeviceTypeId = deviceTypeId ?? HttpContext.Session.GetInt32("selectedDeviceTypeId");
            SelectedTillId = deviceTillId ?? HttpContext.Session.GetInt32("selectedTillId");
            if (SelectedDeviceCategoryId.HasValue)
                HttpContext.Session.SetInt32("selectedDeviceCategoryId", SelectedDeviceCategoryId.Value);

            if (SelectedDeviceTypeId.HasValue)
                HttpContext.Session.SetInt32("selectedDeviceTypeId", SelectedDeviceTypeId.Value);

            if (SelectedTillId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedTillId", SelectedTillId.Value);
            }

            // Fetch device types for the selected category
            if (SelectedDeviceCategoryId.HasValue)
            {
                MachineList = obj.GetMachine(Convert.ToInt32(DTDevice.cmp_id), ConnStr, SelectedTillId ?? 0);
                DTDeviceType = obj.GetDeviceType(Convert.ToInt32(DTDevice.cmp_id), SelectedDeviceCategoryId.Value, SelectedTillId ?? 0, ConnStr);

            }
            if (SelectedTillId.HasValue)
            {
                DTDevice.machine_id = SelectedTillId.Value;
                MachineList = obj.GetMachine(Convert.ToInt32(DTDevice.cmp_id), ConnStr, SelectedTillId ?? 0);
                DTDeviceCategory = obj.GetDeviceCategory(Convert.ToInt32(DTDevice.cmp_id), ConnStr);
            }

            ViewData["SelectedDeviceCategoryId"] = SelectedDeviceCategoryId;
            ViewData["SelectedTillId"] = SelectedTillId;
            ViewData["SelectedDeviceTypeId"] = SelectedDeviceTypeId;
            ViewData["IsCashCamera"] = isCashCamera;
            //if (!ModelState.IsValid)
            //{
            //    // If model state is invalid, return the page with error messages
            //    return Page();
            //}
            //string deviceTypeIdStr = deviceTypeId?.ToString() ?? string.Empty;
            if (SelectedDeviceTypeId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedDeviceTypeId", SelectedDeviceTypeId.Value);
                bool isEvo = (SelectedDeviceTypeId.Value == 10 || SelectedDeviceTypeId.Value == 11); // You can adjust this ID based on your actual device type IDs

                if (isEvo)
                {
                    // If the selected device type is EVO, fetch the relevant fields
                    isEvoOrPayWorks = true;
                    int deviceTillIdValue = SelectedTillId ?? 0;  // Default to 0 if deviceTillId is null
                    int selectedDeviceTypeIdValue = SelectedDeviceTypeId ?? 0;
                    // Retrieve EVO-related data for the fields
                    var evoDeviceData = obj.GetEvoDeviceDetails(DTDevice.cmp_id, deviceTillIdValue, SelectedDeviceTypeId.Value, ConnStr, SelectedDeviceCategoryId.Value);

                    if (evoDeviceData != null)
                    {
                        userName = DTDevice.user_name;
                        password = DTDevice.password;
                        bluetoothName = DTDevice.bluetooth_name;
                        api = DTDevice.application_profile_id;
                        serviceKey = DTDevice.service_key;
                    }
                    ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                    TempData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                    ViewData["UserName"] = userName;
                    ViewData["Password"] = password;
                    ViewData["BluetoothName"] = bluetoothName;
                    ViewData["API"] = api;
                    ViewData["ServiceKey"] = serviceKey;
                }
                isPaxCommunicator = SelectedDeviceTypeId.Value == 18;

                if (isPaxCommunicator)
                {
                    // If the selected device type is EVO, fetch the relevant fields
                    isPaxCommunicator = true;
                    int deviceTillIdValue = SelectedTillId ?? 0;  // Default to 0 if deviceTillId is null
                    int selectedDeviceTypeIdValue = SelectedDeviceTypeId ?? 0;
                    // Retrieve EVO-related data for the fields
                    var paxDeviceData = obj.GetEvoDeviceDetails(DTDevice.cmp_id, deviceTillIdValue, SelectedDeviceTypeId.Value, ConnStr, SelectedDeviceCategoryId.Value);

                    if (paxDeviceData != null)
                    {
                        userName = DTDevice.user_name;
                        password = DTDevice.password;
                        Printer_ipAddress = DTDevice.printer_ip_address;
                        port = DTDevice.port;
                    }
                    else
                    {
                        // Handle case where Pax device data is not available
                        Printer_ipAddress = "Default IP"; // Set a default IP or handle error.
                    }
                    ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                    ViewData["IsPaxCommunicator"] = isPaxCommunicator;
                    ViewData["UserName"] = userName;
                    ViewData["Password"] = password;

                    ViewData["IpAddress"] = Printer_ipAddress;
                    ViewData["Port"] = port;
                }
                isPrinter = SelectedDeviceTypeId.Value == 21;
                List<SelectListItem> printernetworkTypeList = new List<SelectListItem>();
                List<SelectListItem> deviceSubType = new List<SelectListItem>();
                if (isPrinter)
                {
                    // If the selected device type is EVO, fetch the relevant fields
                    isPrinter = true;
                    int deviceTillIdValue = SelectedTillId ?? 0;  // Default to 0 if deviceTillId is null
                    int selectedDeviceTypeIdValue = SelectedDeviceTypeId ?? 0;
                    // Retrieve EVO-related data for the fields
                    var printerDeviceData = obj.GetEvoDeviceDetails(DTDevice.cmp_id, deviceTillIdValue, SelectedDeviceTypeId.Value, ConnStr, SelectedDeviceCategoryId.Value);

                    if (printerDeviceData != null)
                    {
                        printernetworkTypeList = new List<SelectListItem>
                        {
                        new SelectListItem { Text = "SELECT", Value = "SELECT" },
                        new SelectListItem { Text = "Serial Port", Value = "Serial Port" },
                        new SelectListItem { Text = "USB", Value = "USB" },
                        new SelectListItem { Text = "LAN", Value = "LAN" },
                        new SelectListItem { Text = "MINIPOS", Value = "MINIPOS" },
                        new SelectListItem { Text = "ADD PAY", Value = "ADD PAY" },
                        new SelectListItem { Text = "SUNMI", Value = "SUNMI" },
                        new SelectListItem { Text = "iMin D1", Value = "iMin D1" },
                        new SelectListItem { Text = "iMin D4", Value = "iMin D4" },
                        new SelectListItem { Text = "Falcon", Value = "Falcon" },
                        };
                        ViewData["NetWorkType"] = printernetworkTypeList.AsEnumerable();
                        ViewData["NetWorkType"] = printernetworkTypeList;
                        ViewData["devicesubtype"] = deviceSubType;
                        ViewData["IsPrinter"] = isPrinter;
                        deviceSubType = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "SELECT", Value = "0" },
                        new SelectListItem { Text = "Star TSP650", Value = "1" },
                        new SelectListItem { Text = "Kinetic Saturn", Value = "2" },
                        new SelectListItem { Text = "Sunmi", Value = "3" },
                        new SelectListItem { Text = "Dualscreen", Value = "4" },
                        new SelectListItem { Text = "iMin D1", Value = "5" },
                        new SelectListItem { Text = "iMin D4", Value = "6" },
                        new SelectListItem { Text = "Falcon", Value = "7" }
                    };
                        ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                        ViewData["IsPaxCommunicator"] = isPaxCommunicator;
                        ViewData["IsCashCamera"] = isCashCamera;
                        ViewData["IsPrinter"] = isPrinter;
                        ViewData["NetWorkType"] = printernetworkTypeList;
                        ViewData["devicesubtype"] = deviceSubType;
                    }
                    ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                    ViewData["IsPaxCommunicator"] = isPaxCommunicator;
                    ViewData["IsPrinter"] = isPrinter;
                    ViewData["NetWorkType"] = printernetworkTypeList;
                    ViewData["devicesubtype"] = deviceSubType;
                }
                isCashCamera = SelectedDeviceTypeId.Value == 12;
                List<SelectListItem> networkTypeList = null;
                if (isCashCamera)
                {
                    // If the selected device type is EVO, fetch the relevant fields
                    isCashCamera = true;
                    int deviceTillIdValue = SelectedTillId ?? 0;  // Default to 0 if deviceTillId is null
                    int selectedDeviceTypeIdValue = SelectedDeviceTypeId ?? 0;
                    // Retrieve EVO-related data for the fields
                    var cashDeviceData = obj.GetEvoDeviceDetails(DTDevice.cmp_id, deviceTillIdValue, SelectedDeviceTypeId.Value, ConnStr, SelectedDeviceCategoryId.Value);

                    if (cashDeviceData != null)
                    {
                        networkTypeList = new List<SelectListItem>
                        {
                        new SelectListItem { Text = "SELECT", Value = "SELECT" },
                        new SelectListItem { Text = "Serial Port", Value = "Serial Port" },
                        new SelectListItem { Text = "USB", Value = "USB" },
                        new SelectListItem { Text = "LAN", Value = "LAN" },
                        new SelectListItem { Text = "MINIPOS", Value = "MINIPOS" },
                        new SelectListItem { Text = "ADD PAY", Value = "ADD PAY" },
                        new SelectListItem { Text = "SUNMI", Value = "SUNMI" },
                        new SelectListItem { Text = "iMin D1", Value = "iMin D1" },
                        new SelectListItem { Text = "iMin D4", Value = "iMin D4" },
                        new SelectListItem { Text = "Falcon", Value = "Falcon" },
                        };

                        ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                        ViewData["IsPaxCommunicator"] = isPaxCommunicator;
                        ViewData["IsCashCamera"] = isCashCamera;
                        ViewData["NetWorkType"] = networkTypeList;

                        HttpContext.Session.SetString("userName", userName);
                        HttpContext.Session.SetString("password", password);
                        HttpContext.Session.SetString("bluetoothName", bluetoothName);
                        HttpContext.Session.SetString("api", api);
                        HttpContext.Session.SetString("serviceKey", serviceKey);
                        HttpContext.Session.SetString("ipAddress", IpAddress);
                        HttpContext.Session.SetString("port", port.ToString());
                    }
                    else
                    {
                        ModelState.AddModelError("cashDeviceData", "Unable to fetch device data.");
                        return Page();
                    }
                    //ViewData["IsEvoOrPayWorks"] = isEvoOrPayWorks;
                    //ViewData["IsPaxCommunicator"] = isPaxCommunicator;
                    //ViewData["IsCashCamera"] = isCashCamera;
                    //ViewData["NetWorkType"] = networkTypeList;

                }
                //if (SelectedDeviceCategoryId == null)
                //{
                //    DTDevice.deviceCategoryId = SelectedDeviceCategoryId.GetValueOrDefault();
                //}
                //if (SelectedDeviceTypeId == null)
                //{
                //    DTDevice.deviceTypeId = SelectedDeviceTypeId.GetValueOrDefault();
                //}
                // Save the values to the session or ViewData for display

                //HttpContext.Session.SetString("userName", userName);
                //HttpContext.Session.SetString("password", password);
                //HttpContext.Session.SetString("bluetoothName", bluetoothName);
                //HttpContext.Session.SetString("api", api);
                //HttpContext.Session.SetString("serviceKey", serviceKey);
                //HttpContext.Session.SetString("ipAddress", ipAddress);
                //HttpContext.Session.SetString("port", port);

                //HttpContext.Session.SetString("networktype", networktype);
                //if (networkTypeList != null)
                //{
                //    var serializedNetworkTypeList = JsonConvert.SerializeObject(networkTypeList);
                //    HttpContext.Session.SetString("networktype", serializedNetworkTypeList);
                //}
            }
            //if (SelectedDeviceCategoryId == null || SelectedDeviceCategoryId == 0)
            //{
            //    // Device Category is required
            //    ModelState.AddModelError("DeviceCategoryId", "Device Category is required.");
            //}

            //if (SelectedDeviceTypeId == null || SelectedDeviceTypeId == 0)
            //{
            //    // Device Type is required
            //    ModelState.AddModelError("DeviceTypeId", "Device Type is required.");
            //}
            //DTDevice.network_type = network_type;
            if (SelectedDeviceCategoryId == null || SelectedDeviceTypeId == null)
            {
                // Redirect to a different page (e.g., an error page or a page with a message)
                return Page(); // You can change this to any appropriate action/page
            }
            if (DTDevice.device_id == 0)
            {
                ModelState.Remove("DTDevice.device_id"); // Remove the required validation error for device_id being 0
            }
            if (DTDevice.port == 0)
            {
                ModelState.Remove("DTDevice.port"); // Remove the required validation error for device_id being 0
            }
            if (!ModelState.IsValid)
            {
                // Log the ModelState errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }

            //DTDevice.deviceCategoryId = deviceCategoryId.GetValueOrDefault();
            //DTDevice.deviceTypeId = deviceTypeId.GetValueOrDefault();
            if (DTDevice.device_id == null || DTDevice.device_id == 0)
            {
                DTDevice.device_id = 0;
                //ViewData["NetworkType"] = network_type;
                var selectedNetworkType = Request.Form["SelectedNetworkType"].ToString();
                string selectedDeviceSubTypeFromForm = Request.Form["SelectedDeviceSubType"].ToString();
                int networkTypeId;
                int deviceSubTypeId;
                // Set these selected values in the DTDevice object if required
                //if (string.IsNullOrEmpty(network_type) || string.IsNullOrEmpty(selectedDeviceSubTypeFromForm))
                //{
                //    // Handle missing values or show an error message
                //    ModelState.AddModelError("NetworkType", "Network Type or Device SubType is not selected.");
                //    return Page(); // Return to the page to show the error
                //}
                if (string.IsNullOrEmpty(selectedNetworkType))
                {
                    selectedNetworkType = ViewData["NetworkType"]?.ToString();
                }
                if (string.IsNullOrEmpty(selectedNetworkType) || string.IsNullOrEmpty(selectedDeviceSubTypeFromForm))
                {
                    // Allow missing values if they are optional
                    // You can set them to null or empty, depending on how your database handles nulls
                    DTDevice.network_type = null;  // Allow null for network_type
                    DTDevice.Device_SubType = null;  // Allow null for device subtype
                }
                else
                {
                    // Try to parse values only if they are not null or empty
                    if (int.TryParse(selectedNetworkType, out networkTypeId))
                    {
                        DTDevice.network_type = networkTypeId.ToString();  // Set the parsed network type ID
                    }
                    else
                    {
                        DTDevice.network_type = network_type;  // Use the string value directly (like "USB", "LAN", etc.)
                        ViewData["Network"] = network_type;
                    }

                    if (int.TryParse(selectedDeviceSubTypeFromForm, out deviceSubTypeId))
                    {
                        DTDevice.Device_SubType = deviceSubTypeId;  // Set the parsed device subtype ID
                    }
                }

                if (int.TryParse(selectedDeviceSubType, out deviceSubTypeId))
                {
                    DTDevice.Device_SubType = deviceSubTypeId;  // Convert int to string if needed
                }
                string selectedDeviceType = Request.Form["SelectedDeviceType"].ToString();
                if (string.IsNullOrEmpty(selectedDeviceType))
                {
                    SelectedDeviceTypeId = deviceTypeId ?? HttpContext.Session.GetInt32("selectedDeviceTypeId");
                    //selectedDeviceType = HttpContext.Session.GetString("selectedDeviceType") ?? string.Empty;
                }
                if (SelectedDeviceTypeId == 10 || SelectedDeviceTypeId == 11)
                {
                    string username = Request.Form["Username"].ToString();
                    string Password = Request.Form["Password"].ToString();
                    string BluetoothName = Request.Form["BluetoothName"].ToString();
                    string applicationProfileId = Request.Form["API"].ToString();
                    string ServiceKey = Request.Form["ServiceKey"].ToString();

                    // Set these values in the DTDevice object
                    DTDevice.user_name = username;
                    DTDevice.password = Password;
                    DTDevice.bluetooth_name = BluetoothName;
                    DTDevice.application_profile_id = applicationProfileId;
                    DTDevice.service_key = ServiceKey;

                    // Optionally validate or set defaults for these values
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(Password))
                    {
                        ModelState.AddModelError("Username", "Username and Password are required for EVO device.");
                        return Page(); // Return to the page with an error message
                    }
                }
                else if (SelectedDeviceTypeId == 18) // Pax Device Type
                {
                    string username = Request.Form["Username"].ToString();
                    string Password = Request.Form["Password"].ToString();
                    string printeripaddress = Request.Form["IpAddress"].ToString();  // Get the IP address from the form
                    string portString = Request.Form["Port"].ToString();
                    // Populate with Pax-specific data
                    DTDevice.user_name = username;
                    DTDevice.password = Password;

                    if (!string.IsNullOrEmpty(printeripaddress))
                    {
                        DTDevice.printer_ip_address = printeripaddress;  // Set the IP address from form
                    }
                    else
                    {
                        DTDevice.printer_ip_address = "192.168.1.100";  // Default IP Address
                    }

                    if (int.TryParse(portString, out var Port))  // Use 'var' here to avoid redeclaring 'port'
                    {
                        DTDevice.port = Convert.ToInt32(portString);  // Set the Port if valid
                    }
                    else
                    {
                        DTDevice.port = 8080;  // Default Port
                    }
                    //DTDevice.user_name = "PaxUser";
                    //DTDevice.password = "PaxPass";
                    //DTDevice.ip_address = "192.168.1.100";  // Example IP Address
                    //DTDevice.port = 8080;  // Example Port
                    //DTDevice.bluetooth_name = "PaxBluetooth";
                    //DTDevice.application_profile_id = "PaxAppProfile";
                    //DTDevice.service_key = "PaxServiceKey";

                    if (string.IsNullOrEmpty(DTDevice.user_name) || string.IsNullOrEmpty(DTDevice.password))
                    {
                        ModelState.AddModelError("Username", "Username and Password are required for EVO device.");
                        return Page(); // Return to the page with an error message
                    }
                    // Optionally, you can add validation or set defaults specific to PAX device here
                }
                else if (SelectedDeviceTypeId == 12) // Network type for Device Type 12
                {

                    // Handle logic for Device Type 12
                    string networkTypeFromForm = Request.Form["NetworkType"];  // Changed variable name

                    // Rename the variable to avoid conflict with outer 'networkTypeId'
                    if (!string.IsNullOrEmpty(networkTypeFromForm))
                    {
                        if (int.TryParse(networkTypeFromForm, out int parsedNetworkTypeId))
                        {
                            // Set the network type in the DTDevice object
                            DTDevice.network_type = parsedNetworkTypeId.ToString();  // Assuming network_type is a string
                        }
                        else
                        {
                            // Default network type if not valid
                            DTDevice.network_type = networkTypeFromForm;  // Default network type for Device Type 12
                        }

                        // Validate if username is empty for this device type
                        if (string.IsNullOrEmpty(DTDevice.network_type))
                        {
                            ModelState.AddModelError("Username", "Username and Password are required for the Network Type device.");
                            return Page(); // Return to the page with an error message
                        }
                    }
                    else
                    {
                        // Handle case where network type is not selected
                        ModelState.AddModelError("NetworkType", "Network Type must be selected for Device Type 12.");
                        return Page(); // Return to the page with an error message
                    }

                }
                else if (SelectedDeviceTypeId == 21) // Network type for Device Type 12
                {
                    //ViewData["NetworkType"] = network_type;
                    string deviceCategoryFromForm = Request.Form["DeviceCategory"];
                    string deviceTypeFromForm = Request.Form["DeviceType"];
                    string networkTypeFromForm = Request.Form["Network"];
                    ViewData["Network"] = network_type;
                    string networkTypeFromViewData = ViewData["Network"]?.ToString();

                    if (!string.IsNullOrEmpty(networkTypeFromViewData))
                    {
                        // Try parsing the NetworkType if it's a valid integer (e.g., 1 for USB, 2 for LAN, etc.)
                        if (int.TryParse(networkTypeFromViewData, out int parsedNetworkTypeId))
                        {
                            DTDevice.network_type = parsedNetworkTypeId.ToString();  // Store the network type as a string
                        }
                        else
                        {
                            // If not an integer, use the string value (like "USB", "LAN")
                            DTDevice.network_type = networkTypeFromViewData;
                        }

                        // Validate if the network type was set correctly
                        if (string.IsNullOrEmpty(DTDevice.network_type))
                        {
                            ModelState.AddModelError("NetworkType", "Network Type must be selected for Device Type 21.");
                            return Page(); // Return to the page with an error message
                        }
                    }
                    else
                    {
                        // Handle case where network type is not selected
                        ModelState.AddModelError("NetworkType", "Network Type must be selected for Device Type 12.");
                        return Page(); // Return to the page with an error message
                    }
                }
                else
                {

                }

                int selectedDeviceTypeId = SelectedDeviceTypeId ?? 0;
                int selectedDeviceCategoryId = SelectedDeviceCategoryId ?? 0;
                obj.Insert(DTDevice, ConnStr, ip_address, DTDevice.printer_ip_address, DTDevice.network_type, selectedDeviceTypeId, selectedDeviceCategoryId);
            }
            else
            {
                int selectedDeviceTypeId = SelectedDeviceTypeId ?? 0;
                int selectedDeviceCategoryId = SelectedDeviceCategoryId ?? 0;
                obj.Update(DTDevice, ConnStr, ip_address, Printer_ipAddress, DTDevice.network_type, selectedDeviceTypeId, selectedDeviceCategoryId);
            }

            HttpContext.Session.Remove("device_id");
            return RedirectToPage("/Master_Setting/Device_list");
        }



        private List<clsDevice> GetDeviceTypes()
        {
            return new List<clsDevice>
    {
        new clsDevice { deviceTypeId = 1, Device_Type_Name = "Scanner" },
        new clsDevice { deviceTypeId = 21, Device_Type_Name = "Printer" },
        new clsDevice { deviceTypeId = 3, Device_Type_Name = "Cash Register" },
        // Add other device types here
    };
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("device_id");
            return RedirectToPage("/Master_Setting/Device_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("device_id");
            return RedirectToPage("/Master_Setting/Device_list");
        }

        private static class DeviceTypeMapping
        {
            public static readonly Dictionary<int, string> DeviceTypeDictionary = new Dictionary<int, string>
    {
        { 1, "Scanner" },
        { 21, "Printer" },
        { 3, "Cash Register" },
        // Add other device types here as needed
    };

            public static string GetDeviceTypeName(int deviceTypeId)
            {
                return DeviceTypeDictionary.ContainsKey(deviceTypeId) ? DeviceTypeDictionary[deviceTypeId] : "Unknown";
            }
        }
    }
}
