using DesTentesEtDesArbres.Core;

namespace DesTentesEtDesArbres.ConsoleApp
{
    internal class Resolver
    {
        private readonly Playground _playground;

        public Resolver(Playground playground)
        {
            _playground = playground;
        }

        public void InitialClean()
        {
            foreach (var tile in _playground.Tiles)
            {
                if (!tile.IsNextToATree && tile.State == TileState.Unknown)
                    tile.State = TileState.Grass;
            }
        }
        public void Clean()
        {
            foreach(var row in _playground.Rows.Where(r => r.NumberOfTents == r.ExpectedNumberOfTents))
            {
                foreach(var unknownTile in row.Tiles.Where(t => t.State == TileState.Unknown))
                    unknownTile.State = TileState.Grass;
            }
            foreach (var column in _playground.Columns.Where(c => c.NumberOfTents == c.ExpectedNumberOfTents))
            {
                foreach (var unknownTile in column.Tiles.Where(t => t.State == TileState.Unknown))
                    unknownTile.State = TileState.Grass;
            }
        }
    }
}
