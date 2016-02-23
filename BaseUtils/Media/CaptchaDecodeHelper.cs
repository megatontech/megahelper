#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class CaptchaDecodeHelper
    {
        [DllImport("AspriseOCR.dll", EntryPoint = "OCR", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCR(string file, int type);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpart", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr OCRpart(string file, int type, int startX, int startY, int width, int height);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRBarCodes", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr OCRBarCodes(string file, int type);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpartBarCodes", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr OCRpartBarCodes(string file, int type, int startX, int startY, int width, int height);

        /// <summary>
        /// 构造函数
        /// </summary>
        public CaptchaDecodeHelper()
        {
        }

        /// <summary>
        /// 解读验证码为字母字符串，输入路径，图片类型，宽/高
        /// </summary>
        /// <param name="CaptchaPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string DecodeCaptchaChar(string CaptchaPath, int width, int height)
        {
            string result = "";
            result = Marshal.PtrToStringAnsi(OCRpart(CaptchaPath, -1, 0, 0, width, height));
            string newStr = "";
            for (int i = 0; i < result.Length; i++)
            {
                int tmp = (int)result[i];
                if ((tmp >= 65 && tmp <= 90) || (tmp >= 97 && tmp <= 122))
                {
                    newStr += result[i];
                }
            }

            return newStr;
        }

        /// <summary>
        /// 解读验证码为数字字符串，输入路径，图片类型，宽/高
        /// </summary>
        /// <param name="CaptchaPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string DecodeCaptchaInt(string CaptchaPath, int width, int height)
        {
            string result = "";
            result = Marshal.PtrToStringAnsi(OCRpart(CaptchaPath, -1, 0, 0, width, height));
            string newStr = "";
            for (int i = 0; i < result.Length; i++)
            {
                int tmp = (int)result[i];
                if ((tmp >= 48 && tmp <= 57))
                {
                    newStr += result[i];
                }
            }

            return newStr;
        }

        /// <summary>
        /// 解读验证码为数字字母混合字符串，输入路径，图片类型，宽/高
        /// </summary>
        /// <param name="CaptchaPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string DecodeCaptchaIntChar(string CaptchaPath, int width, int height)
        {
            string result = "";
            result = Marshal.PtrToStringAnsi(OCRpart(CaptchaPath, -1, 0, 0, width, height));
            string newStr = "";
            for (int i = 0; i < result.Length; i++)
            {
                int tmp = (int)result[i];
                if ((tmp >= 65 && tmp <= 90) || (tmp >= 97 && tmp <= 122) || (tmp >= 48 && tmp <= 57))
                {
                    newStr += result[i];
                }
            }

            return newStr;
        }
    }
}