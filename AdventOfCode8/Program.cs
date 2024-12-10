
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
        if (item != '.')
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

            var dist_y = Math.Abs(item.Value[i].Y - item.Value[j].Y); 
            var dist_x = Math.Abs(item.Value[i].X - item.Value[j].X);

            Position antinode1 = new(0, 0);
            Position antinode2 = new(0, 0);
            if (secondAntenna.Y >= firstAntenna.Y && secondAntenna.X >= firstAntenna.X)
            {
                antinode1 = new Position(secondAntenna.X + dist_x, secondAntenna.Y + dist_y);
                antinode2 = new Position(firstAntenna.X - dist_x, firstAntenna.Y - dist_y);
            }
            else if (secondAntenna.Y >= firstAntenna.Y && secondAntenna.X < firstAntenna.X)
            {
                antinode1 = new Position(secondAntenna.X - dist_x, secondAntenna.Y + dist_y);
                antinode2 = new Position(firstAntenna.X + dist_x, firstAntenna.Y - dist_y);
            }
            else if (secondAntenna.Y < firstAntenna.Y && secondAntenna.X >= firstAntenna.X)
            {
                antinode1 = new Position(secondAntenna.X + dist_x, secondAntenna.Y - dist_y);
                antinode2 = new Position(firstAntenna.X - dist_x, firstAntenna.Y + dist_y);
            }
            else
            {
                antinode1 = new Position(secondAntenna.X - dist_x, secondAntenna.Y - dist_y);
                antinode2 = new Position(firstAntenna.X + dist_x, firstAntenna.Y + dist_y);
            }

            Console.WriteLine($"Antinode 1: {antinode1}");
            Console.WriteLine($"Antinode 2: {antinode2}");

            // For each pair of antennas, the position of the two antinodes is the point that is at both at the same distance of one antenna and twice the distance of the other.
            // So basically it is on the same line as the two antenna, but outside of between the two antennas.

            // Now let's see if the antenna is located intside the map.
            if (antinode1.Y >= 0 && antinode1.Y < lineCount && antinode1.X >= 0 && antinode1.X < columnCount)
            {
                antinode.Add(antinode1);
                Console.WriteLine("Antinode 1 is inside the map.");
            }

            if (antinode2.Y >= 0 && antinode2.Y < lineCount && antinode2.X >= 0 && antinode2.X < columnCount)
            {
                antinode.Add(antinode2);
                Console.WriteLine("Antinode 2 is inside the map.");
            }
        }
    }
}

Console.WriteLine(antinode.Count);