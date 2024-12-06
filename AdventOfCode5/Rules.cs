internal class Rules(int i, int j)
{
    internal int i = i;
    internal int j = j;

    internal bool Validate(int firstNumber, int secondNumber)
    {
        return firstNumber == i && secondNumber == j;
    }
}

