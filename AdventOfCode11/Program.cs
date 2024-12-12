LinkedList<long> stones = new();
// Example.
//stones.AddLast(125);
//stones.AddLast(17);

// Real input.
stones.AddLast(5910927);
stones.AddLast(0);
stones.AddLast(1);
stones.AddLast(47);
stones.AddLast(261223);
stones.AddLast(94788);
stones.AddLast(545);
stones.AddLast(7771);

Console.WriteLine(string.Join("->", stones));

for (int i = 0; i < 25; i++)
{
    LinkedListNode<long>? node = stones.First;
    while (node != null)
    {
        if (node.Value == 0)
        {
            node.Value = 1;
            node = node.Next;
        }
        else if (HasEvenNumberOfDigits(node.Value))
        {
            var (first, second) = GetTwoPartsOfNumber(node.Value);

            var newFirstNode = stones.AddBefore(node, first);
            var newSecondNode = stones.AddBefore(node, second);

            stones.Remove(node);
            node = newSecondNode.Next;
        }
        else
        {
            node.Value = node.Value * 2024;
            node = node.Next;
        }

    }

    //Console.WriteLine(string.Join("->", stones));
}

Console.WriteLine(stones.Count);

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