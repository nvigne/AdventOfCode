namespace AdventOfCode6
{
    enum Direction
    {
        Up, Down, Left, Right
    }

    internal class Guard(Position position, Direction direction)
    {
        public Position Position = position;
        public Direction Direction = direction;
        private HashSet<Tuple<Position, Direction>> _visited = new() { new(position, direction) };
        private HashSet<Position> _visitedOnlyPosition = new() { position };

        // Indicates if we already visited one of the position, i.e we are in a loop.
        public bool LoopDetected = false;


        public int Visited => _visitedOnlyPosition.Count;

        public Position Move()
        {
            switch (Direction)
            {
                case Direction.Up:
                    Position = new Position(Position.X - 1, Position.Y);
                    break;
                case Direction.Down:
                    Position = new Position(Position.X + 1, Position.Y);
                    break;
                case Direction.Left:
                    Position = new Position(Position.X, Position.Y - 1);
                    break;
                case Direction.Right:
                    Position = new Position(Position.X, Position.Y + 1);
                    break;
            }

            LoopDetected =  !_visited.Add(new(Position, Direction));
            _visitedOnlyPosition.Add(Position);

            return Position;
        }

        /// <summary>
        /// Doesn't alter current position.
        /// </summary>
        public Position NextPosition()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return new Position(Position.X - 1, Position.Y);
                case Direction.Down:
                    return new Position(Position.X + 1, Position.Y);
                case Direction.Left:
                    return new Position(Position.X, Position.Y - 1);
                case Direction.Right:
                    return new Position(Position.X, Position.Y + 1);
            }
            return Position;
        }

        public void ChangeDirection()
        {
            switch (Direction)
            {
                case Direction.Up:
                    Direction = Direction.Right;
                    break;
                case Direction.Down:
                    Direction = Direction.Left;
                    break;
                case Direction.Left:
                    Direction = Direction.Up;
                    break;
                case Direction.Right:
                    Direction = Direction.Down;
                    break;
            }
        }
    }
}
