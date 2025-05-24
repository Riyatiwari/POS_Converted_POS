//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
 
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration; // (If needed; in Core you usually use IConfiguration)
//using System.Text;
//using System.Web.Script.Serialization; // You can install the System.Web.Script.Serialization NuGet package if needed
//using Microsoft.AspNetCore.Mvc;     // (Only if you want to decorate with attributes)
//using Newtonsoft.Json;

//namespace Converted_POS.WebService
//{
//    public class WS
//    {
//               // For JSON (if you prefer using Newtonsoft.Json)

//// You can remove any unused namespaces. In ASP.NET Core you usually do not use System.Web.
//namespace YourNamespace.Services
//    {
//        // This class is not an MVC controller—it’s a service that can be invoked by one.
//        public class POS_WebService : BaseClass
//        {
//            public string _ConnectionString = "";

//            // Converts a DataTable into a JSON string.
//            public string DataTableToJson(DataTable dt)
//            {
//                // You can use the JavaScriptSerializer or Newtonsoft.Json:
//                // Here we use JavaScriptSerializer similar to your VB code.
//                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
//                var rows = new List<Dictionary<string, object>>();
//                foreach (DataRow dr in dt.Rows)
//                {
//                    var row = new Dictionary<string, object>();
//                    foreach (DataColumn col in dt.Columns)
//                    {
//                        row.Add(col.ColumnName, dr[col].ToString() == "" ? "" : dr[col]);
//                    }
//                    rows.Add(row);
//                }
//                return serializer.Serialize(rows);
//            }

//            // Same as above, but encodes the value for the "username" column.
//            public string DataTableEncode(DataTable dt)
//            {
//                var serializer = new JavaScriptSerializer();
//                var rows = new List<Dictionary<string, object>>();
//                foreach (DataRow dr in dt.Rows)
//                {
//                    var row = new Dictionary<string, object>();
//                    foreach (DataColumn col in dt.Columns)
//                    {
//                        if (col.ColumnName.Equals("username", StringComparison.OrdinalIgnoreCase))
//                        {
//                            row.Add(col.ColumnName, dr[col].ToString() == "" ? "" : EncodeData(dr[col].ToString()));
//                        }
//                        else
//                        {
//                            row.Add(col.ColumnName, dr[col].ToString() == "" ? "" : dr[col]);
//                        }
//                    }
//                    rows.Add(row);
//                }
//                return serializer.Serialize(rows);
//            }

//            public string EncodeData(string str)
//            {
//                try
//                {
//                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
//                }
//                catch (Exception)
//                {
//                    return "";
//                }
//            }

//            public string DecodeData(string str)
//            {
//                try
//                {
//                    return Encoding.UTF8.GetString(Convert.FromBase64String(str));
//                }
//                catch (Exception)
//                {
//                    return "";
//                }
//            }

//            // Returns data from a stored procedure as a DataTable.
//            public DataTable GetDataTableSp(string spName, ColSqlparram oColSqlparram, string tableName = "", string constr = "")
//            {
//                DataTable dt = null;
//                SqlCommand com = new SqlCommand();
//                SqlDataAdapter adapter = new SqlDataAdapter();

//                try
//                {
//                    using (SqlConnection connection = new SqlConnection(constr))
//                    {
//                        connection.Open();
//                        com.CommandText = spName;
//                        com.CommandType = CommandType.StoredProcedure;
//                        com.Connection = connection;
//                        com.Parameters.Clear();
//                        com.CommandTimeout = 100;

//                        foreach (ClsSqlParameter param in oColSqlparram)
//                        {
//                            SqlParameter parameter = new SqlParameter
//                            {
//                                ParameterName = param.ParaName,
//                                SqlDbType = param.ParaType,
//                                Value = param.ParaValue,
//                                Direction = param.ParaDirection
//                            };
//                            com.Parameters.Add(parameter);
//                        }

//                        adapter = new SqlDataAdapter(com);
//                        dt = new DataTable();
//                        adapter.Fill(dt);
//                        if (!string.IsNullOrEmpty(tableName))
//                            dt.TableName = tableName;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    LogHelper.Error("POS_WebService:GetDataTableSp: " + ex.Message);
//                    throw; // rethrow the exception if needed
//                }
//                finally
//                {
//                    com.Parameters.Clear();
//                    com = null;
//                    adapter = null;
//                }
//                return dt;
//            }

