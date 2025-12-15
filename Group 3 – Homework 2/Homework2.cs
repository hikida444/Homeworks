using System;

class Program
{
    // Перечисление для типа банковского счета
    enum AccountType
    {
        Current,      // Текущий счет
        Savings       // Сберегательный счет
    }

    // Структура для банковского счета
    struct BankAccount
    {
        public string Number;    // Номер счета
        public AccountType Type; // Тип счета 
        public decimal Balance;  // Баланс
    }

    // Перечисление для университетов
    enum University
    {
        KGU,  // КГУ
        KAI,  // КАИ
        KHTI  // КХТИ
    }

    // Структура для работника
    struct Employee
    {
        public string Name;      // Имя работника
        public University Univ;  // ВУЗ 
    }

    // Структура для напитка
    struct Drink
    {
        public string Name;      // Название напитка
        public double AlcoholPercent; // Процент спирта
    }

    // Структура для студента
    struct Student
    {
        public string LastName;   // Фамилия
        public string FirstName;  // Имя
        public int Id;           // Идентификатор
        public DateTime BirthDate; // Дата рождения
        public char AlcoholCategory; // Категория алкоголизма
        public double DrinkVolume;   // Объем выпитого
        public Drink DrinkType;     // Тип напитка
    }

    static void Main()
    {
        Console.WriteLine("3.1");
        AccountType myAccountType = AccountType.Savings; // Создание переменной типа AccountType
        Console.WriteLine($"Тип счета: {myAccountType}");

        // Создание структуры BankAccount, заполнение полей и вывод значений
        Console.WriteLine("\n3.2");
        BankAccount account; // Объявление структуры
        account.Number = "1234567890";
        account.Type = AccountType.Current;
        account.Balance = 100;
        Console.WriteLine($"Номер счета: {account.Number}");
        Console.WriteLine($"Тип счета: {account.Type}");
        Console.WriteLine($"Баланс: {account.Balance} руб.");

        // Создание структуры Employee с полем типа University
        Console.WriteLine("\nДомашнее задание 1");
        Employee worker;
        worker.Name = "Анастасия";
        worker.Univ = University.KGU;
        Console.WriteLine($"Имя: {worker.Name}");
        Console.WriteLine($"ВУЗ: {worker.Univ}");

        // Вывод минимальных и максимальных значений для int, double и decimal
        Console.WriteLine("\nЗадача 1");
        Console.WriteLine("Тип данных int - максимальное значение: " + int.MaxValue + " - минимальное значение: " + int.MinValue);
        Console.WriteLine("Тип данных double - максимальное значение: " + double.MaxValue + " - минимальное значение: " + double.MinValue);
        Console.WriteLine("Тип данных decimal - максимальное значение: " + decimal.MaxValue + " - минимальное значение: " + decimal.MinValue);

        // Чтение строки, преобразование строчных букв в заглавные и наоборот
        Console.WriteLine("\nЗадача 2");
        string inputString = Console.ReadLine(); // Чтение ввода пользователя
        string resultString = "";
        foreach (char c in inputString)
        {
            if (char.IsLower(c))
                resultString += char.ToUpper(c); // Преобразование в верхний регистр
            else if (char.IsUpper(c))
                resultString += char.ToLower(c); // Преобразование в нижний регистр
            else
                resultString += c; // Символы, не являющиеся буквами, остаются без изменений
        }
        Console.WriteLine($"Результат: {resultString}");

        
        // Поиск всех вхождений подстроки с использованием IndexOf
        Console.WriteLine("\nЗадача 3");
        Console.WriteLine("Введите строку:");
        string mainString = Console.ReadLine();
        Console.WriteLine("Введите подстроку для поиска:");
        string subString = Console.ReadLine();
        int count = 0;
        int index = 0;
        while ((index = mainString.IndexOf(subString, index)) != -1)
        {
            count++; // Увеличение счетчика найденных вхождений
            index += subString.Length; // Перескакиваем через уже найденную часть строки
                                       // чтобы избежать повторного нахождения того же вхождения
        }
        Console.WriteLine($"Подстрока '{subString}' встречается {count} раз");

        // Вычисление экономии на одной бутылке и расчет необходимого количества
        Console.WriteLine("\nЗадача 4");
        Console.Write("Введите обычную цену бутылки: ");
        int normPrice = int.Parse(Console.ReadLine());
        Console.Write("Введите размер скидки в Duty Free (в процентах): ");
        int salePrice = int.Parse(Console.ReadLine());
        Console.Write("Введите стоимость отпуска: ");
        int holidayPrice = int.Parse(Console.ReadLine());
        double savingPerBottle = normPrice * (salePrice / 100.0); // Экономия на одной бутылке
        int bottlesNeeded = (int)Math.Floor(holidayPrice / savingPerBottle); // Расчет количества
        Console.WriteLine($"Нужно купить {bottlesNeeded} бутылок");

        // Создание массива структур Student, расчет индивидуальных и общих показателей
        Console.WriteLine("\nЗадача 5");

        // Создание напитков
        Drink beer = new Drink { Name = "Пиво", AlcoholPercent = 5 };
        Drink wine = new Drink { Name = "Вино", AlcoholPercent = 15 };
        Drink vodka = new Drink { Name = "Водка", AlcoholPercent = 40 };
        Drink juice = new Drink { Name = "Сок", AlcoholPercent = 0 };
        Drink tea = new Drink { Name = "Чай", AlcoholPercent = 0 };

        // Создание студентов с их данными
        Student student1 = new Student
        {
            LastName = "Кокина",
            FirstName = "Вероника",
            Id = 1,
            BirthDate = new DateTime(2006, 2, 18),
            AlcoholCategory = 'a',
            DrinkVolume = 2.5,
            DrinkType = beer
        };
        // аналогично для student2-student5

        Student student2 = new Student
        {
            LastName = "Макарова",
            FirstName = "Дарина",
            Id = 2,
            BirthDate = new DateTime(2007, 9, 8),
            AlcoholCategory = 'b',
            DrinkVolume = 1.0,
            DrinkType = tea
        };

        Student student3 = new Student
        {
            LastName = "Резник",
            FirstName = "Владимир",
            Id = 3,
            BirthDate = new DateTime(2006, 9, 30),
            AlcoholCategory = 'c',
            DrinkVolume = 0.5,
            DrinkType = vodka
        };

        Student student4 = new Student
        {
            LastName = "Гаипов",
            FirstName = "Айдар",
            Id = 4,
            BirthDate = new DateTime(2007, 6, 23),
            AlcoholCategory = 'd',
            DrinkVolume = 1.5,
            DrinkType = juice
        };

        Student student5 = new Student
        {
            LastName = "Хадиев",
            FirstName = "Раиль",
            Id = 5,
            BirthDate = new DateTime(2007, 10, 3),
            AlcoholCategory = 'b',
            DrinkVolume = 3.0,
            DrinkType = wine
        };


        // Расчет общих объемов
        double totalVolume = 0;
        double totalAlcohol = 0;
        // Для каждого студента добавляем его объем и вычисляем алкоголь
        totalVolume += student1.DrinkVolume;
        totalAlcohol += student1.DrinkVolume * (student1.DrinkType.AlcoholPercent / 100);
        // аналогично для других студентов\

        totalVolume += student2.DrinkVolume;
        totalAlcohol += student2.DrinkVolume * (student2.DrinkType.AlcoholPercent / 100);

        totalVolume += student3.DrinkVolume;
        totalAlcohol += student3.DrinkVolume * (student3.DrinkType.AlcoholPercent / 100);

        totalVolume += student4.DrinkVolume;
        totalAlcohol += student4.DrinkVolume * (student4.DrinkType.AlcoholPercent / 100);

        totalVolume += student5.DrinkVolume;
        totalAlcohol += student5.DrinkVolume * (student5.DrinkType.AlcoholPercent / 100);


        // Вывод информации по каждому студенту
        Console.WriteLine("\nИнформация о студентах:");
        // Для каждого студента:
        // 1. Рассчитываем количество чистого алкоголя
        // 2. Рассчитываем процент от общего объема
        // 3. Рассчитываем процент от общего алкоголя (с проверкой деления на ноль)
        // 4. Выводим форматированную информацию

        / Студент 1
        double studentAlcohol1 = student1.DrinkVolume * (student1.DrinkType.AlcoholPercent / 100);
        double volumePercent1 = (student1.DrinkVolume / totalVolume) * 100;

        double alcoholPercent1;

        if (totalAlcohol > 0)  // если есть алкоголь
        {
            alcoholPercent1 = (studentAlcohol1 / totalAlcohol) * 100;
        }
        else  // если алкоголя нет
        {
            alcoholPercent1 = 0;
        }


        Console.WriteLine($"{student1.LastName} {student1.FirstName}:");
        Console.WriteLine($"  Напиток: {student1.DrinkType.Name}");
        Console.WriteLine($"  Выпито: {student1.DrinkVolume} л");
        Console.WriteLine($"  Алкоголя: {studentAlcohol1:F2} л");
        Console.WriteLine($"  % от общего объема: {volumePercent1:F1}%");
        Console.WriteLine($"  % от общего алкоголя: {alcoholPercent1:F1}%");

        // Студент 2
        double studentAlcohol2 = student2.DrinkVolume * (student2.DrinkType.AlcoholPercent / 100);
        double volumePercent2 = (student2.DrinkVolume / totalVolume) * 100;

        double alcoholPercent2;

        if (totalAlcohol > 0)  // если есть алкоголь
        {
            alcoholPercent2 = (studentAlcohol2 / totalAlcohol) * 100;
        }
        else  // если алкоголя нет
        {
            alcoholPercent2 = 0;
        }


        Console.WriteLine($"{student2.LastName} {student2.FirstName}:");
        Console.WriteLine($"  Напиток: {student2.DrinkType.Name}");
        Console.WriteLine($"  Выпито: {student2.DrinkVolume} л");
        Console.WriteLine($"  Алкоголя: {studentAlcohol2:F2} л");
        Console.WriteLine($"  % от общего объема: {volumePercent2:F1}%");
        Console.WriteLine($"  % от общего алкоголя: {alcoholPercent2:F1}%");

        // Студент 3
        double studentAlcohol3 = student3.DrinkVolume * (student3.DrinkType.AlcoholPercent / 100);
        double volumePercent3 = (student3.DrinkVolume / totalVolume) * 100;
        double alcoholPercent3;

        if (totalAlcohol > 0)  // если есть алкоголь
        {
            alcoholPercent3 = (studentAlcohol3 / totalAlcohol) * 100;
        }
        else  // если алкоголя нет
        {
            alcoholPercent3 = 0;
        }


        Console.WriteLine($"{student3.LastName} {student3.FirstName}:");
        Console.WriteLine($"  Напиток: {student3.DrinkType.Name}");
        Console.WriteLine($"  Выпито: {student3.DrinkVolume} л");
        Console.WriteLine($"  Алкоголя: {studentAlcohol3:F2} л");
        Console.WriteLine($"  % от общего объема: {volumePercent3:F1}%");
        Console.WriteLine($"  % от общего алкоголя: {alcoholPercent3:F1}%");

        // Студент 4
        double studentAlcohol4 = student4.DrinkVolume * (student4.DrinkType.AlcoholPercent / 100);
        double volumePercent4 = (student4.DrinkVolume / totalVolume) * 100;
        double alcoholPercent4;

        if (totalAlcohol > 0)  // если есть алкоголь
        {
            alcoholPercent4 = (studentAlcohol4 / totalAlcohol) * 100;
        }
        else  // если алкоголя нет
        {
            alcoholPercent4 = 0;
        }


        Console.WriteLine($"{student4.LastName} {student4.FirstName}:");
        Console.WriteLine($"  Напиток: {student4.DrinkType.Name}");
        Console.WriteLine($"  Выпито: {student4.DrinkVolume} л");
        Console.WriteLine($"  Алкоголя: {studentAlcohol4:F2} л");
        Console.WriteLine($"  % от общего объема: {volumePercent4:F1}%");
        Console.WriteLine($"  % от общего алкоголя: {alcoholPercent4:F1}%");

        // Студент 5
        double studentAlcohol5 = student5.DrinkVolume * (student5.DrinkType.AlcoholPercent / 100);
        double volumePercent5 = (student5.DrinkVolume / totalVolume) * 100;

        double alcoholPercent5;

        if (totalAlcohol > 0)  // если есть алкоголь
        {
            alcoholPercent5 = (studentAlcohol5 / totalAlcohol) * 100;
        }
        else  // если алкоголя нет
        {
            alcoholPercent5 = 0;
        }

        Console.WriteLine($"{student5.LastName} {student5.FirstName}:");
        Console.WriteLine($"  Напиток: {student5.DrinkType.Name}");
        Console.WriteLine($"  Выпито: {student5.DrinkVolume} л");
        Console.WriteLine($"  Алкоголя: {studentAlcohol5:F2} л");
        Console.WriteLine($"  % от общего объема: {volumePercent5:F1}%");
        Console.WriteLine($"  % от общего алкоголя: {alcoholPercent5:F1}%");

        // Вывод общей статистики
        Console.WriteLine($"\nОбщая статистика:");
        Console.WriteLine($"Всего выпито жидкости: {totalVolume:F2} л");
        Console.WriteLine($"Всего выпито алкоголя: {totalAlcohol:F2} л");
    }
}
