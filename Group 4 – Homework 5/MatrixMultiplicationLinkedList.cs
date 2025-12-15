using System;
using System.Collections.Generic;

namespace Homework06.Collections
{
    public static class MatrixMultiplicationLinkedList
    {
        public static void HomeWork()
        {
            Console.ForegroundColor = ConsoleColor.White;

            int rowsFirst, columnsFirst, rowsSecond, columnsSecond;
            bool loop = true;

            while (loop)
            {
                Console.WriteLine("=== 6.2 HW — Умножение матриц (LinkedList<LinkedList<int>>)\n");

                // Первая матрица
                ChangeColor(ConsoleColor.Blue, "Первая матрица.\n");
                Console.Write("Строк: ");
                rowsFirst = Check(Console.ReadLine()!, out bool ok);
                if (!ok) continue;

                Console.Write("Столбцов: ");
                columnsFirst = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                var m1 = new LinkedList<LinkedList<int>>();
                ChangeColor(ConsoleColor.Blue, "\nВвод элементов первой матрицы.\n");
                for (int i = 0; i < rowsFirst; i++)
                {
                    var row = new LinkedList<int>();
                    for (int j = 0; j < columnsFirst; j++)
                    {
                        Console.Write($"Введите {j + 1}-й элемент {i + 1}-й строки: ");
                        int val = Check(Console.ReadLine()!, out ok);
                        if (!ok) { j--; continue; }
                        row.AddLast(val);
                    }
                    m1.AddLast(row);
                }

                // Вторая матрица
                ChangeColor(ConsoleColor.Blue, "\nВторая матрица.\n");
                Console.Write("Строк: ");
                rowsSecond = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                Console.Write("Столбцов: ");
                columnsSecond = Check(Console.ReadLine()!, out ok);
                if (!ok) continue;

                var m2 = new LinkedList<LinkedList<int>>();
                ChangeColor(ConsoleColor.Blue, "\nВвод элементов второй матрицы.\n");
                for (int i = 0; i < rowsSecond; i++)
                {
                    var row = new LinkedList<int>();
                    for (int j = 0; j < columnsSecond; j++)
                    {
                        Console.Write($"Введите {j + 1}-й элемент {i + 1}-й строки: ");
                        int val = Check(Console.ReadLine()!, out ok);
                        if (!ok) { j--; continue; }
                        row.AddLast(val);
                    }
                    m2.AddLast(row);
                }

                if (columnsFirst != rowsSecond)
                {
                    ChangeColor(ConsoleColor.Red, "\nНевозможно перемножить матрицы.\n");
                    break;
                }

                var result = MultiplyMatrices(m1, m2, rowsFirst, columnsFirst, columnsSecond);

                ChangeColor(ConsoleColor.Green, "\nРезультирующая матрица:\n");
                PrintMatrix(result);

                loop = false;
            }

            Console.WriteLine("\n=== Программа завершена.");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        public static LinkedList<LinkedList<int>> MultiplyMatrices(
            LinkedList<LinkedList<int>> first,
            LinkedList<LinkedList<int>> second,
            int rowsFirst,
            int columnsFirst,
            int columnsSecond)
        {
            var result = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < rowsFirst; i++)
            {
                var rowRes = new LinkedList<int>();
                for (int j = 0; j < columnsSecond; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < columnsFirst; k++)
                        sum += Elem(first, i, k) * Elem(second, k, j);

                    rowRes.AddLast(sum);
                }
                result.AddLast(rowRes);
            }
            return result;
        }

        public static int Elem(LinkedList<LinkedList<int>> m, int r, int c)
        {
            var row = m.First!;
            for (int i = 0; i < r; i++) row = row.Next!;

            var col = row.Value.First!;
            for (int j = 0; j < c; j++) col = col.Next!;

            return col.Value;
        }

        public static void PrintMatrix(LinkedList<LinkedList<int>> m)
        {
            foreach (var row in m)
            {
                foreach (var x in row) Console.Write(x + " ");
                Console.WriteLine();
            }
        }

        public static int Check(string s, out bool ok)
        {
            if (!int.TryParse(s, out int v))
            {
                ChangeColor(ConsoleColor.Red, "Введено некорректное значение.\n");
                ok = false;
                return 0;
            }
            ok = true;
            return v;
        }

        public static void ChangeColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
