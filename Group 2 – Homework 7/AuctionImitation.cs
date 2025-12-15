class Programm
{
    public static void Main()
    {
        Program.start_task();
    }
}

// Класс Auction представляет общий аукционный товар
public class Auction(string name, decimal price, string description)
{
    // Имя товара
    public string Name { get; set; } = name;

    // Стартовая цена товара (первоначальная цена на аукционе)
    public decimal StartPrice { get; set; } = price;

    // Текущая цена товара (будет обновляться при ставках)
    public decimal CurrentPrice { get; set; } = price;

    // Флаг, указывающий, продан товар или нет
    public bool IsSold { get; set; } = false;

    // Описание товара
    public string Description { get; set; } = description;

    // Метод для выставления ставки на товар
    public void MakeBid(decimal bidAmount)
    {
        // Если товар уже продан, ставки не принимаются
        if (IsSold)
        {
            Console.WriteLine($"Товар '{Name}' уже продан!");
            return;
        }

        // Сравниваем новую ставку с текущей ценой
        if (bidAmount > CurrentPrice)
        {
            // Ставка принята, обновляем текущую цену
            CurrentPrice = bidAmount;
            Console.WriteLine($"Ставка на товар '{Name}' принята: {bidAmount} руб.");
        }
        else
        {
            // Ставка слишком мала, не принимаем
            Console.WriteLine($"Ставка {bidAmount} руб. на товар '{Name}' слишком мала! Текущая цена: {CurrentPrice} руб.");
        }
    }

    // Метод для продажи товара (меняет статус на продан)
    public void Sell()
    {
        IsSold = true;
        Console.WriteLine($"Товар '{Name}' продан за {CurrentPrice} руб.!");
    }

    // Метод для вывода информации о товаре
    public void ShowInfo()
    {
        Console.WriteLine($"Товар: {Name}");
        Console.WriteLine($"Описание: {Description}");
        Console.WriteLine($"Стартовая цена: {StartPrice} руб.");
        Console.WriteLine($"Текущая цена: {CurrentPrice} руб.");
        Console.WriteLine($"Статус: {(IsSold ? "Продан" : "На продаже")}");
    }

    // Метод возвращает тип товара
    public string GetItemType()
    {
        return "Аукционный товар";
    }
}

// Класс Painting наследуется от Auction и представляет картину
public class Painting : Auction
{
    public string Artist { get; set; }   // Имя художника
    public string Style { get; set; }    // Стиль картины

    // Конструктор класса Painting
    public Painting(string name, decimal price, string artist, string style = "Неизвестно")
        : base(name, price, $"Картина художника {artist}") // Вызываем базовый конструктор Auction
    {
        Artist = artist;
        Style = style;
    }

    // Переопределение метода для типа предмета (статический)
    public static new string GetItemType()
    {
        return "Картина";
    }

    // Метод для отображения художника
    public void ShowArtist()
    {
        Console.WriteLine($"Художник: {Artist}");
    }

    // Метод для проверки, является ли картина дорогой
    public bool IsExpensive()
    {
        return CurrentPrice > 100000;
    }
}

// Класс Antique наследуется от Auction и представляет антиквариат
public class Antique : Auction
{
    public int Age { get; set; }        // Возраст антикварного предмета
    public string Material { get; set; } // Материал изготовления

    // Конструктор класса Antique
    public Antique(string name, decimal price, int age, string material = "Дерево")
        : base(name, price, $"Антикварный предмет из {material}") // Вызываем базовый конструктор
    {
        Age = age;
        Material = material;
    }

    // Переопределение метода для типа предмета (статический)
    public static new string GetItemType()
    {
        return "Антиквариат";
    }

    // Метод для отображения возраста предмета
    public void ShowAge()
    {
        Console.WriteLine($"Возраст: {Age} лет");
    }

    // Метод для проверки, является ли предмет очень старым
    public bool IsVeryOld()
    {
        return Age > 100;
    }
}

// Основной класс программы
public class Program
{
    public static void start_task()
    {
        // Создаем несколько объектов картин
        Painting painting1 = new("Мона Лиза", 500000, "Леонардо да Винчи", "Ренессанс");
        Painting painting2 = new("Звездная ночь", 300000, "Ван Гог");

        // Создаем несколько объектов антиквариата
        Antique antique1 = new("Старинная ваза", 150000, 200, "Фарфор");
        Antique antique2 = new("Древний сундук", 80000, 150);

        // Список всех товаров (используем базовый тип Auction для хранения разных объектов)
        List<Auction> allItems = [painting1, painting2, antique1, antique2];

        // Показываем все товары на аукционе
        Console.WriteLine("Все товары на аукционе:");
        Console.WriteLine("-----------------------");

        foreach (var item in allItems)
        {
            item.ShowInfo();
            Console.WriteLine(value: $"Тип: {item.GetItemType()}"); // Показываем тип товара
            Console.WriteLine();
        }

        Console.WriteLine("Начинаем торги!");
        Console.WriteLine("===============");

        // Пробуем сделать ставки на первую картину
        painting1.MakeBid(450000); // Ставка слишком мала
        painting1.MakeBid(550000); // Ставка принята
        painting1.Sell();           // Продажа товара

        Console.WriteLine();

        // Ставки на антиквариат
        antique1.MakeBid(160000); // Принята
        antique1.MakeBid(180000); // Принята

        Console.WriteLine();

        Console.WriteLine("Информация о товарах:");
        Console.WriteLine("---------------------");

        // Дополнительная информация по картинам
        painting2.ShowArtist();
        Console.WriteLine($"Дорогая картина: {painting2.IsExpensive()}");

        // Дополнительная информация по антиквариату
        antique2.ShowAge();
        Console.WriteLine($"Очень старый: {antique2.IsVeryOld()}");

        Console.WriteLine();

        Console.WriteLine("Итоги аукциона:");
        Console.WriteLine("================");

        // Показываем финальное состояние всех товаров
        foreach (var item in allItems)
        {
            item.ShowInfo();
            Console.WriteLine();
        }
    }
}
