using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class MachineController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");
        public IEnumerable<clsMachine> SelectAll(int? c_id, string conn)
        {
            List<clsMachine> list = new List<clsMachine>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsMachine machine = new clsMachine();

                    machine.machine_id = Convert.ToInt32(rdr["machine_id"]);
                    machine.name = rdr["name"].ToString();
                    machine.location_name = rdr["location"].ToString();
                    machine.isassign = rdr["is_assign"].ToString();
                    if (rdr["is_assign"].ToString() == "Yes")
                    {
                        machine.is_assign = true;
                    }
                    else
                    {
                        // Handle invalid value or assign a default value (e.g., false)
                        machine.is_assign = false; // Or handle as per your business logic
                    }
                    machine.isactive = rdr["Active"].ToString();
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        machine.is_active = true;
                    }
                    else
                    {
                        machine.is_active = false;
                    }
                    list.Add(machine);
                }
                con.Close();
            }
            return list;
        }

        public clsMachine Select(int? c_id, int? id, string connStr, int function_id, int machine_id)
        {

            clsMachine machine = new clsMachine();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@machine_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    machine.machine_id = Convert.ToInt32(rdr["machine_id"]);
                    machine.name = rdr["name"].ToString();
                    machine.serial_no = rdr["serial_no"].ToString();
                    machine.mac_address = rdr["mac_address"].ToString();
                    machine.model = rdr["model"].ToString();
                    machine.code = rdr["code"].ToString();
                    machine.ip_address = rdr["tillip_address"].ToString();
                    machine.location_id = Convert.ToInt32(rdr["location_id"].ToString());
                    machine.master_till = Convert.ToInt32(rdr["master_till"].ToString());
                    machine.hardware_type = Convert.ToInt32(rdr["hardware_type"].ToString());
                    machine.gtway_TID = rdr["gtway_TID"].ToString();
                    machine.sync_time = rdr["sync_time"].ToString();
                    machine.second_screen_image_1 = rdr["second_screen_image_1"] as byte[];
                    machine.second_screen_image_2 = rdr["second_screen_image_2"] as byte[];
                    machine.VideoFilePath = rdr["sunmi_video_path"].ToString();
                    machine.TblTranLimit = rdr["TblTranLimit"] != DBNull.Value ? Convert.ToInt32(rdr["TblTranLimit"]) : 0;

                    if (rdr["Active"].ToString() == "Yes")
                    {
                        machine.is_active = true;
                    }
                    else
                    {
                        machine.is_active = false;
                    }

                    if (rdr["is_assign"].ToString() == "Yes")
                    {
                        machine.is_assign = true;
                    }
                    else
                    {
                        machine.is_assign = false;
                    }
                    if (rdr["till_server"].ToString() == "Yes")
                    {
                        machine.till_server = true;
                    }
                    else
                    {
                        machine.till_server = false;
                    }
                    if (rdr["is_master_self"].ToString() == "Yes")
                    {
                        machine.is_master_self = true;
                    }
                    else
                    {
                        machine.is_master_self = false;
                    }
                    if (rdr["extraSurcharge"].ToString() == "Yes")
                    {
                        machine.extraSurcharge = true;
                    }
                    else
                    {
                        machine.extraSurcharge = false;
                    }
                    if (rdr["Only_table_trans"].ToString() == "Yes")
                    {
                        machine.Only_table_trans = true;
                    }
                    else
                    {
                        machine.Only_table_trans = false;
                    }
                    if (rdr["AutoSurcharge"].ToString() == "Yes")
                    {
                        machine.AutoSurcharge = true;
                    }
                    else
                    {
                        machine.AutoSurcharge = false;
                    }
                    if (rdr["NoCashDrawer"].ToString() == "Yes")
                    {
                        machine.NoCashDrawer = true;
                    }
                    else
                    {
                        machine.NoCashDrawer = false;
                    }
                    if (rdr["ReSync_Request"].ToString() == "Yes")
                    {
                        machine.ReSync_Request = true;
                    }
                    else
                    {
                        machine.ReSync_Request = false;
                    }
                    if (rdr["Is_PrintServer"].ToString() == "Yes")
                    {
                        machine.Is_PrintServer = true;
                    }
                    else
                    {
                        machine.Is_PrintServer = false;
                    }
                    if (rdr["Service_Controller_Start"].ToString() == "Yes")
                    {
                        machine.Service_Controller_Start = true;
                    }
                    else
                    {
                        machine.Service_Controller_Start = false;
                    }
                    if (rdr["Service_Print_Share"].ToString() == "Yes")
                    {
                        machine.Service_Print_Share = true;
                    }
                    else
                    {
                        machine.Service_Print_Share = false;
                    }
                    if (rdr["Service_Websale_print"].ToString() == "Yes")
                    {
                        machine.Service_Websale_print = true;
                    }
                    else
                    {
                        machine.Service_Websale_print = false;
                    }
                    if (rdr["Service_print_Share_Other_Till"].ToString() == "Yes")
                    {
                        machine.Service_print_Share_Other_Till = true;
                    }
                    else
                    {
                        machine.Service_print_Share_Other_Till = false;
                    }
                    if (rdr["Is_ServiceBooking"].ToString() == "Yes")
                    {
                        machine.Is_ServiceBooking = true;
                    }
                    else
                    {
                        machine.Is_ServiceBooking = false;
                    }
                    if (rdr["Is_OnlineZreport"].ToString() == "Yes")
                    {
                        machine.Is_OnlineZreport = true;
                    }
                    else
                    {
                        machine.Is_OnlineZreport = false;
                    }
                    if (rdr["ElinaTran"].ToString() == "Yes")
                    {
                        machine.ElinaTran = true;
                    }
                    else
                    {
                        machine.ElinaTran = false;
                    }
                    if (rdr["QuickTran"].ToString() == "Yes")
                    {
                        machine.QuickTran = true;
                    }
                    else
                    {
                        machine.QuickTran = false;
                    }
                    if (rdr["table_sharing"].ToString() == "Yes")
                    {
                        machine.table_sharing = true;
                    }
                    else
                    {
                        machine.table_sharing = false;
                    }
                    if (rdr["printer_sharing"].ToString() == "Yes")
                    {
                        machine.printer_sharing = true;
                    }
                    else
                    {
                        machine.printer_sharing = false;
                    }

                    if (rdr["back_to_main_function_on_till"].ToString() == "Yes")
                    {
                        machine.back_to_main_function_on_till = true;
                    }
                    else
                    {
                        machine.back_to_main_function_on_till = false;
                    }
                    if (rdr["Is_NoLogout"].ToString() == "Yes")
                    {
                        machine.Is_NoLogout = true;
                    }
                    else
                    {
                        machine.Is_NoLogout = false;
                    }
                    if (rdr["poslite"].ToString() == "Yes")
                    {
                        machine.poslite = true;
                    }
                    else
                    {
                        machine.poslite = false;
                    }
                    if (rdr["AutoSurchargeTables"].ToString() == "Yes")
                    {
                        machine.AutoSurchargeTables = true;
                    }
                    else
                    {
                        machine.AutoSurchargeTables = false;
                    }
                    if (rdr["AutoSurchargeNonTables"].ToString() == "Yes")
                    {
                        machine.AutoSurchargeNonTables = true;
                    }
                    else
                    {
                        machine.AutoSurchargeNonTables = false;
                    }
                    if (rdr["AutoSurchargeCloakroom"].ToString() == "Yes")
                    {
                        machine.AutoSurchargeCloakroom = true;
                    }
                    else
                    {
                        machine.AutoSurchargeCloakroom = false;
                    }
                    if (rdr["AutoSurchargeChips"].ToString() == "Yes")
                    {
                        machine.AutoSurchargeChips = true;
                    }
                    else
                    {
                        machine.AutoSurchargeChips = false;
                    }
                    if (rdr["kitchenPrint"].ToString() == "Yes")
                    {
                        machine.kitchenPrint = true;
                    }
                    else
                    {
                        machine.kitchenPrint = false;
                    } 
                    if (rdr["TillRequest"].ToString() == "Yes")
                    {
                        machine.TillRequest = true;
                    }
                    else
                    {
                        machine.TillRequest = false;
                    }
                    if (rdr["KioskRequest"].ToString() == "Yes")
                    {
                        machine.KioskRequest = true;
                    }
                    else
                    {
                        machine.KioskRequest = false;
                    }
                    if (rdr["is_sunmi_second_screen"].ToString() == "Yes")
                    {
                        machine.is_sunmi_second_screen = true;
                    }
                    else
                    {
                        machine.is_sunmi_second_screen = false;
                    }
                    if (rdr["ReceiptPrint"].ToString() == "Yes")
                    {
                        machine.ReceiptPrint = true;
                    }
                    else
                    {
                        machine.ReceiptPrint = false;
                    }

                }
                rdr.Close();
               
                string query = "SELECT device_id FROM M_Machine_Details WHERE machine_id = @machine_id";
                SqlCommand deviceCmd = new SqlCommand(query, con);
                deviceCmd.Parameters.AddWithValue("@machine_id", machine.machine_id??0);

                SqlDataReader deviceRdr = deviceCmd.ExecuteReader();

                // Assuming machine has a list or property to hold the devices
                List<int> deviceIds = new List<int>();

                while (deviceRdr.Read())
                {
                    deviceIds.Add(Convert.ToInt32(deviceRdr["device_id"]));
                }

                // Optionally, you can assign the device IDs to a property of your machine object
                machine.device_id = deviceIds;   // Make sure `DeviceIds` is a property of the `clsMachine` class

                deviceRdr.Close();
                GetSelectedDevicesForMachine(machine.machine_id??0, connStr);
                string functionQuery = "SELECT function_id FROM M_Till_MainFunction WHERE machine_id = @machine_id";
                SqlCommand functionCmd = new SqlCommand(functionQuery, con);
                functionCmd.Parameters.AddWithValue("@machine_id", machine.machine_id??0);

                SqlDataReader functionRdr = functionCmd.ExecuteReader();
                if (functionRdr.Read())
                {
                    machine.function_id = Convert.ToInt32(functionRdr["function_id"]);
                }

                functionRdr.Close();
                UpdateFunction(machine.cmp_id, machine.till_id, machine.machine_id??0, machine.function_id ?? 0, connStr);


                con.Close();
                //}


                //InsertFunction(machine.cmp_id, machine.till_id, machine.machine_id, function_id, connStr);
                //UpdateFunction(machine.cmp_id, machine.till_id, machine.machine_id, function_id, connStr);
                //con.Close();
            }
            return machine;
        }

        private Dictionary<int, int> GetSelectedDevicesForMachine(int machine_id, string ConnStr)
        {
            var SelectedDevices = new Dictionary<int, int>();

            // Example: Query the database for the shorting numbers associated with this machine and its keymaps
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                string query = "select Printer_id , Device_id from M_Machine_Details WHERE machine_id = @machine_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@machine_id", machine_id);
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

        private void UpdateFunction(int cmp_id, int till_id, int machine_id, int function_id, string ConnStr)
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

        public void Delete(clsMachine machine, string connStr, string Mainip_address)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", machine.cmp_id);
                cmd.Parameters.AddWithValue("@machine_id ", machine.machine_id ?? 0);
                cmd.Parameters.AddWithValue("@name", machine.name);
                cmd.Parameters.AddWithValue("@serial_no", machine.serial_no ?? "0");
                cmd.Parameters.AddWithValue("@mac_address", machine.mac_address ?? "");
                cmd.Parameters.AddWithValue("@model", machine.model ?? "");
                cmd.Parameters.AddWithValue("@code", /*machine.code*/ "0");
                cmd.Parameters.AddWithValue("@ip_address", Mainip_address);
                cmd.Parameters.AddWithValue("@tillip_address", string.IsNullOrEmpty(machine.ip_address) ? DBNull.Value : (object)machine.ip_address);
                cmd.Parameters.AddWithValue("@login_id", machine.login_id);
                cmd.Parameters.AddWithValue("@location_id", machine.location_id);
                cmd.Parameters.AddWithValue("@hardware_type", machine.hardware_type);
                cmd.Parameters.AddWithValue("@is_active", machine.is_active);
                cmd.Parameters.AddWithValue("@is_assign", machine.is_assign);
                cmd.Parameters.AddWithValue("@till_server", machine.till_server);
                cmd.Parameters.AddWithValue("@is_master_self", machine.is_master_self);
                cmd.Parameters.AddWithValue("@extraSurcharge", machine.extraSurcharge);
                cmd.Parameters.AddWithValue("@onlytabletrans", machine.Only_table_trans);
                cmd.Parameters.AddWithValue("@AutoSurcharge", machine.AutoSurcharge);
                cmd.Parameters.AddWithValue("@NoCashDrawer", machine.NoCashDrawer);
                cmd.Parameters.AddWithValue("@ReSync_Request", machine.ReSync_Request);
                cmd.Parameters.AddWithValue("@Is_PrintServer", machine.Is_PrintServer);
                cmd.Parameters.AddWithValue("@Service_Controller_Start", machine.Service_Controller_Start);
                cmd.Parameters.AddWithValue("@Service_Websale_print", machine.Service_Websale_print);
                cmd.Parameters.AddWithValue("@Service_Print_Share", machine.Service_Print_Share);
                cmd.Parameters.AddWithValue("@Service_print_Share_Other_Till", machine.Service_print_Share_Other_Till);
                cmd.Parameters.AddWithValue("@Is_ServiceBooking", machine.Is_ServiceBooking);
                cmd.Parameters.AddWithValue("@Is_OnlineZreport", machine.Is_OnlineZreport);
                cmd.Parameters.AddWithValue("@ElinaTran", machine.ElinaTran);
                cmd.Parameters.AddWithValue("@QuickTran", machine.QuickTran);
                cmd.Parameters.AddWithValue("@table_sharing", machine.table_sharing);
                cmd.Parameters.AddWithValue("@printer_sharing", machine.printer_sharing);
                cmd.Parameters.AddWithValue("@back_to_main_function_on_till", machine.back_to_main_function_on_till);
                cmd.Parameters.AddWithValue("@Is_NoLogout", machine.Is_NoLogout);
                cmd.Parameters.AddWithValue("@poslite", machine.poslite);
                cmd.Parameters.AddWithValue("@sunmiSecondScreen", machine.is_sunmi_second_screen);
                cmd.Parameters.AddWithValue("@gtway_TID", machine.gtway_TID);
                cmd.Parameters.AddWithValue("@TranLimit", machine.TblTranLimit);
                cmd.Parameters.AddWithValue("@sync_time", machine.sync_time);
                cmd.Parameters.AddWithValue("@SecondScreenImage1", machine.second_screen_image_1);
                cmd.Parameters.AddWithValue("@SecondScreenImage2", machine.second_screen_image_2);
                //cmd.Parameters.AddWithValue("@sunmi_video_path", machine.sunmi_video_path);
                 

                
                cmd.Parameters.AddWithValue("@kitchenPrint", machine.kitchenPrint);
                cmd.Parameters.AddWithValue("@AutoSurchargeTables", machine.AutoSurchargeTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeNonTables", machine.AutoSurchargeNonTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeCloakroom", machine.AutoSurchargeCloakroom);
                cmd.Parameters.AddWithValue("@AutoSurchargeChips", machine.AutoSurchargeChips);
                cmd.Parameters.AddWithValue("@TillRequest", machine.TillRequest);
                cmd.Parameters.AddWithValue("@KioskRequest", machine.KioskRequest);
                cmd.Parameters.AddWithValue("@ReceiptPrint", machine.ReceiptPrint);
                cmd.Parameters.AddWithValue("@master_till", machine.master_till);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");
           
                con.Open();
                cmd.ExecuteNonQuery();               
                con.Close();
            }
        }

        public IActionResult SyncModelToController(string ConnStr, int cmpID, int storeUUID, string modelName)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=TMS-LA04\\SQLEXPRESS;Initial Catalog=POS_Con;User ID=sa;Password=TMS@2017;"))
            {
                connection.Open();

                // Retrieve the models from the database (like SelectAll in VB.NET)
                MachineController obj = new MachineController();
                List<clsMachine> machineList = obj.SelectAll(cmpID, ConnStr).ToList();  // Assuming SelectAll returns a list of machines with models

                // Loop through each machine/model
                foreach (var machine in machineList)
                {
                    // Use a different name for the local variable to avoid conflict
                    string currentModelName = machine.model;  // Rename the variable to 'currentModelName'

                    // Execute the stored procedure to sync the model to the controller
                    using (SqlCommand command = new SqlCommand("P_M_Sync_AllModels", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@StoreUUID", storeUUID); // Assuming `storeUUID` comes from the session or passed argument
                        command.Parameters.AddWithValue("@Model_Name", currentModelName);  // Use the renamed variable 'currentModelName'

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();  // Close the connection after processing all models
            }

            // Optionally redirect after success
            return RedirectToAction("Index");
        }

        public void Update(clsMachine machine, string connStr, int function_id, IFormFile VideoFile)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", machine.cmp_id);
                cmd.Parameters.AddWithValue("@machine_id ", machine.machine_id??0);
                cmd.Parameters.AddWithValue("@name", machine.name);
                cmd.Parameters.AddWithValue("@serial_no", machine.serial_no??"0");
                cmd.Parameters.AddWithValue("@mac_address", machine.mac_address??"");
                cmd.Parameters.AddWithValue("@model", machine.model??"");
                cmd.Parameters.AddWithValue("@code", /*machine.code*/ "0");
                cmd.Parameters.AddWithValue("@ip_address",machine.Mainip_address);
                cmd.Parameters.AddWithValue("@tillip_address", string.IsNullOrEmpty(machine.ip_address) ? DBNull.Value : (object)machine.ip_address);
                cmd.Parameters.AddWithValue("@login_id", machine.login_id);
                cmd.Parameters.AddWithValue("@location_id", machine.location_id);
                cmd.Parameters.AddWithValue("@hardware_type", machine.hardware_type);
                cmd.Parameters.AddWithValue("@is_active", machine.is_active);
                cmd.Parameters.AddWithValue("@is_assign", machine.is_assign);
                cmd.Parameters.AddWithValue("@till_server", machine.till_server);
                cmd.Parameters.AddWithValue("@is_master_self", machine.is_master_self);
                cmd.Parameters.AddWithValue("@extraSurcharge", machine.extraSurcharge);
                cmd.Parameters.AddWithValue("@onlytabletrans", machine.Only_table_trans);
                cmd.Parameters.AddWithValue("@AutoSurcharge", machine.AutoSurcharge);
                cmd.Parameters.AddWithValue("@NoCashDrawer", machine.NoCashDrawer);
                cmd.Parameters.AddWithValue("@ReSync_Request", machine.ReSync_Request);
                cmd.Parameters.AddWithValue("@Is_PrintServer", machine.Is_PrintServer);
                cmd.Parameters.AddWithValue("@Service_Controller_Start", machine.Service_Controller_Start);
                cmd.Parameters.AddWithValue("@Service_Websale_print", machine.Service_Websale_print);
                cmd.Parameters.AddWithValue("@Service_Print_Share", machine.Service_Print_Share);
                cmd.Parameters.AddWithValue("@Service_print_Share_Other_Till", machine.Service_print_Share_Other_Till);
                cmd.Parameters.AddWithValue("@Is_ServiceBooking", machine.Is_ServiceBooking);
                cmd.Parameters.AddWithValue("@Is_OnlineZreport", machine.Is_OnlineZreport);
                cmd.Parameters.AddWithValue("@ElinaTran", machine.ElinaTran);
                cmd.Parameters.AddWithValue("@QuickTran", machine.QuickTran);
                cmd.Parameters.AddWithValue("@table_sharing", machine.table_sharing);
                cmd.Parameters.AddWithValue("@printer_sharing", machine.printer_sharing);
                cmd.Parameters.AddWithValue("@back_to_main_function_on_till", machine.back_to_main_function_on_till);
                cmd.Parameters.AddWithValue("@Is_NoLogout", machine.Is_NoLogout);
                cmd.Parameters.AddWithValue("@poslite", machine.poslite);
                cmd.Parameters.AddWithValue("@sunmiSecondScreen", machine.is_sunmi_second_screen);
                cmd.Parameters.AddWithValue("@gtway_TID", machine.gtway_TID);
                 cmd.Parameters.AddWithValue("@TranLimit", machine.TblTranLimit);
                cmd.Parameters.AddWithValue("@sync_time", machine.sync_time);
                cmd.Parameters.AddWithValue("@SecondScreenImage1", machine.second_screen_image_1);
                cmd.Parameters.AddWithValue("@SecondScreenImage2", machine.second_screen_image_2);
                //cmd.Parameters.AddWithValue("@sunmi_video_path", machine.sunmi_video_path);
                string videoFilePath = null;
                if (VideoFile != null && VideoFile.Length > 0)
                {
                    // Generate a unique filename for the video
                    string videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(VideoFile.FileName); 
                    string videoUploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedVideos");

                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(videoUploadFolder))
                        Directory.CreateDirectory(videoUploadFolder);

                    // Save the video file to the server
                    string videoFullPath = Path.Combine(videoUploadFolder, videoFileName);
                    using (var stream = new FileStream(videoFullPath, FileMode.Create))
                    {
                        VideoFile.CopyTo(stream);
                    }

                    // Store the relative path to the video
                    videoFilePath = "/UploadedVideos/" + videoFileName;
                }

                // Set the video status byte and add video path as parameter
                byte videoStatus = (string.IsNullOrEmpty(videoFilePath)) ? (byte)0 : (byte)1;
                cmd.Parameters.AddWithValue("@sunmi_video_path", videoFilePath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@kitchenPrint", machine.kitchenPrint);
                cmd.Parameters.AddWithValue("@AutoSurchargeTables", machine.AutoSurchargeTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeNonTables", machine.AutoSurchargeNonTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeCloakroom", machine.AutoSurchargeCloakroom);
                cmd.Parameters.AddWithValue("@AutoSurchargeChips", machine.AutoSurchargeChips);
                cmd.Parameters.AddWithValue("@TillRequest", machine.TillRequest);
                cmd.Parameters.AddWithValue("@KioskRequest", machine.KioskRequest);
                cmd.Parameters.AddWithValue("@ReceiptPrint", machine.ReceiptPrint);
                cmd.Parameters.AddWithValue("@master_till", machine.master_till);
                cmd.Parameters.AddWithValue("@Receipt_Header", machine.Receipt_Header);
                cmd.Parameters.AddWithValue("@Receipt_Footer", machine.Receipt_Footer);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                UpdateFunction(machine.cmp_id, machine.till_id, machine.machine_id??0, function_id, connStr);
                con.Close();
            }
        }

        public void Insert(clsMachine machine, string connStr, int function_id, IFormFile VideoFile)
        {
            //if (MachineExists(machine.machine_id, machine.serial_no, connStr))
            //{
            //    // If the machine already exists, don't insert a new record
            //    Console.WriteLine("Machine already exists.");
            //    return;  // or handle this case as you want (e.g., update existing record)
            //}
            int newMachineId;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Machine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", machine.cmp_id);
                cmd.Parameters.AddWithValue("@machine_id ", machine.machine_id??0);
                cmd.Parameters.AddWithValue("@name", machine.name);
                cmd.Parameters.AddWithValue("@serial_no", machine.serial_no??"");
                cmd.Parameters.AddWithValue("@mac_address", machine.mac_address??"");
                cmd.Parameters.AddWithValue("@model", machine.model??"");
                cmd.Parameters.AddWithValue("@code", /*machine.code*/ "0");
                cmd.Parameters.AddWithValue("@ip_address",machine.Mainip_address);
                cmd.Parameters.AddWithValue("@tillip_address", string.IsNullOrEmpty(machine.ip_address) ? DBNull.Value : (object)machine.ip_address);
                cmd.Parameters.AddWithValue("@login_id", machine.login_id);
                cmd.Parameters.AddWithValue("@location_id", machine.location_id);
                cmd.Parameters.AddWithValue("@hardware_type", machine.hardware_type);
                cmd.Parameters.AddWithValue("@is_active", machine.is_active);
                cmd.Parameters.AddWithValue("@is_assign", machine.is_assign);
                cmd.Parameters.AddWithValue("@till_server", machine.till_server);
                cmd.Parameters.AddWithValue("@is_master_self", machine.is_master_self);
                cmd.Parameters.AddWithValue("@extraSurcharge", machine.extraSurcharge);
                cmd.Parameters.AddWithValue("@onlytabletrans", machine.Only_table_trans);
                cmd.Parameters.AddWithValue("@AutoSurcharge", machine.AutoSurcharge);
                cmd.Parameters.AddWithValue("@NoCashDrawer", machine.NoCashDrawer);
                cmd.Parameters.AddWithValue("@ReSync_Request", machine.ReSync_Request);
                cmd.Parameters.AddWithValue("@Is_PrintServer", machine.Is_PrintServer);
                cmd.Parameters.AddWithValue("@Service_Controller_Start", machine.Service_Controller_Start);
                cmd.Parameters.AddWithValue("@Service_Websale_print", machine.Service_Websale_print);
                cmd.Parameters.AddWithValue("@Service_Print_Share", machine.Service_Print_Share);
                cmd.Parameters.AddWithValue("@Service_print_Share_Other_Till", machine.Service_print_Share_Other_Till);
                cmd.Parameters.AddWithValue("@Is_ServiceBooking", machine.Is_ServiceBooking);
                cmd.Parameters.AddWithValue("@Is_OnlineZreport", machine.Is_OnlineZreport);
                cmd.Parameters.AddWithValue("@ElinaTran", machine.ElinaTran);
                cmd.Parameters.AddWithValue("@QuickTran", machine.QuickTran);
                cmd.Parameters.AddWithValue("@table_sharing", machine.table_sharing);
                cmd.Parameters.AddWithValue("@printer_sharing", machine.printer_sharing);
                cmd.Parameters.AddWithValue("@back_to_main_function_on_till", machine.back_to_main_function_on_till);
                cmd.Parameters.AddWithValue("@Is_NoLogout", machine.Is_NoLogout);
                cmd.Parameters.AddWithValue("@poslite", machine.poslite);
               cmd.Parameters.AddWithValue("@sunmiSecondScreen", machine.is_sunmi_second_screen);
                cmd.Parameters.AddWithValue("@gtway_TID", machine.gtway_TID);
                cmd.Parameters.AddWithValue("@sync_time", machine.sync_time);
                 cmd.Parameters.AddWithValue("@TranLimit", machine.TblTranLimit);
                cmd.Parameters.AddWithValue("@SecondScreenImage1", machine.second_screen_image_1);
                cmd.Parameters.AddWithValue("@SecondScreenImage2", machine.second_screen_image_2);
                string videoFilePath = null;
                if (VideoFile != null && VideoFile.Length > 0)
                {
                    // Generate a unique filename for the video
                    string videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(VideoFile.FileName);
                    string videoUploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedVideos");

                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(videoUploadFolder))
                        Directory.CreateDirectory(videoUploadFolder);

                    // Save the video file to the server
                    string videoFullPath = Path.Combine(videoUploadFolder, videoFileName);
                    using (var stream = new FileStream(videoFullPath, FileMode.Create))
                    {
                        VideoFile.CopyTo(stream);
                    }

                    // Store the relative path to the video
                    videoFilePath = /*"/UploadedVideos/" +*/ videoFileName;
                }

                // Add the video file path as a parameter for the stored procedure
                //cmd.Parameters.AddWithValue("@sunmi_video_path", videoFilePath ?? (object)DBNull.Value);
                //cmd.Parameters.Add("@sunmi_video_path", SqlDbType.NVarChar, 500).Value = videoFilePath ?? (object)DBNull.Value;
                byte videoStatus = (string.IsNullOrEmpty(videoFilePath)) ? (byte)0 : (byte)1;
                cmd.Parameters.AddWithValue("@sunmi_video_path", videoStatus);

 

                cmd.Parameters.AddWithValue("@kitchenPrint", machine.kitchenPrint);
                cmd.Parameters.AddWithValue("@AutoSurchargeTables", machine.AutoSurchargeTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeNonTables", machine.AutoSurchargeNonTables);
                cmd.Parameters.AddWithValue("@AutoSurchargeCloakroom", machine.AutoSurchargeCloakroom);
                cmd.Parameters.AddWithValue("@AutoSurchargeChips", machine.AutoSurchargeChips);
                cmd.Parameters.AddWithValue("@TillRequest", machine.TillRequest);
                cmd.Parameters.AddWithValue("@KioskRequest", machine.KioskRequest);
                cmd.Parameters.AddWithValue("@ReceiptPrint", machine.ReceiptPrint);
                cmd.Parameters.AddWithValue("@master_till", machine.master_till);
                cmd.Parameters.AddWithValue("@Receipt_Header", machine.Receipt_Header);
                cmd.Parameters.AddWithValue("@Receipt_Footer", machine.Receipt_Footer);

                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                newMachineId = Convert.ToInt32(cmd.ExecuteScalar());
                machine.machine_id = newMachineId;
                //cmd.ExecuteNonQuery();
                //InsertFunction(machine.cmp_id, machine.till_id, machine.machine_id, function_id, connStr);
                con.Close();
            }
        }

        private bool MachineExists(int machine_id, string serial_no, string connStr)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM M_Machine WHERE machine_id = @machine_id OR serial_no = @serial_no", con);
                cmd.Parameters.AddWithValue("@machine_id", machine_id);
                cmd.Parameters.AddWithValue("@serial_no", serial_no);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;  // If count > 0, the machine already exists
            }
        }

        private void InsertFunction(int cmp_id, int TillId, int machine_id, int function_id, string ConnStr)
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
                cmd.Parameters.AddWithValue("@till_id", TillId);
                //cmd.Parameters.AddWithValue("@shorting_num", shortingNum);
                cmd.Parameters.AddWithValue("@Tran_Type", "I"); // Assuming "I" for Insert

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
