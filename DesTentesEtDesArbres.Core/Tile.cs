namespace DesTentesEtDesArbres.Core
{
    public class Tile
    {
        public readonly uint X , Y;
        public TileState State { get; private set; }
        private readonly Playground _playground;
        public Tile(uint x, uint y, Playground playground, TileState tileState)
        {
            X = x;
            Y = y;
            State = tileState;
            _playground = playground;
        }
        public List<Tile> GetNeighbors()
        {
            var result = new List<Tile>();
            if (X != 0)
                result.Add(_playground.Tiles[X - 1, Y]);
            if (Y != 0)
                result.Add(_playground.Tiles[X, Y - 1]);
            if (X != _playground.Height - 1)
                result.Add(_playground.Tiles[X + 1, Y]);
            if (Y != _playground.Width - 1)
                result.Add(_playground.Tiles[X, Y + 1]);
            return result;
        }
        public List<Tile> GetNeighborsIncludingDiagonals()
        {
            var result = GetNeighbors();
            if (X != 0 && Y != 0)
                result.Add(_playground.Tiles[X - 1, Y - 1]);
            if (X != _playground.Height - 1 && Y != 0)
                result.Add(_playground.Tiles[X + 1, Y - 1]);
            if (X != 0 && Y != _playground.Width - 1)
                result.Add(_playground.Tiles[X - 1, Y + 1]);
            if (X != _playground.Height - 1 && Y != _playground.Width - 1)
                result.Add(_playground.Tiles[X + 1, Y + 1]);
            return result;
        }
        public bool IsNextToATree => GetNeighbors()
            .Where(n => n.State == TileState.Tree)
            .Any();
        public bool PutGrassIfIsUnknown()
        {
            if (State == TileState.Unknown)
            {
                State = TileState.Grass;
                return true;
            }
            return false;

        }
        public bool PutTentIfIsUnknown()
        {
            if (State == TileState.Unknown)
            {
                State = TileState.Tent;
                return true;
            }
            return false;
        }
    }
}
