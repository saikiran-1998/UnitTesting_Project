using System;
using Xunit;

namespace ConsoleApp_Program.Test
{
    public class MathHelperTest
    {
        [Fact]
        public void IsEvenTest()
        {
            var calculator = new MathHelper();
            int x = 1;
            int y = 2;
            var xResult = calculator.IsEven(x);
            var yResult = calculator.IsEven(y);
            Assert.False(xResult);
            Assert.True(yResult);
        }

        [Theory]
        [InlineData(2, 1, 1)]
        //[InlineData(1, 1, 1)] // if we uncomment this test will fail
        [InlineData(1.4, 1, 0.4)]
        public void DifferenceTest(int x, int y, int expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Difference(x, y);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(2, 1, 3)]
        //[InlineData(1, 1, 1)] // if we uncomment this test will fail
        [InlineData(1.4, 1, 2.4)]
        public void AdditionTest(int x, int y, int expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Addition(x, y);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 6)]
        [InlineData(new int[] { -1, -2 }, -3)]
        public void SumTest(int[] intArray, int expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Sum(intArray);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 2)]
        [InlineData(new int[] { -2, -3 }, -2.5)]
        public void AverageTest(int[] intArray, double expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Average(intArray);
            Assert.Equal(expectedValue, result);
        }
        [Theory]
        //[InlineData(1, 2, 3)]
        [MemberData(nameof(MathHelper.Data), MemberType = typeof(MathHelper))]
        [MemberData(nameof(MathHelper.Data2), MemberType = typeof(MathHelper))]
        public void MemberData_AddTest(int x, int y, int expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Addition(x, y);
            Assert.Equal(expectedValue, result);
        }
        [Theory(Skip ="This will skip this test to run")]
        [ClassData(typeof(MathHelper))]
        //[ClassData(typeof(MathHelper))]
        //[MemberData(nameof(MathHelper.Data2), MemberType = typeof(MathHelper))]
        //[InlineData(2, 1, 3)]
        public void ClassData_AddTest(int x, int y, int expectedValue)
        {
            var calculator = new MathHelper();
            var result = calculator.Addition(x, y);
            Assert.Equal(expectedValue, result);
        }
    }
}
