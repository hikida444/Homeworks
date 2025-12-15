using System;
using System.Globalization;

class Program
{
    // Перечисление для дней недели 
    enum DayOfWeek
    {
        Понедельник,
        Вторник,
        Среда,
        Четверг,
        Пятница,
        Суббота,
        Воскресенье
    }

    static void Main()
    {
        //Task41();
        //Task42();
        //TaskDz41();
        //Task1();
        //Task2();
        //Task3();
        //Task4();
        //Task5();  

        

        // Задание 4.1: Конвертация порядкового номера дня в году в дату (день и месяц)
        // Пользователь вводит номер дня (1-365). Программа определяет, какому дню какого месяца соответствует этот номер.
        // Алгоритм: Используется массив с количеством дней в каждом месяце. Последовательно вычитаем дни каждого месяца, пока не найдем нужный месяц.
        void Task41()
        {
            Console.WriteLine($"Задание 4.1 \n Считать номер дня в году и вывести месяц и день месяца");

            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }; //количесвто дней в каждом месяце

            Console.WriteLine($"Введите номер дня в году: ");
            int dayOfYear = int.Parse(Console.ReadLine());

            // Проверка валидности введенного номера дня
            if (dayOfYear < 1 || dayOfYear > 365)
            {
                Console.WriteLine($"Ошибка!");
                return;
            }

            int month = 1;
            int day = dayOfYear;

            // Определение месяца: вычитаем дни каждого месяца, пока не останется дней в текущем месяце
            while (day > daysInMonth[month - 1])
            {
                day -= daysInMonth[month - 1];
                month++;
            }

            Console.WriteLine($"{day} {MonthName(month)}");

            // Вспомогательный метод для получения названия месяца по его номеру
            static string MonthName(int month)
            {
                switch (month)
                {
                    case 1: return "января";
                    case 2: return "февраля";
                    case 3: return "марта";
                    case 4: return "апреля";
                    case 5: return "мая";
                    case 6: return "июня";
                    case 7: return "июля";
                    case 8: return "августа";
                    case 9: return "сентября";
                    case 10: return "октября";
                    case 11: return "ноября";
                    case 12: return "декабря";
                    default: return "неизвестный месяц";
                }
            }
        }

        // Задание 4.2: Аналогично Task41
        void Task42()
        {
            Console.WriteLine($"Задание 4.2 \n Добавить к предыдущей задаче проверку дня");

            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }; //количесвто днев в каждом месяце

            Console.WriteLine($"Введите номер дня в году: ");
            int dayOfYear = int.Parse(Console.ReadLine());

            if (dayOfYear < 1 || dayOfYear > 365)
            {
                Console.WriteLine($"Ошибка!");
                return;
            }

            int month = 1;
            int day = dayOfYear;

            while (day > daysInMonth[month - 1])
            {
                day -= daysInMonth[month - 1];
                month++;
            }

            Console.WriteLine($"{day} {MonthName(month)}");

