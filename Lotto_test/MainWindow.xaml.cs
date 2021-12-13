using System;
using System.Collections.Generic;
using System.Windows;

namespace Lotto_test
{
    
    public partial class MainWindow : Window
    {
        List<LottoCoupon> coupons = new List<LottoCoupon>();
        public string pathForCoupons = @"d:\lotto_kuponer\";

        public MainWindow()
        {
            InitializeComponent();
            proBar.IsEnabled = false;
        }

        private void Print_Button_Click(object sender, RoutedEventArgs e)
        {
            printButton.IsEnabled = false;
            doneLabel.Content = "";
            proBar.IsEnabled = true;
            jokerJa.IsEnabled = false;
            jokerNej.IsEnabled = false;
            kuponSlider.IsEnabled = false;
            var rowGenerator = new RowsGenerator();

            // GENERATE COUPONS UPON BUTTON CLICK
            for (int i = 0; i < kuponSlider.Value; i++)
            {
                DateTime date = DateTime.Now;
                string dateStr = date.ToString("dd-MM-yyyy");
                if (jokerJa.IsChecked == true)
                {
                    int[][] couponNum = rowGenerator.GenerateRowsForCoupon();
                    int[][] jokerCouponNum = rowGenerator.GenerateJokeRowsForCoupon();
                    var coupon = new LottoCoupon(dateStr, couponNum, jokerCouponNum);
                    coupons.Add(coupon);
                }
                else
                {
                    int[][] couponNum = rowGenerator.GenerateRowsForCoupon();
                    var coupon = new LottoCoupon(dateStr, couponNum);
                    coupons.Add(coupon);
                    
                }
                proBar.Value += 20;
            }
            foreach (var coupon in coupons)
            {
                var toTxt = new CreateTextFile(coupon, pathForCoupons);
                toTxt.ToTextFile();
                proBar.Value += 20;
            }
            coupons.Clear();
            proBar.Value = 100;
            doneLabel.Content = "Færdig";
            showCouponButton.IsEnabled = true;
        }

        private void showCouponButton_Click(object sender, RoutedEventArgs e)
        {
            CouponViewer viewerWindow = new CouponViewer(pathForCoupons);
            viewerWindow.couponListbox.Items.Refresh();
            if (viewerWindow.couponListbox.Items.IndexOf(0).ToString() == "")
            {
                viewerWindow.couponListbox.Items.Remove(0);
            }
            viewerWindow.Show();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            printButton.IsEnabled = true;
            proBar.Value = 0;
            doneLabel.Content = "";
            kuponSlider.IsEnabled = true;
            kuponSlider.Value = 1;
            jokerJa.IsEnabled = true;
            jokerNej.IsEnabled = true;
            jokerNej.IsChecked = true;
        }
    }
} 
// Lasse Willaume Pedersen
