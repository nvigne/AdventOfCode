StreamReader reader = new("input.txt");

int[] numbers = new int[0];
var line = reader.ReadLine();
int indexId = 0;
int index = 0;
foreach (var item in line)
{
    int number = int.Parse(item.ToString());
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

//Console.WriteLine(string.Join("", numbers).Replace("-1", "."));

// Let's go through the numbers from the end to the start.
for (int i = numbers.Length - 1; i >= 0; i--)
{
    if (numbers[i] == -1)
    {
        // If we have an empty space, let's continue.
        continue;
    }
    
    var number = numbers[i];
    var nextAvailableIndex = GetNextAvailableSpace(numbers);

    // We don't want to go to end of the array, once we have arranged everything.
    // If we try to move the number to a place that is greater than the next available index,
    // it means we already re-arranged everything.
    if (nextAvailableIndex >= i)
    {
        break;
    }

    // We replace the space with the number.
    numbers[nextAvailableIndex] = number;

    // Then replace the number with a space.
    numbers[i] = -1;
}

Console.WriteLine(string.Join(",", numbers).Replace("-1", ""));

// Now calculate the checksum.
int checksum = 0;
bool firstSpace = false;
for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] != -1)
    {
        checksum += numbers[i] * i;
        if (firstSpace)
        {
            // Just validate that we didn't let a space.
            Console.WriteLine("WARNING: There is a space between the numbers.");
        }
    }
    else
    {
        firstSpace = true;
    }
}

Console.WriteLine(checksum);

static int GetNextAvailableSpace(int[] numbers)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] == -1)
        {
            return i;
        }
    }
    return -1;
}

