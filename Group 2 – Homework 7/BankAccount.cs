using System;

class Programm
{
    public static void Main()
    {
        Bank.GetInfo();
    }
}

// Перечисление для типов банковских счетов
enum BankAccount
{
    Current, // Текущий счёт
    Saving   // Сберегательный счёт
}

class Bank
{
    private static int numberOfAccount; // Статический счётчик для уникальных номеров счетов
                                        // Общий для всех объектов Bank
    private decimal balanceOfAccount;   // Баланс конкретного счёта
    private BankAccount bankAccountType; // Тип конкретного счёта (Current или Saving)

    // Конструктор создаёт новый счёт с указанным балансом и типом
    public Bank(decimal BalanceOfAccount, BankAccount bankAccount)
    {
        numberOfAccount = GeneratedNumber(); // Генерируем уникальный номер счёта
        balanceOfAccount = BalanceOfAccount; // Устанавливаем баланс
        bankAccountType = bankAccount;       // Устанавливаем тип счёта
    }

    // Возвращает текущий баланс счёта
    public decimal PrintBalance()
    {
        return balanceOfAccount;
    }

    // Изменяет баланс счёта
    public void SetBalance(decimal Balance)
    {
        balanceOfAccount = Balance;
    }

    // Возвращает форматированный номер счёта в виде ####-####-####-####
    public static string PrintNumber()
    {
        // Преобразуем номер счёта в int и форматируем
        string result = string.Format("{0:####-####-####-####}", Convert.ToInt32(numberOfAccount));
        return result;
    }

    // Генерация нового номера счёта
    public static int GeneratedNumber()
    {
        // Конвертация текущего номера (не обязательно, просто демонстрирует преобразование типов)
        Convert.ToInt32(numberOfAccount);

        numberOfAccount += 1; // Увеличиваем счетчик на 1, чтобы следующий счёт имел уникальный номер
        return numberOfAccount; // Возвращаем новый номер
    }

    // Устанавливает тип счёта
    public void SetAccountType(BankAccount AccountType)
    {
        bankAccountType = AccountType;
    }

    // Возвращает тип счёта
    public BankAccount PrintAccountType()
    {
        return bankAccountType;
    }

    // Метод для снятия денег со счёта
    // Проверяет, хватает ли денег на балансе
    public bool GetMoney(decimal count)
    {
        if (count <= balanceOfAccount) // Если денег хватает
        {
            balanceOfAccount -= count; // Вычитаем сумму
            return true;               // Возвращаем успешный результат
        }
        else
        {
            return false;              // Если денег не хватает, операция не выполнена
        }
    }

    // Метод для добавления денег на счёт
    public void GiveMoney(decimal num)
    {
        balanceOfAccount += num; // Добавляем деньги к балансу
    }

