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
    public class NumberHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NumberHelper()
        {
        }

        #region 补足位数

        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }

        #endregion 补足位数

        #region 各进制数间转换

        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            if (!isBaseNumber(from))
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");

            if (!isBaseNumber(to))
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");

            int intValue = Convert.ToInt32(value, from);  //先转成10进制
            string result = Convert.ToString(intValue, to);  //再转成目标进制
            if (to == 2)
            {
                int resultLength = result.Length;  //获取二进制的长度
                switch (resultLength)
                {
                    case 7:
                        result = "0" + result;
                        break;

                    case 6:
                        result = "00" + result;
                        break;

                    case 5:
                        result = "000" + result;
                        break;

                    case 4:
                        result = "0000" + result;
                        break;

                    case 3:
                        result = "00000" + result;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否是  2 8 10 16
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <returns></returns>
        private static bool isBaseNumber(int baseNumber)
        {
            if (baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16)
                return true;
            return false;
        }

        #endregion 各进制数间转换

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="oText"></param>
        /// <returns></returns>
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