StreamReader reader = new("input.txt");

int[] numbers = new int[0];
var line = reader.ReadLine();
int indexId = 0;
int index = 0;
int expectedSize = 0;
foreach (var item in line)
{
    int number = int.Parse(item.ToString());
    expectedSize += number;
    int previousSize = numbers.Length;
    Array.Resize(ref numbers, numbers.Length + number);
    if (index % 2 == 0)
    {
        for (int i = 0; i < number; i++)
        {
            numbers[i+previousSize] = indexId;
        }
        indexId++;
        index++;
    }
    else
    {
        for (int i = 0; i < number; i++)
        {
            numbers[i+previousSize] = -1;
        }
        index++;
    }
}

Console.WriteLine(expectedSize);

//Console.WriteLine(string.Join("", numbers).Replace("-1", "."));


int valueIndex = numbers.Length - 1;
// Let's go through the numbers from the end to the start.
for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] == -1)
    {
        valueIndex = GetNextIndexToSwap(numbers, valueIndex);
        if (i >= valueIndex)
        {
            break;
        }

        var number = numbers[valueIndex];

        numbers[i] = number;
        numbers[valueIndex] = -1;

        //Console.WriteLine(string.Join("", numbers).Replace("-1", "."));
    }
}

//Console.WriteLine(string.Join(",", numbers).Replace("-1", ""));

// Now calculate the checksum.
long checksum = 0;

Console.WriteLine(numbers.Length);

for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] >= 0)
    {
        checksum = checked(checksum + numbers[i] * i);
    }
    else
    {
        break;
    }
}

Console.WriteLine(checksum);

static int GetNextIndexToSwap(int[] numbers, int max)
{
    // Start from the end of the array.
    // Get first number that is not -1.
    for (int i = max; i >= 0; i--)
    {
        if (numbers[i] != -1)
        {
            return i;
        }
    }

    return -1;
}

