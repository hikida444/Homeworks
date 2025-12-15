namespace hw9music;

class Song
{
    // название песни
    private string name;
    // автор/исполнитель
    private string author;
    // ссылка на предыдущую песню (по сути, простая цепочка как в связном списке)
    private Song prev;

    // конструктор по умолчанию: безопасные значения, prev не задан
    public Song()
    {
        name = "";
        author = "";
        prev = null;
    }

    // конструктор: задаем название и автора, предыдущей песни нет
    public Song(string name, string author)
    {
        this.name = name;
        this.author = author;
        this.prev = null;
    }

    // конструктор: задаем название, автора и ссылку на предыдущую песню в цепочке
    public Song(string name, string author, Song prev)
    {
        this.name = name;
        this.author = author;
        this.prev = prev;
    }

    // сеттеры: меняют поля после создания объекта
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetAuthor(string author)
    {
        this.author = author;
    }

    public void SetPrev(Song prev)
    {
        this.prev = prev;
    }

    // возвращает строку для удобного вывода
    public string Title()
    {
        return $"{name} - {author}";
    }

    // печатает песню в консоль (использует Title, чтобы не дублировать форматирование)
    public void Print()
    {
        Console.WriteLine(Title());
    }

    // сравнение песен: считаем равными, если совпадают name и author
    public override bool Equals(object d)
    {
        // базовая защита: null или объект другого типа — не равны
        if (d == null || !(d is Song))
            return false;

        // приводим к Song и сравниваем поля
        Song other = (Song)d;
        return this.name == other.name && this.author == other.author;
    }

    // примечание: раз Equals переопределен, обычно стоит переопределить и GetHashCode
    // чтобы корректно работать с HashSet/Sictionary. [web:40][web:41]
}

class Program
{
    static void Main(string[] args)
    {
        // список для хранения созданных песен
        List<Song> songs = new List<Song>();

        // создаем несколько объектов и задаем поля через сеттеры
        Song s1 = new Song();
        s1.SetName("Song A");
        s1.SetAuthor("Author A");

        Song s2 = new Song();
        s2.SetName("Song A");
        s2.SetAuthor("Author A");

        Song s3 = new Song();
        s3.SetName("Song C");
        s3.SetAuthor("Author C");

        Song s4 = new Song();
        s4.SetName("Song D");
        s4.SetAuthor("Author D");

        // связываем песни в цепочку через prev (s4 <- s3 <- s2 <- s1)
        s2.SetPrev(s1);
        s3.SetPrev(s2);
        s4.SetPrev(s3);

        // добавляем в список для перебора и вывода
        songs.Add(s1);
        songs.Add(s2);
        songs.Add(s3);
        songs.Add(s4);

        Console.WriteLine("Список песен:");
        foreach (var song in songs)
        {
            song.Print();
        }

        // проверяем Equals для двух разных объектов с одинаковыми полями
        Console.WriteLine("\nСравнение первой и второй песни:");
        if (songs[0].Equals(songs[1]))
            Console.WriteLine("Песни одинаковые");
        else
            Console.WriteLine("Песни разные");

        Console.WriteLine("\nИспользование новых конструкторов");

        // создание сразу с name и author
        Song song1 = new Song("My Song", "My Author");
        Console.WriteLine($"Создана через конструктор (name, author): {song1.Title()}");

        // создание с привязкой к предыдущей песне
        Song song2 = new Song("Next Song", "Same Author", song1);
        Console.WriteLine($"Создана через конструктор (name, author, prev): {song2.Title()}");

        // создание через конструктор по умолчанию (поля пустые)
        Song mySong = new Song();
        Console.WriteLine($"\nСоздание через new Song(): {mySong.Title()}");
    }
}
