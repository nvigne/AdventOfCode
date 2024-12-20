﻿using StreamReader reader = new StreamReader("input.txt");

List<List<int>> reports = new();
string? line;
while ((line = reader.ReadLine()) is not null)
{
    var numbersAsString = line.Split(" ");
    var levels = new List<int>();
    foreach (var numberAsString in numbersAsString)
    {
        var level = int.Parse(numberAsString);
        levels.Add(level);
    }
    reports.Add(levels);
}

static bool IsSafePart1(List<int> level)
{
    // a level is considered safe if all the values are increasing or decreasing and if two adjacent values differs by at least one or at max three.
    // First let's try to know if we decrease or increase.
    var firstDiff = level[0] - level[1];
    var isIncreasing = firstDiff < 0;

    for (int i = 0; i < level.Count - 1; i++)
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
foreach (var level in reports)
{
    if (IsSafePart1(level))
    {
        totalSafe++;
    }
    else
    {
        // if not safe with first logic, let's try the second logic to remove one level per report
        for (int i = 0; i < level.Count; i++)
        {
            var newLevel = new List<int>(level);
            newLevel.RemoveAt(i);
            if (IsSafePart1(newLevel))
            {
                totalSafe++;
                break;
            }
        }
    }
}

Console.WriteLine(totalSafe);