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

// We want to basically runs the guard through the map and see how many positions it visited.
bool ProcessMap(char[,] map, Guard guard)
{
    bool guardOut = false;
    while (!guardOut)
    {
        var nextPosition = guard.NextPosition();
        if (nextPosition.X >= lineNumber || nextPosition.Y >= maxColumn || nextPosition.X < 0 || nextPosition.Y < 0)
        {
            // If next position is out, let's stop here.
            guardOut = true;
            break;
        }

        // Goal is for each turn to move the position of the guard based on the map.
        if (map[nextPosition.X, nextPosition.Y] == '#')
        {
            guard.ChangeDirection();
        }
        else
        {
            guard.Move();
            map[guard.Position.X, guard.Position.Y] = 'X';
        }

        if (guard.LoopDetected)
        {
            return false;
        }
    }

    return true;
}


// Process the default map without any obstacles.
ProcessMap(table, guard);

Console.WriteLine(guard.Position);
Console.WriteLine(guard.Visited);

// Now we would like to add one obstacle to the map (# symbol) in a way that the guard will be stuck (infinite loop).
// Firs thing we need to do is to find the first position that the guard will visit twice.
// We can do this by checking the visited positions of the guard.

int loopDetected = 0;
for (int i = 0; i < lineNumber; i++)
{
    for (int j = 0; j < maxColumn; j++)
    {
        var copyMap = table.Clone() as char[,];
        copyMap[i, j] = '#';
        var copyGuard = guard.Clone();

        if (!ProcessMap(copyMap, copyGuard))
        {
            loopDetected++;
        }
    }
}

Console.WriteLine(loopDetected);



