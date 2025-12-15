namespace hw_9_1;

public enum AccountType
{
    Current,
    Savings
}

public class BankAccount
{
    private static int counter = 0;  
    
    private int accountNumber;
    private double balance;
    private AccountType type;
    
    private static int GenerateAccountNumber()
    {
        counter++;
        return counter;
    }

    public BankAccount()
    {
        accountNumber = GenerateAccountNumber();
    }
    
    public BankAccount(double balance)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
    }
    
    public BankAccount(AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.type = type;
    }
    
    public BankAccount(double balance, AccountType type)
    {
        accountNumber = GenerateAccountNumber();
        this.balance = balance;
        this.type = type;
    }


    public int AccountNumber
    {
        get { return accountNumber; }
    }

    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    public AccountType Type
    {
        get { return type; }
        set { type = value; }
    }

    public void Deposit(double amount) 
    {
        if (amount > 0)
            balance += amount;
    }

    public bool Withdraw(double amount) 
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            return true;
        }
        return false;
    }

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
        BankAccount acc1 = new BankAccount(500, AccountType.Current);
        BankAccount acc2 = new BankAccount(AccountType.Savings);
        acc2.Deposit(100);
        
        Console.WriteLine($"Счет №{acc1.AccountNumber}, Баланс: { acc1.Balance}");
        Console.WriteLine($"Счет №{acc2.AccountNumber}, Баланс: { acc2.Balance}");
        
        Console.Write($"Введите сумму которую хотите перевести с текущего счета № {acc1.AccountNumber}: ");
        int amount = int.Parse(Console.ReadLine());
        bool ok = acc1.Transfer(acc2, amount);
        Console.WriteLine(ok ? "Перевод выполнен успешно!" : "Перевод НЕ выполнен");

        Console.WriteLine("\nОстаток на счетах после перевода: ");
        Console.WriteLine($"Счёт {acc1.AccountNumber}: {acc1.Balance}");
        Console.WriteLine($"Счёт {acc2.AccountNumber}: {acc2.Balance}");

    }
}
