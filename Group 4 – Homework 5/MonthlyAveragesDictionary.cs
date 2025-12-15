using System;
using System.Collections.Generic;

namespace Homework06.Collections
{
    public static class MonthlyAveragesDictionary
    {
        public static void HW()
        {
            Console.WriteLine("=== 6.3 HW — Средние температуры (Dictionary<string, double[]>)\n");

            Random numberDouble = new();
            Random numberInt = new();

            Dictionary<string, double[]> monthsTemp = new();
            List<string> months = new()
            {
                "Декабрь","Январь","Февраль","Март","Апрель","Май",
                "Июнь","Июль","Август","Сентябрь","Октябрь","Ноябрь"
            };

            // Формируем массивы температур по месяцам
            double[][] temps = new double[12][];
            for (int i = 0; i < temps.Length; i++)
            {
                temps[i] = new double[30];
                for (int j = 0; j < temps[i].Length; j++)
                    temps[i][j] = Math.Round(numberDouble.NextDouble() * numberInt.Next(-30, 31), 1);

                monthsTemp.Add(months[i], temps[i]);
            }

            // Средние температуры
            double[] avg = new double[12];
            for (int i = 0; i < avg.Length; i++)
                avg[i] = CalculatingAverageTemperature(temps[i]);

            // Сортировка
            for (int i = 0; i < avg.Length; i++)
                for (int j = 0; j < avg.Length; j++)
                    if (avg[i] < avg[j])
                        (avg[i], avg[j]) = (avg[j], avg[i]);

            // Соответствие «месяц → средняя температура»
            Dictionary<string, double> monthAvg = new();
            for (int i = 0; i < months.Count; i++)
                monthAvg.Add(months[i], avg[i]);

            Console.WriteLine("Средние температуры по месяцам:");
            foreach (var month in monthAvg.Keys)
                Console.WriteLine($"{month}: {monthAvg[month]}");

            Console.WriteLine("\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        public static double CalculatingAverageTemperature(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++) sum += array[i];
            return Math.Round(sum / array.Length, 1);
        }

        public static void ChangeColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
