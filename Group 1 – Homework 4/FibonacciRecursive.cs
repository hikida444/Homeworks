using System;

class Program
{
    static void Main()
    {
        // суть задачи: программа реализует рекурсивную функцию Фибоначчи
        Console.Write("Введите n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"F({n}) = {Fibbonachi(n)}");
    }

    static int Fibbonachi(int n) // рекурсивная функция Фибоначчи
    {
        if (n == 1 || n == 2) 
            return 1;
        return Fibbonachi(n - 1) + Fibbonachi(n - 2); 
    }
}
