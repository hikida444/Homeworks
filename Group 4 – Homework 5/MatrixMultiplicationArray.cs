using System;

namespace Tasks06.Matrix
{
    public static class MatrixMultiplicationArray
    {
        public static void Exercise()
        {
            Console.ForegroundColor = ConsoleColor.White;

            int rowsFirst, columnsFirst, rowsSecond, columnsSecond;
            bool loop = true;

            while (loop)
            {
                Console.WriteLine("=== Задача 6.2 — Умножение матриц (двумерные массивы)\n");

                // Первая матрица
                ChangeColor(ConsoleColor.Blue, "Первая матрица.\n");
                Console.Write("Строк: ");
                rowsFirst = Check(Console.ReadLine()!, out bool ok);
                if (!ok) continue;

                Console.Write("Столбцов: ");
                columnsFirst = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                int[,] a = new int[rowsFirst, columnsFirst];

                ChangeColor(ConsoleColor.Blue, "\nЭлементы первой матрицы.\n");
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    Console.Write($"Введите {a.GetLength(1)} чисел {i + 1}-й строки: ");
                    if (!ReadRow(a, i))
                    {
                        i--;
                        continue;
                    }
                }

                // Вторая матрица
                ChangeColor(ConsoleColor.Blue, "\nВторая матрица.\n");
                Console.Write("Строк: ");
                rowsSecond = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                Console.Write("Столбцов: ");
                columnsSecond = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                int[,] b = new int[rowsSecond, columnsSecond];

                ChangeColor(ConsoleColor.Blue, "\nЭлементы второй матрицы.\n");
                for (int i = 0; i < b.GetLength(0); i++)
                {
                    Console.Write($"Введите {b.GetLength(1)} чисел {i + 1}-й строки: ");
                    if (!ReadRow(b, i))
                    {
                        i--;
                        continue;
                    }
                }

                // Совместимость
                if (columnsFirst != rowsSecond)
                {
                    ChangeColor(ConsoleColor.Red,
                        "\nНевозможно перемножить матрицы: столбцы(A) != строки(B).\n");
                    break;
                }

                int[,] res = Multiply(a, b);

                ChangeColor(ConsoleColor.Green, "\nРезультат:\n");
                Print(res);

                loop = false;
            }

            Console.WriteLine("\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        private static bool ReadRow(int[,] m, int row)
        {
            string[] tokens = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != m.GetLength(1))
            {
                ChangeColor(ConsoleColor.Red, "Неверное количество элементов.\n");
                return false;
            }
            for (int j = 0; j < m.GetLength(1); j++)
            {
                if (!int.TryParse(tokens[j], out m[row, j]))
                {
                    ChangeColor(ConsoleColor.Red, "Элементы должны быть целыми.\n");
                    return false;
                }
            }
            return true;
        }

        public static int[,] Multiply(int[,] a, int[,] b)
        {
            int r = a.GetLength(0), c = b.GetLength(1), t = a.GetLength(1);
            int[,] rM = new int[r, c];

            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < t; k++) sum += a[i, k] * b[k, j];
                    rM[i, j] = sum;
                }
            return rM;
        }

        public static void Print(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write($"{m[i, j],5}");
                Console.WriteLine();
            }
        }

        public static int Check(string s, out bool ok)
        {
            if (!int.TryParse(s, out int v) || v <= 0)
            {
                ChangeColor(ConsoleColor.Red, "Некорректное число.\n");
                ok = false;
                return 0;
            }
            ok = true;
            return v;
        }

        public static void ChangeColor(ConsoleColor color, string msg)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
