using System;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApp_Program
{
    public class MathHelper : IEnumerable<object[]>
    {
        public bool IsEven(int x)
        {
            if (x % 2 == 0)
            {
                return true;
            }
            return false;
        }
        public int Difference(int x, int y)
        {
            return x - y;
        }
        public int Addition(int x, int y)
        {
            return x + y;
        }
        public int Sum(int[] numArray)
        {
            int sum = 0;
            foreach (var item in numArray)
            {
                sum = sum + item;
            }
            return sum;
        }
        public double Average(int[] numArray)
        {
            double sum = 0;
            foreach (var item in numArray)
            {
                sum = sum + item;
            }
            return sum / numArray.Length;
        }
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[]{1,2,3,},
                new object[]{-3,-4,-7 },
                new object[]{-2,2,0 },
                new object[]{int.MinValue,0,int.MinValue },
            };
        }
        public static IEnumerable<object[]> Data2()
        {
            return new List<object[]>
            {
                new object[]{2,3,5,},
                new object[]{-3,-5,-8 },
                new object[]{-2,1,-1 },
                new object[]{int.MaxValue,0,int.MaxValue },
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, 3, };
            yield return new object[] { -3, -4, -7 };
            yield return new object[] { -2, 2, 0 };
            yield return new object[] { int.MaxValue, 0, int.MaxValue };
        }
        public IEnumerator<object[]> GetEnumerator2()
        {
            yield return new object[] { 1, 2, 3, };
            yield return new object[] { -3, -4, -7 };
            yield return new object[] { -2, 2, 0 };
            yield return new object[] { int.MaxValue, 0, int.MaxValue };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
       
    }
}
