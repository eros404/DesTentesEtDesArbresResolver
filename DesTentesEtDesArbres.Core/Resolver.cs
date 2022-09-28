namespace DesTentesEtDesArbres.Core
{
    public class Resolver
    {
        private readonly Playground _playground;
        public uint NumberOfChanges { get; private set; }
        public event EventHandler StateChanged = default!;

        public Resolver(Playground playground)
        {
            _playground = playground;
            _playground.StateChanged += PlaygroundStateChangedHandler;
        }
        private void PlaygroundStateChangedHandler(object? sender, EventArgs args)
        {
            NumberOfChanges++;
        }
        private void NotifyChanges()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Resolve()
        {
            InitialClean();
            NotifyChanges();
            CompleteByUnknownGroups();
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
        public void CompleteByUnknownGroups()
        {
            ExecuteWhilePlaygroundStateChange(() =>
            {
                foreach (var line in _playground.RowsAndColumns)
                {
                    uint storedNumberOfChanges = NumberOfChanges;
                    if (line.NumberOfMissingTents == 0)
                        continue;
                    var unknownGroups = line.GetGroups().Where(g => g.TilesState == TileState.Unknown).ToList();
                    if (unknownGroups.Sum(group => group.Length - (group.Length / 2)) == line.NumberOfMissingTents)
                    {
                        foreach (var group in unknownGroups)
                        {
                            if (group.Length % 2 == 0)
                            {
                                group.GetNeighbors()
                                    .Where(neighbor => (line.IsRow && neighbor.X != group.Tiles[0].X) || (line.IsColumn && neighbor.Y != group.Tiles[0].Y))
                                    .ToList()
                                    .ForEach(tile => tile.PutGrassIfIsUnknown());
                            }
                            else
                            {
                                group.Tiles.ForEach(tile => PlaceTentIfUnknown(tile));
                            }
                        }
                    }
                    if (storedNumberOfChanges < NumberOfChanges)
                        NotifyChanges();
                }
                Clean();
            });
        }
    }
}
