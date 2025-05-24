using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
 
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Converted_POS.Models
{





    //public class ClsSqlParameter
    //{
    //    private string _ParaName;
    //    private object _ParaValue;
    //    private SqlDbType _ParaType;
    //    private ParameterDirection _ParaDirection;
    //    private int _Parasize;
    //    public object ParaValue
    //    {
    //        get
    //        {
    //            return _ParaValue;
    //        }
    //        set
    //        {
    //            _ParaValue = value;
    //        }
    //    }
    //    public string ParaName
    //    {
    //        get
    //        {
    //            return _ParaName;
    //        }
    //        set
    //        {
    //            _ParaName = value;
    //        }
    //    }

    //    public SqlDbType ParaType
    //    {
    //        get
    //        {
    //            return _ParaType;
    //        }
    //        set
    //        {
    //            _ParaType = value;
    //        }
    //    }

    //    public ParameterDirection ParaDirection
    //    {
    //        get
    //        {
    //            return _ParaDirection;
    //        }
    //        set
    //        {
    //            _ParaDirection = value;
    //        }
    //    }

    //    public int ParaSize
    //    {
    //        get
    //        {
    //            return _Parasize;
    //        }
    //        set
    //        {
    //            _Parasize = value;
    //        }
    //    }

    //    public ClsSqlParameter(string sqlParaName, object SqlParaValue, SqlDbType SqlParaType = SqlDbType.VarChar, ParameterDirection SqlParaDirection = ParameterDirection.Input, int SqlParaSize = -1)
    //    {
    //        try
    //        {
    //            ParaName = sqlParaName;
    //            ParaValue = SqlParaValue;
    //            ParaType = SqlParaType;
    //            ParaDirection = SqlParaDirection;
    //            ParaSize = SqlParaSize;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}

    //public class ColSqlparram : System.Collections.ObjectModel.KeyedCollection<string, ClsSqlParameter>
    //{
    //    private string _Skey;
    //    private string _SPName;
    //    private IHostingEnvironment hostingEnvironment;

    //    public string sKey
    //    {
    //        get
    //        {
    //            return _Skey;
    //        }
    //        set
    //        {
    //            _Skey = value;
    //        }
    //    }
    //    public string SPName
    //    {
    //        get
    //        {
    //            return _SPName;
    //        }
    //        set
    //        {
    //            _SPName = value;
    //        }
    //    }

    //    protected override string GetKeyForItem(ClsSqlParameter item)
    //    {
    //        return item.ParaName;
    //    }
    //    public new void Add(string sqlParaName, object SqlParaValue, SqlDbType SqlParaType = SqlDbType.VarChar, ParameterDirection SqlParaDirection = ParameterDirection.Input, int SqlParaSize = -1)
    //    {
    //        try
    //        {
    //            this.Add(new ClsSqlParameter(sqlParaName, SqlParaValue, SqlParaType, SqlParaDirection, SqlParaSize));
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Public Overloads Sub Add_Line_No102:" + ex.Message, hostingEnvironment);
    //        }
    //    }
    //}



    //public class ColStoredProcedure : System.Collections.ObjectModel.KeyedCollection<string, ColSqlparram>
    //{
    //    protected override string GetKeyForItem(ColSqlparram item)
    //    {
    //        return item.sKey;
    //    }
    //}

    //public class ClsDataccess : System.IDisposable
    //{
    //    private int _intNoRecords;
    //    private string Strcon;
    //    private string Strcon_Max;
    //    private IHostingEnvironment hostingEnvironment;
    //    // Dim dbmgr As DBManager = dbmgr.GetSingleton()
    //    private DBManager dbmgr; //= new DBManager();


    //    public ClsDataccess(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    //    {
    //        dbmgr = new DBManager(httpContextAccessor, configuration, hostingEnvironment);
    //    }
    //    public int intNoRecords
    //    {
    //        get
    //        {
    //            return _intNoRecords;
    //        }
    //        set
    //        {
    //            _intNoRecords = value;
    //        }
    //    }
    //    public void Dispose()
    //    {
    //        try
    //        {
    //            GC.SuppressFinalize(this);
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataAccess.Dispose[Method]:" + ex.Message,hostingEnvironment);
    //        }
    //    }
    //    public void Destroy()
    //    {
    //        try
    //        {
    //            GC.SuppressFinalize(this);
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Destroy" + ex.Message, hostingEnvironment);
    //        }
    //    }
    //    public void directexecute(string strqrye)
    //    {
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
    //                com.CommandText = strqrye;
    //                com.CommandType = CommandType.Text;
    //                com.Connection = connection;
    //                com.ExecuteScalar();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:directexecute" + ex.Message, hostingEnvironment);
    //        }
    //    }
    //    public void ExecStoredProcedure_Login(string strProcedureName, ColSqlparram oColSqlparram)
    //    {
    //        int intParaCount;
    //        System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                com.CommandText = strProcedureName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                com.CommandTimeout = 0;
    //                com.Parameters.Clear();
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter();
    //                            parameter.ParameterName = withBlock1.ParaName;
    //                            parameter.SqlDbType = withBlock1.ParaType;
    //                            if (parameter.SqlDbType == SqlDbType.VarChar & withBlock1.ParaDirection == ParameterDirection.Input)
    //                                parameter.Size = IIf(Len(withBlock1.ParaValue) == 0, 1, Len(withBlock1.ParaValue));
    //                            else if (parameter.SqlDbType == SqlDbType.VarChar & withBlock1.ParaDirection != ParameterDirection.Input)
    //                                parameter.Size = IIf(Len(withBlock1.ParaValue) == 0, 1000, Len(withBlock1.ParaValue));
    //                            parameter.Value = withBlock1.ParaValue;
    //                            parameter.Direction = withBlock1.ParaDirection;
    //                            com.Parameters.Add(parameter);
    //                        }
    //                    }
    //                }
    //                com.ExecuteScalar();
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                                withBlock1.ParaValue = CheckNull(com.Parameters(withBlock1.ParaName).Value);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Strings.UCase(Strings.Left(ex.Message, 27)) == "DELETE STATEMENT CONFLICTED" | Strings.InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0)
    //            {
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                                withBlock1.ParaValue = 0;
    //                        }
    //                    }
    //                }
    //            }
    //            else if (ex.Message.Contains("Your Version Expired Please Contact Administrator"))
    //                throw ex;
    //            else
    //                LogHelper.Error("ClsDataccess:ExecStoredProcedure_Login:" + ex.Message);
    //        }
    //        finally
    //        {
    //            com = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //    }
    //    public void ExecStoredProcedure(string strProcedureName, ColSqlparram oColSqlparram)
    //    {
    //        int intParaCount;
    //        System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
    //        using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //        {
    //            System.Data.SqlClient.SqlTransaction sql_Tran=null;
    //            try
    //            {
    //                connection.Open();
    //                sql_Tran = connection.BeginTransaction();
    //                com.CommandText = strProcedureName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                // com.CommandTimeout = 0
    //                com.CommandTimeout = 600;
    //                com.Transaction = sql_Tran;
    //                com.Parameters.Clear();
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter();
    //                            parameter.ParameterName = withBlock1.ParaName;
    //                            parameter.SqlDbType = withBlock1.ParaType;
    //                            if (parameter.SqlDbType == SqlDbType.VarChar & withBlock1.ParaDirection == ParameterDirection.Input)
    //                                parameter.Size = IIf(Len(withBlock1.ParaValue) == 0, 1, Len(withBlock1.ParaValue));
    //                            else if (parameter.SqlDbType == SqlDbType.VarChar & withBlock1.ParaDirection != ParameterDirection.Input)
    //                                parameter.Size = IIf(Len(withBlock1.ParaValue) == 0, 1000, Len(withBlock1.ParaValue));
    //                            parameter.Value = withBlock1.ParaValue;
    //                            parameter.Direction = withBlock1.ParaDirection;
    //                            com.Parameters.Add(parameter);
    //                        }
    //                    }
    //                }
    //                // com.ResetCommandTimeout()
    //                com.ExecuteNonQuery();
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                                withBlock1.ParaValue = CheckNull(com.Parameters(withBlock1.ParaName).Value);
    //                        }
    //                    }
    //                }
    //                sql_Tran.Commit();
    //            }
    //            catch (Exception ex)
    //            {
    //                sql_Tran.Rollback();
    //                if (Strings.UCase(Strings.Left(ex.Message, 27)) == "DELETE STATEMENT CONFLICTED" | Strings.InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0)
    //                {
    //                    {
    //                        var withBlock = oColSqlparram;
    //                        for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                        {
    //                            {
    //                                var withBlock1 = withBlock.Item(intParaCount);
    //                                if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                                    withBlock1.ParaValue = 0;
    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                    throw ex;
    //            }
    //            finally
    //            {
    //                com = null/* TODO Change to default(_) if this is not a reference type */;
    //            }
    //        }
    //    }
    //    public void execBatchStoredProcedure(ColStoredProcedure oClscollstoredprocedure, bool blBreakonerror = true)
    //    {
    //        try
    //        {
    //            int intParaCount;

    //            for (intParaCount = 0; intParaCount <= oClscollstoredprocedure.Count - 1; intParaCount++)
    //            {
    //                try
    //                {
    //                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount));
    //                }
    //                catch (Exception ex)
    //                {
    //                    if (blBreakonerror)
    //                        throw ex;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public void Salary_execBatchStoredProcedure(ColStoredProcedure oClscollstoredprocedure, bool blBreakonerror = true, ref int flg = 0)
    //    {
    //        try
    //        {
    //            int intParaCount;

    //            for (intParaCount = 0; intParaCount <= oClscollstoredprocedure.Count - 1; intParaCount++)
    //            {
    //                try
    //                {
    //                    ExecStoredProcedure(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount));
    //                    flg = 1;
    //                }
    //                catch (Exception ex)
    //                {
    //                    if (blBreakonerror)
    //                        throw ex;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void execBatchStoredProcedure_New(ColStoredProcedure oClscollstoredprocedure, bool blBreakonerror = true)
    //    {
    //        using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //        {
    //            System.Data.SqlClient.SqlTransaction Sql_Tran ;
    //            try
    //            {
    //                int intParaCount;

    //                connection.Open();
    //                Sql_Tran = connection.BeginTransaction();
    //                for (intParaCount = 0; intParaCount <= oClscollstoredprocedure.Count - 1; intParaCount++)
    //                {
    //                    try
    //                    {
    //                        ExecStoredProcedure_New(oClscollstoredprocedure(intParaCount).SPName, oClscollstoredprocedure(intParaCount), oClscollstoredprocedure(0), connection, Sql_Tran);
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        if (blBreakonerror)
    //                            throw ex;
    //                    }
    //                }
    //                Sql_Tran.Commit();
    //            }
    //            catch (Exception ex)
    //            {
    //                if (Sql_Tran != null) 
    //                    Sql_Tran.Rollback();
    //                throw ex;
    //            }
    //            finally
    //            {
    //            }
    //        }
    //    }
    //    public void ExecStoredProcedure_New(string strProcedureName, ColSqlparram oColSqlparram, ColSqlparram oColSqlparram_Main, SqlConnection sqlconnection = null/* TODO Change to default(_) if this is not a reference type */, SqlClient.SqlTransaction Sql_Tran = null/* TODO Change to default(_) if this is not a reference type */)
    //    {
    //        int intParaCount;
    //        string strReturnValue;
    //        System.Data.SqlClient.SqlCommand com = new SqlClient.SqlCommand();
    //        try
    //        {
    //            com.CommandText = strProcedureName;
    //            com.CommandType = CommandType.StoredProcedure;
    //            com.Connection = sqlconnection;
    //            com.Transaction = Sql_Tran;
    //            com.CommandTimeout = 0;
    //            com.Parameters.Clear();
    //            strReturnValue = "";
    //            {
    //                var withBlock = oColSqlparram;
    //                for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                {
    //                    if (CheckValue_New(withBlock.Item(intParaCount).ParaValue, oColSqlparram_Main, ref strReturnValue))
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            System.Data.SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                            parameter.ParameterName = withBlock1.ParaName;
    //                            parameter.SqlDbType = withBlock1.ParaType;
    //                            if (parameter.SqlDbType == SqlDbType.VarChar)
    //                                IIf(Len(parameter.Size) == 0, 1, Len(withBlock1.ParaValue));
    //                            if (withBlock1.ParaType == SqlDbType.Int | withBlock1.ParaType == SqlDbType.Decimal | withBlock1.ParaType == SqlDbType.Float)
    //                                parameter.Value = Conversion.Val(strReturnValue);
    //                            else
    //                                parameter.Value = strReturnValue;
    //                            parameter.Direction = withBlock1.ParaDirection;
    //                            com.Parameters.Add(parameter);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        var withBlock1 = withBlock.Item(intParaCount);
    //                        SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                        parameter.ParameterName = withBlock1.ParaName;
    //                        parameter.SqlDbType = withBlock1.ParaType;
    //                        if (parameter.SqlDbType == SqlDbType.VarChar)
    //                            IIf(Len(parameter.Size) == 0, 1, Len(withBlock1.ParaValue));
    //                        parameter.Value = withBlock1.ParaValue;
    //                        parameter.Direction = withBlock1.ParaDirection;
    //                        com.Parameters.Add(parameter);
    //                    }
    //                }
    //            }
    //            com.ExecuteNonQuery();
    //            {
    //                var withBlock = oColSqlparram;
    //                for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                {
    //                    {
    //                        var withBlock1 = withBlock.Item(intParaCount);
    //                        if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                            withBlock1.ParaValue = CheckNull(com.Parameters(withBlock1.ParaName).Value);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Strings.UCase(Strings.Left(ex.Message, 27)) == "DELETE STATEMENT CONFLICTED" | Strings.InStr(ex.Message, "DELETE STATEMENT CONFLICTED", CompareMethod.Text) > 0)
    //            {
    //                {
    //                    var withBlock = oColSqlparram;
    //                    for (intParaCount = 0; intParaCount <= withBlock.Count - 1; intParaCount++)
    //                    {
    //                        {
    //                            var withBlock1 = withBlock.Item(intParaCount);
    //                            if (withBlock1.ParaDirection == ParameterDirection.InputOutput | withBlock1.ParaDirection == ParameterDirection.Output)
    //                                withBlock1.ParaValue = 0;
    //                        }
    //                    }
    //                }
    //                Information.Err.Raise(Information.Err.Number, Information.Err.Source, Information.Err.Description);
    //            }
    //            else
    //                throw ex;
    //        }

    //        finally
    //        {
    //            com = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //    }
    //    private bool CheckValue_New(string strValue, ColSqlparram oColSqlparram, ref string strReturnValue)
    //    {
    //        try
    //        {
    //            string strFromStoredProcedure;
    //            string strFromParameter;
    //            int intStoredProcedurePosition;
    //            int intParameterPosition;
    //            CheckValue_New = false;

    //            if (Strings.UCase(Strings.Left(strValue, 11)) == Strings.UCase("::GetFrom::"))
    //            {
    //                intStoredProcedurePosition = Strings.InStr(Strings.Len("::GetFrom::") + 1, strValue, ",") - 2;
    //                strFromStoredProcedure = Strings.Mid(strValue, Strings.Len("::GetFrom::(") + 1, intStoredProcedurePosition - Strings.Len("::GetFrom::"));

    //                intParameterPosition = Strings.InStr(intStoredProcedurePosition + 1, strValue, ")") - 2 - (intStoredProcedurePosition + 1);
    //                strFromParameter = Strings.Mid(strValue, intStoredProcedurePosition + 3, intParameterPosition);

    //                strReturnValue = oColSqlparram.Item(strFromParameter).ParaValue;

    //                CheckValue_New = true;
    //            }
    //            else
    //                return;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:CheckValue_New:" + ex.Message,hostingEnvironment);
    //        }
    //    }

    //    public string CheckNull(object vData)
    //    {
    //        try
    //        {
    //            CheckNull = Interaction.IIf(Information.IsDBNull(vData), "", vData);
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:CheckNull:" + ex.Message);
    //        }
    //    }

    //    public void Bind_Ddl_Month(DropDownList ddl_name)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.Add("Month_ID");
    //            dt.Columns.Add("Month");
    //            int i;
    //            for (i = 1; i <= 12; i++)
    //            {
    //                DataRow row;
    //                row = dt.NewRow;
    //                row("Month_ID") = i;
    //                switch (i)
    //                {
    //                    case 1:
    //                        {
    //                            row("Month") = "January";
    //                            break;
    //                        }

    //                    case 2:
    //                        {
    //                            row("Month") = "February";
    //                            break;
    //                        }

    //                    case 3:
    //                        {
    //                            row("Month") = "March";
    //                            break;
    //                        }

    //                    case 4:
    //                        {
    //                            row("Month") = "April";
    //                            break;
    //                        }

    //                    case 5:
    //                        {
    //                            row("Month") = "May";
    //                            break;
    //                        }

    //                    case 6:
    //                        {
    //                            row("Month") = "June";
    //                            break;
    //                        }

    //                    case 7:
    //                        {
    //                            row("Month") = "July";
    //                            break;
    //                        }

    //                    case 8:
    //                        {
    //                            row("Month") = "August";
    //                            break;
    //                        }

    //                    case 9:
    //                        {
    //                            row("Month") = "September";
    //                            break;
    //                        }

    //                    case 10:
    //                        {
    //                            row("Month") = "October";
    //                            break;
    //                        }

    //                    case 11:
    //                        {
    //                            row("Month") = "November";
    //                            break;
    //                        }

    //                    case 12:
    //                        {
    //                            row("Month") = "December";
    //                            break;
    //                        }
    //                }
    //                dt.Rows.Add(row);
    //            }
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = "Month";
    //            ddl_name.DataValueField = "Month_ID";
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_Month:" + ex.Message);
    //        }
    //    }

    //    public void Bind_Ddl_Month_Experience(DropDownList ddl_name)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.Add("Month_ID");
    //            dt.Columns.Add("Month");
    //            int i;
    //            for (i = 0; i <= 11; i++)
    //            {
    //                DataRow row;
    //                row = dt.NewRow;
    //                row("Month_ID") = i;
    //                row("Month") = i;
    //                dt.Rows.Add(row);
    //            }
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = "Month";
    //            ddl_name.DataValueField = "Month_ID";
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_Month_Experience:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl_Year_Experience(DropDownList ddl_name)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.Add("Year_ID");
    //            dt.Columns.Add("Year");
    //            int i;
    //            for (i = 0; i <= 20; i++)
    //            {
    //                DataRow row;
    //                row = dt.NewRow;
    //                row("Year_ID") = i;
    //                row("Year") = i;
    //                dt.Rows.Add(row);
    //            }
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = "Year";
    //            ddl_name.DataValueField = "Year_ID";
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_Year_Experience:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl_year(DropDownList ddl_name)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.Add("year_ID");
    //            dt.Columns.Add("year");
    //            int i;
    //            for (i = 2005; i <= DateTime.Today.Year + 2; i++)
    //            {
    //                DataRow row;
    //                row = dt.NewRow;
    //                row("year_ID") = i;
    //                row("year") = i;
    //                dt.Rows.Add(row);
    //            }
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = "year";
    //            ddl_name.DataValueField = "year_ID";
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_year:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl_FYear(DropDownList ddl_name)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.Add("year_ID");
    //            dt.Columns.Add("year");
    //            int i;
    //            for (i = 2005; i <= DateTime.Today.Year + 2; i++)
    //            {
    //                DataRow row;
    //                row = dt.NewRow;
    //                row("year_ID") = i;
    //                row("year") = i + "-" + i + 1;
    //                dt.Rows.Add(row);
    //            }
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = "year";
    //            ddl_name.DataValueField = "year_ID";
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_FYear:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl(object ddl_name, string sqlquery, string Textfield, string Valuefield)
    //    {
    //        try
    //        {
    //            Bind_Ddl(ddl_name, Getdatatable(sqlquery), Textfield, Valuefield);
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl(DropDownList ddl_name, SqlDataReader sdr)
    //    {
    //        try
    //        {
    //            AddDefaultField(ddl_name);
    //            ddl_name.DataSource = sdr;
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddlwithout(DropDownList ddl_name, SqlDataReader sdr)
    //    {
    //        try
    //        {
    //            ddl_name.Items.Clear();
    //            ddl_name.AppendDataBoundItems = true;
    //            ddl_name.DataSource = sdr;
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddlwithout:" + ex.Message);
    //        }
    //    }
    //    public void Bind_CheckboxList(CheckBoxList chkl, SqlDataReader sdr)
    //    {
    //        try
    //        {
    //            chkl.DataSource = sdr;
    //            chkl.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_CheckboxList:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Listbox(ListBox chkl, string Query, string Textfield, string Valuefield)
    //    {
    //        System.Data.DataTable dt;
    //        try
    //        {
    //            dt = Getdatatable(Query);
    //            chkl.DataSource = dt;
    //            chkl.DataTextField = Textfield;
    //            chkl.DataValueField = Valuefield;
    //            chkl.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Listbox:" + ex.Message);
    //        }
    //    }
    //    public void BindCheckbox(CheckBoxList lst_name, string Query, string Textfield, string Valuefield)
    //    {
    //        System.Data.DataTable dt;
    //        try
    //        {
    //            dt = Getdatatable(Query);
    //            lst_name.DataSource = dt;
    //            lst_name.DataTextField = Textfield;
    //            lst_name.DataValueField = Valuefield;
    //            lst_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:BindCheckbox:" + ex.Message);
    //        }
    //    }
    //    public void AddDefaultField(DropDownList ddl_name, string strText = "Select")
    //    {
    //        try
    //        {
    //            ddl_name.Items.Clear();
    //            ddl_name.Items.Insert(0, "------" + strText + "------");
    //            ddl_name.Items(0).Value = 0;
    //            ddl_name.SelectedValue = 0;
    //            ddl_name.AppendDataBoundItems = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:AddDefaultField:" + ex.Message);
    //        }
    //    }
    //    public void AddDefaultFieldSearch(DropDownList ddl_name, string strText = "Select")
    //    {
    //        try
    //        {
    //            ddl_name.Items.Clear();
    //            ddl_name.Items.Insert(0, "---" + strText + "---");
    //            ddl_name.Items(0).Value = 0;
    //            ddl_name.SelectedValue = 0;
    //            ddl_name.AppendDataBoundItems = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:AddDefaultFieldSearch:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl(object ddl_name, DataTable dt, string Textfield, string Valuefield)
    //    {
    //        try
    //        {
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = Textfield;
    //            ddl_name.DataValueField = Valuefield;
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl:" + ex.Message);
    //        }
    //    }
    //    public void Bind_Ddl_New(object ddl_name, DataTable dt, string Textfield, string Valuefield)
    //    {
    //        try
    //        {
    //            ddl_name.DataSource = dt;
    //            ddl_name.DataTextField = Textfield;
    //            ddl_name.DataValueField = Valuefield;
    //            ddl_name.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_Ddl_New:" + ex.Message);
    //        }
    //    }
    //    public SqlDataReader Getdatareader(string sqlquery, SqlConnection SqlConnection = null/* TODO Change to default(_) if this is not a reference type */)
    //    {
    //        SqlDataReader glb_dr;
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        try
    //        {
    //            glb_com.CommandText = sqlquery;
    //            glb_com.CommandType = CommandType.Text;
    //            glb_com.Connection = SqlConnection;
    //            glb_dr = glb_com.ExecuteReader();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Getdatareader:" + ex.Message);
    //        }
    //        return glb_dr;
    //    }
    //    public DataTable Bind_DDL_Month(object oGridView, string sqlquery)
    //    {
    //        DataTable dttable = new DataTable();
    //        try
    //        {
    //            dttable = Getdatatable(sqlquery);
    //            oGridView.DataSource = dttable;
    //            oGridView.datamember = "table";
    //            oGridView.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Bind_DDL_Month:" + ex.Message);
    //        }
    //        return dttable;
    //    }
    //    public void BindRepeater(Repeater rp, SqlDataReader sdr)
    //    {
    //        try
    //        {
    //            rp.DataSource = sdr;
    //            rp.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:BindRepeater:" + ex.Message);
    //        }
    //    }
    //    public void BindDatalist(DataList ds, SqlDataReader sdr)
    //    {
    //        try
    //        {
    //            ds.DataSource = sdr;
    //            ds.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:BindDatalist:" + ex.Message);
    //        }
    //    }
    //    public DataTable Getdatatable(string Sqlquery)
    //    {
    //        DataTable glb_dt = new DataTable();
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        SqlClient.SqlDataAdapter glb_adp = new SqlClient.SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandTimeout = 0;
    //                glb_com.CommandText = Sqlquery;
    //                glb_com.CommandType = CommandType.Text;
    //                glb_com.Connection = connection;
    //                glb_adp.SelectCommand = glb_com;
    //                glb_dt.Clear();
    //                glb_adp.Fill(glb_dt);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Getdatatable:" + ex.Message);
    //        }
    //        finally
    //        {
    //            glb_com = null/* TODO Change to default(_) if this is not a reference type */;
    //            glb_adp = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return glb_dt;
    //    }
    //    public DataView GetDataview(string Sqlquery)
    //    {
    //        DataSet Glb_ds = new DataSet();
    //        DataView glb_dt = new DataView();
    //        try
    //        {
    //            SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //            SqlClient.SqlDataAdapter glb_adp = new SqlClient.SqlDataAdapter();

    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandText = Sqlquery;
    //                glb_com.CommandType = CommandType.Text;
    //                glb_com.Connection = connection;
    //                glb_adp.SelectCommand = glb_com;
    //                glb_adp.Fill(Glb_ds);
    //                glb_dt = Glb_ds.Tables(0).DefaultView;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:GetDataview:" + ex.Message);
    //        }
    //        finally
    //        {
    //        }
    //        return glb_dt;
    //    }
    //    public DataTable Getdatatable_param(string Sqlquery, SqlClient.SqlParameter Param)
    //    {
    //        DataTable glb_dt = new DataTable();
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        SqlClient.SqlDataAdapter glb_adp = new SqlClient.SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandText = Sqlquery;
    //                glb_com.Parameters.Add(Param);
    //                glb_com.CommandType = CommandType.Text;
    //                glb_com.Connection = connection;
    //                glb_adp.SelectCommand = glb_com;
    //                glb_dt.Clear();
    //                glb_adp.Fill(glb_dt);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Getdatatable_param:" + ex.Message);
    //        }
    //        finally
    //        {
    //            glb_com = null/* TODO Change to default(_) if this is not a reference type */;
    //            glb_adp = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return glb_dt;
    //    }
    //    public DataSet Getdataset(string Sqlquery, string TblAlias)
    //    {
    //        DataSet glb_dst = new DataSet();
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        SqlClient.SqlDataAdapter glb_adp = new SqlClient.SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandText = Sqlquery;
    //                glb_com.CommandType = CommandType.Text;
    //                glb_com.Connection = connection;
    //                glb_adp.SelectCommand = glb_com;
    //                glb_dst.Clear();
    //                glb_adp.Fill(glb_dst, TblAlias);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:Getdataset:" + ex.Message);
    //        }
    //        finally
    //        {
    //            glb_adp = null/* TODO Change to default(_) if this is not a reference type */;
    //            glb_com = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return glb_dst;
    //    }
    //    public DataList GetdataList(string Sqlquery, DataList glb_dst)
    //    {
    //        try
    //        {
    //            SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();

    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandText = Sqlquery;
    //                glb_com.CommandType = CommandType.Text;
    //                glb_com.Connection = connection;
    //                glb_dst.DataSource = glb_com.ExecuteReader();
    //                glb_dst.DataBind();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:GetdataList:" + ex.Message);
    //        }
    //        finally
    //        {
    //        }
    //    }
    //    public System.Data.DataSet GetdatasetSp(string SPName, ColSqlparram oColSqlparram, string TblAlias)
    //    {
    //        DataSet sdr = null/* TODO Change to default(_) if this is not a reference type */;
    //        SqlCommand com = new SqlCommand();
    //        SqlDataAdapter SpAdepter = new SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                com.CommandText = SPName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                com.CommandTimeout = 600;
    //                com.Parameters.Clear();

    //                foreach (ClsSqlParameter oClsSqlParameter in oColSqlparram)
    //                {
    //                    SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                    parameter.ParameterName = oClsSqlParameter.ParaName;
    //                    parameter.SqlDbType = oClsSqlParameter.ParaType;
    //                    parameter.Value = oClsSqlParameter.ParaValue;
    //                    parameter.Direction = oClsSqlParameter.ParaDirection;
    //                    com.Parameters.Add(parameter);
    //                }
    //                SpAdepter = new SqlDataAdapter(com);
    //                sdr = new System.Data.DataSet();
    //                SpAdepter.Fill(sdr);
    //                return sdr;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            LogHelper.Error("ClsDataccess:GetdatasetSp:" + ex.Message);
    //        }
    //        finally
    //        {
    //            com.Parameters.Clear();
    //            com = null/* TODO Change to default(_) if this is not a reference type */;
    //            SpAdepter = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return sdr;
    //    }

    //    public System.Data.DataTable GetdataTableSp(string SPName, ColSqlparram oColSqlparram, string strTableName = "")
    //    {
    //        DataTable sdr = null/* TODO Change to default(_) if this is not a reference type */;
    //        SqlCommand com = new SqlCommand();
    //        SqlDataAdapter SpAdepter = new SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                com.CommandText = SPName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                com.Parameters.Clear();
    //                com.CommandTimeout = 600;

    //                foreach (ClsSqlParameter oClsSqlParameter in oColSqlparram)
    //                {
    //                    SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                    parameter.ParameterName = oClsSqlParameter.ParaName;
    //                    parameter.SqlDbType = oClsSqlParameter.ParaType;
    //                    parameter.Value = oClsSqlParameter.ParaValue;
    //                    parameter.Direction = oClsSqlParameter.ParaDirection;
    //                    com.Parameters.Add(parameter);
    //                }

    //                SpAdepter = new SqlDataAdapter(com);
    //                sdr = new System.Data.DataTable();
    //                SpAdepter.Fill(sdr);
    //                if (strTableName != "")
    //                    sdr.TableName = strTableName;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            LogHelper.Error("ClsDataccess:GetdataTableSp:" + ex.Message);
    //        }
    //        finally
    //        {
    //            com.Parameters.Clear();
    //            com = null/* TODO Change to default(_) if this is not a reference type */;
    //            SpAdepter = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return sdr;
    //    }

    //    public System.Data.DataTable GetdataTableSp1(string SPName, string strTableName = "")
    //    {
    //        DataTable sdr = null/* TODO Change to default(_) if this is not a reference type */;
    //        SqlCommand com = new SqlCommand();
    //        SqlDataAdapter SpAdepter = new SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                com.CommandText = SPName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                com.Parameters.Clear();
    //                com.CommandTimeout = 0;

    //                SpAdepter = new SqlDataAdapter(com);
    //                sdr = new System.Data.DataTable();
    //                SpAdepter.Fill(sdr);
    //                if (strTableName != "")
    //                    sdr.TableName = strTableName;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            LogHelper.Error("ClsDataccess:GetdataTableSp:" + ex.Message);
    //        }
    //        finally
    //        {
    //            com.Parameters.Clear();
    //            com = null/* TODO Change to default(_) if this is not a reference type */;
    //            SpAdepter = null/* TODO Change to default(_) if this is not a reference type */;
    //        }
    //        return sdr;
    //    }
    //    public System.Data.DataView GetdataView(string SPName, ColSqlparram oColSqlparram, string strTableName = "")
    //    {
    //        DataTable sdr = null/* TODO Change to default(_) if this is not a reference type */;
    //        DataView sdr1 = null/* TODO Change to default(_) if this is not a reference type */;
    //        SqlCommand com = new SqlCommand();
    //        SqlDataAdapter SpAdepter = new SqlDataAdapter();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                com.CommandText = SPName;
    //                com.CommandType = CommandType.StoredProcedure;
    //                com.Connection = connection;
    //                com.Parameters.Clear();
    //                com.CommandTimeout = 0;
    //                foreach (ClsSqlParameter oClsSqlParameter in oColSqlparram)
    //                {
    //                    SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                    parameter.ParameterName = oClsSqlParameter.ParaName;
    //                    parameter.SqlDbType = oClsSqlParameter.ParaType;
    //                    parameter.Value = oClsSqlParameter.ParaValue;
    //                    parameter.Direction = oClsSqlParameter.ParaDirection;
    //                    com.Parameters.Add(parameter);
    //                }
    //                SpAdepter = new SqlDataAdapter(com);
    //                sdr = new System.Data.DataTable();
    //                SpAdepter.Fill(sdr);
    //                sdr1 = sdr.DefaultView;
    //                if (strTableName != "")
    //                    sdr.TableName = strTableName;
    //            }
    //            return sdr1;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            LogHelper.Error("ClsDataccess:GetdataView:" + ex.Message);
    //        }
    //        finally
    //        {
    //            com.Parameters.Clear();
    //        }
    //        return sdr1;
    //    }
    //    public string getparentcategory(int categoryid)
    //    {
    //        try
    //        {
    //            ArrayList paramarr = new ArrayList();
    //            paramarr.Add(new object[] { "@category_string", SqlDbType.VarChar, "", ParameterDirection.InputOutput });
    //            paramarr.Add(new object[] { "@category_id", SqlDbType.Int, categoryid, ParameterDirection.Input });
    //            return getstring_fromprocedure("Sp_Get_Parent_Category", paramarr);
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:getparentcategory:" + ex.Message);
    //        }
    //    }
    //    public string getstring_fromprocedure(string SPName, ArrayList SPParameter)
    //    {
    //        string output_value = string.Empty;
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();
    //                glb_com.CommandText = SPName;
    //                glb_com.CommandType = CommandType.StoredProcedure;
    //                glb_com.Connection = connection;
    //                glb_com.Parameters.Clear();
    //                object param;
    //                foreach (var param in SPParameter)
    //                {
    //                    SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                    parameter.ParameterName = System.Convert.ToString(param(0));
    //                    parameter.SqlDbType = (SqlDbType)param(1);
    //                    parameter.Value = param(2);
    //                    parameter.Direction = param(3);
    //                    if (parameter.SqlDbType == SqlDbType.VarChar & parameter.Direction == ParameterDirection.InputOutput)
    //                        parameter.Size = 150;
    //                    glb_com.Parameters.Add(parameter);
    //                }
    //                glb_com.ExecuteScalar();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:getstring_fromprocedure:" + ex.Message);
    //        }
    //        finally
    //        {
    //        }
    //        return glb_com.Parameters(0).Value;
    //    }
    //    public DataTable gettable_fromprocedure(string SPName, ArrayList SPParameter = null/* TODO Change to default(_) if this is not a reference type */)
    //    {
    //        string output_value = string.Empty;
    //        DataTable glb_dt = new DataTable();
    //        SqlClient.SqlCommand glb_com = new SqlClient.SqlCommand();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(dbmgr.get_first_ConnString))
    //            {
    //                connection.Open();

    //                glb_com.CommandText = SPName;
    //                glb_com.CommandType = CommandType.StoredProcedure;
    //                glb_com.Connection = connection;
    //                glb_com.Parameters.Clear();
    //                object param;
    //                if (!SPParameter == null)
    //                {
    //                    foreach (var param in SPParameter)
    //                    {
    //                        SqlClient.SqlParameter parameter = new SqlClient.SqlParameter();
    //                        parameter.ParameterName = System.Convert.ToString(param(0));
    //                        parameter.SqlDbType = (SqlDbType)param(1);
    //                        parameter.Value = param(2);
    //                        parameter.Direction = param(3);
    //                        if (parameter.SqlDbType == SqlDbType.VarChar & parameter.Direction == ParameterDirection.InputOutput)
    //                            parameter.Size = 150;
    //                        glb_com.Parameters.Add(parameter);
    //                    }
    //                }
    //                glb_dt.Load(glb_com.ExecuteReader());
    //            }
    //            return glb_dt;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:gettable_fromprocedure:" + ex.Message);
    //        }
    //        finally
    //        {
    //        }
    //        return glb_dt;
    //    }

    //    public SqlDataReader GetdatareaderSp(string SPName, ColSqlparram oColSqlparram, SqlConnection sqlconn = null/* TODO Change to default(_) if this is not a reference type */)
    //    {
    //        SqlDataReader sdr = null/* TODO Change to default(_) if this is not a reference type */;
    //        SqlCommand com = new SqlCommand();
    //        try
    //        {
    //            com.CommandText = SPName;
    //            com.CommandType = CommandType.StoredProcedure;
    //            if (!sqlconn == null)
    //                com.Connection = sqlconn;
    //            else
    //            {
    //            }
    //            com.Parameters.Clear();
    //            foreach (ClsSqlParameter oClsSqlParameter in oColSqlparram)
    //            {
    //                SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter();
    //                parameter.ParameterName = oClsSqlParameter.ParaName;
    //                parameter.SqlDbType = oClsSqlParameter.ParaType;
    //                parameter.Value = oClsSqlParameter.ParaValue;
    //                parameter.Direction = oClsSqlParameter.ParaDirection;
    //                com.Parameters.Add(parameter);
    //            }
    //            sdr = com.ExecuteReader();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.Error("ClsDataccess:GetdatareaderSp:" + ex.Message,hostingEnvironment);
    //        }
    //        finally
    //        {
    //            com.Parameters.Clear();
    //        }
    //        return sdr;
    //    }
    //}

}