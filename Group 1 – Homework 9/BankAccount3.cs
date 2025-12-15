namespace hw_9_3;

using System.Collections;
using System.IO;

public enum AccountType
{
    Current,
    Savings
}

public class BankTransaction
{
    // фиксируем дату/время операции (локальное время)
    public readonly DateTime TransactionDate;
    // сумма операции: + пополнение, - снятие
    public readonly double Amount;

    public BankTransaction(double amount)
    {
        TransactionDate = DateTime.Now;
        Amount = amount;
    }
}

public class BankAccount : IDisposable
{
    // счетчик для генерации уникальных номеров счетов (в рамках одного запуска)
    private static int counter = 0;

    // данные счета
    private int accountNumber;
    private double balance;
    private AccountType type;

    // очередь операций: добавляем транзакции по мере выполнения (FIFO)
    private Queue transactions;

    // выдает следующий номер счета
    private static int GenerateAccountNumber()
    {
        counter++;
        return counter;
    }

    // конструктор по умолчанию: нулевой баланс, пустая история операций
    public BankAccount()
    {
        accountNumber = GenerateAccountNumber();
        transactions = new Queue();
    }

    // конструктор с начальным балансом
    public BankAccount(double balance)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
        transactions = new Queue();
    }

    // конструктор с заданным типом счета
    public BankAccount(AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.type = type;
        transactions = new Queue();
    }

    // конструктор с балансом и типом
    public BankAccount(double balance, AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
        this.type = type;
        transactions = new Queue();
    }

    // номер счета только для чтения
    public int AccountNumber
    {
        get { return accountNumber; }
    }

    // баланс доступен на чтение/запись (в реальном проекте setter часто закрывают)
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

    // пополнение: обновляем баланс и пишем транзакцию в историю
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            transactions.Enqueue(new BankTransaction(amount));
        }
    }

    // снятие: проверяем, хватает ли денег; если да — списываем и фиксируем операцию
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

    // перевод: снимаем с текущего счета и кладем на целевой (если снятие прошло успешно)
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

    // освобождение ресурсов: здесь используем как точку сохранения истории операций в файл
    public void Dispose()
    {
        // имя файла зависит от номера счета, чтобы не перезаписывать другие счета
        string fileName = $"account_{accountNumber}_transactions.txt";

        // using гарантирует закрытие StreamWriter и освобождение ресурса файла
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine($"Транзакции по счету №{accountNumber}:");
            // перебираем все операции, которые были добавлены в очередь
            foreach (BankTransaction transaction in transactions)
            {
                writer.WriteLine($"{transaction.TransactionDate}: {transaction.Amount}");
            }
        }

        Console.WriteLine($"Транзакции сохранены в файл: {fileName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // создаем счет и выполняем несколько операций, чтобы появилась история
        BankAccount acc1 = new BankAccount(500, AccountType.Current);

        acc1.Deposit(100);
        acc1.Withdraw(50);
        acc1.Deposit(200);

        // выводим итоговый баланс
        Console.WriteLine($"Счет №{acc1.AccountNumber}, Баланс: {acc1.Balance}");

        // вручную вызываем Dispose; обычно лучше делать это через using(...)
        acc1.Dispose();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
