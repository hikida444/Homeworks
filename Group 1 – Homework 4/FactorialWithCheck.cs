using System;

class Program
{
    static void Main()
    {
        // суть задачи: написать программу для возвращение факториала числа 
        Console.Write("Введите число: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (Factorial(n, out long result)) 
            Console.WriteLine($"Факториал числа {n} = {result}");
        else
            Console.WriteLine("Произошло переполнение! Число слишком большое для типа long.");
    }

    static bool Factorial(int n, out long result)  // метод вычисляет факториал числа n
    {
        result = 1; 

        try
        {
            checked  // отслеживаем переполнение
            {
                for (int i = 2; i <= n; i++)
                {
                    result *= i; 
                }
            }
            return true;
        }
        catch (OverflowException) // ловим исключение переполнения
        {
            result = 0; 
            return false; 
        }
    }
}
