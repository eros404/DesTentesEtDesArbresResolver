using DesTentesEtDesArbres.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.Tests
{
    [TestClass]
    public class ResolverTest : TestBase
    {
        private readonly TileState[,] _tilesStates = new TileState[7, 4]
        {
            { TileState.Unknown, TileState.Tree, TileState.Unknown, TileState.Unknown },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Unknown },
            { TileState.Tree, TileState.Tree, TileState.Unknown, TileState.Unknown },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Unknown },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Unknown },
            { TileState.Tree, TileState.Tree, TileState.Tree, TileState.Unknown },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Unknown }
        };
        private readonly TileState[,] _tilesStatesAfterInitialClean = new TileState[7, 4]
        {
            { TileState.Unknown, TileState.Tree, TileState.Unknown, TileState.Grass },
            { TileState.Unknown, TileState.Unknown, TileState.Grass, TileState.Grass },
            { TileState.Tree, TileState.Tree, TileState.Unknown, TileState.Grass },
            { TileState.Grass, TileState.Grass, TileState.Grass, TileState.Grass },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass },
            { TileState.Tree, TileState.Tree, TileState.Tree, TileState.Grass },
            { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass }
        };
        private readonly uint[] _numberOfTreesByRow = new uint[7]
        {
            1, 1, 1, 0, 1, 0, 2
        };
        private readonly uint[] _numberOfTreesByColumn = new uint[4]
        {
            2, 1, 3, 0
        };

        [TestMethod]
        public void InitialClean()
        {
            var playground = new Playground(_tilesStates, _numberOfTreesByRow, _numberOfTreesByColumn);
            var resolver = new Resolver(playground);
            resolver.InitialClean();
            var result = playground.GetTileStateMatrix();
            CompareTwoMatrix(_tilesStatesAfterInitialClean, result, playground.Height, playground.Width);
        }
        [TestMethod]
        public void Clean()
        {
            var playground = new Playground(_tilesStates, _numberOfTreesByRow, _numberOfTreesByColumn);
            var resolver = new Resolver(playground);
            resolver.Clean();
            var result = playground.GetTileStateMatrix();
            var expectedResult = new TileState[7, 4]
                {
                    { TileState.Unknown, TileState.Tree, TileState.Unknown, TileState.Grass },
                    { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass },
                    { TileState.Tree, TileState.Tree, TileState.Unknown, TileState.Grass },
                    { TileState.Grass, TileState.Grass, TileState.Grass, TileState.Grass },
                    { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass },
                    { TileState.Tree, TileState.Tree, TileState.Tree, TileState.Grass },
                    { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass }
                };
            CompareTwoMatrix(expectedResult, result, playground.Height, playground.Width);
        }
        [TestMethod]
        public void CompleteEvidentLines()
        {
            var playground = new Playground(_tilesStatesAfterInitialClean, _numberOfTreesByRow, _numberOfTreesByColumn);
            var resolver = new Resolver(playground);
            resolver.CompleteEvidentLines();
            var result = playground.GetTileStateMatrix();
            var expectedResult = new TileState[7, 4]
                {
                    { TileState.Unknown, TileState.Tree, TileState.Unknown, TileState.Grass },
                    { TileState.Unknown, TileState.Grass, TileState.Grass, TileState.Grass },
                    { TileState.Tree, TileState.Tree, TileState.Tent, TileState.Grass },
                    { TileState.Grass, TileState.Grass, TileState.Grass, TileState.Grass },
                    { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass },
                    { TileState.Tree, TileState.Tree, TileState.Tree, TileState.Grass },
                    { TileState.Unknown, TileState.Unknown, TileState.Unknown, TileState.Grass }
                };
            CompareTwoMatrix(expectedResult, result, playground.Height, playground.Width);
        }
    }
}
