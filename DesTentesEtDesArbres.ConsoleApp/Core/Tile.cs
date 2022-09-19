using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.ConsoleApp.Core
{
    internal class Tile
    {
        public readonly uint X , Y;
        public TileState State { get; set; }
        private readonly Playground _playground;
        public Tile(uint x, uint y, Playground playground, TileState tileState)
        {
            X = x;
            Y = y;
            State = tileState;
            _playground = playground;
        }
        public List<Tile> GetNeighbors()
        {
            var result = new List<Tile>();
            if (X != 0)
                result.Add(_playground.Tiles[X - 1, Y]);
            if (Y != 0)
                result.Add(_playground.Tiles[X, Y - 1]);
            if (X != _playground.Height - 1)
                result.Add(_playground.Tiles[X + 1, Y]);
            if (X != _playground.Width - 1)
                result.Add(_playground.Tiles[X, Y + 1]);
            return result;
        }
        public bool IsNextToATree => GetNeighbors()
            .Where(n => n.State == TileState.Tree)
            .Any();
    }
}
