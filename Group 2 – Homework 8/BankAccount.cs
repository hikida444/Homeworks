using System;

class Programm
{
    public static void Main()
    {
        Bank.GetInfo();
    }
}

// Перечисление, описывающее тип банковского счёта
enum BankAccount
{
    Current, // Текущий счёт
    Saving   // Сберегательный счёт
}

class Bank
{
    // Номер банковского счёта (может быть null до генерации)
    private string? numberOfAccount;

    // Баланс счёта
    private decimal balanceOfAccount;

    // Тип банковского счёта (Current / Saving)
    private BankAccount bankAccountType;

    // Конструктор класса Bank
    // Вызывается при создании нового счёта
    public Bank(decimal BalanceOfAccount, BankAccount bankAccount)
    {
        // Устанавливаем начальный баланс
        balanceOfAccount = BalanceOfAccount;

        // Устанавливаем тип счёта
        bankAccountType = bankAccount;
    }

    // Возвращает текущий баланс счёта
    public decimal GetBalance()
    {
        return balanceOfAccount;
    }

    // Устанавливает новый баланс счёта
    public void SetBalance(decimal Balance)
    {
        balanceOfAccount = Balance;
    }

    // Возвращает номер банковского счёта
    public string GetNumber()
    {
        // Оператор ! говорит компилятору, что значение не равно null
        return numberOfAccount!;
    }

    // Генерирует номер банковского счёта
    public void GeneratedNumber()
    {
        // Создаём генератор случайных чисел
        Random random = new();

        // Массив для хранения частей номера счёта
        var parts = new string[4];

        // Генерируем 4 части номера
        for (int i = 0; i < 4; i++)
        {
            // Каждая часть — 4 цифры
            parts[i] = random.Next(1000, 10000).ToString("D4");
        }

        // Соединяем части через дефис
        numberOfAccount = string.Join("-", parts);
    }

    // Устанавливает тип банковского счёта
    public void SetAccountType(BankAccount AccountType)
    {
        bankAccountType = AccountType;
    }

    // Возвращает тип банковского счёта
    public BankAccount PrintAccountType()
    {
        return bankAccountType;
    }

    // Метод для снятия денег со счёта
    // Возвращает true, если операция успешна
    public bool GetMoney(decimal count)
    {
        // Проверяем, хватает ли денег на счёте
        if (count <= balanceOfAccount)
        {
            // Уменьшаем баланс
            balanceOfAccount -= count;
            return true;
        }
        else
        {
            // Если денег недостаточно — операция не выполняется
            return false;
        }
    }

    // Метод для пополнения счёта
    public void GiveMoney(decimal num)
    {
        balanceOfAccount += num;
    }

