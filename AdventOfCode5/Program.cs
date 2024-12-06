using StreamReader reader = new StreamReader("input.txt");

bool isUpdateSection = false;
List<Tuple<int, int>> orderRules = new();
string? line;
List<List<int>> sections = new();
while ((line = reader.ReadLine()) is not null)
{
    if (!isUpdateSection && string.IsNullOrEmpty(line))
    {
        isUpdateSection = true;
        continue;
    }

    if (isUpdateSection)
    {
        var listNumbers = line.Split(',').Select(int.Parse).ToList();
        sections.Add(listNumbers);
    }
    else
    {
        orderRules.Add(new Tuple<int, int>(int.Parse(line.Split("|")[0]), int.Parse(line.Split("|")[1])));
    }
}

// Let's build a dictionary to easily find if we have rules.
Dictionary<int, List<Rules>> dictRules = new();
foreach (var item in orderRules)
{
    var rule = new Rules(item.Item1, item.Item2);

    if (dictRules.ContainsKey(item.Item1))
    {
        dictRules[item.Item1].Add(rule);
    }
    else
    {
        dictRules.Add(item.Item1, [rule]);
    }

    if (dictRules.ContainsKey(item.Item2))
    {
        dictRules[item.Item2].Add(rule);
    }
    else
    {
        dictRules.Add(item.Item2, [rule]);
    }
}

Console.WriteLine(orderRules.Count);
Console.WriteLine(sections.Count);

int sumMiddle = 0;
foreach (var item in sections)
{
    bool isValidSection = true;
    Console.WriteLine($"Evaluating {string.Join(",", item)}");

    // We need to validate for each number if it respects the rules with order numbers.
    for (int i = 0; i < item.Count; i++)
    {
        // Let's iterate over the remaining numbers
        for (int j = i + 1; j < item.Count; j++)
        {
            // We want to get if we have a rule that applies to both number.
            if (dictRules.ContainsKey(item[i]) && dictRules.ContainsKey(item[j]))
            {
                var rules = dictRules[item[i]].Intersect(dictRules[item[j]]).ToList();
                if (rules.Count > 0)
                {
                    Console.WriteLine($"Rule found for {item[i]} and {item[j]}");
                    if (rules[0].Validate(item[i], item[j]))
                    {
                        Console.WriteLine($"Rule validated for {item[i]} and {item[j]}");
                    }
                    else
                    {
                        Console.WriteLine($"Rule not validated for {item[i]} and {item[j]}");
                        isValidSection = false;
                        // What we want is each time we don't validate a rule, we invert the two numbers.
                        item.Swap(i, j);
                    }
                }
            }
        }
    }

    if (!isValidSection)
    {
        sumMiddle += GetMiddleNumber(item);
    }

    Console.WriteLine($"Section is valid: {isValidSection}");
}

Console.WriteLine($"Sum middle: {sumMiddle}");

static int GetMiddleNumber(List<int> numbers)
{
    return numbers[numbers.Count / 2];
}

