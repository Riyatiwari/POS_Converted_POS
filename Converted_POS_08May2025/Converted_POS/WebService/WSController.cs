using Converted_POS.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
 
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using System.Data.SqlClient;
 
using Newtonsoft.Json;
//using ServiceReference1; 
namespace Converted_POS
{
    [ApiController]
    [Route("WebService/MyWCFService")]
    public class WSController : Controller
    {


        private readonly IConfiguration _configuration;

        public WSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return Redirect("/Login");
            }

            return Content("This is the WebService page");
        }
        // Example usage: /POSWS?Function=Login&authU=...&authP=... etc.
        [HttpGet]
        //public IActionResult CallMethod()
        //{
        //    try
        //    {
        //        // Retrieve the function from the query string
        //        string function = Request.Query["Function"];
        //        if (string.IsNullOrWhiteSpace(function))
        //        {
        //            return Content("Invalid Request: Missing Function parameter.");
        //        }

        //        // Create SOAP client. In production, you'd typically inject this via DI.

        //        string str; switch (function)
        //        {
        //            case "Login":
        //                str = obj.Login(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    Request.Query["loginU"],
        //                    Request.Query["LoginP"],
        //                    Request.Query["IP"],
        //                    Request.Query["mac_id"],
        //                    Request.Query["store_name"]
        //                );
        //                return Content(ValidateResult(str));

        //            case "Customer_List":
        //                str = _configuration.Customer_List(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"]
        //                );
        //                return Content(ValidateResult(str));

        //            case "KioskSettiong_List":
        //                str = _configuration.KioskSettiong_List(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"],
        //                    TryParseInt(Request.Query["TillID"])
        //                );
        //                return Content(ValidateResult(str));

        //            case "AutoSync_List":
        //                str = _configuration.AutoSync_List(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"],
        //                    TryParseInt(Request.Query["TillID"])
        //                );
        //                return Content(ValidateResult(str));

        //            case "AutoSync_Update":
        //                str = _configuration.AutoSync_Update(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"],
        //                    TryParseInt(Request.Query["AutoSync_Id"])
        //                );
        //                return Content(ValidateResult(str));

        //            case "Graphic_Table":
        //                str = _configuration.Graphic_Table(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"],
        //                    Request.Query["location_id"]
        //                );
        //                return Content(ValidateResult(str));

        //            case "Schedule_List":
        //                str = _configuration.Schedule_List(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    Request.Query["store_name"],
        //                    Request.Query["location_id"]
        //                );
        //                return Content(ValidateResult(str));

        //            case "Customer_Master":
        //                str = _configuration.Customer_Master(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    TryParseInt(Request.Query["cmp"]),
        //                    TryParseInt(Request.Query["login"]),
        //                    TryParseInt(Request.Query["customer_id"]),
        //                    Request.Query["first_name"],
        //                    Request.Query["last_name"],
        //                    Request.Query["email"],
        //                    Request.Query["contact"],
        //                    Request.Query["address"],
        //                    TryParseInt(Request.Query["country"]),
        //                    TryParseInt(Request.Query["state"]),
        //                    TryParseInt(Request.Query["city"]),
        //                    Request.Query["postal_code"],
        //                    TryParseInt(Request.Query["is_active"]),
        //                    Request.Query["ip"],
        //                    Request.Query["Tran_Type"],
        //                    Request.Query["mac_id"],
        //                    TryParseInt(Request.Query["venue_id"]),
        //                    Request.Query["other_id"],
        //                    Request.Query["store_name"],
        //                    TryParseInt(Request.Query["machine_id"])
        //                );
        //                return Content(ValidateResult(str));

        //            case "AddDPT_Lite":
        //                str = _configuration.AddDPT_Lite(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    Request.Query["store_name"],
        //                    Request.Query["department_name"],
        //                    TryParseInt(Request.Query["department_id"]),
        //                    TryParseInt(Request.Query["is_product"])
        //                );
        //                return Content(ValidateResult(str));

        //            case "Communicator_transactionData":
        //                str = _configuration.Communicator_transactionData(
        //                    Request.Query["authU"],
        //                    Request.Query["authP"],
        //                    Request.Query["store_name"],
        //                    TryParseInt(Request.Query["location_id"]),
        //                    TryParseInt(Request.Query["id"]),
        //                    Request.Query["pay_uuid"],
        //                    Request.Query["TOTAL_AMOUNT"],
        //                    Request.Query["LANGUAGE"],
        //                    TryParseInt(Request.Query["TABLE_MASTER_REF_ID"]),
        //                    Request.Query["TRAN_UUID"],
        //                    Request.Query["TABLE_UUID"],
        //                    TryParseInt(Request.Query["CREATED_USER_ID"]),
        //                    TryParseInt(Request.Query["MACHINE_ID"]),
        //                    TryParseInt(Request.Query["IS_SYNC"]),
        //                    TryParseInt(Request.Query["IS_REPLAY"]),
        //                    DateTime.TryParse(Request.Query["CREATED_DATE"], out var createdDate) ? createdDate : DateTime.MinValue,
        //                    DateTime.TryParse(Request.Query["MODIFIED_DATE"], out var modifiedDate) ? modifiedDate : DateTime.MinValue,
        //                    TryParseInt(Request.Query["PAX_POS_ID"]),
        //                    Request.Query["IP_ADDRESS"],
        //                    TryParseInt(Request.Query["IS_COMPLETE_TASK"]),
        //                    Request.Query["STATUS"],
        //                    TryParseInt(Request.Query["IS_PROCESSING"]),
        //                    Request.Query["PAX_UUID"],
        //                    TryParseInt(Request.Query["IS_WEBOR_POS"]),
        //                    Request.Query["Payment_Method"],
        //                    Request.Query["transactionType"],
        //                    TryParseInt(Request.Query["is_lastdata"])
        //                );
        //                return Content(ValidateResult(str));

        //            default:
        //                return Content($"Invalid Function: {function}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log your exception here, e.g. using ILogger or a custom LogHelper.
        //        // Example: _logger.LogError(ex, "POS_WS:CallMethod()");
        //        return Content("Invalid Request: " + ex.Message);
        //    }
        //}

        /// <summary>
        /// Replaces "[]" / "No User Found." / "Invalid Webservice User Access...!" with a generic message.
        /// Otherwise returns the original string.
        /// </summary>
        /// 

        public string DataTable(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, string.IsNullOrEmpty(dr[col].ToString()) ? "" : dr[col]);
                }
                rows.Add(row);
            }
            string str = JsonConvert.SerializeObject(rows, new JsonSerializerSettings { MaxDepth = 32 });
            return str;
        }



        public string DataTableToJsonWithEncoding(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "username")
                    {
                        row.Add(col.ColumnName, string.IsNullOrEmpty(dr[col].ToString()) ? "" : EncodeData(dr[col].ToString()));
                    }
                    else
                    {
                        row.Add(col.ColumnName, string.IsNullOrEmpty(dr[col].ToString()) ? "" : dr[col]);
                    }
                }
                rows.Add(row);
            }

            string str = JsonConvert.SerializeObject(rows, new JsonSerializerSettings { MaxDepth = 32 });
            return str;
        }

        public string EncodeData(string str)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
            }
            catch (Exception)
            {
                return "";
            }
        }



        public string DecodeData(string str)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(str));
            }
            catch (Exception)
            {
                return "";
            }
        }
        public DataTable GetDataTableSp(string spName, ColSqlparram oColSqlparram, string strTableName = "", string constr = "")
        {
            DataTable dataTable = null;
            SqlCommand com = new SqlCommand();
            SqlDataAdapter spAdapter = new SqlDataAdapter();

            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    connection.Open();
                    com.CommandText = spName;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = connection;
                    com.Parameters.Clear();
                    com.CommandTimeout = 100;

                    foreach (ClsSqlParameter oClsSqlParameter in oColSqlparram)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = oClsSqlParameter.ParaName,
                            SqlDbType = oClsSqlParameter.ParaType,
                            Value = oClsSqlParameter.ParaValue,
                            Direction = oClsSqlParameter.ParaDirection
                        };
                        com.Parameters.Add(parameter);
                    }

                    spAdapter = new SqlDataAdapter(com);
                    dataTable = new DataTable();
                    spAdapter.Fill(dataTable);

                    if (!string.IsNullOrEmpty(strTableName))
                    {
                        dataTable.TableName = strTableName;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                com.Parameters.Clear();
                com = null;
                spAdapter = null;
            }

            return dataTable;
        }
        public void ExecStoredProcedure(string strProcedureName, ColSqlparram oColSqlparram, string constr)
        {
            int intParaCount;
            SqlCommand com = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlTransaction sql_Tran = null;
                try
                {
                    connection.Open();
                    sql_Tran = connection.BeginTransaction();
                    com.CommandText = strProcedureName;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = connection;
                    com.CommandTimeout = 100;
                    com.Transaction = sql_Tran;
                    com.Parameters.Clear();

                    foreach (var parameter in oColSqlparram)
                    {
                        SqlParameter sqlParameter = new SqlParameter
                        {
                            ParameterName = parameter.ParaName,
                            SqlDbType = parameter.ParaType,
                            Value = parameter.ParaValue,
                            Direction = parameter.ParaDirection
                        };

                        // Adjust parameter size for string type
                        if (sqlParameter.SqlDbType == SqlDbType.VarChar && parameter.ParaDirection == ParameterDirection.Input)
                        {
                            sqlParameter.Size = string.IsNullOrEmpty(parameter.ParaValue.ToString()) ? 1 : parameter.ParaValue.ToString().Length;
                        }
                        else if (sqlParameter.SqlDbType == SqlDbType.VarChar && parameter.ParaDirection != ParameterDirection.Input)
                        {
                            sqlParameter.Size = string.IsNullOrEmpty(parameter.ParaValue.ToString()) ? 1000 : parameter.ParaValue.ToString().Length;
                        }

                        com.Parameters.Add(sqlParameter);
                    }

                    // Execute stored procedure
                    com.ExecuteNonQuery();

                    // Update input/output parameters
                    foreach (var parameter in oColSqlparram)
                    {
                        if (parameter.ParaDirection == ParameterDirection.InputOutput || parameter.ParaDirection == ParameterDirection.Output)
                        {
                            parameter.ParaValue = CheckNull(com.Parameters[parameter.ParaName].Value);
                        }
                    }

                    sql_Tran.Commit();
                }
                catch (Exception ex)
                {
                    sql_Tran?.Rollback();
                    if (ex.Message.ToUpper().StartsWith("DELETE STATEMENT CONFLICTED") || ex.Message.Contains("DELETE STATEMENT CONFLICTED"))
                    {
                        // Handle specific error
                        foreach (var parameter in oColSqlparram)
                        {
                            if (parameter.ParaDirection == ParameterDirection.InputOutput || parameter.ParaDirection == ParameterDirection.Output)
                            {
                                parameter.ParaValue = 0;
                            }
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    com = null;
                }
            }
        }

        public string CheckNull(object vData)
        {
            try
            {
                return vData == DBNull.Value ? "" : vData.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpGet("WebLogin")]
        public string Login([FromQuery] string authUsername, [FromQuery] string authPassword, [FromQuery] string user, [FromQuery] string pass, [FromQuery] string IP_Address, [FromQuery] string mac_id, [FromQuery] string store_name)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        oColSqlparram.Add("@User_Name", user);
                        oColSqlparram.Add("@Password", Encrypt(pass));
                        oColSqlparram.Add("@IP_Address", IP_Address);
                        oColSqlparram.Add("@Access_Status", 1);
                        oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                        oColSqlparram.Add("@mac_id", mac_id);

                        ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));

                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@Username", DecodeData(user));
                        oColSqlPar.Add("@Password", Encrypt(DecodeData(pass)));
                        oColSqlPar.Add("@Login_URL", System.Configuration.ConfigurationManager.AppSettings["Login_URL"]);

                        DataTable datatbl = GetDataTableSp("WS_do_Login", oColSqlPar, "M_Login", getconstr(dt));

                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", user);
                    oColSqlparram.Add("@Password", pass);
                    oColSqlparram.Add("@IP_Address", IP_Address);
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", mac_id);
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);

                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));

                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [HttpGet("Customer_List")]
        public string Customer_List(string authUsername, string authPassword, int Cmp_id, string store_name)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_get_M_Customer", oColSqlPar, "WS_get_M_Customer", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("KioskSettiong_List")]
        public string KioskSettiong_List(string authUsername, string authPassword, int Cmp_id, string store_name, int TillID)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
                        oColSqlPar.Add("@TillID", TillID, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_get_KioskSettings", oColSqlPar, "WS_get_KioskSettings", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("AutoSync_List")]
        public string AutoSync_List(string authUsername, string authPassword, int Cmp_id, string store_name, int TillID)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
                        oColSqlPar.Add("@TillID", TillID, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_get_M_AutoSyncRecord", oColSqlPar, "WS_get_M_AutoSyncRecord", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("AutoSync_Update")]
        public string AutoSync_Update(string authUsername, string authPassword, int Cmp_id, string store_name, int AutoSync_Id)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
                        oColSqlPar.Add("@AutoSync_Id", AutoSync_Id, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_Update_M_AutoSyncRecord", oColSqlPar, "WS_Update_M_AutoSyncRecord", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return "[{\"Message\":\"Status Updated.\"}]";
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("Graphic_Table")]
        public string Graphic_Table(string authUsername, string authPassword, int Cmp_id, string store_name, int location_id)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
                        oColSqlPar.Add("@location_id", location_id, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_get_Graphic_table", oColSqlPar, "WS_get_Graphic_table", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("Schedule_List")]
        public string Schedule_List(string authUsername, string authPassword, int Cmp_id, string store_name, int location_id)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    if (authUsername == dt.Rows[0]["user_name"].ToString() && authPassword == dt.Rows[0]["password"].ToString())
                    {
                        ColSqlparram oColSqlPar = new ColSqlparram();
                        oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
                        oColSqlPar.Add("@location_id", location_id, SqlDbType.Int);

                        DataTable datatbl = GetDataTableSp("WS_get_Schedule_List", oColSqlPar, "WS_get_Schedule_List", getconstr(dt));
                        if (datatbl.Rows.Count > 0)
                        {
                            return DataTable(datatbl);
                        }
                        else
                        {
                            return "[{\"Message\":\"No Data Found.\"}]";
                        }
                    }
                    else
                    {
                        return "Invalid UserName or Password";
                    }
                }
                else
                {
                    oColSqlparram.Add("@User_Name", authUsername);
                    oColSqlparram.Add("@Password", authPassword);
                    oColSqlparram.Add("@IP_Address", "");
                    oColSqlparram.Add("@Access_Status", 0);
                    oColSqlparram.Add("@mac_id", "Web");
                    oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
                    ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
                    return "Invalid Webservice User Access...!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DataTable getStore(string storeName)
        {
            string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");
            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                ColSqlparram oColSqlPar = new ColSqlparram();
                oColSqlPar.Add("@store_name", storeName);
                DataTable dt = GetDataTableSp("WS_Get_Store", oColSqlPar, "getStore", con.ConnectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D,
                                                                     0x65, 0x64, 0x76, 0x65, 0x64, 0x65,
                                                                     0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D,
                                                                     0x65, 0x64, 0x76, 0x65, 0x64, 0x65,
                                                                     0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public string getconstr(DataTable dt)
        {
            try
            {
                string constr = "Data Source=" + dt.Rows[0]["db_server"] + ";" +
                                "Initial Catalog=" + dt.Rows[0]["db_name"] + ";" +
                                "User ID=" + dt.Rows[0]["db_username"] + ";" +
                                "Password=" + DecodeData(dt.Rows[0]["db_password"].ToString()) +
                                ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500";
                return constr;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }


}
