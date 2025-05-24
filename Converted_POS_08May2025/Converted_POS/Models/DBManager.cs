using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
 
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;


namespace Converted_POS.Models
{
    public class DBManager
    {

        //private static DBManager singleInstance;
        //private SqlConnection dbconnection;
        //private string _ConnectionString;

        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IConfiguration _configuration;
        //private readonly IHostingEnvironment _hostingEnvironment;
        //public DBManager(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _configuration = configuration;
        //    _hostingEnvironment = hostingEnvironment;
        //}

        //public string connection_data()
        //{
        //    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
        //    string str_connection = string.Empty;
        //    try
        //    {
        //        var session = _httpContextAccessor.HttpContext.Session;
        //        if (session.GetString("db_server") != null && session.GetString("db_server") != "")
        //        {
        //            if (str_connection == "")
        //            {
        //                if (_configuration["Windows_Authenticate"] == "Yes")
        //                {
        //                    str_connection = "Data Source=" + session.GetString("db_server") + ";" +
        //                                     "Initial Catalog=" + session.GetString("db_name") + ";" +
        //                                     "User ID=" + session.GetString("user_name") + ";" +
        //                                     "Password=" + session.GetString("password");
        //                }
        //                else
        //                {
        //                    str_connection = "Data Source=" + session.GetString("db_server") + ";" +
        //                                     "Initial Catalog=" + session.GetString("db_name") + ";" +
        //                                     "User ID=" + session.GetString("user_name") + ";" +
        //                                     "Password=" + session.GetString("password");
        //                }
        //            }

        //            string strMin_Pool_Size = _configuration["Min_Pool_Size"];
        //            str_connection += ";Min pool size=" + strMin_Pool_Size;

        //            string strMax_Pool_Size = _configuration["Max_Pool_Size"];
        //            str_connection += ";Max pool size=" + strMax_Pool_Size;

        //            string strConnect_Timeout = _configuration["Connect_Timeout"];
        //            str_connection += ";Connect Timeout=" + strConnect_Timeout;

        //            sb.ConnectionString = str_connection;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("DBManager:connection_data:" + ex.Message, _hostingEnvironment);
        //    }

        //    return sb.ConnectionString;
        //}
        //public SqlConnection getconnection()
        //{
        //    try
        //    {
        //        if (dbconnection == null)
        //        {
        //            dbconnection = new SqlConnection();
        //            dbconnection.ConnectionString = this.connection_data();
        //        }
        //        else if (dbconnection.ConnectionString == "")
        //        {
        //            dbconnection.ConnectionString = this.connection_data();
        //        }

        //        if (dbconnection.State == ConnectionState.Open)
        //        {
        //            dbconnection.Close();
        //        }

        //        if (dbconnection != null || dbconnection.State == ConnectionState.Closed)
        //        {
        //            dbconnection.Open();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached."))
        //        {
        //            SqlConnection.ClearAllPools();
        //        }

        //        dbconnection.Close();
        //        dbconnection.Dispose();
        //        LogHelper.Error("DataManager:" + ex.Message, _hostingEnvironment);
        //    }

        //    return dbconnection;
        //}

        //public string getConnString()
        //{
        //    try
        //    {
        //        return singleInstance._ConnectionString;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("DataManager:getConnString" + ex.Message, _hostingEnvironment);
        //        return null;
        //    }
        //}

        //public string get_first_ConnString()
        //{
        //    try
        //    {
        //        if (_ConnectionString == String.Empty || _ConnectionString == "Min Pool Size=10;Max Pool Size=100;Connect Timeout=0")
        //        {
        //            connection_data();
        //        }
        //        else
        //        {
        //            return _ConnectionString;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("DataManager:get_first_ConnString" + ex.Message, _hostingEnvironment);
        //    }

        //    return null;
        //}

        //public void Closeconnection()
        //{
        //    try
        //    {
        //        if (dbconnection != null && dbconnection.State == ConnectionState.Open)
        //        {
        //            dbconnection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dbconnection.Close();
        //        dbconnection.Dispose();
        //        LogHelper.Error("DBManager:Closeconnection:" + ex.Message, _hostingEnvironment);
        //    }
        //}

        //public void Reset_string()
        //{
        //    try
        //    {
        //        singleInstance._ConnectionString = String.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("DBManager:Reset_string:" + ex.Message, _hostingEnvironment);
        //    }
        //}

        //public void Destory()
        //{
        //    try
        //    {
        //        Closeconnection();
        //        dbconnection.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("DBManager:Destory:" + ex.Message, _hostingEnvironment);
        //    }
        //}



    }


}
