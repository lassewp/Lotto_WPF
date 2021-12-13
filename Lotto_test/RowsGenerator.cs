using System;
using System.Collections.Generic;

namespace Lotto_test
{
    public class RowsGenerator
    {
        public int Rows = 10; // number of rows to generate (fixed for this assignment)
        public int NumOnRow = 7; // number of rows to generate (fixed for this assignment)
        private int JokerRows = 2; // number of rows to generate (fixed for this assignment)
        private int JokerNumOnRow = 7; // number of rows to generate (fixed for this assignment)

        public int[] GenerateRow() // generate single row, with no duplicates on that particular row
        {
            Random random = new Random(Guid.NewGuid().GetHashCode()); // random for finding numbers
            List<int> tmpNumList = new List<int>(); // list to store the numbers we've used, so we dont add duplicates

            int[] couponRow= new int[NumOnRow]; // our int array to fill and return

            int tmp; // tmp int to store random generated number and compare to our used numbers list
            for (int i = 0; i < NumOnRow; i++) // find x (NumOnRow) amount of numbers on a single row
            {
                tmp = random.Next(1, 36); // find a new random number
                while (tmpNumList.Contains(tmp) == true) // generate a new number, untill a number not used is found
                {
                    tmp = random.Next(1, 36); // find new random number
                }
                if (tmpNumList.Contains(tmp) != true) // add the number to array and list if not used
                {
                    couponRow[i] = tmp;
                    tmpNumList.Add(tmp);
                }
            }
            return couponRow; // return our filled row int array
        }

        public int[][] GenerateRowsForCoupon() // group rows and generate new row if duplicate rows in group
        {
            List<int[]> tmpRowsList = new List<int[]>(); // list to store the rows of numbers we've used, so we dont add duplicates
            int[][] allRows = new int[Rows][]; // the jagged array that we return as our final coupon
            for (int i = 0; i < Rows; i++)
            {
                int[] tmp = GenerateRow();
                Array.Sort(tmp);
                while (tmpRowsList.Contains(tmp) == true)
                {
                    tmp = GenerateRow();
                }
                if (tmpRowsList.Contains(tmp) != true)
                {
                    allRows[i] = tmp;
                    tmpRowsList.Add(tmp);
                }

            }
            return allRows;
        }

        public int[] JokerRowGenerator()
        {
            Random random2 = new Random(Guid.NewGuid().GetHashCode()); // random for finding numbers
            
            int[] jokerCouponRow = new int[JokerNumOnRow]; // our int array to fill and return

            int tmp;
            for (int i = 0; i < JokerNumOnRow; i++) // find x (JokerNumOnRow) amount of numbers on a single row
            {
                tmp = random2.Next(1, 9);
                jokerCouponRow[i] = tmp;
            }
            return jokerCouponRow; // return our filled row int array
        } // generate single joker rows - no duplicate filter

        public int[][] GenerateJokeRowsForCoupon()
        {
            List<int[]> tmpJokerRowsList = new List<int[]>(); // list to store the rows of numbers we've used, so we dont add duplicates
            int[][] allJokerRows = new int[JokerRows][]; // the jagged array that we return as our final coupon
            for (int i = 0; i < JokerRows; i++)
            {
                int[] tmp = JokerRowGenerator();
                Array.Sort(tmp);
                while (tmpJokerRowsList.Contains(tmp) == true)
                {
                    tmp = JokerRowGenerator();
                }
                if (tmpJokerRowsList.Contains(tmp) != true)
                {
                    allJokerRows[i] = tmp;
                    tmpJokerRowsList.Add(tmp);
                }
            }
            return allJokerRows;
        } // group joker rows and generate new row if duplicate row in group
    }
} 
// Lasse Willaume Pedersen
