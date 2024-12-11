StreamReader reader = new("input.txt");

int[,] map = new int[140, 140];

string? line;
int lineNumber = 0;
int maxColumn = 0;
List<Position> startTrail = new();
while ((line = reader.ReadLine()) is not null)
{
    int x = 0;
    foreach (var item in line)
    {
        map[lineNumber, x] = int.Parse(item.ToString());

        if (map[lineNumber, x] == 0)
        {
            startTrail.Add(new Position(x, lineNumber));
        }

        x++;
    }
    maxColumn = x;
    lineNumber++;
}

Console.WriteLine(startTrail.Count);

// Now let's start the trail for each of the starting points.
// Follow a trail is simple, we need to discover if we have another route to follow.
// A route to follow is one adjacent tile with a value that is exactly current value +1.
// Let's start with the first starting point.
int totalPaths = 0;
foreach (var position in startTrail)
{
    Console.WriteLine($"Starting position: {position}");
    HashSet<Position> hillVisitedForCurrentTrail = new();
    // We want to perform backtracking to find each possible path.
    // We will use a stack to keep track of the current path.
    IsValidPath(map, position, -1, hillVisitedForCurrentTrail);

    Console.WriteLine($"Total paths: {totalPaths}");
}

Console.WriteLine(totalPaths);

bool IsValidPath(int[,] map, Position position, int previousValue, HashSet<Position> visited)
{
    if (position.X < 0 || position.Y < 0 || position.X >= maxColumn || position.Y >= lineNumber)
    {
        return false;
    }

    if (map[position.Y, position.X] != previousValue + 1)
    {
        return false;
    }

    if (map[position.Y, position.X] == 9)
    {
        if (!visited.Contains(position))
        {
            totalPaths++;
        }

        visited.Add(position);
        return true;
    }

    if (IsValidPath(map, new Position(position.X + 1, position.Y), map[position.Y, position.X], visited))
    {
    }
    if (IsValidPath(map, new Position(position.X - 1, position.Y), map[position.Y, position.X], visited))
    {
    }
    if (IsValidPath(map, new Position(position.X, position.Y + 1), map[position.Y, position.X], visited))
    {
    }
    if (IsValidPath(map, new Position(position.X, position.Y - 1), map[position.Y, position.X], visited))
    {
    }

    return true;
}