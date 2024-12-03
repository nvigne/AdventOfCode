using StreamReader reader = new StreamReader("input.txt");

int c;
int total = 0;
while ((c = reader.Read()) != -1)
{
    var character = (char)c;
    if (character == 'm')
    {
        character = (char)reader.Read();
        if (character == 'u')
        {
            character = (char)reader.Read();
            if (character == 'l')
            {
                character = (char)reader.Read();
                if (character == '(')
                {
                    bool isValid = true;
                    bool isNumber2 = false;
                    int number1 = 0;
                    int number2 = 0;
                    while ((c = reader.Read()) != -1)
                    {
                        character = (char)c;
                        if (Char.IsDigit(character))
                        {
                            if (isNumber2)
                            {
                                number2 = number2 * 10 + (character - '0');
                            }
                            else
                            {
                                number1 = number1 * 10 + (character - '0');
                            }
                        }
                        else if (character == ',' && !isNumber2)
                        {
                            isNumber2 = true;
                        }
                        else if (character == ')')
                        {
                            break;
                        }
                        else
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (!isValid)
                    {
                        continue;
                    }
                    else
                    {
                        total += number1 * number2;
                    }
                }
            }
        }
    }
}

Console.WriteLine(total);
