using Converted_POS.Models;
 
using Microsoft.AspNetCore.Mvc;
 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Converted_POS
{
    
    internal class MyWCFService : IMyWCFService
    {
        public string SomeMethod(string input)
        {
            
            return $"Processed input: {input}";
        }

        public string MyMethod()
        {
             
            return "Hello from MyMethod!";
        }

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

            string str = JsonSerializer.Serialize(rows, new JsonSerializerOptions { MaxDepth = 32 });
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

            string str = JsonSerializer.Serialize(rows, new JsonSerializerOptions { MaxDepth = 32 });
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
                return vData == System.DBNull.Value ? "" : vData.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpGet("WebLogin")]
        public string Login( [FromQuery] string authUsername,[FromQuery] string authPassword,[FromQuery] string user, [FromQuery] string pass,[FromQuery] string IP_Address,[FromQuery] string mac_id,[FromQuery] string store_name)
        {
            try
            {
                ColSqlparram oColSqlparram = new ColSqlparram();
                DataTable dt = getStore(store_name);

                if (dt.Rows.Count > 0)
                {
                    // Validate against your DB
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
                        oColSqlPar.Add("@Login_URL", ConfigurationManager.AppSettings["Login_URL"]);

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
        public DataTable getStore(string storeName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POSControllerConnectionString"].ConnectionString);

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