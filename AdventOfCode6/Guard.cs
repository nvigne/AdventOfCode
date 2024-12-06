﻿namespace AdventOfCode6
{
    enum Direction
    {
        Up, Down, Left, Right
    }

    internal class Guard(Position position, Direction direction)
    {
        public Position Position = position;
        public Direction Direction = direction;

        HashSet<Position> _visited = new() { position };

        public int Visited => _visited.Count;

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

            _visited.Add(Position);

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