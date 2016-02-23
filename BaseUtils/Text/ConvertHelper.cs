#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class ConvertHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConvertHelper()
        {
        }

        #region 将byte[]转换成int

        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }

        #endregion 将byte[]转换成int

        #region 将数据转换为整型

        /// <summary>
        /// 将数据转换为整型   转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32<T>(T data, int defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToInt32(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为整型   转换失败返回默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(string data, int defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            int temp = 0;
            if (Int32.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
            //if (data != null)
            //{
            //    string str = data.ToString();
            //    if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
            //    {
            //        if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
            //        {
            //            return Convert.ToInt32(str);
            //        }
            //    }
            //}
            //return defValue;
        }

        /// <summary>
        /// 将数据转换为整型  转换失败返回默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(object data, int defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToInt32(data);
            }
            catch
            {
                return defValue;
            }
        }

        #endregion 将数据转换为整型

        #region 将数据转换为布尔型

        /// <summary>
        /// 将数据转换为布尔类型  转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool ToBoolean<T>(T data, bool defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为布尔类型  转换失败返回 默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool ToBoolean(string data, bool defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            bool temp = false;
            if (bool.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为布尔类型  转换失败返回 默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool ToBoolean(object data, bool defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }

            //if (data != null)
            //{
            //    if (string.Compare(data.ToString(), "true", true) == 0)
            //    {
            //        return true;
            //    }
            //    else if (string.Compare(data.ToString(), "false", true) == 0)
            //    {
            //        return false;
            //    }
            //}
            //return defValue;
        }

        #endregion 将数据转换为布尔型

        #region 将数据转换为单精度浮点型

        /// <summary>
        /// 将数据转换为单精度浮点型  转换失败 返回默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static float ToFloat<T>(T data, float defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToSingle(data);
            }
            catch
            {
                return defValue;
            }
        }

        #endregion 将数据转换为单精度浮点型

        #region 将数据转换为双精度浮点型

        /// <summary>
        /// 将数据转换为双精度浮点型   转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位   转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, int decimals, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(object data, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(string data, double defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            double temp = 0;

            if (double.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(object data, int decimals, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
            ////如果为空则返回默认值
            //if (string.IsNullOrEmpty(data))
            //{
            //    return defValue;
            //}

            //double temp = 0;

            //if (double.TryParse(data, out temp))
            //{
            //    return Math.Round(temp, decimals);
            //}
            //else
            //{
            //    return defValue;
            //}
        }

        #endregion 将数据转换为双精度浮点型

        #region 将数据转换为指定类型

        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }

            Type type2 = data.GetType();
            if (targetType == type2)
            {
                return data;
            }
            if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(data.ToString()))
                {
                    return null;
                }
                return new Guid(data.ToString());
            }

            if (targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, data.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(targetType, data);
                }
            }

            if (targetType.IsGenericType)
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            return Convert.ChangeType(data, targetType);
        }

        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            if (data == null || Convert.IsDBNull(data))
                return default(T);

            object obj = ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }

        #endregion 将数据转换为指定类型

        #region 将数据转换Decimal

        /// <summary>
        /// 将数据转换为Decimal  转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal<T>(T data, Decimal defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDecimal(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为Decimal  转换失败返回 默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object data, Decimal defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDecimal(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为Decimal  转换失败返回 默认值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(string data, Decimal defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            decimal temp = 0;

            if (decimal.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        #endregion 将数据转换Decimal

        #region 将对象转换为数值(Long)类型

        /// <summary>
        /// 将对象转换为数值(Long)类型,转换失败返回-1。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Long数值。</returns>
        public static long ToLong(object obj)
        {
            try
            {
                return long.Parse(ToObjectString(obj));
            }
            catch
            { return -1L; }
        }

        /// <summary>
        /// 将对象转换为数值(Long)类型。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Long数值。</returns>
        public static long ToLong(object obj, long returnValue)
        {
            try
            {
                return long.Parse(ToObjectString(obj));
            }
            catch
            { return returnValue; }
        }

        #endregion 将对象转换为数值(Long)类型

        #region 将long型数值转换为Int32类型

        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int LongToInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            string strNum = objNum.ToString();
            if (IsNumberic(strNum))
            {
                if (strNum.ToString().Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
                return Int32.Parse(strNum);
            }
            else
            {
                return 0;
            }
        }

        #endregion 将long型数值转换为Int32类型

        #region 数据转换obj为null时返回空值

        /// <summary>
        /// 返回对象obj的String值,obj为null时返回空值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>字符串。</returns>
        public static string ToObjectString(object obj)
        {
            return null == obj ? String.Empty : obj.ToString();
        }

        #endregion 数据转换obj为null时返回空值

        private static bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}