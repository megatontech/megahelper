#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class CaptchaEncodeHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CaptchaEncodeHelper()
        {
        }

        public MemoryStream MakeCaptchaImage(string sourceFile, int width, int height, string text)
        {
            MemoryStream ms = new MemoryStream();

            try
            {
                //原图
                Bitmap sImage = new Bitmap(sourceFile);
                //验证码图
                Bitmap wImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics wg = Graphics.FromImage(wImage);
                wg.Clear(Color.White);
                wg.DrawString(text, new Font("Verdana", 18), new SolidBrush(Color.Black), 0, 0);
                wg.Save();
                Graphics g = Graphics.FromImage(sImage);
                int x; //临时变量
                int y; //监时变量
                int w = width; //验证图的宽度
                int h = height; //验证图的高度
                int al; //alpha
                int rl; //Red
                int gl; //Green
                int bl; //Blue
                //开始绘图
                for (x = 1; x < w; x++)
                {
                    for (y = 1; y < h; y++)
                    {
                        al = wImage.GetPixel(x, y).A;
                        rl = wImage.GetPixel(x, y).R;
                        gl = wImage.GetPixel(x, y).G;
                        bl = wImage.GetPixel(x, y).B;
                        al = 70;

                        if (rl - 50 > 0)
                            rl -= 50;
                        if (gl - 50 > 0)
                            gl -= 50;
                        if (bl - 50 > 0)
                            bl -= 50;
                        g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(al, rl, gl, bl))), x, y, 1, 1);
                    }
                }
                g.Save();
                sImage.Save(ms, ImageFormat.Jpeg);
                wg.Dispose();
                g.Dispose();
                sImage.Dispose();
                wImage.Dispose();
            }
            catch (Exception e)
            {
            }
            return ms;
        }
    }
}