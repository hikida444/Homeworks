using System;

class Program
{
    static void Main()
    {
        // суть задачи: работа со структурами и проверка матерных слов у дедов
        {
            // создаем 5 дедов с разными фразами
            Grandpa[] grandpas = new Grandpa[5];

            grandpas[0] = new Grandpa("Иван", GrumpinessLevel.Low,
                new string[] { "Ох, молодежь...", "В наше время так не делали" });

            grandpas[1] = new Grandpa("Петр", GrumpinessLevel.Medium,
                new string[] { "Да чтоб вас!", "Бл**ь!", "Опять цены подняли!" });

            grandpas[2] = new Grandpa("Семен", GrumpinessLevel.High,
                new string[] { "Пиз**ц!", "Твари!", "Бл**ь!" });

            grandpas[3] = new Grandpa("Михаил", GrumpinessLevel.Medium,
                new string[] { "Безобразие!", "Хулиганы!" });

            grandpas[4] = new Grandpa("Алексей", GrumpinessLevel.High,
                new string[] { "Еб**ь!", "С**а!", "На**й!" });

            // список матерных слов для проверки
            string[] swearWords = { "бл**ь", "пиз**ц", "еб**ь", "с**а", "на**й" };

            Console.WriteLine("Проверка дедов на матерщину:\n");

            // проверяем каждого деда
            foreach (Grandpa grandpa in grandpas)
            {
                // создаем копию для передачи по ссылке (так как grandpa в foreach readonly)
                Grandpa tempGrandpa = grandpa;

                int Bruises = Grandpa.CheckSwearing(ref tempGrandpa, swearWords);

                Console.WriteLine($"Дед {tempGrandpa.Name}:");
                Console.WriteLine($"  Уровень ворчливости: {tempGrandpa.Grumpiness}");
                Console.WriteLine($"  Фразы: {string.Join(", ", tempGrandpa.Phrases)}");
                Console.WriteLine($"  Получено фингалов: {Bruises}");
                Console.WriteLine($"  Всего фингалов: {tempGrandpa.Bruises}");
                Console.WriteLine();
            }
        }
    }

    // перечисление для уровня ворчливости
    public enum GrumpinessLevel
    {
        Low,    
        Medium,  
        High    
    }

    // структура Дед
    public struct Grandpa
    {
        public string Name;          
        public GrumpinessLevel Grumpiness; 
        public string[] Phrases;      
        public int Bruises;           

        // конструктор
        public Grandpa(string name, GrumpinessLevel grumpiness, string[] phrases)
        {
            Name = name;
            Grumpiness = grumpiness;
            Phrases = phrases;
            Bruises = 0; 
        }

        // метод для проверки матерных слов и начисления фингалов
        public static int CheckSwearing(ref Grandpa grandpa, params string[] swearWords)
        {
            int newBruises = 0;

            foreach (string phrase in grandpa.Phrases)
            {
                bool hasSwear = false; 

                foreach (string word in swearWords)
                {
                   
                    if (phrase.ToLower().Contains(word.ToLower()))
                    {
                        hasSwear = true;
                        break; // нашли матерное слово — больше проверять не нужно
                    }
                }

                // если нашли матерное слово, увеличиваем счетчик фингалов
                if (hasSwear)
                {
                    newBruises++;
                }
            }

            grandpa.Bruises += newBruises;
            return newBruises;
        }
    }
}
