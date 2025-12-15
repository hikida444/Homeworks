// Интерфейс ICipher задаёт общий контракт для всех шифраторов:
// любой класс-реализация обязан уметь шифровать и дешифровать строку.
interface ICipher
{
    string Encode(string input); // шифрование строки
    string Decode(string input); // дешифрование строки
}

// Класс A: шифр «сдвиг на 1» в русском алфавите.
class A : ICipher
{
    public string Encode(string input)
    {
        char[] chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsLetter(chars[i]))
                continue;

            if (chars[i] == 'Я') chars[i] = 'А';
            else if (chars[i] == 'я') chars[i] = 'а';
            else chars[i] = (char)(chars[i] + 1);
        }

        return new string(chars);
    }

    public string Decode(string input)
    {
        char[] chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsLetter(chars[i]))
                continue;

            if (chars[i] == 'А') chars[i] = 'Я';
            else if (chars[i] == 'а') chars[i] = 'я';
            else chars[i] = (char)(chars[i] - 1);
        }

        return new string(chars);
    }
}

// Класс B: зеркальное шифрование относительно русского алфавита.
class B : ICipher
{
    private const string AlphabetUpper = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    private const string AlphabetLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

    public string Encode(string input)
    {
        char[] chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsLetter(chars[i]))
                continue;

            if (char.IsUpper(chars[i]))
            {
                int index = AlphabetUpper.IndexOf(chars[i]);
                chars[i] = AlphabetUpper[AlphabetUpper.Length - 1 - index];
            }
            else
            {
                int index = AlphabetLower.IndexOf(chars[i]);
                chars[i] = AlphabetLower[AlphabetLower.Length - 1 - index];
            }
        }

        return new string(chars);
    }

    // Зеркальный шифр обратим сам к себе: Encode(Encode(text)) == text.
    public string Decode(string input) => Encode(input);
}
