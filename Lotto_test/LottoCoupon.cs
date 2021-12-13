namespace Lotto_test
{
    public class LottoCoupon
    {
        public string Timestamp;
        public string Week = "1-uge";
        public string Title = "LYN-LOTTO";
        public int[][] Rows;
        public int[][] JokerRows;

        public LottoCoupon(string timestamp, int[][] rowNum) // constructor without joker numbers (constructor overloading)
        {
            this.Timestamp = timestamp;
            this.Rows = rowNum;
            var empty = new int[0][];
            this.JokerRows = empty;
        }

        public LottoCoupon(string timestamp, int[][] rowNum, int[][] joker) // constructor with joker numbers (constructor overloading)
        {
            this.Timestamp = timestamp;
            this.Rows = rowNum;
            this.JokerRows = joker;
        }
    }
} 
// Lasse Willaume Pedersen
