using System;

class Program
{
    static void Main()
    {
        // суть задачи: написать программу, которая обращается к методу который меняет параметры местами
        Console.Write("Введите первое число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"\nДо обмена: a = {a}, b = {b}");

        Swap(ref a, ref b); // передача параметров по ссылке 

        Console.WriteLine($"После обмена: a = {a}, b = {b}");
    }

    static void Swap(ref int x, ref int y) // метод, который меняет числа местами
    {
        int temp = x; 
        x = y;        
        y = temp;     
    }
}
