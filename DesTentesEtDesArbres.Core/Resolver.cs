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
    }
}
