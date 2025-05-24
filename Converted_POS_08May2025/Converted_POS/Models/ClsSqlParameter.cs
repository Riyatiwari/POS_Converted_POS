 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class ClsSqlParameter
    {
        private string _ParaName;
        private object _ParaValue;
        private SqlDbType _ParaType;
        private ParameterDirection _ParaDirection;
        private int _Parasize;

        public object ParaValue
        {
            get { return _ParaValue; }
            set { _ParaValue = value; }
        }

        public string ParaName
        {
            get { return _ParaName; }
            set { _ParaName = value; }
        }

        public SqlDbType ParaType
        {
            get { return _ParaType; }
            set { _ParaType = value; }
        }

        public ParameterDirection ParaDirection
        {
            get { return _ParaDirection; }
            set { _ParaDirection = value; }
        }

        public int ParaSize
        {
            get { return _Parasize; }
            set { _Parasize = value; }
        }

        public ClsSqlParameter(string sqlParaName, object sqlParaValue, SqlDbType sqlParaType = SqlDbType.VarChar, ParameterDirection sqlParaDirection = ParameterDirection.Input, int sqlParaSize = -1)
        {
            try
            {
                ParaName = sqlParaName;
                ParaValue = sqlParaValue;
                ParaType = sqlParaType;
                ParaDirection = sqlParaDirection;
                ParaSize = sqlParaSize;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ColSqlparram : System.Collections.ObjectModel.KeyedCollection<string, ClsSqlParameter>
    {
        private string _Skey;
        private string _SPName;

        public string sKey
        {
            get { return _Skey; }
            set { _Skey = value; }
        }

        public string SPName
        {
            get { return _SPName; }
            set { _SPName = value; }
        }

        protected override string GetKeyForItem(ClsSqlParameter item)
        {
            return item.ParaName;
        }

        public void Add(string sqlParaName, object sqlParaValue, SqlDbType sqlParaType = SqlDbType.VarChar, ParameterDirection sqlParaDirection = ParameterDirection.Input, int sqlParaSize = -1)
        {
            try
            {
                this.Add(new ClsSqlParameter(sqlParaName, sqlParaValue, sqlParaType, sqlParaDirection, sqlParaSize));
            }
            catch (Exception ex)
            {
                //LogHelper.Error("ClsDataccess:Public Add_Line_No102:" + ex.Message);
            }
        }
    }

    public class ColStoredProcedure : System.Collections.ObjectModel.KeyedCollection<string, ColSqlparram>
    {
        protected override string GetKeyForItem(ColSqlparram item)
        {
            return item.sKey;
        }
    }

    public class ClsDataccess : IDisposable
    {
        private int _intNoRecords;
        private string Strcon;
        private string Strcon_Max;
        private DBManager dbmgr = new DBManager();

        public int intNoRecords
        {
            get { return _intNoRecords; }
            set { _intNoRecords = value; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
