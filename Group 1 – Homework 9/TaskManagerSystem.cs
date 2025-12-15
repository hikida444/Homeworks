using System;
using System.Collections.Generic;
using System.Linq;

namespace hw9last
{
    // статусы проекта: от создания до закрытия
    public enum ProjectStatus
    {
        Project,
        Execution,
        Closed
    }

    // статусы задачи: простой жизненный цикл от назначения до завершения
    public enum TaskStatus
    {
        Assigned,
        InWork,
        UnderReview,
        Completed
    }

    public class Person
    {
        // простая модель участника команды/заказчика
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        // удобное строковое представление для вывода
        public override string ToString() => $"{Name} ({Position})";
    }

    public class Report
    {
        // отчет по задаче (создается исполнителем)
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Person Executor { get; set; }

        // краткий вывод отчета
        public override string ToString() =>
            $"Отчет #{Id} от {CreatedDate:dd.MM.yyyy}";
    }

    public class Task
    {
        // сущность задачи внутри проекта
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }

        // инициатор — кто ставит задачу (в примере тимлид)
        public Person Initiator { get; set; }
        // исполнитель — кто выполняет задачу
        public Person Executor { get; set; }

        // статус по умолчанию — назначена
        public TaskStatus Status { get; set; } = TaskStatus.Assigned;

        // список отчетов по задаче (обычно 1+, если возвращали на доработку)
        public List<Report> Reports { get; set; } = new List<Report>();

        public void TakeToWork(Person executor)
        {
            // защищаем жизненный цикл: в работу можно взять только из "назначена"
            if (Status != TaskStatus.Assigned)
                throw new InvalidOperationException("Задачу можно взять в работу только из статуса 'Назначена'");

            Executor = executor;
            Status = TaskStatus.InWork;
            Console.WriteLine($"{executor.Name} взял задачу '{Title}' в работу");
        }

        public void DelegateTask(Person newExecutor)
        {
            // делегировать можно пока задача не завершена
            if (Status != TaskStatus.Assigned && Status != TaskStatus.InWork)
                throw new InvalidOperationException("Задачу можно делегировать только из статусов 'Назначена' или 'В работе'");

            Console.WriteLine($"{Executor?.Name} делегировал задачу '{Title}' {newExecutor.Name}");
            Executor = newExecutor;
            // после делегирования задача снова "назначена" новому исполнителю
            Status = TaskStatus.Assigned;
        }

        public void RejectTask()
        {
            // отказ означает: исполнителя снимаем, задача возвращается в "назначена"
            Console.WriteLine($"{Executor?.Name} отклонил задачу '{Title}'");
            Executor = null;
            Status = TaskStatus.Assigned;
        }

        public Report CreateReport(string text)
        {
            // отчет может создать только назначенный исполнитель
            if (Executor == null)
                throw new InvalidOperationException("У задачи нет исполнителя");

            var report = new Report
            {
                // простой авто-id внутри задачи
                Id = Reports.Count + 1,
                Text = text,
                CreatedDate = DateTime.Now,
                Executor = Executor
            };

            Reports.Add(report);
            // после отчета задача переходит на проверку
            Status = TaskStatus.UnderReview;

            Console.WriteLine($"Создан отчет по задаче '{Title}'");
            return report;
        }

        public void ApproveReport(Person approver)
        {
            // утверждать отчет может только инициатор
            if (approver.Id != Initiator.Id)
                throw new InvalidOperationException("Только инициатор задачи может утверждать отчет");

            // нельзя утвердить, если отчетов нет
            if (!Reports.Any())
                throw new InvalidOperationException("Нет отчетов для утверждения");

            Status = TaskStatus.Completed;
            Console.WriteLine($"Задача '{Title}' завершена (отчет утвержден)");
        }

        public void RejectReport(Person reviewer)
        {
            // вернуть на доработку тоже может только инициатор
            if (reviewer.Id != Initiator.Id)
                throw new InvalidOperationException("Только инициатор задачи может возвращать отчет на доработку");

            // возвращаем из проверки обратно в работу
            Status = TaskStatus.InWork;
            Console.WriteLine($"Отчет по задаче '{Title}' возвращен на доработку");
        }