    // Основной статический метод для работы с пользователем через консоль
    public static void GetInfo()
    {
        List<Bank> accounts = []; // Список для хранения всех созданных банковских счетов
                                   // Позволяет работать с множеством аккаунтов одновременно

        while (true) // Основной цикл меню — повторяется, пока пользователь не выйдет
        {
            // Выводим меню действий
            Console.WriteLine("Здравствуйте! Вот меню доступных для вас действий:" +
                "\n1) Создать счёт" +
                "\n2) Получить баланс" +
                "\n3) Узнать тип счёта" +
                "\n4) Снять сумму" +
                "\n5) Положить деньги на счёт");
            Console.WriteLine(new string('-', 60));

            // Обработка выбора пользователя
            switch (Console.ReadLine())
            {
                case "1": // Создание нового счёта
                   Console.Write($"Введите баланс аккаунта: ");
                   var balance = Convert.ToDecimal(Console.ReadLine()); // Конвертируем ввод в decimal
                   Console.WriteLine(new string('-', 60));
                   Console.Write($"\nВведите тип аккаунта: ");
                   // Проверяем, соответствует ли введённое значение одному из типов BankAccount
                   if (!Enum.TryParse(Console.ReadLine(), out BankAccount account))
                   {
                       Console.WriteLine("Вы неверно вводите тип банковского счёта");
                   }
                   Console.WriteLine(new string('-', 60));
                   accounts.Add(new Bank(balance, account)); // Создаём объект Bank и добавляем в список
                   var Number = PrintNumber(); // Получаем номер нового счёта
                   Console.WriteLine("Вы создали аккаунт!");
                   Console.WriteLine(new string('-', 60));
                   continue; // Возврат к меню

                case "2": // Получение баланса выбранного счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int num = Convert.ToInt32(Console.ReadLine()); // Пользователь выбирает счёт
                    // Проверка корректности ввода
                    if (num <= 0 && num > accounts.Count)
                    {
                        Console.WriteLine("Вы ввели неверное число");
                        break;
                    }
                    Console.WriteLine($"Ваш баланс: {accounts[num - 1].balanceOfAccount}"); // Выводим баланс
                    continue;

                case "3": // Получение типа счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    Console.WriteLine(new string('-', 60));
                    int number = Convert.ToInt32(Console.ReadLine());
                    if (number <= 0 && number > accounts.Count) // Проверка корректности
                    {
                        Console.WriteLine("Вы ввели неверное число");
                        break;
                    }
                    Console.WriteLine(new string('-', 60));
                    // Выводим тип счёта
                    Console.WriteLine($"Ваш тип счёта: {accounts[number - 1].PrintAccountType()}");
                    Console.WriteLine(new string('-', 60));
                    continue;

                case "4": // Снятие денег со счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int numb = Convert.ToInt32(Console.ReadLine());
                    if (numb <= 0 && numb > accounts.Count) // Проверка корректности ввода
                    {
                        Console.WriteLine("Вы ввели неверное число");
                        break;
                    }
                    Console.WriteLine($"Ваш баланс: {accounts[numb - 1].PrintBalance()}");
                    Console.WriteLine("Сколько денег вы хотите снять со счёта?");
                    decimal money = Convert.ToDecimal(Console.ReadLine()); // Ввод суммы для снятия
                    accounts[numb - 1].GetMoney(money); // Пытаемся снять деньги
                    Console.WriteLine($"Ваш текущий баланс: {accounts[numb - 1].PrintBalance()}"); // Отображаем новый баланс
                    continue;

                case "5": // Пополнение счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int num1 = Convert.ToInt32(Console.ReadLine());
                    if (num1 <= 0 && num1 > accounts.Count) // Проверка корректности
                    {
                        Console.WriteLine("Вы ввели неверное число");
                        break;
                    }
                    Console.WriteLine($"Ваш баланс: {accounts[num1 - 1].PrintBalance()}");
                    Console.WriteLine("Сколько денег вы хотите положить на счёт?");
                    decimal cash = Convert.ToDecimal(Console.ReadLine()); // Ввод суммы пополнения
                    accounts[num1 - 1].GiveMoney(cash); // Добавляем деньги на счёт
                    Console.WriteLine($"Ваш текущий баланс: {accounts[num1 - 1].PrintBalance()}"); // Показываем обновлённый баланс
                    Console.WriteLine(new string('-', 60));
                    continue;

                default: // Если пользователь ввёл неправильное значение
                    Console.WriteLine("Вы ввели неверное значение попробуйте снова!");
                    Console.WriteLine(new string('-', 60));
                    continue;
            }

            // Запрос пользователю о продолжении работы
            Console.WriteLine("Вы хотите продолжить?");
            string mean = Console.ReadLine()!.ToLower(); // Приводим ответ к нижнему регистру для проверки
            if (mean == "да")
            {
                Console.WriteLine(new string('-', 60));
                continue; // Продолжаем цикл меню
            }
            if (mean == "нет")
            {
                Console.WriteLine(new string('-', 60));
                break; // Прерываем цикл — выход из программы
            }
            Console.WriteLine(new string('-', 60));
            break;
        }
    }
}
