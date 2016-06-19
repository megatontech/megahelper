#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class EncodeHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EncodeHelper()
        {
        }

        #region md5

        #region MD5加密

        /// <summary>
        /// MD5 Encrypt
        /// </summary>
        /// <param name="strText">text</param>
        /// <returns>md5 Encrypt string</returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }

        public static string MD5EncryptHash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            //copy to a char array which can be passed to a String constructor
            Array.Copy(res, temp, res.Length);
            //return the result as a string
            return new String(temp);
        }

        public static string MD5EncryptHashHex(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);

            String returnThis = string.Empty;

            for (int i = 0; i < res.Length; i++)
            {
                returnThis += Uri.HexEscape((char)res[i]);
            }
            returnThis = returnThis.Replace("%", "");
            returnThis = returnThis.ToLower();

            return returnThis;
        }

        /// <summary>
        /// MD5 三次加密算法.计算过程: (QQ使用)
        /// 1. 验证码转为大写
        /// 2. 将密码使用这个方法进行三次加密后,与验证码进行叠加
        /// 3. 然后将叠加后的内容再次MD5一下,得到最终验证码的值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncyptMD5_3_16(string s)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes3)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }

        #endregion MD5加密

        #endregion md5

        #region DES

        #region DES对称加密解密

        public const string DEFAULT_ENCRYPT_KEY = "337845818";

        /// <summary>
        /// 使用默认加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string DesEncrypt(string strText)
        {
            try
            {
                return DesEncrypt(strText, DEFAULT_ENCRYPT_KEY);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Encrypt the string
        /// Attention:key must be 8 bits
        /// </summary>
        /// <param name="strText">string</param>
        /// <param name="strEncrKey">key</param>
        /// <returns></returns>
        public static string DesEncrypt(string strText, string strEncrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Encrypt files
        /// Attention:key must be 8 bits
        /// </summary>
        /// <param name="m_InFilePath">Encrypt file path</param>
        /// <param name="m_OutFilePath">output file</param>
        /// <param name="strEncrKey">key</param>
        public static void DesEncrypt(string m_InFilePath, string m_OutFilePath, string strEncrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            FileStream fin = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0; //This is the total number of bytes written.
            long totlen = fin.Length; //This is the total length of the input file.
            int len; //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }

        #endregion DES对称加密解密

        #endregion DES

        #region RSA

        #region RSA 的密钥产生

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }

        #endregion RSA 的密钥产生

        #region RSA的加密函数

        //##############################################################################
        //RSA 方式加密
        //说明KEY必须是XML的行式,返回的是字符串
        //在有一点需要说明！！该加密方式有 长度 限制的！！
        //##############################################################################

        //RSA的加密函数  string
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        //RSA的加密函数 byte[]
        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            CypherTextBArray = rsa.Encrypt(EncryptString, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        #endregion RSA的加密函数

        #endregion RSA

        #region AES

        #region 对称加密算法AES RijndaelManaged加密解密

        private static readonly string Default_AES_Key = "337845818";

        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79,
											 0x53,0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密结果字符串</returns>
        public static string AES_Encrypt(string encryptString)
        {
            return AES_Encrypt(encryptString, Default_AES_Key);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        public static string AES_Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0开始</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(string sourceString, int startIndex, int length, string tailString)
        {
            string myResult = sourceString;

            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }
                else
                {
                    return sourceString.Substring(startIndex,
                                                   ((length + startIndex) > sourceString.Length) ? (sourceString.Length - startIndex) : length);
                }
            }

            //中文字符，如"中国人民abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }
            byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

            //当字符串长度大于起始位置
            if (bytesSource.Length > startIndex)
            {
                int endIndex = bytesSource.Length;

                //当要截取的长度在字符串的有效长度范围内
                if (bytesSource.Length > (startIndex + length))
                {
                    endIndex = length + startIndex;
                }
                else
                {   //当不在有效范围内时,只取到字符串的结尾
                    length = bytesSource.Length - startIndex;
                    tailString = "";
                }

                int[] anResultFlag = new int[length];
                int nFlag = 0;
                //字节大于127为双字节字符
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (bytesSource[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3)
                        {
                            nFlag = 1;
                        }
                    }
                    else
                    {
                        nFlag = 0;
                    }
                    anResultFlag[i] = nFlag;
                }
                //最后一个字节为双字节字符的一半
                if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                {
                    length = length + 1;
                }

                byte[] bsResult = new byte[length];
                Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                myResult = Encoding.Default.GetString(bsResult);
                myResult = myResult + tailString;

                return myResult;
            }

            return string.Empty;
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static CryptoStream AES_EncryptStrream(FileStream fs, string decryptKey)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = Keys;

            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            CryptoStream cytptostreamEncr = new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
            return cytptostreamEncr;
        }

        /// <summary>
        /// 对指定文件加密
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <returns></returns>
        public static bool AES_EncryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "337845818";

                FileStream fr = new FileStream(InputFile, FileMode.Open);
                FileStream fren = new FileStream(OutputFile, FileMode.Create);
                CryptoStream Enfr = AES_EncryptStrream(fren, decryptKey);
                byte[] bytearrayinput = new byte[fr.Length];
                fr.Read(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Close();
                fr.Close();
                fren.Close();
            }
            catch
            {
                //文件异常
                return false;
            }
            return true;
        }

        #endregion 对称加密算法AES RijndaelManaged加密解密

        #endregion AES

        #region BASE64

        #region Base64加密

        protected string m_codeTable = @"ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";
        protected string m_pad = "v";
        protected Dictionary<int, char> m_t1 = new Dictionary<int, char>();
        protected Dictionary<char, int> m_t2 = new Dictionary<char, int>();

        /// <summary>
        /// 返回编码后的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Base64Encode(string source)
        {
            if (source == null || source == "")
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                byte[] tmp = System.Text.UTF8Encoding.UTF8.GetBytes(source);
                int remain = tmp.Length % 3;
                int patch = 3 - remain;
                if (remain != 0)
                {
                    Array.Resize(ref tmp, tmp.Length + patch);
                }
                int cnt = (int)Math.Ceiling(tmp.Length * 1.0 / 3);
                for (int i = 0; i < cnt; i++)
                {
                    sb.Append(this.EncodeUnit(tmp[i * 3], tmp[i * 3 + 1], tmp[i * 3 + 2]));
                }
                if (remain != 0)
                {
                    sb.Remove(sb.Length - patch, patch);
                    for (int i = 0; i < patch; i++)
                    {
                        sb.Append(m_pad);
                    }
                }
                return sb.ToString();
            }
        }

        protected string EncodeUnit(params byte[] unit)
        {
            int[] obj = new int[4];
            obj[0] = (unit[0] & 0xfc) >> 2;
            obj[1] = ((unit[0] & 0x03) << 4) + ((unit[1] & 0xf0) >> 4);
            obj[2] = ((unit[1] & 0x0f) << 2) + ((unit[2] & 0xc0) >> 6);
            obj[3] = unit[2] & 0x3f;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < obj.Length; i++)
            {
                sb.Append(this.GetEC((int)obj[i]));
            }
            return sb.ToString();
        }

        protected char GetEC(int code)
        {
            return m_t1[code];//m_codeTable[code];
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeBase64(string text)
        {
            //如果字符串为空，则返回
            if (StringHelper.IsNullOrEmpty<string>(text))
            {
                return "";
            }

            try
            {
                char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
											'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
											'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
											'8','9','+','/','='};
                byte empty = (byte)0;
                ArrayList byteMessage = new ArrayList(Encoding.Default.GetBytes(text));
                StringBuilder outmessage;
                int messageLen = byteMessage.Count;
                int page = messageLen / 3;
                int use = 0;
                if ((use = messageLen % 3) > 0)
                {
                    for (int i = 0; i < 3 - use; i++)
                        byteMessage.Add(empty);
                    page++;
                }
                outmessage = new System.Text.StringBuilder(page * 4);
                for (int i = 0; i < page; i++)
                {
                    byte[] instr = new byte[3];
                    instr[0] = (byte)byteMessage[i * 3];
                    instr[1] = (byte)byteMessage[i * 3 + 1];
                    instr[2] = (byte)byteMessage[i * 3 + 2];
                    int[] outstr = new int[4];
                    outstr[0] = instr[0] >> 2;
                    outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                    if (!instr[1].Equals(empty))
                        outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                    else
                        outstr[2] = 64;
                    if (!instr[2].Equals(empty))
                        outstr[3] = (instr[2] & 0x3f);
                    else
                        outstr[3] = 64;
                    outmessage.Append(Base64Code[outstr[0]]);
                    outmessage.Append(Base64Code[outstr[1]]);
                    outmessage.Append(Base64Code[outstr[2]]);
                    outmessage.Append(Base64Code[outstr[3]]);
                }
                return outmessage.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Base64加密

        #endregion BASE64

        #region hash

        /// <summary>
        /// 得到随机哈希加密字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSecurity()
        {
            string Security = HashEncoding(GetRandomValue());
            return Security;
        }

        /// <summary>
        /// 得到一个随机数值
        /// </summary>
        /// <returns></returns>
        public static string GetRandomValue()
        {
            Random Seed = new Random();
            string RandomVaule = Seed.Next(1, int.MaxValue).ToString();
            return RandomVaule;
        }

        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
        /// <param name="Security"></param>
        /// <returns></returns>
        public static string HashEncoding(string Security)
        {
            byte[] Value;
            UnicodeEncoding Code = new UnicodeEncoding();
            byte[] Message = Code.GetBytes(Security);
            SHA512Managed Arithmetic = new SHA512Managed();
            Value = Arithmetic.ComputeHash(Message);
            Security = "";
            foreach (byte o in Value)
            {
                Security += (int)o + "O";
            }
            return Security;
        }

        #endregion hash

        #region SHA256

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

        #endregion SHA256
    }
}