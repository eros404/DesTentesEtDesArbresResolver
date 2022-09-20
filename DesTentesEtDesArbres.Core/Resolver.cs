using DesTentesEtDesArbres.Core;

namespace DesTentesEtDesArbres.Core
{
    public class Resolver
    {
        private readonly Playground _playground;

        public Resolver(Playground playground)
        {
            _playground = playground;
        }

        public void InitialClean()
        {
            Clean();
            foreach (var tile in _playground.Tiles)
            {
                if (!tile.IsNextToATree)
                    tile.PutGrassIfIsUnknown();
            }
        }
        public void Clean()
        {
            _playground.RowsAndColumns
                .Where(line => line.NumberOfTents == line.ExpectedNumberOfTents && line.HasUnknowns)
                .ToList()
                .ForEach(line => line.PutGrassOnAllUnknownTile());
        }
        private static void PlaceTentIfIsUnknown(Tile tile)
        {
            if (tile.PutTentIfIsUnknown())
            {
                var neighbors = tile.GetNeighborsIncludingDiagonals();
                neighbors.ForEach(n => n.PutGrassIfIsUnknown());
            }
        }
        public void CompleteEvidentLines()
        {
            _playground.RowsAndColumns
                .Where(line => line.ExpectedNumberOfTents == line.NumberOfTents + line.NumberOfUnknows)
                .SelectMany(line => line.Tiles)
                .ToList()
                .ForEach(tile => PlaceTentIfIsUnknown(tile));
        }
        public void CompleteEvidentTrees()
        {
            foreach (var tile in _playground.GetAllTrees())
            {
                var neighborsUnknown = tile.GetNeighbors();
                if (neighborsUnknown.Any(n => n.State == TileState.Tent))
                    continue;
                if (neighborsUnknown.Where(n => n.State == TileState.Unknown)
                    .Count() == 1)
                    PlaceTentIfIsUnknown(neighborsUnknown.First());
            }
        }
    }
}
