﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.Core
{
    public class TileGroup
    {
        public readonly List<Tile> Tiles;
        public readonly TileState TilesState;
        public int Length => Tiles.Count;
        public TileGroup(List<Tile> tiles, TileState tilesState)
        {
            Tiles = tiles;
            TilesState = tilesState;
        }
    }
}