        // флаг для удобных проверок (например, можно ли закрыть проект)
        public bool IsCompleted => Status == TaskStatus.Completed;
    }

    public class Project
    {
        // сущность проекта, внутри которой создаются задачи
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }

        // заказчик проекта (в примере отдельный Person)
        public Person Customer { get; set; }
        // тимлид проекта (в примере первый человек команды)
        public Person TeamLead { get; set; }

        // список задач проекта
        public List<Task> Tasks { get; set; } = new List<Task>();

        // статус по умолчанию — "проект" (до старта исполнения)
        public ProjectStatus Status { get; set; } = ProjectStatus.Project;

        public Task CreateTask(string title, string description, DateTime deadline, Person executor)
        {
            // задачи создаем только пока проект в стадии "проект"
            if (Status != ProjectStatus.Project)
                throw new InvalidOperationException("Задачи можно создавать только в статусе 'Проект'");

            var task = new Task
            {
                Id = Tasks.Count + 1,
                Title = title,
                Description = description,
                CreatedDate = DateTime.Now,
                Deadline = deadline,
                Initiator = TeamLead,
                Executor = executor,
                Status = TaskStatus.Assigned
            };

            Tasks.Add(task);
            Console.WriteLine($"Создана задача: '{title}' для {executor.Name}");
            return task;
        }

        public void StartExecution()
        {
            // старт возможен только из "проект"
            if (Status != ProjectStatus.Project)
                throw new InvalidOperationException("Проект уже запущен или закрыт");

            // бессмысленно начинать проект без задач
            if (Tasks.Count == 0)
                throw new InvalidOperationException("Нельзя начать проект без задач");

            Status = ProjectStatus.Execution;
            Console.WriteLine($"\nПроект '{Title}' переведен в статус 'Исполнение'");
        }

        public bool CanBeClosed()
        {
            // закрывать можно только если все задачи завершены
            return Tasks.All(t => t.IsCompleted);
        }

        public void CloseProject()
        {
            // финальная проверка перед закрытием
            if (!CanBeClosed())
                throw new InvalidOperationException("Не все задачи выполнены");

            Status = ProjectStatus.Closed;
            Console.WriteLine($"\nПроект '{Title}' закрыт");
        }

        public void DeleteTask(int taskId)
        {
            // ищем задачу по id, если нет — просто выходим
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) return;

            // удаляем только те задачи, которые фактически не взяли в работу
            if (task.Executor != null && task.Status != TaskStatus.Assigned)
                throw new InvalidOperationException("Можно удалять только не принятые задачи");

            Tasks.Remove(task);
            Console.WriteLine($"Задача '{task.Title}' удалена из проекта");
        }

        public void ReassignTask(int taskId, Person newExecutor)
        {
            // переназначение ищет задачу и ставит исполнителя
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) return;

            // если исполнитель уже есть — переназначать нельзя (по правилам примера)
            if (task.Executor != null)
                throw new InvalidOperationException("Задача уже назначена");

            task.Executor = newExecutor;
            task.Status = TaskStatus.Assigned;
            Console.WriteLine($"Задача '{task.Title}' переназначена {newExecutor.Name}");
        }
    }

    public class TaskManagerSystem
    {
        // команда сотрудников (в примере 10 человек)
        private List<Person> _team = new List<Person>();
        // список проектов (в примере создается один)
        private List<Project> _projects = new List<Project>();

        public void Run()
        {
            // основной сценарий демонстрации системы
            Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ ЗАДАЧАМИ\n");

            CreateTeam();
            var project = CreateProject();
            CreateTasks(project);
            project.StartExecution();
            SimulateWork(project);
            ShowStatistics(project);
            Console.WriteLine("\nРАБОТА ЗАВЕРШЕНА");
        }

        private void CreateTeam()
        {
            // заполняем команду фиксированными именами и ролями
            Console.WriteLine("СОЗДАЕМ КОМАНДУ ИЗ 10 ЧЕЛОВЕК:\n");

            string[] positions = new[]
            {
                "Тимлид",
                "Бэкенд разработчик",
                "Фронтенд разработчик",
                "Fullstack разработчик",
                "QA инженер",
                "DevOps инженер",
                "Бизнес-аналитик",
                "Дизайнер UI/UX",
                "Технический писатель",
                "Архитектор"
            };

            string[] names = new[]
            {
                "Алексей Петров",
                "Мария Сидорова",
                "Дмитрий Иванов",
                "Елена Кузнецова",
                "Сергей Смирнов",
                "Анна Попова",
                "Иван Новиков",
                "Ольга Морозова",
                "Павел Волков",
                "Наталья Романова"
            };

            for (int i = 0; i < 10; i++)
            {
                _team.Add(new Person
                {
                    Id = i + 1,
                    Name = names[i],
                    Position = positions[i]
                });
                Console.WriteLine($"  {i + 1}. {_team.Last()}");
            }
            Console.WriteLine();
        }

        private Project CreateProject()
        {
            // заказчик не входит в команду, поэтому отдельный Person с другим Id
            var customer = new Person { Id = 100, Name = "ООО 'ТехноКорп'", Position = "Заказчик" };
            // тимлид — первый сотрудник списка
            var teamLead = _team[0];

            var project = new Project
            {
                Id = 1,
                Title = "Разработка CRM-системы 'SalesPro'",
                Description = "Создание системы управления взаимоотношениями с клиентами",
                CreatedDate = DateTime.Now,
                Deadline = DateTime.Now.AddMonths(3),
                Customer = customer,
                TeamLead = teamLead
            };

            _projects.Add(project);
            Console.WriteLine("СОЗДАН НОВЫЙ ПРОЕКТ:");
            Console.WriteLine($"   Название: {project.Title}");
            Console.WriteLine($"   Заказчик: {customer.Name}");
            Console.WriteLine($"   Тимлид: {teamLead.Name}");
            Console.WriteLine($"   Срок: до {project.Deadline:dd.MM.yyyy}");
            Console.WriteLine($"   Статус: {project.Status}\n");

            return project;
        }

        private void CreateTasks(Project project)
        {
            Console.WriteLine("СОЗДАЕМ ЗАДАЧИ ДЛЯ КОМАНДЫ:\n");

            // задачи для каждого сотрудника (кроме тимлида)
            string[] taskTitles = new[]
            {
                "Разработка серверной части API",
                "Создание интерфейса пользователя",
                "Интеграция платежной системы",
                "Написание тестов для модулей",
                "Настройка CI/CD пайплайна",
                "Анализ требований заказчика",
                "Дизайн системы и макеты",
                "Документация API",
                "Проектирование архитектуры БД"
            };

            string[] taskDescriptions = new[]
            {
                "Реализация REST API для работы с клиентами и заказами",
                "Разработка React-компонентов для интерфейса администратора",
                "Интеграция с Яндекс.Кассой и Stripe для приема платежей",
                "Unit и интеграционные тесты для основных модулей системы",
                "Настройка Docker, Jenkins и мониторинга в облаке",
                "Сбор требований, создание user stories и use case диаграмм",
                "Создание дизайн-системы, прототипов и UI-кита",
                "Написание Swagger документации для всех endpoints",
                "Проектирование схемы базы данных и миграций"
            };

            // i стартует с 1, чтобы пропустить тимлида (он инициатор, а не исполнитель задач)
            for (int i = 1; i < _team.Count; i++)
            {
                project.CreateTask(
                    title: taskTitles[i - 1],
                    description: taskDescriptions[i - 1],
                    deadline: DateTime.Now.AddDays(20 + i * 3),
                    executor: _team[i]
                );
            }
            Console.WriteLine();
        }

        private void SimulateWork(Project project)
        {
            // демонстрация разных сценариев прохождения статусов
            Console.WriteLine("НАЧИНАЕМ РАБОЧИЙ ПРОЦЕСС:\n");

            Console.WriteLine("1. НОРМАЛЬНЫЙ ПРОЦЕСС ВЫПОЛНЕНИЯ:");
            var normalTask = project.Tasks[0];
            normalTask.TakeToWork(normalTask.Executor);
            normalTask.CreateReport("API реализован по спецификации, проведено тестирование");
            normalTask.ApproveReport(project.TeamLead);

            Console.WriteLine("\n2. ПРОЦЕСС С ДЕЛЕГИРОВАНИЕМ:");
            var delegatedTask = project.Tasks[1];
            delegatedTask.TakeToWork(delegatedTask.Executor);
            delegatedTask.DelegateTask(_team[3]);
            delegatedTask.TakeToWork(delegatedTask.Executor);
            delegatedTask.CreateReport("Интерфейс реализован, адаптивная верстка");
            delegatedTask.ApproveReport(project.TeamLead);

            Console.WriteLine("\n3. ПРОЦЕСС С ВОЗВРАТОМ ОТЧЕТА:");
            var rejectedReportTask = project.Tasks[2];
            rejectedReportTask.TakeToWork(rejectedReportTask.Executor);
            rejectedReportTask.CreateReport("Предварительная интеграция");
            rejectedReportTask.RejectReport(project.TeamLead);
            rejectedReportTask.CreateReport("Полная интеграция с тестами и документацией");
            rejectedReportTask.ApproveReport(project.TeamLead);

            Console.WriteLine("\n4. ПРОЦЕСС С ОТКЛОНЕНИЕМ ЗАДАЧИ:");
            var rejectedTask = project.Tasks[3];
            rejectedTask.TakeToWork(rejectedTask.Executor);
            rejectedTask.RejectTask();
            Console.WriteLine("   → Тимлид переназначает задачу другому сотруднику");
            project.ReassignTask(rejectedTask.Id, _team[5]);
            rejectedTask.TakeToWork(rejectedTask.Executor);
            rejectedTask.CreateReport("Написаны тесты с покрытием 85%");
            rejectedTask.ApproveReport(project.TeamLead);

            Console.WriteLine("\n5. ПРОЦЕСС С УДАЛЕНИЕМ ЗАДАЧИ:");
            var deletedTask = project.Tasks[4];
            deletedTask.TakeToWork(deletedTask.Executor);
            deletedTask.RejectTask();
            Console.WriteLine("   → Тимлид принимает решение удалить задачу");
            project.DeleteTask(deletedTask.Id);

            Console.WriteLine("\n6. ВЫПОЛНЕНИЕ ОСТАЛЬНЫХ ЗАДАЧ:");
            for (int i = 5; i < project.Tasks.Count; i++)
            {
                var task = project.Tasks[i];
                // выполняем только задачи, у которых есть исполнитель
                if (task.Executor != null)
                {
                    task.TakeToWork(task.Executor);
                    task.CreateReport($"Задача '{task.Title}' выполнена");
                    task.ApproveReport(project.TeamLead);
                }
            }
        }

        private void ShowStatistics(Project project)
        {
            // используем LINQ для подсчетов и группировки по исполнителям
            Console.WriteLine("\n СТАТИСТИКА ПРОЕКТА:");
            Console.WriteLine("══════════════════════════════════");
            Console.WriteLine($"Проект: {project.Title}");
            Console.WriteLine($"Статус: {project.Status}");
            Console.WriteLine($"Всего задач: {project.Tasks.Count}");
            Console.WriteLine($"Выполнено: {project.Tasks.Count(t => t.IsCompleted)}");
            Console.WriteLine($"Отчетов создано: {project.Tasks.Sum(t => t.Reports.Count)}");

            Console.WriteLine("\nРАСПРЕДЕЛЕНИЕ ЗАДАЧ:");
            foreach (var person in _team)
            {
                // выбираем задачи конкретного сотрудника
                var tasks = project.Tasks.Where(t => t.Executor?.Id == person.Id).ToList();
                if (tasks.Any())
                {
                    Console.WriteLine($"  {person.Name}: {tasks.Count} задач");
                }
            }

            // пробуем закрыть проект, если все задачи завершены
            if (project.CanBeClosed())
            {
                Console.WriteLine("\nВСЕ ЗАДАЧИ ВЫПОЛНЕНЫ!");
                project.CloseProject();
            }
            else
            {
                Console.WriteLine($"\nПроект не может быть закрыт: " +
                    $"{project.Tasks.Count(t => !t.IsCompleted)} задач не выполнено");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // точка входа: запускаем демонстрационный сценарий
            var taskManager = new TaskManagerSystem();
            taskManager.Run();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
