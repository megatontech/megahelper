#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils.DataBase
{
    public sealed partial class SqliteHelper
    {
        #region 实例方法

        public T ExecuteObject<T>(string commandText, params SQLiteParameter[] parms)
        {
            return ExecuteSqliteObject<T>(commandText, parms);
        }

        public List<T> ExecuteObjects<T>(string commandText, params SQLiteParameter[] parms)
        {
            return ExecuteSqliteObjects<T>(commandText, parms);
        }

        #endregion 实例方法

        #region 静态方法

        public static T ExecuteSqliteObject<T>(string commandText, params SQLiteParameter[] parms)
        {
            using (SQLiteDataReader reader = ExecuteDataReader(ConnectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();
            }
        }

        public static List<T> ExecuteSqliteObjects<T>(string commandText, params SQLiteParameter[] parms)
        {
            using (SQLiteDataReader reader = ExecuteDataReader(ConnectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
            }
        }

        #endregion 静态方法
    }
}