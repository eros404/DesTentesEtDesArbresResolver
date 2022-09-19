using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.ConsoleApp.Core
{
    internal class Playground
    {
        public readonly Tile[,] Tiles;
        public readonly uint[] NumberOfTreesByRow;
        public readonly uint[] NumberOfTreesByColumn;
        public readonly TileLine[] Rows;
        public readonly TileLine[] Columns;
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
                    Tiles[i, y] = new Tile(i, y, this, tilesStates[i, y]);
            NumberOfTreesByRow = numberOfTreesByRow;
            NumberOfTreesByColumn = numberOfTreesByColumn;
            var rows = new List<TileLine>();
            for (int i = 0; i < Height; i++)
            {
                rows.Add(new TileLine(Enumerable.Range(0, Height)
                        .Select(x => Tiles[x, i])
                        .ToList(), numberOfTreesByRow[i]));
            }
            Rows = rows.ToArray();
            var columns = new List<TileLine>();
            for (int i = 0; i < Width; i++)
            {
                columns.Add(new TileLine(Enumerable.Range(0, Width)
                        .Select(x => Tiles[i, x])
                        .ToList(), numberOfTreesByColumn[i]));
            }
            Columns = columns.ToArray();
        }
        public TileState[,] GetTileStateMatrix()
        {
            var matrix = new TileState[Height, Width];
            for (uint i = 0; i < Height; i++)
                for (uint y = 0; y < Width; y++)
                    matrix[i, y] = Tiles[i, y].State;
            return matrix;
        }
    }
}
