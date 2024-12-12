using System.Collections.Generic;

Dictionary<long, long> stones = new();
// Example.
//stones.Add(125, 1);
//stones.Add(17, 1);

// Real input.
stones.Add(5910927, 1);
stones.Add(0, 1);
stones.Add(1, 1);
stones.Add(47, 1);
stones.Add(261223, 1);
stones.Add(94788, 1);
stones.Add(545, 1);
stones.Add(7771, 1);

for (int i = 0; i < 75; i++)
{
    Dictionary<long, long> newStones = new();
    foreach (var stone in stones.ToList())
    {
        if (stone.Key == 0)
        {
            if (newStones.ContainsKey(1))
            {
                newStones[1] += stone.Value;
            }
            else
            {
                newStones.Add(1, stone.Value);
            }
        }
        else if (HasEvenNumberOfDigits(stone.Key))
        {
            var (first, second) = GetTwoPartsOfNumber(stone.Key);
            if (newStones.ContainsKey(first))
            {
                newStones[first] += stone.Value;
            }
            else
            {
                newStones.Add(first, stone.Value);
            }

            if (newStones.ContainsKey(second))
            {
                newStones[second] += stone.Value;
            }
            else
            {
                newStones.Add(second, stone.Value);
            }
        }
        else
        {
            if (newStones.ContainsKey(stone.Key * 2024))
            {
                newStones[stone.Key * 2024] += stone.Value;
            }
            else
            {
                newStones.Add(stone.Key * 2024, stone.Value);
            }
        }
    }

    stones = newStones;

    //Console.WriteLine(stones.Count);
}

long total = 0;
foreach (var stone in stones)
{
    total += stone.Value;
}

Console.WriteLine(total);

static bool HasEvenNumberOfDigits(long number)
{
    return number.ToString().Length % 2 == 0;
}

static Tuple<long, long> GetTwoPartsOfNumber(long number)
{
    string numberAsString = number.ToString();
    int middle = numberAsString.Length / 2;
    return new Tuple<long, long>(long.Parse(numberAsString[..middle]), long.Parse(numberAsString[middle..]));
}