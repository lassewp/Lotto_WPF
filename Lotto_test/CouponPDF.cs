using System.IO;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Lotto_test
{
    public class CouponPDF
    {
        public string Path;
        private PdfDocument PdfDoc;
        public CouponPDF(string path)
        {
            this.Path = path;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.PdfDoc = new PdfDocument();
        }

        public void AddCoupons()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                PdfPage page = PdfDoc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
                XFont fontb = new XFont("Verdana", 14, XFontStyle.Regular);

                string timestamp = File.ReadLines(file.FullName).ElementAtOrDefault(1 - 1);
                string week = File.ReadLines(file.FullName).ElementAtOrDefault(3 - 1);
                string title = File.ReadLines(file.FullName).ElementAtOrDefault(4 - 1);

                List<string> rows = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    string row = File.ReadLines(file.FullName).ElementAtOrDefault((i+6) - 1);
                    rows.Add(row);
                }

                gfx.DrawString(timestamp, font, XBrushes.Black, new XRect(-10, 20, page.Width, page.Height), XStringFormat.TopCenter);
                gfx.DrawString(week, fontb, XBrushes.Black, new XRect(-20, 60, page.Width, page.Height), XStringFormat.TopCenter);
                gfx.DrawString(title, font, XBrushes.Black, new XRect(-15, 80, page.Width, page.Height), XStringFormat.TopCenter);

                int space = 120;
                foreach (var row in rows)
                {
                    gfx.DrawString(row, fontb, XBrushes.Black, new XRect(0, space, page.Width, page.Height), XStringFormat.TopCenter);
                    space += 20;
                }

                if (file.OpenText().ReadToEnd().Contains("*"))
                {
                    string jokerTitle = File.ReadLines(file.FullName).ElementAtOrDefault(17 - 1);
                    string jokerRow1 = File.ReadLines(file.FullName).ElementAtOrDefault(18 - 1);
                    string jokerRow2 = File.ReadLines(file.FullName).ElementAtOrDefault(19 - 1);
                    gfx.DrawString(jokerTitle, fontb, XBrushes.Black, new XRect(0, 340, page.Width, page.Height), XStringFormat.TopCenter);
                    gfx.DrawString(jokerRow1, fontb, XBrushes.Black, new XRect(25, 360, page.Width, page.Height), XStringFormat.TopCenter);
                    gfx.DrawString(jokerRow2, fontb, XBrushes.Black, new XRect(25, 380, page.Width, page.Height), XStringFormat.TopCenter);
                }
            }

            FileInfo[] filesB = directoryInfo.GetFiles("*.pdf");
            if (filesB != null)
            {
                foreach (FileInfo file in filesB)
                    file.Delete();
            }
            string datetime = DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("HH.mm.ss");
            PdfDoc.Save(Path + "Coupons_" + datetime + ".pdf");
        }

        public void OpenPDF()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            FileInfo[] files = directoryInfo.GetFiles("*.pdf");

            if(files != null)
            {
                foreach (FileInfo file in files)
                {
                    Process.Start(file.FullName);

                }
            }

        }
    }
}
