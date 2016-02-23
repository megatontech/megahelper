#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils.DataBase
{
    public sealed partial class MysqlHelper
    {
        #region 实例方法

        public T ExecuteObject<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteMysqlObject<T>(this.ConnectionString, commandText, parms);
        }

        public List<T> ExecuteObjects<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteMysqlObjects<T>(this.ConnectionString, commandText, parms);
        }

        #endregion 实例方法

        #region 静态方法

        public static T ExecuteMysqlObject<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();
            }
        }

        public static List<T> ExecuteMysqlObjects<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
            }
        }

        #endregion 静态方法
    }
}