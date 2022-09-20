namespace DesTentesEtDesArbres.Core
{
    public class Resolver
    {
        private readonly Playground _playground;
        public uint NumberOfChanges { get; private set; }

        public Resolver(Playground playground)
        {
            _playground = playground;
            _playground.StateChanged += PlaygroundStateChangedHandler;
        }
        private void PlaygroundStateChangedHandler(object? sender, EventArgs args)
        {
            NumberOfChanges++;
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
        private static void PlaceTentIfUnknown(Tile tile)
        {
            if (tile.PutTentIfIsUnknown())
            {
                var neighbors = tile.GetNeighborsIncludingDiagonals();
                neighbors.ForEach(n => n.PutGrassIfIsUnknown());
            }
        }
        private void ExecuteWhilePlaygroundStateChange(Action action)
        {
            uint storedNumberOfChanges;
            do
            {
                storedNumberOfChanges = NumberOfChanges;
                action();
            } while (storedNumberOfChanges < NumberOfChanges);
        }
        public void CompleteEvidentTrees()
        {
            var mappingTreeAndNeighbors = new Dictionary<Tile, List<Tile>>();
            foreach (var tile in _playground.GetAllTrees())
            {
                mappingTreeAndNeighbors[tile] = tile.GetNeighbors();
            }
            ExecuteWhilePlaygroundStateChange(() =>
            {
                foreach (var treeAndNeighborsPair in mappingTreeAndNeighbors)
                {
                    if (treeAndNeighborsPair.Value.Any(n => n.State == TileState.Tent))
                        continue;
                    var unknownNeighbors = treeAndNeighborsPair.Value.Where(n => n.State == TileState.Unknown).ToList();
                    if (unknownNeighbors.Count == 1)
                        PlaceTentIfUnknown(unknownNeighbors[0]);
                }
            });
        }
        public void CompleteEasyGroups()
        {
            ExecuteWhilePlaygroundStateChange(() =>
            {
                foreach (var line in _playground.RowsAndColumns)
                {
                    var unknownGroups = line.GetGroups().Where(g => g.TilesState == TileState.Unknown).ToList();
                    if (unknownGroups.Count == 1 &&
                        (unknownGroups[0].Length == line.ExpectedNumberOfTents || unknownGroups[0].Length == (int)Math.Ceiling(line.ExpectedNumberOfTents * 1.5)))
                    {
                        unknownGroups[0].Tiles.ForEach(tile => PlaceTentIfUnknown(tile));
                    }
                }
            });
        }
    }
}
