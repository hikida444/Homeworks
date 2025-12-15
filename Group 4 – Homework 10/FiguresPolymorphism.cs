using System;

// Интерфейс: общий контракт для всех фигур.
interface IFigure
{
    void MoveHorizontal(int distance);
    void MoveVertical(int distance);
    void ChangeColor(string newColor);
    bool IsVisible();
    void Print();
}

// Абстрактный базовый класс, реализующий общую логику фигур.
abstract class Figure : IFigure
{
    protected string color;
    protected bool isVisible;
    protected int x, y;

    protected Figure(string color, bool isVisible, int x, int y)
    {
        this.color = color;
        this.isVisible = isVisible;
        this.x = x;
        this.y = y;
    }

    public virtual void MoveHorizontal(int distance)
    {
        x += distance;
        Console.WriteLine($"Переместили по горизонтали на {distance}. Новая позиция: ({x}, {y})");
    }

    public virtual void MoveVertical(int distance)
    {
        y += distance;
        Console.WriteLine($"Переместили по вертикали на {distance}. Новая позиция: ({x}, {y})");
    }

    public virtual void ChangeColor(string newColor)
    {
        color = newColor;
        Console.WriteLine($"Изменили цвет на {newColor}");
    }

    public bool IsVisible() => isVisible;

    public virtual void Print()
    {
        Console.WriteLine($"Цвет: {color}");
        Console.WriteLine($"Видимость: {(isVisible ? "Видимый" : "Невидимый")}");
        Console.WriteLine($"Позиция: ({x}, {y})");
    }
}

// Точка.
class Point : Figure
{
    public Point(string color, bool isVisible, int x, int y)
        : base(color, isVisible, x, y) { }

    public override void Print()
    {
        Console.WriteLine("=== ТОЧКА ===");
        base.Print();
        Console.WriteLine();
    }
}

// Окружность.
class Circle : Point
{
    private double radius;

    public Circle(string color, bool isVisible, int x, int y, double radius)
        : base(color, isVisible, x, y)
    {
        this.radius = radius;
    }

    public double CalculateArea() => Math.PI * radius * radius;

    public override void Print()
    {
        Console.WriteLine("=== ОКРУЖНОСТЬ ===");
        base.Print();
        Console.WriteLine($"Радиус: {radius}");
        Console.WriteLine($"Площадь: {CalculateArea():F2}");
        Console.WriteLine();
    }
}

// Прямоугольник.
class Rectangle : Point
{
    private double width;
    private double height;

    public Rectangle(string color, bool isVisible, int x, int y, double width, double height)
        : base(color, isVisible, x, y)
    {
        this.width = width;
        this.height = height;
    }

    public double CalculateArea() => width * height;

    public override void Print()
    {
        Console.WriteLine("=== ПРЯМОУГОЛЬНИК ===");
        base.Print();
        Console.WriteLine($"Ширина: {width}");
        Console.WriteLine($"Высота: {height}");
        Console.WriteLine($"Площадь: {CalculateArea():F2}");
        Console.WriteLine();
    }
}

// Точка входа.
class Program
{
    static void Main()
    {
        Console.WriteLine("=== ГЕОМЕТРИЧЕСКИЕ ФИГУРЫ ===\n");

        Point point = new Point("Красный", true, 10, 20);
        Circle circle = new Circle("Синий", true, 30, 40, 5);
        Rectangle rectangle = new Rectangle("Зелёный", false, 50, 60, 8, 6);

        IFigure[] figures = { point, circle, rectangle };

        for (int i = 0; i < figures.Length; i++)
        {
            Console.WriteLine($"ФИГУРА {i + 1}:");
            figures[i].Print();

            figures[i].MoveHorizontal(5);
            figures[i].MoveVertical(-3);

            if (i == 0) figures[i].ChangeColor("Жёлтый");
            if (i == 1) figures[i].ChangeColor("Фиолетовый");
            if (i == 2) figures[i].ChangeColor("Оранжевый");

            Console.WriteLine($"Фигура видима? {figures[i].IsVisible()}");
            Console.WriteLine();
        }

        Console.WriteLine("=== ДОПОЛНИТЕЛЬНЫЕ ДЕЙСТВИЯ ===\n");

        rectangle.ChangeColor("Золотой");
        rectangle.Print();

        Console.WriteLine($"Площадь круга: {circle.CalculateArea():F2}");
        Console.WriteLine($"Площадь прямоугольника: {rectangle.CalculateArea():F2}");
    }
}
