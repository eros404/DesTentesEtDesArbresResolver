using DesTentesEtDesArbres.Core;

namespace DesTentesEtDesArbres.Tests
{
    [TestClass]
    public class PlaygroundTest
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
        private readonly uint[] _numberOfTreesByRow = new uint[7]
        {
            1, 1, 1, 0, 1, 0, 2
        };
        private readonly uint[] _numberOfTreesByColumn = new uint[4]
        {
            2, 1, 3, 0
        };

        private Playground Playground { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Playground = new Playground(_tilesStates, _numberOfTreesByRow, _numberOfTreesByColumn);
        }
        [TestMethod]
        public void PlaygroundInitialization_HeightAndWidth()
        {
            Assert.AreEqual(_tilesStates.GetLength(0), Playground.Height);
            Assert.AreEqual(_tilesStates.GetLength(1), Playground.Width);
            for (uint i = 0; i < _tilesStates.GetLength(0); i++)
                for (uint y = 0; y < _tilesStates.GetLength(1); y++)
                    Assert.AreEqual(_tilesStates[i, y], Playground.Tiles[i, y].State);
        }
        [TestMethod]
        public void PlaygroundInitialization_TitleStateMatrix()
        {
            var matrix = Playground.GetTileStateMatrix();
            for (uint i = 0; i < _tilesStates.GetLength(0); i++)
                for (uint y = 0; y < _tilesStates.GetLength(1); y++)
                    Assert.AreEqual(_tilesStates[i, y],matrix[i, y]);
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(6)]
        public void PlaygroundInitialization_Rows(int rowIndex)
        {
            var row = Playground.Rows[rowIndex];
            Assert.AreEqual(_numberOfTreesByRow[rowIndex], row.ExpectedNumberOfTents);
            for (int i = 0; i < _tilesStates.GetLength(1); i++)
                Assert.AreEqual(_tilesStates[rowIndex, i], row.Tiles[i].State);
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        [DataRow(3)]
        public void PlaygroundInitialization_Columns(int columnIndex)
        {
            var column = Playground.Columns[columnIndex];
            Assert.AreEqual(_numberOfTreesByColumn[columnIndex], column.ExpectedNumberOfTents);
            for (int i = 0; i < _tilesStates.GetLength(0); i++)
                Assert.AreEqual(_tilesStates[i, columnIndex], column.Tiles[i].State);
        }
        [TestMethod]
        public void TileNeighbors_CenteredTile()
        {
            var tile = Playground.Tiles[3, 1];
            var neighbors = tile.GetNeighbors();
            Assert.AreEqual(4, neighbors.Count);
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[4, 1]));
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[2, 1]));
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[3, 2]));
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[3, 0]));
        }
        [TestMethod]
        public void TileNeighbors_OriginTile()
        {
            var tile = Playground.Tiles[0, 0];
            var neighbors = tile.GetNeighbors();
            Assert.AreEqual(2, neighbors.Count);
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[0, 1]));
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[1, 0]));
        }
        [TestMethod]
        public void TileNeighbors_LastTile()
        {
            var tile = Playground.Tiles[6, 3];
            var neighbors = tile.GetNeighbors();
            Assert.AreEqual(2, neighbors.Count);
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[5, 3]));
            Assert.IsTrue(neighbors.Contains(Playground.Tiles[6, 2]));
        }
        [TestMethod]
        [DataRow(0, 3)]
        [DataRow(3, 2)]
        public void TileIsNextToATree_IsFalse(int x, int y)
        {
            Assert.IsFalse(Playground.Tiles[x, y].IsNextToATree);
        }
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(5, 3)]
        public void TileIsNextToATree_IsTrue(int x, int y)
        {
            Assert.IsTrue(Playground.Tiles[x, y].IsNextToATree);
        }
    }
}