//            // Executes a stored procedure with parameters.
//            public void ExecStoredProcedure(string procedureName, ColSqlparram oColSqlparram, string constr)
//            {
//                SqlCommand com = new SqlCommand();
//                using (SqlConnection connection = new SqlConnection(constr))
//                {
//                    SqlTransaction sqlTran = null;
//                    try
//                    {
//                        connection.Open();
//                        sqlTran = connection.BeginTransaction();
//                        com.CommandText = procedureName;
//                        com.CommandType = CommandType.StoredProcedure;
//                        com.Connection = connection;
//                        com.CommandTimeout = 100;
//                        com.Transaction = sqlTran;
//                        com.Parameters.Clear();

//                        for (int i = 0; i < oColSqlparram.Count; i++)
//                        {
//                            ClsSqlParameter param = oColSqlparram[i];
//                            SqlParameter parameter = new SqlParameter
//                            {
//                                ParameterName = param.ParaName,
//                                SqlDbType = param.ParaType
//                            };

//                            if (parameter.SqlDbType == SqlDbType.VarChar && param.ParaDirection == ParameterDirection.Input)
//                            {
//                                parameter.Size = string.IsNullOrEmpty(param.ParaValue.ToString()) ? 1 : param.ParaValue.ToString().Length;
//                            }
//                            else if (parameter.SqlDbType == SqlDbType.VarChar && param.ParaDirection != ParameterDirection.Input)
//                            {
//                                parameter.Size = string.IsNullOrEmpty(param.ParaValue.ToString()) ? 1000 : param.ParaValue.ToString().Length;
//                            }
//                            parameter.Value = param.ParaValue;
//                            parameter.Direction = param.ParaDirection;
//                            com.Parameters.Add(parameter);
//                        }

//                        com.ExecuteNonQuery();

//                        // Get output or input-output parameter values.
//                        for (int i = 0; i < oColSqlparram.Count; i++)
//                        {
//                            if (oColSqlparram[i].ParaDirection == ParameterDirection.InputOutput ||
//                                oColSqlparram[i].ParaDirection == ParameterDirection.Output)
//                            {
//                                oColSqlparram[i].ParaValue = CheckNull(com.Parameters[oColSqlparram[i].ParaName].Value);
//                            }
//                        }
//                        sqlTran.Commit();
//                    }
//                    catch (Exception ex)
//                    {
//                        sqlTran.Rollback();
//                        if (ex.Message.ToUpper().StartsWith("DELETE STATEMENT CONFLICTED") ||
//                            ex.Message.IndexOf("DELETE STATEMENT CONFLICTED", StringComparison.OrdinalIgnoreCase) > 0)
//                        {
//                            for (int i = 0; i < oColSqlparram.Count; i++)
//                            {
//                                if (oColSqlparram[i].ParaDirection == ParameterDirection.InputOutput ||
//                                    oColSqlparram[i].ParaDirection == ParameterDirection.Output)
//                                {
//                                    oColSqlparram[i].ParaValue = 0;
//                                }
//                            }
//                        }
//                        else
//                        {
//                            throw;
//                        }
//                    }
//                    finally
//                    {
//                        com = null;
//                    }
//                }
//            }

//            public string CheckNull(object vData)
//            {
//                try
//                {
//                    return (vData == DBNull.Value) ? "" : vData.ToString();
//                }
//                catch (Exception ex)
//                {
//                    LogHelper.Error("POS_WebService:CheckNull: " + ex.Message);
//                    return "";
//                }
//            }

//            // Example Web Method that logs in a user.
//            public string Login(string authUsername, string authPassword, string user, string pass, string IP_Address, string mac_id, string store_name)
//            {
//                try
//                {
//                    ColSqlparram oColSqlparram = new ColSqlparram();
//                    DataTable dt = getStore(store_name);

//                    if (dt.Rows.Count > 0)
//                    {
//                        if (authUsername == dt.Rows[0]["user_name"].ToString() &&
//                            authPassword == dt.Rows[0]["password"].ToString())
//                        {
//                            oColSqlparram.Add("@User_Name", user);
//                            oColSqlparram.Add("@Password", Encrypt(pass));
//                            oColSqlparram.Add("@IP_Address", IP_Address);
//                            oColSqlparram.Add("@Access_Status", 1);
//                            oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
//                            oColSqlparram.Add("@mac_id", mac_id);

//                            ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
//                            ColSqlparram oColSqlPar = new ColSqlparram();
//                            oColSqlPar.Add("@Username", DecodeData(user));
//                            oColSqlPar.Add("@Password", Encrypt(DecodeData(pass)));
//                            oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings["Login_URL"]);
//                            DataTable datatbl = GetDataTableSp("WS_do_Login", oColSqlPar, "M_Login", getconstr(dt));