    // Статический метод для перевода денег между счетами
    // Принимает список всех существующих счетов
    public static void TransferMoney(List<Bank> accounts)
    {
        Console.Clear();
        Console.WriteLine(new string('-', 60));

        // Внешний цикл — повтор выбора счетов
        while (true)
        {
            // Выводим список всех счетов
            for (var i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}");
                Console.WriteLine(new string('-', 60));
            }

            // Выбор счёта-источника
            Console.WriteLine($"Выберите счёт С которого вы хотите перевести деньги (введите номер от 1 до {accounts.Count})");
            Console.WriteLine(new string('-', 60));

            var answer = Console.ReadLine()!;
            Console.WriteLine(new string('-', 60));

            int fromBalance;

            // Пытаемся преобразовать ввод в число
            try
            {
                var x = int.Parse(answer);
                fromBalance = x;
            }
            catch (ArgumentException)
            {
                // Если пользователь ввёл не число
                Console.WriteLine("Вы ввели не число! Ну-ка попробуйте снова!");
                Console.WriteLine(new string('-', 60));
                continue;
            }

            Console.Clear();

            // Повторно выводим список счетов,
            // помечая выбранный счёт-источник
            for (var i = 0; i < accounts.Count; i++)
            {
                if (fromBalance == i + 1)
                {
                    Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}  <--- переводим отсюда");
                    Console.WriteLine(new string('-', 60));
                    continue;
                }
                Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}");
                Console.WriteLine(new string('-', 60));
            }

            // Выбор счёта-получателя
            Console.WriteLine($"А теперь выберите счёт НА который вы хотите перевести деньги (введите номер от 1 до {accounts.Count})");
            Console.WriteLine(new string('-', 60));

            var toGive = Console.ReadLine()!;
            int toBalance;

            try
            {
                var x = int.Parse(toGive);
                toBalance = x;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Вы ввели не число! Ну-ка попробуйте снова!");
                continue;
            }

            Console.Clear();

            // Отображаем оба выбранных счёта
            for (var i = 0; i < accounts.Count; i++)
            {
                if (fromBalance == i + 1)
                {
                    Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}  <--- переводим отсюда");
                    Console.WriteLine(new string('-', 60));
                    continue;
                }
                if (toBalance == i + 1)
                {
                    Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}  <--- переводим сюда");
                    Console.WriteLine(new string('-', 60));
                    continue;
                }
                Console.WriteLine($"{i + 1}) {accounts[i].GetNumber()}");
                Console.WriteLine(new string('-', 60));
            }

            // Внутренний цикл — ввод суммы перевода
            while (true)
            {
                Console.Write($"Введите сумму которую хотите перевести (не более {accounts[fromBalance - 1].GetBalance()}): ");
                var sum = Console.ReadLine()!;
                int summa;

                try
                {
                    summa = int.Parse(sum);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Вы ввели не число! Ну-ка попробуйте снова!");
                    continue;
                }

                // Сохраняем старые балансы всех счетов
                List<decimal> oldBalance = new();
                foreach (var i in accounts)
                {
                    oldBalance.Add(i.GetBalance());
                }

                // Проверяем корректность суммы
                if (summa >= 0 && summa <= accounts[fromBalance - 1].GetBalance())
                {
                    // Снимаем деньги с одного счёта
                    accounts[fromBalance - 1].GetMoney(summa);

                    // Кладём деньги на другой счёт
                    accounts[toBalance - 1].GiveMoney(summa);

                    // Выводим информацию о переводе
                    Console.WriteLine($"Аккаунт: {accounts[fromBalance - 1].GetNumber()}");
                    Console.WriteLine($"Баланс: {oldBalance[0]} ---> {accounts[fromBalance - 1].GetBalance()}");
                    Console.WriteLine(new string('-', 60));
                    Console.WriteLine($"Аккаунт: {accounts[toBalance - 1].GetNumber()}");
                    Console.WriteLine($"Баланс: {oldBalance[1]} ---> {accounts[toBalance - 1].GetBalance()}");
                }
                else
                {
                    Console.WriteLine("Сумма превышает допустимое значение. Попробуйте снова!");
                    continue;
                }
                break;
            }
            break;
        }
    }

    // Главный метод меню программы
    public static void GetInfo()
    {
        // Список всех созданных банковских счетов
        List<Bank> accounts = [];

        // Основной цикл работы программы
        while (true)
        {
            Console.WriteLine("Здравствуйте! Вот меню доступных для вас действий:" +
                "\n1) Создать счёт" +
                "\n2) Получить баланс" +
                "\n3) Узнать тип счёта" +
                "\n4) Снять сумму" +
                "\n5) Положить деньги на счёт" +
                "\n6) Перевести деньги с одного счёта на другой");
            Console.WriteLine(new string('-', 60));

            // Обработка выбора пользователя
            switch (Console.ReadLine())
            {
                case "1":
                    // Создание нового счёта
                    Console.Write($"Введите баланс аккаунта: ");
                    var balance = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine(new string('-', 60));

                    BankAccount accountBank;

                    // Ввод типа счёта
                    while (true)
                    {
                        Console.Write($"\nВведите тип аккаунта (Saving, Current): ");
                        try
                        {
                            var account = Console.ReadLine()!;
                            accountBank = Enum.Parse<BankAccount>(account);
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Вы неверно вводите тип банковского счёта\nПопробуйте снова!");
                            continue;
                        }
                        break;
                    }

                    // Создание объекта Bank
                    var bank = new Bank(balance, accountBank);

                    Console.WriteLine(new string('-', 60));
                    accounts.Add(bank);

                    // Генерация номера счёта
                    bank.GeneratedNumber();

                    Console.WriteLine("Вы создали аккаунт!");
                    Console.WriteLine(new string('-', 60));
                    continue;

                case "2":
                    // Получение баланса
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int num = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Ваш баланс: {accounts[num - 1].balanceOfAccount}");
                    continue;

                case "3":
                    // Получение типа счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    Console.WriteLine(new string('-', 60));
                    int number = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(new string('-', 60));
                    Console.WriteLine($"Ваш тип счёта: {accounts[number - 1].PrintAccountType()}");
                    Console.WriteLine(new string('-', 60));
                    continue;

                case "4":
                    // Снятие денег
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int numb = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Ваш баланс: {accounts[numb - 1].GetBalance()}");
                    Console.WriteLine("Сколько денег вы хотите снять со счёта?");
                    decimal money = Convert.ToDecimal(Console.ReadLine());
                    accounts[numb - 1].GetMoney(money);
                    Console.WriteLine($"Ваш текущий баланс: {accounts[numb - 1].GetBalance()}");
                    continue;

                case "5":
                    // Пополнение счёта
                    Console.WriteLine($"Введите число от 1 до {accounts.Count}");
                    int num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Ваш баланс: {accounts[num1 - 1].GetBalance()}");
                    Console.WriteLine("Сколько денег вы хотите положить на счёт?");
                    decimal cash = Convert.ToDecimal(Console.ReadLine());
                    accounts[num1 - 1].GiveMoney(cash);
                    Console.WriteLine($"Ваш текущий баланс: {accounts[num1 - 1].GetBalance()}");
                    Console.WriteLine(new string('-', 60));
                    continue;

                case "6":
                    // Перевод денег
                    TransferMoney(accounts);
                    break;

                default:
                    Console.WriteLine("Вы ввели неверное значение попробуйте снова!");
                    Console.WriteLine(new string('-', 60));
                    continue;
            }

            // Запрос на продолжение работы
            Console.WriteLine("Вы хотите продолжить?");
            string mean = Console.ReadLine()!.ToLower();

            if (mean == "да")
            {
                Console.Clear();
                continue;
            }
            if (mean == "нет")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Не очень вас понял, но видимо продолжаем");
                Console.WriteLine(new string('-', 60));
                continue;
            }
        }
    }
}
