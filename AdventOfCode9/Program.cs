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
while (valueIndex > 0)
{
    //Console.WriteLine(string.Join(",", numbers).Replace("-1", ""));

    var (nextNumberIndex, size) = GetNextIndexToSwap(numbers, valueIndex);
    var availableIndex = GetNextAvailableSpace(numbers, size);

    // If we can't place the numbers, let's skip them.
    if (availableIndex == -1)
    {
        valueIndex = nextNumberIndex - 1;
        continue;
    }

    // If the only place available for the numbers is after them, let's skip them.
    if (availableIndex > nextNumberIndex)
    {
        valueIndex = nextNumberIndex - 1;
        continue;
    }

    for (int i = 0; i < size; i++)
    {
        numbers[availableIndex + i] = numbers[nextNumberIndex + i];
        numbers[nextNumberIndex + i] = -1;
    }


    valueIndex = nextNumberIndex-1;
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
}

Console.WriteLine(checksum);

static (int index, int size) GetNextIndexToSwap(int[] numbers, int max)
{
    // Start from the end of the array.
    // Get first number that is not -1.
    int firstNumber = -1;
    int size = 0;
    for (int i = max; i >= 0; i--)
    {
        if (numbers[i] == -1 && firstNumber == -1)
        {
            continue;
        }
        else if (firstNumber == -1)
        {
            firstNumber = numbers[i];
            size++;
        }
        else if (firstNumber != numbers[i])
        {
            return (i+1, size);
        }
        else
        {
            size++;
        }
    }

    return (-1, -1);
}

static int GetNextAvailableSpace(int[] numbers, int size)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] == -1)
        {
            bool isAvailable = true;
            for (int j = i; j < i + size; j++)
            {
                if (j >= numbers.Length)
                {
                    isAvailable = false;
                    break;
                }

                if (numbers[j] != -1)
                {
                    isAvailable = false;
                    break;
                }
            }

            if (isAvailable)
            {
                return i;
            }
        }
    }

    return -1;
}
