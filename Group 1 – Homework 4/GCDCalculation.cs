using System;

class Program
{
    static void Main()
    {
        // суть задачи: написать программу для вычисления НОД двух и трех чисел
        Console.Write("Введите первое число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите третье число: ");
        int c = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"\nНОД({a}, {b}) = {nod(a, b)}");
        Console.WriteLine($"НОД({a}, {b}, {c}) = {nod(a, b, c)}");
    }

    static int nod(int a, int b)  // НОД двух чисел (алгоритм Евклида)
    {
        while (b != 0) 
        {
            int t = b;
            b = a % b; 
            a = t;    
        }
        return a;
    }

    static int nod(int a, int b, int c) // НОД трех чисел через перегрузку метода
    {
        return nod(nod(a, b), c); 
    }
}
