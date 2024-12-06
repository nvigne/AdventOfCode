using AdventOfCode6;

using StreamReader reader = new StreamReader("input.txt");

char[,] table = new char[130, 130];
string? line;
int lineNumber = 0;
int maxColumn = 0;
Position guardPosition = new(0,0);
Guard guard = new(guardPosition, Direction.Up);
while ((line = reader.ReadLine()) is not null)
{
    int y = 0;
    foreach (var item in line)
    {
        if (item == '^')
        {
            // Let's get the guard position start and mark is position with X.
            guardPosition = new Position(lineNumber, y);
            guard = new Guard(guardPosition, Direction.Up);
            table[guardPosition.X, guardPosition.Y] = 'X';
            Console.WriteLine($"Guard position: {guardPosition}");
        }

        table[lineNumber, y] = item;
        y++;
    }
    maxColumn = y;
    lineNumber++;
}

Console.WriteLine(lineNumber);
Console.WriteLine(maxColumn);
Console.WriteLine(guardPosition);

bool guardOut = false;
while (!guardOut)
{
    // Goal is for each turn to move the position of the guard based on the map.
    var nextPosition = guard.NextPosition();
    if (nextPosition.X >= lineNumber || nextPosition.Y >= maxColumn || nextPosition.X < 0 || nextPosition.Y < 0)
    {
        // If next position is out, let's stop here.
        guardOut = true;
        break;
    }

    if (table[nextPosition.X, nextPosition.Y] == '#')
    {
        guard.ChangeDirection();
    }
    else
    {
        guard.Move();
        table[guard.Position.X, guard.Position.Y] = 'X';
    }
}

Console.WriteLine(guard.Position);
Console.WriteLine(guard.Visited);
