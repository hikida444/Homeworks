using System;

class Program
{
    static void Main()
    {
        // суть задачи: написать программу которая возвращает максимальное из двух чисел
        Console.WriteLine("Введите число 1: ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите число 2: ");
        int y = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(Maximum(x, y));  // обращаемся к методу и возвращаем макс.
    }

    static int Maximum(int x, int y) // метод который возвращает максимальное из двух чисел (для hw1)
    {
        int a = Math.Max(x, y); 
        return a; 
    }
}
