namespace M9Studio.HexMatrix
{
    public readonly struct HexGridPosition
    {
        public int X { get; }
        public int Y { get; }
        public int Z => -X - Y;

        public HexGridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public HexGridPosition(int x, int y, int z)
        {
            if (x + y + z != 0)
                throw new ArgumentException("x + y + z must be 0");
            X = x;
            Y = y;
        }

        public HexGridPosition MoveX(int dx) => new HexGridPosition(X + dx, Y - dx);
        public HexGridPosition MoveY(int dy) => new HexGridPosition(X - dy, Y + dy);
        public HexGridPosition MoveZ(int dz) => new HexGridPosition(X + dz, Y - dz);

        public static HexGridPosition operator +(HexGridPosition a, HexGridPosition b) => new HexGridPosition(a.X + b.X, a.Y + b.Y);
        public static HexGridPosition operator -(HexGridPosition a, HexGridPosition b) => new HexGridPosition(a.X - b.X, a.Y - b.Y);
        
        public int DistanceTo(HexGridPosition other) => (Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z)) / 2;

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
