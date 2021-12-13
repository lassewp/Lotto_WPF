using System.IO;

namespace Lotto_test
{
    public class CreateTextFile
    {
        private LottoCoupon Coupon;
        private string Path;
        private string fileName = "kupon";

        public CreateTextFile(LottoCoupon coupon, string path)
        {
            this.Coupon = coupon;
            this.Path = path;
            if (Directory.Exists(Path) == false)
            {
                Directory.CreateDirectory(Path);
            }
        }

        public void ToTextFile()
        {
            // FILE CREATION AND INCREMENTING END NUMBER ON FILENAME IF IT ALREADY EXIST
            string tmpFileName = fileName; // tmp string from our default filename
            int increment = 0;
            
            Directory.SetCurrentDirectory(Path); // set the current directory we work in
            while (File.Exists(tmpFileName + ".txt")) // checks if file exist
            {
                increment++;
                if (File.Exists(tmpFileName + increment + ".txt") == false) // if our new end number does not exist, then use it as the new tmpFileName
                {
                    tmpFileName += increment; // the new tmpFileName, equal to itself plus the new ending number
                }
            }
            using (StreamWriter sw = new StreamWriter(tmpFileName + ".txt"))
            {
                sw.WriteLine("     Lotto " + Coupon.Timestamp + "\n");
                sw.WriteLine("          " + Coupon.Week);
                sw.WriteLine("        " + Coupon.Title + "\n");
                int count = 1;
                foreach (var row in Coupon.Rows)
                {
                    if (count < 10)
                        sw.Write(" " + count + ". ");
                    else
                        sw.Write(count + ". ");
                    count++;
                    foreach (var item in row)
                    {
                        if (item > 9)
                            sw.Write(item + " ");
                        else
                            sw.Write("0" + item + " ");
                    }
                    sw.WriteLine();
                }
                if(Coupon.JokerRows.Length > 0)
                {
                    sw.WriteLine("\n****** Joker tal *******");
                    foreach (var row in Coupon.JokerRows)
                    {
                        sw.Write("     ");
                        foreach (var item in row)
                        {
                            sw.Write(item + "  ");
                        }
                        sw.WriteLine();
                    }
                }
            } 
        }
    }
} 
// Lasse Willaume Pedersen
