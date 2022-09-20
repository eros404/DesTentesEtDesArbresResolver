﻿namespace DesTentesEtDesArbres.Core
{
    public class TileLine
    {
        public readonly uint ExpectedNumberOfTents;
        public readonly List<Tile> Tiles;
        public TileLine(List<Tile> tiles, uint expectedNumberOfTents)
        {
            ExpectedNumberOfTents = expectedNumberOfTents;
            Tiles = tiles;
        }
        public int NumberOfTents => Tiles.Where(t => t.State == TileState.Tent).Count();
        public int NumberOfUnknows => Tiles.Where(t => t.State == TileState.Unknown).Count();
        public bool HasUnknowns => Tiles.Where(t => t.State == TileState.Unknown).Any();
        public void PutGrassOnAllUnknownTile()
        {
            Tiles.ForEach(tile => tile.PutGrassIfIsUnknown());
        }
        public List<TileGroup> GetGroups()
        {
            var result = new List<TileGroup>();
            TileGroup? storedGroup = null;
            foreach (var tile in Tiles)
            {
                if (storedGroup == null)
                    storedGroup = new TileGroup(new() { tile }, tile.State);
                else if (storedGroup.TilesState == tile.State)
                    storedGroup.Tiles.Add(tile);
                else
                {
                    result.Add(storedGroup);
                    storedGroup = new TileGroup(new() { tile }, tile.State);
                }
            }
            if (storedGroup != null)
                result.Add(storedGroup);
            return result;
        }
    }
}
