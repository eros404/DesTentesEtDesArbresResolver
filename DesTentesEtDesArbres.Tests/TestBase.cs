namespace DesTentesEtDesArbres.Tests
{
    public class TestBase
    {
        protected static void CompareTwoMatrix<T>(T[,] expected, T[,] actual, int height, int width)
        {
            for (uint i = 0; i < height; i++)
                for (uint y = 0; y < width; y++)
                    Assert.AreEqual(expected[i, y], actual[i, y]);
        }
    }
}
