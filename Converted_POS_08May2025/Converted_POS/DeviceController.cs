using Converted_POS.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class DeviceController : Controller
    {
        public IEnumerable<clsDevice> SelectAll(int? c_id, string conn)
        {
            List<clsDevice> list = new List<clsDevice>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsDevice device = new clsDevice();

                    device.device_id = Convert.ToInt32(rdr["device_id"]);
                    device.code = rdr["code"].ToString();
                    device.name = rdr["name"].ToString();
                    device.machine_name = rdr["Machine_Name"].ToString();
                    device.serial_no = rdr["serial_no"].ToString();
                    device.Device_Type_Name = rdr["Device_Type"].ToString();
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        device.is_active = true;
                    }
                    else
                    {
                        device.is_active = false;
                    }
                    //device.Name = rdr["name"].ToString();
                    //device.courses_category_id = Convert.ToInt32(rdr["course_category_id"]);
                    //device.courses_category_name = rdr["cname"].ToString();
                    //device.Value = Convert.ToInt32(rdr["value"]);
                    list.Add(device);
                }
                con.Close();
            }
            return list;
        }

        public List<SelectListItem> BindDevice(int cmp_id, string ConnStr, int deviceTypeId, out bool isEvoOrPayWorks, out bool isPrinter, out bool isCashCamera, out bool isPaxCommunicator, out string userName, out string password, out string bluetoothName, out string api, out string serviceKey, out string ipAddress, out string port, out string devicesubtype, out string networktype)
        {
            var deviceData = new List<SelectListItem>();

            // Initialize output parameters
            isEvoOrPayWorks = false;
            isPaxCommunicator = false;
            isPrinter = false;
            isCashCamera = false;
            userName = string.Empty;
            password = string.Empty;
            bluetoothName = string.Empty;
            api = string.Empty;
            serviceKey = string.Empty;
            ipAddress = string.Empty;
            port = string.Empty;
            devicesubtype = string.Empty;
            networktype = string.Empty;
            try
            {
                var dtDevice = SelectDeviceData(cmp_id, deviceTypeId, ConnStr) as List<clsDevice>;

                if (dtDevice != null && dtDevice.Count > 0)
                {
                    // Loop through the device list and add items to the SelectListItem list
                    foreach (var device in dtDevice)
                    {
                        deviceData.Add(new SelectListItem
                        {
                            Value = device.device_id.ToString(),
                            Text = device.device_name
                        });

                        // Check if the device type is "EVO" or "PAY WORKS"
                        if (device.device_name.ToUpper() == "EVO" || device.device_name.ToUpper() == "PAY WORKS")
                        {
                            // Set the flag indicating that we have EVO or PAY WORKS
                            isEvoOrPayWorks = true;

                            // Set the specific fields (UserName, Password, BluetoothName, etc.)
                            userName = device.user_name;
                            password = device.password;
                            bluetoothName = device.bluetooth_name;
                            api = device.application_profile_id;
                            serviceKey = device.service_key;

                            ViewData["UserName"] = userName;
                            ViewData["Password"] = password;
                            ViewData["BluetoothName"] = bluetoothName;
                            ViewData["API"] = api;
                            ViewData["ServiceKey"] = serviceKey;
                            ViewData["DivEvoVisible"] = true;
                        }
                        else if (device.device_name.ToUpper() == "PRINTER")
                        {
                            isPrinter = true;

                            // Set the specific fields (UserName, Password, BluetoothName, etc.)
                            devicesubtype = device.Device_SubType.ToString();
                            networktype = device.network_type;


                            ViewData["DeviceSubType"] = devicesubtype;
                            ViewData["NetWorkType"] = networktype;

                            ViewData["DivEvoVisible"] = true;
                        }
                        else if (device.device_name.ToUpper() == "CASH CAMERA")
                        {
                            isCashCamera = true;

                            // Set the specific fields (UserName, Password, BluetoothName, etc.)

                            networktype = device.network_type;
                            ViewData["NetWorkType"] = networktype;
                        }
                        else if (device.device_name.ToUpper() == "PAX COMMUNICATOR")
                        {
                            isPaxCommunicator = true;
                            userName = device.user_name;
                            password = device.password;
                            ipAddress = device.ip_address;
                            port = device.port.ToString(); // Ensure port is an integer, convert it to string if necessary
                        }

                        // For Kinetic or similar devices, skip binding user-related fields
                        else if (device.device_name.ToUpper() == "KINETIC SATURN" || device.device_name.ToUpper() == "KINETIC")
                        {
                            continue;  // Skip to next device
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
            }

            return deviceData;
        }

        public List<clsDevice> SelectDeviceData(int cmp_id, int deviceTypeId, string connStr)
        {
            try
            {
                // Example: database logic to fetch device data
                List<clsDevice> devices = new List<clsDevice>();

                // Assume you are querying a database or API, so ensure the connection and query are working fine.
                // Example: return a list of devices based on the cmp_id, deviceTypeId, and ConnStr

                // Add dummy devices for testing
                devices.Add(new clsDevice
                {
                    device_id = 1,
                    device_name = "EVO",
                    user_name = "admin",
                    password = "password123",
                    bluetooth_name = "EVO-BT",
                    application_profile_id = "APID-1234",
                    service_key = "SVC-1234"
                });

                devices.Add(new clsDevice
                {
                    device_id = 2,
                    device_name = "PAX COMMUNICATOR",
                    user_name = "admin_pax",
                    password = "password1234",
                    ip_address = "192.168.1.100",
                    port = 8080
                });

                return devices; // Return the list of devices
            }
            catch (Exception ex)
            {
                // Log or handle exceptions in SelectDeviceData
                Console.WriteLine("Error in SelectDeviceData: " + ex.Message);
                return null;  // Or throw; to propagate the exception
            }
        }

        public clsDevice Select(int cmp_id, string deviceId, string ConnStr)
        {
            clsDevice device = new clsDevice();
            // Set device properties here after fetching from the database
            return device;
        }
        public void BindDeviceType(int cmpId, int catId, int deviceCategory)
        {
            try
            {
                var deviceTypes = SelectDeviceType(cmpId, catId);

                var deviceTypeList = deviceTypes.AsEnumerable()
                    .Select(row => new SelectListItem
                    {
                        Value = row["Device_Type_id"].ToString(),
                        Text = row["Device_Type"].ToString()
                    }).ToList();

                // Add "SELECT" option
                deviceTypeList.Insert(0, new SelectListItem { Text = "SELECT", Value = "0" });

                ViewData["DeviceTypeList"] = deviceTypeList;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error in BindDeviceType");
            }
        }

        public DataTable SelectDeviceType(int cmpId, object catId)
        {
            string connectionString = HttpContext.Session.GetString("conString");

            // Ensure that connection string is valid
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not available.");
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create dynamic parameters for the query
                var parameters = new DynamicParameters();
                parameters.Add("@cmp_id", cmpId);

                // Check if catId is not null and add it to parameters
                if (catId != null)
                {
                    parameters.Add("@cat_id", catId);
                }

                // Execute the query to get the data (stored procedure)
                var result = connection.Query("Get_M_Device_Type", parameters, commandType: CommandType.StoredProcedure);

                // Convert the result to DataTable (if needed, implement this method)
                return ToDataTable(result);
            }
        }

        private DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            var table = new DataTable();

            // Use reflection to get the properties of the first item
            var firstItem = items.FirstOrDefault();
            if (firstItem != null)
            {
                var properties = firstItem.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                // Add rows to DataTable
                foreach (var item in items)
                {
                    var row = table.NewRow();
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }


        public void Delete(clsDevice dTDevice, string connStr)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", dTDevice.cmp_id);
                cmd.Parameters.AddWithValue("@device_id ", dTDevice.device_id);
                cmd.Parameters.AddWithValue("@name", dTDevice.name);
                cmd.Parameters.AddWithValue("@code", dTDevice.code);
                cmd.Parameters.AddWithValue("@serial_no", dTDevice.serial_no);
                cmd.Parameters.AddWithValue("@machine_id", dTDevice.machine_id);
                cmd.Parameters.AddWithValue("@Device_Type_id", 0);
                cmd.Parameters.AddWithValue("@printer_ip_address", dTDevice.printer_ip_address);
                cmd.Parameters.AddWithValue("@ip_address", 106);
                cmd.Parameters.AddWithValue("@login_id", dTDevice.login_id);
                cmd.Parameters.AddWithValue("@network_type", "LAN");
                cmd.Parameters.AddWithValue("@budrate", "19200");
                cmd.Parameters.AddWithValue("@device_name", "Device Name");
                cmd.Parameters.AddWithValue("@user_name", "User Name");
                cmd.Parameters.AddWithValue("@password", "password");
                cmd.Parameters.AddWithValue("@bluetooth_name", "bluetooth_name");
                cmd.Parameters.AddWithValue("@service_key", "service_key");
                cmd.Parameters.AddWithValue("@application_profile_id", 0);
                cmd.Parameters.AddWithValue("@is_active", dTDevice.is_active);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public EvoDeviceDetails GetEvoDeviceDetails(int cmp_id, int deviceTillId, int deviceTypeId, string ConnStr, int deviceCategoryId)
        {
            var deviceDetails = new EvoDeviceDetails();
            if (deviceTypeId == 10 || deviceTypeId == 11) // Evo device types
            {

                // Populate with Evo-specific data
                deviceDetails.UserName = "EvoUser";
                deviceDetails.Password = "EvoPass";
                deviceDetails.BluetoothName = "EvoBT";
                deviceDetails.ApplicationProfileId = "EvoAppID";
                deviceDetails.ServiceKey = "EvoServiceKey";
            }
            else if (deviceTypeId == 18) // Pax device type
            {
                // Populate with Pax-specific data
                deviceDetails.UserName = "PaxUser";
                deviceDetails.Password = "PaxPass";
                deviceDetails.IpAddress = "192.168.1.100"; // Example IP Address
                deviceDetails.Port = "8080"; // Example Port

            }
            else if (deviceTypeId == 12) // Pax device type
            {
                var networktype = new List<SelectListItem>
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
        new SelectListItem { Text = "Falcon", Value = "Falcon" }
    };

                // Store this list in ViewData
                ViewData["NetWorkType"] = networktype;

                // Optionally, you can also store other relevant data in ViewData
                ViewData["IsCashCamera"] = true;

            }
            else if (deviceTypeId == 21) // Pax device type
            {
                var networktype = new List<SelectListItem>
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
        new SelectListItem { Text = "Falcon", Value = "Falcon" }
    };
                var deviceSubType = new List<SelectListItem>
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
                // Store this list in ViewData
                ViewData["NetWorkType"] = networktype;
                ViewData["DeviceSubType"] = deviceSubType;
                ViewData["IsPrinter"] = true;
                // Optionally, you can also store other relevant data in ViewData
                //ViewData["IsCashCamera"] = true;

            }
            else
            {
                // Handle other device types, if needed
                deviceDetails.UserName = "DefaultUser";
                deviceDetails.Password = "DefaultPass";
                deviceDetails.BluetoothName = "DefaultBT";
                deviceDetails.ApplicationProfileId = "DefaultAppID";
                deviceDetails.ServiceKey = "DefaultServiceKey";
            }

            // Fetch the data based on the cmpId, deviceTypeId, and connStr from the database or other source
            // Return the populated EvoDeviceDetails object

            return deviceDetails;
        }
        private static readonly Dictionary<int, string> DeviceSubTypeNames = new Dictionary<int, string>
{
    { 1, "Star TSP650" },
    { 2, "Kinetic Saturn" },
    { 3, "Sunmi" },
    { 4, "Dualscreen" },
    { 5, "iMin D1" },
    { 6, "iMin D4" },
    { 7, "Falcon" }
};
        public clsDevice Select(int? c_id, int? id, string connStr)
        {
            clsDevice device = new clsDevice();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@device_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    device.device_id = Convert.ToInt32(rdr["device_id"]);
                    device.name = rdr["name"].ToString();
                    device.code = rdr["code"].ToString();
                    device.serial_no = rdr["serial_no"].ToString();
                    device.width = Convert.ToInt32(rdr["width"].ToString());
                    device.user_name = rdr["user_name"].ToString();
                    device.password = rdr["password"].ToString();
                    device.bluetooth_name = rdr["bluetooth_name"].ToString();
                    device.application_profile_id = rdr["application_profile_id"].ToString();
                    device.service_key = rdr["service_key"].ToString();
                    device.ip_address = rdr["ip_address"].ToString();
                    device.port = Convert.ToInt32(rdr["port"].ToString());
                    device.printer_ip_address = rdr["printer_ip_address"].ToString();
                    device.machine_name = rdr["machine_name"].ToString();
                    device.machine_id = Convert.ToInt32(rdr["machine_id"]);
                    device.Device_Type_id = Convert.ToInt32(rdr["Device_Type_id"]);
                    device.SelectedDeviceTypeId = device.Device_Type_id;
                    if (device.Device_Type_id == 10 || device.Device_Type_id == 11)
                    {
                        ViewData["IsEvoOrPayWorks"] = true;
                        ViewData["UserName"] = device.user_name;
                        ViewData["Password"] = device.password;
                        ViewData["BluetoothName"] = device.bluetooth_name;
                        ViewData["API"] = device.application_profile_id;
                        ViewData["ServiceKey"] = device.service_key;
                    }
                    else
                    {
                        ViewData["IsEvoOrPayWorks"] = false;
                    }

                    //if (device.Device_Type_id == 21)
                    //{
                    //    // Fetch username and password from the database (or other source)
                    //    device.user_name = rdr["user_name"].ToString(); // Assuming 'Username' is a column in your database
                    //    device.password = rdr["password"].ToString(); // Assuming 'Password' is a column in your database
                    //    device.bluetooth_name = rdr["bluetooth_name"].ToString();
                    //    device.application_profile_id = rdr["application_profile_id"].ToString();
                    //    device.service_key = rdr["service_key"].ToString();


                    //}
                    device.Device_Type_Name = rdr["Device_Type"].ToString();
                    device.Device_SubType = Convert.ToInt32(rdr["Device_SubType"].ToString());
                    device.Device_SubTypeName = DeviceSubTypeNames.ContainsKey(device.Device_SubType.GetValueOrDefault())
    ? DeviceSubTypeNames[device.Device_SubType.GetValueOrDefault()]
    : "Unknown";
                    //device.Device_SubTypeName = rdr["Device_SubType"].ToString();
                    device.network_type = rdr["network_type"].ToString();
                    device.device_category = Convert.ToInt32(rdr["device_category"]);
                    if (device.Device_Type_id == 21)
                    {
                        ViewData["IsPrinter"] = true;
                        ViewData["DeviceSubType"] = device.Device_SubType;
                        ViewData["NetWorkType"] = device.network_type;

                    }
                    else
                    {
                        ViewData["IsPrinter"] = false;
                    }
                    if (device.Device_Type_id == 18)
                    {
                        ViewData["IsPaxCommunicator"] = true;
                        ViewData["UserName"] = device.user_name;
                        ViewData["Password"] = device.password;
                        ViewData["IpAddress"] = device.printer_ip_address;
                        ViewData["Port"] = device.port;

                    }
                    else
                    {
                        ViewData["IsPaxCommunicator"] = false;
                    }
                    if (device.Device_Type_id == 12)
                    {
                        ViewData["IsCashCamera"] = true;

                        ViewData["NetWorkType"] = device.network_type;

                    }
                    else
                    {
                        ViewData["IsCashCamera"] = false;
                    }
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        device.is_active = true;
                    }
                    else
                    {
                        device.is_active = false;
                    }
                    ViewData["Device_Type_Name"] = device.Device_Type_Name;
                    ViewData["Device_Type_id"] = device.Device_Type_id;
                    ViewData["Device_Category"] = device.device_category;
                    ViewData["IsEvoOrPayWorks"] = device.Device_Type_id == 10 || device.Device_Type_id == 11;
                    ViewData["IsPrinter"] = device.Device_Type_id == 21;
                    ViewData["IsPaxCommunicator"] = device.Device_Type_id == 18;
                    ViewData["IsCashCamera"] = device.Device_Type_id == 12;
                }
                con.Close();
            }
            ViewData["SelectedDeviceSubType"] = device.Device_SubType;
            ViewData["SelectedNetworkType"] = device.network_type;

            ViewData["devicesubtype"] = new List<SelectListItem>
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

            // Similarly, for network types
            ViewData["NetWorkType"] = new List<SelectListItem>
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
                        new SelectListItem { Text = "Falcon", Value = "Falcon" }
    };
            return device;
        }

        public string GetDeviceSubTypeName(int? device_SubType, string connStr)
        {
            string subTypeName = string.Empty;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT Device_SubType_Name FROM M_Device WHERE Device_SubType_id = @Device_SubType_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Device_SubType_id", device_SubType);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    subTypeName = rdr["Device_SubType_Name"].ToString();
                }
                con.Close();
            }

            return subTypeName;
        }

        public void Update(clsDevice dTDevice, string connStr, string ip_address, string Printer_ipAddress, string network_type, int SelectedDeviceTypeId, int SelectedDeviceCategoryId)
        {
            if (!string.IsNullOrEmpty(Printer_ipAddress) && !IsValidIpAddress(Printer_ipAddress))
            {
                // If invalid IP address, throw exception
                throw new Exception("Invalid IP address format.");
            }
            if (string.IsNullOrEmpty(Printer_ipAddress))
            {
                dTDevice.printer_ip_address = null;  // or set a default value if necessary
            }
            else
            {
                dTDevice.printer_ip_address = Printer_ipAddress.Trim();  // Validate if provided and trim it
            }

            // Proceed with the rest of the device information
            dTDevice.ip_address = ip_address;
            dTDevice.network_type = network_type;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", dTDevice.cmp_id);
                cmd.Parameters.AddWithValue("@device_id ", dTDevice.device_id);
                cmd.Parameters.AddWithValue("@name", dTDevice.name);
                cmd.Parameters.AddWithValue("@code", dTDevice.code);
                cmd.Parameters.AddWithValue("@serial_no", dTDevice.serial_no);
                cmd.Parameters.AddWithValue("@width", dTDevice.width);
                cmd.Parameters.AddWithValue("@machine_id", dTDevice.machine_id);
                cmd.Parameters.AddWithValue("@Device_Type_id", SelectedDeviceTypeId);
                cmd.Parameters.AddWithValue("@printer_ip_address", dTDevice.printer_ip_address);
                cmd.Parameters.AddWithValue("@port", dTDevice.port);
                cmd.Parameters.AddWithValue("@vender_id", dTDevice.vender_id);
                cmd.Parameters.AddWithValue("@ip_address", dTDevice.ip_address);
                cmd.Parameters.AddWithValue("@login_id", dTDevice.login_id);
                if (string.IsNullOrEmpty(network_type))
                {
                    cmd.Parameters.AddWithValue("@network_type", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@network_type", network_type); // Pass the network_type if it is provided
                }

                //cmd.Parameters.AddWithValue("@network_type", network_type); /*network_type*/
                cmd.Parameters.AddWithValue("@budrate", "19200");
                cmd.Parameters.AddWithValue("@device_name", "Device Name");
                //cmd.Parameters.AddWithValue("@user_name", "User Name");
                if (string.IsNullOrEmpty(dTDevice.user_name))
                {
                    cmd.Parameters.AddWithValue("@user_name", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@user_name", dTDevice.user_name); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@password", "password");
                if (string.IsNullOrEmpty(dTDevice.password))
                {
                    cmd.Parameters.AddWithValue("@password", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@password", dTDevice.password); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@bluetooth_name", "bluetooth_name");
                if (string.IsNullOrEmpty(dTDevice.bluetooth_name))
                {
                    cmd.Parameters.AddWithValue("@bluetooth_name", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bluetooth_name", dTDevice.bluetooth_name); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@service_key", "service_key");
                if (string.IsNullOrEmpty(dTDevice.service_key))
                {
                    cmd.Parameters.AddWithValue("@service_key", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@service_key", dTDevice.service_key); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@application_profile_id", 0);
                if (string.IsNullOrEmpty(dTDevice.application_profile_id))
                {
                    cmd.Parameters.AddWithValue("@application_profile_id", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@application_profile_id", dTDevice.application_profile_id); // Pass the network_type if it is provided
                }
                cmd.Parameters.AddWithValue("@is_active", dTDevice.is_active);
                cmd.Parameters.AddWithValue("@Device_SubType", dTDevice.Device_SubType);
                cmd.Parameters.AddWithValue("@cat_id", SelectedDeviceCategoryId);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Insert(clsDevice dTDevice, string connStr, string ip_address, string printer_ip_address, string network_type, int SelectedDeviceTypeId, int SelectedDeviceCategoryId)
        {
            clsDevice DTDevice = new clsDevice();
            if (!string.IsNullOrEmpty(DTDevice.printer_ip_address) && !IsValidIpAddress(DTDevice.printer_ip_address))
            {
                // If invalid IP address, throw exception
                throw new Exception("Invalid IP address format.");
            }
            if (string.IsNullOrEmpty(DTDevice.printer_ip_address))
            {
                dTDevice.printer_ip_address = null;  // or set a default value if necessary
            }
            else
            {
                dTDevice.printer_ip_address = DTDevice.printer_ip_address.Trim();  // Validate if provided and trim it
            }

            // Proceed with the rest of the device information
            dTDevice.ip_address = ip_address;

            dTDevice.network_type = network_type;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", dTDevice.cmp_id);
                cmd.Parameters.AddWithValue("@device_id ", dTDevice.device_id);
                cmd.Parameters.AddWithValue("@name", dTDevice.name);
                cmd.Parameters.AddWithValue("@code", dTDevice.code);
                cmd.Parameters.AddWithValue("@serial_no", dTDevice.serial_no);
                cmd.Parameters.AddWithValue("@width", dTDevice.width);
                cmd.Parameters.AddWithValue("@machine_id", dTDevice.machine_id);
                cmd.Parameters.AddWithValue("@Device_Type_id", SelectedDeviceTypeId);
                cmd.Parameters.AddWithValue("@printer_ip_address", printer_ip_address);
                cmd.Parameters.AddWithValue("@port", dTDevice.port);
                cmd.Parameters.AddWithValue("@vender_id", dTDevice.vender_id);
                cmd.Parameters.AddWithValue("@ip_address", dTDevice.ip_address);
                cmd.Parameters.AddWithValue("@login_id", dTDevice.login_id);
                if (string.IsNullOrEmpty(network_type))
                {
                    cmd.Parameters.AddWithValue("@network_type", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@network_type", network_type); // Pass the network_type if it is provided
                }

                //cmd.Parameters.AddWithValue("@network_type", network_type); /*network_type*/
                cmd.Parameters.AddWithValue("@budrate", "19200");
                cmd.Parameters.AddWithValue("@device_name", "Device Name");
                //cmd.Parameters.AddWithValue("@user_name", "User Name");
                if (string.IsNullOrEmpty(dTDevice.user_name))
                {
                    cmd.Parameters.AddWithValue("@user_name", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@user_name", dTDevice.user_name); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@password", "password");
                if (string.IsNullOrEmpty(dTDevice.password))
                {
                    cmd.Parameters.AddWithValue("@password", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@password", dTDevice.password); // Pass the network_type if it is provided
                }
                //if (string.IsNullOrEmpty(dTDevice.printer_ip_address))
                //{
                //    cmd.Parameters.AddWithValue("@printer_ip_address", DBNull.Value); // Pass NULL if network_type is empty or null
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@printer_ip_address", dTDevice.printer_ip_address); // Pass the network_type if it is provided
                //}
                //if (string.IsNullOrEmpty(dTDevice.port.ToString()))
                //{
                //    cmd.Parameters.AddWithValue("@port", DBNull.Value); // Pass NULL if network_type is empty or null
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@port", dTDevice.port); // Pass the network_type if it is provided
                //}
                //cmd.Parameters.AddWithValue("@bluetooth_name", "bluetooth_name");
                if (string.IsNullOrEmpty(dTDevice.bluetooth_name))
                {
                    cmd.Parameters.AddWithValue("@bluetooth_name", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bluetooth_name", dTDevice.bluetooth_name); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@service_key", "service_key");
                if (string.IsNullOrEmpty(dTDevice.service_key))
                {
                    cmd.Parameters.AddWithValue("@service_key", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@service_key", dTDevice.service_key); // Pass the network_type if it is provided
                }
                //cmd.Parameters.AddWithValue("@application_profile_id", 0);
                if (string.IsNullOrEmpty(dTDevice.application_profile_id))
                {
                    cmd.Parameters.AddWithValue("@application_profile_id", DBNull.Value); // Pass NULL if network_type is empty or null
                }
                else
                {
                    cmd.Parameters.AddWithValue("@application_profile_id", dTDevice.application_profile_id); // Pass the network_type if it is provided
                }
                cmd.Parameters.AddWithValue("@is_active", dTDevice.is_active);
                cmd.Parameters.AddWithValue("@Device_SubType", dTDevice.Device_SubType);
                cmd.Parameters.AddWithValue("@cat_id", SelectedDeviceCategoryId);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private bool IsValidIpAddress(string printer_ipAddress)
        {
            if (string.IsNullOrEmpty(printer_ipAddress))
            {
                return false;
            }
            var regex = @"^(\d{1,3}\.){3}\d{1,3}$";

            // Check if the string matches the IP address pattern
            return Regex.IsMatch(printer_ipAddress, regex);
        }

        public List<SelectListItem> GetDeviceTypeCat(int cmp_id, int cat_id, string connStr)
        {
            List<SelectListItem> devicetypecat = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device_Type_Cat", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@cat_id", cat_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    devicetypecat.Add(new SelectListItem
                    {
                        Value = rdr["Device_Type_id"].ToString(),
                        Text = rdr["Device_Type"].ToString()
                    });
                }

                con.Close();
            }
            return devicetypecat;
        }
        public List<SelectListItem> GetDeviceType(int cmp_id, int device_category, int deviceTillId, string connStr)
        {
            List<SelectListItem> deviceTypes = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device_Type_Cat", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                cmd.Parameters.AddWithValue("@cat_id", device_category);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    deviceTypes.Add(new SelectListItem
                    {
                        Value = rdr["Device_Type_id"].ToString(),
                        Text = rdr["Device_Type"].ToString()
                    });

                }

                con.Close();
            }
            deviceTypes = deviceTypes.GroupBy(x => x.Text)
                 .Select(group => group.First())
                 .ToList();

            if (deviceTillId != 0)
            {
                // Call GetMachine if deviceTillId is available and pass the necessary parameters
                var machines = GetMachine(cmp_id, connStr, deviceTillId);
                //ViewData["Machines"] = machines;
                // Optional: Process or store the machines list as needed
                // Example: You could add machines to the ViewData or return them as needed
                // ViewData["Machines"] = machines;
                var model = new clsDevice
                {
                    MachineList = machines,  // Use Devices if it fits
                    SelectedTillId = deviceTillId // Pass selected device till id if needed
                };
            }
            //GetDeviceTypeCat( cmp_id, cat_id, connStr);
            return deviceTypes;
        }

        public List<SelectListItem> GetDeviceCategory(int cmp_id, string connStr)
        {
            List<SelectListItem> machine = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Device_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    machine.Add(new SelectListItem
                    {
                        Value = rdr["device_category_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }
                con.Close();
            }
            return machine;
        }

        public List<SelectListItem> GetMachine(int cmp_id, string connStr, int deviceTillId)
        {
            List<SelectListItem> machine = new List<SelectListItem>();

            string selectedMachineName = string.Empty;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Machine_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int currentMachineId = Convert.ToInt32(rdr["machine_id"]);
                    string machineName = rdr["name"].ToString();

                    // Check if this machine is the selected one (matching deviceTillId)
                    if (currentMachineId == deviceTillId)
                    {
                        selectedMachineName = machineName; // Store the name of the selected machine
                        ViewData["SelectedDeviceTillIdName"] = selectedMachineName; // Store selected machine's name in ViewData
                    }

                    machine.Add(new SelectListItem
                    {
                        Value = currentMachineId.ToString(),
                        Text = machineName,
                        Selected = (currentMachineId == deviceTillId) // Mark this item as selected if it matches the deviceTillId
                    });
                }

                con.Close();
            }
            return machine;
        }
    }
}
