public struct Position(int x, int y)
{
    public int X = x;
    public int Y = y;

    public override string ToString()
    {
        return "X: " + X + " Y: " + Y;
    }

    public override int GetHashCode()
    {
        return (Y << 16) ^ X;
    }

    public bool IsOut(int maxX, int maxY)
    {
        return X < 0 || Y < 0 || X >= maxX || Y >= maxY;
    }
}
