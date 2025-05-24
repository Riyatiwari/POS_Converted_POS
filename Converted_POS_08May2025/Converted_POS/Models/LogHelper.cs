using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;

namespace Converted_POS.Models
{
    //public class LogHelper
    //{
    //    private readonly AppHelper _appHelper;

    //    public LogHelper(IHostingEnvironment hostingEnvironment)
    //    {
    //        _appHelper = new AppHelper(hostingEnvironment);
    //    }

    //    //AppHelper appHelper = new AppHelper();

    //    public static string logfile = @"\logs\DBLibrary.log";
    //    public static string debugfile = @"\logs\DBLibrary.dbg";
    //    public static string errlogfile = @"\logs\DBLibrary.err";

    //    public static void Write(string message, LogHelper logHelper)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        byte[] info = null;
    //        System.IO.FileStream fs = null;
    //        try
    //        {
    //            if (!System.IO.Directory.Exists(logHelper._appHelper.AppPath + @"\logs"))
    //                System.IO.Directory.CreateDirectory(logHelper._appHelper.AppPath + @"\logs");

    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            logfile = @"\Trace\Trace_" + datewisefile + ".log";
    //            fs = System.IO.File.Open(logHelper._appHelper.AppPath + logfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);

    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        finally
    //        {
    //        }
    //    }
    //    public static void Debug(string message, LogHelper logHelper)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        string name = logHelper._appHelper.AppPath;

    //        try
    //        {
    //            if (!System.IO.Directory.Exists(logHelper._appHelper.AppPath + @"\logs"))
    //                System.IO.Directory.CreateDirectory(logHelper._appHelper.AppPath + @"\logs");

    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            debugfile = @"\logs\Err_" + datewisefile + ".dbg";
    //            fs = System.IO.File.Open(logHelper._appHelper.AppPath + debugfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);

    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        finally
    //        {
    //        }
    //    }
    //    public static void Error(string message, IHostingEnvironment hostingEnvironment)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        try
    //        {
    //            AppHelper appHelper = new AppHelper(hostingEnvironment);
    //            if (!System.IO.Directory.Exists(appHelper.AppPath + "logs"))
    //                System.IO.Directory.CreateDirectory(appHelper.AppPath + "logs");

    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            errlogfile = @"\Err_" + datewisefile + ".err";
    //            fs = System.IO.File.Open(appHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);

    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        finally
    //        {
    //        }
    //    }
    //    public static void database_Error(string message, IHostingEnvironment hostingEnvironment)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        try
    //        {

    //            AppHelper appHelper = new AppHelper(hostingEnvironment);
    //            if (!System.IO.Directory.Exists(appHelper.AppPath + "logs"))
    //                System.IO.Directory.CreateDirectory(appHelper.AppPath + "logs");
    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            errlogfile = @"\Err_" + datewisefile + ".err";
    //            fs = System.IO.File.Open(appHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);

    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //        }
    //    }
    //    public static void User_Error(string message, IHostingEnvironment hostingEnvironment)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        string errlogfile = "";
    //        try
    //        {
    //            AppHelper appHelper = new AppHelper(hostingEnvironment);

    //            if (!System.IO.Directory.Exists(appHelper.AppPath + "logs"))
    //                System.IO.Directory.CreateDirectory(appHelper.AppPath + "logs");
    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            errlogfile = @"\Err_" + datewisefile + ".err";
    //            fs = System.IO.File.Open(appHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);
    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //        }
    //    }
    //    public static void Session_Log(string message, IHostingEnvironment hostingEnvironment)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        string errlogfile = "";

    //        try
    //        {

    //            AppHelper appHelper = new AppHelper(hostingEnvironment);

    //            if (!System.IO.Directory.Exists(appHelper.AppPath + "logs"))
    //                System.IO.Directory.CreateDirectory(appHelper.AppPath + "logs");
    //            datewisefile = ((dt.Year + "") + "-" + (dt.Month <= 9 ? "0" + dt.Month + "" : dt.Month + "") + "") + "-" + (dt.Day <= 9 ? "0" + dt.Day + "" : dt.Day + "");
    //            errlogfile = @"\Session_Time_" + datewisefile + ".txt";
    //            fs = System.IO.File.Open(appHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + Constants.vbLf);
    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //            fs.Close();
    //            fs = null;
    //            info = null;
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //        }
    //    }
    //    public static void Application_Error_Log(string message, IHostingEnvironment hostingEnvironment)
    //    {
    //        string datewisefile = "";
    //        DateTime dt = DateTime.Now;
    //        System.IO.FileStream fs = null;
    //        byte[] info = null;
    //        string errlogfile = "";

    //        try
    //        {
    //            AppHelper appHelper = new AppHelper(hostingEnvironment);

    //            if (!System.IO.Directory.Exists(appHelper.AppPath + "logs"))
    //                System.IO.Directory.CreateDirectory(appHelper.AppPath + "logs");

    //            datewisefile = $"{dt.Year}-{(dt.Month <= 9 ? "0" + dt.Month : dt.Month.ToString())}-{(dt.Day <= 9 ? "0" + dt.Day : dt.Day.ToString())}";
    //            errlogfile = @"\Application_Error_" + datewisefile + ".Err";

    //            fs = System.IO.File.Open(appHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
    //            info = new System.Text.UTF8Encoding(true).GetBytes($"{System.DateTime.Now.ToString()}: {message}{Constants.vbLf}");
    //            fs.Write(info, 0, info.Length);
    //            fs.Flush();
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle exception
    //        }
    //        finally
    //        {
    //            if (fs != null)
    //            {
    //                fs.Close();
    //                fs = null;
    //            }
    //        }
    //    }
    //}
}
