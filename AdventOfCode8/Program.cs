StreamReader reader = new("input.txt");

char[,] table = new char[130, 130];
string? line;
int lineCount = 0;
int columnCount = 0;

Dictionary<char, List<Position>> antennaPositions = new();
while ((line = reader.ReadLine()) is not null)
{
    int x = 0;
    foreach (var item in line)
    {
        if (item != '.' && item != '#')
        {
            table[lineCount, x] = item;

            if (antennaPositions.ContainsKey(item))
            {
                antennaPositions[item].Add(new Position(x, lineCount));
            }
            else
            {
                antennaPositions.Add(item, [new(x, lineCount)]);
            }
        }

        x++;
    }

    columnCount = x;
    lineCount++;
}

HashSet<Position> antinode = new();
foreach (var item in antennaPositions)
{
    Console.WriteLine($"Antenna: {item.Key}");

    // Our goal is to calculate all the antinode position between each pair of antennas.
    for (int i = 0; i < item.Value.Count; i++)
    {
        for (int j = i + 1; j < item.Value.Count; j++)
        {
            var firstAntenna = item.Value[i];
            var secondAntenna = item.Value[j];

            Console.WriteLine($"Antenna 1: {firstAntenna}");
            Console.WriteLine($"Antenna 2: {secondAntenna}");

            antinode.Add(firstAntenna);

            var dist_y = item.Value[i].Y - item.Value[j].Y; 
            var dist_x = item.Value[i].X - item.Value[j].X;

            int multiplier = 1;
            while (true)
            {
                // We take the first antenna as our reference point,
                // and we calculate the antinode position based on the distance between the two antennas.
                Position newAntinode = new(firstAntenna.X + multiplier*dist_x, firstAntenna.Y + multiplier*dist_y);

                if (newAntinode.Y >= 0 && newAntinode.Y < lineCount && newAntinode.X >= 0 && newAntinode.X < columnCount)
                {
                    Console.WriteLine($"Antinode: {newAntinode}");
                    antinode.Add(newAntinode);
                    multiplier++;
                }
                else
                {
                    break;
                }
            }

            multiplier = 1;
            while (true)
            {
                // We take the first antenna as our reference point,
                // and we calculate the antinode position based on the distance between the two antennas.
                Position newAntinode = new(firstAntenna.X - multiplier*dist_x, firstAntenna.Y - multiplier*dist_y);

                if (newAntinode.Y >= 0 && newAntinode.Y < lineCount && newAntinode.X >= 0 && newAntinode.X < columnCount)
                {
                    Console.WriteLine($"Antinode: {newAntinode}");
                    antinode.Add(newAntinode);
                    multiplier++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}

Console.WriteLine(antinode.Count);