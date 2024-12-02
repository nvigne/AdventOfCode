using StreamReader reader = new StreamReader("input.txt");

List<List<int>> levels = new();
string? line;
while ((line = reader.ReadLine()) is not null)
{
    var numbersAsString = line.Split(" ");
    var level = new List<int>();
    foreach (var numberAsString in numbersAsString)
    {
        var number = int.Parse(numberAsString);
        level.Add(number);
    }
    levels.Add(level);
}

static bool IsSafe(List<int> level)
{
    // a level is considered safe if all the values are increasing or decreasing and if two adjacent values differs by at least one or at max three.
    // First let's try to know if we decrease or increase.
    var firstDiff = level[0] - level[1];
    if (firstDiff == 0 || Math.Abs(firstDiff) > 3)
    {
        return false;
    }

    var isIncreasing = firstDiff < 0;

    for (int i = 1; i < level.Count - 1; i++)
    {
        // Let's calculate if we decrease or increase
        var diff = level[i] - level[i + 1];
        if (isIncreasing && diff > 0)
        {
            return false;
        }
        else if (!isIncreasing && diff < 0)
        {
            return false;
        }
        else if (Math.Abs(diff) > 3)
        {
            return false;
        }
        else if (diff == 0)
        {
            // Case where we have a difference of 0
            return false;
        }
    }

    return true;
}

int totalSafe = 0;
foreach (var level in levels)
{
    if (IsSafe(level))
    {
        totalSafe++;
    }
}

Console.WriteLine(totalSafe);