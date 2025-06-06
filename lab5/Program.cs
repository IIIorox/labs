using System.Collections;

namespace lab5
{
    class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public double AverageGrade { get; set; }

        public Student(string name, double averageGrade)
        {
            Name = name;
            AverageGrade = averageGrade;
        }

        public override string ToString() => $"{Name} ({AverageGrade})";

        public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            return AverageGrade.CompareTo(other.AverageGrade);
        }
    }

    class Book
    {
        public string Title { get; set; }
        public Book(string title) => Title = title;
    }

    class BookCollection : IEnumerable<Book>
    {
        private List<Book> books = new();

        public void Add(Book book) => books.Add(book);

        public IEnumerator<Book> GetEnumerator() => books.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    class FileWriter : IDisposable
    {
        private StreamWriter writer;
        private bool disposed = false;

        public FileWriter(string path)
        {
            writer = new StreamWriter(path, append: true);
        }

        public void WriteLine(string text)
        {
            if (disposed) throw new ObjectDisposedException("FileWriter");
            writer.WriteLine(text);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                writer?.Dispose();
                disposed = true;
            }
        }
    }

    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            var students = new List<Student>
            {
                new Student("Kto", 4.8),
                new Student("Da", 3.5),
                new Student("Net", 4.2)
            };

            students.Sort();

            foreach (var student in students)
                Console.WriteLine(student);

            Console.WriteLine("####################");

            var collection = new BookCollection
            {
                new Book("1984"),
                new Book("WOWd"),
                new Book("EEEEE")
            };

            foreach (var book in collection)
                Console.WriteLine(book.Title);
        }
    }
}