            static string MonthName(int month)
            {
                switch (month)
                {
                    case 1: return "января";
                    case 2: return "февраля";
                    case 3: return "марта";
                    case 4: return "апрояля";
                    case 5: return "мая";
                    case 6: return "июня";
                    case 7: return "июля";
                    case 8: return "августа";
                    case 9: return "сентября";
                    case 10: return "октября";
                    case 11: return "ноября";
                    case 12: return "декабря";
                    default: return "неизвестный месяц";
                }
            }
        }

        // Задание 4.1 (Домашнее): Учет високосного года при конвертации номера дня в дату
        // Пользователь вводит номер дня и год. Программа учитывает, является ли год високосным .
        // Алгоритм: Проверка года на високосность. Используется два массива: для обычного и високосного года. Определение месяца аналогично предыдущим задачам.
        void TaskDz41()
        {
            Console.WriteLine($"Задание 4.1 \n Изменить программу из заданий 4.1 и 4.2, чтобы она учитывала високосный год или нет");

            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }; //количесвто днев в каждом месяце
            int[] daysInMonthV = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            Console.WriteLine($"Введите номер дня в году: ");
            int dayOfYear = int.Parse(Console.ReadLine());

            Console.WriteLine($"Введите год: ");
            int year = int.Parse(Console.ReadLine());

            // Проверка валидности номера дня (только для невисокосного года, что является недочетом)
            if (dayOfYear < 1 || dayOfYear > 365)
            {
                Console.WriteLine($"Ошибка!");
                return;
            }

            int month = 1;
            int day = dayOfYear;

            // Проверка на високосность: год кратен 4 или 400
            if (year % 4 == 0 | year % 400 == 0)
            {
                // Используем массив для високосного года
                while (day > daysInMonthV[month - 1])
                {
                    day -= daysInMonthV[month - 1];
                    month++;
                }
            }
            else
            {
                // Используем массив для обычного года
                while (day > daysInMonth[month - 1])
                {
                    day -= daysInMonth[month - 1];
                    month++;
                }
            }

            Console.WriteLine($"{day} {MonthName(month)}");

            static string MonthName(int month)
            {
                switch (month)
                {
                    case 1: return "января";
                    case 2: return "февраля";
                    case 3: return "марта";
                    case 4: return "апреля";
                    case 5: return "мая";
                    case 6: return "июня";
                    case 7: return "июля";
                    case 8: return "августа";
                    case 9: return "сентября";
                    case 10: return "октября";
                    case 11: return "ноября";
                    case 12: return "декабря";
                    default: return "неизвестный месяц";
                }
            }
        }

        // Задание 1: Проверка последовательности на возрастание
        //Генерируется массив из 10 случайных чисел. Программа проверяет, является ли последовательность строго возрастающей.
        // Если нет, выводится индекс первого элемента, который нарушает возрастание.
        void Task1()
        {
            Console.WriteLine($"Задание 1 \n Проверить последовательность на возрастаниеБ в случае отрицательного ответа определить порядковый номер первого числа нарушающего послежовательность");

            int[] posl = new int[10];
            Random rnd = new Random();

            // Заполнение массива случайными числами и их вывод
            for (int i = 0; i < posl.Length; i++)
            {
                posl[i] = rnd.Next(-100, 100);
                Console.WriteLine(posl[i]);
            }

            Console.WriteLine(posl);

            // Проверка на возрастание: каждый следующий элемент должен быть больше предыдущего
            for (int i = 1; i < posl.Length; i++)
            {
                if (posl[i] > posl[i - 1])
                {
                    continue; // Все пока в порядке, продолжаем
                }
                else
                {
                    // Найден элемент, нарушающий возрастание. Выводим его порядковый номер (индекс + 1).
                    Console.WriteLine($"{i + 1}");
                    break;
                }
            }
        }

        // Задание 2: Определение достоинства карты по ее номеру
        // Пользователь вводит число от 6 до 14. Программа возвращает название карты (например, 6 -> "шестерка", 11 -> "валет").
        // Используется switch выражение для сопоставления.
        void Task2()
        {
            Console.WriteLine($"Задание 2 \n По заданному номеру карты определить ее достоинство");

            try
            {
                Console.Write("Введите номер карты (6-14): ");
                int k = int.Parse(Console.ReadLine());

                // Сопоставление номера карты с ее названием
                string card = k switch
                {
                    6 => "шестерка",
                    7 => "семерка",
                    8 => "восьмерка",
                    9 => "девятка",
                    10 => "десятка",
                    11 => "валет",
                    12 => "дама",
                    13 => "король",
                    14 => "туз",
                    _ => "неизвестная карта" // Значение по умолчанию при неверном вводе
                };

                Console.WriteLine($"Это: {card}");
            }
            catch
            {
                // Обработка ошибок ввода (нечисловые данные)
                Console.WriteLine("Ошибка!");
            }
        }

        // Задание 3: Определение напитка по профессии 
        //  Пользователь вводит профессию. Программа возвращает рекомендуемый напиток в зависимости от профессии.
        // Используется switch выражение для сравнения строк в нижнем регистре.
        void Task3()
        {
            Console.WriteLine($"Задание 3 \n Программа принимает строку и выводит данные согласно введенным значениям");

            string stroka = Console.ReadLine();

            // Сопоставление профессии (в нижнем регистре) с напитком
            string outt = stroka.ToLower() switch
            {
                "jabroni" => "Patron Tequlla",
                "school counselor" => "Anything with Alcohol",
                "programmer" => "Hipster Craft Beer",
                "bike gang member" => "moonshine",
                "politician" => "your tax dollars",
                "rapper" => "cristal",
                _ => "Beer" // Напиток по умолчанию для всех остальных профессий
            };

            Console.WriteLine($"{outt}");
        }

        // Задание 4: Определение дня недели по его номеру
        // Пользователь вводит число от 1 до 7. Программа возвращает название дня недели на русском, используя перечисление DayOfWeek.
        void Task4()
        {
            Console.WriteLine("Задание 4 \n Выводит в зависимости от введенной цифры день недели");

            try
            {
                Console.Write("Введите номер дня недели (1-7): ");
                int dayNumber = int.Parse(Console.ReadLine());

                string dayName = GetDayName(dayNumber);
                Console.WriteLine($"День недели: {dayName}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите число от 1 до 7");
            }
        }

        // Вспомогательный метод для Task4: преобразует номер дня в название с помощью перечисления DayOfWeek
        static string GetDayName(int dayNumber)
        {
            return dayNumber switch
            {
                1 => DayOfWeek.Понедельник.ToString(),
                2 => DayOfWeek.Вторник.ToString(),
                3 => DayOfWeek.Среда.ToString(),
                4 => DayOfWeek.Четверг.ToString(),
                5 => DayOfWeek.Пятница.ToString(),
                6 => DayOfWeek.Суббота.ToString(),
                7 => DayOfWeek.Воскресенье.ToString(),
                _ => "Неверный номер дня" // Значение при неверном номере
            };
        }

        // Задание 5: Подсчет определенных игрушек в массиве
        //  Имеется массив строк с названиями игрушек. Нужно подсчитать, сколько раз встречаются куклы "Hello Kitty" или "Barbie doll".
        // Результат — количество найденных кукол.
        void Task5()
        {
            Console.WriteLine("Задание 5 \n При встрече 'Hello Kitty' или 'Barbie doll' необходимо положить их в 'сумку' и вывести сколько там кукол");

            string[] toys = { "Мяч", "Hello Kitty", "Машинка", "Barbie doll", "Кубик", "Hello Kitty", "Медведь" };

            int dollCount = 0;
            // Перебор всех игрушек в массиве
            foreach (string toy in toys)
            {
                // Если игрушка - одна из кукол, увеличиваем счетчик
                if (toy == "Hello Kitty" || toy == "Barbie doll")
                {
                    dollCount++;
                }
            }

            Console.WriteLine($"\nВсего кукол в сумке: {dollCount}");
        }
    }
}
