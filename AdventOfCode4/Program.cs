using StreamReader reader = new StreamReader("input.txt");

char[,] table = new char[140, 140];
string? line;
int lineNumber = 0;
int maxColumn = 0;
while ((line = reader.ReadLine()) is not null)
{
    int y = 0;
    foreach (var item in line)
    {
        table[lineNumber, y] = item;
        y++;
    }
    maxColumn = y;
    lineNumber++;
}

// Let's go through the table and fix each occurence of the word 'XMAS' inside the table
reader.Close();
reader.Dispose();

int CalculateXmas()
{
    int totalXmas = 0;
    for (int i = 0; i < lineNumber; i++)
    {
        for (int j = 0; j < maxColumn; j++)
        {
            if (table[i, j] == 'X')
            {
                // XMAS vertically
                if (i + 3 < lineNumber && table[i + 1, j] == 'M' && table[i + 2, j] == 'A' && table[i + 3, j] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                // XMAS vertically up
                if (i - 3 >= 0 && table[i - 1, j] == 'M' && table[i - 2, j] == 'A' && table[i - 3, j] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                // XMAS hortizontaly backwaard
                if (j - 3 >= 0 && table[i, j - 1] == 'M' && table[i, j - 2] == 'A' && table[i, j - 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                // XMAS horizontally
                if (j + 3 < maxColumn && table[i, j + 1] == 'M' && table[i, j + 2] == 'A' && table[i, j + 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                // It can be written diagonally - 3 directions
                if (i + 3 < lineNumber && j + 3 < maxColumn && table[i + 1, j + 1] == 'M' && table[i + 2, j + 2] == 'A' && table[i + 3, j + 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                if (i - 3 >= 0 && j - 3 >= 0 && table[i - 1, j - 1] == 'M' && table[i - 2, j - 2] == 'A' && table[i - 3, j - 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                if (i - 3 >= 0 && j + 3 < maxColumn && table[i - 1, j + 1] == 'M' && table[i - 2, j + 2] == 'A' && table[i - 3, j + 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }

                if (i + 3 < lineNumber && j - 3 >= 0 && table[i + 1, j - 1] == 'M' && table[i + 2, j - 2] == 'A' && table[i + 3, j - 3] == 'S')
                {
                    Console.WriteLine($"{i} {j}");
                    totalXmas++;
                }
            }
        }
    }

    return totalXmas;
}

int calculeXMAS2()
{
    int totalXmas = 0;
    for (int i = 0; i < lineNumber; i++)
    {
        for (int j = 0; j < maxColumn; j++)
        {
            if (table[i, j] == 'A')
            {
                // We validate that we can be in the middle of the an X-MAS.
                if (i - 1 >= 0 && i + 1 < lineNumber && j - 1 >= 0 && j + 1 < maxColumn)
                {
                    // Now find if we have an X-MAS
                    // M M
                    //  A
                    // S S
                    if (table[i - 1, j - 1] == 'M' && table[i + 1, j + 1] == 'S' && table[i - 1, j + 1] == 'M' && table[i + 1, j - 1] == 'S')
                    {
                        totalXmas++;
                    }

                    // Find
                    // S S
                    //  A
                    // M M
                    if (table[i - 1, j - 1] == 'S' && table[i + 1, j + 1] == 'M' && table[i - 1, j + 1] == 'S' && table[i + 1, j - 1] == 'M')
                    {
                        totalXmas++;
                    }

                    // Find
                    // M S
                    //  A
                    // M S
                    if (table[i - 1, j - 1] == 'M' && table[i + 1, j + 1] == 'S' && table[i - 1, j + 1] == 'S' && table[i + 1, j - 1] == 'M')
                    {
                        totalXmas++;
                    }

                    // Find
                    // S M
                    //  A
                    // S M
                    if (table[i - 1, j - 1] == 'S' && table[i + 1, j + 1] == 'M' && table[i - 1, j + 1] == 'M' && table[i + 1, j - 1] == 'S')
                    {
                        totalXmas++;
                    }
                }
            }
        }
    }
    
    return totalXmas;
}

var totalXmas = calculeXMAS2();


Console.WriteLine($"{totalXmas}");
