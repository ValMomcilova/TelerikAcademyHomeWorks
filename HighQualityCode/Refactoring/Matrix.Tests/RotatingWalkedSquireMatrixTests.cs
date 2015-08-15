namespace Matrix.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RotatingWalkedSquireMatrixTests
    {
        [TestMethod]
        public void TestRotatingWalkedSquire3x3()
        {
            int[,] expectedMatix = new int[,] 
            {
                { 1, 7, 8 },
                { 6, 2, 9 },
                { 5, 4, 3 }
            };

            int n = 3;
            RotatingWalkedSquireMatrix matrix = new RotatingWalkedSquireMatrix(n);

            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Assert.AreEqual(
                        expectedMatix[i, j],
                        matrix[i, j],
                        string.Format("Matrix element {0}, {1} is not the same as expected", i, j));
                }
            }
        }

        [TestMethod]
        public void TestRotatingWalkedSquire6x6()
        {
            int[,] expectedMatix = new int[,] 
            {
                { 1, 16, 17, 18, 19, 20 },
                { 15, 2, 27, 28, 29, 21 },
                { 14, 31, 3, 26, 30, 22 },
                { 13, 36, 32, 4, 25, 23 },
                { 12, 35, 34, 33, 5, 24 },
                { 11, 10, 9, 8, 7, 6 }
            };

            int n = 6;
            RotatingWalkedSquireMatrix matrix = new RotatingWalkedSquireMatrix(n);

            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Assert.AreEqual(
                        expectedMatix[i, j],
                        matrix[i, j],
                        string.Format("Matrix element {0}, {1} is not the same as expected", i, j));
                }
            }
        }
    }
}
