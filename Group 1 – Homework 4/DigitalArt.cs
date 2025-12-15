using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // суть задачи: вывод цифр в виде рисунков с обработкой команд выхода
        try
        {
            string chislo = Console.ReadLine().ToLower(); // читаем ввод и приводим к нижнему регистру

            if (chislo == "exit" || chislo == "закрыть") 
            {
                Console.WriteLine("Завершение работы программы...");
                Thread.Sleep(1000); 
                Environment.Exit(0); 
            }

            else if (int.Parse(chislo) < 0 || int.Parse(chislo) > 9) // проверка диапазона
                throw new ArgumentOutOfRangeException();

            // switch для отображения цифр в псевдографике
            switch (chislo)
            {
                case "0":
                    Console.WriteLine("\n### \n# # \n# # \n###");
                    break;
                case "1":
                    Console.WriteLine("\n # \n##\n # \n # \n###");
                    break;
                case "2":
                    Console.WriteLine("\n ## \n#  # \n  # \n #\n####");
                    break;
                case "3":
                    Console.WriteLine("\n ## \n#  # \n  # \n#  # \n ##");
                    break;
                case "4":
                    Console.WriteLine("\n#  # \n#  # \n#### \n   # \n   #");
                    break;
                case "5":
                    Console.WriteLine("\n### \n# \n### \n  # \n###");
                    break;
                case "6":
                    Console.WriteLine("\n### \n# \n### \n# # \n### ");
                    break;
                case "7":
                    Console.WriteLine("\n### \n  # \n # \n#");
                    break;
                case "8":
                    Console.WriteLine("\n### \n# # \n### \n# # \n###");
                    break;
                case "9":
                    Console.WriteLine("\n### \n# # \n### \n # \n#");
                    break;
            }
        }

        catch (ArgumentOutOfRangeException) // ловим исключение неверного диапазона
        {
            Console.ForegroundColor = ConsoleColor.Red; // меняем цвет текста на красный
            Console.WriteLine("Ошибка: Число не в заданном диапозоне!");
            Thread.Sleep(3000); // задержка 3 секунды
            Console.ResetColor(); // сбрасываем цвет к стандартному
        }

        catch (FormatException) // ловим исключение неверного формата ввода
        {
            Console.WriteLine("Не число!"); // можно добавить более информативное сообщение
        }
    }
}
