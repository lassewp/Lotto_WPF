using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Collections.Generic;

namespace Lotto_test 
{ 
    public partial class CouponViewer : Window
    {
        public string Path;
        
        public CouponViewer(string path)
        {
            this.Path = path;
            InitializeComponent();

            // INITIALIZE SYSTEM IO CLASSES
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] Files = dinfo.GetFiles("*.txt"); // get all .txt files from folder

            // ADD COUPONS (EACH FILE IN FOLDER) TO LISTBOX WHEN WINDOW IS LOADED
            foreach (FileInfo file in Files)
            {
                StreamReader sr = new StreamReader(file.FullName);
                List<string> lines = new List<string>(); // list to store each line from the txt file
                while (!sr.EndOfStream) // add each line to the list untill we reach end of txt file
                {
                    lines.Add(sr.ReadLine());
                }

                // not quite indutry leading method to format the text to look nice in the listbox:
                if (file.OpenText().ReadToEnd().Contains("*")) // if coupon do contains joker
                {
                    couponListbox.Items.Add((lines[0] + "\n\n" + lines[1] + "    " + lines[2] + "\n" + " " + lines[3] + "\n" + lines[4] + "\n" + " " + lines[5] + "\n" + " " + lines[6] + "\n" + " " + lines[7] + "\n" + " " + lines[8] + "\n" + " " + lines[9] + "\n" + " " + lines[10] + "\n" + " " + lines[11] + "\n" + " " + lines[12] + "\n" + " " + lines[13] + "\n" + lines[14] + "\n\n" + lines[16] + "\n" + lines[17] + "\n" + lines[18] + "\n\n\n\n"));
                }
                else // if coupon do NOT contain joker
                {
                    couponListbox.Items.Add(("\n\n" + lines[0] + "\n\n" + lines[1] + "    " + lines[2] + "\n" + " " + lines[3] + "\n" + lines[4] + "\n" + " " + lines[5] + "\n" + " " + lines[6] + "\n" + " " + lines[7] + "\n" + " " + lines[8] + "\n" + " " + lines[9] + "\n" + " " + lines[10] + "\n" + " " + lines[11] + "\n" + " " + lines[12] + "\n" + " " + lines[13] + "\n" + lines[14] + "\n\n\n\n"));
                }
            }
        }

        private void clearFolderButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Path);
            FileInfo[] files = dinfo.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
            couponListbox.Items.Clear();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveTxtButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", Path);
        }

        private void savePdfButton_Click(object sender, RoutedEventArgs e)
        {
            var createPdf = new CouponPDF(Path);
            createPdf.AddCoupons();
            Process.Start("explorer.exe", Path);
        }
    }

}
