StreamReader streamReader = new("input.txt");

List<Tuple<long, List<long>>> values = new();
string? line;
while ((line = streamReader.ReadLine()) is not null)
{
    var split = line.Split(": ");
    var result = long.Parse(split[0]);

    var valuesSplit = split[1].Split(" ");
    List<long> numbers = new();
    foreach (var v in valuesSplit)
    {
        numbers.Add(long.Parse(v));
    }

    values.Add(new(result, numbers));
}

long sumTotalResult = 0;
foreach (var item in values)
{
    List<long> results = new();
    CalculateAllValues(item.Item2[0], item.Item2.GetRange(1, item.Item2.Count-1), results);
    if (results.Contains(item.Item1))
    {
        sumTotalResult += item.Item1;
    }
}

long CalculateAllValues(long value, List<long> remainingNumbers, List<long> results)
{
    if (remainingNumbers.Count == 0)
    {
        return value;
    }

    var sum = CalculateAllValues(value + remainingNumbers[0], remainingNumbers.GetRange(1, remainingNumbers.Count - 1), results);
    var mult = CalculateAllValues(value * remainingNumbers[0], remainingNumbers.GetRange(1, remainingNumbers.Count - 1), results);

    var con = long.Parse(value.ToString() + remainingNumbers[0].ToString());
    var concat = CalculateAllValues(con, remainingNumbers.GetRange(1, remainingNumbers.Count - 1), results);

    results.Add(sum);
    results.Add(mult);
    results.Add(concat);

    return 0;
}

Console.Write(sumTotalResult);