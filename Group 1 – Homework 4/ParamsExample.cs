using System;

class Program
{
    static void Main()
    {
        long proiz = 1;  
        double average;     

        // вызов с отдельными числами
        int sum = MassiveWork(ref proiz, out average, 1, 2, 3, 4, 5);

        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Произведение: {proiz}");
        Console.WriteLine($"Среднее арифметическое: {average}");

        // вызов с массивом
        int[] arr = { 10, 20, 30 };
        int sum2 = MassiveWork(ref proiz, out average, arr);

        Console.WriteLine($"\nСумма: {sum2}");
        Console.WriteLine($"Произведение: {proiz}");
        Console.WriteLine($"Среднее арифметическое: {average}");
    }

    static int MassiveWork(ref long proiz, out double average, params int[] numbers)
    {
        proiz = 1; // сбрасываем произведение
        int sum = 0;

        foreach (int num in numbers)
        {
            sum += num;
            proiz *= num;
        }

        // вычисляем среднее (если передали хотя бы одно число)
        average = numbers.Length > 0 ? (double)sum / numbers.Length : 0;

        return sum;
    }
}
