namespace DesTentesEtDesArbres.Core
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
            foreach (var tile in Tiles)
                tile.PutGrassIfIsUnknown();
        }
    }
}
