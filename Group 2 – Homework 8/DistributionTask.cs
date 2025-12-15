using System;

class Programm
{
    public static void Main()
    {
        Hierarchy();
    }

    // Метод для вывода текста в консоль с изменением цвета
    // Используется для визуального разделения успешных действий и ошибок
    public static void ChangeColorAndWrite(string text, ConsoleColor color)
    {
        // Устанавливаем цвет текста
        Console.ForegroundColor = color;
    
        // Выводим сообщение
        Console.WriteLine(text);
    
        // Возвращаем стандартный цвет
        Console.ForegroundColor = ConsoleColor.White;
    }
    
    // Абстрактный класс сотрудника
    // Является базовым для всех типов работников
    public abstract class Employee
    {
        // Имя сотрудника
        public string Name { get; set; }
    
        // Должность сотрудника
        public string Position { get; set; }
    
        // Руководитель сотрудника
        public Employee Supervisor { get; set; }
    
        // Список подчинённых
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
    
        // Проверка возможности назначения задачи
        // Можно назначать задачу только своему прямому подчинённому
        public bool CanAssignTask(Task task)
        {
            // Проверяем, есть ли подчинённый с нужным именем
            return Subordinates.Any(s => s.Name == task.AssigneeName);
        }
    
        // Метод назначения задачи
        public void AssignTask(Task task)
        {
            // Если подчинённый найден
            if (CanAssignTask(task))
            {
                // Получаем подчинённого по имени
                var subordinate = Subordinates.First(s => s.Name == task.AssigneeName);
    
                // Сообщение об успешном назначении задачи
                ChangeColorAndWrite(
                    $"{Name} назначает задачу {task.Description} для {subordinate.Name}",
                    ConsoleColor.Green
                );
    
                // Передаём задачу подчинённому
                subordinate.ReceiveTask(task);
            }
            else
            {
                // Сообщение об ошибке
                ChangeColorAndWrite("ОШИБКА", ConsoleColor.Red);
                ChangeColorAndWrite(
                    $"{Name} не может назначить эту задачу для {task.AssigneeName}",
                    ConsoleColor.Red
                );
            }
        }
    
        // Метод получения задачи сотрудником
        public void ReceiveTask(Task task)
        {
            // Уведомление о получении задачи
            ChangeColorAndWrite($"{task.AssigneeName} получает задачу!", ConsoleColor.Green);
        }
    }
    
    // Класс задачи
    public class Task
    {
        // Описание задачи
        public string Description { get; set; }
    
        // Имя сотрудника, которому назначена задача
        public string AssigneeName { get; set; }
    
        // Тип задачи (для логики или расширения в будущем)
        public string Type { get; set; }
    
        // Конструктор задачи
        public Task(string description, string assigneeName, string type)
        {
            Description = description;
            AssigneeName = assigneeName;
            Type = type;
        }
    }
    
    // Директор компании
    public class Director : Employee
    {
    }
    
    // Руководитель отдела
    public class DepartamentHead : Employee
    {
    }
    
    // Тимлид
    public class TeamLead : Employee
    {
    }
    
    // Обычный сотрудник
    public class DefaultEmployee : Employee
    {
        // Обычный сотрудник не может назначать задачи
        public void AssignTask(Task task)
        {
            ChangeColorAndWrite(
                $"{Name} не может назначать задачи, т.к. он регулярный сотрудник!",
                ConsoleColor.Red
            );
        }
    }
    
