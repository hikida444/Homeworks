using System;

class Programm
{
    public static void Main()
    {
        FileChange.WorkWithFile();
    }
}

class FileChange
{
  // Метод для работы с файлом
    public static void WorkWithFile()
    {
        // Запрашиваем имя файла у пользователя
        Console.Write("Введите имя файла с которым мне стоит поработать: ");
        string FileName = Console.ReadLine()!;

        // Проверяем, существует ли файл
        if (File.Exists(FileName))
        {
            // Считываем весь текст из файла
            string ReadInfo = File.ReadAllText(FileName);

            // Приводим текст к верхнему регистру (результат не сохраняется)
            ReadInfo.ToUpper();

            // Добавляем текст в файл OutFile.txt
            File.AppendAllText("OutFile.txt", ReadInfo);
        }
        else
        {
            // Сообщаем пользователю, если файл не найден
            Console.WriteLine("Такого файла не существует!");
        }
    }
}
