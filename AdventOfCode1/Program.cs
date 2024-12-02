Tuple<List<int>, List<int>> GetColumns() {
    using StreamReader reader = new StreamReader("input.txt");

    List<int> column1 = new List<int>();
    List<int> column2 = new List<int>();
    string? line;
    while ((line = reader.ReadLine()) is not null)
    {
        var numbersAsString = line.Split("   ");
        var number1 = int.Parse(numbersAsString[0]);
        var number2 = int.Parse(numbersAsString[1]);

        column1.Add(number1);
        column2.Add(number2);
    }

    column1.Sort();
    column2.Sort();

    return new Tuple<List<int>, List<int>>(column1, column2);
}

void Part1()
{
    var tuple = GetColumns();
    var column1 = tuple.Item1;
    var column2 = tuple.Item2;

    // Calculate the distance between the two columns for each row and sum all the distances
    int sum = 0;
    for (int i = 0; i < column1.Count; i++)
    {
        sum += Math.Abs(column1[i] - column2[i]);
    }

    Console.WriteLine(sum);
}

void Part2()
{
    var tuple = GetColumns();
    var column1 = tuple.Item1;
    var column2 = tuple.Item2;

    // Calculate the sum of the value from the left column is in the right column
    int sum = 0;
    for (int i = 0; i < column2.Count; i++)
    {
        if (column1.Contains(column2[i]))
        {
            sum += column2[i];
        }
    }

    Console.WriteLine(sum);
}

Part2();


