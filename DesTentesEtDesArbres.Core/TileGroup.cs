namespace DesTentesEtDesArbres.Core
{
    public class TileGroup
    {
        private readonly Playground _playground;
        public readonly List<Tile> Tiles;
        public readonly TileState TilesState;
        public int Length => Tiles.Count;
        public TileGroup(Playground playground, List<Tile> tiles, TileState tilesState)
        {
            Tiles = tiles;
            TilesState = tilesState;
            _playground = playground;
        }
        public List<Tile> GetNeighbors()
        {
            var result = new HashSet<Tile>();
            foreach (var tile in Tiles)
            {
                tile.GetNeighbors().ForEach(neighbor => result.Add(neighbor));
            }
            return result.ToList(); ;
        }
    }
}
