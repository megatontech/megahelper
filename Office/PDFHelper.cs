#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using iTextSharp.text;
using iTextSharp.text.io;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class PDFHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PDFHelper()
        {
        }

        #region itextsharp

        public void GeneratePDF()
        {
            byte[] pdfbyte = null;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4, 25, 25, 25, 25))
                {
                    StreamUtil.AddToResourceSearch("iTextAsian.dll");
                    StreamUtil.AddToResourceSearch("iTextAsianCmaps.dll");
                    BaseFont bfChinese = BaseFont.CreateFont("c:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese);
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        PdfPTable table = new PdfPTable(1);
                        PdfPCell cell = new PdfPCell(new Paragraph(fontEncoding("设备详细信息"), font));
                        cell.Colspan = 1;
                        table.AddCell(cell);
                        table.AddCell(new Paragraph(fontEncoding("序号:" + "1"), font));
                        table.AddCell(new Paragraph(fontEncoding("设备类型:" + "2"), font));
                        doc.Add(table);
                        doc.Close();
                    }
                    var bytes = ms.ToArray();
                    pdfbyte = bytes;
                }
            }
            var fileStream = BytesToStream(pdfbyte);
            var mimeType = "application/pdf";
            var fileDownloadName = "download.pdf";
            //return File(fileStream, mimeType, fileDownloadName);
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public string fontEncoding(string oriString)
        {
            var bytes = System.Text.Encoding.Unicode.GetBytes(oriString);
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        #endregion itextsharp
    }
}