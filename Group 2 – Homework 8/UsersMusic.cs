using System;

class Programm
{
    public static void Main()
    {
        Music.WorkWithMusic();
    }
}

class Music
{
    // Метод для работы с музыкальной коллекцией
    public static void WorkWithMusic()
    {
        // Список списков: каждый элемент — [название песни, автор]
        List<List<string>> music = [];

        // Ввод данных о 4 песнях
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Получаем данные о песне #{i + 1}");
            Console.WriteLine(new string('-', 60));

            Console.Write("Введите название песни: ");
            string NameOfSong1 = Console.ReadLine()!;

            Console.Write("Введите автора песни: ");
            string AuthorOfSong1 = Console.ReadLine()!;

            // Добавляем информацию о песне в список
            music.Add([NameOfSong1, AuthorOfSong1]);
        }

        // Меню действий
        while (true)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("Что вы хотите дальше сделать?");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("1) Вывести на экран информацию про песни," +
                              "\n2) Сравнить песни");

            switch (Console.ReadLine())
            {
                case "1":
                    {
                        // Вывод всей информации о песнях
                        PrintOfAllMusic(music);
                        return;
                    }
                case "2":
                    {
                        // Сравнение песен по предпочтениям пользователя
                        Equals(music);
                        return;
                    }
            }
        }
    }

    // Метод для формирования пользовательского рейтинга песен
    public static void Equals(List<List<string>> music)
    {
        Console.WriteLine("ВВЕДИТЕ ПОРЯДОК ДЛЯ ПЕСЕН");
        PrintOfAllMusic(music);

        while (true)
        {
            try
            {
                Console.WriteLine(new string('-', 60));

                // Пользователь вводит номера песен в порядке предпочтения
                Console.WriteLine("ВВЕДИТЕ НОМЕР ПЕСНИ КОТОРУЮ БЫ ВЫ ПОСТАВИЛИ НА МЕСТО #1");
                int num1 = Convert.ToInt32(Console.ReadLine()!);

                Console.WriteLine("ВВЕДИТЕ НОМЕР ПЕСНИ КОТОРУЮ БЫ ВЫ ПОСТАВИЛИ НА МЕСТО #2");
                int num2 = Convert.ToInt32(Console.ReadLine()!);

                Console.WriteLine("ВВЕДИТЕ НОМЕР ПЕСНИ КОТОРУЮ БЫ ВЫ ПОСТАВИЛИ НА МЕСТО #3");
                int num3 = Convert.ToInt32(Console.ReadLine()!);

                Console.WriteLine("ВВЕДИТЕ НОМЕР ПЕСНИ КОТОРУЮ БЫ ВЫ ПОСТАВИЛИ НА МЕСТО #4");
                int num4 = Convert.ToInt32(Console.ReadLine()!);

                // Выводим персональный рейтинг
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Ваш персональный список песен:");
                Console.WriteLine($"1) {music[num1 - 1][0]}");
                Console.WriteLine($"2) {music[num2 - 1][0]}");
                Console.WriteLine($"3) {music[num3 - 1][0]}");
                Console.WriteLine($"4) {music[num4 - 1][0]}");
                Console.WriteLine(new string('-', 60));

                // Предложение продолжить
                Console.WriteLine("Вы хотите продолжить?");
                string BoolInput = Console.ReadLine()!.ToLower();

                if (BoolInput == "да")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            catch (FormatException)
            {
                // Обработка ошибки неверного ввода
                Console.WriteLine("Ошибка! Попробуйте снова!");
            }
        }
    }

    // Метод выводит информацию обо всех песнях
    public static void PrintOfAllMusic(List<List<string>> music)
    {
        for (int i = 0; i < music.Count; i++)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Выводим информацию про песню #{i + 1}");
            Console.WriteLine($"Название песни: {music[i][0]}");
            Console.WriteLine($"Автор песни: {music[i][1]}");
            Console.WriteLine(new string('-', 60));
        }
    }
}
