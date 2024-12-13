using AdventOfCode12;

internal class Fence(Position position, Direction direction, Side side)
{
    public Position Position { get; } = position;
    public Direction Direction { get; } = direction;
    public Side Side { get; } = side;
}