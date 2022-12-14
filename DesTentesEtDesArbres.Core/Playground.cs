namespace DesTentesEtDesArbres.Core
{
    public class Playground
    {
        public readonly Tile[,] Tiles;
        public readonly uint[] NumberOfTreesByRow;
        public readonly uint[] NumberOfTreesByColumn;
        public readonly TileLine[] Rows;
        public readonly TileLine[] Columns;
        public readonly TileLine[] RowsAndColumns;
        public readonly int Width;
        public readonly int Height;
        public Playground(TileState[,] tilesStates, uint[] numberOfTreesByRow, uint[] numberOfTreesByColumn)
        {
            Height = tilesStates.GetLength(0);
            Width = tilesStates.GetLength(1);
            if (Height != numberOfTreesByRow.Length)
                throw new Exception();
            if (Width != numberOfTreesByColumn.Length)
                throw new Exception();
                
            Tiles = new Tile[Height, Width];
            for (uint i = 0; i < Height; i++)
                for (uint y = 0; y < Width; y++)
                {
                    var tile = new Tile(i, y, this, tilesStates[i, y]);
                    tile.StateChanged += TileStateChangedHandler;
                    Tiles[i, y] = tile;
                }
            NumberOfTreesByRow = numberOfTreesByRow;
            NumberOfTreesByColumn = numberOfTreesByColumn;
            var rows = new List<TileLine>();
            for (int i = 0; i < Height; i++)
            {
                rows.Add(new TileLine(this, LineOrientation.Horizontal, Enumerable.Range(0, Width)
                        .Select(x => Tiles[i, x])
                        .ToList(), numberOfTreesByRow[i]));
            }
            Rows = rows.ToArray();
            var columns = new List<TileLine>();
            for (int i = 0; i < Width; i++)
            {
                columns.Add(new TileLine(this, LineOrientation.Vertical, Enumerable.Range(0, Height)
                        .Select(x => Tiles[x, i])
                        .ToList(), numberOfTreesByColumn[i]));
            }
            Columns = columns.ToArray();
            var rowsAndColumns = Rows.ToList();
            rowsAndColumns.AddRange(Columns);
            RowsAndColumns = rowsAndColumns.ToArray();
        }
        public event EventHandler StateChanged = default!;
        private void TileStateChangedHandler(object? sender, EventArgs args)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
        public TileState[,] GetTileStateMatrix()
        {
            var matrix = new TileState[Height, Width];
            for (uint i = 0; i < Height; i++)
                for (uint y = 0; y < Width; y++)
                    matrix[i, y] = Tiles[i, y].State;
            return matrix;
        }
        public List<Tile> GetAllTrees()
        {
            var result = new List<Tile>();
            foreach (var tile in Tiles)
                if (tile.State == TileState.Tree)
                    result.Add(tile);
            return result;
        }
    }
}