    // Метод построения иерархии компании
    public static void Hierarchy()
    {
        // Создание директоров
        var Timur = new Director { Name = "Тимур", Position = "Ген директор" };
    
        var Rashid = new Director
        {
            Name = "Рашид",
            Position = "Фин директор",
            Supervisor = Timur
        };
        Timur.Subordinates.Add(Rashid);
    
        var Ilham = new Director
        {
            Name = "Ильгам",
            Position = "Директор по автомотизации",
            Supervisor = Timur
        };
        Timur.Subordinates.Add(Ilham);
    
        // -------- БУХГАЛТЕРИЯ --------
        var Lukas = new DepartamentHead
        {
            Name = "Лукас",
            Position = "Глав бухгалтер",
            Supervisor = Rashid
        };
        Rashid.Subordinates.Add(Lukas);
    
        var Accountant1 = new DefaultEmployee
        {
            Name = "1 Бухгалтер",
            Position = "Бухгалтер",
            Supervisor = Lukas
        };
        Lukas.Subordinates.Add(Accountant1);
    
        var Accountant2 = new DefaultEmployee
        {
            Name = "2 Бухгалтер",
            Position = "Бухгалтер",
            Supervisor = Lukas
        };
        Lukas.Subordinates.Add(Accountant2);
    
        // -------- IT ОТДЕЛ --------
        var Arkadiy = new DepartamentHead
        {
            Name = "Аркадий",
            Position = "Начальник IT систем",
            Supervisor = Ilham
        };
        Ilham.Subordinates.Add(Arkadiy);
    
        var Vlad = new DepartamentHead
        {
            Name = "Влад",
            Position = "Зам начальника IT систем",
            Supervisor = Ilham
        };
        Ilham.Subordinates.Add(Vlad);
    
        // -------- СИСТЕМЩИКИ --------
        var Ilshat = new TeamLead
        {
            Name = "Ильшат",
            Position = "Главный системщик",
            Supervisor = Arkadiy
        };
        Arkadiy.Subordinates.Add(Ilshat);
    
        var Ivanich = new TeamLead
        {
            Name = "Иваныч",
            Position = "Зам главы системщиков",
            Supervisor = Ilshat
        };
        Ilshat.Subordinates.Add(Ivanich);
    
        var Ilya = new DefaultEmployee
        {
            Name = "Илья",
            Position = "Системщик",
            Supervisor = Ilshat
        };
    
        var Vitya = new DefaultEmployee
        {
            Name = "Витя",
            Position = "Системщик",
            Supervisor = Ivanich
        };
        Ivanich.Subordinates.Add(Vitya);
    
        var Zhenya = new DefaultEmployee
        {
            Name = "Женя",
            Position = "Системщик",
            Supervisor = Ivanich
        };
        Ivanich.Subordinates.Add(Zhenya);
    
        // -------- РАЗРАБОТЧИКИ --------
        var Sergey = new TeamLead
        {
            Name = "Сергей",
            Position = "Главный разработчик",
            Supervisor = Arkadiy
        };
        Arkadiy.Subordinates.Add(Sergey);
    
        var Lyaisan = new TeamLead
        {
            Name = "Ляйсан",
            Position = "Зам.разработчика",
            Supervisor = Sergey
        };
        Sergey.Subordinates.Add(Lyaisan);
    
        var Marat = new DefaultEmployee
        {
            Name = "Марат",
            Position = "Разработчик",
            Supervisor = Lyaisan
        };
        Lyaisan.Subordinates.Add(Marat);
    
        var Dina = new DefaultEmployee
        {
            Name = "Дина",
            Position = "Разработчик",
            Supervisor = Lyaisan
        };
        Lyaisan.Subordinates.Add(Dina);
    
        var Ildar = new DefaultEmployee
        {
            Name = "Ильдар",
            Position = "Разработчик",
            Supervisor = Lyaisan
        };
        Lyaisan.Subordinates.Add(Ildar);
    
        var Anton = new DefaultEmployee
        {
            Name = "Антон",
            Position = "Разработчик",
            Supervisor = Lyaisan
        };
        Lyaisan.Subordinates.Add(Anton);
    
        // -------- НАЗНАЧЕНИЕ ЗАДАЧ --------
    
        // Попытка назначить задачу сотруднику не из своего отдела
        ChangeColorAndWrite("Рашид пытается дать задачу не своему сектору", ConsoleColor.Red);
        Rashid.AssignTask(new Task("Разработать мир танков", "Дина", "development"));
    
        Console.ReadKey();
        Console.Clear();
    
        // Корректное назначение задачи своему подчинённому
        ChangeColorAndWrite("Рашид назначает задачу своему сектору", ConsoleColor.Green);
        Rashid.AssignTask(new Task("Сдать главное здание в чер мет", "Лукас", "accounting"));
    
        Console.ReadKey();
        Console.Clear();
    
        // Назначение задачи директором компании
        ChangeColorAndWrite("Тимур назначает задачу Ильгаму", ConsoleColor.Green);
        Timur.AssignTask(new Task("Проиграть компанию в карты", "Ильгам", "IT Отдел"));
    }
}
