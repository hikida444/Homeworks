using System;

class Program
{
    static void Main()
    {
        // суть задачи: возвращение факториала числа с помощью рекурсии
        Console.Write("Введите число: ");
        long x = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine($"Факториал числа равен {Factorial1(x)}");
        // работает только до 20 включительно ибо long переполняется
    }

    static long Factorial1(long x) // метод для возвращения факториала числа (рекурсивный)
    {
        if (x == 1) return 1; 
        else return x * Factorial1(x - 1); 
    }
}
