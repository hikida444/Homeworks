using System;

class Program
{
    static void Main()
    {
        // создаем массив из 20 случайных чисел
        int[] m = new int[20];
        Random rnd = new Random();

        // заполняем массив случайными числами от 1 до 98
        for (int i = 0; i < 20; i++)
        {
            m[i] = rnd.Next(1, 99);
        }

        // выводим массив на экран
        Console.WriteLine("Исходный массив:");
        Console.WriteLine(string.Join(", ", m));

        // вводим два числа, которые хотим поменять местами
        Console.Write("\nВведите первое число из массива: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число из массива: ");
        int b = Convert.ToInt32(Console.ReadLine());

        // находим, где эти числа находятся в массиве
        int indA = Array.IndexOf(m, a);
        int indB = Array.IndexOf(m, b);

        // проверяем, что оба числа найдены
        if (indA == -1 || indB == -1)
        {
            Console.WriteLine("\nОшибка: одно или оба числа не найдены в массиве!");
        }
        else
        {
            // меняем числа местами с помощью временной переменной
            int temp = m[indA];
            m[indA] = m[indB];
            m[indB] = temp;

            // выводим новый массив
            Console.WriteLine("\nМассив после обмена:");
            Console.WriteLine(string.Join(", ", m));
        }
    }
}