//                            if (datatbl.Rows.Count > 0)
//                            {
//                                return DataTableToJson(datatbl);
//                            }
//                            else
//                            {
//                                return "[{\"Message\":\"No Data Found.\"}]";
//                            }
//                        }
//                        else
//                        {
//                            return "Invalid UserName or Password";
//                        }
//                    }
//                    else
//                    {
//                        oColSqlparram.Add("@User_Name", user);
//                        oColSqlparram.Add("@Password", pass);
//                        oColSqlparram.Add("@IP_Address", IP_Address);
//                        oColSqlparram.Add("@Access_Status", 0);
//                        oColSqlparram.Add("@mac_id", mac_id);
//                        oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
//                        ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
//                        return "Invalid Webservice User Access...!";
//                    }
//                }
//                catch (Exception ex)
//                {
//                    LogHelper.Error("POS_WebService:Login: " + ex.Message);
//                    return ex.Message;
//                }
//            }

//            // Example Web Method that returns a customer list.
//            public string Customer_List(string authUsername, string authPassword, int Cmp_id, string store_name)
//            {
//                try
//                {
//                    ColSqlparram oColSqlparram = new ColSqlparram();
//                    DataTable dt = getStore(store_name);

//                    if (dt.Rows.Count > 0)
//                    {
//                        if (authUsername == dt.Rows[0]["user_name"].ToString() &&
//                            authPassword == dt.Rows[0]["password"].ToString())
//                        {
//                            ColSqlparram oColSqlPar = new ColSqlparram();
//                            oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);

//                            DataTable datatbl = GetDataTableSp("WS_get_M_Customer", oColSqlPar, "WS_get_M_Customer", getconstr(dt));
//                            if (datatbl.Rows.Count > 0)
//                            {
//                                return DataTableToJson(datatbl);
//                            }
//                            else
//                            {
//                                return "[{\"Message\":\"No Data Found.\"}]";
//                            }
//                        }
//                        else
//                        {
//                            return "Invalid UserName or Password";
//                        }
//                    }
//                    else
//                    {
//                        oColSqlparram.Add("@User_Name", authUsername);
//                        oColSqlparram.Add("@Password", authPassword);
//                        oColSqlparram.Add("@IP_Address", "");
//                        oColSqlparram.Add("@Access_Status", 0);
//                        oColSqlparram.Add("@mac_id", "Web");
//                        oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
//                        ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
//                        return "Invalid Webservice User Access...!";
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return ex.Message;
//                }
//            }

//            // Example Web Method for kiosk settings.
//            public string KioskSettiong_List(string authUsername, string authPassword, int Cmp_id, string store_name, int TillID)
//            {
//                try
//                {
//                    ColSqlparram oColSqlparram = new ColSqlparram();
//                    DataTable dt = getStore(store_name);

//                    if (dt.Rows.Count > 0)
//                    {
//                        if (authUsername == dt.Rows[0]["user_name"].ToString() &&
//                            authPassword == dt.Rows[0]["password"].ToString())
//                        {
//                            ColSqlparram oColSqlPar = new ColSqlparram();
//                            oColSqlPar.Add("@cmp_id", Cmp_id, SqlDbType.Int);
//                            oColSqlPar.Add("@TillID", TillID, SqlDbType.Int);
//                            DataTable datatbl = GetDataTableSp("WS_get_KioskSettings", oColSqlPar, "WS_get_KioskSettings", getconstr(dt));
//                            if (datatbl.Rows.Count > 0)
//                            {
//                                return DataTableToJson(datatbl);
//                            }
//                            else
//                            {
//                                return "[{\"Message\":\"No Data Found.\"}]";
//                            }
//                        }
//                        else
//                        {
//                            return "Invalid UserName or Password";
//                        }
//                    }
//                    else
//                    {
//                        oColSqlparram.Add("@User_Name", authUsername);
//                        oColSqlparram.Add("@Password", authPassword);
//                        oColSqlparram.Add("@IP_Address", "");
//                        oColSqlparram.Add("@Access_Status", 0);
//                        oColSqlparram.Add("@mac_id", "Web");
//                        oColSqlparram.Add("@Created_Date", DateTime.Now, SqlDbType.DateTime);
//                        ExecStoredProcedure("W_Add_Log", oColSqlparram, getconstr(dt));
//                        return "Invalid Webservice User Access...!";
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return ex.Message;
//                }
//            }

//            // Implement additional methods (e.g., AutoSync_List, AutoSync_Update, Graphic_Table, Schedule_List,
//            // Customer_Master, Customer_Master_withCardNumber, Customer_Details, User_List) similarly.
//        }
//    }





//}
//}
