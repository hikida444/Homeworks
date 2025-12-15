namespace hw_9_2;

using System.Collections;

public enum AccountType
{
    Current,
    Savings
}

public class BankTransaction
{
    // дата/время фиксации операции (локальное время машины)
    public readonly DateTime TransactionDate;
    // сумма операции: положительная — пополнение, отрицательная — снятие
    public readonly double Amount;

    public BankTransaction(double amount)
    {
        // сохраняем момент совершения операции
        TransactionDate = DateTime.Now;
        Amount = amount;
    }
}

public class BankAccount
{
    // счетчик для генерации уникальных номеров счетов (в рамках одного запуска)
    private static int counter = 0;

    // основные поля состояния счета
    private int accountNumber;
    private double balance;
    private AccountType type;

    // история операций: складываем каждое пополнение/снятие
    private Queue transactions;

    // выдает следующий номер счета
    private static int GenerateAccountNumber()
    {
        counter++;
        return counter;
    }

    // создаем счет с дефолтными значениями и пустой историей операций
    public BankAccount()
    {
        accountNumber = GenerateAccountNumber();
        transactions = new Queue();
    }

    // создаем счет с начальным балансом
    public BankAccount(double balance)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
        transactions = new Queue();
    }

    // создаем счет с указанным типом
    public BankAccount(AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.type = type;
        transactions = new Queue();
    }

    // создаем счет с начальным балансом и типом
    public BankAccount(double balance, AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
        this.type = type;
        transactions = new Queue();
    }

    // номер счета не меняется после создания
    public int AccountNumber
    {
        get { return accountNumber; }
    }

    // баланс доступен для чтения и записи (в реальности лучше закрыть setter)
    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    // тип счета можно менять
    public AccountType Type
    {
        get { return type; }
        set { type = value; }
    }

    // пополнение: увеличиваем баланс и пишем транзакцию в очередь
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            transactions.Enqueue(new BankTransaction(amount));
        }
    }

    // снятие: проверяем сумму и доступный баланс, фиксируем транзакцию отрицательной суммой
    public bool Withdraw(double amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            transactions.Enqueue(new BankTransaction(-amount));
            return true;
        }
        return false;
    }

    // перевод: пробуем снять с текущего счета и пополнить целевой
    public bool Transfer(BankAccount targetAccount, double amount)
    {
        if (targetAccount == null) return false;
        if (amount <= 0) return false;

        if (this.Withdraw(amount))
        {
            targetAccount.Deposit(amount);
            return true;
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // создаем два счета и делаем стартовое пополнение второго
        BankAccount acc1 = new BankAccount(500, AccountType.Current);
        BankAccount acc2 = new BankAccount(AccountType.Savings);
        acc2.Deposit(100);

        // показываем исходные балансы
        Console.WriteLine($"Счет №{acc1.AccountNumber}, Баланс: {acc1.Balance}");
        Console.WriteLine($"Счет №{acc2.AccountNumber}, Баланс: {acc2.Balance}");

        // запрашиваем сумму перевода (лучше использовать TryParse вместо Parse)
        Console.Write($"Введите сумму которую хотите перевести с текущего счета № {acc1.AccountNumber}: ");
        int amount = int.Parse(Console.ReadLine());

        // выполняем перевод и выводим результат
        bool ok = acc1.Transfer(acc2, amount);
        Console.WriteLine(ok ? "Перевод выполнен успешно!" : "Перевод НЕ выполнен");

        // выводим остатки после перевода
        Console.WriteLine("\nОстаток на счетах после перевода: ");
        Console.WriteLine($"Счёт {acc1.AccountNumber}: {acc1.Balance}");
        Console.WriteLine($"Счёт {acc2.AccountNumber}: {acc2.Balance}");
    }
}
