internal struct Position(int x, int y)
{
    internal int X = x;
    internal int Y = y;

    public override string ToString()
    {
        return "X: " + X + " Y: " + Y;
    }

    public override int GetHashCode()
    {
        return (Y << 16) ^ X;
    }
}
