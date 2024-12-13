using AdventOfCode12;

StreamReader reader = new("input.txt");

char[,] map = new char[140, 140];
string? line;
int lineNumber = 0;
int maxColumn = 0;
while ((line = reader.ReadLine()) is not null)
{
    int x = 0;
    foreach (var item in line)
    {
        map[lineNumber, x] = item;
        x++;
    }
    maxColumn = x;
    lineNumber++;
}

Position currentPosition = new(0, 0);
HashSet<Position> notVisited = [];
for (int i = 0; i < lineNumber; i++)
{
    for (int j = 0; j < maxColumn; j++)
    {
        notVisited.Add(new Position(j, i));
    }
}


Queue<Position> toVisit = new();
toVisit.Enqueue(currentPosition);
char currentValue = map[currentPosition.Y, currentPosition.X];
int currentArea = 0;
int currentFences = 0;
int totalPrice = 0;

List<Fence> fences = new();

while (toVisit.Count != 0)
{
    var position = toVisit.Dequeue();
    var value = map[position.Y, position.X];
    notVisited.Remove(position);

    currentArea++;

    var top = new Position(position.X, position.Y - 1);
    var bottom = new Position(position.X, position.Y + 1);
    var left = new Position(position.X - 1, position.Y);
    var right = new Position(position.X + 1, position.Y);

    if (!top.IsOut(maxColumn, lineNumber) && notVisited.Contains(top) && map[top.Y, top.X] == currentValue)
    {
        toVisit.Enqueue(top);
        notVisited.Remove(top);
    }
    else if (top.IsOut(maxColumn, lineNumber) || map[top.Y, top.X] != currentValue)
    {
        fences.Add(new Fence(position, Direction.Horizontal, Side.Top));
        currentFences++;
    }

    if (!bottom.IsOut(maxColumn, lineNumber) && notVisited.Contains(bottom) && map[bottom.Y, bottom.X] == currentValue)
    {
        toVisit.Enqueue(bottom);
        notVisited.Remove(bottom);
    }
    else if (bottom.IsOut(maxColumn, lineNumber) || map[bottom.Y, bottom.X] != currentValue)
    {
        fences.Add(new Fence(position, Direction.Horizontal, Side.Bottom));
        currentFences++;
    }

    if (!left.IsOut(maxColumn, lineNumber) && notVisited.Contains(left) && map[left.Y, left.X] == currentValue)
    {
        toVisit.Enqueue(left);
        notVisited.Remove(left);
    }
    else if (left.IsOut(maxColumn, lineNumber) || map[left.Y, left.X] != currentValue)
    {
        fences.Add(new Fence(position, Direction.Vertical, Side.Left));
        currentFences++;
    }

    if (!right.IsOut(maxColumn, lineNumber) && notVisited.Contains(right) && map[right.Y, right.X] == currentValue)
    {
        toVisit.Enqueue(right);
        notVisited.Remove(right);
    }
    else if (right.IsOut(maxColumn, lineNumber) || map[right.Y, right.X] != currentValue)
    {
        fences.Add(new Fence(position, Direction.Vertical, Side.Right));
        currentFences++;
    }

    if (toVisit.Count == 0)
    {
        Console.WriteLine($"Current value {currentValue}");
        Console.WriteLine($"Area: {currentArea} Fences: {currentFences}");

        var sides = CalculateNumberOfSides(fences);

        Console.WriteLine($"Sides: {sides}");

        totalPrice += currentArea * sides;

        if (notVisited.Count != 0)
        {
            currentArea = 0;
            currentFences = 0;
            fences.Clear();
            var nextPosition = notVisited.First();
            toVisit.Enqueue(nextPosition);
            currentValue = map[nextPosition.Y, nextPosition.X];
        }
    }
}

static int CalculateNumberOfSides(List<Fence> fences)
{
    int totalSide = 0;

    var top = fences.Where(x => x.Side == Side.Top);

    // We want to iterate over each fence from a same side to see how many part we have.
    // We can do this by grouping the fences by their position and then counting the number of groups.
    // This will give us the number of parts we have.
    var topGroups = top.GroupBy(x => x.Position.Y);
    var bottomGroups = fences.Where(x => x.Side == Side.Bottom).GroupBy(x => x.Position.Y);

    totalSide += GetNumberOfSidesHorizontally(topGroups);
    totalSide += GetNumberOfSidesHorizontally(bottomGroups);

    var leftGroups = fences.Where(x => x.Side == Side.Left).GroupBy(x => x.Position.X);
    var rightGroups = fences.Where(x => x.Side == Side.Right).GroupBy(x => x.Position.X);

    totalSide += GetNumberOfSidesVertically(leftGroups);
    totalSide += GetNumberOfSidesVertically(rightGroups);

    return totalSide;
}

static int GetNumberOfSidesHorizontally(IEnumerable<IGrouping<int, Fence>> group)
{
    int totalSide = 0;
    foreach (var item in group)
    {
        var orderedFences = item.OrderBy(x => x.Position.X).ToList();
        for (int i = 0; i < orderedFences.Count; i++)
        {
            if (i == 0)
            {
                totalSide++;
            }
            else
            {
                if (orderedFences[i].Position.X - orderedFences[i - 1].Position.X > 1)
                {
                    totalSide++;
                }
            }
        }
    }

    return totalSide;
}

static int GetNumberOfSidesVertically(IEnumerable<IGrouping<int, Fence>> group)
{
    int totalSide = 0;
    foreach (var item in group)
    {
        var orderedFences = item.OrderBy(x => x.Position.Y).ToList();
        for (int i = 0; i < orderedFences.Count; i++)
        {
            if (i == 0)
            {
                totalSide++;
            }
            else
            {
                if (orderedFences[i].Position.Y - orderedFences[i - 1].Position.Y > 1)
                {
                    totalSide++;
                }
            }
        }
    }
    return totalSide;
}


Console.WriteLine(totalPrice);