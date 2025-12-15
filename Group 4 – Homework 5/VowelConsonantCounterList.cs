using System;
using System.Collections.Generic;
using System.IO;

namespace Homework06.Collections
{
    public static class VowelConsonantCounterList
    {
        public static void HomeWork(string[] args)
        {
            Console.WriteLine("=== 6.1 HW — Подсчёт гласных/согласных (List<char>)\n");

            if (args.Length == 0)
            {
                Console.WriteLine("Ошибка: не указан путь к файлу.");
                return;
            }

            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"Файл \"{path}\" не найден.");
                return;
            }

            Console.WriteLine($"Рассмотрение файла {path}:");
            string text = File.ReadAllText(path);
            List<char> symbols = new(text);

            (int vowels, int consonants) = CountLetters(symbols);

            Console.WriteLine($"Количество гласных: {vowels}");
            Console.WriteLine($"Количество согласных: {consonants}");

            Console.WriteLine("\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        public static (int vowels, int consonants) CountLetters(List<char> chars)
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
    }
}
