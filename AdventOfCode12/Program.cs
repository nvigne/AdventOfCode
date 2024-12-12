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
        currentFences++;
    }

    if (!bottom.IsOut(maxColumn, lineNumber) && notVisited.Contains(bottom) && map[bottom.Y, bottom.X] == currentValue)
    {
        toVisit.Enqueue(bottom);
        notVisited.Remove(bottom);
    }
    else if (bottom.IsOut(maxColumn, lineNumber) || map[bottom.Y, bottom.X] != currentValue)
    {
         currentFences++;
    }

    if (!left.IsOut(maxColumn, lineNumber) && notVisited.Contains(left) && map[left.Y, left.X] == currentValue)
    {
        toVisit.Enqueue(left);
        notVisited.Remove(left);
    }
    else if (left.IsOut(maxColumn, lineNumber) || map[left.Y, left.X] != currentValue)
    {
        currentFences++;
    }

    if (!right.IsOut(maxColumn, lineNumber) && notVisited.Contains(right) && map[right.Y, right.X] == currentValue)
    {
        toVisit.Enqueue(right);
        notVisited.Remove(right);
    }
    else if (right.IsOut(maxColumn, lineNumber) || map[right.Y, right.X] != currentValue)
    {
         currentFences++;
    }

    if (toVisit.Count == 0)
    {
        Console.WriteLine($"Current value {currentValue}");
        Console.WriteLine($"Area: {currentArea} Fences: {currentFences}");

        totalPrice += currentArea * currentFences;

        if (notVisited.Count != 0)
        {
            currentArea = 0;
            currentFences = 0;
            var nextPosition = notVisited.First();
            toVisit.Enqueue(nextPosition);
            currentValue = map[nextPosition.Y, nextPosition.X];
        }
    }
}


Console.WriteLine(totalPrice);