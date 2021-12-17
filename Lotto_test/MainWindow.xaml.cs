using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Lotto_test
{
    
    public partial class MainWindow : Window
    {
        List<LottoCoupon> coupons = new List<LottoCoupon>(); // list to store generated coupons
        public string pathForCoupons = @"d:\lotto_kuponer\"; // change path for application to generate coupon to (txt and pdf)

        public MainWindow()
        {
            InitializeComponent();
            proBar.IsEnabled = false;
            
        }

        // START COUPON GENERATION UPON CLICK
        private void Print_Button_Click(object sender, RoutedEventArgs e)
        {
            // visuals
            printButton.IsEnabled = false;
            printButton.Visibility = Visibility.Hidden;
            doneLabel.Content = "";
            proBar.IsEnabled = true;
            jokerJa.IsEnabled = false;
            jokerNej.IsEnabled = false;
            kuponSlider.IsEnabled = false;
            var rowGenerator = new RowsGenerator();

            // LOOP TO GENERATE COUPONS
            for (int i = 0; i < kuponSlider.Value; i++) // amount of coupons to generate, depending on slider value
            {
                DateTime date = DateTime.Now; // gets current time
                string dateStr = date.ToString("dd-MM-yyyy"); // saves the DateTime to a string, with the formated wanted
                if (jokerJa.IsChecked == true) // cycle this if joker
                {
                    int[][] couponNum = rowGenerator.GenerateRowsForCoupon();               //
                    int[][] jokerCouponNum = rowGenerator.GenerateJokeRowsForCoupon();      // generate jagged arrays (coupon rows) that we need to construct a coupon
                    var coupon = new LottoCoupon(dateStr, couponNum, jokerCouponNum);       // generate the coupon from our LottoCoupon construtor
                    coupons.Add(coupon);                                                    // add coupon to our coupon list
                }
                else // cycle this if no joker
                {
                    int[][] couponNum = rowGenerator.GenerateRowsForCoupon();               // generate jagged arrays (coupon rows) that we need to construct a coupon
                    var coupon = new LottoCoupon(dateStr, couponNum);                       // generate the coupon from our second LottoCoupon construtor
                    coupons.Add(coupon);                                                    // add coupon to our coupon list

                }
                proBar.Value += 20;
            }

            // GENERATE A TEXT FILE FOR EACH GENERATED COUPON
            foreach (var coupon in coupons)
            {
                var toTxt = new CreateTextFile(coupon, pathForCoupons);
                toTxt.ToTextFile();
                proBar.Value += 20;
            }
            coupons.Clear();
            proBar.Value = 100;
            doneLabel.Content = "Færdig";
            myCoupons.IsEnabled = true;
        }

        // RESET FIELDS IN MAIN WINDOW
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            // reset visuals and reset fields
            printButton.IsEnabled = true;
            printButton.Visibility = Visibility.Visible;
            proBar.Value = 0;
            doneLabel.Content = "";
            kuponSlider.IsEnabled = true;
            kuponSlider.Value = 1;
            jokerJa.IsEnabled = true;
            jokerNej.IsEnabled = true;
            jokerNej.IsChecked = true;
        }

        // EXIT APPLICATION
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // OPEN COUPON VIEWER
        private void myCoupons_Click(object sender, RoutedEventArgs e)
        {
            CouponViewer viewerWindow = new CouponViewer(pathForCoupons); // initialize new CouponViewer
            viewerWindow.couponListbox.Items.Refresh();
            if (viewerWindow.couponListbox.Items.IndexOf(0).ToString() == "") // used to remove the first empty item in listbox, which appears for some reason?
            {
                viewerWindow.couponListbox.Items.Remove(0);
            }
            viewerWindow.Show(); // the show the initialized window
        }

        // SCROLL TEXT EFFECT
        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            LottoWinnerNumbers lottoWinnerNumbers = new LottoWinnerNumbers();
            tbmarquee.Text = "1. PRÆMIEPULJEN LØRDAG: " + lottoWinnerNumbers.WinAmount + "KR." + "                                        " + "SENESTE VINDERTAL: " + lottoWinnerNumbers.WinNum;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -tbmarquee.ActualWidth;
            doubleAnimation.To = canMain.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:10"));
            tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        }
    }
} 
// Lasse Willaume Pedersen
