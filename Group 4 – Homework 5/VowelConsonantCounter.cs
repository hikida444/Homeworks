using System;
using System.IO;

namespace Tasks06.StringsFiles
{
    public static class VowelConsonantCounter
    {
        // Точка входа для задачи 6.1: путь к файлу передается аргументом
        public static void Exercise(string[] args)
        {
            Console.WriteLine("=== Задача 6.1 — Подсчёт гласных и согласных (char[])");

            if (args.Length == 0)
            {
                ChangeColor(ConsoleColor.Red, "Ошибка: не указан путь к файлу.\n");
                return;
            }

            string path = args[0];
            if (!File.Exists(path))
            {
                ChangeColor(ConsoleColor.Red, $"Файл \"{path}\" не найден.\n");
                return;
            }

            ChangeColor(ConsoleColor.Yellow, $"Рассматривается файл: {path}\n\n");

            string text = File.ReadAllText(path);
            char[] symbols = text.ToCharArray();

            (int vowels, int consonants) = CountLetters(symbols);

            Console.WriteLine($"Количество гласных: {vowels}");
            Console.WriteLine($"Количество согласных: {consonants}");

            Console.WriteLine("\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        // Подсчёт гласных/согласных
        public static (int vowels, int consonants) CountLetters(char[] chars)
        {
            int vowels = 0, consonants = 0;
            string vowelsSet = "аеёиоуыэюя";

            foreach (char c in chars)
            {
                char lower = char.ToLower(c);
                if (!char.IsLetter(lower)) continue;

                if (vowelsSet.Contains(lower)) vowels++;
                else consonants++;
            }
            return (vowels, consonants);
        }

        // Цветной вывод
        public static void ChangeColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
