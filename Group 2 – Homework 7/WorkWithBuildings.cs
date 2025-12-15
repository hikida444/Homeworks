using System;

class Programm
{
    public static void Main()
    {
        Building.Start();
    }
}

class Building
{
    // Статическая переменная для отслеживания последнего присвоенного номера здания
    // Она общая для всех объектов класса, поэтому каждый новый объект будет получать уникальный номер
    private static int lastNumber = 0;

    // Поля экземпляра класса для хранения характеристик конкретного здания
    private int number;        // Номер здания
    private double height;     // Высота здания
    private int floors;        // Количество этажей
    private int apartments;    // Количество квартир
    private int entrances;     // Количество подъездов

    // Конструктор класса для инициализации нового здания
    public Building(double h, int f, int a, int e)
    {
        lastNumber++;       // Увеличиваем общий счетчик зданий
        number = lastNumber; // Присваиваем уникальный номер этому зданию
        height = h;         // Задаем высоту
        floors = f;         // Задаем количество этажей
        apartments = a;     // Задаем количество квартир
        entrances = e;      // Задаем количество подъездов
    }

    // Возвращают значения полей, обеспечивая доступ к ним снаружи класса
    public int GetNumber() => number;
    public double GetHeight() => height;
    public int GetFloors() => floors;
    public int GetApartments() => apartments;
    public int GetEntrances() => entrances;

    // Вычисляем среднюю высоту одного этажа
    public double GetFloorHeight()
    {
        return height / floors;
    }

    // Вычисляем количество квартир в одном подъезде
    public int GetApartmentsInEntrance()
    {
        return apartments / entrances;
    }

    // Вычисляем количество квартир на одном этаже одного подъезда
    public int GetApartmentsOnFloor()
    {
        return apartments / (floors * entrances);
    }

    // Вывод информации о здании в консоль
    public void PrintInfo()
    {
        Console.WriteLine($"Здание {number}");
        Console.WriteLine($"Высота: {height}м, Этажей: {floors}");
        Console.WriteLine($"Квартир: {apartments}, Подъездов: {entrances}");
        Console.WriteLine($"Высота этажа: {GetFloorHeight()}м");
        Console.WriteLine($"Квартир в подъезде: {GetApartmentsInEntrance()}");
        Console.WriteLine($"Квартир на этаже: {GetApartmentsOnFloor()}");
        Console.WriteLine("------------------------");
    }

    // Главный метод программы, запускающий консольное меню
    public static void Start()
    {
        // Создаем список для хранения всех зданий
        List<Building> buildings = [];

        while (true)
        {
            // Меню пользователя
            Console.WriteLine("1 - Добавить здание");
            Console.WriteLine("2 - Показать все здания");
            Console.WriteLine("3 - Выйти");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                AddBuilding(buildings); // Добавление нового здания
            }
            else if (choice == "2")
            {
                ShowBuildings(buildings); // Показ всех зданий
            }
            else if (choice == "3")
            {
                break; // Выход из программы
            }
            else
            {
                Console.WriteLine("Неверный выбор!"); // Обработка некорректного ввода
            }
        }
    }

    // Метод для добавления нового здания
    static void AddBuilding(List<Building> buildings)
    {
        // Считываем характеристики здания с консоли
        Console.Write("Введите высоту здания: ");
        double h = double.Parse(Console.ReadLine()!);

        Console.Write("Введите этажность: ");
        int f = int.Parse(Console.ReadLine()!);

        Console.Write("Введите количество квартир: ");
        int a = int.Parse(Console.ReadLine()!);

        Console.Write("Введите количество подъездов: ");
        int e = int.Parse(Console.ReadLine()!);

        // Создаем объект здания и добавляем его в список
        Building newBuilding = new Building(h, f, a, e);
        buildings.Add(newBuilding);

        Console.WriteLine("Здание добавлено!");
        Console.WriteLine("------------------------");
    }

    // Метод для вывода информации обо всех зданиях
    static void ShowBuildings(List<Building> buildings)
    {
        if (buildings.Count == 0)
        {
            Console.WriteLine("Нет зданий!");
            return;
        }

        // Перебор всех зданий и вызов метода PrintInfo для каждого
        foreach (Building b in buildings)
        {
            b.PrintInfo();
        }
    }
}
