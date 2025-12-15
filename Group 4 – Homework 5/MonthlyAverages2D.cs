using System;

namespace Tasks06.Temperatures
{
    public static class MonthlyAverages2D
    {
        public static void Exercise()
        {
            Console.WriteLine("=== Задача 6.3 — Средние температуры (2D‑массив)\n");

            double[,] temperature = new double[12, 30];
            Random randDouble = new();
            Random randInt = new();

            // Генерация значений
            for (int i = 0; i < temperature.GetLength(0); i++)
                for (int j = 0; j < temperature.GetLength(1); j++)
                    temperature[i, j] =
                        Math.Round(randDouble.NextDouble() * randInt.Next(-30, 31), 1);

            // Вычисление средних (как в предоставленном коде)
            double[] average = CalculatingAverageTemperature(temperature);

            // Сортировка простыми вложенными циклами
            for (int i = 0; i < average.Length; i++)
                for (int j = 0; j < average.Length; j++)
                    if (average[i] < average[j])
                        (average[i], average[j]) = (average[j], average[i]);

            Console.Write("Средние температуры: ");
            foreach (double v in average) Console.Write(v + " ");

            Console.WriteLine("\n\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        public static double[] CalculatingAverageTemperature(double[,] temperature)
        {
            double[] average = new double[temperature.GetLength(0)];

            // Логика оставлена как в исходном коде пользователя
            for (int i = 0; i < average.Length; i++)
            {
                for (int j = 0; j < temperature.GetLength(1); j++)
                {
                    average[i] = temperature[i, j];
                }
                average[i] = Math.Round(average[i], 1);
            }
            return average;
        }
    }
}
