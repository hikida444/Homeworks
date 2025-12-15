using System;

class Programm
{
    public static void Main()
    {
        /// Email.WorkWithEmail(string[] args);
    }
}

class Email
{
    // Метод для извлечения email-адресов из файла
    public static void WorkWithEmail(string[] args)
    {
        try
        {
            // Считываем все строки из файла input.txt
            string[] lines = File.ReadAllLines("input.txt");

            // Создаём массив для хранения email-адресов
            string[] emails = new string[lines.Length];

            // Обрабатываем каждую строку
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Извлекаем email из строки
                SearchMail(ref line);

                // Сохраняем результат
                emails[i] = line;
            }

            // Записываем email-адреса в файл emails.txt
            File.WriteAllLines("emails.txt", emails);

            // Сообщаем об успешном завершении
            Console.WriteLine("Файл с email адресами успешно создан!");
        }
        catch (Exception ex)
        {
            // Обрабатываем возможные ошибки
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    // Метод ищет email в строке и изменяет её по ссылке
    public static void SearchMail(ref string s)
    {
        // Разделяем строку по символу '#'
        string[] parts = s.Split('#', StringSplitOptions.RemoveEmptyEntries);

        // Если после разделения есть нужная часть
        if (parts.Length >= 2)
        {
            // Берём вторую часть и удаляем лишние пробелы
            s = parts[1].Trim();
        }
        else
        {
            // Если email не найден — делаем строку пустой
            s = string.Empty;
        }
    }

}
