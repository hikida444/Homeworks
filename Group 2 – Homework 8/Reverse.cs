using System;

class Programm
{
    public static void Main()
    {
        Reverse.GetStartedReverse();
    }
}

class Reverse()
{
    // Метод запускает работу с переворачиванием слова
    public static void GetStartedReverse()
    {
        // Вывод приветственного сообщения пользователю
        Console.WriteLine("Вас приветствует программа," +
            "которая переворачивает слова!");

        // Запрос слова у пользователя
        Console.Write("Введите слово, с которым мне следует поработать: ");

        // Читаем введённую строку и передаём её в метод ReverseOrder,
        // который возвращает перевёрнутое слово
        var ReversedWord = ReverseOrder(Console.ReadLine()!);

        // Выводим результат на экран
        Console.WriteLine($"Вот ваше слово, да только наоборот: {ReversedWord}");
    }

    // Метод принимает строку и возвращает её в перевёрнутом виде
    private static string ReverseOrder(string InputWord)
    {
        // Создаём список символов для хранения исходного слова
        List<char> SplitInput = [];

        // Разбиваем строку на символы и добавляем их в список
        foreach (char i in InputWord)
        {
            SplitInput.Add(i);
        }

        // Создаём список для хранения символов в обратном порядке
        List<char> ReversedWord = new();

        // Проходим по списку с конца к началу
        for (int i = SplitInput.Count; i != 0; i--)
        {
            // Добавляем символы в обратном порядке
            ReversedWord.Add(SplitInput[i - 1]);
        }

        // Объединяем список символов обратно в строку
        string Joined = string.Join("", ReversedWord);

        // Возвращаем перевёрнутую строку
        return Joined;
    }
}
