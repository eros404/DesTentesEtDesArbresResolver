using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.ConsoleApp.Core
{
    internal class TileLine
    {
        public readonly uint ExpectedNumberOfTents;
        public readonly List<Tile> Tiles;
        public TileLine(List<Tile> tiles, uint expectedNumberOfTents)
        {
            ExpectedNumberOfTents = expectedNumberOfTents;
            Tiles = tiles;
        }
        public int NumberOfTents => Tiles.Select(t => t.State == TileState.Tent).Count();
    }
}
